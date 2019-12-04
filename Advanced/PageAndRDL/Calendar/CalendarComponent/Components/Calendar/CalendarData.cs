using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using GrapeCity.ActiveReports.Calendar.Tools;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.PageReportModel;
using Image = System.Drawing.Image;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Drawing.Gdi;
using GrapeCity.ActiveReports.Drawing.Gdi.Tools;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the additional info required in drawing.
	/// </summary>
	public sealed class CalendarData : IDisposable
	{
		public const float MonthTitleFactor = 0.1f;     // 10% of calendar area height
		public const float WeekDaysTitleFactor = 0.1f;  // 10% of calendar area height
		public const int MonthsInYear = 12;
		public const int WeeksInMonth = 6;
		public const int DaysInWeek = 7;
		public const int MonthsSpace = 60;              // 3pt
		private const float DefaultHeightFactor = 20;

		private readonly ICalendar _calendar;
		private readonly Collection<DayOfWeek> _weekDays;

		/// <summary>
		/// Creates the instance and binds it to the <see cref="CalendarDataRegion"/>.
		/// </summary>
		public CalendarData(ICalendar calendar)
		{
			if (calendar == null)
				throw new ArgumentNullException("calendar");

			_calendar = calendar;
			// fill the week days collection with proper order according to the first day of week
			DayOfWeek firstDay = Culture.DateTimeFormat.FirstDayOfWeek;
			_weekDays = new Collection<DayOfWeek>();
			for (int i = 0; i < DaysInWeek; i++)
			{
				_weekDays.Add((DayOfWeek)(((int)firstDay + i) % DaysInWeek));
			}
		}

		/// <summary>
		/// Get the bound calendar.
		/// </summary>
		private ICalendar Calendar
		{
			get { return _calendar; }
		}

		/// <summary>
		/// Gets the collection of week days to iterate through.
		/// </summary>
		public Collection<DayOfWeek> WeekDays
		{
			get { return _weekDays; }
		}

		/// <summary>
		/// Gets the culture to use in the measurement and the rendering.
		/// </summary>
		public CultureInfo Culture
		{
			get
			{
				CultureInfo culture = _calendar.CalendarCultureService.GetCulture();
				if (culture != null)
					return culture;
				return CultureInfo.CurrentUICulture;
			}
		}

		public DayOfWeek FirstDayOfWeek
		{
			get { return Culture.DateTimeFormat.FirstDayOfWeek; }
		}

		public DayOfWeek LastDayOfWeek
		{
			get { return (DayOfWeek)(((int)FirstDayOfWeek + DaysInWeek - 1) % DaysInWeek); }
		}

		public static ImageStyle GetAppointmentImageStyle(Appointment appointment)
		{
			object imageValue = appointment.ImageValue;
			if (imageValue == null || (imageValue is string && string.IsNullOrEmpty((string)imageValue)))
				return null;
			return new ImageStyle(appointment.ImageSource, imageValue, appointment.MimeType);
		}

		public Image GetAppointmentImage(Appointment appointment)
		{
			ImageStyle imageStyle = GetAppointmentImageStyle(appointment);
			if (imageStyle == null) return null;
			return Calendar.GetImage(imageStyle);
		}

		public SizeF GetAppointmentImageRealSize(Appointment appointment)
		{
			Image img = GetAppointmentImage(appointment);
			if (img == null)
				return new SizeF();
			Size sz = img.Size;
			return new SizeF(RenderUtils.ConvertPixelsToTwips(sz.Width), RenderUtils.ConvertPixelsToTwips(sz.Height));
		}

		public static float GetAppointmentImageSize(Appointment appointment)
		{
			var fontDesc = new FontInfo(
				appointment.FontFamily, appointment.FontSize.ToPoints(),
				appointment.FontStyle, appointment.FontWeight, appointment.FontDecoration);
			{
				return fontDesc.Size * DefaultHeightFactor;
			}
		}

		#region Months

		/// <summary>
		/// Gets the month collection of the current calendar.
		/// </summary>
		private Collection<MonthInfo> Months
		{
			get
			{
				if (_months == null)
				{
					_months = new Collection<MonthInfo>();

					// get the first and last appointments dates 
					List<DateTime> datesList = GetSortedDatesList(Calendar.Appointments);
					DateTime dateFirst = datesList[0];
					DateTime dateLast = datesList[datesList.Count - 1];

					// NOTE: review the calculation to simplify
					// get the count of the months
					int displayMonthCount = ((dateLast.Month - dateFirst.Month) + 1);
					// if the firts and last dates in the different years
					displayMonthCount += MonthsInYear * (dateLast.Year - dateFirst.Year);

					// fill the month information
					for (int iMonth = 0; iMonth < displayMonthCount; iMonth++)
					{
						MonthInfo monthInfo = new MonthInfo(dateFirst.AddMonths(iMonth));
						DateTime startDate = ArrangeDate(new DateTime(monthInfo.Year, monthInfo.Month, 1));
						//Fix for case 44590
						//retrieving the last filler day of the month
						DateTime endDate = monthInfo.GetDateOfDay(WeeksInMonth - 1, LastDayOfWeek, FirstDayOfWeek, true);

						foreach (Appointment appointment in Calendar.Appointments)
						{
							if (appointment.StartDate <= endDate && appointment.EndDate >= startDate)
							{
								monthInfo.Appointments.Add(appointment);
							}
						}

						_months.Add(monthInfo);
					}
				}
				return _months;
			}
		}
		private Collection<MonthInfo> _months;

		private static List<DateTime> GetSortedDatesList(IEnumerable<Appointment> appts)
		{
			List<DateTime> datesList = new List<DateTime>();
			// get the sorted unique dates from the appointments
			foreach (Appointment appt in appts)
			{
				if (!datesList.Contains(appt.StartDate.Date))
					datesList.Add(appt.StartDate.Date);

				if (!datesList.Contains(appt.EndDate.Date))
					datesList.Add(appt.EndDate.Date);
			}
			datesList.Sort(Comparer<DateTime>.Default);


			// if appointments collection is empty, just fill the current month
			if (datesList.Count == 0)
			{
				datesList.Add(DateTime.Today);
			}

			return datesList;
		}

		private DateTime ArrangeDate(DateTime date)
		{
			// shift the date to the first day of week in order to display the days from the prev. month
			return date.AddDays(MonthInfo.GetDayOfWeekShift(FirstDayOfWeek, date.DayOfWeek, FirstDayOfWeek));
		}

		/// <summary>
		/// Returns true if the month has next month after in the months list, or false otherwise.
		/// </summary>
		public bool HasMonthAfter(MonthInfo monthInfo)
		{
			return Months.IndexOf(monthInfo) < Months.Count - 1;
		}

		/// <summary>
		/// Gets the first month.
		/// </summary>
		public MonthInfo GetFirstMonth()
		{
			return Months[0];
		}

		/// <summary>
		/// Gets the last month.
		/// </summary>
		public MonthInfo GetLastMonth()
		{
			return Months[Months.Count - 1];
		}

		/// <summary>
		/// Gets the next month from the months list
		/// </summary>
		public MonthInfo GetNextMonth(MonthInfo monthInfo)
		{
			return Months[Months.IndexOf(monthInfo) + 1];
		}

		/// <summary>
		/// Gets the months range from the list
		/// </summary>
		public Collection<MonthInfo> GetMonthsRange(MonthInfo monthFrom, MonthInfo monthTo)
		{
			int indexFrom = Months.IndexOf(monthFrom);
			int indexTo = Months.IndexOf(monthTo);

			Collection<MonthInfo> monthsRange = new Collection<MonthInfo>();
			for (int i = indexFrom; i >= 0 && i <= indexTo; i++)
			{
				monthsRange.Add(Months[i]);
			}
			return monthsRange;
		}

		#endregion

		#region Measurement

		public static SizeF GetMonthTitleRowSize(SizeF monthAreaSize)
		{
			return new SizeF(monthAreaSize.Width, monthAreaSize.Height * MonthTitleFactor);
		}

		public static SizeF GetDaysOfWeekRowSize(SizeF monthAreaSize)
		{
			return new SizeF(monthAreaSize.Width, monthAreaSize.Height * WeekDaysTitleFactor);
		}

		public static SizeF GetDayCellSize(SizeF dayOfWeekRowSize)
		{
			return new SizeF(dayOfWeekRowSize.Width / DaysInWeek, dayOfWeekRowSize.Height);
		}

		public static SizeF GetDefaultWeekRowSize(SizeF monthAreaSize)
		{
			// head
			float monthRowHeight = GetMonthTitleRowSize(monthAreaSize).Height;
			float daysOfWeekRowHeight = GetDaysOfWeekRowSize(monthAreaSize).Height;
			float headRowHeight = monthRowHeight + daysOfWeekRowHeight;
			// body
			float daysScopeRowHeight = monthAreaSize.Height - headRowHeight;
			float weekRowHeight = daysScopeRowHeight / WeeksInMonth;

			return new SizeF(monthAreaSize.Width, weekRowHeight);
		}

		/// <summary>
		/// Calcs size of the day header.
		/// </summary>
		public SizeF GetDayHeaderSize(SizeF dayCellSize)
		{
			// NOTE: why do we use the first month only? I think we should find more common way to calculate this
			int lastDayOfMonth = DateTime.DaysInMonth(GetFirstMonth().Year, GetFirstMonth().Month);
			string lastDayOfMonthString = lastDayOfMonth.ToString(Culture);

			SizeF dayHeaderSize = new SizeF();
			foreach (DayKind dayKind in Enum.GetValues(typeof(DayKind)))
			{
				var fontDesc = GetDayFontStyle(dayKind).CreateFontInfo();
				using (StringFormat sf = GetDayStringFormat(dayKind))
				{
					SizeF localSize = MeasureString(lastDayOfMonthString, fontDesc, dayCellSize.Width, sf);
					dayHeaderSize.Width = Math.Max(dayHeaderSize.Width, localSize.Width);
					dayHeaderSize.Height = Math.Max(dayHeaderSize.Height, localSize.Height);
				}
			}

			return dayHeaderSize;
		}

		/// <summary>
		/// Measures actual size of the month.
		/// </summary>
		public SizeF MeasureMonth(MonthInfo month, SizeF monthAreaSize)
		{
			// head
			float monthRowHeight = GetMonthTitleRowSize(monthAreaSize).Height;
			float weekDaysRowHeight = GetDaysOfWeekRowSize(monthAreaSize).Height;
			float monthHeight = monthRowHeight + weekDaysRowHeight;
			// body
			SizeF weekRowSize = GetDefaultWeekRowSize(monthAreaSize);
			SizeF dayCellSize = GetDayCellSize(weekRowSize);

			for (int week = 0; week < WeeksInMonth; week++)
			{
				// measure the actual size of the week row
				SizeF actualWeekRowSize = MeasureWeek(month, week, dayCellSize);
				// add the week row height to height of the month
				monthHeight += actualWeekRowSize.Height > weekRowSize.Height
					? actualWeekRowSize.Height : weekRowSize.Height;
			}

			return new SizeF(monthAreaSize.Width, monthHeight);
		}

		/// <summary>
		/// Measures actual size of the week row in the month.
		/// </summary>
		public SizeF MeasureWeek(MonthInfo month, int weekNumber, SizeF dayCellSize)
		{
			DateTime weekStartDate = month.GetDateOfDay(weekNumber, FirstDayOfWeek, FirstDayOfWeek);
			DateTime weekEndDate = month.GetDateOfDay(weekNumber, LastDayOfWeek, FirstDayOfWeek, true);

			SizeF dayHeader = GetDayHeaderSize(dayCellSize);
			float appointmentRowHeight = dayCellSize.Height - dayHeader.Height;

			AppointmentLayoutHelper layoutHelper = new AppointmentLayoutHelper(FirstDayOfWeek, AppointmentGap);
			foreach (Appointment appointment in GetAppointmentsInWeek(month, weekNumber))
			{
				int appWeekDaysCount = appointment.GetDurationInPeriod(weekStartDate, weekEndDate);
				Debug.Assert(appWeekDaysCount > 0, "The appointment should belong to the rendering week.");

				DateTime appStartDate = appointment.ArrangeStartDate(weekStartDate);
				DateTime appEndDate = appStartDate.AddDays(appWeekDaysCount - 1);
				// measure the height of the day basing on the appointment height and top offset
				SizeF appSize = MeasureAppointment(appointment, weekStartDate, weekEndDate, dayCellSize);
				float topOffset = layoutHelper.Add(appStartDate, appEndDate, appSize.Height);
				//If this appointment will be in the next week, save some info to help with next weeks layout.
				if (appEndDate != appointment.EndDate)
				{
					AppointmentLayoutHelper.AddCarryoverAppointment(appointment, topOffset);
				}

				appointmentRowHeight = Math.Max(topOffset + appSize.Height, appointmentRowHeight);
			}

			return new SizeF(dayCellSize.Width, appointmentRowHeight + dayHeader.Height);
		}

		/// <summary>
		/// Measures the size of the appoitnment in twips within the date range accordingly to the width of the day
		/// </summary>
		public SizeF MeasureAppointment(Appointment appointment, DateTime startDate, DateTime endDate, SizeF dayCellSize)
		{
			SizeF appSize;
			var fontDesc = new FontInfo(
				appointment.FontFamily, appointment.FontSize.ToPoints(),
				appointment.FontStyle, appointment.FontWeight, appointment.FontDecoration);
			{
				float defaultHeight = fontDesc.Size * DefaultHeightFactor;
				string value = FormatString(appointment.Value, appointment.Format);
				int duration = appointment.GetDurationInPeriod(startDate, endDate);
				float maxAppointmentWidth = duration * dayCellSize.Width;
				appSize = MeasureString(value, fontDesc, maxAppointmentWidth, GetAppointmentFormat(appointment, _calendar.Direction == Direction.RTL));
				//Case 35646 : Set a minimum height so if there isn't text, the appointment will still be rendered on the calendar.
				appSize.Height = Math.Max(appSize.Height, defaultHeight);

				if (GetAppointmentImage(appointment) != null && appointment.StartDate >= startDate && appointment.StartDate <= endDate)
				{
					// the size of the image would be the minimum from the appointment with and the default height.
					float imgSize = Math.Min(defaultHeight, appSize.Width);
					if (imgSize == defaultHeight)
					{
						maxAppointmentWidth -= imgSize;
						appSize = MeasureString(value, fontDesc, maxAppointmentWidth, GetAppointmentFormat(appointment, _calendar.Direction == Direction.RTL));
						appSize.Height = Math.Max(appSize.Height, defaultHeight);
					}
					else
					{
						appSize.Height += imgSize;
					}
				}

			}
			return appSize;
		}

		/// <summary>
		/// Measures the specified text string using specified <see cref="IFontInfo"/> object.
		/// </summary>
		private static SizeF MeasureString(string text, FontInfo font, float maxWidth, StringFormat stringFormat)
		{
			using (Graphics graphics = SafeGraphics.CreateReferenceGraphics())
			{
				graphics.PageUnit = GraphicsUnit.Point;
				graphics.PageScale = 0.05f; // 1/20
				using (var f = font.ToNative())
					return graphics.MeasureString(text, f, Convert.ToInt32(maxWidth), stringFormat);
			}
		}

		/// <summary>
		/// Gets the appointments scheduled on this week.
		/// </summary>
		public ICollection<Appointment> GetAppointmentsInWeek(MonthInfo month, int week)
		{
			DateTime startDate = month.GetDateOfDay(week, FirstDayOfWeek, FirstDayOfWeek);
			DateTime endDate = month.GetDateOfDay(week, LastDayOfWeek, FirstDayOfWeek, true);

			List<Appointment> apps = new List<Appointment>();
			foreach (Appointment appointment in month.Appointments)
			{
				if (appointment.StartDate <= endDate && appointment.EndDate >= startDate)
				{
					apps.Add(appointment);
				}
			}

			AppointmentLayoutHelper.CarryoverAppointmentComparer comparer = new AppointmentLayoutHelper.CarryoverAppointmentComparer();

			Utils.InsertionSort(apps, (x, y) => comparer.Compare(x, y));
			return apps;
		}

		#endregion

		#region Formatting

		/// <summary>
		/// Formats the given value to <see cref="string"/> type by using the current culture.
		/// </summary>
		/// <param name="value">the value to convert</param>
		/// <param name="format">the format string to use in conversion</param>
		public string FormatString(object value, string format)
		{
			return ApplyFormat(value, format, Culture);
		}

		/// <summary>
		/// Returns a string representation of the specified value using format specifier and according to the given format provider.
		/// </summary>
		private static string ApplyFormat(object value, string format, IFormatProvider formatProvider)
		{
			// Since in this case IFormattable.ToString raises an exception then we should convert the value to integral type.
			Debug.Assert(value != null, "Tried to format null value"); // added to avoid any confusion
			object convertingValue = value;
			try
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(format, formatProvider);
				}
				if (format == null)
				{
					return Convert.ToString(value, formatProvider);
				}
			}
			catch (FormatException)
			{
				convertingValue = null;
			}
			try
			{
				// Try to convert a value to integral type if previous convertions have a fault.
				if (convertingValue == null)
				{
					convertingValue = Convert.ToInt64(value);
				}
			}
			catch (InvalidCastException)
			{
				// returns the format string if the convertions are not possible
				return format;
			}

			return String.Format(formatProvider, "{0:" + format + "}", convertingValue);
		}

		#endregion

		#region Rendering arguments

		#region Month title styles

		/// <summary>
		/// Gets the backcolor for month title.
		/// </summary>
		public Color MonthTitleBackcolor
		{
			get { return Calendar.MonthTitleBackcolor; }
		}

		/// <summary>
		/// Gets the border style of month title from a bound <see cref="ICalendar"/> instance.
		/// </summary>
		public LineStyle MonthTitleBorderStyle
		{
			get { return new LineStyle(Calendar.MonthTitleBorderColor, Calendar.MonthTitleBorderWidth, Calendar.MonthTitleBorderStyle); }
		}

		/// <summary>
		/// Gets the font style of month title from a bound <see cref="ICalendar"/> instance.
		/// </summary>
		public TextStyle MonthTitleFontStyle
		{
			get
			{
				return new TextStyle(Calendar.MonthTitleFontFamily, Calendar.MonthTitleFontSize,
					Calendar.MonthTitleFontStyle, Calendar.MonthTitleFontWeight, Calendar.MonthTitleFontDecoration,
					Calendar.MonthTitleFontColor);
			}
		}

		/// <summary>
		/// Gets the string format for month title.
		/// </summary>
		public string MonthTitleFormat
		{
			get { return Calendar.MonthTitleFormat; }
		}

		/// <summary>
		/// Gets the string format for month title.
		/// </summary>
		public StringFormat MonthTitleStringFormat
		{
			get
			{
				if (_monthRowFormat == null)
				{
					TextAlign textAlign = Calendar.MonthTitleTextAlign;
					if (textAlign == TextAlign.General)
					{//Need to set to Right (far), otherwise RenderUtils.GetAlignmentString will translate General to Left (near).
						textAlign = TextAlign.Right;
					}
					_monthRowFormat = GetAlignmentString(textAlign);
					_monthRowFormat.FormatFlags = StringFormatFlags.LineLimit;
					if (_calendar.Direction == Direction.RTL)
					{
						_monthRowFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
				}
				return (StringFormat)_monthRowFormat.Clone();
			}
		}
		private StringFormat _monthRowFormat;

		#endregion

		#region Day styles

		/// <summary>
		/// Returns the backcolor for days.
		/// </summary>
		public Color GetDayBackcolor(DayKind dayKind)
		{
			switch (dayKind)
			{
				case DayKind.Filler:
					return Calendar.FillerDayBackcolor;
				case DayKind.Weekend:
					return Calendar.WeekendBackcolor;
				default:
					return Calendar.DayBackcolor;
			}
		}

		/// <summary>
		/// Returns the border style for days.
		/// </summary>
		public LineStyle GetDayBorderStyle(DayKind dayKind)
		{
			Color color;
			Length width;
			BorderStyle style;
			switch (dayKind)
			{
				case DayKind.Filler:
					color = Calendar.FillerDayBorderColor;
					width = Calendar.FillerDayBorderWidth;
					style = Calendar.FillerDayBorderStyle;
					break;
				case DayKind.Weekend:
					color = Calendar.WeekendBorderColor;
					width = Calendar.WeekendBorderWidth;
					style = Calendar.WeekendBorderStyle;
					break;
				default:
					color = Calendar.DayBorderColor;
					width = Calendar.DayBorderWidth;
					style = Calendar.DayBorderStyle;
					break;
			}
			return new LineStyle(color, width, style);
		}

		/// <summary>
		/// Returns the font style for days.
		/// </summary>
		public TextStyle GetDayFontStyle(DayKind dayKind)
		{
			string family;
			Length size;
			FontStyle style;
			FontWeight weight;
			FontDecoration decoration;
			Color color;
			switch (dayKind)
			{
				case DayKind.Filler:
					family = Calendar.FillerDayFontFamily;
					size = Calendar.FillerDayFontSize;
					style = Calendar.FillerDayFontStyle;
					weight = Calendar.FillerDayFontWeight;
					decoration = Calendar.FillerDayFontDecoration;
					color = Calendar.FillerDayFontColor;
					break;
				case DayKind.Weekend:
					family = Calendar.WeekendFontFamily;
					size = Calendar.WeekendFontSize;
					style = Calendar.WeekendFontStyle;
					weight = Calendar.WeekendFontWeight;
					decoration = Calendar.WeekendFontDecoration;
					color = Calendar.WeekendFontColor;
					break;
				default:
					family = Calendar.DayFontFamily;
					size = Calendar.DayFontSize;
					style = Calendar.DayFontStyle;
					weight = Calendar.DayFontWeight;
					decoration = Calendar.DayFontDecoration;
					color = Calendar.DayFontColor;
					break;
			}

			return new TextStyle(family, size, style, weight, decoration, color);
		}

		/// <summary>
		/// Gets the string format for days.
		/// </summary>
		public StringFormat GetDayStringFormat(DayKind dayKind)
		{
			StringFormat dayFormat;
			switch (dayKind)
			{
				case DayKind.Filler:
					{
						TextAlign textAlign = Calendar.FillerDayTextAlign;
						if (textAlign == TextAlign.General)
						{//Need to set to Right (far), otherwise RenderUtils.GetAlignmentString will translate General to Left (near).
							textAlign = TextAlign.Right;
						}
						dayFormat = GetAlignmentString(textAlign);
					}
					break;
				case DayKind.Weekend:
					{
						TextAlign textAlign = Calendar.WeekendTextAlign;
						if (textAlign == TextAlign.General)
						{//Need to set to Right (far), otherwise RenderUtils.GetAlignmentString will translate General to Left (near).
							textAlign = TextAlign.Right;
						}
						dayFormat = GetAlignmentString(textAlign);
					}
					break;
				default:
					{
						TextAlign textAlign = Calendar.DayTextAlign;
						if (textAlign == TextAlign.General)
						{//Need to set to Right (far), otherwise RenderUtils.GetAlignmentString will translate General to Left (near).
							textAlign = TextAlign.Right;
						}
						dayFormat = GetAlignmentString(textAlign, Calendar.DayVerticalAlign);
					}
					break;
			}
			dayFormat.FormatFlags = StringFormatFlags.LineLimit;
			if (_calendar.Direction == Direction.RTL)
			{
				dayFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}

			return dayFormat;
		}

		#endregion

		#region Day Headers Styles
		/// <summary>
		/// Gets the backcolor for the day headers.
		/// </summary>
		public Color DayHeadersBackcolor
		{
			get { return Calendar.DayHeadersBackcolor; }
		}

		/// <summary>
		/// Gets the border style of the day headers.
		/// </summary>
		public LineStyle DayHeadersBorderStyle
		{
			get { return new LineStyle(Calendar.DayHeadersBorderColor, Calendar.DayHeadersBorderWidth, Calendar.DayHeadersBorderStyle); }
		}

		/// <summary>
		/// Gets the font style of the day headers
		/// </summary>
		public TextStyle DayHeadersFontStyle
		{
			get
			{
				return new TextStyle(Calendar.DayHeadersFontFamily, Calendar.DayHeadersFontSize,
					Calendar.DayHeadersFontStyle, Calendar.DayHeadersFontWeight, Calendar.DayHeadersFontDecoration,
					Calendar.DayHeadersFontColor);
			}
		}
		/// <summary>
		/// Gets the format for the day headers.
		/// </summary>
		public StringFormat DayHeadersFormat
		{
			get
			{
				if (_defaultFormat == null)
				{
					_defaultFormat = GetAlignmentString(TextAlign.Center);
					_defaultFormat.FormatFlags = StringFormatFlags.LineLimit;
					_defaultFormat.Trimming = StringTrimming.Word;
					if (_calendar.Direction == Direction.RTL)
					{
						_defaultFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}
				}
				return (StringFormat)_defaultFormat.Clone();
			}
		}
		private StringFormat _defaultFormat;
		#endregion


		#region Appointment styles

		/// <summary>
		/// Returns string format for specified appointment.
		/// </summary>
		public static StringFormat GetAppointmentFormat(Appointment appointment, bool rightToLeft)
		{
			StringFormat sf = GetAlignmentString(appointment.TextAlign);
			sf.FormatFlags |= StringFormatFlags.LineLimit;
			if (rightToLeft)
			{
				sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			sf.Trimming |= StringTrimming.Word;
			return sf;
		}

		/// <summary>
		/// Gets the appointment vertical gap measured in twips.
		/// </summary>
		public float AppointmentGap
		{
			get { return Calendar.AppointmentGap.ToTwips(); }
		}

		#endregion

		private static StringFormat GetAlignmentString(TextAlign textAlign)
		{
			return GetAlignmentString(textAlign, VerticalAlign.Middle);
		}

		private static StringFormat GetAlignmentString(TextAlign textAlign, VerticalAlign verticalAlign)
		{
			// convert alignment enum values to hardcoded hashcode
			int hashcode = ((byte)verticalAlign << 8) + (byte)textAlign;

			StringFormat sf = new StringFormat();
			switch (hashcode)
			{
				case 0x0000: // VerticalAlign.Top, TextAlign.General
				case 0x0001: // VerticalAlign.Top, TextAlign.Left
					{
						sf.Alignment = StringAlignment.Near;
						sf.LineAlignment = StringAlignment.Near;
						break;
					}
				case 0x0002: // VerticalAlign.Top, TextAlign.Center
					{
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Near;
						break;
					}
				case 0x0003: // VerticalAlign.Top, TextAlign.Right
					{
						sf.Alignment = StringAlignment.Far;
						sf.LineAlignment = StringAlignment.Near;
						break;
					}
				case 0x0100: // VerticalAlign.Middle, TextAlign.General
				case 0x0101: // VerticalAlign.Middle, TextAlign.Left
					{
						sf.Alignment = StringAlignment.Near;
						sf.LineAlignment = StringAlignment.Center;
						break;
					}
				case 0x0102: // VerticalAlign.Middle, TextAlign.Center
					{
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Center;
						break;
					}
				case 0x0103: // VerticalAlign.Middle, TextAlign.Right
					{
						sf.Alignment = StringAlignment.Far;
						sf.LineAlignment = StringAlignment.Center;
						break;
					}
				case 0x0200: // VerticalAlign.Bottom, TextAlign.General
				case 0x0201: // VerticalAlign.Bottom, TextAlign.Left
					{
						sf.Alignment = StringAlignment.Near;
						sf.LineAlignment = StringAlignment.Far;
						break;
					}
				case 0x0202: // VerticalAlign.Bottom, TextAlign.Center
					{
						sf.Alignment = StringAlignment.Center;
						sf.LineAlignment = StringAlignment.Far;
						break;
					}
				case 0x0203: // VerticalAlign.Bottom, TextAlign.Right
					{
						sf.Alignment = StringAlignment.Far;
						sf.LineAlignment = StringAlignment.Far;
						break;
					}
			}
			return sf;
		}

		#endregion

		#region AppointmentLayoutHelper

		/// <summary>
		/// Provides a helper for appointment layout.
		/// </summary>
		public sealed class AppointmentLayoutHelper
		{
			private readonly DayOfWeek _firstDayOfWeek;
			private readonly Dictionary<DayOfWeek, IDictionary<int, float>> _dictionary;
			private readonly float _verticalGap;

			public AppointmentLayoutHelper(DayOfWeek firstDayOfWeek, float verticalGap)
			{
				_firstDayOfWeek = firstDayOfWeek;
				_verticalGap = verticalGap;
				_dictionary = new Dictionary<DayOfWeek, IDictionary<int, float>>();
				foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
				{
					_dictionary.Add(dayOfWeek, new Dictionary<int, float>());
				}
			}

			/// <summary>
			/// Adds the appointment to layout pool.
			/// </summary>
			/// <param name="startDate">start date of appointment in the range of one week</param>
			/// <param name="endDate">end date of appointment in the range of one week</param>
			/// <param name="height">appointment height</param>
			/// <returns>Returns the top offset to draw appointment rectangle</returns>
			public float Add(DateTime startDate, DateTime endDate, float height)
			{
				// we should perform layout in one week range only
				Debug.Assert(MonthInfo.GetDayOfWeekShift(startDate.DayOfWeek, _firstDayOfWeek, _firstDayOfWeek) >= 0
					&& MonthInfo.GetDayOfWeekShift(startDate.DayOfWeek, _firstDayOfWeek, _firstDayOfWeek) < DaysInWeek);
				Debug.Assert(MonthInfo.GetDayOfWeekShift(endDate.DayOfWeek, _firstDayOfWeek, _firstDayOfWeek) >= 0
					&& MonthInfo.GetDayOfWeekShift(endDate.DayOfWeek, _firstDayOfWeek, _firstDayOfWeek) < DaysInWeek);

				DayOfWeek startDay = startDate.DayOfWeek;
				IDictionary<int, float> layoutedRows = _dictionary[startDay];

				// find the empty row and calculate offset of empty row
				float offset = 0;
				int emptyRowIndex = 0;
				while (layoutedRows.ContainsKey(emptyRowIndex))
				{
					offset += layoutedRows[emptyRowIndex];
					offset += _verticalGap;
					emptyRowIndex++;
				}

				// add the appointment and appointment height to the pool
				for (int i = 0; i <= MonthInfo.GetDayOfWeekShift(endDate.DayOfWeek, startDay, _firstDayOfWeek); i++)
				{
					DayOfWeek currentDay = (DayOfWeek)(((int)startDay + i) % DaysInWeek);
					_dictionary[currentDay].Add(emptyRowIndex, height);
				}

				return offset;
			}

			/// <summary>
			/// Keeps a collection of Appointments that will be laid out over multiple weeks to allow
			/// for consistent ordering of the Appointments from week to week.
			/// </summary>
			public static void AddCarryoverAppointment(Appointment appointment, float offset)
			{
				if (!CarryoverAppointments.ContainsKey(appointment))
					CarryoverAppointments.Add(appointment, offset);
			}
			private static readonly Dictionary<Appointment, float> CarryoverAppointments = new Dictionary<Appointment, float>();

			#region Comparer

			/// <summary>
			/// A specialized IComparer that will sort any Appointments that have carried over from the previous week to
			/// the beginning of the collection based on where they were laid out in the previous week.
			/// </summary>
			public sealed class CarryoverAppointmentComparer : Appointment.Comparer
			{
				///<summary>
				/// Compares two objects and returns a value indicating whether one is less than, 
				/// equal to, or greater than the other.
				///</summary>
				///<param name="y">The second object to compare.</param>
				///<param name="x">The first object to compare.</param>
				///<returns>
				/// Value Condition Less than zero x is less than y. Zero x equals y. Greater than zero x is greater than y.
				///</returns>
				public override int Compare(Appointment x, Appointment y)
				{
					if (ReferenceEquals(x, y)) return 0;
					if (ReferenceEquals(x, null)) return -1;
					if (ReferenceEquals(y, null)) return 1;

					if (CarryoverAppointments.ContainsKey(x) &&
						CarryoverAppointments.ContainsKey(y))
					{
						int compare = CarryoverAppointments[x].CompareTo(CarryoverAppointments[y]);
						if (compare != 0)
							return compare;

					}
					return base.Compare(x, y);
				}
			}
			#endregion
		}

		#endregion

		#region IDisposable members

		///<summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (disposed) return;
			if (disposing)
			{
				if (_defaultFormat != null)
					_defaultFormat.Dispose();
				if (_monthRowFormat != null)
					_monthRowFormat.Dispose();
			}
			disposed = true;
		}

		private bool disposed;

		#endregion

		#region Property Names

		public const string CalendarPrefix = "calendar:";
		// Month title prop names
		public const string MonthTitleBackcolorPropertyName = CalendarPrefix + "MonthTitle.Backcolor";
		public const string MonthTitleBorderColorPropertyName = CalendarPrefix + "MonthTitle.BorderColor";
		public const string MonthTitleBorderWidthPropertyName = CalendarPrefix + "MonthTitle.BorderWidth";
		public const string MonthTitleBorderStylePropertyName = CalendarPrefix + "MonthTitle.BorderStyle";
		public const string MonthTitleFontFamilyPropertyName = CalendarPrefix + "MonthTitle.FontFamily";
		public const string MonthTitleFontSizePropertyName = CalendarPrefix + "MonthTitle.FontSize";
		public const string MonthTitleFontStylePropertyName = CalendarPrefix + "MonthTitle.FontStyle";
		public const string MonthTitleFontWeightPropertyName = CalendarPrefix + "MonthTitle.FontWeight";
		public const string MonthTitleFontDecorationPropertyName = CalendarPrefix + "MonthTitle.FontDecoration";
		public const string MonthTitleFontColorPropertyName = CalendarPrefix + "MonthTitle.FontColor";
		public const string MonthTitleTextAlignPropertyName = CalendarPrefix + "MonthTitle.TextAlign";
		public const string MonthTitleFormatPropertyName = CalendarPrefix + "MonthTitle.Format";
		// Filler day prop names
		public const string FillerDayBackcolorPropertyName = CalendarPrefix + "FillerDay.Backcolor";
		public const string FillerDayBorderColorPropertyName = CalendarPrefix + "FillerDay.BorderColor";
		public const string FillerDayBorderWidthPropertyName = CalendarPrefix + "FillerDay.BorderWidth";
		public const string FillerDayBorderStylePropertyName = CalendarPrefix + "FillerDay.BorderStyle";
		public const string FillerDayFontFamilyPropertyName = CalendarPrefix + "FillerDay.FontFamily";
		public const string FillerDayFontSizePropertyName = CalendarPrefix + "FillerDay.FontSize";
		public const string FillerDayFontStylePropertyName = CalendarPrefix + "FillerDay.FontStyle";
		public const string FillerDayFontWeightPropertyName = CalendarPrefix + "FillerDay.FontWeight";
		public const string FillerDayFontDecorationPropertyName = CalendarPrefix + "FillerDay.FontDecoration";
		public const string FillerDayFontColorPropertyName = CalendarPrefix + "FillerDay.FontColor";
		public const string FillerDayTextAlignPropertyName = CalendarPrefix + "FillerDay.TextAlign";
		public const string FillerDayVerticalAlignPropertyName = CalendarPrefix + "FillerDay.VerticalAlign";
		// General day prop names
		public const string DayBackcolorPropertyName = CalendarPrefix + "Day.Backcolor";
		public const string DayBorderColorPropertyName = CalendarPrefix + "Day.BorderColor";
		public const string DayBorderWidthPropertyName = CalendarPrefix + "Day.BorderWidth";
		public const string DayBorderStylePropertyName = CalendarPrefix + "Day.BorderStyle";
		public const string DayFontFamilyPropertyName = CalendarPrefix + "Day.FontFamily";
		public const string DayFontSizePropertyName = CalendarPrefix + "Day.FontSize";
		public const string DayFontStylePropertyName = CalendarPrefix + "Day.FontStyle";
		public const string DayFontWeightPropertyName = CalendarPrefix + "Day.FontWeight";
		public const string DayFontDecorationPropertyName = CalendarPrefix + "Day.FontDecoration";
		public const string DayFontColorPropertyName = CalendarPrefix + "Day.FontColor";
		public const string DayTextAlignPropertyName = CalendarPrefix + "Day.TextAlign";
		public const string DayVerticalAlignPropertyName = CalendarPrefix + "Day.VerticalAlign";
		// Weekend day prop names
		public const string WeekendBackcolorPropertyName = CalendarPrefix + "Weekend.Backcolor";
		public const string WeekendBorderColorPropertyName = CalendarPrefix + "Weekend.BorderColor";
		public const string WeekendBorderWidthPropertyName = CalendarPrefix + "Weekend.BorderWidth";
		public const string WeekendBorderStylePropertyName = CalendarPrefix + "Weekend.BorderStyle";
		public const string WeekendFontFamilyPropertyName = CalendarPrefix + "Weekend.FontFamily";
		public const string WeekendFontSizePropertyName = CalendarPrefix + "Weekend.FontSize";
		public const string WeekendFontStylePropertyName = CalendarPrefix + "Weekend.FontStyle";
		public const string WeekendFontWeightPropertyName = CalendarPrefix + "Weekend.FontWeight";
		public const string WeekendFontDecorationPropertyName = CalendarPrefix + "Weekend.FontDecoration";
		public const string WeekendFontColorPropertyName = CalendarPrefix + "Weekend.FontColor";
		public const string WeekendTextAlignPropertyName = CalendarPrefix + "Weekend.TextAlign";
		public const string WeekendVerticalAlignPropertyName = CalendarPrefix + "Weekend.VerticalAlign";
		// Holiday day prop names
		public const string HolidayBackcolorPropertyName = CalendarPrefix + "Holiday.Backcolor";
		public const string HolidayBorderColorPropertyName = CalendarPrefix + "Holiday.BorderColor";
		public const string HolidayBorderWidthPropertyName = CalendarPrefix + "Holiday.BorderWidth";
		public const string HolidayBorderStylePropertyName = CalendarPrefix + "Holiday.BorderStyle";
		public const string HolidayFontFamilyPropertyName = CalendarPrefix + "Holiday.FontFamily";
		public const string HolidayFontSizePropertyName = CalendarPrefix + "Holiday.FontSize";
		public const string HolidayFontStylePropertyName = CalendarPrefix + "Holiday.FontStyle";
		public const string HolidayFontWeightPropertyName = CalendarPrefix + "Holiday.FontWeight";
		public const string HolidayFontDecorationPropertyName = CalendarPrefix + "Holiday.FontDecoration";
		public const string HolidayFontColorPropertyName = CalendarPrefix + "Holiday.FontColor";
		public const string HolidayTextAlignPropertyName = CalendarPrefix + "Holiday.TextAlign";
		public const string HolidayVerticalAlignPropertyName = CalendarPrefix + "Holiday.VerticalAlign";
		// Appointment prop names
		public const string AppointmentStartDatePropertyName = CalendarPrefix + "Event.StartDate";
		public const string AppointmentEndDatePropertyName = CalendarPrefix + "Event.EndDate";
		public const string AppointmentValuePropertyName = CalendarPrefix + "Event.Value";
		public const string AppointmentBackcolorPropertyName = CalendarPrefix + "Event.Backcolor";
		public const string AppointmentBorderColorPropertyName = CalendarPrefix + "Event.BorderColor";
		public const string AppointmentFontFamilyPropertyName = CalendarPrefix + "Event.FontFamily";
		public const string AppointmentFontSizePropertyName = CalendarPrefix + "Event.FontSize";
		public const string AppointmentFontStylePropertyName = CalendarPrefix + "Event.FontStyle";
		public const string AppointmentFontWeightPropertyName = CalendarPrefix + "Event.FontWeight";
		public const string AppointmentFontDecorationPropertyName = CalendarPrefix + "Event.FontDecoration";
		public const string AppointmentFontColorPropertyName = CalendarPrefix + "Event.FontColor";
		public const string AppointmentTextAlignPropertyName = CalendarPrefix + "Event.TextAlign";
		public const string AppointmentFormatPropertyName = CalendarPrefix + "Event.Format";
		public const string AppointmentImageSourcePropertyName = CalendarPrefix + "Event.ImageSource";
		public const string AppointmentImageValuePropertyName = CalendarPrefix + "Event.ImageValue";
		public const string AppointmentMimeTypePropertyName = CalendarPrefix + "Event.MimeType";
		// The day headers properties names.
		public const string DayHeadersBackColorPropertyName = CalendarPrefix + "DayHeaders.Backcolor";
		public const string DayHeadersBorderStylePropertyName = CalendarPrefix + "DayHeaders.BorderStyle";
		public const string DayHeadersBorderColorPropertyName = CalendarPrefix + "DayHeaders.BorderColor";
		public const string DayHeadersBorderWidthPropertyName = CalendarPrefix + "DayHeaders.BorderWidth";
		public const string DayHeadersFontFamilyPropertyName = CalendarPrefix + "DayHeaders.FontFamily";
		public const string DayHeadersFontSizePropertyName = CalendarPrefix + "DayHeaders.FontSize";
		public const string DayHeadersFontStylePropertyName = CalendarPrefix + "DayHeaders.FontStyle";
		public const string DayHeadersFontWeightPropertyName = CalendarPrefix + "DayHeaders.FontWeight";
		public const string DayHeadersFontDecorationPropertyName = CalendarPrefix + "DayHeaders.FontDecoration";
		public const string DayHeadersFontColorPropertyName = CalendarPrefix + "DayHeaders.FontColor";
		//Misc
		public const string StylePropertyName = "Style";

		// Action property names
		public const string ActionHyperlinkPropertyName = CalendarPrefix + "Action.Hyperlink";
		public const string ActionBookmarkPropertyName = CalendarPrefix + "Action.BookmarkLink";
		public const string ActionDrillthroughPropertyName = CalendarPrefix + "Action.Drillthrough";
		public const string ActionDrillthroughParameterListPropertyName = CalendarPrefix + "Action.Drillthrough.ParameterList";
		public const string ActionDrillthroughParameterPropertyName = CalendarPrefix + "Action.Drillthrough.Parameter.";
		public const string ActionDrillthroughParameterValueSuffix = ".Value";
		public const string ActionDrillthroughParameterOmitSuffix = ".Omit";
		public static readonly char[] ActionDrillthroughParameterDelimeter = new[] { ',' };

		public const string FixedWidthPropertyName = CalendarPrefix + "FixedWidth";
		public const string FixedHeightPropertyName = CalendarPrefix + "FixedHeight";
		public const string OverflowNamePropertyName = CalendarPrefix + "OverflowName";

		#endregion

		#region Default values

		// Month title default values
		public static Color DefaultMonthTitleBackcolor = Color.White;
		public static Color DefaultMonthTitleBorderColor = Color.Black;
		public static Length DefaultMonthTitleBorderWidth = new Length("1pt");
		public static BorderStyle DefaultMonthTitleBorderStyle = BorderStyle.None;
		public static string DefaultMonthTitleFontFamily = "Arial";
		public static Length DefaultMonthTitleFontSize = new Length("16pt");
		public static FontStyle DefaultMonthTitleFontStyle = FontStyle.Normal;
		public static FontWeight DefaultMonthTitleFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultMonthTitleFontDecoration = FontDecoration.None;
		public static Color DefaultMonthTitleFontColor = Color.Black;
		public static TextAlign DefaultMonthTitleTextAlign = TextAlign.General;
		public static string DefaultMonthTitleFormat = "MMMM";
		// Filler day default values
		public static Color DefaultFillerDayBackcolor = Color.Gainsboro;
		public static Color DefaultFillerDayBorderColor = Color.DarkGray;
		public static Length DefaultFillerDayBorderWidth = new Length("1pt");
		public static BorderStyle DefaultFillerDayBorderStyle = BorderStyle.Solid;
		public static string DefaultFillerDayFontFamily = "Arial";
		public static Length DefaultFillerDayFontSize = new Length("9pt");
		public static FontStyle DefaultFillerDayFontStyle = FontStyle.Normal;
		public static FontWeight DefaultFillerDayFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultFillerDayFontDecoration = FontDecoration.None;
		public static Color DefaultFillerDayFontColor = Color.DimGray;
		public static TextAlign DefaultFillerDayTextAlign = TextAlign.General;
		public static VerticalAlign DefaultFillerDayVerticalAlign = VerticalAlign.Middle;
		// General day default values
		public static Color DefaultDayBackcolor = Color.White;
		public static Color DefaultDayBorderColor = Color.DarkGray;
		public static Length DefaultDayBorderWidth = new Length("1pt");
		public static BorderStyle DefaultDayBorderStyle = BorderStyle.Solid;
		public static string DefaultDayFontFamily = "Arial";
		public static Length DefaultDayFontSize = new Length("9pt");
		public static FontStyle DefaultDayFontStyle = FontStyle.Normal;
		public static FontWeight DefaultDayFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultDayFontDecoration = FontDecoration.None;
		public static Color DefaultDayFontColor = Color.Black;
		public static TextAlign DefaultDayTextAlign = TextAlign.General;
		public static VerticalAlign DefaultDayVerticalAlign = VerticalAlign.Middle;
		// Weekend day default values
		public static Color DefaultWeekendBackcolor = Color.WhiteSmoke;
		public static Color DefaultWeekendBorderColor = Color.DarkGray;
		public static Length DefaultWeekendBorderWidth = new Length("1pt");
		public static BorderStyle DefaultWeekendBorderStyle = BorderStyle.Solid;
		public static string DefaultWeekendFontFamily = "Arial";
		public static Length DefaultWeekendFontSize = new Length("9pt");
		public static FontStyle DefaultWeekendFontStyle = FontStyle.Normal;
		public static FontWeight DefaultWeekendFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultWeekendFontDecoration = FontDecoration.None;
		public static Color DefaultWeekendFontColor = Color.Black;
		public static TextAlign DefaultWeekendTextAlign = TextAlign.General;
		public static VerticalAlign DefaultWeekendVerticalAlign = VerticalAlign.Middle;
		// Holiday default values
		public static Color DefaultHolidayBackcolor = Color.Coral;
		public static Color DefaultHolidayBorderColor = Color.Black;
		public static Length DefaultHolidayBorderWidth = new Length("1pt");
		public static BorderStyle DefaultHolidayBorderStyle = BorderStyle.Solid;
		public static string DefaultHolidayFontFamily = "Arial";
		public static Length DefaultHolidayFontSize = new Length("9pt");
		public static FontStyle DefaultHolidayFontStyle = FontStyle.Normal;
		public static FontWeight DefaultHolidayFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultHolidayFontDecoration = FontDecoration.None;
		public static Color DefaultHolidayFontColor = Color.Black;
		public static TextAlign DefaultHolidayTextAlign = TextAlign.General;
		public static VerticalAlign DefaultHolidayVerticalAlign = VerticalAlign.Middle;
		// Appointment
		public static Color DefaultAppointmentBackcolor = Color.LightYellow;
		public static Color DefaultAppointmentBorderColor = Color.DarkGray;
		public static string DefaultAppointmentFontFamily = "Arial";
		public static Length DefaultAppointmentFontSize = new Length("8pt");
		public static FontStyle DefaultAppointmentFontStyle = FontStyle.Normal;
		public static FontWeight DefaultAppointmentFontWeight = FontWeight.Normal;
		public static FontDecoration DefaultAppointmentFontDecoration = FontDecoration.None;
		public static Color DefaultAppointmentFontColor = Color.Black;
		public static TextAlign DefaultAppointmentTextAlign = TextAlign.General;
		public static string DefaultAppointmentFormat = string.Empty;
		public static ImageSource DefaultAppointmentImageSource = ImageSource.External;
		public static string DefaultAppointmentImageValue = string.Empty;
		public static string DefaultAppointmentMimeType = string.Empty;
		// Day headers
		public static Color DefaultDayHeadersBackcolor = Color.Gainsboro;
		public static Color DefaultDayHeadersBorderColor = Color.DarkGray;
		public static Length DefaultDayHeadersBorderWidth = new Length("1pt");
		public static BorderStyle DefaultDayHeadersBorderStyle = BorderStyle.Solid;
		public static string DefaultDayHeadersFontFamily = "Arial";
		public static Length DefaultDayHeadersFontSize = new Length("9pt");
		public static FontStyle DefaultDayHeadersFontStyle = FontStyle.Normal;
		public static FontWeight DefaultDayHeadersFontWeight = FontWeight.Bold;
		public static FontDecoration DefaultDayHeadersFontDecoration = FontDecoration.None;
		public static Color DefaultDayHeadersFontColor = Color.Black;
		//Misc
		public static Direction DefaultDirection = Direction.LTR;

		#endregion
	}
}
