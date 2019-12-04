using System;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using NUnit.Framework;

namespace GrapeCity.ActiveReports.Calendar.Tests
{
	/// <summary>
	/// The fixture to test the some calendar correction calculations.
	/// </summary>
	[TestFixture]
	public class CalendarUtilitiesFixture
	{
		/// <summary>
		/// Tests the calculation of week day adjustment when Sunday is the first week day.
		/// </summary>
		[Test]
		public void SundayIsFirstDayTest()
		{
			DayOfWeek sunday = DayOfWeek.Sunday;
			DayOfWeek baseDay;

			baseDay = DayOfWeek.Thursday;
			Assert.AreEqual(-4, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, sunday));
			Assert.AreEqual(-3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, sunday));
			Assert.AreEqual(-2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, sunday));
			Assert.AreEqual(-1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, sunday));
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, sunday));
			Assert.AreEqual(1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, sunday));
			Assert.AreEqual(2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, sunday));

			baseDay = DayOfWeek.Sunday;
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, sunday));
			Assert.AreEqual(1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, sunday));
			Assert.AreEqual(2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, sunday));
			Assert.AreEqual(3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, sunday));
			Assert.AreEqual(4, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, sunday));
			Assert.AreEqual(5, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, sunday));
			Assert.AreEqual(6, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, sunday));

			baseDay = DayOfWeek.Monday;
			Assert.AreEqual(-1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, sunday));
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, sunday));
			Assert.AreEqual(1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, sunday));
			Assert.AreEqual(2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, sunday));
			Assert.AreEqual(3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, sunday));
			Assert.AreEqual(4, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, sunday));
			Assert.AreEqual(5, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, sunday));
		}

		/// <summary>
		/// Tests the calculation of week day adjustment when Monday is the first week day.
		/// </summary>
		[Test]
		public void MondayIsFirstDayTest()
		{
			DayOfWeek monday = DayOfWeek.Monday;
			DayOfWeek baseDay;

			baseDay = DayOfWeek.Thursday;
			Assert.AreEqual(-3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, monday));
			Assert.AreEqual(-2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, monday));
			Assert.AreEqual(-1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, monday));
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, monday));
			Assert.AreEqual(1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, monday));
			Assert.AreEqual(2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, monday));
			Assert.AreEqual(3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, monday));

			baseDay = DayOfWeek.Sunday;
			Assert.AreEqual(-6, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, monday));
			Assert.AreEqual(-5, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, monday));
			Assert.AreEqual(-4, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, monday));
			Assert.AreEqual(-3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, monday));
			Assert.AreEqual(-2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, monday));
			Assert.AreEqual(-1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, monday));
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, monday));

			baseDay = DayOfWeek.Monday;
			Assert.AreEqual(0, MonthInfo.GetDayOfWeekShift(DayOfWeek.Monday, baseDay, monday));
			Assert.AreEqual(1, MonthInfo.GetDayOfWeekShift(DayOfWeek.Tuesday, baseDay, monday));
			Assert.AreEqual(2, MonthInfo.GetDayOfWeekShift(DayOfWeek.Wednesday, baseDay, monday));
			Assert.AreEqual(3, MonthInfo.GetDayOfWeekShift(DayOfWeek.Thursday, baseDay, monday));
			Assert.AreEqual(4, MonthInfo.GetDayOfWeekShift(DayOfWeek.Friday, baseDay, monday));
			Assert.AreEqual(5, MonthInfo.GetDayOfWeekShift(DayOfWeek.Saturday, baseDay, monday));
			Assert.AreEqual(6, MonthInfo.GetDayOfWeekShift(DayOfWeek.Sunday, baseDay, monday));
		}

		/// <summary>
		/// Tests the calculation of week day adjustment for December'07.
		/// </summary>
		[Test]
		public void December2007_Test()
		{
			// Dec'07
			MonthInfo month = new MonthInfo(new DateTime(2007, 12, 1));

			DayOfWeek sunday = DayOfWeek.Sunday;
			Assert.AreEqual(25, month.GetDateOfDay(0, DayOfWeek.Sunday, sunday).Day, "25 Oct'07");
			Assert.AreEqual(26, month.GetDateOfDay(0, DayOfWeek.Monday, sunday).Day, "26 Oct'07");
			Assert.AreEqual(27, month.GetDateOfDay(0, DayOfWeek.Tuesday, sunday).Day, "27 Oct'07");
			Assert.AreEqual(28, month.GetDateOfDay(0, DayOfWeek.Wednesday, sunday).Day, "28 Oct'07");
			Assert.AreEqual(29, month.GetDateOfDay(0, DayOfWeek.Thursday, sunday).Day, "29 Oct'07");
			Assert.AreEqual(30, month.GetDateOfDay(0, DayOfWeek.Friday, sunday).Day, "30 Oct'07");

			var day = month.GetDateOfDay(0, DayOfWeek.Saturday, sunday, false);
			Assert.AreEqual("0:0:0", day.Hour + ":" + day.Minute + ":" + day.Second, "Correct time is 0:0:0");
			Assert.AreEqual(1, day.Day, "01 Dec'07");

			//Get End of the day
			day = month.GetDateOfDay(0, DayOfWeek.Saturday, sunday, true);
			Assert.AreEqual("23:59:59", day.Hour + ":" + day.Minute + ":" + day.Second, "Correct time is 23:59:59");

			DayOfWeek monday = DayOfWeek.Monday;
			Assert.AreEqual(26, month.GetDateOfDay(0, DayOfWeek.Monday, monday).Day, "26 Oct'07");
			Assert.AreEqual(27, month.GetDateOfDay(0, DayOfWeek.Tuesday, monday).Day, "27 Oct'07");
			Assert.AreEqual(28, month.GetDateOfDay(0, DayOfWeek.Wednesday, monday).Day, "28 Oct'07");
			Assert.AreEqual(29, month.GetDateOfDay(0, DayOfWeek.Thursday, monday).Day, "29 Oct'07");
			Assert.AreEqual(30, month.GetDateOfDay(0, DayOfWeek.Friday, monday).Day, "30 Oct'07");
			Assert.AreEqual(1, month.GetDateOfDay(0, DayOfWeek.Saturday, monday).Day, "01 Dec'07");
			Assert.AreEqual(2, month.GetDateOfDay(0, DayOfWeek.Sunday, monday).Day, "02 Dec'07");
		}

		/// <summary>
		/// Tests the duration calculation for different periods. 
		/// </summary>
		[Test]
		public void AppointmentDurationCalculationTest()
		{
			const int Year = 2007;
			const int Month = 12;

			Appointment app = Appointment.Create(new DateTime(Year, Month, 10), new DateTime(Year, Month, 20), "app");

			Assert.AreEqual(11, app.GetDurationInPeriod(new DateTime(Year, Month, 1), new DateTime(Year, Month, 28)), "All days are included");
			Assert.AreEqual(0, app.GetDurationInPeriod(new DateTime(Year, Month, 21), new DateTime(Year, Month, 28)), "Appointment is scheduled before the period");
			Assert.AreEqual(0, app.GetDurationInPeriod(new DateTime(Year, Month, 1), new DateTime(Year, Month, 9)), "Appointment is scheduled after the period");
			Assert.AreEqual(9, app.GetDurationInPeriod(new DateTime(Year, Month, 11), new DateTime(Year, Month, 19)), "Appointment contains the period");
			Assert.AreEqual(6, app.GetDurationInPeriod(new DateTime(Year, Month, 1), new DateTime(Year, Month, 15)), "Appointment contains the end date of period");
			Assert.AreEqual(6, app.GetDurationInPeriod(new DateTime(Year, Month, 15), new DateTime(Year, Month, 25)), "Appointment contains the start date of period");
		}
	}
}
