using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;

namespace GrapeCity.ActiveReports.Viewer.Helper

{
	internal static class ViewerHelper
	{
		/// <summary>
		/// Determines if the specified report is FPL report
		/// </summary>
		public static ExportForm.ReportType DetermineReportType(FileInfo fileName)
		{
			var extension = Path.GetExtension(fileName.FullName).ToLowerInvariant();
			if (extension == ".json" || extension == ".bson") return ExportForm.ReportType.PageCpl;
			if (extension == ".rdf" || extension == ".rpx") return ExportForm.ReportType.Section;

			PageReport report;
			try
			{
				report = new PageReport(fileName);
			}
			catch (ReportException)
			{
				return ExportForm.ReportType.Section;
			}
			catch (XmlException)
			{
				return ExportForm.ReportType.Section;
			}
			if (report == null || report.Report == null || report.Report.Body == null) return ExportForm.ReportType.Section;

			ReportItemCollection items = report.Report.Body.ReportItems;
			return items != null && items.Count == 1 && items[0] is FixedPage ? ExportForm.ReportType.PageFpl : ExportForm.ReportType.PageCpl;
		}

		/// <summary>
		/// Checks file extansion.
		/// </summary>
		public static bool IsRdf(FileInfo fileName)
		{
			var extension = Path.GetExtension(fileName.FullName);
			return ".rdf".Equals(extension, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string GetReportServerUri(FileInfo file)
		{
			var content = File.ReadAllText(file.FullName);
			XDocument document = XDocument.Load(new StringReader(content));

			if (document.Root == null)
				return string.Empty;

			var ns = document.Root.GetDefaultNamespace();
			var namespaceManager = new XmlNamespaceManager(new NameTable());
			namespaceManager.AddNamespace("ns", ns.NamespaceName);

			// try to get remote server uri from rdlx document
			var element = document.XPathSelectElement("/ns:Report/ns:CustomProperties/ns:CustomProperty[ns:Name[text()='ReportServerUri']]/ns:Value", namespaceManager);
			if (element != null)
				return element.Value;

			// try to get remote server uri from rpx document
			element = document.XPathSelectElement("/ns:ActiveReportsLayout", namespaceManager);
			var attribute = element != null ? element.Attribute("ReportServerUri") : null;
			return attribute != null ? attribute.Value : string.Empty;
		}
	}
}
