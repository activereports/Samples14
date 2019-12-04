using System;
using System.Collections.Generic;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.PageReportModel;
using NUnit.Framework;
using BorderStyle = GrapeCity.ActiveReports.Calendar.Components.Calendar.BorderStyle;
using Color = System.Drawing.Color;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;
using TextAlign = GrapeCity.ActiveReports.Calendar.Components.Calendar.TextAlign;
using VerticalAlign = GrapeCity.ActiveReports.Calendar.Components.Calendar.VerticalAlign;

namespace GrapeCity.ActiveReports.Calendar.Tests.Calendar
{
	/// <summary>
	/// Represents the base tests for calendar data region.
	/// </summary>
	[TestFixture]
	public sealed class CalendarBaseFixture
	{
		/// <summary>
		/// Tests simple filters for calendar appointments.
		/// </summary>
		[Test]
		public void AppointmentFilters()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Filters.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(4, calendar.Appointments.Count);
			// calendar2
			calendar = (ICalendar)renderer.GetReportItem("Calendar2");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(1, calendar.Appointments.Count);
		}

		/// <summary>
		/// Tests the ordering of appointments in multiple weeks.
		/// </summary>
		/// <remarks>Case 35658</remarks>
		[Test]
		public void AppointmentOrdering()
		{
			PageDocument report = TestHelper.GetReport("Calendar-AppointmentWrapping.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// StartTime(string),EndTime(string),Value(string)
			//2007-12-07,2007-12-09,"Appt#1 Text ..."
			//2007-12-08,2007-12-09,"Appt#2 Text ..."
			//2007-12-09,2007-12-09,"Appt#3 Text ..."
			//2007-12-09,2007-12-11,"Appt#4 Text ..."

			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(4, calendar.Appointments.Count);
			CalendarData calendarData = new CalendarData(calendar);
			Assert.IsNotNull(calendarData);
			MonthInfo month = calendarData.GetFirstMonth();
			Assert.IsNotNull(month);
			Assert.AreEqual(12, month.Month);
			Assert.AreEqual(2007, month.Year);
			ICollection<Appointment> week1Appointments = calendarData.GetAppointmentsInWeek(month, 1);
			Assert.AreEqual(2, week1Appointments.Count);
			ICollection<Appointment> week2Appointments = calendarData.GetAppointmentsInWeek(month, 2);
			Assert.AreEqual(4, week2Appointments.Count);
			// first week : make sure of the ordering of appointments.
			int index = 1;
			foreach (Appointment appointment in week1Appointments)
			{
				Assert.AreEqual(string.Format("Appt#{0} Text ...", index), appointment.Value);
				index++;
			}
			// second week : make sure of the ordering of appointments is the same as prior week.
			index = 1;
			foreach (Appointment appointment in week1Appointments)
			{
				Assert.AreEqual(string.Format("Appt#{0} Text ...", index), appointment.Value);
				index++;
			}
		}

		/// <summary>
		/// Tests that appointments without text are still created.
		/// </summary>
		/// <remarks>Case 35646</remarks>
		[Test]
		public void AppointmentNoText()
		{
			PageDocument report = TestHelper.GetReport("Calendar-AppointmentNoText.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// StartTime(string),EndTime(string),Value(string)
			//2007-12-07,2007-12-09,""

			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(1, calendar.Appointments.Count);
			// second row
			Appointment app1 = calendar.Appointments[0];
			Assert.AreEqual(new DateTime(2007, 12, 7), app1.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 9), app1.EndDate.Date);
			Assert.AreEqual("", app1.Value);
		}

		/// <summary>
		/// Tests the loading of appointments with bad formatted dates.
		/// </summary>
		[Test]
		public void AppointmentWithBadFormattedDates()
		{
			PageDocument report = TestHelper.GetReport("Calendar-BadDates.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// StartTime(string),EndTime(string),Value(string)
			// "2007-1205","2007-12-07","Appt#1 Text ..."
			// "2007-12-05","2007-1207","Appt#2 Text ..."
			// "2007-12-05","2007-12-07","Appt#3 Text ..."

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(2, calendar.Appointments.Count);
			// second row
			Appointment app1 = calendar.Appointments[0];
			Assert.AreEqual(new DateTime(2007, 12, 5), app1.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 5), app1.EndDate.Date);
			Assert.AreEqual("Appt#2 Text ...", app1.Value);
			// third row
			Appointment app2 = calendar.Appointments[1];
			Assert.AreEqual(new DateTime(2007, 12, 5), app2.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 7), app2.EndDate.Date);
			Assert.AreEqual("Appt#3 Text ...", app2.Value);
		}

