Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting

Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Extensibility.Layout
Imports GrapeCity.ActiveReports.Extensibility.Rendering
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components

Public NotInheritable Class RadarChart
		Implements IDataRegion
		Implements ICustomReportItem
		Implements IStaticItem
		Implements IReportItemRenderersFactory
		Private Const SeriesValueName As String = "SeriesValue"

#Region "Rendering"

		Private NotInheritable Class ImageRenderer
			Implements IImageRenderer
			Function Render(layoutArea As ILayoutArea, mimeType As String, dpi As SizeF) As ImageInfo Implements IImageRenderer.Render
				Dim reportItem As Object = layoutArea.ReportItem

				Dim customData As Object = (CType(reportItem, ICustomReportItem)).CustomData
				If customData Is Nothing Then
					Return New ImageInfo()
				End If
				If customData.DataRowGroupings Is Nothing OrElse customData.DataRowGroupings.Count <> 1 Then
					Return New ImageInfo()
				End If
				Dim values As New List(Of Double)
				values.Add(customData.DataRowGroupings(0).Count)

				' read the values from nested data members
				For Each member As DataMember In customData.DataRowGroupings(0)
					Dim value As Double
					If ReadValue(member, value) Then
						' we have to check that there is corresponding property to read series value
						values.Add(value)
					End If
				Next

				Dim bounds As Object = GetImageBounds(reportItem, layoutArea)
				If dpi.IsEmpty Then
					dpi = New SizeF(Resolution.Width, Resolution.Height)
				End If
				Dim imageSize As New Size(FromTwipsToPixels(bounds.Width, dpi.Width), FromTwipsToPixels(bounds.Height, dpi.Height))
				If imageSize.Width = 0 OrElse imageSize.Height = 0 Then
					Return New ImageInfo()
				End If

				Using nonScaledImage As New Bitmap(imageSize.Width, imageSize.Height)
					nonScaledImage.SetResolution(dpi.Width, dpi.Height)
					Using chart As New System.Windows.Forms.DataVisualization.Charting.Chart() With {
						.Dock = DockStyle.Fill,
						.Size = imageSize
					}
						Dim chartArea As New ChartArea("Main")
						chart.ChartAreas.Add(chartArea)

						' Create series and add it to the chart
						Dim seriesColumns As New Series("Columns")
						For Each val As Object In values
							seriesColumns.Points.Add(Convert.ToDouble(val))
						Next
						seriesColumns.ChartType = SeriesChartType.Radar

						chart.Series.Add(seriesColumns)
						chart.DrawToBitmap(nonScaledImage, New Rectangle(Point.Empty, imageSize))
					End Using
					Dim stream As New MemoryStream()
					nonScaledImage.Save(stream, ImageFormat.Png)
					stream.Flush()
					stream.Position = 0
					Return New ImageInfo(stream, "image/png")
				End Using
			End Function

			Function RenderBackground(layoutArea As ILayoutArea, mimeType As String, dpi As SizeF) As ImageInfo Implements IImageRenderer.RenderBackground
				Return Nothing
			End Function

			Private Shared Function ReadValue(dataMember As DataMember, ByRef value As Double) As Boolean
				If dataMember Is Nothing Then
					Throw New ArgumentNullException("dataMember")
				End If
				value = 0
				Dim labelProperty As Object = dataMember.CustomProperties(SeriesValueName)
				If labelProperty IsNot Nothing Then
					value = Convert.ToDouble(labelProperty.Value, CultureInfo.InvariantCulture)
					Return True
				End If
				Return False
			End Function

			Private Shared Function GetImageBounds(reportItem As IReportItem, layoutArea As ILayoutArea) As RectangleF
				Return If(Not IsNothing(layoutArea), New RectangleF(layoutArea.Left, layoutArea.Top, layoutArea.Width, layoutArea.Height), New RectangleF(reportItem.Left.ToTwips(), reportItem.Top.ToTwips(), reportItem.Width.ToTwips(), reportItem.Height.ToTwips()))
			End Function

			Private Shared Function FromTwipsToPixels(twips As Single, dpi As Single) As Integer
				Const TwipsPerInch As Single = 1440.0F
				Return Convert.ToInt32(twips * dpi / TwipsPerInch)
			End Function

			Private Shared _resolution As System.Nullable(Of SizeF)

			Private Shared ReadOnly Property Resolution() As SizeF
				Get
					If Not _resolution.HasValue Then
						Using bitmap As New Bitmap(1, 1)
							_resolution = New SizeF(bitmap.HorizontalResolution, bitmap.VerticalResolution)
						End Using
					End If
					Return _resolution.Value
				End Get
			End Property
		End Class

