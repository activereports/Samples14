Imports GrapeCity.ActiveReports.Design
Imports GrapeCity.ActiveReports.Design.Resources
Imports System.IO
Imports System.Drawing.Design
Imports GrapeCity.ActiveReports.PageReportModel
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports GrapeCity.ActiveReports.Viewer.Win.Internal.Export
Imports GrapeCity.ActiveReports.Configuration
Imports GrapeCity.ActiveReports.ReportsCore.Configuration
Imports System.Reflection
Imports GrapeCity.ActiveReports.Design.ReportsLibrary

Public Class DesignerForm
	Private Const DefaultReportType As ExportForm.ReportType = ExportForm.ReportType.PageCpl

	Private _reportName As String
	Private _reportType As DesignerReportType
	Private _exportReportType As ExportForm.ReportType = DefaultReportType
	Private _isDirty As Boolean

	Private _exportMenuItem As ToolStripMenuItem
	Private _exportForm As ExportForm

	Private _disconnectFromServerButton As ToolStripButton

	Private Const ReportPartsDirectoryKey As String = "ReportPartsDirectory"

	Public Sub New()
		InitializeComponent()
		Icon = My.Resources.App

		'Create new report instance and assign to Report Explorer
		'Note:  Assigning the ToolBox to the designer before calling NewReport
		' will automaticly add the default controls to the toolbox in a group called
		' "ActiveReports"
		arDesigner.Toolbox = arToolbox
		AddHandler arDesigner.LayoutChanged, AddressOf OnDesignerLayoutChanged
		reportExplorer.ReportDesigner = arDesigner
		layerList.ReportDesigner = arDesigner
		groupEditor.ReportDesigner = arDesigner
		reportsLibrary.ReportDesigner = arDesigner
		Dim config = Configuration
		If config IsNot Nothing AndAlso Not String.IsNullOrEmpty(config.Settings(ReportPartsDirectoryKey)) Then
			arDesigner.ReportPartsDirectory = config.Settings(ReportPartsDirectoryKey)
		End If

		' Add Menu and CommandBar to Form
		Dim menuStrip As ToolStrip = arDesigner.CreateToolStrips(DesignerToolStrips.Menu)(0)

		Dim fileMenu = CType(menuStrip.Items(0), ToolStripDropDownItem)
		CreateFileMenu(fileMenu)

		AppendToolStrips(0, {menuStrip})
		AppendToolStrips(1, arDesigner.CreateToolStrips(DesignerToolStrips.Edit, DesignerToolStrips.Undo, DesignerToolStrips.Zoom))

		Dim reportStrip As ToolStrip = CreateReportToolbar()
		AppendToolStrips(1, New List(Of ToolStrip)() From {
			reportStrip
		})

		AppendToolStrips(2, arDesigner.CreateToolStrips(DesignerToolStrips.Format, DesignerToolStrips.Layout))

		menuStrip.Items.Add(CreateHelpMenu())

		' Activate default group on the toolbox
		arToolbox.SelectedCategory = My.Resources.DefaultGroup

		SetReportName(Nothing)

		AddHandler arDesigner.ReportChanged, AddressOf UpdateReportName
		AddHandler arDesigner.LayoutChanged, Sub(_s, args)
												 If args.Type = LayoutChangeType.ReportLoad OrElse args.Type = LayoutChangeType.ReportClear Then
													 RefreshExportEnabled()
												 End If
											 End Sub
		RefreshExportEnabled()
		RefreshLayersTab()
		RefreshGroupEditor()
		InitGroupEditorToggle()

		AddHandler Load, AddressOf DesignerForm_Load

		AllowDrop = True
		AddHandler DragEnter, AddressOf DesignerForm_DragEnter
		AddHandler DragDrop, AddressOf DesignerForm_DragDrop
	End Sub

	Private ReadOnly Property Configuration() As IConfigurationManager
		Get
			Dim configManager = TryCast(GlobalServices.Instance.GetService(GetType(IConfigurationManager)), IConfigurationManager)
			If configManager Is Nothing Then
				Dim provider = TryCast(GlobalServices.Instance.GetService(GetType(IConfigurationProvider)), IConfigurationProvider)
				If provider IsNot Nothing Then
					Return New ReportingConfiguration(provider)
				End If
			End If
			Return configManager
		End Get
	End Property

	Private Sub DesignerForm_DragEnter(sender As Object, e As DragEventArgs)
		If Visible AndAlso Not CanFocus Then
			Return
		End If

		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			Dim filename As String = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
			Dim fi = New FileInfo(filename)
			If (fi.Extension = ".rdlx") OrElse (fi.Extension = ".rpx") OrElse (fi.Extension = ".rdlx-master") OrElse (fi.Extension = ".rdl") Then
				e.Effect = DragDropEffects.Copy
			Else
				e.Effect = DragDropEffects.None
			End If
		End If
	End Sub

	Private Sub DesignerForm_DragDrop(sender As Object, e As DragEventArgs)
		Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())
		If files.Length <= 0 Then
			Return
		End If
		If Not ConfirmSaveChanges(Me) Then
			Return
		End If
		Dim reportName As String = TryLoadReport(files(0))
		_isDirty = False
		_exportReportType = DetermineReportType()
		SetReportName(reportName)
	End Sub

	Private Sub DesignerForm_Load(sender As Object, e As EventArgs)
		_exportReportType = DefaultReportType
		Dim commandLineArgs As String() = Environment.GetCommandLineArgs()
		If commandLineArgs.Length > 1 Then
			Dim reportName As String = TryLoadReport(commandLineArgs(1))
			_isDirty = False
			_exportReportType = DetermineReportType()
			SetReportName(reportName)
		End If
	End Sub

	Private Sub RefreshExportEnabled()
		RemoveHandler arDesigner.ActiveTabChanged, AddressOf OnEnableExport
		AddHandler arDesigner.ActiveTabChanged, AddressOf OnEnableExport
		OnEnableExport(Me, EventArgs.Empty)
	End Sub

	Private Sub OnEnableExport(sender As Object, eventArgs As EventArgs)
		_exportMenuItem.Enabled = arDesigner.ActiveTab = DesignerTab.Preview
	End Sub

	Private Sub OnDesignerLayoutChanged(sender As Object, e As LayoutChangedArgs)
		' load or new report events
		If e.Type = LayoutChangeType.ReportLoad OrElse e.Type = LayoutChangeType.ReportClear Then
			arToolbox.Reorder(arDesigner)
			' reorder toolbox
			arToolbox.EnsureCategories()
			'check Data tools availability
			arToolbox.Refresh()

			RefreshLayersTab()
			RefreshGroupEditor()
		End If
		'only new report event
		If e.Type = LayoutChangeType.ReportClear Then
			_isDirty = False
			_exportReportType = DetermineReportType()
			' set report name to "untitled"
			SetReportName(Nothing)
		End If
		' only load report event
		If e.Type = LayoutChangeType.ReportLoad Then
			If Not String.IsNullOrEmpty(_reportName) Then
				' FPL/CPL conversion trigger this event
				' so we should notify user, that report was updated
				_isDirty = _exportReportType <> DetermineReportType()

				' if page report was converted to master - update its extension
				If GetIsMaster() Then
					Dim extansion = Path.GetExtension(_reportName)
					If Not String.IsNullOrEmpty(extansion) AndAlso (extansion.ToLowerInvariant() = ".rdl" OrElse extansion.ToLowerInvariant() = ".rdlx") Then
						_reportName = _reportName.Substring(0, _reportName.Length - extansion.Length) + ".rdlx-master"

						' if file with this name already exists - set dirty flag
						_isDirty = File.Exists(_reportName)
					End If
				End If
			End If

			_exportReportType = DetermineReportType()
		End If

		BeginInvoke(New MethodInvoker(AddressOf UpdateReportName))
	End Sub

	Private Sub RefreshLayersTab()
		If arDesigner.ReportType = DesignerReportType.Section Then
			If splitContainerProperties.Panel1.Controls.Contains(tabControl1) Then
				' remove tabs, leave only report explorer
				reportExplorerTabPage.SuspendLayout()
				splitContainerProperties.Panel1.SuspendLayout()

				reportExplorerTabPage.Controls.Remove(reportExplorer)
				splitContainerProperties.Panel1.Controls.Remove(tabControl1)
				splitContainerProperties.Panel1.Controls.Add(reportExplorer)

				reportExplorerTabPage.ResumeLayout()
				splitContainerProperties.Panel1.ResumeLayout()
			End If
		ElseIf Not splitContainerProperties.Panel1.Controls.Contains(tabControl1) Then
			' restore tabs
			reportExplorerTabPage.SuspendLayout()
			splitContainerProperties.Panel1.SuspendLayout()

			splitContainerProperties.Panel1.Controls.Remove(reportExplorer)
			reportExplorerTabPage.Controls.Add(reportExplorer)
			splitContainerProperties.Panel1.Controls.Add(tabControl1)

			reportExplorerTabPage.ResumeLayout()
			splitContainerProperties.Panel1.ResumeLayout()
		End If
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
														  _groupEditorSize = splitContainerGroupEditor.ClientSize.Height - splitContainerGroupEditor.SplitterDistance
														  splitContainerGroupEditor.SplitterDistance = splitContainerGroupEditor.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - splitContainerGroupEditor.Panel2.Padding.Vertical - splitContainerGroupEditor.SplitterWidth
														  splitContainerGroupEditor.IsSplitterFixed = True
														  groupEditor.Visible = False
													  Else
														  GroupEditorToggleButton.Image = My.Resources.GroupEditorHide
														  splitContainerGroupEditor.SplitterDistance = CInt(If(_groupEditorSize < splitContainerGroupEditor.ClientSize.Height, splitContainerGroupEditor.ClientSize.Height - _groupEditorSize, splitContainerGroupEditor.ClientSize.Height * 2 / 3))
														  splitContainerGroupEditor.IsSplitterFixed = False
														  groupEditor.Visible = True
													  End If
												  End Sub

		AddHandler groupEditor.VisibleChanged, Sub() GroupPanelVisibility.SetToolTip(GroupEditorToggleButton, If(groupEditor.Visible, My.Resources.HideGroupPanelToolTip, My.Resources.ShowGroupPanelToolTip))
	End Sub

	Private Sub RefreshGroupEditor()
		splitContainerGroupEditor.Panel2Collapsed = arDesigner.ReportType = DesignerReportType.Section
	End Sub

	Protected Overrides Sub OnClosing(e As CancelEventArgs)
		e.Cancel = e.Cancel Or Not ConfirmSaveChanges(Me)
		MyBase.OnClosing(e)
	End Sub

	Private Sub UpdateReportName()
		SetReportName(_reportName)
	End Sub

	Private Sub AppendToolStrips(row As Integer, toolStrips As IList(Of ToolStrip))
		Dim panel As ToolStripPanel = toolStripContainer1.TopToolStripPanel
		Dim i As Integer = toolStrips.Count
		While System.Threading.Interlocked.Decrement(i) >= 0
			panel.Join(toolStrips(i), row)
		End While
	End Sub

