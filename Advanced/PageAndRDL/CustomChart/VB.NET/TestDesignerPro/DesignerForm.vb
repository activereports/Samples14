Imports System.IO
Imports GrapeCity.ActiveReports.Design

Public Class DesignerForm
	Dim _reportName As String = "..\..\..\..\Report\Radar.rdlx"
	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		'Populating the ToolBox, ReportExplorer and PropertyGrid.
		reportDesigner.Toolbox = reportToolbox 'Attaches the toolbox to the report designer.
		AddHandler reportDesigner.LayoutChanged, Sub(s, e) OnDesignerLayoutChanged(s, e)
		reportExplorer.ReportDesigner = reportDesigner 'Attaches the report explorer to the report designer.
		reportDesigner.PropertyGrid = propertyGrid 'Attaches the Property Grid to the report designer.
		groupEditor.ReportDesigner = reportDesigner
		Dim toolStrip As ToolStrip = reportDesigner.CreateToolStrips(DesignerToolStrips.Menu)(0)
		toolStrip.Items.RemoveAt(2)
		Dim fileMenu As ToolStripDropDownItem = CType(toolStrip.Items(0), ToolStripDropDownItem)
		CreateFileMenu(fileMenu)
		AppendToolStrips(0, New ToolStrip() {toolStrip})
		AppendToolStrips(1, reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Edit, DesignerToolStrips.Undo, DesignerToolStrips.Zoom}))
		AppendToolStrips(2, reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Format, DesignerToolStrips.Layout}))
		AddHandler reportDesigner.ReportChanged, AddressOf UpdateReportName
		InitGroupEditorToggle()
	End Sub

	Private Sub SetReportName(reportName As String)
		If String.IsNullOrEmpty(reportName) Then
			_reportName = If(TypeOf reportDesigner.Report Is PageReport, My.Resources.DefaultReportNameRdlx, My.Resources.DefaultReportNameRpx)
		Else
			_reportName = reportName
		End If
		Text = My.Resources.SampleNameTitle + Path.GetFileName(_reportName) + (If(reportDesigner.IsDirty, My.Resources.DirtySign, String.Empty))
	End Sub

	Private Sub UpdateReportName(sender As Object, e As EventArgs)
		SetReportName(_reportName)
	End Sub

	Private _groupEditorSize As Integer
	Private Sub InitGroupEditorToggle()
		GroupEditorToggleButton.Image = My.Resources.GroupEditorHide
		AddHandler GroupEditorToggleButton.MouseEnter, Sub(sender, args)
														   GroupEditorToggleButton.BackColor = Color.LightGray
													   End Sub
		AddHandler GroupEditorToggleButton.MouseLeave, Sub(sender, args)
														   GroupEditorToggleButton.BackColor = Color.Gainsboro
													   End Sub
		AddHandler GroupEditorToggleButton.Click, Sub(sender, args)
													  If groupEditor.Visible Then
														  GroupEditorToggleButton.Image = My.Resources.GroupEditorShow
														  _groupEditorSize = splitContainer1.ClientSize.Height - splitContainer1.SplitterDistance
														  splitContainer1.SplitterDistance = splitContainer1.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - splitContainer1.Panel2.Padding.Vertical - splitContainer1.SplitterWidth
														  splitContainer1.IsSplitterFixed = True
														  groupEditor.Visible = False
													  Else
														  GroupEditorToggleButton.Image = My.Resources.GroupEditorHide
														  splitContainer1.SplitterDistance = If(_groupEditorSize < splitContainer1.ClientSize.Height, splitContainer1.ClientSize.Height - _groupEditorSize, splitContainer1.ClientSize.Height * 2 / 3)
														  splitContainer1.IsSplitterFixed = False
														  groupEditor.Visible = True
													  End If
												  End Sub

		AddHandler groupEditor.VisibleChanged, Sub() GroupPanelVisibility.SetToolTip(GroupEditorToggleButton, If(groupEditor.Visible, My.Resources.HideGroupPanelToolTip, My.Resources.ShowGroupPanelToolTip))
	End Sub

	Private Sub CreateFileMenu(fileMenu As ToolStripDropDownItem)
		fileMenu.DropDownItems.Clear()
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuNew, My.Resources.cmdnewreport, AddressOf OnNew, (Keys.Control Or Keys.N)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuOpen, My.Resources.cmdopen, AddressOf OnOpen, (Keys.Control Or Keys.O)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuSave, My.Resources.cmdsave, AddressOf OnSave, (Keys.Control Or Keys.S)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuSaveAs, My.Resources.cmdsaveas, AddressOf OnSaveAs))
		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuExit, Nothing, AddressOf OnExit))
	End Sub

	Private Sub OnExit(sender As Object, e As EventArgs)
		Close()
	End Sub

	'Getting the Designer to open a new report on "New" menu item click.
	Private Sub OnNew(sender As Object, e As EventArgs)
		If ConfirmSaveChanges() Then
			reportDesigner.IsDirty = False
			RemoveHandler reportDesigner.ReportChanged, AddressOf UpdateReportName
			reportDesigner.ExecuteAction(DesignerAction.NewReport)
			SetReportName(Nothing)
			AddHandler reportDesigner.ReportChanged, AddressOf UpdateReportName
		End If
		ShowHideGroupEditor()
	End Sub

	'Getting the Designer to open a report on "Open" menu item click.
	Private Sub OnOpen(sender As Object, e As EventArgs)
		If Not ConfirmSaveChanges() Then
			Return
		End If

		Using openDialog As Object = New OpenFileDialog()
			openDialog.FileName = String.Empty
			openDialog.Filter = My.Resources.RdlxFilter
			If openDialog.ShowDialog(Me) = DialogResult.OK Then
				_reportName = openDialog.FileName
				reportDesigner.LoadReport(New FileInfo(_reportName))
			End If
		End Using
		ShowHideGroupEditor()
	End Sub

	Private Sub OnDesignerLayoutChanged(sender As Object, e As LayoutChangedArgs)
		If e.Type = LayoutChangeType.ReportLoad OrElse e.Type = LayoutChangeType.ReportClear Then
			reportToolbox.Reorder(reportDesigner)
			reportToolbox.EnsureCategories()
			reportToolbox.Refresh()
		End If
	End Sub

	Private Sub ShowHideGroupEditor()
		If reportDesigner.ReportType = DesignerReportType.Section Then
			splitContainer1.Panel2Collapsed = True
		Else
			splitContainer1.Panel2Collapsed = False
		End If
	End Sub

	'Getting the Designer to open a report on "Save" menu item click.
	Private Sub OnSave(sender As Object, e As EventArgs)
		If String.IsNullOrEmpty(_reportName) OrElse String.IsNullOrEmpty(Path.GetDirectoryName(_reportName)) OrElse Not File.Exists(_reportName) Then
			If PerformSaveAs() Then
				reportDesigner.SaveReport(New FileInfo(_reportName))
			End If
		Else
			reportDesigner.SaveReport(New FileInfo(_reportName))
		End If
		SetReportName(_reportName)
	End Sub

	'Getting the Designer to open a report on "Save As" menu item click.
	Private Sub OnSaveAs(sender As Object, e As EventArgs)
		PerformSaveAs()
	End Sub

	'Checking whether modifications have been made to the report loaded to the designer
	Private Function ConfirmSaveChanges() As Boolean
		If reportDesigner.IsDirty Then
			Dim result As DialogResult = MessageBox.Show(My.Resources.SaveConformation, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If result = DialogResult.Cancel Then
				Return False
			End If
			If result = DialogResult.Yes Then

				Return PerformSaveAs()
			End If
		End If
		Return True
	End Function

	Private Function GetSaveFilter() As String
		Select Case reportDesigner.ReportType
			Case DesignerReportType.Section
				Return My.Resources.RpxFilter
			Case DesignerReportType.Page, DesignerReportType.Rdl
				Return My.Resources.RdlxFilter
			Case Else
				Return My.Resources.RpxFilter
		End Select
	End Function

	Private Function PerformSaveAs() As Boolean
		Using saveDialog As Object = New SaveFileDialog()
			saveDialog.Filter = GetSaveFilter()
			saveDialog.FileName = Path.GetFileName(_reportName)
			saveDialog.DefaultExt = ".rdlx"
			saveDialog.InitialDirectory = New DirectoryInfo(Application.ExecutablePath).Parent.Parent.Parent.FullName
			If saveDialog.ShowDialog() = DialogResult.OK Then
				_reportName = saveDialog.FileName
				reportDesigner.SaveReport(New FileInfo(_reportName))
				reportDesigner.IsDirty = False
				Return True
			End If
		End Using

		Return False
	End Function

	Private Sub AppendToolStrips(row As Integer, toolStrips As IList(Of ToolStrip))
		Dim topToolStripPanel As ToolStripPanel = toolStripContainer.TopToolStripPanel
		Dim num As Integer = toolStrips.Count
		num -= 1
		While num >= 0
			topToolStripPanel.Join(toolStrips(num), row)
			num -= 1
		End While
	End Sub

	Protected Overrides Sub OnLoad(e As EventArgs)
		MyBase.OnLoad(e)
		reportDesigner.LoadReport(New FileInfo(_reportName))
	End Sub
End Class
