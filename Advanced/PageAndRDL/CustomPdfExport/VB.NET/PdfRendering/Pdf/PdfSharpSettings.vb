Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports System.Globalization
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports System
Imports GrapeCity.ActiveReports.PageReportModel

''' <summary>
''' PDF settings.
''' </summary>
Public NotInheritable Class PdfSharpSettings
		Implements ISettings

		''' <summary>
		''' Initializes a new instance of the <see cref="PdfSharpSettings"/> class.
		''' </summary>
		Public Sub New()
			EmbedFonts = True
			EndPage = -1
			Target = TargetDeviceKind.Export
		End Sub

		''' <summary>
		''' Initializes a new instance of the <see cref="Settings"/> class.
		''' </summary>
		Public Sub New(settings As NameValueCollection)
			Me.New()
			CType(Me, ISettings).ApplySettings(settings)
		End Sub



	''' <summary>
	''' Embeds fonts or not.
	''' </summary>
	<DefaultValue(True)>
	<PdfDisplayName("EmbedFonts")>
	<PdfDescription("EmbedFontsDesc")>
	Public Property EmbedFonts() As Boolean

	''' <summary>
	''' Start page number.
	''' </summary>
	<DefaultValue(0)>
	<PdfDisplayName("StartPage")>
	<PdfDescription("StartPageDesc")>
	Public Property StartPage() As Integer

	''' <summary>
	''' End page number.
	''' </summary>
	<DefaultValue(-1)>
	<PdfDisplayName("EndPage")>
	<PdfDescription("EndPageDesc")>
	Public Property EndPage() As Integer

		' just parsing
		Friend Property Target() As TargetDeviceKind

#Region "ISettings implementation"

		''' <summary>
		''' Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing the settings for the rendering extension.
		''' </summary>        
		Function GetSettings() As NameValueCollection Implements ISettings.GetSettings
			Dim settings As New NameValueCollection()
			If Not EmbedFonts Then
				settings("EmbedFonts") = Boolean.FalseString
			End If
			If StartPage <> 0 Then
				settings("StartPage") = StartPage.ToString(CultureInfo.InvariantCulture)
			End If
			If EndPage <> -1 Then
				settings("EndPage") = EndPage.ToString(CultureInfo.InvariantCulture)
			End If
			If Target <> TargetDeviceKind.Export Then
				settings("Target") = Target.ToString()
			End If
			Return settings
		End Function

		''' <summary>
		''' Apply settings for the rendering extension.
		''' </summary>
		''' <param name="settings">Settings for the rendering extension.</param>
		Sub ApplySettings(settings As NameValueCollection) Implements ISettings.ApplySettings
			settings = If(settings, New NameValueCollection())
			EmbedFonts = String.IsNullOrEmpty(settings.[Get]("EmbedFonts")) OrElse settings.[Get]("EmbedFonts").Equals(Boolean.TrueString, StringComparison.OrdinalIgnoreCase)
			Dim page As Integer
			StartPage = If(Integer.TryParse(If(settings.[Get]("StartPage"), String.Empty), page), page, 0)
			EndPage = If(Integer.TryParse(If(settings.[Get]("EndPage"), String.Empty), page), page, -1)
			' just parsing
			Target = If(String.IsNullOrEmpty(settings.[Get]("Target")), TargetDeviceKind.Export, CType([Enum].Parse(GetType(TargetDeviceKind), settings.[Get]("Target"), True), TargetDeviceKind))
		End Sub

#End Region
	End Class
