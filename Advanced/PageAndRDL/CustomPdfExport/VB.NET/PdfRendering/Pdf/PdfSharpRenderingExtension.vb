Imports GrapeCity.ActiveReports.Rendering.Export
Imports GrapeCity.ActiveReports.Extensibility.Rendering.IO
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports System.Collections.Specialized
Imports System

''' <summary>
''' <see cref="IRenderingExtension" /> implementation used to create a PDF.
''' </summary>
Public NotInheritable Class PdfSharpRenderingExtension
	Implements IRenderingExtension
	Implements IConfigurable
#Region "IRenderingExtension implementation"

	''' <summary>
	''' Renders report.
	''' </summary>
	Sub Render(report As IReport, streamProvider As StreamProvider) Implements IRenderingExtension.Render
		CType(Me, IRenderingExtension).Render(report, streamProvider, Nothing)
	End Sub

	''' <summary>
	''' Renders report.
	''' </summary>
	Sub Render(report As IReport, streamProvider As StreamProvider, settings As NameValueCollection) Implements IRenderingExtension.Render
		If report Is Nothing Then
			Throw New ArgumentNullException("report")
		End If
		If streamProvider Is Nothing Then
			Throw New ArgumentNullException("streamProvider")
		End If

		settings = If(settings, New NameValueCollection())
		Dim pdfSettings = New PdfSharpSettings(settings)

		Try
			Dim primaryStream = If(streamProvider.GetPrimaryStream(), streamProvider.CreatePrimaryStream("application/pdf", ".pdf"))
			Using outputStream = primaryStream.OpenStream()
				Using generator As New PdfGenerator(outputStream, pdfSettings.EmbedFonts)
					Dim renderingCore As New DocumentRenderer(report, generator)
					Dim startPage As Integer = pdfSettings.StartPage
					Dim endPage As Integer = Math.Max(startPage, pdfSettings.EndPage)
					If startPage <= 0 Then
						startPage = 1
					End If
					If endPage <= 0 Then
						endPage = Integer.MaxValue
					End If
					renderingCore.Render(pdfSettings.Target, False, New ImageRenderers.PageControl.PageControllerSettings() With {
						.EndPage = endPage,
						.StartPage = startPage
					})
				End Using
			End Using
		Catch
			streamProvider.CleanUpOnError()
			Throw
		End Try
	End Sub

#End Region

#Region "IConfigurable implementation"

	''' <summary>
	''' Gets settings.
	''' </summary>
	Function GetSupportedSettings() As ISettings Implements IConfigurable.GetSupportedSettings
		Return New PdfSharpSettings()
	End Function

	''' <summary>
	''' Gets settings.
	''' </summary>
	Function GetSupportedSettings(fpl As Boolean) As ISettings Implements IConfigurable.GetSupportedSettings
		Return New PdfSharpSettings()
	End Function

#End Region
End Class
