Imports System.Xml
Imports System.Runtime.InteropServices
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Designers
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Behavior
Imports System.Windows.Forms
Imports System.Drawing.Design
Imports System.Drawing
Imports System.ComponentModel.Design
Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports GrapeCity.ActiveReports.Drawing.Gdi
Imports Svg


<Guid("ED362C95-D11C-4D98-ACF2-060F9A7FE68C")>
<ToolboxBitmap(GetType(SvgDesigner), "SvgIcon.png")>
<DisplayName("SvgImage")>
Public NotInheritable Class SvgDesigner
	Inherits CustomReportItemDesigner
	Implements IValidateable

	Private _designerVerbCollection As DesignerVerbCollection
	Public Sub New()
		AddProperty(Me, Function(x) x.Svg, CategoryAttribute.Data, New DescriptionAttribute(My.Resources.SvgDescription), New DisplayNameAttribute(My.Resources.SvgDisplayName), New EditorAttribute(GetType(MultilineStringEditor), GetType(UITypeEditor)))
	End Sub

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing Then
			_graphics.Dispose()
			If _metafile IsNot Nothing Then _metafile.Dispose()
			_metafile = Nothing
		End If

		MyBase.Dispose(disposing)
	End Sub

	Function Validate(context As ValidationContext) As ValidationEntry() Implements IValidateable.Validate
		Return New ValidationEntry(0) {}
	End Function

#Region "Public properties"

	Public Overrides ReadOnly Property Verbs() As DesignerVerbCollection
		Get
			If IsNothing(_designerVerbCollection) Then
				_designerVerbCollection = New DesignerVerbCollection()
			End If
			Return _designerVerbCollection
		End Get
	End Property

	Public Property Svg() As String
		Get
			Dim customProperty = ReportItem.CustomProperties("Svg")
			If customProperty IsNot Nothing Then
				Dim expValue = customProperty.Value
				If expValue.IsConstant Then
					Return expValue.ToString()
				End If
			End If
			Return String.Empty
		End Get
		Set
			_metafile = Nothing
			Dim customProperty = ReportItem.CustomProperties("Svg")
			If customProperty IsNot Nothing Then
				ReportItem.CustomProperties.Remove(customProperty)
			End If
			customProperty = New CustomPropertyDefinition("Svg", Value)
			ReportItem.CustomProperties.Add(customProperty)
		End Set
	End Property

#End Region

#Region "Design-time rendering"

	Private ReadOnly _graphics As Graphics = SafeGraphics.CreateReferenceGraphics()
	Private _metafile As Metafile

	Friend ReadOnly Property RenderedSvg As Metafile
		Get
			If String.IsNullOrWhiteSpace(Svg) Then Return Nothing
			If _metafile IsNot Nothing Then Return _metafile
			Dim doc = New XmlDocument()
			doc.LoadXml(Svg)
			Dim document = SvgDocument.Open(doc)
			_metafile = New Metafile(_graphics.GetHdc(), (CType(document, ISvgBoundable)).Bounds, MetafileFrameUnit.Pixel, EmfType.EmfPlusDual)

			Try
				Using gfx = Graphics.FromImage(_metafile)
					document.Draw(gfx)
				End Using
			Finally
				_graphics.ReleaseHdc()
			End Try

			Return _metafile
		End Get
	End Property

	Private _controlGlyph As SvgControlGlyph

	Public Overrides ReadOnly Property ControlGlyph() As Glyph
		Get
			If (Not IsNothing(_controlGlyph)) Then
				Return _controlGlyph
			Else
				_controlGlyph = New SvgControlGlyph(ReportItem, Me)
				Return _controlGlyph
			End If
		End Get
	End Property

	Private NotInheritable Class SvgControlGlyph
		Inherits ControlBodyGlyph
		Public Sub New(reportItem As ReportItem, designer As SvgDesigner)
			MyBase.New(designer.BehaviorService, designer)
			Behavior = New MovableBehavior(reportItem, Me)
		End Sub

		Public Overrides Sub Paint(pe As PaintEventArgs)
			If PaintSelectionOnly(pe) Then
				MyBase.Paint(pe)
				Return
			End If

			pe.Graphics.FillRectangle(BackgroundBrush, Bounds)
			Try
				Dim svg = (CType(ComponentDesigner, SvgDesigner)).RenderedSvg
				If svg IsNot Nothing Then pe.Graphics.DrawImage(svg, Bounds)
			Catch ex As Exception
				pe.Graphics.DrawString(ex.Message, SystemFonts.DefaultFont, SystemBrushes.ControlText, Bounds, StringFormat.GenericTypographic)
			End Try

			MyBase.Paint(pe)
		End Sub

		Private NotInheritable Class MovableBehavior
			Inherits DefaultControlBehavior
			Private ReadOnly _reportItem As ReportItem
			Private ReadOnly _glyph As Glyph

			Public Sub New(reportItem As ReportItem, glyph As Glyph)
				_reportItem = reportItem
				_glyph = glyph
			End Sub

			Public Overrides ReadOnly Property Cursor() As Cursor
				Get
					Return If(CanMoveGlyph(_glyph) AndAlso IsActiveLayerItem(_reportItem), Cursors.SizeAll, MyBase.Cursor)
				End Get
			End Property

			Private Shared Function IsActiveLayerItem(item As ReportItem) As Boolean
				If item Is Nothing OrElse item.Site Is Nothing Then
					Return True
				End If
				Dim host = TryCast(item.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
				If host Is Nothing Then
					Return True
				End If
				Dim reportDef = TryCast(host.RootComponent, PageReport)
				If reportDef Is Nothing Then
					Return True
				End If
				Dim reportDesigner = TryCast(host.GetDesigner(reportDef.Report), ReportDesigner)
				If reportDesigner Is Nothing Then
					Return True
				End If
				Dim activeLayer = reportDesigner.ActiveLayer
				Return String.Equals(activeLayer, item.LayerName, StringComparison.InvariantCultureIgnoreCase)
			End Function
		End Class
	End Class

#End Region
End Class
