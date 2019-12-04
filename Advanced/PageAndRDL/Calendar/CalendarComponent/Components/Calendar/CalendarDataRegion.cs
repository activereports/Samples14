using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using GrapeCity.ActiveReports.Calendar.Tools;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Calendar.Layout;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using IList = System.Collections.IList;
using Image = System.Drawing.Image;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Provides the functionality of the Calendar Data Region.
	/// </summary>
	[LayoutManager(typeof(CalendarLayoutManager))]
	public sealed class CalendarDataRegion : DataRegionBase, ICalendar, ICustomReportItem, IFixedSizeItem, IReportItemRenderersFactory
	{
		private CalendarCulture _calendarCultureService;
		private ImageLocatorService _imageLocatorService;
		private IDictionary<int, IAction> _actions;

		private Length _fixedWidth;
		private Length _fixedHeight;

		/// <summary>
		/// Creates a new instance of <see cref="CalendarDataRegion" />.
		/// </summary>
		public CalendarDataRegion() { }

		/// <summary>
		/// IReportItem property implementation. Returns null.
		/// </summary>
		public override IAction Action
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Initializes the report item.
		/// </summary>
		/// <param name="dataContext">Data context associated with this report item.</param>
		/// <param name="properties">Property bag associated with this report item.</param>
		public override void Initialize(IDataScope dataContext, IPropertyBag properties)
		{
			base.Initialize(dataContext, properties);

			#region Month Title

			_monthTitleBorderWidth = CalendarData.DefaultMonthTitleBorderWidth;
			object value = properties.GetValue(CalendarData.MonthTitleBorderWidthPropertyName);
			if (value != null)
			{
				_monthTitleBorderWidth = Utils.ParseLength(Convert.ToString(value), _monthTitleBorderWidth);
			}

			_monthTitleBorderStyle = CalendarData.DefaultMonthTitleBorderStyle;
			value = properties.GetValue(CalendarData.MonthTitleBorderStylePropertyName);
			if (value != null)
			{
				_monthTitleBorderStyle = Utils.Parse(Convert.ToString(value), _monthTitleBorderStyle);
			}

			_monthTitleTextAlign = CalendarData.DefaultMonthTitleTextAlign;
			value = properties.GetValue(CalendarData.MonthTitleTextAlignPropertyName);
			if (value != null)
			{
				_monthTitleTextAlign = Utils.Parse(Convert.ToString(value), _monthTitleTextAlign);
			}

			_monthTitleFormat = CalendarData.DefaultMonthTitleFormat;
			value = PropertyBag.GetValue(CalendarData.MonthTitleFormatPropertyName);
			if (value != null)
			{
				_monthTitleFormat = Convert.ToString(value);
			}

			#endregion

			#region General Days

			_dayBorderWidth = CalendarData.DefaultDayBorderWidth;
			value = properties.GetValue(CalendarData.DayBorderWidthPropertyName);
			if (value != null)
			{
				_dayBorderWidth = Utils.ParseLength(Convert.ToString(value), _dayBorderWidth);
			}

			_dayBorderStyle = CalendarData.DefaultDayBorderStyle;
			value = properties.GetValue(CalendarData.DayBorderStylePropertyName);
			if (value != null)
			{
				_dayBorderStyle = Utils.Parse(Convert.ToString(value), _dayBorderStyle);
			}

			_dayTextAlign = CalendarData.DefaultDayTextAlign;
			value = properties.GetValue(CalendarData.DayTextAlignPropertyName);
			if (value != null)
			{
				_dayTextAlign = Utils.Parse(Convert.ToString(value), _dayTextAlign);
			}

			_dayVerticalAlign = CalendarData.DefaultDayVerticalAlign;
			value = properties.GetValue(CalendarData.DayVerticalAlignPropertyName);
			if (value != null)
			{
				_dayVerticalAlign = Utils.Parse(Convert.ToString(value), _dayVerticalAlign);
			}

			#endregion

			#region Filler Days

			_fillerDayBorderWidth = (this as ICalendar).DayBorderWidth;
			value = properties.GetValue(CalendarData.FillerDayBorderWidthPropertyName);
			if (value != null)
			{
				_fillerDayBorderWidth = Utils.ParseLength(Convert.ToString(value), _fillerDayBorderWidth);
			}

			_fillerDayBorderStyle = (this as ICalendar).DayBorderStyle;
			value = properties.GetValue(CalendarData.FillerDayBorderStylePropertyName);
			if (value != null)
			{
				_fillerDayBorderStyle = Utils.Parse(Convert.ToString(value), _fillerDayBorderStyle);
			}

			_fillerDayTextAlign = (this as ICalendar).DayTextAlign;
			value = properties.GetValue(CalendarData.FillerDayTextAlignPropertyName);
			if (value != null)
			{
				_fillerDayTextAlign = Utils.Parse(Convert.ToString(value), _fillerDayTextAlign);
			}

			_fillerDayVerticalAlign = (this as ICalendar).DayVerticalAlign;
			value = properties.GetValue(CalendarData.FillerDayVerticalAlignPropertyName);
			if (value != null)
			{
				_fillerDayVerticalAlign = Utils.Parse(Convert.ToString(value), _fillerDayVerticalAlign);
			}

			#endregion

			#region Weekend Days

			_weekendBorderWidth = (this as ICalendar).DayBorderWidth;
			value = properties.GetValue(CalendarData.WeekendBorderWidthPropertyName);
			if (value != null)
			{
				_weekendBorderWidth = Utils.ParseLength(Convert.ToString(value), _weekendBorderWidth);
			}

			_weekendBorderStyle = (this as ICalendar).DayBorderStyle;
			value = properties.GetValue(CalendarData.WeekendBorderStylePropertyName);
			if (value != null)
			{
				_weekendBorderStyle = Utils.Parse(Convert.ToString(value), _weekendBorderStyle);
			}

			_weekendTextAlign = (this as ICalendar).DayTextAlign;
			value = properties.GetValue(CalendarData.WeekendTextAlignPropertyName);
			if (value != null)
			{
				_weekendTextAlign = Utils.Parse(Convert.ToString(value), _weekendTextAlign);
			}

			_weekendVerticalAlign = (this as ICalendar).DayVerticalAlign;
			value = properties.GetValue(CalendarData.WeekendVerticalAlignPropertyName);
			if (value != null)
			{
				_weekendVerticalAlign = Utils.Parse(Convert.ToString(value), _weekendVerticalAlign);
			}

			#endregion

			#region Holidays

			_holidayBorderWidth = CalendarData.DefaultHolidayBorderWidth;
			value = properties.GetValue(CalendarData.HolidayBorderWidthPropertyName);
			if (value != null)
			{
				_holidayBorderWidth = Utils.ParseLength(Convert.ToString(value), _holidayBorderWidth);
			}

			_holidayBorderStyle = CalendarData.DefaultHolidayBorderStyle;
			value = properties.GetValue(CalendarData.HolidayBorderStylePropertyName);
			if (value != null)
			{
				_holidayBorderStyle = Utils.Parse(Convert.ToString(value), _holidayBorderStyle);
			}

			_holidayTextAlign = CalendarData.DefaultHolidayTextAlign;
			value = properties.GetValue(CalendarData.HolidayTextAlignPropertyName);
			if (value != null)
			{
				_holidayTextAlign = Utils.Parse(Convert.ToString(value), _holidayTextAlign);
			}

			_holidayVerticalAlign = CalendarData.DefaultHolidayVerticalAlign;
			value = properties.GetValue(CalendarData.HolidayVerticalAlignPropertyName);
			if (value != null)
			{
				_holidayVerticalAlign = Utils.Parse(Convert.ToString(value), _holidayVerticalAlign);
			}

			#endregion

			#region Day Headers

			_dayHeadersBorderWidth = CalendarData.DefaultDayHeadersBorderWidth;
			value = properties.GetValue(CalendarData.DayHeadersBorderWidthPropertyName);
			if (value != null)
			{
				_dayHeadersBorderWidth = Utils.ParseLength(Convert.ToString(value), _dayHeadersBorderWidth);
			}

			_dayHeadersBorderStyle = CalendarData.DefaultDayHeadersBorderStyle;
			value = properties.GetValue(CalendarData.DayHeadersBorderStylePropertyName);
			if (value != null)
			{
				_dayHeadersBorderStyle = Utils.Parse(Convert.ToString(value), _dayHeadersBorderStyle);
			}

			#endregion

			_direction = CalendarData.DefaultDirection;
			value = properties.GetValue(CalendarData.StylePropertyName);
			if (value != null && value is IStyle)
			{
				_direction = Utils.Parse(((IStyle)value).Direction, _direction);
			}

			value = properties.GetValue(CalendarData.FixedWidthPropertyName);
			if (value != null)
				_fixedWidth = Utils.ParseLength(Convert.ToString(value), Length.Empty);

			value = properties.GetValue(CalendarData.FixedHeightPropertyName);
			if (value != null)
				_fixedHeight = Utils.ParseLength(Convert.ToString(value), Length.Empty);
		}

		/// <summary>
		/// Gets a renderer of the specified type that can render this component.
		/// </summary>
		/// <param name="rendererType">The <see cref="System.Type" /> of the base class or interface that the returned renderer must implement.</param>
		/// <returns>The instance of the renderer that that provides rendering services for this component.</returns>
		public TRenderer GetRenderer<TRenderer>() where TRenderer : class, IRenderer
		{
			return typeof(TRenderer) == typeof(IGraphicsRenderer) ? new CalendarRenderer() as TRenderer : null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reportItem"></param>
		/// <param name="xPosition"></param>
		/// <param name="yPosition"></param>
		/// <param name="imageMapId"></param>
		/// <param name="button"></param>
		/// <returns></returns>
		public override ChangeResult OnClick(IReportItem reportItem, int xPosition, int yPosition, string imageMapId, MouseButton button)
		{
			if (string.IsNullOrEmpty(imageMapId))
			{
				Trace.TraceError("Invalid event ID");
			}

			int actionId;
			bool parseResult = int.TryParse(imageMapId, out actionId);
			if (!parseResult)
			{
				Trace.TraceError("unable parse event ID");
				return ChangeResult.None;
			}

			if (!AppointmentActions.ContainsKey(actionId))
			{
				Trace.TraceError("unable locate event with specified ID: " + actionId);
				return ChangeResult.None;
			}

			return new ChangeResult(AppointmentActions[actionId], ChangeType.Action);
		}

		#region Month Title

		/// <summary>
		/// Gets the backcolor of month title.
		/// </summary>
		Color ICalendar.MonthTitleBackcolor
		{
			get
			{
				_monthTitleBackcolor = CalendarData.DefaultMonthTitleBackcolor;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleBackcolorPropertyName);
				if (value != null)
				{
					_monthTitleBackcolor = Utils.ParseColor(Convert.ToString(value), _monthTitleBackcolor);
				}
				return _monthTitleBackcolor;
			}
		}
		private Color _monthTitleBackcolor;


		/// <summary>
		/// Gets the border color of month title.
		/// </summary>
		Color ICalendar.MonthTitleBorderColor
		{
			get
			{
				_monthTitleBorderColor = CalendarData.DefaultMonthTitleBorderColor;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleBorderColorPropertyName);
				if (value != null)
				{
					_monthTitleBorderColor = Utils.ParseColor(Convert.ToString(value), _monthTitleBorderColor);
				}
				return _monthTitleBorderColor;
			}
		}
		private Color _monthTitleBorderColor;

		/// <summary>
		/// Gets the border width of month title.
		/// </summary>
		Length ICalendar.MonthTitleBorderWidth
		{
			get { return _monthTitleBorderWidth; }
		}
		private Length _monthTitleBorderWidth;

		/// <summary>
		/// Gets the border style of month title.
		/// </summary>
		BorderStyle ICalendar.MonthTitleBorderStyle
		{
			get { return _monthTitleBorderStyle; }
		}
		private BorderStyle _monthTitleBorderStyle;

		/// <summary>
		/// Gets the font family of month title.
		/// </summary>
		string ICalendar.MonthTitleFontFamily
		{
			get
			{
				_monthTitleFontFamily = CalendarData.DefaultMonthTitleFontFamily;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontFamilyPropertyName);
				if (value != null)
				{
					_monthTitleFontFamily = Convert.ToString(value);
				}
				return _monthTitleFontFamily;
			}
		}
		private string _monthTitleFontFamily;

		/// <summary>
		/// Gets the font size of month title.
		/// </summary>
		Length ICalendar.MonthTitleFontSize
		{
			get
			{
				_monthTitleFontSize = CalendarData.DefaultMonthTitleFontSize;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontSizePropertyName);
				if (value != null)
				{
					_monthTitleFontSize = Utils.ParseLength(Convert.ToString(value), _monthTitleFontSize);
				}
				return _monthTitleFontSize;
			}
		}
		private Length _monthTitleFontSize;

		/// <summary>
		/// Gets the font style of month title.
		/// </summary>
		Drawing.FontStyle ICalendar.MonthTitleFontStyle
		{
			get
			{
				_monthTitleFontStyle = CalendarData.DefaultMonthTitleFontStyle;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontStylePropertyName);
				if (value != null)
				{
					_monthTitleFontStyle = Utils.Parse(Convert.ToString(value), _monthTitleFontStyle);
				}
				return _monthTitleFontStyle;
			}
		}
		private FontStyle _monthTitleFontStyle;

		/// <summary>
		/// Gets the font weight of month title.
		/// </summary>
		FontWeight ICalendar.MonthTitleFontWeight
		{
			get
			{
				_monthTitleFontWeight = CalendarData.DefaultMonthTitleFontWeight;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontWeightPropertyName);
				if (value != null)
				{
					_monthTitleFontWeight = Utils.Parse(Convert.ToString(value), _monthTitleFontWeight);
				}
				return _monthTitleFontWeight;
			}
		}
		private FontWeight _monthTitleFontWeight;

		/// <summary>
		/// Gets the font decoration of month title.
		/// </summary>
		FontDecoration ICalendar.MonthTitleFontDecoration
		{
			get
			{
				_monthTitleFontDecoration = CalendarData.DefaultMonthTitleFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontDecorationPropertyName);
				if (value != null)
				{
					_monthTitleFontDecoration = Utils.Parse(Convert.ToString(value), _monthTitleFontDecoration);
				}
				return _monthTitleFontDecoration;
			}
		}
		private FontDecoration _monthTitleFontDecoration;

		/// <summary>
		/// Gets the font color of month title.
		/// </summary>
		Color ICalendar.MonthTitleFontColor
		{
			get
			{
				_monthTitleFontColor = CalendarData.DefaultMonthTitleFontColor;
				object value = PropertyBag.GetValue(CalendarData.MonthTitleFontColorPropertyName);
				if (value != null)
				{
					_monthTitleFontColor = Utils.ParseColor(Convert.ToString(value), _monthTitleFontColor);
				}
				return _monthTitleFontColor;
			}
		}
		private Color _monthTitleFontColor;

		/// <summary>
		/// Gets the text align of month title.
		/// </summary>
		TextAlign ICalendar.MonthTitleTextAlign
		{
			get { return _monthTitleTextAlign; }
		}
		private TextAlign _monthTitleTextAlign;

		/// <summary>
		/// Gets the format of month title.
		/// </summary>
		string ICalendar.MonthTitleFormat
		{
			get { return _monthTitleFormat; }
		}
		private string _monthTitleFormat;

		#endregion

		#region Filler Days

		/// <summary>
		/// Gets the backcolor of filler day.
		/// </summary>
		Color ICalendar.FillerDayBackcolor
		{
			get
			{
				_fillerDayBackcolor = CalendarData.DefaultFillerDayBackcolor;
				object value = PropertyBag.GetValue(CalendarData.FillerDayBackcolorPropertyName);
				if (value != null)
				{
					_fillerDayBackcolor = Utils.ParseColor(Convert.ToString(value), _fillerDayBackcolor);
				}
				return _fillerDayBackcolor;
			}
		}
		private Color _fillerDayBackcolor;

		/// <summary>
		/// Gets the border color of filler day.
		/// </summary>
		Color ICalendar.FillerDayBorderColor
		{
			get
			{
				_fillerDayBorderColor = (this as ICalendar).DayBorderColor;
				object value = PropertyBag.GetValue(CalendarData.FillerDayBorderColorPropertyName);
				if (value != null)
				{
					_fillerDayBorderColor = Utils.ParseColor(Convert.ToString(value), _fillerDayBorderColor);
				}
				return _fillerDayBorderColor;
			}
		}
		private Color _fillerDayBorderColor;

		/// <summary>
		/// Gets the border width of filler day.
		/// </summary>
		Length ICalendar.FillerDayBorderWidth
		{
			get { return _fillerDayBorderWidth; }
		}
		private Length _fillerDayBorderWidth;

		/// <summary>
		/// Gets the border style of filler day.
		/// </summary>
		BorderStyle ICalendar.FillerDayBorderStyle
		{
			get { return _fillerDayBorderStyle; }
		}
		private BorderStyle _fillerDayBorderStyle;

		/// <summary>
		/// Gets the font family of filler day.
		/// </summary>
		string ICalendar.FillerDayFontFamily
		{
			get
			{
				_fillerDayFontFamily = (this as ICalendar).DayFontFamily;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontFamilyPropertyName);
				if (value != null)
				{
					_fillerDayFontFamily = Convert.ToString(value);
				}
				return _fillerDayFontFamily;
			}
		}
		private string _fillerDayFontFamily;

		/// <summary>
		/// Gets the font size of filler day.
		/// </summary>
		Length ICalendar.FillerDayFontSize
		{
			get
			{
				_fillerDayFontSize = (this as ICalendar).DayFontSize;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontSizePropertyName);
				if (value != null)
				{
					_fillerDayFontSize = Utils.ParseLength(Convert.ToString(value), _fillerDayFontSize);
				}
				return _fillerDayFontSize;
			}
		}
		private Length _fillerDayFontSize;

		/// <summary>
		/// Gets the font style of filler day.
		/// </summary>
		FontStyle ICalendar.FillerDayFontStyle
		{
			get
			{
				_fillerDayFontStyle = (this as ICalendar).DayFontStyle;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontStylePropertyName);
				if (value != null)
				{
					_fillerDayFontStyle = Utils.Parse(Convert.ToString(value), _fillerDayFontStyle);
				}
				return _fillerDayFontStyle;
			}
		}
		private FontStyle _fillerDayFontStyle;

		/// <summary>
		/// Gets the font weight of filler day.
		/// </summary>
		FontWeight ICalendar.FillerDayFontWeight
		{
			get
			{
				_fillerDayFontWeight = (this as ICalendar).DayFontWeight;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontWeightPropertyName);
				if (value != null)
				{
					_fillerDayFontWeight = Utils.Parse(Convert.ToString(value), _fillerDayFontWeight);
				}
				return _fillerDayFontWeight;
			}
		}
		private FontWeight _fillerDayFontWeight;

		/// <summary>
		/// Gets the font decoration of filler day.
		/// </summary>
		FontDecoration ICalendar.FillerDayFontDecoration
		{
			get
			{
				_fillerDayFontDecoration = (this as ICalendar).DayFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontDecorationPropertyName);
				if (value != null)
				{
					_fillerDayFontDecoration = Utils.Parse(Convert.ToString(value), _fillerDayFontDecoration);
				}
				return _fillerDayFontDecoration;
			}
		}
		private FontDecoration _fillerDayFontDecoration;

		/// <summary>
		/// Gets the font color of filler day.
		/// </summary>
		Color ICalendar.FillerDayFontColor
		{
			get
			{
				_fillerDayFontColor = CalendarData.DefaultFillerDayFontColor;
				object value = PropertyBag.GetValue(CalendarData.FillerDayFontColorPropertyName);
				if (value != null)
				{
					_fillerDayFontColor = Utils.ParseColor(Convert.ToString(value), _fillerDayFontColor);
				}
				return _fillerDayFontColor;
			}
		}
		private Color _fillerDayFontColor;

		/// <summary>
		/// Gets the text align of filler day.
		/// </summary>
		TextAlign ICalendar.FillerDayTextAlign
		{
			get { return _fillerDayTextAlign; }
		}
		private TextAlign _fillerDayTextAlign;

		/// <summary>
		/// Gets the vertical align of filler day.
		/// </summary>
		VerticalAlign ICalendar.FillerDayVerticalAlign
		{
			get { return _fillerDayVerticalAlign; }
		}
		private VerticalAlign _fillerDayVerticalAlign;

		#endregion

		#region General Days

		/// <summary>
		/// Gets the backcolor of general day.
		/// </summary>
		Color ICalendar.DayBackcolor
		{
			get
			{
				_dayBackcolor = CalendarData.DefaultDayBackcolor;
				object value = PropertyBag.GetValue(CalendarData.DayBackcolorPropertyName);
				if (value != null)
				{
					_dayBackcolor = Utils.ParseColor(Convert.ToString(value), _dayBackcolor);
				}
				return _dayBackcolor;
			}
		}
		private Color _dayBackcolor;

		/// <summary>
		/// Gets the border color of general day.
		/// </summary>
		Color ICalendar.DayBorderColor
		{
			get
			{
				_dayBorderColor = CalendarData.DefaultDayBorderColor;
				object value = PropertyBag.GetValue(CalendarData.DayBorderColorPropertyName);
				if (value != null)
				{
					_dayBorderColor = Utils.ParseColor(Convert.ToString(value), _dayBorderColor);
				}
				return _dayBorderColor;
			}
		}
		private Color _dayBorderColor;

		/// <summary>
		/// Gets the border width of general day.
		/// </summary>
		Length ICalendar.DayBorderWidth
		{
			get { return _dayBorderWidth; }
		}
		private Length _dayBorderWidth;

		/// <summary>
		/// Gets the border style of general day.
		/// </summary>
		BorderStyle ICalendar.DayBorderStyle
		{
			get { return _dayBorderStyle; }
		}
		private BorderStyle _dayBorderStyle;

		/// <summary>
		/// Gets the font family of general day.
		/// </summary>
		string ICalendar.DayFontFamily
		{
			get
			{
				_dayFontFamily = CalendarData.DefaultDayFontFamily;
				object value = PropertyBag.GetValue(CalendarData.DayFontFamilyPropertyName);
				if (value != null)
				{
					_dayFontFamily = Convert.ToString(value);
				}
				return _dayFontFamily;
			}
		}
		private string _dayFontFamily;

		/// <summary>
		/// Gets the font size of general day.
		/// </summary>
		Length ICalendar.DayFontSize
		{
			get
			{
				_dayFontSize = CalendarData.DefaultDayFontSize;
				object value = PropertyBag.GetValue(CalendarData.DayFontSizePropertyName);
				if (value != null)
				{
					_dayFontSize = Utils.ParseLength(Convert.ToString(value), _dayFontSize);
				}
				return _dayFontSize;
			}
		}
		private Length _dayFontSize;

		/// <summary>
		/// Gets the font style of general day.
		/// </summary>
		FontStyle ICalendar.DayFontStyle
		{
			get
			{
				_dayFontStyle = CalendarData.DefaultDayFontStyle;
				object value = PropertyBag.GetValue(CalendarData.DayFontStylePropertyName);
				if (value != null)
				{
					_dayFontStyle = Utils.Parse(Convert.ToString(value), _dayFontStyle);
				}
				return _dayFontStyle;
			}
		}
		private FontStyle _dayFontStyle;

		/// <summary>
		/// Gets the font weight of general day.
		/// </summary>
		FontWeight ICalendar.DayFontWeight
		{
			get
			{

				_dayFontWeight = CalendarData.DefaultDayFontWeight;
				object value = PropertyBag.GetValue(CalendarData.DayFontWeightPropertyName);
				if (value != null)
				{
					_dayFontWeight = Utils.Parse(Convert.ToString(value), _dayFontWeight);
				}
				return _dayFontWeight;
			}
		}
		private FontWeight _dayFontWeight;

		/// <summary>
		/// Gets the font decoration of general day.
		/// </summary>
		FontDecoration ICalendar.DayFontDecoration
		{
			get
			{
				_dayFontDecoration = CalendarData.DefaultDayFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.DayFontDecorationPropertyName);
				if (value != null)
				{
					_dayFontDecoration = Utils.Parse(Convert.ToString(value), _dayFontDecoration);
				}
				return _dayFontDecoration;
			}
		}
		private FontDecoration _dayFontDecoration;

		/// <summary>
		/// Gets the font color of general day.
		/// </summary>
		Color ICalendar.DayFontColor
		{
			get
			{
				_dayFontColor = CalendarData.DefaultDayFontColor;
				object value = PropertyBag.GetValue(CalendarData.DayFontColorPropertyName);
				if (value != null)
				{
					_dayFontColor = Utils.ParseColor(Convert.ToString(value), _dayFontColor);
				}
				return _dayFontColor;
			}
		}
		private Color _dayFontColor;

		/// <summary>
		/// Gets the text align of general day.
		/// </summary>
		TextAlign ICalendar.DayTextAlign
		{
			get { return _dayTextAlign; }
		}
		private TextAlign _dayTextAlign;

		/// <summary>
		/// Gets the vertical align of general day.
		/// </summary>
		VerticalAlign ICalendar.DayVerticalAlign
		{
			get { return _dayVerticalAlign; }
		}
		private VerticalAlign _dayVerticalAlign;

		#endregion

		#region Weekend Days

		/// <summary>
		/// Gets the backcolor of weekend.
		/// </summary>
		Color ICalendar.WeekendBackcolor
		{
			get
			{
				_weekendBackcolor = CalendarData.DefaultWeekendBackcolor;
				object value = PropertyBag.GetValue(CalendarData.WeekendBackcolorPropertyName);
				if (value != null)
				{
					_weekendBackcolor = Utils.ParseColor(Convert.ToString(value), _weekendBackcolor);
				}
				return _weekendBackcolor;
			}
		}
		private Color _weekendBackcolor;

		/// <summary>
		/// Gets the border color of weekend.
		/// </summary>
		Color ICalendar.WeekendBorderColor
		{
			get
			{
				_weekendBorderColor = (this as ICalendar).DayBorderColor;
				object value = PropertyBag.GetValue(CalendarData.WeekendBorderColorPropertyName);
				if (value != null)
				{
					_weekendBorderColor = Utils.ParseColor(Convert.ToString(value), _weekendBorderColor);
				}
				return _weekendBorderColor;
			}
		}
		private Color _weekendBorderColor;

		/// <summary>
		/// Gets the border width of weekend.
		/// </summary>
		Length ICalendar.WeekendBorderWidth
		{
			get { return _weekendBorderWidth; }
		}
		private Length _weekendBorderWidth;

		/// <summary>
		/// Gets the border style of weekend.
		/// </summary>
		BorderStyle ICalendar.WeekendBorderStyle
		{
			get { return _weekendBorderStyle; }
		}
		private BorderStyle _weekendBorderStyle;

		/// <summary>
		/// Gets the font family of weekend.
		/// </summary>
		string ICalendar.WeekendFontFamily
		{
			get
			{
				_weekendFontFamily = (this as ICalendar).DayFontFamily;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontFamilyPropertyName);
				if (value != null)
				{
					_weekendFontFamily = Convert.ToString(value);
				}
				return _weekendFontFamily;
			}
		}
		private string _weekendFontFamily;

		/// <summary>
		/// Gets the font size of weekend.
		/// </summary>
		Length ICalendar.WeekendFontSize
		{
			get
			{
				_weekendFontSize = (this as ICalendar).DayFontSize;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontSizePropertyName);
				if (value != null)
				{
					_weekendFontSize = Utils.ParseLength(Convert.ToString(value), _weekendFontSize);
				}
				return _weekendFontSize;
			}
		}
		private Length _weekendFontSize;

		/// <summary>
		/// Gets the font style of weekend.
		/// </summary>
		FontStyle ICalendar.WeekendFontStyle
		{
			get
			{
				_weekendFontStyle = (this as ICalendar).DayFontStyle;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontStylePropertyName);
				if (value != null)
				{
					_weekendFontStyle = Utils.Parse(Convert.ToString(value), _weekendFontStyle);
				}
				return _weekendFontStyle;
			}
		}
		private FontStyle _weekendFontStyle;

		/// <summary>
		/// Gets the font weight of weekend.
		/// </summary>
		FontWeight ICalendar.WeekendFontWeight
		{
			get
			{
				_weekendFontWeight = (this as ICalendar).DayFontWeight;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontWeightPropertyName);
				if (value != null)
				{
					_weekendFontWeight = Utils.Parse(Convert.ToString(value), _weekendFontWeight);
				}
				return _weekendFontWeight;
			}
		}
		private FontWeight _weekendFontWeight;

		/// <summary>
		/// Gets the font decoration of weekend.
		/// </summary>
		FontDecoration ICalendar.WeekendFontDecoration
		{
			get
			{
				_weekendFontDecoration = (this as ICalendar).DayFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontDecorationPropertyName);
				if (value != null)
				{
					_weekendFontDecoration = Utils.Parse(Convert.ToString(value), _weekendFontDecoration);
				}
				return _weekendFontDecoration;
			}
		}
		private FontDecoration _weekendFontDecoration;

		/// <summary>
		/// Gets the font color of weekend.
		/// </summary>
		Color ICalendar.WeekendFontColor
		{
			get
			{
				_weekendFontColor = (this as ICalendar).DayFontColor;
				object value = PropertyBag.GetValue(CalendarData.WeekendFontColorPropertyName);
				if (value != null)
				{
					_weekendFontColor = Utils.ParseColor(Convert.ToString(value), _weekendFontColor);
				}
				return _weekendFontColor;
			}
		}
		private Color _weekendFontColor;

		/// <summary>
		/// Gets the text align of weekend.
		/// </summary>
		TextAlign ICalendar.WeekendTextAlign
		{
			get
			{
				return _weekendTextAlign;
			}
		}
		private TextAlign _weekendTextAlign;

		/// <summary>
		/// Gets the vertical align of weekend.
		/// </summary>
		VerticalAlign ICalendar.WeekendVerticalAlign
		{
			get { return _weekendVerticalAlign; }
		}
		private VerticalAlign _weekendVerticalAlign;

		#endregion

		#region Holidays

		/// <summary>
		/// Gets the backcolor of holidays.
		/// </summary>
		Color ICalendar.HolidayBackcolor
		{
			get
			{
				_holidayBackcolor = CalendarData.DefaultHolidayBackcolor;
				object value = PropertyBag.GetValue(CalendarData.HolidayBackcolorPropertyName);
				if (value != null)
				{
					_holidayBackcolor = Utils.ParseColor(Convert.ToString(value), _holidayBackcolor);
				}
				return _holidayBackcolor;
			}
		}
		private Color _holidayBackcolor;

		/// <summary>
		/// Gets the border color of holidays.
		/// </summary>
		Color ICalendar.HolidayBorderColor
		{
			get
			{
				_holidayBorderColor = CalendarData.DefaultHolidayBorderColor;
				object value = PropertyBag.GetValue(CalendarData.HolidayBorderColorPropertyName);
				if (value != null)
				{
					_holidayBorderColor = Utils.ParseColor(Convert.ToString(value), _holidayBorderColor);
				}
				return _holidayBorderColor;
			}
		}
		private Color _holidayBorderColor;

		/// <summary>
		/// Gets the border width of holidays.
		/// </summary>
		Length ICalendar.HolidayBorderWidth
		{
			get { return _holidayBorderWidth; }
		}
		private Length _holidayBorderWidth;

		/// <summary>
		/// Gets the border style of holidays.
		/// </summary>
		BorderStyle ICalendar.HolidayBorderStyle
		{
			get { return _holidayBorderStyle; }
		}
		private BorderStyle _holidayBorderStyle;

		/// <summary>
		/// Gets the font family of holidays.
		/// </summary>
		string ICalendar.HolidayFontFamily
		{
			get
			{
				_holidayFontFamily = CalendarData.DefaultHolidayFontFamily;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontFamilyPropertyName);
				if (value != null)
				{
					_holidayFontFamily = Convert.ToString(value);
				}
				return _holidayFontFamily;
			}
		}
		private string _holidayFontFamily;

		/// <summary>
		/// Gets the font size of holidays.
		/// </summary>
		Length ICalendar.HolidayFontSize
		{
			get
			{
				_holidayFontSize = CalendarData.DefaultHolidayFontSize;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontSizePropertyName);
				if (value != null)
				{
					_holidayFontSize = Utils.ParseLength(Convert.ToString(value), _holidayFontSize);
				}
				return _holidayFontSize;
			}
		}
		private Length _holidayFontSize;

		/// <summary>
		/// Gets the font style of holidays.
		/// </summary>
		FontStyle ICalendar.HolidayFontStyle
		{
			get
			{
				_holidayFontStyle = CalendarData.DefaultHolidayFontStyle;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontStylePropertyName);
				if (value != null)
				{
					_holidayFontStyle = Utils.Parse(Convert.ToString(value), _holidayFontStyle);
				}
				return _holidayFontStyle;
			}
		}
		private FontStyle _holidayFontStyle;

		/// <summary>
		/// Gets the font weight of holidays.
		/// </summary>
		FontWeight ICalendar.HolidayFontWeight
		{
			get
			{
				_holidayFontWeight = CalendarData.DefaultHolidayFontWeight;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontWeightPropertyName);
				if (value != null)
				{
					_holidayFontWeight = Utils.Parse(Convert.ToString(value), _holidayFontWeight);
				}
				return _holidayFontWeight;
			}
		}
		private FontWeight _holidayFontWeight;

		/// <summary>
		/// Gets the font decoration of holidays.
		/// </summary>
		FontDecoration ICalendar.HolidayFontDecoration
		{
			get
			{
				_holidayFontDecoration = CalendarData.DefaultHolidayFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontDecorationPropertyName);
				if (value != null)
				{
					_holidayFontDecoration = Utils.Parse(Convert.ToString(value), _holidayFontDecoration);
				}
				return _holidayFontDecoration;
			}
		}
		private FontDecoration _holidayFontDecoration;

		/// <summary>
		/// Gets the font color of holidays.
		/// </summary>
		Color ICalendar.HolidayFontColor
		{
			get
			{
				_holidayFontColor = CalendarData.DefaultHolidayFontColor;
				object value = PropertyBag.GetValue(CalendarData.HolidayFontColorPropertyName);
				if (value != null)
				{
					_holidayFontColor = Utils.ParseColor(Convert.ToString(value), _holidayFontColor);
				}
				return _holidayFontColor;
			}
		}
		private Color _holidayFontColor;

		/// <summary>
		/// Gets the text align of holidays.
		/// </summary>
		TextAlign ICalendar.HolidayTextAlign
		{
			get { return _holidayTextAlign; }
		}
		private TextAlign _holidayTextAlign;

		/// <summary>
		/// Gets the vertical align of holidays.
		/// </summary>
		VerticalAlign ICalendar.HolidayVerticalAlign
		{
			get { return _holidayVerticalAlign; }
		}
		private VerticalAlign _holidayVerticalAlign;

		#endregion

		#region Appointments

		/// <summary>
		/// Gets the collection of calendar appointments.
		/// </summary>
		Collection<Appointment> ICalendar.Appointments
		{
			get
			{
				if (_appointments == null)
				{
					_appointments = new Collection<Appointment>();
					if (CustomData != null)
					{
						Extensibility.Rendering.DataGroupingCollection appointmentsData = CustomData.DataRowGroupings;
						if (appointmentsData != null && appointmentsData.Count > 0)
						{
							int actionCounter = 0;
							foreach (DataMember data in appointmentsData[0])
							{
								// read the properties
								object startDateValue = GetPropertyValue(data, CalendarData.AppointmentStartDatePropertyName);
								object endDateValue = GetPropertyValue(data, CalendarData.AppointmentEndDatePropertyName);
								object value = GetPropertyValue(data, CalendarData.AppointmentValuePropertyName);
								if (startDateValue == null)
								{
									// if StartDate was not found, the appointment should not be added in the list
									continue;
								}

								DateTime startDate;
								if (!RetrieveDate(startDateValue, out startDate))
								{
									continue;
								}

								// restore appointement object with properties values
								Appointment appt;
								if (endDateValue == null)
								{
									appt = Appointment.Create(startDate, value);
								}
								else
								{
									DateTime endDate;
									if (RetrieveDate(endDateValue, out endDate))
									{
										appt = Appointment.Create(startDate, endDate, value);
									}
									else
									{
										// if EndDate is an invalid date, act as if it were the same as StartDate										
										appt = Appointment.Create(startDate, value);
									}
								}

								appt.Action = RetrieveAction(data, actionCounter++);
								if (appt.Action != null)
								{
									AppointmentActions.Add(appt.Action.ActionId, appt.Action);
								}

								SetAppointmentStyle(data, appt);

								// add appointment to the list
								_appointments.Add(appt);
							}
						}
					}
				}
				return _appointments;
			}
		}

		private Collection<Appointment> _appointments;

		private static Action RetrieveAction(DataMember data, int actionCounter)
		{
			object value = GetPropertyValue(data, CalendarData.ActionHyperlinkPropertyName);
			if (value != null)
			{
				return Calendar.Action.CreateHyperlink(Convert.ToString(value), actionCounter);
			}

			value = GetPropertyValue(data, CalendarData.ActionBookmarkPropertyName);
			if (value != null)
			{
				return Calendar.Action.CreateBookmark(Convert.ToString(value), actionCounter);
			}

			value = GetPropertyValue(data, CalendarData.ActionDrillthroughPropertyName);
			if (value != null)
			{
				string reportName = Convert.ToString(value);
				IList parameters = RetrieveDrillThroughParameterList(data);
				IDrillthrough drillthrough = Drillthrough.Create(reportName, parameters);

				return Calendar.Action.CreateDrillthrouth(drillthrough, actionCounter);
			}

			return null;
		}

		private static IList RetrieveDrillThroughParameterList(DataMember data)
		{
			// firstly retrieve list of parameters
			object value = GetPropertyValue(data, CalendarData.ActionDrillthroughParameterListPropertyName);
			if (value == null)
			{
				return null;
			}

			string parametersList = Convert.ToString(value);
			string[] parameterNames = parametersList.Split(CalendarData.ActionDrillthroughParameterDelimeter);

			if (parameterNames.Length == 0)
			{
				return null;
			}

			IList parameters = new List<DrillthroughParameter>();

			foreach (string parameter in parameterNames)
			{
				DrillthroughParameter drillthroughParameter;
				bool success = RetrieveDrillThroughParameter(data, parameter, out drillthroughParameter);
				if (success)
				{
					parameters.Add(drillthroughParameter);
				}
			}

			return parameters;
		}

		private static bool RetrieveDrillThroughParameter(DataMember data, string parameterId,
			out DrillthroughParameter drillthroughParameter)
		{
			drillthroughParameter = new DrillthroughParameter("", null, false);

			// reading parameter name
			object value = GetPropertyValue(data, CalendarData.ActionDrillthroughParameterPropertyName + parameterId);
			if (value == null)
			{
				return false;
			}

			string parameterName = Convert.ToString(value);

			// reading parameter value
			object parameterValue = GetPropertyValue(data, CalendarData.ActionDrillthroughParameterPropertyName +
				parameterId + CalendarData.ActionDrillthroughParameterValueSuffix);

			if (parameterValue == null)
			{
				return false;
			}

			// reading parameter omit property
			value = GetPropertyValue(data, CalendarData.ActionDrillthroughParameterPropertyName +
				parameterId + CalendarData.ActionDrillthroughParameterOmitSuffix);

			bool omit;
			bool error = bool.TryParse(Convert.ToString(value), out omit);
			if (!error)
			{
				return false;
			}

			drillthroughParameter = new DrillthroughParameter(parameterName, parameterValue, omit);

			return true;
		}

		private static bool RetrieveDate(object dateValue, out DateTime dateTime)
		{
			bool isParseError = false;

			dateTime = DateTime.Now;
			try
			{
				dateTime = RenderUtils.ConvertToDateTime(dateValue);
			}
			catch (FormatException)
			{
				isParseError = true;
			}
			catch (InvalidCastException)
			{
				isParseError = true;
			}

			return !isParseError;
		}

		private IDictionary<int, IAction> AppointmentActions
		{
			get { return _actions ?? (_actions = new Dictionary<int, IAction>()); }
		}

		/// <summary>
		/// Gets the gap between appointment along y-axis.
		/// </summary>
		/// <remarks>Using 2 pixels to take into account overlap of 1 pixel border on the bottom and top of events bordering the gap.</remarks>
		Length ICalendar.AppointmentGap
		{
			get { return _appointmentGap; }
		}
		private readonly Length _appointmentGap = Length.FromTwips(RenderUtils.ConvertPixelsToTwips(2), Length.Unit.Points);

		/// <summary>
		/// Returns the value of the custom property from the specified data member if found, or null.
		/// </summary>
		private static object GetPropertyValue(DataMember data, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException("propertyName");

			CustomProperty customProperty = null;
			if (data != null)
				customProperty = data.CustomProperties[propertyName];
			if (customProperty != null)
				return customProperty.Value;

			return null;
		}

		/// <summary>
		/// Fills the appointment style from data member custom property collection.
		/// </summary>
		private static void SetAppointmentStyle(DataMember dataMember, Appointment appointment)
		{
			// backcolor
			object value = GetPropertyValue(dataMember, CalendarData.AppointmentBackcolorPropertyName);
			if (value != null)
			{
				appointment.Backcolor = Utils.ParseColor(Convert.ToString(value), appointment.Backcolor);
			}
			// border color
			value = GetPropertyValue(dataMember, CalendarData.AppointmentBorderColorPropertyName);
			if (value != null)
			{
				appointment.BorderColor = Utils.ParseColor(Convert.ToString(value), appointment.BorderColor);
			}
			// font family
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontFamilyPropertyName);
			if (value != null)
			{
				appointment.FontFamily = Convert.ToString(value);
			}
			// font size
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontSizePropertyName);
			if (value != null)
			{
				appointment.FontSize = Utils.ParseLength(Convert.ToString(value), appointment.FontSize);
			}
			// font style
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontStylePropertyName);
			if (value != null)
			{
				appointment.FontStyle = Utils.Parse(Convert.ToString(value), appointment.FontStyle);
			}
			// font weight
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontWeightPropertyName);
			if (value != null)
			{
				appointment.FontWeight = Utils.Parse(Convert.ToString(value), appointment.FontWeight);
			}
			// font decoration
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontDecorationPropertyName);
			if (value != null)
			{
				appointment.FontDecoration = Utils.Parse(Convert.ToString(value), appointment.FontDecoration);
			}
			// font color
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFontColorPropertyName);
			if (value != null)
			{
				appointment.FontColor = Utils.ParseColor(Convert.ToString(value), appointment.FontColor);
			}
			// text align
			value = GetPropertyValue(dataMember, CalendarData.AppointmentTextAlignPropertyName);
			if (value != null)
			{
				appointment.TextAlign = Utils.Parse(Convert.ToString(value), appointment.TextAlign);
			}
			// format
			value = GetPropertyValue(dataMember, CalendarData.AppointmentFormatPropertyName);
			if (value != null)
			{
				appointment.Format = Convert.ToString(value);
			}
			// image source
			value = GetPropertyValue(dataMember, CalendarData.AppointmentImageSourcePropertyName);
			if (value != null)
			{
				appointment.ImageSource = Utils.Parse(Convert.ToString(value), appointment.ImageSource);
			}
			// image value
			value = GetPropertyValue(dataMember, CalendarData.AppointmentImageValuePropertyName);
			if (value != null)
			{
				byte[] imageBytes = value as byte[];
				appointment.ImageValue = imageBytes ?? (object)Convert.ToString(value);
			}
			// mime type
			value = GetPropertyValue(dataMember, CalendarData.AppointmentMimeTypePropertyName);
			if (value != null)
			{
				appointment.MimeType = Convert.ToString(value);
			}
		}

		#endregion

		#region DayHeaders

		/// <summary>
		/// Gets the backcolor of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersBackcolor
		{
			get
			{
				_dayHeadersBackcolor = CalendarData.DefaultDayHeadersBackcolor;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersBackColorPropertyName);
				if (value != null)
				{
					_dayHeadersBackcolor = Utils.ParseColor(Convert.ToString(value), _dayHeadersBackcolor);
				}
				return _dayHeadersBackcolor;
			}
		}
		private Color _dayHeadersBackcolor;

		/// <summary>
		/// Gets the border color of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersBorderColor
		{
			get
			{
				_dayHeadersBorderColor = CalendarData.DefaultDayHeadersBorderColor;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersBorderColorPropertyName);
				if (value != null)
				{
					_dayHeadersBorderColor = Utils.ParseColor(Convert.ToString(value), _dayHeadersBorderColor);
				}
				return _dayHeadersBorderColor;
			}
		}
		private Color _dayHeadersBorderColor;

		/// <summary>
		/// Gets the border width of the day Headers.
		/// </summary>
		Length ICalendar.DayHeadersBorderWidth
		{
			get { return _dayHeadersBorderWidth; }
		}
		private Length _dayHeadersBorderWidth;

		/// <summary>
		/// Gets the border style of the day Headers.
		/// </summary>
		BorderStyle ICalendar.DayHeadersBorderStyle
		{
			get { return _dayHeadersBorderStyle; }
		}
		private BorderStyle _dayHeadersBorderStyle;

		/// <summary>
		/// Gets the font family of the day Headers.
		/// </summary>
		string ICalendar.DayHeadersFontFamily
		{
			get
			{
				_dayHeadersFontFamily = CalendarData.DefaultDayHeadersFontFamily;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontFamilyPropertyName);
				if (value != null)
				{
					_dayHeadersFontFamily = Convert.ToString(value);
				}
				return _dayHeadersFontFamily;
			}
		}
		private string _dayHeadersFontFamily;

		/// <summary>
		/// Gets the font size of the day Headers.
		/// </summary>
		Length ICalendar.DayHeadersFontSize
		{
			get
			{
				_dayHeadersFontSize = CalendarData.DefaultDayHeadersFontSize;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontSizePropertyName);
				if (value != null)
				{
					_dayHeadersFontSize = Utils.ParseLength(Convert.ToString(value), _dayHeadersFontSize);
				}
				return _dayHeadersFontSize;
			}
		}
		private Length _dayHeadersFontSize;

		/// <summary>
		/// Gets the font style of the day Headers.
		/// </summary>
		FontStyle ICalendar.DayHeadersFontStyle
		{
			get
			{
				_dayHeadersFontStyle = CalendarData.DefaultDayHeadersFontStyle;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontStylePropertyName);
				if (value != null)
				{
					_dayHeadersFontStyle = Utils.Parse(Convert.ToString(value), _dayHeadersFontStyle);
				}
				return _dayHeadersFontStyle;
			}
		}
		private FontStyle _dayHeadersFontStyle;

		/// <summary>
		/// Gets the font weight of the day Headers.
		/// </summary>
		FontWeight ICalendar.DayHeadersFontWeight
		{
			get
			{
				_dayHeadersFontWeight = CalendarData.DefaultDayHeadersFontWeight;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontWeightPropertyName);
				if (value != null)
				{
					_dayHeadersFontWeight = Utils.Parse(Convert.ToString(value), _dayHeadersFontWeight);
				}
				return _dayHeadersFontWeight;
			}
		}
		private FontWeight _dayHeadersFontWeight;

		/// <summary>
		/// Gets the font decoration of the day Headers.
		/// </summary>
		FontDecoration ICalendar.DayHeadersFontDecoration
		{
			get
			{
				_dayHeadersFontDecoration = CalendarData.DefaultDayHeadersFontDecoration;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontDecorationPropertyName);
				if (value != null)
				{
					_dayHeadersFontDecoration = Utils.Parse(Convert.ToString(value), _dayHeadersFontDecoration);
				}
				return _dayHeadersFontDecoration;
			}
		}
		private FontDecoration _dayHeadersFontDecoration;

		/// <summary>
		/// Gets the font color of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersFontColor
		{
			get
			{
				_dayHeadersFontColor = CalendarData.DefaultDayHeadersFontColor;
				object value = PropertyBag.GetValue(CalendarData.DayHeadersFontColorPropertyName);
				if (value != null)
				{
					_dayHeadersFontColor = Utils.ParseColor(Convert.ToString(value), _dayHeadersFontColor);
				}
				return _dayHeadersFontColor;
			}
		}
		private Color _dayHeadersFontColor;

		#endregion

		private IReport Report
		{
			get { return (IReport)PropertyBag.GetValue("Report"); }
		}

		/// <summary>
		/// Gets <see cref="CalendarCulture" /> service for the calendar report item at runtime.
		/// </summary>
		CalendarCulture ICalendar.CalendarCultureService
		{
			get { return _calendarCultureService ?? (_calendarCultureService = new CalendarRendererCulture(this, Report)); }
		}

		/// <summary>
		/// Obtains and returns the <see cref="System.Drawing.Image"/> by image style definition.
		/// </summary>
		/// <param name="imageStyle"></param>
		/// <returns></returns>
		Image ICalendar.GetImage(ImageStyle imageStyle)
		{
			return ImageLocatorService.GetImage(imageStyle);
		}

		private ImageLocatorService ImageLocatorService
		{
			get { return _imageLocatorService ?? (_imageLocatorService = new ImageLocatorService(Report)); }
		}

		/// <summary>
		/// Specifies the direction of text.
		/// </summary>
		Direction ICalendar.Direction
		{
			get { return _direction; }
		}

		private Direction _direction;

		#region ICustomReportItem Members

		Extensibility.Rendering.CustomData ICustomReportItem.CustomData
		{
			get { return CustomData; }
		}

		string ICustomReportItem.DataElementValue
		{
			get { return string.Empty; }
		}
		
		#endregion

		Length IFixedSizeItem.FixedWidth
		{
			get { return _fixedWidth; }
		}

		Length IFixedSizeItem.FixedHeight
		{
			get { return _fixedHeight; }
		}

		/// <summary>
		/// Overflow name
		/// </summary>
		public override string OverflowName
		{
			get { return (string)PropertyBag.GetValue(CalendarData.OverflowNamePropertyName); }
		}

		/// <summary>
		/// Overriding of System.Object method. Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current CalendarDataRegion.</returns>
		public override int GetHashCode()
		{
			return (Name + "." + DataScope.Id).GetHashCode();
		}

		/// <summary>
		/// Overriding of System.Object method. Determines whether the specified System.Object is equal to the current CalendarDataRegion.
		/// </summary>
		/// <param name="obj">The System.Object to compare with the current CalendarDataRegion.</param>
		/// <returns>true if the specified System.Object is equal to the current CalendarDataRegion; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			var calendarDataRegion = obj as CalendarDataRegion;
			if (calendarDataRegion == null)
				return false;

			return calendarDataRegion.Name == Name && calendarDataRegion.DataScope.Id == DataScope.Id;
		}
	}
}
