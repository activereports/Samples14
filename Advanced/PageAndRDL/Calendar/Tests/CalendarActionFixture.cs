using System.Collections.Specialized;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Layout;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Drawing;
using NUnit.Framework;
using Action = GrapeCity.ActiveReports.Calendar.Components.Calendar.Action;
using IList = System.Collections.IList;
using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Drawing.Gdi;
using System.Drawing;
using System;

namespace GrapeCity.ActiveReports.Calendar.Tests
{
	/// <summary>
	/// Test of calendar action entities
	/// </summary>
	[TestFixture]
	public sealed class CalendarActionFixture
	{
		/// <summary>
		/// Test rendering extension, used for retrieving calendar's ILayoutArea after rendering process finished
		/// </summary>
		private sealed class ActionRenderingExtension : IRenderingExtension, ITargetDevice
		{
			private readonly IGraphicsRenderer _graphicsRenderer;
			private ILayoutArea _calendarLayoutArea;

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

			public ActionRenderingExtension()
			{
				_graphicsRenderer = CalendarRenderer.Instance;
			}

			public ILayoutArea CalendarLayoutArea
			{
				get
				{
					return _calendarLayoutArea;
				}
			}

			public void Render(IReport report, StreamProvider streams)
			{
				Render(report, streams, null);
			}

			public void Render(IReport report, StreamProvider streams, NameValueCollection settings)
			{
				ILayoutTree tree = GetLayoutTree(report, this, null);
				foreach (ILayoutPage page in tree.Pages)
				{
					RenderPage(page);
				}
			}

			private void RenderPage(ILayoutPage page)
			{
				ProcessLayoutArea(page.BodyArea);
			}

			private void ProcessLayoutArea(ILayoutArea area)
			{
				Assert.IsNotNull(area);
				Assert.IsNotNull(area.Children);

				foreach (ILayoutArea child in area.Children)
				{
					if (child.ContentRange is CalendarContentRange)
					{
						// TODO - fix!!!
						_graphicsRenderer.Render(new GraphicsRenderContext(this, GraphicsCanvasFactory.Create(GetGraphics()), new TextMetricsProvider(), RenderersFactory.Instance, null), child);
						_calendarLayoutArea = child;
						break;
					}
				}
			}

			private static ILayoutTree GetLayoutTree(IReport report, ITargetDevice targetDevice, LayoutNotificationCallback callback)
			{
				LayoutInfo layoutInfo = new LayoutInfo(report, targetDevice, new TextMetricsProvider(), callback);
				ILayoutEngineFactory factory = report.GetService(typeof(ILayoutEngineFactory)) as ILayoutEngineFactory;

				Assert.IsNotNull(factory);

				ILayoutEngine engine = factory.GetLayoutEngine();
				return engine.BuildLayout(layoutInfo);
			}

			#region ITargetDevice Members

			public DeviceCapabilities this[Features feature]
			{
				get { return new DeviceCapabilities(false, feature); }
			}

			#endregion
		}

		///<summary>
		/// Tests for bookmark action of appointment
		///</summary>
		[Test]
		public void CalendarBookmarklinkActionRenderingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Bookmarklink.rdlx");

			ActionRenderingExtension renderer = new ActionRenderingExtension();
			
			report.Render(renderer, null);

			Assert.IsNotNull(renderer.CalendarLayoutArea);
			Assert.IsNotNull(renderer.CalendarLayoutArea.InteractivityAreas);

			//StartTime(string),EndTime(string),Value(string)
			//2007-12-05,2007-12-07,"www.google.com" - appeared as one interactive area
			//2007-12-06,2007-12-07,"www.rambler.ru" - appeared as one interactive area
			//2007-12-07,2007-12-07,"www.ya.ru" - appeared as one interactive area 
			//2008-02-05,2008-03-07,"www.yahoo.com" - appeared as seven interactive areas
			Assert.AreEqual(11, renderer.CalendarLayoutArea.InteractivityAreas.Count);
			/*foreach (KeyValuePair<IImageMapArea, IAction> area in renderer.CalendarLayoutArea.InteractivityAreas)
			{
				Assert.IsTrue(area.Key.Shape == ImageMapShape.Rectangle);
				Assert.IsNotNull(area.Key.Coordinates);
				Assert.AreEqual(4, area.Key.Coordinates.Length);
				Assert.IsNotNull(area.Value);
				Assert.IsTrue(area.Value.ActionType == ActionType.BookmarkLink);
				Assert.IsFalse(string.IsNullOrEmpty(area.Value.BookmarkLink));
			}*/
		}

		///<summary>
		/// Tests for hyperlink action of appointment
		///</summary>
		[Test]
		public void CalendarHyperlinkActionRenderingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Hyperlink.rdlx");

