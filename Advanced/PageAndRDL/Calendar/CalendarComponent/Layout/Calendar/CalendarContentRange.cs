using System;

using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Layout.Internal;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;

namespace GrapeCity.ActiveReports.Calendar.Layout
{
	/// <summary>
	/// Represents a calendar laid out in an area.
	/// </summary>
	public sealed class CalendarContentRange : ContentRange, IStaticContentRange, ICustomContentRange
	{
		private readonly IReportItem _owner;
		private readonly MonthInfo _monthFrom;
		private readonly MonthInfo _monthTo;
		private readonly StaticRange _staticRange;

		public int ItemWidth { get; }
		public int ItemHeight { get; }

		private CalendarContentRange(StaticRange staticRange)
		{
			_staticRange = staticRange;
		}

		public CalendarContentRange(IReportItem owner, MonthInfo MonthFrom, MonthInfo MonthTo)
			: this(new StaticRange(0, -1, 0, -1))
		{
			_owner = owner;
			_monthFrom = MonthFrom;
			_monthTo = MonthTo;
		}

		public CalendarContentRange(StaticRange staticRange, IReportItem owner, MonthInfo MonthFrom, MonthInfo MonthTo)
			: this(staticRange)
		{
			_owner = owner;
			_monthFrom = MonthFrom;
			_monthTo = MonthTo;
		}

		/// <summary>
		/// The month from we need to render the data 
		/// </summary>
		public MonthInfo MonthFrom
		{
			get { return _monthFrom; }
		}

		/// <summary>
		/// The month to we need to render the data
		/// </summary>
		public MonthInfo MonthTo
		{
			get { return _monthTo; }
		}

		#region ContentRange members

		public override IReportItem Owner
		{
			get { return _owner; }
		}

		public IContentRange Fork(ICustomReportItem reportItem)
		{
			return new CalendarContentRange(_staticRange, reportItem, MonthFrom, MonthTo);
		}

		// TODO: implement properly

		#endregion

		public float StartVertRange
		{
			get { return _staticRange.StartVertRange; }
		}

		public float EndVertRange
		{
			get { return _staticRange.EndVertRange; }
			set { _staticRange.EndVertRange = value; }
		}

		public float StartHorzRange
		{
			get { return _staticRange.StartHorzRange; }
		}

		public float EndHorzRange
		{
			get { return _staticRange.EndHorzRange; }
			set { _staticRange.EndHorzRange = value; }
		}

		public bool CompleteItemHorizontally
		{
			get
			{
				return (_staticRange.StartHorzRange == 0 && _staticRange.EndHorzRange == -1);
			}
		}

		public bool CompleteItemVertically
		{
			get
			{
				return (_staticRange.StartVertRange == 0 && _staticRange.EndVertRange == -1);
			}
		}

		internal StaticRange StaticContentRange
		{
			get { return _staticRange; }
		}

		public override string ToString()
		{
			return string.Format("[StartHorz:{0} StartVert:{1} EndHorz:{2} EndVert:{3}]"
				, StartHorzRange
				, StartVertRange
				, EndHorzRange
				, EndVertRange);
		}

		#region StaticRange
		public sealed class StaticRange
		{
			private readonly float _startVertRange;
			private float _endVertRange;
			private readonly float _startHorzRange;
			private float _endHorzRange;

			public StaticRange(float startVertRange)
				: this(startVertRange, -1, 0, -1)
			{
			}

			public StaticRange(float startVertRange, float endVertRange, float startHorzRange, float endHorzRange)
			{
				_startVertRange = startVertRange;
				_endVertRange = endVertRange;
				_startHorzRange = startHorzRange;
				_endHorzRange = endHorzRange;
			}

			public StaticRange(float startVertRange, float startHorzRange)
				: this(Math.Max(0, startVertRange), -1, Math.Max(0, startHorzRange), -1)
			{
			}

			public float StartVertRange { get { return _startVertRange; } }

			public float StartHorzRange { get { return _startHorzRange; } }

			public float EndVertRange
			{
				get { return _endVertRange; }
				set { _endVertRange = value; }
			}

			public float EndHorzRange
			{
				get { return _endHorzRange; }
				set { _endHorzRange = value; }
			}

			/// <summary>
			/// Creates a new range that logically continues the supplied range.
			/// </summary>
			public StaticRange ContinueRange(LayoutContext context)
			{
				float startVertical, startHorizontal;
				if (context.LayoutDirection == LayoutDirection.Horizontal)
					startVertical = StartVertRange;
				else
				{
					if (EndVertRange <= 0)
						startVertical = StartVertRange;
					else
						startVertical = EndVertRange;
				}

				if (context.LayoutDirection == LayoutDirection.Vertical)
					startHorizontal = StartHorzRange;
				else
				{
					if (EndHorzRange <= 0)
						startHorizontal = StartHorzRange;
					else
						startHorizontal = EndHorzRange;
				}
				return new StaticRange(startVertical, startHorizontal);
			}
		}
		#endregion
	}
}