#Region "Menu and Toolbar command handlers"

	Private Function ConfirmSaveChanges(control As Control) As Boolean
		If IsDirty Then
			Dim fileName As Object = If(Not IsNothing(_reportName), Path.GetFileName(_reportName), GetDefaultReportName(_reportType))
			Dim result As DialogResult = MessageBox.Show(control, [String].Format(My.Resources.EudUnsavedChangesMessage, fileName), My.Resources.EudUnsavedChangesTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If result = DialogResult.Cancel Then
				Return False
			End If

			If result = DialogResult.Yes Then
				Return PerformSave()
			End If
		End If

		Return True
	End Function

	Private Sub OnNew(sender As Object, e As EventArgs)
		If Not ConfirmSaveChanges(Me) Then
			Return
		End If

		arDesigner.ExecuteAction(DesignerAction.NewReport)
	End Sub

	Private Sub OnOpen(sender As Object, e As EventArgs)
		If Not ConfirmSaveChanges(Me) Then
			Return
		End If

		openDialog.FileName = String.Empty

		Dim selectedIndex As Integer
		If openDialog.ShowDialog(Me, {My.Resources.OpenAsLibrary}, selectedIndex) = DialogResult.OK Then
			Dim asLibrary = selectedIndex = 0
			If Not asLibrary Then
				Dim reportName As String = TryLoadReport(openDialog.FileName)
				_isDirty = False
				_exportReportType = DetermineReportType()
				SetReportName(reportName)
			Else
				Dim host = TryCast(CType(arDesigner.Report, IComponent).Site.GetService(GetType(IDesignerHost)), IDesignerHost)
				Dim reportsLibraryService = If(Not IsNothing(host), TryCast(host.GetService(GetType(IReportsLibraryService)), IReportsLibraryService), Nothing)
				If reportsLibraryService IsNot Nothing Then
					reportsLibraryService.LoadReport(openDialog.FileName)
				End If
			End If
		End If
	End Sub

	Private Function TryLoadReport(fileName As String) As String
		Try
			arDesigner.LoadReport(New FileInfo(fileName))
			Return fileName
		Catch ex As Exception
			MessageBox.Show(Me, [String].Format(My.Resources.ReportIsNotValid, fileName), My.Resources.DesignerFormTitle, MessageBoxButtons.OK, MessageBoxIcon.[Error])
			If String.IsNullOrEmpty(_reportName) Then
				arDesigner.NewReport(_reportType)
				Return Nothing
			End If
			Return TryLoadReport(_reportName)
		End Try
	End Function

	Private Function PerformSave() As Boolean
		If String.IsNullOrEmpty(_reportName) OrElse String.IsNullOrEmpty(Path.GetDirectoryName(_reportName)) OrElse Not File.Exists(_reportName) Then
			Return PerformSaveAs()
		End If
		arDesigner.SaveReport(New FileInfo(_reportName))
		_isDirty = False
		UpdateReportName()
		Return True
	End Function

	Private Shared Function GetSaveFilter(type As DesignerReportType, isMaster As Boolean) As String
		Select Case type
			Case DesignerReportType.Section
				Return My.Resources.SaveRpxFilter
			Case DesignerReportType.Page
				Return My.Resources.SaveRdlxFilter
			Case DesignerReportType.Rdl
				Return If(isMaster, My.Resources.SaveRdlxMasterFilter, My.Resources.SaveRdlFilter)
			Case Else
				Return My.Resources.SaveRpxFilter
		End Select
	End Function

	Private Function PerformSaveAs() As Boolean
		Dim fileName = If(Not IsNothing(_reportName), Path.GetFileName(_reportName), GetDefaultReportName(_reportType))
		saveDialog.FileName = fileName
		saveDialog.Filter = GetSaveFilter(arDesigner.ReportType, GetIsMaster())

		If saveDialog.ShowDialog() = DialogResult.OK Then
			arDesigner.SaveReport(New FileInfo(saveDialog.FileName))
			_isDirty = False
			SetReportName(saveDialog.FileName)
			Return True
		End If

		Return False
	End Function

	Private Sub PerformExport()
		If _exportForm Is Nothing Then
			_exportForm = New ExportForm(ConfigurationHelper.GetConfigFlag(ConfigurationHelper.UsePdfExportFilterKey).HasValue AndAlso ConfigurationHelper.GetConfigFlag(ConfigurationHelper.UsePdfExportFilterKey).Value = True)
		End If
		_exportForm.Show(Me, New ExportViewer(arDesigner.ReportViewer), _exportReportType)
	End Sub

	''' <summary>
	''' Determines if the specified report is FPL report
	''' </summary>
	Private Function DetermineReportType() As ExportForm.ReportType
		Dim sectionReport = TryCast(arDesigner.Report, SectionReport)
		If sectionReport IsNot Nothing Then
			Return ExportForm.ReportType.Section
		End If

		Dim pageReport = TryCast(arDesigner.Report, PageReport)
		If pageReport Is Nothing Then
			Return DefaultReportType
		End If

		Dim report = pageReport.Report
		If report Is Nothing OrElse report.Body Is Nothing Then
			Return DefaultReportType
		End If

		Dim items As ReportItemCollection = report.Body.ReportItems
		Return If(Not IsNothing(items) AndAlso items.Count = 1 AndAlso TypeOf items(0) Is FixedPage, ExportForm.ReportType.PageFpl, ExportForm.ReportType.PageCpl)
	End Function

	Private Function GetIsMaster() As Boolean
		Dim isMaster As Boolean = False
		If arDesigner.ReportType = DesignerReportType.Rdl Then
			Dim report = CType(arDesigner.Report, Component)
			Dim site = If(IsNothing(report), Nothing, report.Site)
			If site IsNot Nothing Then
				Dim host As IDesignerHost = TryCast(site.GetService(GetType(IDesignerHost)), IDesignerHost)
				If host IsNot Nothing Then
					Dim mcs = TryCast(host.GetService(GetType(IMasterContentService)), IMasterContentService)
					isMaster = Not IsNothing(mcs) AndAlso mcs.IsMaster
				End If
			End If
		End If
		Return isMaster
	End Function

	Private Sub OnSave(sender As Object, e As EventArgs)
		PerformSave()
	End Sub

	Private Sub OnSaveAs(sender As Object, e As EventArgs)
		PerformSaveAs()
	End Sub

	Private Sub OnExport(sender As Object, e As EventArgs)
		PerformExport()
	End Sub
	''' <summary>
	''' OnExit
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub OnExit(sender As Object, e As EventArgs)
		Close()
	End Sub

	''' <summary>
	''' OnAbout
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub OnAbout(sender As Object, e As EventArgs)
		Const showAboutBoxMethodName As String = "ShowAboutBox"
		Dim attributes = arDesigner.[GetType]().GetCustomAttributes(GetType(LicenseProviderAttribute), False)
		If attributes.Length <> 1 Then
			Return
		End If
		Dim attr = CType(attributes(0), LicenseProviderAttribute)
		Dim provider = CType(Activator.CreateInstance(attr.LicenseProvider), LicenseProvider)

		Dim methodInfo = provider.[GetType]().GetMethod(showAboutBoxMethodName, BindingFlags.NonPublic Or BindingFlags.Instance)

		If methodInfo IsNot Nothing Then
			methodInfo.Invoke(provider, New Object() {arDesigner.[GetType]()})
		End If
	End Sub

#End Region

#Region "Menu and Toolbar"

	Private Function CreateHelpMenu() As ToolStripDropDownItem
		Dim helpMenu = New ToolStripMenuItem(My.Resources.Help, Nothing, New ToolStripItem() {})
		helpMenu.DropDownItems.Clear()
		helpMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.About, Nothing, AddressOf OnAbout))
		Return helpMenu
	End Function

	Private Sub CreateFileMenu(fileMenu As ToolStripDropDownItem)
		_exportMenuItem = New ToolStripMenuItem(My.Resources.ExportText, My.Resources.CmdExport, AddressOf OnExport, Keys.Control Or Keys.E)

		fileMenu.DropDownItems.Clear()
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.NewText, My.Resources.CmdNewReport, AddressOf OnNew, Keys.Control Or Keys.N))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.Open, My.Resources.CmdOpen, AddressOf OnOpen, Keys.Control Or Keys.O))

		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.Save, My.Resources.CmdSave, AddressOf OnSave, Keys.Control Or Keys.S))
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.SaveAs, My.Resources.CmdSaveAs, AddressOf OnSaveAs))

		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(_exportMenuItem)
		fileMenu.DropDownItems.Add(New ToolStripSeparator())
		fileMenu.DropDownItems.Add(New ToolStripMenuItem(My.Resources.ExitText, Nothing, AddressOf OnExit))

		_exportMenuItem.Enabled = arDesigner.ActiveTab = DesignerTab.Preview
	End Sub

	Private Function CreateReportToolbar() As ToolStrip


		Return New ToolStrip({CreateToolStripButton(My.Resources.NewText, My.Resources.CmdNewReport, AddressOf OnNew, My.Resources.NewToolTip), CreateToolStripButton(My.Resources.Open, My.Resources.CmdOpen, AddressOf OnOpen, My.Resources.OpenToolTip), CreateToolStripButton(My.Resources.Save, My.Resources.CmdSave, AddressOf OnSave, My.Resources.SaveToolTip)}) With {
			.AccessibleName = "toolStripFile"
		}
	End Function

	Private Shared Function CreateToolStripButton(text As String, image As System.Drawing.Image, handler As EventHandler, toolTip As String) As ToolStripButton
		Dim button = New ToolStripButton(text, image, handler) With {
			.DisplayStyle = ToolStripItemDisplayStyle.Image,
			.ToolTipText = toolTip,
			.DoubleClickEnabled = True
		}
		Return button
	End Function

#End Region

#Region "Form title"

	Private Sub SetReportName(reportName As String)
		_reportType = arDesigner.ReportType
		_reportName = If(String.IsNullOrEmpty(reportName), My.Resources.DefaultReportNameRdlx, reportName)
		Text = My.Resources.SampleNameTitle + Path.GetFileName(_reportName) + If(arDesigner.IsDirty, My.Resources.DirtySign, String.Empty)
	End Sub

	Private ReadOnly Property IsDirty() As Boolean
		Get
			Return arDesigner.IsDirty OrElse _isDirty
		End Get
	End Property

	Private Function GetDefaultReportName(reportType As DesignerReportType) As String
		Select Case reportType
			Case DesignerReportType.Section
				Return My.Resources.DefaultReportNameRpx
			Case DesignerReportType.Rdl
				Return If(GetIsMaster(), My.Resources.DefaultReportNameRdlxMaster, My.Resources.DefaultReportNameRdl)
			Case DesignerReportType.Page
				Return My.Resources.DefaultReportNameRdlx
		End Select

		Throw New ApplicationException("Unsupported report type: " + reportType.ToString)
	End Function

#End Region

End Class
