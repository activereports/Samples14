Imports GrapeCity.ActiveReports.Design
Imports System.IO
Imports System.Xml
Imports System.Drawing.Design

Public Class ReportsForm
	Private Shared _folderPath As String
	Dim _reportName As String
	Public Shared ReadOnly Property FolderPath() As String
		Get
			Return _folderPath
		End Get
	End Property

	Private Shared _excludeFoldersList As New List(Of String)()
	Public Shared ReadOnly Property ExcludeFoldersList() As List(Of String)
		Get
			Return _excludeFoldersList
		End Get
	End Property

	Private Shared _excludeFilesList As New List(Of String)()
	Public Shared ReadOnly Property ExcludeFilesList() As List(Of String)
		Get
			Return _excludeFilesList
		End Get
	End Property

	'Add nodes to tree view
	Private Sub ListDirectory(ByVal treeView As TreeView, ByVal path As String)
		treeView.Nodes.Clear()
		Dim rootDirectoryInfo = New DirectoryInfo(path)
		For Each directory As DirectoryInfo In rootDirectoryInfo.GetDirectories()
			treeView.Nodes.Add(CreateDirectoryNode(directory))
		Next
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

	' Traverse Samples Folder and create tree
	Private Shared Function CreateDirectoryNode(ByVal directoryInfo As DirectoryInfo) As TreeNode
		Dim directoryNode = New TreeNode(directoryInfo.Name)
		For Each directory As DirectoryInfo In directoryInfo.GetDirectories()
			Dim dirname As String = directory.FullName
			If Not ExcludeFoldersList.Exists(Function(t) t.ToString() = dirname) Then
				directoryNode.Nodes.Add(CreateDirectoryNode(directory))
			End If
		Next
		For Each file As FileInfo In directoryInfo.GetFiles("*.rpx").Concat(directoryInfo.GetFiles("*.rdlx"))
			Dim filename As String = file.FullName
			If Not ExcludeFilesList.Exists(Function(t) t.ToString() = filename) Then
				Dim reportFileNode As New TreeNode(file.Name)
				reportFileNode.ImageIndex = 2
				reportFileNode.SelectedImageIndex = 2
				reportFileNode.ForeColor = Color.Brown
				reportFileNode.Tag = file.FullName
				directoryNode.Nodes.Add(reportFileNode)
			End If
		Next
		Return directoryNode
	End Function
	' Folder Localization
	Private Sub FolderLocalization()

		Dim strReplace As New Hashtable()
		Dim reader As New StreamReader(New FileStream("..\..\ReportsGallery.config", FileMode.Open, FileAccess.Read, FileShare.Read))
		Dim doc As New XmlDocument()
		Dim xmlIn As String = reader.ReadToEnd()
		reader.Close()
		doc.LoadXml(xmlIn)
		For Each child As XmlNode In doc.ChildNodes(1).ChildNodes
			If child.Name.Equals("Localization") Then
				For Each node As XmlNode In child.ChildNodes
					If node.Name.Equals("ReplaceName") Then
						strReplace.Add(node.Attributes("OriginalName").Value, node.Attributes("ReplaceWith").Value)
					End If
				Next
			End If
		Next

		For i As Integer = 0 To treeView.Nodes.Count - 1
			For Each entry As DictionaryEntry In strReplace
				If treeView.Nodes(i).Text.Equals(entry.Key.ToString()) Then
					treeView.Nodes(i).Text = entry.Value.ToString()
				End If
			Next
		Next
	End Sub

	Private Sub ReportsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not String.IsNullOrEmpty(FolderPath) Then
			ListDirectory(treeView, FolderPath)
		End If
		FolderLocalization()
		treeView.Nodes(1).Expand()
		treeView.Nodes(1).Nodes(0).Expand()
		Dim report = New FileInfo(treeView.Nodes(1).Nodes(0).Nodes(0).Tag.ToString)
		reportDesigner.LoadReport(report)
		_reportName = report.Name
		UpdateReportName()
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

	Shared Sub New()
		Dim loaded As XDocument = XDocument.Load("ReportsGallery.config")
		_folderPath = loaded.Descendants("FolderPath").[Select](Function(t) t.Value.ToString()).ToList()(0)
		Dim reportbasefolder As New DirectoryInfo(FolderPath)
		_excludeFilesList = loaded.Descendants("ExcludeFiles").ToList()(0).Descendants("File").[Select](Function(t) reportbasefolder.FullName + "\" & t.Value.ToString()).ToList()
		_excludeFoldersList = loaded.Descendants("ExcludeFolders").ToList()(0).Descendants("Folder").[Select](Function(t) reportbasefolder.FullName + "\" & t.Value.ToString()).ToList()
	End Sub

	Public Sub New()

		InitializeComponent()
		'Add any initialization after the InitializeComponent() 
		'Populate the ToolBox, ReportExplorer and PropertyGrid
		reportDesigner.Toolbox = reportToolbox 'Attach the Toolbox to the report designer
		AddHandler reportDesigner.LayoutChanged, Sub(s, e) OnDesignerLayoutChanged(s, e)
		reportExplorer.ReportDesigner = reportDesigner 'Attach the Report Explorer to the report designer
		reportDesigner.PropertyGrid = reportPropertyGrid 'Attach the Property Grid to the report designer
		layerList.ReportDesigner = reportDesigner
		groupEditor.ReportDesigner = reportDesigner

		'Create a toolstrip to be used as a menu.
		CreateReportExplorer()
		Dim toolstrip As ToolStrip = reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Menu})(0)
		toolstrip.Items.Remove(toolstrip.Items(2))
		Dim filemenu As ToolStripDropDownItem = DirectCast(toolstrip.Items(0), ToolStripDropDownItem)
		CreateFileMenu(filemenu)
		AppendToolStrips(0, New ToolStrip() {toolstrip})
		AppendToolStrips(1, reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Edit, DesignerToolStrips.Undo, DesignerToolStrips.Zoom}))
		AppendToolStrips(2, reportDesigner.CreateToolStrips(New DesignerToolStrips() {DesignerToolStrips.Format, DesignerToolStrips.Layout}))
		SetReportName(Nothing)

		AddHandler reportDesigner.ReportChanged, AddressOf UpdateReportName

		reportDesigner.NewReport(DesignerReportType.Page)
		' load config settings
		InitGroupEditorToggle()
	End Sub

	'Adding DropDownItems to the ToolStripDropDownItem
	Private Sub CreateFileMenu(ByVal fileMenu As ToolStripDropDownItem)
		fileMenu.DropDownItems.Clear()
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.NewText, My.Resources.CmdNewReport, New EventHandler(AddressOf OnNew), (Keys.Control Or Keys.N)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.OpenText, My.Resources.CmdOpen, New EventHandler(AddressOf OnOpen), (Keys.Control Or Keys.O)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.SaveText, My.Resources.CmdSave, New EventHandler(AddressOf OnSave), (Keys.Control Or Keys.S)))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.SaveAsText, My.Resources.CmdSaveAs, New EventHandler(AddressOf OnSaveAs)))
		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.ExitText, Nothing, New EventHandler(AddressOf OnExit)))
	End Sub


	Private Sub OnExit(sender As Object, e As EventArgs)
		Close()
	End Sub

	'Getting the Designer to open a new report on "New" menu item click.
	Private Sub OnNew(sender As Object, e As EventArgs)
		If ConfirmSaveChanges() Then
			Dim reportChangedHandler As New EventHandler(AddressOf UpdateReportName)
			RemoveHandler reportDesigner.ReportChanged, reportChangedHandler
			reportDesigner.ExecuteAction(DesignerAction.NewReport)
			reportDesigner.IsDirty = False
			SetReportName(Nothing)
			AddHandler reportDesigner.ReportChanged, reportChangedHandler
		End If
		ShowHideGroupEditor()
	End Sub

	'Getting the Designer to open a report on "Open" menu item click.
	Private Sub OnOpen(sender As Object, e As EventArgs)
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

	Private Sub ShowHideGroupEditor()
		If reportDesigner.ReportType = DesignerReportType.Section Then
			SplitContainer1.Panel2Collapsed = True
		Else
			SplitContainer1.Panel2Collapsed = False
		End If
	End Sub

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
			Dim result As DialogResult = MessageBox.Show(My.Resources.ReportDirtyMessage, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If result = DialogResult.Cancel Then
				Return False
			End If
			If result = DialogResult.Yes Then

				Return PerformSaveAs()
			End If
		End If
		Return True
	End Function

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

	Private Sub AppendToolStrips(ByVal row As Integer, ByVal toolStrips As IList(Of ToolStrip))
		Dim topToolStripPanel As ToolStripPanel = toolStripContainer.TopToolStripPanel
		Dim num As Integer = toolStrips.Count
		While Threading.Interlocked.Decrement(num) >= 0
			topToolStripPanel.Join(toolStrips(num), row)
		End While
	End Sub

	Private Sub reportDesigner_LayoutChanged(ByVal sender As Object, ByVal e As LayoutChangedArgs)
		CreateReportExplorer()
	End Sub

	Private Sub CreateReportExplorer()
		If reportDesigner.ReportType = DesignerReportType.Section Then
			If explorerPropertyGridContainer.Panel1.Controls.Contains(reportExplorerTabControl) Then
				reportExplorerTabControl.TabPages(0).SuspendLayout()
				explorerPropertyGridContainer.Panel1.SuspendLayout()
				explorerPropertyGridContainer.Panel1.Controls.Remove(reportExplorerTabControl)
				explorerPropertyGridContainer.Panel1.Controls.Add(reportExplorer)
				reportExplorerTabControl.TabPages(0).ResumeLayout()
				explorerPropertyGridContainer.Panel1.ResumeLayout()
			End If
		ElseIf Not explorerPropertyGridContainer.Panel1.Controls.Contains(reportExplorerTabControl) Then
			reportExplorerTabControl.TabPages(0).SuspendLayout()
			explorerPropertyGridContainer.Panel1.SuspendLayout()
			explorerPropertyGridContainer.Panel1.Controls.Remove(reportExplorer)
			reportExplorerTabControl.TabPages(0).Controls.Add(reportExplorer)
			explorerPropertyGridContainer.Panel1.Controls.Add(reportExplorerTabControl)
			reportExplorerTabControl.TabPages(0).ResumeLayout()
			explorerPropertyGridContainer.Panel1.ResumeLayout()
		End If
	End Sub

	Private Sub TreeView_AfterCollapse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView.AfterCollapse
		e.Node.ImageIndex = 0
		e.Node.SelectedImageIndex = 0
	End Sub

	Private Sub TreeView_AfterExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView.AfterExpand
		e.Node.ImageIndex = 1
		e.Node.ImageIndex = 1
		e.Node.SelectedImageIndex = 1
	End Sub

	'Handle click on Tree Node
	Private Sub TreeView_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles treeView.NodeMouseClick
		If (e.Node.Text.ToLower().Contains(".rdlx")) OrElse (e.Node.Text.ToLower().Contains(".rpx")) Then
			e.Node.ImageIndex = 2
			treeView.SelectedNode = e.Node
			Dim reportFile As New FileInfo(e.Node.Tag.ToString())
			_reportName = reportFile.FullName
			reportDesigner.LoadReport(reportFile)
			reportToolbox.PerformLayout()
		Else

			If e.Node.Parent IsNot Nothing Then
				If e.Node.Parent.Parent IsNot Nothing Then
					MessageBox.Show(My.Resources.InvalidFileText)
				End If
			End If
		End If
	End Sub
End Class
