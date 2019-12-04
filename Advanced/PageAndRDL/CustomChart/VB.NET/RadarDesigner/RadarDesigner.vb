Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Drawing
Imports System.ComponentModel.Design
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports System.CodeDom.Compiler
Imports GrapeCity.Enterprise.Data.DataEngine.Expressions
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Designers
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Behavior
Imports Cursor = System.Windows.Forms.Cursor

<Guid("2F06C3C6-A794-4803-A529-B43DC96019B4")>
<ToolboxBitmap(GetType(RadarDesigner), "RadarIcon.png")>
<DisplayName("RadarChart")>
Public NotInheritable Class RadarDesigner
	Inherits CustomReportItemDesigner
	Implements IValidateable

	Private Const SeriesValueName As String = "SeriesValue"
	Private _designerVerbCollection As DesignerVerbCollection
	Public Sub New()
		' add new data set property
		AddProperty(Me, Function(x) x.DataSetName, CategoryAttribute.Data, New DescriptionAttribute(My.Resources.DataSetDescription), New DisplayNameAttribute(My.Resources.DataSetDisplayName), New TypeConverterAttribute(GetType(DataSetNamesConverter)))
		' add new series value property
		AddProperty(Me, Function(x) x.SeriesValue, CategoryAttribute.Data, New DescriptionAttribute(My.Resources.SeriesValueDescription), New DisplayNameAttribute(My.Resources.SeriesValueDisplayName), New TypeConverterAttribute(GetType(RadarValuesConverter)))
	End Sub

	Public Overrides Sub Initialize(component As IComponent)
		MyBase.Initialize(component)
		' create custom data if it's required, e.g. for a new item
		If ReportItem.CustomData Is Nothing Then
			ReportItem.CustomData = New CustomData()
		End If
		' we have to add a data grouping because it's required to edit group expressions and series value in smart panels.
		If ReportItem.CustomData.DataRowGroupings.Count = 0 Then
			ReportItem.CustomData.DataRowGroupings.Add(New DataGrouping())
		End If
	End Sub

	Private Function Validate(context As ValidationContext) As ValidationEntry() Implements IValidateable.Validate
		Return New ValidationEntry(-1) {}
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

	Public Property DataSetName() As String
		Get
			Dim customProperty As Object = ReportItem.CustomProperties("DataSetName")
			If customProperty IsNot Nothing Then
				Dim expValue As Object = customProperty.Value
				If expValue.IsConstant Then
					Return expValue.ToString()
				End If
			End If
			Return String.Empty
		End Get
		Set
			Dim customProperty As Object = ReportItem.CustomProperties("DataSetName")
			If customProperty IsNot Nothing Then
				ReportItem.CustomProperties.Remove(customProperty)
			End If
			customProperty = New CustomPropertyDefinition("DataSetName", Value)
			ReportItem.CustomProperties.Add(customProperty)
		End Set
	End Property

	Public Property SeriesValue() As ExpressionInfo
		Get
			If ReportItem.CustomData.DataRowGroupings.Count > 0 Then
				Dim [property] = ReportItem.CustomData.DataRowGroupings(0).CustomProperties(SeriesValueName)
				If [property] IsNot Nothing Then
					Return [property].Value
				End If
			End If
			Return ExpressionInfo.FromString(String.Empty)
		End Get
		Set
			' find series value property
			Dim grouping As Object = ReportItem.CustomData.DataRowGroupings(0)
			Dim [property] = grouping.CustomProperties(SeriesValueName)
			If Value Is Nothing OrElse ExpressionInfo.FromString(String.Empty) = Value Then
				' default value: empty expression
				' if value is empty expression then we should reset the property.
				If [property] IsNot Nothing Then
					ReportItem.CustomData.DataRowGroupings(0).CustomProperties.Remove([property])
				End If
			Else
				' otherwise we should set the custom property
				' if it's required then the property must be created
				If [property] Is Nothing Then
					[property] = New CustomPropertyDefinition(SeriesValueName, Value)
					grouping.CustomProperties.Add([property])
				End If
				' set the value to the property
				[property].Value = Value
			End If
		End Set
	End Property

	Private Shared Function GetServiceFromTypeDescriptorContext(serviceType As Type, context As ITypeDescriptorContext) As Object
		Dim service As Object = context.GetService(serviceType)
		Dim host As IDesignerHost = Nothing
		If service Is Nothing AndAlso (InlineAssignHelper(host, TryCast(context.Container, IDesignerHost))) IsNot Nothing Then
			service = host.GetService(serviceType)
		End If
		Dim component As Object = TryCast(context.Instance, IComponent)
		If service Is Nothing AndAlso component IsNot Nothing AndAlso component.Site IsNot Nothing Then
			service = component.Site.GetService(serviceType)
		End If
		Return service
	End Function

	Private Shared Function GetReportFromServiceProvider(serviceProvider As IServiceProvider) As Report
		If serviceProvider Is Nothing Then
			Return Nothing
		End If
		Dim host As Object = If(TryCast(serviceProvider, IDesignerHost), TryCast(serviceProvider.GetService(GetType(IDesignerHost)), IDesignerHost))
		If host Is Nothing Then
			Return Nothing
		End If
		Dim reportDef As Object = TryCast(host.RootComponent, PageReport)
		Return If(reportDef = Nothing, Nothing, reportDef.Report)
	End Function

	Public Class RadarValuesConverter
		Inherits StringConverter
		Public Overrides Function GetStandardValuesSupported(context As ITypeDescriptorContext) As Boolean
			Return True
		End Function

		Public Overrides Function GetStandardValues(context As ITypeDescriptorContext) As StandardValuesCollection
			Dim stringCollection As Object = New StringCollection()
			If context Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim host As Object = TryCast(GetServiceFromTypeDescriptorContext(GetType(IDesignerHost), context), IDesignerHost)
			If host Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim report As Object = GetReportFromServiceProvider(host)
			If report Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim selectionService As Object = TryCast(GetServiceFromTypeDescriptorContext(GetType(ISelectionService), context), ISelectionService)
			If selectionService Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim component As Object = TryCast(selectionService.PrimarySelection, IReportComponent)
			If component Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim radar As Object = TryCast(component, CustomReportItem)
			If radar Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim dataSetName As Object = radar.CustomProperties("DataSetName")
			If dataSetName Is Nothing OrElse String.IsNullOrEmpty(dataSetName.Value) Then
				Return New StandardValuesCollection(stringCollection)
			End If
			For Each dataSet As DataSet In report.DataSets
				If dataSet.Name = dataSetName.Value Then
					For Each field As Field In dataSet.Fields
						stringCollection.Add(ExpressionInfo.FromString(String.Format(If(CodeGenerator.IsValidLanguageIndependentIdentifier(field.Name), "=Fields!{0}.Value", "=Fields.Item(""{0}"").Value"), field.Name)))
					Next
				End If
			Next
			Return New StandardValuesCollection(stringCollection)
		End Function

		Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
			If sourceType = GetType(String) Then
				Return True
			End If
			Return MyBase.CanConvertFrom(context, sourceType)
		End Function

		Public Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As CultureInfo, value As Object) As Object
			If TypeOf value Is String Then
				Return ExpressionInfo.FromString(TryCast(value, String))
			End If
			Return MyBase.ConvertFrom(context, culture, value)
		End Function
	End Class

	Private NotInheritable Class DataSetNamesConverter
		Inherits StringConverter
		Public Overrides Function GetStandardValuesSupported(context As ITypeDescriptorContext) As Boolean
			Return True
		End Function

		Public Overrides Function GetStandardValues(context As ITypeDescriptorContext) As StandardValuesCollection
			Dim stringCollection As Object = New StringCollection()
			If context Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim host As Object = TryCast(GetServiceFromTypeDescriptorContext(GetType(IDesignerHost), context), IDesignerHost)
			If host Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			Dim report As Object = GetReportFromServiceProvider(host)
			If report Is Nothing Then
				Return New StandardValuesCollection(stringCollection)
			End If
			For Each dataSet As DataSet In report.DataSets
				stringCollection.Add(dataSet.Name)
			Next
			Return New StandardValuesCollection(stringCollection)
		End Function
	End Class

	Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
		target = value
		Return value
	End Function

