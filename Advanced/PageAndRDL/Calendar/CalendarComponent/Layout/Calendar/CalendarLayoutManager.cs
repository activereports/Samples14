using System;
using System.ComponentModel;
using System.Drawing;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Tools;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using LayoutDirection = GrapeCity.ActiveReports.Extensibility.Layout.LayoutDirection;

namespace GrapeCity.ActiveReports.Calendar.Layout
{
	/// <summary>
	/// Provides a <see cref="ILayoutManager" /> implementaiton for <see cref="CalendarDataRegion" />.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class CalendarLayoutManager : ILayoutManagerEx
	{
		private CalendarDataRegion _calendar;
		private SizeF? _completeSize;
		private int _continueVerticallyAttempts;

		LayoutCapabilities ILayoutManager.Capabilities
		{
			get { return LayoutCapabilities.CanGrowVertically; }
		}

		void ILayoutManager.Initialize(IReportItem forReportItem, ITargetDevice targetDevice)
		{
			_calendar = forReportItem as CalendarDataRegion;
			if (_calendar == null)
				throw new ArgumentException("The argument has wrong type.", "forReportItem");
		}

		LayoutResult ILayoutManager.Measure(LayoutContext context)
		{
			var content = (CalendarContentRange)context.ContentRange;

			LayoutResult result;
			using (var calendarData = new CalendarData(_calendar))
			{
				// the first time run
				if (content == null)
				{
					content = new CalendarContentRange(_calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());
				}
				else if ((context.LayoutDirection & LayoutDirection.Vertical) != 0) // continue vertical run
				{
					// do we have anything else to render ?
					if (!calendarData.HasMonthAfter(content.MonthTo))
					{
						return new LayoutResult(null, LayoutStatus.NoContent | LayoutStatus.Complete, new SizeF());
					}

					var continuedRange = content.StaticContentRange.ContinueRange(context);
					content = new CalendarContentRange(continuedRange, _calendar, calendarData.GetNextMonth(content.MonthTo), calendarData.GetLastMonth());
				}
				else if ((context.LayoutDirection & LayoutDirection.Horizontal) != 0) // continue horizontal run
				{
					var continuedRange = content.StaticContentRange.ContinueRange(context);
					content = new CalendarContentRange(continuedRange, _calendar, content.MonthFrom, content.MonthTo);
				}

				result = MeasureContentRange(content, calendarData, context.AvailableSize);
			}

			return result;
		}

		LayoutResult ILayoutManager.Layout(LayoutContext context)
		{
			CalendarContentRange content = context.ContentRange as CalendarContentRange;
			_continueVerticallyAttempts = 0;
			LayoutResult result;
			using (CalendarData calendarData = new CalendarData(_calendar))
			{
				if (content == null)
				{
					content = new CalendarContentRange(_calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());
				}

				result = LayoutContentRange(content, calendarData, context);
			}

			return result;
		}

		private LayoutResult LayoutContentRange(CalendarContentRange range, CalendarData calendarData, LayoutContext context)
		{
			LayoutResult result = MeasureContentRange(range, calendarData, new SizeF(float.MaxValue, float.MaxValue));
			SizeF completeSize = GetCompleteSize(calendarData);

			if (range.EndHorzRange <= 0)
				range.EndHorzRange = completeSize.Width;

			if (range.EndVertRange <= 0)
				range.EndVertRange = completeSize.Height;

			SizeF size = new SizeF(range.EndHorzRange - range.StartHorzRange, range.EndVertRange - range.StartVertRange);
			if (context.AvailableSize.Width > 0 && size.Width > context.AvailableSize.Width)
			{
				size.Width = context.AvailableSize.Width;
				range.EndHorzRange = range.StartHorzRange + context.AvailableSize.Width;
			}

			if (context.LayoutDirection != LayoutDirection.Horizontal)
				range.EndHorzRange = -1;
			else
				range.EndVertRange = -1;

			if (context.AvailableSize.Height > 0 && size.Height > context.AvailableSize.Height)
			{
				size.Height = context.AvailableSize.Height;
				range.EndVertRange = range.StartVertRange + context.AvailableSize.Height + CalendarData.MonthsSpace;
			}

			if (Utils.ApproxGreaterOrEquals(range.EndHorzRange, completeSize.Width))
			{
				if (context.LayoutDirection == LayoutDirection.Horizontal && range.EndVertRange > 0)
				{
					CalendarContentRange.StaticRange staticRange = new CalendarContentRange.StaticRange(range.EndVertRange);
					range = new CalendarContentRange(staticRange, range.Owner, range.MonthFrom, range.MonthTo);// so the layout will continue in a vertical direction
				}
				else
					range.EndHorzRange = -1;
			}

			if (Utils.ApproxGreaterOrEquals(range.EndVertRange, completeSize.Height))
				range.EndVertRange = -1;

			return result;
		}

		private SizeF GetCompleteSize(CalendarData calendarData)
		{
			if (_completeSize == null)
			{
				CalendarContentRange content = new CalendarContentRange(_calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());
				LayoutResult result = MeasureContentRange(content, calendarData, new SizeF(float.MaxValue, float.MaxValue));
				_completeSize = result.ActualSize;
			}

			return _completeSize.Value;
		}

		private LayoutResult MeasureContentRange(CalendarContentRange content, CalendarData calendarData, SizeF maxSize)
		{
			SizeF actualSize = new SizeF();
			LayoutStatus layoutStatus = LayoutStatus.None;

			MonthInfo currentMonth = content.MonthFrom;
			MonthInfo lastMonthtFitted = currentMonth;

			while (currentMonth <= content.MonthTo)
			{
				// measure the month
				SizeF monthSize = calendarData.MeasureMonth(
					currentMonth,
					new SizeF(_calendar.Width.ToTwips(), _calendar.Height.ToTwips()));

				if (currentMonth != content.MonthFrom)
					monthSize.Height += CalendarData.MonthsSpace;

				if (actualSize.Height + monthSize.Height > maxSize.Height)
				{
					layoutStatus |= LayoutStatus.ContinueVertically;
					_continueVerticallyAttempts++;
				}

				// if the current month didn't fit vertically
				bool continueVertically = (layoutStatus & LayoutStatus.ContinueVertically) == LayoutStatus.ContinueVertically;
				// limit the number of attempts vertical continue
				// currentMonth.Month != lastMonthtFitted.Month for prevent the creation of blank pages (case 149145)
				if (continueVertically && _continueVerticallyAttempts < 2 && currentMonth.Month != lastMonthtFitted.Month) break;

				layoutStatus |= LayoutStatus.SomeContent;
				actualSize.Width = Math.Max(actualSize.Width, monthSize.Width);
				actualSize.Height += monthSize.Height;

				// if the current month didn't fit vertically AND emptyFplPage == true
				if (continueVertically) break;

				lastMonthtFitted = currentMonth;
				if (!calendarData.HasMonthAfter(currentMonth))
				{
					// all months were fitted
					layoutStatus |= LayoutStatus.Complete;
					break;
				}

				currentMonth = calendarData.GetNextMonth(currentMonth);
			}

			if (actualSize.Width > maxSize.Width)
				layoutStatus |= LayoutStatus.ContinueHorizontally;

			return new LayoutResult(
			   new CalendarContentRange(content.StaticContentRange, _calendar, content.MonthFrom, lastMonthtFitted),
			   layoutStatus, actualSize);
		}

		bool ILayoutManagerEx.IsOverflowLayout { get; set; }

		/// <summary>
		/// Implements ILayoutManagerEx method. Returns true.
		/// </summary>
		public bool ConditionalRollback(System.Func<IReportItem, bool> condition)
		{
			return true; // never reset, this is a DataRegion!
		}
	}
}
