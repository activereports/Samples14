Imports GrapeCity.ActiveReports.Viewer.Win.Internal.Export
Imports GrapeCity.ActiveReports.PageReportModel
Imports System.Xml.XPath
Imports System.Xml
Imports System.IO
Imports GrapeCity.ActiveReports

Namespace GrapeCity.ActiveReports.Viewer.Helper

	Public Class ViewerHelper
		''' <summary>
		''' Determines if the specified report is FPL report
		''' </summary>
		Public Shared Function DetermineReportType(fileName As FileInfo) As ExportForm.ReportType
			Dim extension As Object = Path.GetExtension(fileName.FullName).ToLowerInvariant()
			If extension = ".json" OrElse extension = ".bson" Then
				Return ExportForm.ReportType.PageCpl
			End If
			If extension = ".rdf" OrElse extension = ".rpx" Then
				Return ExportForm.ReportType.Section
			End If

			Dim report As PageReport
			Try
				report = New PageReport(fileName)
			Catch generatedExceptionName As ReportException
				Return ExportForm.ReportType.Section
			Catch generatedExceptionName As XmlException
				Return ExportForm.ReportType.Section
			End Try
			If IsNothing(report) OrElse IsNothing(report.Report) OrElse IsNothing(report.Report.Body) Then
				Return ExportForm.ReportType.Section
			End If

			Dim items As ReportItemCollection = report.Report.Body.ReportItems
			Return If(Not IsNothing(items) AndAlso items.Count = 1 AndAlso TypeOf items(0) Is FixedPage, ExportForm.ReportType.PageFpl, ExportForm.ReportType.PageCpl)
		End Function

		''' <summary>
		''' Checks file extansion.
		''' </summary>
		Public Shared Function IsRdf(fileName As FileInfo) As Boolean
			Dim extension As Object = Path.GetExtension(fileName.FullName)
			Return ".rdf".Equals(extension)
		End Function

		Public Shared Function GetReportServerUri(fileInfo As FileInfo) As String
			Dim content = File.ReadAllText(fileInfo.FullName)
			Dim document As XDocument = XDocument.Load(New StringReader(content))

			If IsNothing(document.Root) Then
				Return String.Empty
			End If

			Dim ns = document.Root.GetDefaultNamespace()
			Dim namespaceManager As New XmlNamespaceManager(New NameTable())
			namespaceManager.AddNamespace("ns", ns.NamespaceName)

			' try to get remote server uri from rdlx document
			Dim element = document.XPathSelectElement("/ns:Report/ns:CustomProperties/ns:CustomProperty[ns:Name[text()='ReportServerUri']]/ns:Value", namespaceManager)
			If Not IsNothing(element) Then
				Return element.Value
			End If

			' try to get remote server uri from rpx document
			element = document.XPathSelectElement("/ns:ActiveReportsLayout", namespaceManager)
			Dim attribute = If(Not IsNothing(element), element.Attribute("ReportServerUri"), Nothing)
			Return If(Not IsNothing(attribute), attribute.Value, String.Empty)
		End Function
	End Class
End Namespace