		/// <summary>
		/// Tests the default values of appointments appearance.
		/// </summary>
		[Test]
		public void DefaultAppearanceAppointmentTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);

			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.AreEqual(Color.LightYellow, appointment.Backcolor);
				Assert.AreEqual(Color.DarkGray, appointment.BorderColor);
				Assert.AreEqual("Arial", appointment.FontFamily);
				Assert.AreEqual(new Length("8pt"), appointment.FontSize);
				Assert.AreEqual(FontStyle.Normal, appointment.FontStyle);
				Assert.AreEqual(FontWeight.Normal, appointment.FontWeight);
				Assert.AreEqual(FontDecoration.None, appointment.FontDecoration);
				Assert.AreEqual(Color.Black, appointment.FontColor);
				Assert.AreEqual(TextAlign.General, appointment.TextAlign);
				Assert.AreEqual(string.Empty, appointment.Format);
				Assert.AreEqual(ImageSource.External, appointment.ImageSource);
				Assert.AreEqual(string.Empty, appointment.ImageValue);
				Assert.AreEqual(string.Empty, appointment.MimeType);
			}
		}

		/// <summary>
		/// Tests the background color of appointments.
		/// </summary>
		[Test]
		public void BackgroundColorAppointmentsTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-BackgroundColor.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(17, calendar.Appointments.Count);

			// there is expression to get background color '=IIF(ToString(Fields!Value.Value).contains("..."), "Coral", "LightGreen")'
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.AreEqual(
					Convert.ToString(appointment.Value).Contains("...")
						? Color.Coral
						: Color.LightGreen, appointment.Backcolor);
			}
		}

		/// <summary>
		/// Tests the text align of appointments.
		/// </summary>
		[Test]
		public void TextAlignAppointmentsTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-TextAlign.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(17, calendar.Appointments.Count);

			// there is expression to get text align '=IIF(ToString(Fields!Value.Value).contains("..."), "Left", "Right")'
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.AreEqual(Convert.ToString(appointment.Value).Contains("...") ? TextAlign.Left : TextAlign.Right,
								appointment.TextAlign);
			}
		}

		/// <summary>
		/// Tests the border style of appointments.
		/// </summary>
		[Test]
		public void BorderStyleAppointmentsTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-BorderStyle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(17, calendar.Appointments.Count);

			// there is expression to get border color '=IIF(ToString(Fields!Value.Value).contains("..."), "Red", "Green")'
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.AreEqual(
					Convert.ToString(appointment.Value).Contains("...")
						? Color.Red
						: Color.Green, appointment.BorderColor);
			}
		}

		/// <summary>
		/// Tests the font style of appointments.
		/// </summary>
		[Test]
		public void FontStyleAppointmentsTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-FontStyle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(17, calendar.Appointments.Count);

			// there are expressions to get 
			//		font style		'=IIF(ToString(Fields!Value.Value).contains("..."), "Normal", "Italic")'
			//		font weight		'=IIF(ToString(Fields!Value.Value).contains("..."), "Normal", "Bold")'
			//		font decoration	'=IIF(ToString(Fields!Value.Value).contains("..."), "None", "Underline")'
			//		font color		'=IIF(ToString(Fields!Value.Value).contains("..."), "Red", "Green")'
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.AreEqual("Times New Roman", appointment.FontFamily);
				Assert.AreEqual(new Length("6pt"), appointment.FontSize);
				if (Convert.ToString(appointment.Value).Contains("..."))
				{
					Assert.AreEqual(FontStyle.Normal, appointment.FontStyle);
					Assert.AreEqual(FontWeight.Normal, appointment.FontWeight);
					Assert.AreEqual(FontDecoration.None, appointment.FontDecoration);
					Assert.AreEqual(Color.Red, appointment.FontColor);
				}
				else
				{
					Assert.AreEqual(FontStyle.Italic, appointment.FontStyle);
					Assert.AreEqual(FontWeight.Bold, appointment.FontWeight);
					Assert.AreEqual(FontDecoration.Underline, appointment.FontDecoration);
					Assert.AreEqual(Color.Green, appointment.FontColor);
				}
			}
		}

		/// <summary>
		/// Tests the format of appointments.
		/// </summary>
		[Test]
		public void FormatAppointmentsTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Format.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(15, calendar.Appointments.Count);

			// there are expressions to get 
			//		format		'=IIF((Convert.ToDateTime(Fields!StartTime.Value).Day - Convert.ToDateTime(Fields!EndTime.Value).Day) = 0, "D", "d")'
			//		background	'=IIF((Convert.ToDateTime(Fields!StartTime.Value).Day - Convert.ToDateTime(Fields!EndTime.Value).Day) = 0, "LightGreen", "Coral")'
			foreach (Appointment appointment in calendar.Appointments)
			{
				if (appointment.StartDate.Day - appointment.EndDate.Day == 0)
				{
					Assert.AreEqual("D", appointment.Format);
					Assert.AreEqual(Color.LightGreen, appointment.Backcolor);
				}
				else
				{
					Assert.AreEqual("d", appointment.Format);
					Assert.AreEqual(Color.Coral, appointment.Backcolor);
				}
			}
		}

		/// <summary>
		/// Tests the formatting of month title.
		/// </summary>
		[Test]
		public void MonthTitleFormattingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-MonthTitle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1 (default values)
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.White, calendar.MonthTitleBackcolor);
			Assert.AreEqual(Color.Black, calendar.MonthTitleBorderColor);
			Assert.AreEqual(new Length("1pt"), calendar.MonthTitleBorderWidth);
			Assert.AreEqual(BorderStyle.None, calendar.MonthTitleBorderStyle);
			Assert.AreEqual("Arial", calendar.MonthTitleFontFamily);
			Assert.AreEqual(new Length("16pt"), calendar.MonthTitleFontSize);
			Assert.AreEqual(FontStyle.Normal, calendar.MonthTitleFontStyle);
			Assert.AreEqual(FontWeight.Normal, calendar.MonthTitleFontWeight);
			Assert.AreEqual(FontDecoration.None, calendar.MonthTitleFontDecoration);
			Assert.AreEqual(Color.Black, calendar.MonthTitleFontColor);
			Assert.AreEqual(TextAlign.General, calendar.MonthTitleTextAlign);
			Assert.AreEqual("MMMM", calendar.MonthTitleFormat);

			// calendar2 (read values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar2");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.LightSteelBlue, calendar.MonthTitleBackcolor);
			Assert.AreEqual(Color.Navy, calendar.MonthTitleBorderColor);
			Assert.AreEqual(new Length("0.5pt"), calendar.MonthTitleBorderWidth);
			Assert.AreEqual(BorderStyle.Dotted, calendar.MonthTitleBorderStyle);
			Assert.AreEqual("Times New Roman", calendar.MonthTitleFontFamily);
			Assert.AreEqual(new Length("12pt"), calendar.MonthTitleFontSize);
			Assert.AreEqual(FontStyle.Italic, calendar.MonthTitleFontStyle);
			Assert.AreEqual(FontWeight.Bold, calendar.MonthTitleFontWeight);
			Assert.AreEqual(FontDecoration.Underline, calendar.MonthTitleFontDecoration);
			Assert.AreEqual(Color.Navy, calendar.MonthTitleFontColor);
			Assert.AreEqual(TextAlign.Center, calendar.MonthTitleTextAlign);
			Assert.AreEqual("D", calendar.MonthTitleFormat);
		}

		/// <summary>
		/// Tests the formatting of filler days.
		/// </summary>
		[Test]
		public void FillerDayFormattingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-FillerDayStyle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1 (default values)
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.Gainsboro, calendar.FillerDayBackcolor);
			Assert.IsNotNull(calendar.FillerDayBorderStyle);
			//filler day border color should be inherited from day
			Assert.AreEqual(calendar.DayBorderColor, calendar.FillerDayBorderColor);
			Assert.AreEqual(new Length("1pt"), calendar.FillerDayBorderWidth);
			Assert.AreEqual(BorderStyle.Solid, calendar.FillerDayBorderStyle);
			Assert.AreEqual("Arial", calendar.FillerDayFontFamily);
			Assert.AreEqual(new Length("9pt"), calendar.FillerDayFontSize);
			Assert.AreEqual(FontStyle.Normal, calendar.FillerDayFontStyle);
			Assert.AreEqual(FontWeight.Normal, calendar.FillerDayFontWeight);
			Assert.AreEqual(FontDecoration.None, calendar.FillerDayFontDecoration);
			Assert.AreEqual(Color.DimGray, calendar.FillerDayFontColor);
			Assert.AreEqual(TextAlign.General, calendar.FillerDayTextAlign);
			Assert.AreEqual(VerticalAlign.Middle, calendar.FillerDayVerticalAlign);

			// calendar2 (read values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar2");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.LightSteelBlue, calendar.FillerDayBackcolor);
			Assert.AreEqual(Color.Navy, calendar.FillerDayBorderColor);
			Assert.AreEqual(new Length("0.5pt"), calendar.FillerDayBorderWidth);
			Assert.AreEqual(BorderStyle.Dotted, calendar.FillerDayBorderStyle);
			Assert.AreEqual("Times New Roman", calendar.FillerDayFontFamily);
			Assert.AreEqual(new Length("8pt"), calendar.FillerDayFontSize);
			Assert.AreEqual(FontStyle.Italic, calendar.FillerDayFontStyle);
			Assert.AreEqual(FontWeight.Bold, calendar.FillerDayFontWeight);
			Assert.AreEqual(FontDecoration.Underline, calendar.FillerDayFontDecoration);
			Assert.AreEqual(Color.Navy, calendar.FillerDayFontColor);
			Assert.AreEqual(TextAlign.Left, calendar.FillerDayTextAlign);
			Assert.AreEqual(VerticalAlign.Bottom, calendar.FillerDayVerticalAlign);
		}

		/// <summary>
		/// Tests the formatting of weekend days.
		/// </summary>
		[Test]
		public void WeekendFormattingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-WeekendStyle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1 (default values)
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.WhiteSmoke, calendar.WeekendBackcolor);
			Assert.IsNotNull(calendar.WeekendBorderStyle);
			//weekend day border color should be inherited from day
			Assert.AreEqual(calendar.DayBorderColor, calendar.WeekendBorderColor);
			Assert.AreEqual(new Length("1pt"), calendar.WeekendBorderWidth);
			Assert.AreEqual(BorderStyle.Solid, calendar.WeekendBorderStyle);
			Assert.AreEqual("Arial", calendar.WeekendFontFamily);
			Assert.AreEqual(new Length("9pt"), calendar.WeekendFontSize);
			Assert.AreEqual(FontStyle.Normal, calendar.WeekendFontStyle);
			Assert.AreEqual(FontWeight.Normal, calendar.WeekendFontWeight);
			Assert.AreEqual(FontDecoration.None, calendar.WeekendFontDecoration);
			Assert.AreEqual(Color.Black, calendar.WeekendFontColor);
			Assert.AreEqual(TextAlign.General, calendar.WeekendTextAlign);
			Assert.AreEqual(VerticalAlign.Middle, calendar.WeekendVerticalAlign);

			// calendar2 (read values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar2");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.WhiteSmoke, calendar.WeekendBackcolor);
			Assert.AreEqual(Color.Navy, calendar.WeekendBorderColor);
			Assert.AreEqual(new Length("0.5pt"), calendar.WeekendBorderWidth);
			Assert.AreEqual(BorderStyle.Solid, calendar.WeekendBorderStyle);
			Assert.AreEqual("Tahoma", calendar.WeekendFontFamily);
			Assert.AreEqual(new Length("8pt"), calendar.WeekendFontSize);
			Assert.AreEqual(FontStyle.Normal, calendar.WeekendFontStyle);
			Assert.AreEqual(FontWeight.Bold, calendar.WeekendFontWeight);
			Assert.AreEqual(FontDecoration.None, calendar.WeekendFontDecoration);
			Assert.AreEqual(Color.Red, calendar.WeekendFontColor);
			Assert.AreEqual(TextAlign.Left, calendar.WeekendTextAlign);
			Assert.AreEqual(VerticalAlign.Bottom, calendar.WeekendVerticalAlign);
		}

		/// <summary>
		/// Tests the formatting of days.
		/// </summary>
		[Test]
		public void DayFormattingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-DayStyle.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1 (default values)
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.White, calendar.DayBackcolor);
			Assert.AreEqual(Color.DarkGray, calendar.DayBorderColor);
			Assert.AreEqual(new Length("1pt"), calendar.DayBorderWidth);
			Assert.AreEqual(BorderStyle.Solid, calendar.DayBorderStyle);
			Assert.AreEqual("Arial", calendar.DayFontFamily);
			Assert.AreEqual(new Length("9pt"), calendar.DayFontSize);
			Assert.AreEqual(FontStyle.Normal, calendar.DayFontStyle);
			Assert.AreEqual(FontWeight.Normal, calendar.DayFontWeight);
			Assert.AreEqual(FontDecoration.None, calendar.DayFontDecoration);
			Assert.AreEqual(Color.Black, calendar.DayFontColor);
			Assert.AreEqual(TextAlign.General, calendar.DayTextAlign);
			Assert.AreEqual(VerticalAlign.Middle, calendar.DayVerticalAlign);

			// calendar2 (read values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar2");
			Assert.IsNotNull(calendar);
			Assert.AreEqual(Color.LightSteelBlue, calendar.DayBackcolor);
			Assert.AreEqual(Color.Navy, calendar.DayBorderColor);
			Assert.AreEqual(new Length("0.5pt"), calendar.DayBorderWidth);
			Assert.AreEqual(BorderStyle.Dotted, calendar.DayBorderStyle);
			Assert.AreEqual("Times New Roman", calendar.DayFontFamily);
			Assert.AreEqual(new Length("8pt"), calendar.DayFontSize);
			Assert.AreEqual(FontStyle.Italic, calendar.DayFontStyle);
			Assert.AreEqual(FontWeight.Bold, calendar.DayFontWeight);
			Assert.AreEqual(FontDecoration.Underline, calendar.DayFontDecoration);
			Assert.AreEqual(Color.Navy, calendar.DayFontColor);
			Assert.AreEqual(TextAlign.Left, calendar.DayTextAlign);
			Assert.AreEqual(VerticalAlign.Bottom, calendar.DayVerticalAlign);
		}

		///<summary>
		/// Tests for hyperlink action of appointment
		///</summary>
		[Test]
		public void CalendarHyperlinkActionTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Hyperlink.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// calendar1 (default values)
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.IsNotNull(appointment.Action);
				Assert.AreEqual(ActionType.HyperLink, appointment.Action.ActionType);
				Assert.AreEqual(appointment.Value, appointment.Action.HyperLink);
			}
		}
		
		/// <summary>
		/// Tests the loading of appointments using detail grouping.
		/// </summary>
		/// <remarks>Case 26230</remarks>
		[Test]
		public void AppointmentDetailGrouping()
		{
			PageDocument report = TestHelper.GetReport("Calendar_AppointmentDetailGrouping.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			// EventID(integer),Date(datetime)
			// 1,2007-12-01
			// 1,2007-12-02
			// 1,2007-12-03
			// 2,2007-12-01
			// 2,2007-12-03
			// 3,2007-12-02
			// 4,2007-12-03

			// calendar1
			ICalendar calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			Assert.IsNotNull(calendar.Appointments);
			Assert.AreEqual(4, calendar.Appointments.Count);
			// event 1
			Appointment app1 = calendar.Appointments[0];
			Assert.AreEqual(new DateTime(2007, 12, 1), app1.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 3), app1.EndDate.Date);
			Assert.AreEqual(1, app1.Value);
			// event 2
			Appointment app2 = calendar.Appointments[1];
			Assert.AreEqual(new DateTime(2007, 12, 1), app2.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 3), app2.EndDate.Date);
			Assert.AreEqual(2, app2.Value);
			// event 3
			Appointment app3 = calendar.Appointments[2];
			Assert.AreEqual(new DateTime(2007, 12, 2), app3.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 2), app3.EndDate.Date);
			Assert.AreEqual(3, app3.Value);
			// event 4
			Appointment app4 = calendar.Appointments[3];
			Assert.AreEqual(new DateTime(2007, 12, 3), app4.StartDate.Date);
			Assert.AreEqual(new DateTime(2007, 12, 3), app4.EndDate.Date);
			Assert.AreEqual(4, app4.Value);
		}
	}
}