#End Region

#Region "Design-time rendering"

	Private _controlGlyph As RadarControlGlyph

	Public Overrides ReadOnly Property ControlGlyph() As Glyph
		Get
			Return If(_controlGlyph, (InlineAssignHelper(_controlGlyph, New RadarControlGlyph(ReportItem, Me))))
		End Get
	End Property

	Private NotInheritable Class RadarControlGlyph
		Inherits ControlBodyGlyph
		Public Sub New(reportItem As ReportItem, designer As RadarDesigner)
			MyBase.New(designer.BehaviorService, designer)
			Behavior = New MovableBehavior(reportItem, Me)
		End Sub

		Public Overrides Sub Paint(pe As PaintEventArgs)
			If PaintSelectionOnly(pe) Then
				MyBase.Paint(pe)
				Return
			End If

			pe.Graphics.FillRectangle(BackgroundBrush, Bounds)
			Using nonScaledImage As Object = New Bitmap(Bounds.Width, Bounds.Height)
				nonScaledImage.SetResolution(pe.Graphics.DpiX, pe.Graphics.DpiY)
				Using chart = New System.Windows.Forms.DataVisualization.Charting.Chart() With {
					.Dock = DockStyle.Fill,
					.Size = Bounds.Size
				}
					Dim chartArea As Object = New ChartArea("Main")
					chart.ChartAreas.Add(chartArea)

					' Create series and add it to the chart
					Dim seriesColumns As Object = New Series("RandomColumns")
					' Add 10 random values to the series
					Dim random As Object = New Random(0)
					Dim i As Integer = 0
					While i < 10
						seriesColumns.Points.Add(random.[Next](100))
						i += 1
					End While
					seriesColumns.ChartType = SeriesChartType.Radar

					chart.Series.Add(seriesColumns)
					chart.DrawToBitmap(nonScaledImage, New Rectangle(Point.Empty, Bounds.Size))
				End Using
				pe.Graphics.DrawImageUnscaled(nonScaledImage, Bounds.Location)
			End Using
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
				Dim host As Object = TryCast(item.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
				If host Is Nothing Then
					Return True
				End If
				Dim reportDef As Object = TryCast(host.RootComponent, PageReport)
				If reportDef Is Nothing Then
					Return True
				End If
				Dim reportDesigner As Object = TryCast(host.GetDesigner(reportDef.Report), ReportDesigner)
				If reportDesigner Is Nothing Then
					Return True
				End If
				Dim activeLayer As Object = reportDesigner.ActiveLayer
				Return String.Equals(activeLayer, item.LayerName, StringComparison.InvariantCultureIgnoreCase)
			End Function
		End Class
	End Class


#End Region

End Class