			ActionRenderingExtension renderer = new ActionRenderingExtension();
			report.Render(renderer, null);

			Assert.IsNotNull(renderer.CalendarLayoutArea);
			Assert.IsNotNull(renderer.CalendarLayoutArea.InteractivityAreas);

			//StartTime(string),EndTime(string),Value(string)
			//2007-12-05,2007-12-07,"www.google.com" - appeared as one interactive area
			//2007-12-06,2007-12-07,"www.rambler.ru" - appeared as one interactive area
			//2007-12-07,2007-12-07,"www.ya.ru" - appeared as one interactive area 
			//2008-02-05,2008-03-07,"www.yahoo.com" - appeared as seven interactive areas

			Assert.AreEqual(11, renderer.CalendarLayoutArea.InteractivityAreas.Count);
			/*foreach (KeyValuePair<IImageMapArea, IAction> area in renderer.CalendarLayoutArea.InteractivityAreas)
			{
				Assert.IsTrue(area.Key.Shape == ImageMapShape.Rectangle);
				Assert.IsNotNull(area.Key.Coordinates);
				Assert.AreEqual(4, area.Key.Coordinates.Length);
				Assert.IsNotNull(area.Value);
				Assert.IsTrue(area.Value.ActionType == ActionType.HyperLink);
				Assert.IsFalse(string.IsNullOrEmpty(area.Value.HyperLink));
			}*/
		}

		///<summary>
		/// Tests for hyperlink action of appointment
		///</summary>
		[Test]
		public void CalendarDrillthroughActionRenderingTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-DrillthroughWithParameters.rdlx");

			ActionRenderingExtension renderer = new ActionRenderingExtension();
			report.Render(renderer, null);

			Assert.IsNotNull(renderer.CalendarLayoutArea);
			Assert.IsNotNull(renderer.CalendarLayoutArea.InteractivityAreas);

			//StartTime(string),EndTime(string),Value(string)
			//2007-12-05,2007-12-07,"Calendar.rdlx" - appeared as one interactive area
			//2007-12-06,2007-12-07,"Calendar.rdlx" - appeared as one interactive area
			//2007-12-07,2007-12-07,"Calendar.rdlx" - appeared as one interactive area 
			//2008-02-05,2008-03-07,"Calendar.rdlx" - appeared as seven interactive areas
			Assert.AreEqual(11, renderer.CalendarLayoutArea.InteractivityAreas.Count);
			/*foreach (KeyValuePair<IImageMapArea, IAction> area in renderer.CalendarLayoutArea.InteractivityAreas)
			{
				Assert.IsTrue(area.Key.Shape == ImageMapShape.Rectangle);
				Assert.IsNotNull(area.Key.Coordinates);
				Assert.AreEqual(4, area.Key.Coordinates.Length);
				Assert.IsNotNull(area.Value);
				Assert.IsTrue(area.Value.ActionType == ActionType.DrillThrough);
				Assert.IsNotNull(area.Value.Drillthrough);
				Assert.IsFalse(string.IsNullOrEmpty(area.Value.Drillthrough.ReportName));
				Assert.IsNotNull(area.Value.Drillthrough.Parameters);
				int parameterCounter = 0;
				foreach (DrillthroughParameter parameter in area.Value.Drillthrough.Parameters)
				{
					++parameterCounter;
					Assert.AreEqual("param", parameter.Name);
					Assert.AreEqual(true, parameter.Omit);
					Assert.IsFalse(string.IsNullOrEmpty(Convert.ToString(parameter.Value)));
				}
				Assert.AreEqual(2, parameterCounter);
			}*/
		}

		///<summary>
		/// Tests for bookmark action of appointment
		///</summary>
		[Test]
		public void CalendarBookmarklinkActionTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Bookmarklink.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			ICalendar calendar;

			// calendar1 (default values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.IsNotNull(appointment.Action);
				Assert.AreEqual(ActionType.BookmarkLink, appointment.Action.ActionType);
				Assert.AreEqual(appointment.Value, appointment.Action.BookmarkLink);
			}
		}

		///<summary>
		/// Tests for drillthrough action of appointment
		///</summary>
		[Test]
		public void CalendarDrillthroughActionTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-Drillthrough.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			ICalendar calendar;

			// calendar1 (default values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.IsNotNull(appointment.Action);
				Assert.AreEqual(ActionType.DrillThrough, appointment.Action.ActionType);
				Assert.IsNotNull(appointment.Action.Drillthrough);
				Assert.AreEqual(appointment.Value, appointment.Action.Drillthrough.ReportName);
				Assert.AreEqual(0, appointment.Action.Drillthrough.NumberOfParameters);
			}
		}

		///<summary>
		/// Tests for drillthrough with parameters action of appointment
		///</summary>
		[Test]
		public void CalendarDrillthroughWithParametersActionTest()
		{
			PageDocument report = TestHelper.GetReport("Calendar-DrillthroughWithParameters.rdlx");

			TestingRenderingExtension renderer = new TestingRenderingExtension();
			report.Render(renderer, null);

			ICalendar calendar;

			// calendar1 (default values)
			calendar = (ICalendar)renderer.GetReportItem("Calendar1");
			Assert.IsNotNull(calendar);
			foreach (Appointment appointment in calendar.Appointments)
			{
				Assert.IsNotNull(appointment.Action);
				Assert.AreEqual(ActionType.DrillThrough, appointment.Action.ActionType);
				Assert.IsNotNull(appointment.Action.Drillthrough);
				Assert.AreEqual(appointment.Value, appointment.Action.Drillthrough.ReportName);
				Assert.AreEqual(2, appointment.Action.Drillthrough.NumberOfParameters);

				foreach (DrillthroughParameter parameter in appointment.Action.Drillthrough.Parameters)
				{
					Assert.AreEqual(true, parameter.Omit);
					Assert.AreEqual("param", parameter.Name);
					Assert.AreEqual(appointment.Value, parameter.Value);
				}
			}
		}

		///<summary>
		/// Hyperlink Action test
		///</summary>
		[Test]
		public void ActionHyperLinkTest()
		{
			const string link = "http://www.grapecity.com";
			const int id = 0;

			Action action = Action.CreateHyperlink(link, id);

			Assert.AreEqual(ActionType.HyperLink, action.ActionType);
			Assert.AreEqual(link, action.HyperLink);
			Assert.AreEqual(id, action.ActionId);
		}

		///<summary>
		/// Bookmark Action test
		///</summary>
		[Test]
		public void ActionBookmarkLinkTest()
		{
			string bookmark = "bookmark";
			int id = 0;

			Action action = Action.CreateBookmark(bookmark, id);

			Assert.AreEqual(ActionType.BookmarkLink, action.ActionType);
			Assert.AreEqual(bookmark, action.BookmarkLink);
			Assert.AreEqual(id, action.ActionId);
		}

		///<summary>
		/// Drillthrough Action test
		///</summary>
		[Test]
		public void ActionDrillthroughTest1()
		{
			int id = 0;
			string reportName = "report";
			IDrillthrough drillthrough = Drillthrough.Create(reportName, null);

			Action action = Action.CreateDrillthrouth(drillthrough, id);

			Assert.AreEqual(ActionType.DrillThrough, action.ActionType);
			Assert.IsNotNull(action.Drillthrough);
			Assert.AreEqual(reportName, action.Drillthrough.ReportName);
			Assert.AreEqual(id, action.ActionId);
			Assert.AreEqual(0, action.Drillthrough.NumberOfParameters);
		}

		///<summary>
		/// Drillthrough Action test
		///</summary>
		[Test]
		public void ActionDrillthroughTest2()
		{
			const int id = 0;
			const string reportName = "report";
			DrillthroughParameter[] parameters = new[]
			{
				new DrillthroughParameter("a", 1, false),
				new DrillthroughParameter("b", 2, false),
			};

			IDrillthrough drillthrough = Drillthrough.Create(reportName, parameters);

			Action action = Action.CreateDrillthrouth(drillthrough, id);

			Assert.AreEqual(ActionType.DrillThrough, action.ActionType);
			Assert.IsNotNull(action.Drillthrough);
			Assert.AreEqual(reportName, action.Drillthrough.ReportName);
			Assert.AreEqual(id, action.ActionId);
			Assert.AreEqual(parameters.Length, action.Drillthrough.NumberOfParameters);
		}

		/// <summary>
		/// IDrillthrough implementation test
		/// </summary>
		[Test]
		public void DrillthroughTest()
		{
			string reportName = "reportName";
			IList parameters = new DrillthroughParameter[] { };
			IDrillthrough drillthrough = Components.Calendar.Drillthrough.Create(reportName, parameters);

			Assert.AreEqual(reportName, drillthrough.ReportName);
			Assert.AreEqual(parameters.Count, drillthrough.NumberOfParameters);

			IList parameters1 = new DrillthroughParameter[] { };
			IDrillthrough drillthrough1 = Components.Calendar.Drillthrough.Create(reportName, parameters);

			Assert.AreEqual(reportName, drillthrough1.ReportName);
			Assert.AreEqual(parameters1.Count, drillthrough1.NumberOfParameters);
		}

	}
}
