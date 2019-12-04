Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Extensibility.Layout
Imports System.Xml
Imports System.Drawing
Imports Svg
Imports GrapeCity.ActiveReports.Drawing

Public NotInheritable Class SvgImage
	Implements IReportItem
	Implements ICustomReportItem
	Implements IStaticItem
	Implements IReportItemRenderersFactory
#Region "Rendering"

	Private NotInheritable Class SvgRendererBridge
		Implements IGraphicsRenderer
		Sub Render(context As GraphicsRenderContext, area As ILayoutArea) Implements IGraphicsRenderer.Render
			Dim reportItem As SvgImage = CType(area.ReportItem, SvgImage)
			Dim svg As String = Convert.ToString(reportItem._properties.GetValue("Svg")).Trim()

			Dim doc As New XmlDocument()
			Try
				doc.LoadXml(svg)
			Catch ex As XmlException
				Throw New Exception(My.Resources.ExceptionMessage)
			End Try
			Dim svgDocument As SvgDocument = SvgDocument.Open(doc)

			Using renderer As New SvgRenderer(context, New RectangleF(area.Left, area.Top, area.Width, area.Height))
				svgDocument.Draw(renderer)
			End Using
		End Sub
	End Class

#End Region

#Region "Default CRI implementation"

	Private _properties As IPropertyBag
	Private _dataScope As IDataScope

	ReadOnly Property CustomData() As GrapeCity.ActiveReports.Extensibility.Rendering.CustomData Implements ICustomReportItem.CustomData
		Get
			Return TryCast(_dataScope, GrapeCity.ActiveReports.Extensibility.Rendering.CustomData)
		End Get
	End Property

	ReadOnly Property DataElementValue() As String Implements ICustomReportItem.DataElementValue
		Get
			Return Nothing
		End Get
	End Property

	Sub Initialize(dataContext As IDataScope, properties As IPropertyBag) Implements IReportItem.Initialize
		_dataScope = dataContext
		_properties = properties
	End Sub

	Function OnClick(reportItem As IReportItem, xPosition As Integer, yPosition As Integer, imageMapId As String, button As MouseButton) As ChangeResult Implements IReportItem.OnClick
		Return ChangeResult.None
	End Function

	ReadOnly Property Action() As IAction Implements IReportItem.Action
		Get
			Return Nothing
		End Get
	End Property

	ReadOnly Property ToggleItem() As String Implements IReportItem.ToggleItem
		Get
			Return (If(_properties.GetValue("ToggleItem"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property ZIndex() As Integer Implements IReportItem.ZIndex
		Get
			Return Convert.ToInt32(If(_properties.GetValue("ZIndex"), 0))
		End Get
	End Property

	ReadOnly Property Bookmark() As String Implements IReportItem.Bookmark
		Get
			Return (If(_properties.GetValue("Bookmark"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property Name() As String Implements IReportItem.Name
		Get
			Return (If(_properties.GetValue("Name"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property Width() As Length Implements IReportItem.Width
		Get
			Return CType((If(_properties.GetValue("Width"), Length.Empty)), Length)
		End Get
	End Property

	ReadOnly Property Height() As Length Implements IReportItem.Height
		Get
			Return CType((If(_properties.GetValue("Height"), Length.Empty)), Length)
		End Get
	End Property

	ReadOnly Property Top() As Length Implements IReportItem.Top
		Get
			Return CType((If(_properties.GetValue("Top"), Length.Empty)), Length)
		End Get
	End Property

	ReadOnly Property Left() As Length Implements IReportItem.Left
		Get
			Return CType((If(_properties.GetValue("Left"), Length.Empty)), Length)
		End Get
	End Property

	ReadOnly Property DataElementOutput() As GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput Implements IReportItem.DataElementOutput
		Get
			Return CType((If(_properties.GetValue("DataElementOutput"), GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput.NoOutput)), GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput)
		End Get
	End Property

	ReadOnly Property DataElementName() As String Implements IReportItem.DataElementName
		Get
			Return (If(_properties.GetValue("DataElementName"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property Style() As IStyle Implements IReportItem.Style
		Get
			Return CType(_properties.GetValue("Style"), IStyle)
		End Get
	End Property

	ReadOnly Property StyleName() As String Implements IReportItem.StyleName
		Get
			Return (If(_properties.GetValue("StyleName"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property Hidden() As Boolean Implements IReportItem.Hidden
		Get
			Return Convert.ToBoolean(If(_properties.GetValue("Hidden"), False))
		End Get
	End Property

	ReadOnly Property IsDynamicallyHidden() As Boolean Implements IReportItem.IsDynamicallyHidden
		Get
			Return Convert.ToBoolean(If(_properties.GetValue("IsDynamicallyHidden"), False))
		End Get
	End Property

	ReadOnly Property ToolTip() As String Implements IReportItem.ToolTip
		Get
			Return (If(_properties.GetValue("ToolTip"), String.Empty)).ToString()
		End Get
	End Property

	ReadOnly Property TargetDevice() As TargetDeviceKind Implements IReportItem.TargetDevice
		Get
			Return CType((If(_properties.GetValue("Target"), TargetDeviceKind.All)), TargetDeviceKind)
		End Get
	End Property

	ReadOnly Property KeepTogether() As Boolean Implements IReportItem.KeepTogether
		Get
			Return Convert.ToBoolean(If(_properties.GetValue("KeepTogether"), False))
		End Get
	End Property

	ReadOnly Property RenderComponents() As IEnumerable(Of IRenderComponent) Implements IRenderComponent.RenderComponents
		Get
			Return Enumerable.Empty(Of IRenderComponent)()
		End Get
	End Property

	Function GetService(serviceType As Type) As Object Implements IServiceProvider.GetService
		Dim definition = TryCast(_properties.GetValue("ReportItemDefinition"), IServiceProvider)
		If definition IsNot Nothing Then
			Return definition.GetService(serviceType)
		End If

		Return Nothing
	End Function

	Public Function GetRenderer(Of TRenderer As {Class, IRenderer})() As TRenderer Implements IReportItemRenderersFactory.GetRenderer
		Return If(GetType(TRenderer) = GetType(IGraphicsRenderer), CType(CType(New SvgRendererBridge(), IGraphicsRenderer), TRenderer), Nothing)
	End Function

	ReadOnly Property Label() As String Implements IDocumentMapItem.Label
		Get
			Return _properties.GetValue("Label").ToString()
		End Get
	End Property

#End Region
End Class
