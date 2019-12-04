Imports GrapeCity.ActiveReports.Extensibility.Rendering.IO
Imports GrapeCity.ActiveReports.Viewer.Win.Internal.Export
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Export
Imports GrapeCity.ActiveReports.Document
Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Specialized
Imports System

Friend Class ExportViewer
	Implements IExportViewer
	Private ReadOnly _viewer As GrapeCity.ActiveReports.Viewer.Win.Viewer

	Public Sub New(viewer As GrapeCity.ActiveReports.Viewer.Win.Viewer)
		_viewer = viewer
	End Sub

	Public Sub Export(export As IDocumentExportEx, file As FileInfo) Implements IExportViewer.Export
		_viewer.Export(export, file)
	End Sub

	Public Sub Export(export As IDocumentExportEx, stream As FileStream) Implements IExportViewer.Export
		_viewer.Export(export, stream)
	End Sub

	Public Sub Render(export As IRenderingExtension, streamProvider As StreamProvider, settings As NameValueCollection) Implements IExportViewer.Render
		_viewer.Render(export, streamProvider, settings)
	End Sub

	Public ReadOnly Property Document() As SectionDocument Implements IExportViewer.Document

		Get
			Return _viewer.Document
		End Get
	End Property

	Public ReadOnly Property CanExport() As Boolean Implements IExportViewer.CanExport
		Get
			Return _viewer.CanExport
		End Get
	End Property

	Public ReadOnly Property Owner() As IWin32Window Implements IExportViewer.Owner
		Get
			Return _viewer
		End Get
	End Property

	Public Sub HandleError(exception As Exception) Implements IExportViewer.HandleError
		_viewer.BeginInvoke(New MethodInvoker(Sub() _viewer.HandleError(exception)))
	End Sub
End Class
