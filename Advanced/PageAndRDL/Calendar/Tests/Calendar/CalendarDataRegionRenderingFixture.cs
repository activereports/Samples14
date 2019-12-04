using System;
using System.Drawing;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Calendar.Layout;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Document;
using NUnit.Framework;
using System.Threading;
using GrapeCity.ActiveReports.Drawing.Gdi;

namespace GrapeCity.ActiveReports.Calendar.Tests.Calendar
{
	/// <summary>
	/// Contains unit tests for <see cref="CalendarDataRegion"/> custom report item.
	/// </summary>
	[TestFixture]
	public class CalendarDataRegionRenderingFixture
	{
		private Graphics _graphics;

		/// <summary>
		/// Returns a <see cref="Graphics"/> to use in DrawingCanvas object.
		/// </summary>
		internal Graphics GetGraphics()
		{
			if (_graphics == null)
			{
				_graphics = Graphics.FromHwnd(IntPtr.Zero);
			}
			return _graphics;
		}

		/// <summary>
		/// Tests the default rendering of <see cref="CalendarDataRegion"/>.
		/// </summary>
		[Test]
		public void CalendarDefaultRenderingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar.rdlx");

			TestingRenderingExtension rendererExt = new TestingRenderingExtension();
			report.Render(rendererExt, null);

			CalendarDataRegion calendar = rendererExt.GetReportItem("Calendar1") as CalendarDataRegion;
			Assert.IsNotNull(calendar);

			CalendarData calendarData = new CalendarData(calendar);
			CalendarContentRange content = new CalendarContentRange(calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());

			// recalibrate graphics to twips
			RectangleF rect = new RectangleF(0, 0, 4000f, 4000f);
			CalendarDesigner.ScaleToTwipsGraphicsAndBound(GetGraphics(), ref rect);

			// create gdi canvas wrapper to use in renderer
			IDrawingCanvas canvas = GraphicsCanvasFactory.Create(GetGraphics());

			// render calendar to canvas
			CalendarRenderer.Instance.Render(content, canvas, rect);
		}

		/// <summary>
		/// Test the simple Render method call with not bound data
		/// </summary>
		[Test]
		public void CalendarRenderWithNoDataBound()
		{
			PageDocument report = TestHelper.GetReport("Calendar-NoData.rdlx");

			TestingRenderingExtension rendererExt = new TestingRenderingExtension();
			report.Render(rendererExt, null);

			CalendarDataRegion calendar = rendererExt.GetReportItem("Calendar1") as CalendarDataRegion;
			Assert.IsNotNull(calendar);

			CalendarData calendarData = new CalendarData(calendar);
			CalendarContentRange content = new CalendarContentRange(calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());

			// recalibrate graphics to twips
			RectangleF rect = new RectangleF(0, 0, 4000f, 4000f);
			CalendarDesigner.ScaleToTwipsGraphicsAndBound(GetGraphics(), ref rect);

			// create gdi canvas wrapper to use in renderer
			IDrawingCanvas canvas = GraphicsCanvasFactory.Create(GetGraphics());

			// render calendar to canvas
			CalendarRenderer.Instance.Render(content, canvas, rect);
		}

		/// <summary>
		/// Tests Calendar rendering in multi-threads.
		/// </summary>
		[Test]
		public void TestRenderingInMultiThreads()
		{
			var report = TestHelper.GetReport("Calendar.rdlx");
			var rendererExt = new TestingRenderingExtension();
			report.Render(rendererExt, null);

			var calendar = rendererExt.GetReportItem("Calendar1") as CalendarDataRegion;
			Assert.IsNotNull(calendar);

			var calendarData = new CalendarData(calendar);
			var content = new CalendarContentRange(calendar, calendarData.GetFirstMonth(), calendarData.GetLastMonth());

			// recalibrate graphics to twips
			var rect = new RectangleF(0, 0, 4000f, 4000f);
			CalendarDesigner.ScaleToTwipsGraphicsAndBound(GetGraphics(), ref rect);

			// the action of rendering calendar to canvas
			System.Action render = () =>
			{
				using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
				{
					var canvas = GraphicsCanvasFactory.Create(graphics);
					(calendar.GetRenderer<IGraphicsRenderer>() as CalendarRenderer)
						.Render(content, canvas, rect);
				}
			};

			var isSuccess1 = false;
			var isSuccess2 = false;

			ThreadStart render1 = () =>
			{
				render();
				isSuccess1 = true;
			};

			ThreadStart render2 = () =>
			{
				render();
				isSuccess2 = true;
			};

			var thread1 = new Thread(render1);
			var thread2 = new Thread(render2);

			thread1.Start();
			thread2.Start();

			thread1.Join();
			thread2.Join();

			Assert.IsTrue(isSuccess1);
			Assert.IsTrue(isSuccess2);
		}
	}
}