#End Region

#Region "Default CRI implementation"

		Private _renderItem As IReportItem

		Private _properties As IPropertyBag
		Private _dataScope As IDataScope

		ReadOnly Property CustomData() As Extensibility.Rendering.CustomData Implements ICustomReportItem.CustomData
			Get
				Return TryCast(_dataScope, Extensibility.Rendering.CustomData)
			End Get
		End Property

		ReadOnly Property DataElementValue() As String Implements ICustomReportItem.DataElementValue
			Get
				Return Nothing
			End Get
		End Property

		ReadOnly Property OverflowName() As String Implements IOverflowItem.OverflowName
			Get
				Return (If(_properties.GetValue("OverflowName"), String.Empty)).ToString()
			End Get
		End Property

		ReadOnly Property NewSection() As Boolean Implements ISectionRegion.NewSection
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("NewSection"), False))
			End Get
		End Property

		ReadOnly Property PageBreakAtStart() As Boolean Implements IPageRegion.PageBreakAtStart
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("PageBreakAtStart"), False))
			End Get
		End Property

		ReadOnly Property PageBreakAtEnd() As Boolean Implements IPageRegion.PageBreakAtEnd
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("PageBreakAtEnd"), False))
			End Get
		End Property

		ReadOnly Property DataSetName() As String Implements IDataRegion.DataSetName
			Get
				Return (If(_properties.GetValue("DataSetName"), String.Empty)).ToString()
			End Get
		End Property

		ReadOnly Property NoRowsMessage() As String Implements IDataRegion.NoRowsMessage
			Get
				Return (If(_properties.GetValue("NoRowsMessage"), String.Empty)).ToString()
			End Get
		End Property

		ReadOnly Property NoRows() As Boolean Implements IDataRegion.NoRows
			Get
				Return Convert.ToBoolean(If(_properties.GetValue("NoRows"), False))
			End Get
		End Property

		ReadOnly Property NoRowsTextBox() As ITextBox Implements IDataRegion.NoRowsTextBox
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

		ReadOnly Property DataElementOutput() As Extensibility.Rendering.Components.DataElementOutput Implements IReportItem.DataElementOutput
			Get
				Return CType((If(_properties.GetValue("DataElementOutput"), Extensibility.Rendering.Components.DataElementOutput.NoOutput)), Extensibility.Rendering.Components.DataElementOutput)
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
			Dim definition As Object = TryCast(_properties.GetValue("ReportItemDefinition"), IServiceProvider)
			If definition IsNot Nothing Then
				Return definition.GetService(serviceType)
			End If

			Return Nothing
		End Function

		Public Function GetRenderer(Of TRenderer As {Class, IRenderer})() As TRenderer Implements IReportItemRenderersFactory.GetRenderer
			Return If(GetType(TRenderer) = GetType(IImageRenderer), CType(CType(New ImageRenderer(), IImageRenderer), TRenderer), Nothing)
		End Function

		ReadOnly Property Label() As String Implements IDocumentMapItem.Label
			Get
				Return _properties.GetValue("Label").ToString()
			End Get
		End Property

#End Region
End Class
