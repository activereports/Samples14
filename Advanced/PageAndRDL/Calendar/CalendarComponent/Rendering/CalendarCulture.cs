using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Calendar.Rendering
{
	/// <summary>
	/// Provides method allows to get the culture should be used to format the calendar view.
	/// </summary>
	public abstract class CalendarCulture
	{
		/// <summary>
		/// Obtains and returns the the culture should be used to format the calendar view.
		/// </summary>
		/// <returns>the culture should be used to format the calendar view.</returns>
		public abstract CultureInfo GetCulture();

		/// <summary>
		/// Returns <see cref="CultureInfo"/> determined from the specified language.
		/// </summary>
		public static CultureInfo CultureFromLanguage(string languageName)
		{
			if (string.IsNullOrEmpty(languageName) || EvaluatorService.IsEmptyExpression(ExpressionInfo.FromString(languageName)))
			{
				return null;
			}
			CultureInfo culture = null;
			try
			{
				culture = new CultureInfo(languageName);
				if (culture.IsNeutralCulture)
				{
					culture = CultureInfo.CreateSpecificCulture(culture.Name);
				}
			}
			catch (ArgumentException)
			{
				Trace.TraceError("Invalid language specified ({0}).", languageName);
			}
			return culture;
		}
	}

	/// <summary>
	/// Implementation of CalendarCulture for design-time rendering.
	/// </summary>
	public sealed class CalendarDesignerCulture : CalendarCulture
	{
		private readonly CustomReportItem _cri;
		private readonly Report _parentReport;
		/// <summary>
		/// Initializes new instance of CalendarDesignerCulture.
		/// </summary>
		/// <param name="cri">Calendar report item that CalendarDesignerCulture is initialized for.</param>
		public CalendarDesignerCulture(CustomReportItem cri)
		{
			_cri = cri;
			IDesignerHost host = _cri.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (host == null)
			{
				Debug.Fail("Can get IDesignerHost for calendar report item");
				return;
			}
			PageReport def = host.RootComponent as PageReport;
			if (def == null)
			{
				Debug.Fail("Can get report definition from the host's root component");
				return;
			}
			_parentReport = def.Report;
		}

		#region CalendarCulture Members

		/// <summary>
		/// Obtains and returns the the culture should be used to format the calendar view.
		/// </summary>
		/// <returns>the culture should be used to format the calendar view.</returns>
		public override CultureInfo GetCulture()
		{
			CultureInfo culture = null;
			string language = _cri.Style.Language.IsInheritedValue() ? Style.DefaultStyle.Language : _cri.Style.Language;
			if (!string.IsNullOrEmpty(language))
			{
				culture = CultureFromLanguage(language);
			}
			if (culture == null)
			{
				culture = CultureFromLanguage(_parentReport.Language);
			}
			return culture ?? CultureInfo.CurrentCulture;
		}

		#endregion
	}

	/// <summary>
	/// Implementation of CalendarCulture for runtime rendering.
	/// </summary>
	internal sealed class CalendarRendererCulture : CalendarCulture
	{
		private readonly IReportItem _reportItem;
		private readonly IReport _parentReport;
		/// <summary>
		/// Initializes new instance of CalendarRendererCulture.
		/// </summary>
		/// <param name="reportItem">Calendar report item that CalendarRendererCulture is initialized for.</param>
		public CalendarRendererCulture(IReportItem reportItem, IReport report)
		{
			_reportItem = reportItem;
			_parentReport = report;
		}

		#region CalendarCulture Members

		/// <summary>
		/// Obtains and returns the the culture should be used to format the calendar view.
		/// </summary>
		/// <returns>the culture should be used to format the calendar view.</returns>
		public override CultureInfo GetCulture()
		{
			CultureInfo culture = null;
			string language = _reportItem.Style.Language;
			if (!string.IsNullOrEmpty(language))
			{
				culture = CultureFromLanguage(language);
			}
			if (culture == null && _parentReport != null)
			{
				culture = _parentReport.Culture;
			}
			return culture ?? CultureInfo.CurrentCulture;
		}

		#endregion
	}
}
