Imports System.Drawing

Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Extensibility.Layout
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components
Imports GrapeCity.ActiveReports.Samples.Rtf.Rendering
Imports GrapeCity.ActiveReports.Samples.Rtf.Rendering.Layout

Namespace Control
	<LayoutManager(GetType(RtfControlLayoutManager))>
	Public NotInheritable Class RtfControl
		Implements IReportItem, ICustomReportItem, IStaticItem, IReportItemRenderersFactory

		Function GetRenderer(Of TRenderer As {Class, IRenderer})() As TRenderer Implements IReportItemRenderersFactory.GetRenderer
			Return If(GetType(TRenderer) = GetType(IImageRenderer), CType(CType(New RtfRendererBridge(), IImageRenderer), TRenderer), Nothing)
		End Function

		Private NotInheritable Class RtfRendererBridge
			Implements IImageRenderer

			Public Function Render(area As ILayoutArea, mimeType As String, dpi As SizeF) As ImageInfo Implements IImageRenderer.Render
				Dim content = CType(area.ContentRange, RtfControlContentRange)
				Dim rtf = content.Info.Rtf
				
				Dim sizeInInches = New SizeF(area.Width / 1440, area.Height / 1440)
				Dim stream = RenderToStream(rtf, sizeInInches)

				Return New ImageInfo(stream, "image/emf")
			End Function

			Public Function RenderBackground(layoutArea As ILayoutArea, mimeType As String, dpi As SizeF) As ImageInfo Implements IImageRenderer.RenderBackground
				Return Nothing
			End Function
		End Class

		Private _properties As IPropertyBag
		Private _dataScope As IDataScope
		
		Public ReadOnly Property Rtf As String
			Get
				Return _properties.GetValue("Rtf").ToString()
			End Get
		End Property

		Public ReadOnly Property CanGrow As Boolean
			Get
				Return Convert.ToBoolean(_properties.GetValue("CanGrow"))
			End Get
		End Property

		Public ReadOnly Property CanShrink As Boolean
			Get
				Return Convert.ToBoolean(_properties.GetValue("CanShrink"))
			End Get
		End Property

		Private ReadOnly Property CustomData As Extensibility.Rendering.CustomData Implements ICustomReportItem.CustomData
			Get
				Return TryCast(_dataScope, Extensibility.Rendering.CustomData)
			End Get
		End Property

		Private ReadOnly Property DataElementValue As String Implements ICustomReportItem.DataElementValue
			Get
				Return Nothing
			End Get
		End Property

		Private Sub Initialize(ByVal dataContext As IDataScope, ByVal properties As IPropertyBag) Implements IReportItem.Initialize
			_dataScope = dataContext
			_properties = properties
		End Sub

		Private Function OnClick(ByVal reportItem As IReportItem, ByVal xPosition As Integer, ByVal yPosition As Integer, ByVal imageMapId As String, ByVal button As MouseButton) As ChangeResult Implements IReportItem.OnClick
			Return ChangeResult.None
		End Function

		Private ReadOnly Property Action As IAction Implements IReportItem.Action
			Get
				Return Nothing
			End Get
		End Property

		Private ReadOnly Property ToggleItem As String Implements IReportItem.ToggleItem
			Get
				Return If(_properties.GetValue("ToggleItem"), String.Empty).ToString()
			End Get
		End Property

		Private ReadOnly Property ZIndex As Integer Implements IReportItem.ZIndex
			Get
				Return Convert.ToInt32(If(_properties.GetValue("ZIndex"), 0))
			End Get
		End Property

		Private ReadOnly Property Bookmark As String Implements IReportItem.Bookmark
			Get
				Return (If(_properties.GetValue("Bookmark"), String.Empty)).ToString()
			End Get
		End Property

		Private ReadOnly Property Name As String Implements IReportItem.Name
			Get
				Return (If(_properties.GetValue("Name"), String.Empty)).ToString()
			End Get
		End Property

		Private ReadOnly Property Width As Length Implements IReportItem.Width
			Get
				Return CType((If(_properties.GetValue("Width"), Length.Empty)), Length)
			End Get
		End Property

		Private ReadOnly Property Height As Length Implements IReportItem.Height
			Get
				Return CType((If(_properties.GetValue("Height"), Length.Empty)), Length)
			End Get
		End Property

		Private ReadOnly Property Top As Length Implements IReportItem.Top
			Get
				Return CType((If(_properties.GetValue("Top"), Length.Empty)), Length)
			End Get
		End Property

		Private ReadOnly Property Left As Length Implements IReportItem.Left
			Get
				Return CType((If(_properties.GetValue("Left"), Length.Empty)), Length)
			End Get
		End Property

		Private ReadOnly Property DataElementOutput As Components.DataElementOutput Implements IReportItem.DataElementOutput
			Get
				Return CType((If(_properties.GetValue("DataElementOutput"), Components.DataElementOutput.NoOutput)), Components.DataElementOutput)
			End Get
		End Property

		Private ReadOnly Property DataElementName As String Implements IReportItem.DataElementName
			Get
				Return (If(_properties.GetValue("DataElementName"), String.Empty)).ToString()
			End Get
		End Property

		Private ReadOnly Property Style As IStyle Implements IReportItem.Style
			Get
				Return CType(_properties.GetValue("Style"), IStyle)
			End Get
		End Property

		Private ReadOnly Property StyleName As String Implements IReportItem.StyleName
			Get
				Return (If(_properties.GetValue("StyleName"), String.Empty)).ToString()
			End Get
		End Property

		Private ReadOnly Property Hidden As Boolean Implements IReportItem.Hidden
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("Hidden"), False))
			End Get
		End Property

		Private ReadOnly Property IsDynamicallyHidden As Boolean Implements IReportItem.IsDynamicallyHidden
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("IsDynamicallyHidden"), False))
			End Get
		End Property

		Private ReadOnly Property ToolTip As String Implements IReportItem.ToolTip
			Get
				Return (If(_properties.GetValue("ToolTip"), String.Empty)).ToString()
			End Get
		End Property

		Private ReadOnly Property TargetDevice As TargetDeviceKind Implements IReportItem.TargetDevice
			Get
				Return CType((If(_properties.GetValue("Target"), TargetDeviceKind.All)), TargetDeviceKind)
			End Get
		End Property

		Private ReadOnly Property KeepTogether As Boolean Implements IReportItem.KeepTogether
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("KeepTogether"), False))
			End Get
		End Property

		Private ReadOnly Property RenderComponents As IEnumerable(Of IRenderComponent) Implements IRenderComponent.RenderComponents
			Get
				Return Enumerable.Empty(Of IRenderComponent)()
			End Get
		End Property

		Private Function GetService(ByVal serviceType As Type) As Object Implements IServiceProvider.GetService
			Dim definition = TryCast(_properties.GetValue("ReportItemDefinition"), IServiceProvider)
			If definition IsNot Nothing Then Return definition.GetService(serviceType)
			Return Nothing
		End Function

		Private ReadOnly Property Label As String Implements IDocumentMapItem.Label
			Get
				Return _properties.GetValue("Label").ToString()
			End Get
		End Property
	End Class
End NameSpace