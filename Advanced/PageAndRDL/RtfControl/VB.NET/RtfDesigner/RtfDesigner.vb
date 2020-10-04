Imports System.Drawing
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Runtime.InteropServices

Imports GrapeCity.ActiveReports.Rendering
Imports GrapeCity.ActiveReports.Drawing.Gdi
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Behavior
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Designers
Imports GrapeCity.ActiveReports.Design.DdrDesigner.Tools
Imports GrapeCity.ActiveReports.Samples.Rtf.Rendering
Imports GrapeCity.Enterprise.Data.DataEngine.Expressions

<Guid("40453B74-5FBB-4AE4-86D0-B6C881FC6D0F")>
<ToolboxBitmap(GetType(RtfDesigner), "RtfIcon.bmp")>
<DisplayName("RichTextBox")>
Public NotInheritable Class RtfDesigner
	Inherits CustomReportItemDesigner
	Implements IValidateable

	Private Const RTF_FIELD_NAME As String = "Rtf"
	Private Const CAN_GROW_FIELD_NAME As String = "CanGrow"
	Private Const CAN_SHRINK_FIELD_NAME As String = "CanShrink"
	
	
	Private _designerVerbCollection As DesignerVerbCollection
	Private _editor As RtfEditor

	Private ReadOnly Property IsActive As Boolean
		Get
			Return _editor.IsActive
		End Get
	End Property

	Public Sub New()
		AddProperty(Me, Function(x) x.Rtf,
					CategoryAttribute.Data,
					New DisplayNameAttribute(My.Resources.PropertyRtf),
					New DescriptionAttribute(My.Resources.PropertyRtfDescription),
					New EditorAttribute(GetType(ExpressionInfoUITypeEditor), GetType(UITypeEditor)))

		AddProperty(Me, Function(x) x.CanGrow,
					CategoryAttribute.Layout,
					New DisplayNameAttribute(My.Resources.PropertyCanGrow),
					New DescriptionAttribute(My.Resources.PropertyCanGrowDescription))

		AddProperty(Me, Function(x) x.CanShrink,
					CategoryAttribute.Layout,
					New DisplayNameAttribute(My.Resources.PropertyCanShrink),
					New DescriptionAttribute(My.Resources.PropertyCanShrinkDescription))

		_editor = New RtfEditor(Me) With {
			.Name = "RTBFEditor",
			.Multiline = True,
			.AllowDrop = False,
			.BorderStyle = Windows.Forms.BorderStyle.FixedSingle,
			.ScrollBars = RichTextBoxScrollBars.Both
		}
	End Sub

	Public Overloads ReadOnly Property ReportItem As CustomReportItem
		Get
			Return MyBase.ReportItem
		End Get
	End Property

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing Then
			If _metafile IsNot Nothing Then _metafile.Dispose()
			_metafile = Nothing
		End If

		MyBase.Dispose(disposing)
	End Sub

	Private Function Validate(context As ValidationContext) As ValidationEntry() Implements IValidateable.Validate
		Return New ValidationEntry(0) {}
	End Function

	Public Overrides ReadOnly Property Verbs As DesignerVerbCollection
		Get
			If _designerVerbCollection Is Nothing Then
				_designerVerbCollection = New DesignerVerbCollection()
			End If

			Return _designerVerbCollection
		End Get
	End Property

	Public Overrides Sub Initialize(component As IComponent)
		MyBase.Initialize(component)
		
		InitializeShadowProperties()
		InitializeCustomProperties()

		SetRtf(ReportItem.GetCustomPropertyAsString(RTF_FIELD_NAME))
	End Sub

	Public Property Rtf As ExpressionInfo = ExpressionInfo.EmptyString

	Public Function GetRtf() As String
		Dim prop = TypeDescriptor.GetProperties(Component)(RTF_FIELD_NAME)
		Dim rtf As ExpressionInfo = prop.GetValue(Component)
		
		Return If (rtf.IsEmptyString, string.Empty, rtf.ToString())
	End Function

	Public Sub SetRtf(rtf As String)
		_metafile = Nothing
		
		Dim prop = TypeDescriptor.GetProperties(Component)(RTF_FIELD_NAME)
		Dim result = If (String.Equals(rtf, ExpressionInfo.EmptyString.ToString()), String.Empty, rtf)
		
		prop.SetValue(Component, ExpressionInfo.FromString(result))
		ReportItem.CustomProperties(RTF_FIELD_NAME).Value = result
	End Sub
	
	<DefaultValue(False)>
	Public Property CanGrow As Boolean
		Get
			Return ReportItem.GetCustomPropertyAsBoolean(CAN_GROW_FIELD_NAME, False)
		End Get
		Set
			ReportItem.SetCustomProperty(CAN_GROW_FIELD_NAME, value.ToString())
		End Set
	End Property

	<DefaultValue(False)>
	Public Property CanShrink As Boolean
		Get
			Return ReportItem.GetCustomPropertyAsBoolean(CAN_SHRINK_FIELD_NAME, False)
		End Get
		Set
			ReportItem.SetCustomProperty(CAN_SHRINK_FIELD_NAME, value.ToString())
		End Set
	End Property

	Protected Overrides Sub OnComponentChanged(sender As Object, e As ComponentChangedEventArgs)
		MyBase.OnComponentChanged(sender, e)
		Dim memberNotNull = e IsNot Nothing AndAlso e.Member IsNot Nothing
		If UndoService.UndoInProgress OrElse memberNotNull AndAlso e.Member.Name = "ComponentSize" Then _metafile = Nothing
		If memberNotNull AndAlso e.Member.Name = "Rtf" Then SetRtf(Rtf)
	End Sub

	Private _metafile As System.Drawing.Image
	Private _controlGlyph As RtfControlGlyph

	Friend ReadOnly Property RenderedRtf As System.Drawing.Image
		Get
			If Rtf.IsEmptyString Then Return Nothing

			If _metafile IsNot Nothing Then Return _metafile

			_metafile = RenderMetafile(Rtf, New SizeF(ReportItem.Width.ToInches(), ReportItem.Height.ToInches()))
			Return _metafile
		End Get
	End Property

	Public Overrides ReadOnly Property ControlGlyph As Glyph
		Get
			If _controlGlyph Is Nothing Then
				_controlGlyph = New RtfControlGlyph(ReportItem, Me)
			End If

			Return _controlGlyph
		End Get
	End Property

	Public Sub RePaint()
		_metafile = Nothing
	End Sub
	
	Private Sub InitializeCustomProperties()
		InitializeCustomProperty(RTF_FIELD_NAME)
		InitializeCustomProperty(CAN_GROW_FIELD_NAME, Boolean.FalseString)
		InitializeCustomProperty(CAN_SHRINK_FIELD_NAME, Boolean.FalseString)
	End Sub

	Private Sub InitializeCustomProperty(propertyName As String, ByVal Optional defaultValue As String = "")
		Dim customProp = ReportItem.CustomProperties(propertyName)
		
		If customProp IsNot Nothing Then Return
		
		customProp = New CustomPropertyDefinition(propertyName, defaultValue)
		ReportItem.CustomProperties.Add(customProp)
	End Sub

	Private NotInheritable Class RtfControlGlyph
		Inherits ControlBodyGlyph

		Private ReadOnly _reportItem As ReportItem

		Public Sub New(reportItem As ReportItem, designer As RtfDesigner)
			MyBase.New(designer.BehaviorService, designer)
			_reportItem = reportItem
			Behavior = New RtfBehavior(reportItem, Me, designer)
		End Sub

		Public Overrides Sub Paint(pe As PaintEventArgs)
			If PaintSelectionOnly(pe) Then
				MyBase.Paint(pe)
				Return
			End If

			pe.Graphics.FillRectangle(BackgroundBrush, Bounds)

			Try
				Dim designer = CType(ComponentDesigner, RtfDesigner)
				Dim meta = designer.RenderedRtf

				If meta IsNot Nothing Then
					pe.Graphics.IntersectClip(Bounds)
					pe.Graphics.DrawImage(meta, New RectangleF(Bounds.X, Bounds.Y, _reportItem.Width.ToInches() * SafeGraphics.HorizontalResolution * designer.ConversionService.ScalingFactor, _reportItem.Height.ToInches() * SafeGraphics.VerticalResolution * designer.ConversionService.ScalingFactor))
				End If

			Catch ex As Exception
				pe.Graphics.DrawString(ex.Message, SystemFonts.DefaultFont, SystemBrushes.ControlText, Bounds, StringFormat.GenericTypographic)
			End Try

			MyBase.Paint(pe)
		End Sub

		Public Overrides Function ContainsFocusedEditor(point As Point) As Boolean
			Dim designer = TryCast(ComponentDesigner, RtfDesigner)
			Return designer.Controls.Count = 1 AndAlso designer.Controls(0).Focused AndAlso designer.ControlGlyph.Bounds.Contains(point)
		End Function

		Private NotInheritable Class RtfBehavior
			Inherits DefaultControlBehavior

			Private ReadOnly _reportItem As ReportItem
			Private ReadOnly _glyph As Glyph
			Private ReadOnly _designer As RtfDesigner
			Private Shared ReadOnly _unTrackedKeys As ICollection(Of Keys) = {Keys.Escape, Keys.Delete, Keys.ControlKey, Keys.ShiftKey, Keys.Apps, Keys.Back, Keys.Insert, Keys.LWin, Keys.RWin, Keys.PageDown, Keys.PageUp, Keys.Home, Keys.[End], Keys.Left, Keys.Up, Keys.Right, Keys.Down}

			Public Sub New(reportItem As ReportItem, glyph As Glyph, designer As RtfDesigner)
				_reportItem = reportItem
				_glyph = glyph
				_designer = designer
			End Sub

			Public Overrides ReadOnly Property Cursor As Cursor
				Get
					Return If(CanMoveGlyph(_glyph) AndAlso IsActiveLayerItem(_reportItem), Cursors.SizeAll, If(_designer.IsActive, _designer._editor.Cursor, MyBase.Cursor))
				End Get
			End Property

			Public Overrides Function OnMouseDoubleClick(g As Glyph, button As MouseButtons, location As Point) As Boolean
				If button = MouseButtons.Left AndAlso g.Bounds.Contains(location) AndAlso _designer.Focused Then _designer._editor.Activate()
				Return MyBase.OnMouseDoubleClick(g, button, location)
			End Function

			Public Overrides Sub OnKeyDown(g As Glyph, e As KeyEventArgs)
				MyBase.OnKeyDown(g, e)
				Dim editor = _designer._editor
				If editor.IsActive OrElse _unTrackedKeys.Contains(e.KeyCode) Then Return
				editor.Activate()
				e.Handled = True
			End Sub

			Protected Overrides Function DisableSizeMoveCapabilities(ByVal glyph As Glyph, ByVal designer As ReportComponentDesigner) As Boolean
				Return _designer.IsActive
			End Function

			Private Shared Function IsActiveLayerItem(item As ReportItem) As Boolean
				If item Is Nothing OrElse item.Site Is Nothing Then Return True

				Dim host = TryCast(item.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
				If host Is Nothing Then Return True

				Dim reportDef = TryCast(host.RootComponent, PageReport)
				If reportDef Is Nothing Then Return True

				Dim reportDesigner = TryCast(host.GetDesigner(reportDef.Report), ReportDesigner)
				If reportDesigner Is Nothing Then Return True

				Dim activeLayer = reportDesigner.ActiveLayer
				Return String.Equals(activeLayer, item.LayerName, StringComparison.InvariantCultureIgnoreCase)
			End Function
		End Class
	End Class
End Class
