using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Layout;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Rendering.Components;
using GrapeCity.ActiveReports.Rendering.GraphicalRenderers;

namespace GrapeCity.ActiveReports.Calendar.Rendering
{
	/// <summary>
	/// Represents the calendar renderer that implements <see cref="IGraphicsRenderer"/> and <see cref="IImageRenderer"/>.
	/// </summary>
	public sealed class CalendarRenderer : IGraphicsRenderer
	{
		/// <summary>
		/// Gets the instance of the renderer.
		/// </summary>
		public static CalendarRenderer Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new CalendarRenderer();
				}
				return _instance;
			}
		}
		private static CalendarRenderer _instance;

		/// <summary>
		/// Renders a calendar content range to the canvas. It is used from the calendar designer.
		/// </summary>
		public void Render(ICalendar calendar, MonthInfo month, IDrawingCanvas canvas, RectangleF rect)
		{
			if (calendar == null)
				throw new ArgumentNullException("calendar");
			if (month == null)
				throw new ArgumentNullException("month");
			if (canvas == null)
				throw new ArgumentNullException("canvas");

			_calendarData = new CalendarData(calendar);
			this._rightToLeft = calendar.Direction == Direction.RTL;
			try
			{
				// store the canvas to use in rendering
				_canvas = canvas;

				// TODO: consider to add gaps
				// allocate the area for calendar drawing
				_calendarArea = rect;
				// allocate the area for only one month
				_monthArea = new SizeF(rect.Width, rect.Height);

				DrawMonths(CalendarData.GetMonthsRange(month, month));
			}
			finally
			{
				if (_calendarData != null)
					_calendarData.Dispose();
			}
		}

		/// <summary>
		/// Renders a calendar content range to the canvas.
		/// </summary>
		/// <param name="content">the content to render</param>
		/// <param name="canvas">the drawing canvas to use</param>
		/// <param name="rect">the calendar rectangle</param>
		public void Render(CalendarContentRange content, IDrawingCanvas canvas, RectangleF rect)
		{
			if (content == null)
				throw new ArgumentNullException("content");
			if (canvas == null)
				throw new ArgumentNullException("canvas");

			_calendarData = new CalendarData((ICalendar)content.Owner);
			this._rightToLeft = ((ICalendar)content.Owner).Direction == Direction.RTL;
			try
			{
				// store the canvas to use in rendering
				_canvas = canvas;

				// TODO: consider to add gaps
				// allocate the area for calendar drawing
				_calendarArea = rect;
				// allocate the area for only one month
				_monthArea = new SizeF(content.Owner.Width.ToTwips(), content.Owner.Height.ToTwips());

				DrawMonths(CalendarData.GetMonthsRange(content.MonthFrom, content.MonthTo));
			}
			finally
			{
				if (_calendarData != null)
					_calendarData.Dispose();
			}
		}

		/// <summary>
		/// Draws all months of the current calendar.
		/// </summary>
		private void DrawMonths(IEnumerable<MonthInfo> months)
		{
			try
			{
				try
				{
					Canvas.PushState();

					// draw the months of the calendar
					float topOffset = 0;
					foreach (MonthInfo month in months)
					{
						DrawMonth(month, ref topOffset);
						topOffset += CalendarData.MonthsSpace;
					}
				}
				finally
				{
					Canvas.PopState();
				}
			}
			catch (Exception ex)
			{
				using (BrushEx brush = Canvas.CreateSolidBrush(Color.FromArgb(255, 255, 255, 255)))
					Canvas.FillRectangle(brush, CalendarArea);
				using (PenEx pen = Canvas.CreatePen(Color.FromArgb(255, 255, 0, 0)))
					Canvas.DrawPolygon(pen, new PointF[]
					{
						new PointF(CalendarArea.X,CalendarArea.Y),
						new PointF(CalendarArea.X + CalendarArea.Width,CalendarArea.Y),
						new PointF(CalendarArea.X + CalendarArea.Width,CalendarArea.Y + CalendarArea.Height),
						new PointF(CalendarArea.X,CalendarArea.Y + CalendarArea.Height),
						new PointF(CalendarArea.X,CalendarArea.Y),
					});
				var font = new FontInfo(SystemFonts.DefaultFont.Name, SystemFonts.DefaultFont.SizeInPoints);
				using (BrushEx brush = Canvas.CreateSolidBrush(Color.FromArgb(255, 0, 0, 0)))
					Canvas.DrawString(ex.ToString(), font, brush, CalendarArea, StringFormatEx.GetGenericDefault);
			}
		}

		/// <summary>
		/// Draws the specified month.
		/// </summary>
		private void DrawMonth(MonthInfo month, ref float topOffset)
		{
			// draw the header of the month
			DrawMonthTitle(month, ref topOffset);

			// draw the week days
			DrawWeekDays(ref topOffset);

			// draw the weeks of the month
			DrawWeeks(month, ref topOffset);
		}

		/// <summary>
		/// Draws the month header row.
		/// </summary>
		private void DrawMonthTitle(MonthInfo month, ref float topOffset)
		{
			// calcs the rect to draw the month title row
			SizeF monthTitleRowSize = CalendarData.GetMonthTitleRowSize(MonthAreaSize);
			RectangleF monthRowRect = new RectangleF(
				CalendarArea.X, CalendarArea.Y + topOffset,
				monthTitleRowSize.Width, monthTitleRowSize.Height);
			float borderWidth = CalendarData.MonthTitleBorderStyle.LineWidth.ToTwips() / 2;

			// get month name to draw
			string monthTitle = string.Format(CalendarData.Culture,
				CalendarData.FormatString(new DateTime(month.Year, month.Month, 1), CalendarData.MonthTitleFormat));

			// fill the area 
			using (BrushEx brush = Canvas.CreateSolidBrush(CalendarData.MonthTitleBackcolor))
				Canvas.FillRectangle(brush, monthRowRect);
			// draw month title string
			var font = CalendarData.MonthTitleFontStyle.CreateFontInfo();
			using (BrushEx brush = Canvas.CreateSolidBrush(CalendarData.MonthTitleFontStyle.FontColor))
			{
				RectangleF titleRect = new RectangleF(
					monthRowRect.X + borderWidth, monthRowRect.Y + borderWidth,
					monthRowRect.Width - 2 * borderWidth, monthRowRect.Height - 2 * borderWidth);

				// HACK: we should consider the issue with drawing string later. the issue comes about in the designer when caledar has small width to fit titles. --SergeyP
				Canvas.PushState();
				Canvas.DrawString(monthTitle, font, brush, titleRect, ToEx(CalendarData.MonthTitleStringFormat));
				Canvas.PopState();
			}
			// draw the borders
			using (PenEx pen = LineStyle.CreatePen(Canvas, CalendarData.MonthTitleBorderStyle))
			{
				pen.Alignment = PenAlignment.Center;
				DrawRectangle(Canvas, pen, monthRowRect.X, monthRowRect.Y,
					monthRowRect.Width, monthRowRect.Height - borderWidth); // shift the bottom border to avoid overlapping
			}

			// set offset
			topOffset += monthTitleRowSize.Height;
		}

		/// <summary>
		/// Draws the week day names.
		/// </summary>
		private void DrawWeekDays(ref float topOffset)
		{
			// calc the rect of the week day names row
			SizeF weekDaysRowSize = CalendarData.GetDaysOfWeekRowSize(MonthAreaSize);
			RectangleF weekDaysRowRect = new RectangleF(
				CalendarArea.X, CalendarArea.Y + topOffset,
				weekDaysRowSize.Width, weekDaysRowSize.Height);

			SizeF dayCellSize = CalendarData.GetDayCellSize(weekDaysRowSize);

			using (BrushEx brush = Canvas.CreateSolidBrush(CalendarData.DayHeadersBackcolor))
				Canvas.FillRectangle(brush, weekDaysRowRect);

			// draw the week days how they represented in month
			var font = CalendarData.DayHeadersFontStyle.CreateFontInfo();
			using (BrushEx brush = Canvas.CreateSolidBrush(CalendarData.DayHeadersFontStyle.FontColor))
			using (PenEx pen = LineStyle.CreatePen(Canvas, CalendarData.DayHeadersBorderStyle))
			{
				for (int i = 0; i < CalendarData.DaysInWeek; i++)
				{
					RectangleF dayCellRect;
					if (!_rightToLeft)
					{
						dayCellRect = new RectangleF(
							weekDaysRowRect.X + (i * dayCellSize.Width),
							weekDaysRowRect.Y,
							dayCellSize.Width,
							dayCellSize.Height);
					}
					else
					{
						dayCellRect = new RectangleF(
							(weekDaysRowRect.X + weekDaysRowRect.Width) - ((i + 1) * dayCellSize.Width),
							weekDaysRowRect.Y,
							dayCellSize.Width,
							dayCellSize.Height);
					}
					var borderPath = new PathEx();
					{
						// create day-box area
						borderPath.AddLines(
							new PointF[]
								{
									new PointF(dayCellRect.X, dayCellRect.Y),
									new PointF(dayCellRect.X + dayCellRect.Width, dayCellRect.Y),
									new PointF(dayCellRect.X + dayCellRect.Width, dayCellRect.Y + dayCellRect.Height),
									new PointF(dayCellRect.X, dayCellRect.Y + dayCellRect.Height),
								});
						borderPath.CloseAllFigures();

						// the day name to draw
						string weekDayName = CalendarData.Culture.DateTimeFormat.GetDayName(CalendarData.WeekDays[i]);
						// HACK: we should consider the issue with drawing string later. the issue comes about in the designer when caledar has small width to fit titles. --SergeyP
						{
							Canvas.PushState();
							Canvas.DrawString(weekDayName, font, brush, dayCellRect, ToEx(CalendarData.DayHeadersFormat));
							Canvas.PopState();
						}
						Canvas.DrawAndFillPath(pen, null, borderPath);
						//RenderUtils.DrawRectangle(Canvas, pen, dayCellRect.X, dayCellRect.Y, dayCellRect.Width, dayCellRect.Height);
					}
				}
			}

			// set offset
			topOffset += weekDaysRowSize.Height;
		}

		/// <summary>
		/// Draws all weeks of the specified month.
		/// </summary>
		private void DrawWeeks(MonthInfo month, ref float topOffset)
		{
			SizeF weekRowSize = CalendarData.GetDefaultWeekRowSize(MonthAreaSize);
			SizeF dayCellSize = CalendarData.GetDayCellSize(weekRowSize);

			// do the first pass to collect day info objects
			List<DayInfo> days = CollectDays(month, dayCellSize, ref topOffset);
			// sort the days according to its day kind
			days.Sort(new DayInfo.Comparer());
			// render sorted days
			RenderDays(days);
			// render appointments on the top
			RenderAppointments(month, days, dayCellSize);
		}

		/// <summary>
		/// Collects the day info objects to produce rendering in one pass after sorting.
		/// </summary>
		private List<DayInfo> CollectDays(MonthInfo month, SizeF dayCellSize, ref float topOffset)
		{
			List<DayInfo> days = new List<DayInfo>();

			for (int iWeek = 0; iWeek < CalendarData.WeeksInMonth; iWeek++)
			{
				// measure the actual height of the week row
				SizeF weekRowSize = CalendarData.MeasureWeek(month, iWeek, dayCellSize);
				if (weekRowSize.Height < dayCellSize.Height)
					weekRowSize.Height = dayCellSize.Height;
				// measure the day header
				SizeF dayHeaderSize = CalendarData.GetDayHeaderSize(dayCellSize);

				for (int iDay = 0; iDay < CalendarData.DaysInWeek; iDay++)
				{
					// get the date of day to draw
					DateTime dayDate = month.GetDateOfDay(iWeek, CalendarData.WeekDays[iDay], CalendarData.FirstDayOfWeek);

					DayInfo day = new DayInfo(dayDate);
					day.DayKind = month.DetermineDayKindForDate(dayDate);
					day.Label = dayDate.Day.ToString(CalendarData.Culture);
					day.HeaderSize = dayHeaderSize;
					if (!_rightToLeft)
					{
						day.Bounds = new RectangleF(
							CalendarArea.X + (iDay * dayCellSize.Width),
							CalendarArea.Y + topOffset,
							dayCellSize.Width,
							weekRowSize.Height);
					}
					else
					{
						day.Bounds = new RectangleF(
							(CalendarArea.X + CalendarArea.Width) - ((iDay + 1) * dayCellSize.Width),
							CalendarArea.Y + topOffset,
							dayCellSize.Width,
							weekRowSize.Height);
					}

					days.Add(day);
				}

				// get offset
				//topOffset += dayHeaderSize.Height;
				topOffset += weekRowSize.Height;
			}

			return days;
		}

		/// <summary>
		/// Draws the days.
		/// </summary>
		/// <param name="days">a sorted list of days to draw</param>
		private void RenderDays(IEnumerable<DayInfo> days)
		{
			foreach (DayInfo day in days)
			{
				RectangleF dayRect = day.Bounds;

				var headerBorderPath = new PathEx();
				var borderPath = new PathEx();
				{
					// create day-box area
					borderPath.AddLines(
						new PointF[]
								{
									new PointF(dayRect.X, dayRect.Y),
									new PointF(dayRect.X + dayRect.Width, dayRect.Y),
									new PointF(dayRect.X + dayRect.Width, dayRect.Y + dayRect.Height),
									new PointF(dayRect.X, dayRect.Y + dayRect.Height),
								});
					borderPath.CloseAllFigures();

					//Fix for case 44962
					headerBorderPath.AddLines(
						new PointF[]
							{
									new PointF(dayRect.X, dayRect.Y),
									new PointF(dayRect.X + dayRect.Width, dayRect.Y),
									new PointF(dayRect.X + dayRect.Width, dayRect.Y + day.HeaderSize.Height),
									new PointF(dayRect.X, dayRect.Y + day.HeaderSize.Height),
							});
					headerBorderPath.CloseAllFigures();

					// fill day-box area
					using (BrushEx brush = Canvas.CreateSolidBrush(CalendarData.GetDayBackcolor(day.DayKind)))
					{
						Canvas.DrawAndFillPath(null, brush, borderPath);
						Canvas.DrawAndFillPath(null, brush, headerBorderPath);
					}
					// draw the day label
					TextStyle fontStyle = CalendarData.GetDayFontStyle(day.DayKind);
					var font = fontStyle.CreateFontInfo();
					using (BrushEx brush = Canvas.CreateSolidBrush(fontStyle.FontColor))
					{
						float borderWidth = CalendarData.GetDayBorderStyle(day.DayKind).LineWidth.ToTwips() / 2;
						RectangleF dayLabelRect = new RectangleF(
							dayRect.X + borderWidth, dayRect.Top + borderWidth,
							day.Bounds.Width - 2 * borderWidth, day.HeaderSize.Height - 2 * borderWidth);

						// HACK: we should consider the issue with drawing string later. the issue comes about in the designer when caledar has small width to fit titles. --SergeyP
						Canvas.PushState();
						Canvas.DrawString(day.Label, font, brush, dayLabelRect, ToEx(CalendarData.GetDayStringFormat(day.DayKind)));
						Canvas.PopState();
					}
					// draw borders
					using (PenEx pen = LineStyle.CreatePen(Canvas, CalendarData.GetDayBorderStyle(day.DayKind)))
					{
						Canvas.DrawAndFillPath(pen, null, borderPath);
						Canvas.DrawAndFillPath(pen, null, headerBorderPath);
					}
				}
			}
		}

		/// <summary>
		/// Draws the appointments of a specified week.
		/// </summary>
		private void RenderAppointments(MonthInfo month, IEnumerable<DayInfo> days, SizeF dayCellSize)
		{
			const float ImagePadding = 0.02f;

			for (int week = 0; week < CalendarData.WeeksInMonth; week++)
			{
				DateTime weekStartDate = month.GetDateOfDay(week, CalendarData.FirstDayOfWeek, CalendarData.FirstDayOfWeek);
				DateTime weekEndDate = month.GetDateOfDay(week, CalendarData.LastDayOfWeek, CalendarData.FirstDayOfWeek, true);

				CalendarData.AppointmentLayoutHelper layoutHelper = new CalendarData.AppointmentLayoutHelper(CalendarData.FirstDayOfWeek, CalendarData.AppointmentGap);
				foreach (Appointment appointment in CalendarData.GetAppointmentsInWeek(month, week))
				{
					int apptWeekDaysCount = appointment.GetDurationInPeriod(weekStartDate, weekEndDate);
					Debug.Assert(apptWeekDaysCount > 0, "The appointment should belong to the rendering week.");

					DateTime appStartDate = appointment.ArrangeStartDate(weekStartDate);
					DayInfo appStartDayInfo = DayInfo.FindDayInfo(days, appStartDate);
					Debug.Assert(appStartDayInfo != null, "Day have to be found in the collection.");

					SizeF appSize = CalendarData.MeasureAppointment(appointment, weekStartDate, weekEndDate, dayCellSize);
					float appOffset = layoutHelper.Add(appStartDate, appStartDate.AddDays(apptWeekDaysCount - 1), appSize.Height);
					RectangleF appointmentRect;
					if (!_rightToLeft)
					{
						appointmentRect = new RectangleF(
							appStartDayInfo.Bounds.X,
							appStartDayInfo.Bounds.Y + appStartDayInfo.HeaderSize.Height + appOffset,
							apptWeekDaysCount * dayCellSize.Width,
							appSize.Height);
					}
					else
					{
						appointmentRect = new RectangleF(
							  appStartDayInfo.Bounds.X - ((apptWeekDaysCount - 1) * dayCellSize.Width),
							  appStartDayInfo.Bounds.Y + appStartDayInfo.HeaderSize.Height + appOffset,
							  apptWeekDaysCount * dayCellSize.Width,
							  appSize.Height);
					}

					AddActionToInteractivityMap(appointment, appointmentRect);

					using (BrushEx backcolorBrush = Canvas.CreateSolidBrush(appointment.Backcolor))
					{
						Canvas.FillRectangle(backcolorBrush, appointmentRect);
					}
					// draw image!
					Image img = CalendarData.GetAppointmentImage(appointment);
					RectangleF appTextRect = new RectangleF(appointmentRect.X, appointmentRect.Y, appointmentRect.Width, appointmentRect.Height);
					bool drawImage = appointment.StartDate >= weekStartDate && appointment.StartDate <= weekEndDate;
					if (img != null && drawImage)
					{
						var imageStream = new MemoryStream();
						img.Save(imageStream, ImageFormat.Png);
						imageStream.Position = 0;
						using (ImageEx image = Canvas.CreateImage(new ImageInfo(imageStream, "image/png")))
						{
							float imgHeight = Math.Min(CalendarData.GetAppointmentImageSize(appointment), appointmentRect.Width);
							float imgWidth = imgHeight;
							float imgLeft;
							if (!_rightToLeft)
								imgLeft = appointmentRect.X;
							else
								imgLeft = appointmentRect.Right - imgWidth;
							float imgTop = appointmentRect.Y + ImagePadding;
							//Fix for case 44294
							Canvas.PushState();
							Canvas.IntersectClip(new RectangleF(imgLeft, imgTop, imgWidth, imgHeight));
							SizeF imgSz = CalendarData.GetAppointmentImageRealSize(appointment);
							Canvas.DrawImage(image, imgLeft, imgTop, imgSz.Width, imgSz.Height);
							Canvas.PopState();
							if (imgHeight == appointmentRect.Width)
							{
								appTextRect.Y += imgHeight;
								appTextRect.Height -= imgHeight;
							}
							else
							{
								if (!_rightToLeft)
									appTextRect.X = appointmentRect.X + imgWidth;
								appTextRect.Width = appointmentRect.Width - imgWidth;
							}
						}
					}
					// HACK: we should consider the issue with drawing string later. the issue comes about in the designer when caledar has small width to fit titles. --SergeyP
					using (StringFormat sf = Components.Calendar.CalendarData.GetAppointmentFormat(appointment, _rightToLeft))
					{
						TextStyle fontStyle = new TextStyle(appointment.FontFamily, appointment.FontSize,
							appointment.FontStyle, appointment.FontWeight, appointment.FontDecoration,
							appointment.FontColor);
						Canvas.PushState();
						var font = fontStyle.CreateFontInfo();
						using (BrushEx forecolorBrush = Canvas.CreateSolidBrush(fontStyle.FontColor))
						{
							string value = CalendarData.FormatString(appointment.Value, appointment.Format);
							Canvas.DrawString(value, font, forecolorBrush, appTextRect, ToEx(sf));
						}
						Canvas.PopState();
					}
					LineStyle borderStyle = new LineStyle(appointment.BorderColor);
					using (PenEx pen = LineStyle.CreatePen(Canvas, borderStyle))
					{
						pen.Width = 1; // HACK: temporary hack because the width is turned off for appointments
						DrawRectangle(Canvas, pen,
							appointmentRect.X, appointmentRect.Y, appointmentRect.Width, appointmentRect.Height);
					}

					RenderAction(appointment, Canvas, appointmentRect);
				}
			}
		}

		/// <summary>
		/// Draws the rectangle by the pen on the canvas.
		/// </summary>
		private static void DrawRectangle(IDrawingCanvas canvas, PenEx pen, float x, float y, float width, float height)
		{
			PointF[] polygon = new[]
				{
					new PointF(x,y),
					new PointF(x + width,y),
					new PointF(x + width,y + height),
					new PointF(x,y + height),
					new PointF(x,y),
				};
			canvas.DrawPolygon(pen, polygon);
		}

		private void RenderAction(Appointment appointment, IDrawingCanvas canvas, RectangleF area)
		{
			if (appointment.Action == null)
			{
				return;
			}

			int pageNumber = (_layoutArea != null && _layoutArea.Page != null) ? _layoutArea.Page.PageNumber : 1;

			var canvasEx = canvas as IDrawingCanvasEx;
			if (canvasEx != null)
				canvasEx.RenderAction(appointment.Action, area, pageNumber);
		}

		private void AddActionToInteractivityMap(Appointment appointment, RectangleF rect)
		{
			if (appointment == null)
			{
				Debug.Fail("appointment is null");
				return;
			}

			if (_layoutArea != null && appointment.Action != null)
			{
				_layoutArea.InteractivityAreas.Add(
					new InteractiveArea(
						appointment.Action.ActionId.ToString(CultureInfo.InvariantCulture),
						RegionEx.CreateRegion(rect),
						appointment.Action));
			}
		}

		public static StringFormatEx ToEx(StringFormat sf)
		{
			sf = sf ?? StringFormat.GenericTypographic;
			return new StringFormatEx()
			{
				Alignment = (StringAlignmentEx)(int)sf.Alignment,
				FormatFlags = (StringFormatFlagsEx)(int)sf.FormatFlags,
				LineAlignment = (StringAlignmentEx)(int)sf.LineAlignment,
				Trimming = (StringTrimmingEx)(int)sf.Trimming,
			};
		}

		#region IGraphicsRenderer implementation

		void IGraphicsRenderer.Render(GraphicsRenderContext context, ILayoutArea area)
		{
			_layoutArea = area;
			const string ErrorMsg = "IGraphicsRenderer was configured improperly.";

			CalendarContentRange content = area.ContentRange as CalendarContentRange;
			if (content == null)
			{
				Trace.TraceError(ErrorMsg);
				return;
			}

			CalendarDataRegion calendar = content.Owner as CalendarDataRegion;
			if (calendar == null)
			{
				Trace.TraceError(ErrorMsg);
				return;
			}

			if (context.DoContent) Render(content, context.Canvas, new RectangleF(area.Left, area.Top, area.Width, area.Height));
		}

		#endregion

		/// <summary>
		/// Gets the calendar settings for the rendering.
		/// </summary>
		private CalendarData CalendarData
		{
			get { return _calendarData; }
		}

		/// <summary>
		/// Returns the <see cref="IDrawingCanvas"/> instance to use in drawing.
		/// </summary>
		private IDrawingCanvas Canvas
		{
			get
			{
				Debug.Assert(_canvas != null);
				return _canvas;
			}
		}

		/// <summary>
		/// Returns a <see cref="RectangleF"/> allocated for calendar drawing.
		/// </summary>
		private RectangleF CalendarArea
		{
			get { return _calendarArea; }
		}

		/// <summary>
		/// Returns a <see cref="RectangleF"/> allocated for one month of calendar.
		/// </summary>
		private SizeF MonthAreaSize
		{
			get { return _monthArea; }
		}

		#region DayInfo

		/// <summary>
		/// Represents the helper class to render days.
		/// </summary>
		private class DayInfo
		{
			private readonly DateTime _date;
			private DayKind _dayKind;
			private string _label;
			private SizeF _headerSize;
			private RectangleF _bounds;

			/// <summary>
			/// Default ctor.
			/// </summary>
			/// <param name="date"></param>
			public DayInfo(DateTime date)
			{
				_date = date;
			}

			/// <summary>
			/// Gets the date of a day.
			/// </summary>
			public DateTime Date
			{
				get { return _date.Date; }
			}

			/// <summary>
			/// Gets or sets the day kind.
			/// </summary>
			public DayKind DayKind
			{
				get { return _dayKind; }
				set { _dayKind = value; }
			}

			/// <summary>
			/// Gets or sets the label of a day.
			/// </summary>
			public string Label
			{
				get { return _label; }
				set { _label = value; }
			}

			/// <summary>
			/// Gets or sets the header section size of day box.
			/// </summary>
			public SizeF HeaderSize
			{
				get { return _headerSize; }
				set { _headerSize = value; }
			}

			/// <summary>
			/// Gets the bounds of day box.
			/// </summary>
			public RectangleF Bounds
			{
				get { return _bounds; }
				set { _bounds = value; }
			}

			/// <summary>
			/// Returns the day or null value from day collection by specified date.
			/// </summary>
			public static DayInfo FindDayInfo(IEnumerable<DayInfo> days, DateTime date)
			{
				DayInfo foundDay = null;
				foreach (DayInfo day in days)
				{
					if (day.Date == date)
					{
						foundDay = day;
						break;
					}
				}
				return foundDay;
			}

			#region Comparer

			public sealed class Comparer : IComparer<DayInfo>
			{
				public int Compare(DayInfo day1, DayInfo day2)
				{
					const string ErrorMessage = "There is no reason to render null instance.";
					if (day1 == null)
						throw new ArgumentNullException("day1", ErrorMessage);
					if (day2 == null)
						throw new ArgumentNullException("day2", ErrorMessage);
					if (ReferenceEquals(day1, day2)) return 0;

					if (day1.DayKind != day2.DayKind)
					{
						// NOTE: we use the hardcoded order of days in DayKind enum
						return (int)day1.DayKind - (int)day2.DayKind;
					}
					return DateTime.Compare(day1.Date, day2.Date);
				}
			}

			#endregion
		}

		#endregion

		private CalendarData _calendarData;
		private IDrawingCanvas _canvas;
		private RectangleF _calendarArea;
		private SizeF _monthArea;
		private bool _rightToLeft;
		private ILayoutArea _layoutArea;
	}
}
