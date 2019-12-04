using System.Collections.ObjectModel;
using System.Drawing;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the unified interface to use in calendar rendering.
	/// </summary>
	public interface ICalendar
	{
		#region Month Title
		/// <summary>
		/// Gets the backcolor of month title.
		/// </summary>
		Color MonthTitleBackcolor { get; }
		/// <summary>
		/// Gets the border color of month title.
		/// </summary>
		Color MonthTitleBorderColor { get; }
		/// <summary>
		/// Gets the border width of month title.
		/// </summary>
		Length MonthTitleBorderWidth { get; }
		/// <summary>
		/// Gets the border style of month title.
		/// </summary>
		BorderStyle MonthTitleBorderStyle { get; }
		/// <summary>
		/// Gets the font family of month title.
		/// </summary>
		string MonthTitleFontFamily { get; }
		/// <summary>
		/// Gets the font size of month title.
		/// </summary>
		Length MonthTitleFontSize { get; }
		/// <summary>
		/// Gets the font style of month title.
		/// </summary>
		FontStyle MonthTitleFontStyle { get; }
		/// <summary>
		/// Gets the font weight of month title.
		/// </summary>
		FontWeight MonthTitleFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of month title.
		/// </summary>
		FontDecoration MonthTitleFontDecoration { get; }
		/// <summary>
		/// Gets the font color of month title.
		/// </summary>
		Color MonthTitleFontColor { get; }
		/// <summary>
		/// Gets the text align of month title.
		/// </summary>
		TextAlign MonthTitleTextAlign { get; }
		/// <summary>
		/// Gets the format of month title.
		/// </summary>
		string MonthTitleFormat { get; }

		#endregion

		#region Filler Days

		/// <summary>
		/// Gets the backcolor of filler day.
		/// </summary>
		Color FillerDayBackcolor { get; }
		/// <summary>
		/// Gets the border color of filler day.
		/// </summary>
		Color FillerDayBorderColor { get; }
		/// <summary>
		/// Gets the border width of filler day.
		/// </summary>
		Length FillerDayBorderWidth { get; }
		/// <summary>
		/// Gets the border style of filler day.
		/// </summary>
		BorderStyle FillerDayBorderStyle { get; }
		/// <summary>
		/// Gets the font family of filler day.
		/// </summary>
		string FillerDayFontFamily { get; }
		/// <summary>
		/// Gets the font size of filler day.
		/// </summary>
		Length FillerDayFontSize { get; }
		/// <summary>
		/// Gets the font style of filler day.
		/// </summary>
		FontStyle FillerDayFontStyle { get; }
		/// <summary>
		/// Gets the font weight of filler day.
		/// </summary>
		FontWeight FillerDayFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of filler day.
		/// </summary>
		FontDecoration FillerDayFontDecoration { get; }
		/// <summary>
		/// Gets the font color of filler day.
		/// </summary>
		Color FillerDayFontColor { get; }
		/// <summary>
		/// Gets the text align of filler day.
		/// </summary>
		TextAlign FillerDayTextAlign { get; }
		/// <summary>
		/// Gets the vertical align of filler day.
		/// </summary>
		VerticalAlign FillerDayVerticalAlign { get; }

		#endregion

		#region General Days

		/// <summary>
		/// Gets the backcolor of general day.
		/// </summary>
		Color DayBackcolor { get; }
		/// <summary>
		/// Gets the border color of general day.
		/// </summary>
		Color DayBorderColor { get; }
		/// <summary>
		/// Gets the border width of general day.
		/// </summary>
		Length DayBorderWidth { get; }
		/// <summary>
		/// Gets the border style of general day.
		/// </summary>
		BorderStyle DayBorderStyle { get; }
		/// <summary>
		/// Gets the font family of general day.
		/// </summary>
		string DayFontFamily { get; }
		/// <summary>
		/// Gets the font size of general day.
		/// </summary>
		Length DayFontSize { get; }
		/// <summary>
		/// Gets the font style of general day.
		/// </summary>
		FontStyle DayFontStyle { get; }
		/// <summary>
		/// Gets the font weight of general day.
		/// </summary>
		FontWeight DayFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of general day.
		/// </summary>
		FontDecoration DayFontDecoration { get; }
		/// <summary>
		/// Gets the font color of general day.
		/// </summary>
		Color DayFontColor { get; }
		/// <summary>
		/// Gets the text align of general day.
		/// </summary>
		TextAlign DayTextAlign { get; }
		/// <summary>
		/// Gets the vertical align of general day.
		/// </summary>
		VerticalAlign DayVerticalAlign { get; }

		#endregion

		#region Weekend Days

		/// <summary>
		/// Gets the backcolor of weekend.
		/// </summary>
		Color WeekendBackcolor { get; }
		/// <summary>
		/// Gets the border color of weekend.
		/// </summary>
		Color WeekendBorderColor { get; }
		/// <summary>
		/// Gets the border width of weekend.
		/// </summary>
		Length WeekendBorderWidth { get; }
		/// <summary>
		/// Gets the border style of weekend.
		/// </summary>
		BorderStyle WeekendBorderStyle { get; }
		/// <summary>
		/// Gets the font family of weekend.
		/// </summary>
		string WeekendFontFamily { get; }
		/// <summary>
		/// Gets the font size of weekend.
		/// </summary>
		Length WeekendFontSize { get; }
		/// <summary>
		/// Gets the font style of weekend.
		/// </summary>
		FontStyle WeekendFontStyle { get; }
		/// <summary>
		/// Gets the font weight of weekend.
		/// </summary>
		FontWeight WeekendFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of weekend.
		/// </summary>
		FontDecoration WeekendFontDecoration { get; }
		/// <summary>
		/// Gets the font color of weekend.
		/// </summary>
		Color WeekendFontColor { get; }
		/// <summary>
		/// Gets the text align of weekend.
		/// </summary>
		TextAlign WeekendTextAlign { get; }
		/// <summary>
		/// Gets the vertical align of weekend.
		/// </summary>
		VerticalAlign WeekendVerticalAlign { get; }

		#endregion

		#region Holidays

		/// <summary>
		/// Gets the backcolor of holidays.
		/// </summary>
		Color HolidayBackcolor { get; }
		/// <summary>
		/// Gets the border color of holidays.
		/// </summary>
		Color HolidayBorderColor { get; }
		/// <summary>
		/// Gets the border width of holidays.
		/// </summary>
		Length HolidayBorderWidth { get; }
		/// <summary>
		/// Gets the border style of holidays.
		/// </summary>
		BorderStyle HolidayBorderStyle { get; }
		/// <summary>
		/// Gets the font family of holidays.
		/// </summary>
		string HolidayFontFamily { get; }
		/// <summary>
		/// Gets the font size of holidays.
		/// </summary>
		Length HolidayFontSize { get; }
		/// <summary>
		/// Gets the font style of holidays.
		/// </summary>
		FontStyle HolidayFontStyle { get; }
		/// <summary>
		/// Gets the font weight of holidays.
		/// </summary>
		FontWeight HolidayFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of holidays.
		/// </summary>
		FontDecoration HolidayFontDecoration { get; }
		/// <summary>
		/// Gets the font color of holidays.
		/// </summary>
		Color HolidayFontColor { get; }
		/// <summary>
		/// Gets the text align of holidays.
		/// </summary>
		TextAlign HolidayTextAlign { get; }
		/// <summary>
		/// Gets the vertical align of holidays.
		/// </summary>
		VerticalAlign HolidayVerticalAlign { get; }

		#endregion

		#region Appointments

		/// <summary>
		/// Gets the collection of calendar appointments.
		/// </summary>
		Collection<Appointment> Appointments { get; }
		/// <summary>
		/// Gets the gap between appointment along y-axis.
		/// </summary>
		Length AppointmentGap { get; }

		#endregion

		#region Day Headers
		/// <summary>
		/// Gets the backcolor of the day headers.
		/// </summary>
		Color DayHeadersBackcolor { get; }
		/// <summary>
		/// Gets the border color of the day headers.
		/// </summary>
		Color DayHeadersBorderColor { get; }
		/// <summary>
		/// Gets the border width of the day Headers.
		/// </summary>
		Length DayHeadersBorderWidth { get; }
		/// <summary>
		/// Gets the border style of the day Headers.
		/// </summary>
		BorderStyle DayHeadersBorderStyle { get; }
		/// <summary>
		/// Gets the font family of the day Headers.
		/// </summary>
		string DayHeadersFontFamily { get; }
		/// <summary>
		/// Gets the font size of the day Headers.
		/// </summary>
		Length DayHeadersFontSize { get; }
		/// <summary>
		/// Gets the font style of the day Headers.
		/// </summary>
		FontStyle DayHeadersFontStyle { get; }
		/// <summary>
		/// Gets the font weight of the day Headers.
		/// </summary>
		FontWeight DayHeadersFontWeight { get; }
		/// <summary>
		/// Gets the font decoration of the day Headers.
		/// </summary>
		FontDecoration DayHeadersFontDecoration { get; }
		/// <summary>
		/// Gets the font color of the day Headers.
		/// </summary>
		Color DayHeadersFontColor { get; }

		#endregion

		/// <summary>
		/// Gets <see cref="CalendarCulture" /> service.
		/// </summary>
		CalendarCulture CalendarCultureService { get; }
		/// <summary>
		/// Obtains and returns the <see cref="System.Drawing.Image"/> by image style definition.
		/// </summary>
		/// <param name="imageStyle"></param>
		/// <returns></returns>
		System.Drawing.Image GetImage(ImageStyle imageStyle);
		/// <summary>
		/// Specifies the direction of text.
		/// </summary>
		Direction Direction { get; }
	}
}
