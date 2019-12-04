Imports GrapeCity.ActiveReports.Design
Imports System.IO

Public Class DesignerForm
	Dim _reportName As String = "DemoReport.rdlx"
	Private Sub UnifiedDesignerForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		reportDesigner.LoadReport(New FileInfo(_reportName))
	End Sub

	Public Sub New()
		InitializeComponent()
		reportDesigner.NewReport(DesignerReportType.Page) 'Sets the designer to create a blank page based report
		'Populating the ToolBox, ReportExplorer and PropertyGrid.
		reportDesigner.Toolbox = reportToolbox 'Attaches the toolbox to the report designer
		AddHandler reportDesigner.LayoutChanged, Sub(s, e) OnDesignerLayoutChanged(s, e)
		reportExplorer.ReportDesigner = reportDesigner 'Attaches the report explorer to the report designer
		groupEditor.ReportDesigner = reportDesigner
		reportsLibrary.ReportDesigner = reportDesigner
		layerList.ReportDesigner = reportDesigner
		reportDesigner.PropertyGrid = propertyGrid 'Attaches the Property Grid to the report designer
		'Populating the menu.
		Dim toolStrip As ToolStrip = reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Menu})(0)
		toolStrip.Items.RemoveAt(2)
		Dim fileMenu As ToolStripDropDownItem = DirectCast(toolStrip.Items(0), ToolStripDropDownItem)
		CreateFileMenu(fileMenu)
		AppendToolStrips(0, New ToolStrip() {toolStrip})
		AppendToolStrips(1, reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Edit, DesignerToolStrips.Undo, DesignerToolStrips.Zoom}))
		AppendToolStrips(1, New ToolStrip() {CreateReportToolbar()})
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

	Private Sub UpdateReportName()
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
														  _groupEditorSize = SplitContainer1.ClientSize.Height - SplitContainer1.SplitterDistance
														  SplitContainer1.SplitterDistance = SplitContainer1.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - SplitContainer1.Panel2.Padding.Vertical - SplitContainer1.SplitterWidth
														  SplitContainer1.IsSplitterFixed = True
														  groupEditor.Visible = False
													  Else
														  GroupEditorToggleButton.Image = My.Resources.GroupEditorHide
														  SplitContainer1.SplitterDistance = CInt(If(_groupEditorSize < SplitContainer1.ClientSize.Height, SplitContainer1.ClientSize.Height - _groupEditorSize, SplitContainer1.ClientSize.Height * 2 / 3))
														  SplitContainer1.IsSplitterFixed = False
														  groupEditor.Visible = True
													  End If

												  End Sub

		AddHandler groupEditor.VisibleChanged, Sub() GroupPanelVisibility.SetToolTip(GroupEditorToggleButton, If(groupEditor.Visible, My.Resources.HideGroupPanelToolTip, My.Resources.ShowGroupPanelToolTip))
	End Sub

	'Adding DropDownItems to the ToolStripDropDownItem
	Private Sub CreateFileMenu(ByVal fileMenu As ToolStripDropDownItem)
		fileMenu.DropDownItems.Clear()
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuNew, My.Resources.CmdNewReport, New EventHandler(AddressOf OnNew), (Keys.Control Or Keys.N)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuOpen, My.Resources.CmdOpen, New EventHandler(AddressOf OnOpen), (Keys.Control Or Keys.O)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuSave, My.Resources.CmdSave, New EventHandler(AddressOf OnSave), (Keys.Control Or Keys.S)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuSaveAs, My.Resources.CmdSaveAs, New EventHandler(AddressOf OnSaveAs)))
		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.MenuExit, Nothing, New EventHandler(AddressOf OnExit)))
	End Sub

	'Getting the Designer to open a new report on "New" menu item click.
	Private Sub OnNew(ByVal sender As Object, ByVal e As EventArgs)
		If ConfirmSaveChanges() Then
			reportDesigner.IsDirty = False
			RemoveHandler reportDesigner.ReportChanged, AddressOf UpdateReportName
			reportDesigner.ExecuteAction(DesignerAction.NewReport)
			SetReportName(Nothing)
			AddHandler reportDesigner.ReportChanged, AddressOf UpdateReportName
			ShowHideGroupEditor()
		End If
	End Sub

	'Getting the Designer to open a report on "Open" menu item click.
	Private Sub OnOpen(ByVal sender As Object, ByVal e As EventArgs)
		If Not ConfirmSaveChanges() Then
			Return
		End If

		Using openDialog = New OpenFileDialog()
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

	'Getting the Designer to open a report on "Save" menu item click.
	Private Sub OnSave(ByVal sender As Object, ByVal e As EventArgs)
		If String.IsNullOrEmpty(_reportName) OrElse String.IsNullOrEmpty(Path.GetDirectoryName(_reportName)) OrElse Not File.Exists(_reportName) Then
			If PerformSaveAs() Then
				reportDesigner.SaveReport(New FileInfo(_reportName))
			End If
		Else
			reportDesigner.SaveReport(New FileInfo(_reportName))
		End If
		SetReportName(_reportName)
	End Sub

	Private Sub ShowHideGroupEditor()
		If reportDesigner.ReportType = DesignerReportType.Section Then
			SplitContainer1.Panel2Collapsed = True
		Else
			SplitContainer1.Panel2Collapsed = False
		End If
	End Sub

	'Getting the Designer to open a report on "Save As" menu item click.
	Private Sub OnSaveAs(ByVal sender As Object, ByVal e As EventArgs)
		PerformSaveAs()
	End Sub

	Private Function PerformSaveAs() As Boolean
		Using saveDialog = New SaveFileDialog()
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

	Private Sub OnExit(ByVal sender As Object, ByVal e As EventArgs)
		Close()
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

	Private Sub AppendToolStrips(ByVal row As Integer, ByVal toolStrips As IList(Of ToolStrip))
		Dim topToolStripPanel As ToolStripPanel = ToolStripContainer.TopToolStripPanel
		Dim num As Integer = toolStrips.Count
		While Threading.Interlocked.Decrement(num) >= 0
			topToolStripPanel.Join(toolStrips(num), row)
		End While
	End Sub

	Private Function CreateReportToolbar() As ToolStrip
		Return New ToolStrip(New ToolStripButton() {
							 CreateToolStripButton(My.Resources.MenuNew, My.Resources.CmdNewReport, New EventHandler(AddressOf OnNew), My.Resources.MenuNew),
							 CreateToolStripButton(My.Resources.MenuOpen, My.Resources.CmdOpen, New EventHandler(AddressOf OnOpen), My.Resources.MenuOpen),
							 CreateToolStripButton(My.Resources.MenuSave, My.Resources.CmdSave, New EventHandler(AddressOf OnSave), My.Resources.MenuSave)
							 })
	End Function

	Private Shared Function CreateToolStripButton(ByVal text As String, ByVal image As Image, ByVal handler As EventHandler, ByVal toolTip As String) As ToolStripButton
		Return New ToolStripButton(text, image, handler) With { _
		 .DisplayStyle = ToolStripItemDisplayStyle.Image, _
			.ToolTipText = toolTip, _
		 .DoubleClickEnabled = True _
		}
	End Function

	Private Sub CreateReportExplorer()
		If reportDesigner.ReportType = DesignerReportType.Section Then

			If explorerpropertygridContainer.Panel1.Controls.Contains(reportExplorerTabControl) Then
				reportExplorerTabControl.TabPages(0).SuspendLayout()
				explorerpropertygridContainer.Panel1.SuspendLayout()
				explorerpropertygridContainer.Panel1.Controls.Remove(reportExplorerTabControl)
				explorerpropertygridContainer.Panel1.Controls.Add(reportExplorer)
				reportExplorerTabControl.TabPages(0).ResumeLayout()
				explorerpropertygridContainer.Panel1.ResumeLayout()
			End If
		ElseIf (Not explorerpropertygridContainer.Panel1.Controls.Contains(reportExplorerTabControl)) Then
			reportExplorerTabControl.TabPages(0).SuspendLayout()
			explorerpropertygridContainer.Panel1.SuspendLayout()
			explorerpropertygridContainer.Panel1.Controls.Remove(reportExplorer)
			reportExplorerTabControl.TabPages(0).Controls.Add(reportExplorer)
			explorerpropertygridContainer.Panel1.Controls.Add(reportExplorerTabControl)
			reportExplorerTabControl.TabPages(0).ResumeLayout()
			explorerpropertygridContainer.Panel1.ResumeLayout()
		End If
	End Sub

End Class
