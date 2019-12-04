using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the month.
	/// </summary>
	public sealed class MonthInfo : IComparable
	{
		private readonly DateTime _firstDayOfMonth;
		private readonly Collection<Appointment> _appointments;

		public MonthInfo(DateTime date)
		{
			_firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
			_appointments = new Collection<Appointment>();
		}

		/// <summary>
		/// Gets the month number.
		/// </summary>
		public int Month
		{
			get { return _firstDayOfMonth.Month; }
		}

		/// <summary>
		/// Gets the year number.
		/// </summary>
		public int Year
		{
			get { return _firstDayOfMonth.Year; }
		}

		/// <summary>
		/// Gets the appointments of the month.
		/// </summary>
		public Collection<Appointment> Appointments
		{
			get { return _appointments; }
		}

		/// <summary>
		/// Computes the day shift of week relative to the base day of week taking into consideration the first day of week.
		/// </summary>
		public static int GetDayOfWeekShift(DayOfWeek day, DayOfWeek baseDay, DayOfWeek firstDayOfWeek)
		{
			int adjustedDay = day < firstDayOfWeek ? CalendarData.DaysInWeek + (int)day : (int)day;
			int adjustedBaseDay = baseDay < firstDayOfWeek ? CalendarData.DaysInWeek + (int)baseDay : (int)baseDay;
			return adjustedDay - adjustedBaseDay;
		}

		/// <summary>
		/// Gets the date of specified day.
		/// </summary>
		public DateTime GetDateOfDay(int week, DayOfWeek dayOfWeek, DayOfWeek firstDayOfWeek, bool isEndDate = false)
		{
			int shift = GetDayOfWeekShift(dayOfWeek, _firstDayOfMonth.DayOfWeek, firstDayOfWeek);
			var day =  _firstDayOfMonth.AddDays(shift + week * CalendarData.DaysInWeek);
			if (isEndDate)
			{
				day = day.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
			}
			else
			{
				day = new DateTime(day.Year, day.Month, day.Day, 0, 0, 0);
			}

			return day;
		}

		/// <summary>
		/// Determines the day kind for specified date in according to the month.
		/// </summary>
		public DayKind DetermineDayKindForDate(DateTime date)
		{
			DayKind dayKind = DayKind.General;
			if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
			{
				dayKind = DayKind.Weekend;
			}
			if (date.Month != Month || date.Year != Year)
			{
				dayKind = DayKind.Filler;
			}
			return dayKind;
		}

		#region IComparable Members

		///<summary>
		///Compares the current instance with another object of the same type.
		///</summary>
		int IComparable.CompareTo(object obj)
		{
			return Compare(this, (MonthInfo)obj);
		}

		/// <summary>
		/// Provides a strongly typed <see cref="IComparable"/> implmentation.
		/// </summary>
		public int CompareTo(MonthInfo peer)
		{
			return Compare(this, peer);
		}

		private static int Compare(MonthInfo m1, MonthInfo m2)
		{
			if (Object.ReferenceEquals(m1, m2)) return 0;
			if ((object)m1 == null) return -1;
			if ((object)m2 == null) return 1;

			// years are different
			if (m2.Year > m1.Year) return -1;
			if (m2.Year < m1.Year) return 1;

			// years are equal but months are different
			if (m2.Month > m1.Month) return -1;
			if (m2.Month < m1.Month) return 1;

			// otherwise the months are same
			return 0;
		}

		#endregion

		#region Object members

		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", Month, Year);
		}

		public override bool Equals(object obj)
		{
			if (obj is MonthInfo)
				return this.CompareTo((MonthInfo)obj) == 0;
			else
				return false;
		}

		public override int GetHashCode()
		{
			string uniqueString = ToString();
			return uniqueString.GetHashCode();
		}

		#endregion

		#region Comparative operators

		public static bool operator <(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) < 0;
		}

		public static bool operator <=(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) <= 0;
		}

		public static bool operator >=(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) >= 0;
		}

		public static bool operator >(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) > 0;
		}

		public static bool operator ==(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) == 0;
		}

		public static bool operator !=(MonthInfo a, MonthInfo b)
		{
			return Compare(a, b) != 0;
		}

		#endregion
	}
}
