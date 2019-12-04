using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Configuration;
using GrapeCity.ActiveReports.Design.ReportsLibrary;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports.ReportsCore.Configuration;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using GrapeCity.ActiveReports.Win.Export;
using Image = System.Drawing.Image;

namespace GrapeCity.ActiveReports.Designer.Win
{
	internal partial class DesignerForm : Form
	{
		private const ExportForm.ReportType DefaultReportType = ExportForm.ReportType.PageCpl;

		private string _reportName;
		private DesignerReportType _reportType;
		private ExportForm.ReportType _exportReportType = DefaultReportType;
		private bool _isDirty;

		private ToolStripMenuItem _exportMenuItem;
		private ExportForm _exportForm;

		private const string ReportPartsDirectoryKey = "ReportPartsDirectory";

		public DesignerForm()
		{
			InitializeComponent();
			Icon = Resources.App;

			//Create new report instance and assign to Report Explorer
			//Note:  Assigning the ToolBox to the designer before calling NewReport
			// will automaticly add the default controls to the toolbox in a group called
			// "ActiveReports"
			arDesigner.Toolbox = arToolbox;
			arDesigner.LayoutChanged += OnDesignerLayoutChanged;
			reportExplorer.ReportDesigner = arDesigner;
			layerList.ReportDesigner = arDesigner;
			groupEditor.ReportDesigner = arDesigner;
			reportsLibrary.ReportDesigner = arDesigner;
			var config = Configuration;
			if (config != null && !string.IsNullOrEmpty(config.Settings[ReportPartsDirectoryKey]))
				arDesigner.ReportPartsDirectory = config.Settings[ReportPartsDirectoryKey];

			// Add Menu and CommandBar to Form
			ToolStrip menuStrip = arDesigner.CreateToolStrips(DesignerToolStrips.Menu)[0];

			var fileMenu = (ToolStripDropDownItem)menuStrip.Items[0];
			CreateFileMenu(fileMenu);

			AppendToolStrips(0, new[] { menuStrip });
			AppendToolStrips(1, arDesigner.CreateToolStrips(
				DesignerToolStrips.Edit,
				DesignerToolStrips.Undo,
				DesignerToolStrips.Zoom));

			ToolStrip reportStrip = CreateReportToolbar();
			AppendToolStrips(1, new List<ToolStrip> { reportStrip });

			AppendToolStrips(2, arDesigner.CreateToolStrips(
				DesignerToolStrips.Format, DesignerToolStrips.Layout));

			menuStrip.Items.Add(CreateHelpMenu());

			// Activate default group on the toolbox
			arToolbox.SelectedCategory = Resources.DefaultGroup;
			
			SetReportName(null);

			arDesigner.ReportChanged += (_, __) => UpdateReportName();
			arDesigner.LayoutChanged += (_, args) => { if (args.Type == LayoutChangeType.ReportLoad || args.Type == LayoutChangeType.ReportClear) RefreshExportEnabled(); };
			RefreshExportEnabled();
			RefreshLayersTab();
			RefreshGroupEditor();
			InitGroupEditorToggle();

			Load += DesignerForm_Load;

			AllowDrop = true;
			DragEnter += DesignerForm_DragEnter;
			DragDrop += DesignerForm_DragDrop;
		}

		IConfigurationManager Configuration
		{
			get
			{
				var configManager = GlobalServices.Instance.GetService(typeof(IConfigurationManager)) as IConfigurationManager;
				if (configManager == null)
				{
					var provider = GlobalServices.Instance.GetService(typeof(IConfigurationProvider)) as IConfigurationProvider;
					if (provider != null)
					{
						return new ReportingConfiguration(provider);
					}
				}
				return configManager;
			}
		}

		void DesignerForm_DragEnter(object sender, DragEventArgs e)
		{
			if (Visible && !CanFocus) return;

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string filename = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
				var fi = new FileInfo(filename);
				if ((fi.Extension == ".rdlx") || (fi.Extension == ".rpx") || (fi.Extension == ".rdlx-master") || (fi.Extension == ".rdl"))
				e.Effect = DragDropEffects.Copy;
				else e.Effect = DragDropEffects.None;				
			}
		}

		void DesignerForm_DragDrop(object sender, DragEventArgs e)
		{
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length <= 0) return;
			if (!ConfirmSaveChanges(this)) return;
			string reportName = TryLoadReport(files[0]);
			_isDirty = false;
			_exportReportType = DetermineReportType();
			SetReportName(reportName);
		}

		void DesignerForm_Load(object sender, EventArgs e)
		{
			_exportReportType = DefaultReportType;
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs.Length > 1)
			{
				string reportName = TryLoadReport(commandLineArgs[1]);
				_isDirty = false;
				_exportReportType = DetermineReportType();
				SetReportName(reportName);
			}
		}

		private void RefreshExportEnabled()
		{
			arDesigner.ActiveTabChanged -= OnEnableExport;
			arDesigner.ActiveTabChanged += OnEnableExport;
			OnEnableExport(this, EventArgs.Empty);
		}

		private void OnEnableExport(object sender, EventArgs eventArgs)
		{
			_exportMenuItem.Enabled = arDesigner.ActiveTab == DesignerTab.Preview;
		}

		private void OnDesignerLayoutChanged(object sender, LayoutChangedArgs e)
		{
			// load or new report events
			if (e.Type == LayoutChangeType.ReportLoad || e.Type == LayoutChangeType.ReportClear)
			{
				arToolbox.Reorder(arDesigner);
				// reorder toolbox
				arToolbox.EnsureCategories(); //check Data tools availability
				arToolbox.Refresh();

				RefreshLayersTab();
				RefreshGroupEditor();
			}
			//only new report event
			if (e.Type == LayoutChangeType.ReportClear)
			{
				_isDirty = false;
				_exportReportType = DetermineReportType();
				// set report name to "untitled"
				SetReportName(null);
			}
			// only load report event
			if (e.Type == LayoutChangeType.ReportLoad)
			{
				if (!string.IsNullOrEmpty(_reportName))
				{
					// FPL/CPL conversion trigger this event
					// so we should notify user, that report was updated
					_isDirty = _exportReportType != DetermineReportType();

					// if page report was converted to master - update its extension
					if (GetIsMaster())
					{
						var extansion = Path.GetExtension(_reportName);
						if (!string.IsNullOrEmpty(extansion) &&
							(extansion.ToLowerInvariant() == ".rdl" ||
							 extansion.ToLowerInvariant() == ".rdlx"))
						{
							_reportName = _reportName.Substring(0, _reportName.Length - extansion.Length) + ".rdlx-master";

							// if file with this name already exists - set dirty flag
							_isDirty = File.Exists(_reportName);
						}
					}
				}

				_exportReportType = DetermineReportType();
			}

			BeginInvoke(new MethodInvoker(UpdateReportName));
		}

		private void RefreshLayersTab()
		{
			if (arDesigner.ReportType == DesignerReportType.Section)
			{
				if (splitContainerProperties.Panel1.Controls.Contains(tabControl1))
				{
					// remove tabs, leave only report explorer
					reportExplorerTabPage.SuspendLayout();
					splitContainerProperties.Panel1.SuspendLayout();

					reportExplorerTabPage.Controls.Remove(reportExplorer);
					splitContainerProperties.Panel1.Controls.Remove(tabControl1);
					splitContainerProperties.Panel1.Controls.Add(reportExplorer);

					reportExplorerTabPage.ResumeLayout();
					splitContainerProperties.Panel1.ResumeLayout();
				}
			}
			else if (!splitContainerProperties.Panel1.Controls.Contains(tabControl1))
			{
				// restore tabs
				reportExplorerTabPage.SuspendLayout();
				splitContainerProperties.Panel1.SuspendLayout();

				splitContainerProperties.Panel1.Controls.Remove(reportExplorer);
				reportExplorerTabPage.Controls.Add(reportExplorer);
				splitContainerProperties.Panel1.Controls.Add(tabControl1);

				reportExplorerTabPage.ResumeLayout();
				splitContainerProperties.Panel1.ResumeLayout();
			}
		}

		private int _groupEditorSize;
		private void InitGroupEditorToggle()
		{
			GroupEditorToggleButton.Image = Resources.GroupEditorHide;
			GroupEditorToggleButton.MouseEnter += (sender, args) => { GroupEditorToggleButton.BackColor = Color.LightGray; };
			GroupEditorToggleButton.MouseLeave += (sender, args) => { GroupEditorToggleButton.BackColor = Color.Gainsboro; };
			GroupEditorToggleButton.Click += (sender, args) =>
			{
				if (groupEditor.Visible)
				{
					GroupEditorToggleButton.Image = Resources.GroupEditorShow;
					_groupEditorSize = splitContainerGroupEditor.ClientSize.Height - splitContainerGroupEditor.SplitterDistance;
					splitContainerGroupEditor.SplitterDistance = splitContainerGroupEditor.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - splitContainerGroupEditor.Panel2.Padding.Vertical - splitContainerGroupEditor.SplitterWidth;
					splitContainerGroupEditor.IsSplitterFixed = true;
					groupEditor.Visible = false;
				}
				else
				{
					GroupEditorToggleButton.Image = Resources.GroupEditorHide;
					splitContainerGroupEditor.SplitterDistance = _groupEditorSize < splitContainerGroupEditor.ClientSize.Height ? splitContainerGroupEditor.ClientSize.Height - _groupEditorSize : splitContainerGroupEditor.ClientSize.Height * 2 / 3;
					splitContainerGroupEditor.IsSplitterFixed = false;
					groupEditor.Visible = true;
				}
			};

			groupEditor.VisibleChanged += (sender, args) => GroupPanelVisibility.SetToolTip(GroupEditorToggleButton,
																										groupEditor.Visible
																											? Resources.HideGroupPanelToolTip
																											: Resources.ShowGroupPanelToolTip);
		}

		private void RefreshGroupEditor()
		{
			splitContainerGroupEditor.Panel2Collapsed = arDesigner.ReportType == DesignerReportType.Section;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel |= !ConfirmSaveChanges(this);
			base.OnClosing(e);
		}

		private void UpdateReportName()
		{
			SetReportName(_reportName);
		}

		private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
		{
			ToolStripPanel panel = toolStripContainer1.TopToolStripPanel;
			for (int i = toolStrips.Count; --i >= 0; )
			{
				panel.Join(toolStrips[i], row);
			}
		}

		#region Menu and Toolbar command handlers

		private bool ConfirmSaveChanges(Control control)
		{
			if (IsDirty)
			{
				var fileName = _reportName != null
									? Path.GetFileName(_reportName)
									: GetDefaultReportName(_reportType);
				DialogResult result =
					MessageBox.Show(control, String.Format(Resources.EudUnsavedChangesMessage, fileName),
									Resources.EudUnsavedChangesTitle
									, MessageBoxButtons.YesNoCancel
									, MessageBoxIcon.Question);

				if (result == DialogResult.Cancel)
				{
					return false;
				}

				if (result == DialogResult.Yes)
				{
					return PerformSave();
				}
			}

			return true;
		}

		private void OnNew(object sender, EventArgs e)
		{
			if (!ConfirmSaveChanges(this)) 
				return;

			arDesigner.ExecuteAction(DesignerAction.NewReport);
		}

		private void OnOpen(object sender, EventArgs e)
		{
			if (!ConfirmSaveChanges(this))
			{
				return;
			}

			openDialog.FileName = string.Empty;

			int selectedIndex;
			if (openDialog.ShowDialog(this, new[] { Resources.OpenAsLibrary }, out selectedIndex) == DialogResult.OK)
			{
				var asLibrary = selectedIndex == 0;
				if (!asLibrary)
				{
					string reportName = TryLoadReport(openDialog.FileName);
					_isDirty = false;
					_exportReportType = DetermineReportType();
					SetReportName(reportName);
				}
				else
				{
					var host = ((IComponent)arDesigner.Report).Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					var reportsLibraryService = host != null ? host.GetService(typeof(IReportsLibraryService)) as IReportsLibraryService : null;
					if (reportsLibraryService != null)
						reportsLibraryService.LoadReport(openDialog.FileName);
				}
			}
		}

		private string TryLoadReport(string fileName)
		{
			try
			{
				arDesigner.LoadReport(new FileInfo(fileName));
				return fileName;
			}
			catch (Exception )
			{
				MessageBox.Show(this, String.Format(Resources.ReportIsNotValid, fileName), 
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (string.IsNullOrEmpty(_reportName))
				{
					arDesigner.NewReport(_reportType);
					return null;
				}
				return TryLoadReport(_reportName);
			}
		}

		private bool PerformSave()
		{
			if (string.IsNullOrEmpty(_reportName)
				|| string.IsNullOrEmpty(Path.GetDirectoryName(_reportName))
				|| !File.Exists(_reportName))
			{
				return PerformSaveAs();
			}
			arDesigner.SaveReport(new FileInfo(_reportName));
			_isDirty = false;
			UpdateReportName();
			return true;
		}

		private static string GetSaveFilter(DesignerReportType type, bool isMaster)
		{
			switch (type)
			{
				case DesignerReportType.Section:
					return Resources.SaveRpxFilter;
				case DesignerReportType.Page:
					return Resources.SaveRdlxFilter;
				case DesignerReportType.Rdl:
					return isMaster ? Resources.SaveRdlxMasterFilter : Resources.SaveRdlFilter;
				default:
					return Resources.SaveRpxFilter;
			}
		}

		private bool PerformSaveAs()
		{
			var fileName = _reportName != null
									? Path.GetFileName(_reportName)
									: GetDefaultReportName(_reportType);
			saveDialog.FileName = fileName;
			saveDialog.Filter = GetSaveFilter(arDesigner.ReportType, GetIsMaster());

			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				arDesigner.SaveReport(new FileInfo(saveDialog.FileName));
				_isDirty = false;
				SetReportName(saveDialog.FileName);
				return true;
			}

			return false;
		}

		private void PerformExport()
		{
			if (_exportForm == null)
			{
				_exportForm = new ExportForm(ConfigurationHelper.GetConfigFlag(ConfigurationHelper.UsePdfExportFilterKey) == true);
			}
			_exportForm.Show(this, new ExportViewer(arDesigner.ReportViewer), _exportReportType);
		}

		/// <summary>
		/// Determines if the specified report is FPL report
		/// </summary>
		private ExportForm.ReportType DetermineReportType()
		{
			var sectionReport = arDesigner.Report as SectionReport;
			if (sectionReport != null) return ExportForm.ReportType.Section;
			
			var pageReport = arDesigner.Report as PageReport;
			if (pageReport == null) return DefaultReportType;

			var report = pageReport.Report;
			if (report == null || report.Body == null) return DefaultReportType;

			ReportItemCollection items = report.Body.ReportItems;
			return items != null && items.Count == 1 && items[0] is FixedPage ? ExportForm.ReportType.PageFpl : ExportForm.ReportType.PageCpl;
		}

		private bool GetIsMaster()
		{
			bool isMaster = false;
			if (arDesigner.ReportType == DesignerReportType.Rdl)
			{
				var report = (Component) arDesigner.Report;
				var site = report == null ? null : report.Site;
				if (site != null)
				{
					var host = site.GetService(typeof (IDesignerHost)) as IDesignerHost;
					if (host != null)
					{
						var mcs = host.GetService(typeof (IMasterContentService)) as IMasterContentService;
						isMaster = mcs != null && mcs.IsMaster;
					}
				}
			}
			return isMaster;
		}

		private void OnSave(object sender, EventArgs e)
		{
			PerformSave();
		}

		private void OnSaveAs(object sender, EventArgs e)
		{
			PerformSaveAs();
		}

		private void OnExport(object sender, EventArgs e)
		{
			PerformExport();
		}
		/// <summary>
		/// OnExit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnExit(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// OnAbout
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAbout(object sender, EventArgs e)
		{
			const string showAboutBoxMethodName = "ShowAboutBox";
			var attributes = arDesigner.GetType().GetCustomAttributes(typeof (LicenseProviderAttribute), false);
			if (attributes.Length != 1) return;
			var attr = (LicenseProviderAttribute) attributes[0];
			var provider = ((LicenseProvider) Activator.CreateInstance(attr.LicenseProvider));

			var methodInfo = provider.GetType().GetMethod(showAboutBoxMethodName, BindingFlags.NonPublic | BindingFlags.Instance);

			if (methodInfo != null)
				methodInfo.Invoke(provider, new object[] {arDesigner.GetType()});
		}

		#endregion

		#region Menu and Toolbar

		private ToolStripDropDownItem CreateHelpMenu()
		{
			var helpMenu = new ToolStripMenuItem(Resources.Help, null, new ToolStripItem[] { });
			helpMenu.DropDownItems.Clear();
			helpMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.About, null, OnAbout));
			return helpMenu;
		}

		private void CreateFileMenu(ToolStripDropDownItem fileMenu)
		{
			_exportMenuItem = new ToolStripMenuItem(Resources.Export, Resources.CmdExport, OnExport, Keys.Control | Keys.E);

			fileMenu.DropDownItems.Clear();
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.New, Resources.CmdNewReport, OnNew, Keys.Control | Keys.N));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.Open, Resources.CmdOpen, OnOpen, Keys.Control | Keys.O));

			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.Save, Resources.CmdSave, OnSave, Keys.Control | Keys.S));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.SaveAs, Resources.CmdSaveAs, OnSaveAs));

			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(_exportMenuItem);
			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.Exit, null, OnExit));

			_exportMenuItem.Enabled = arDesigner.ActiveTab == DesignerTab.Preview;
		}

		private ToolStrip CreateReportToolbar()
		{

			return new ToolStrip(new[]
			                     	{
			                     		CreateToolStripButton(Resources.New, Resources.CmdNewReport, OnNew, Resources.NewToolTip),
			                     		CreateToolStripButton(Resources.Open, Resources.CmdOpen, OnOpen, Resources.OpenToolTip),
			                     		CreateToolStripButton(Resources.Save, Resources.CmdSave, OnSave, Resources.SaveToolTip),

			                     	})
			{
				AccessibleName = "toolStripFile"
			};
		}

		private static ToolStripButton CreateToolStripButton(string text, Image image, EventHandler handler, string toolTip)
		{
			var button = new ToolStripButton(text, image, handler)
				{
					DisplayStyle = ToolStripItemDisplayStyle.Image,
					ToolTipText = toolTip,
					DoubleClickEnabled = true
				};
			return button;
		}
        
		#endregion

		#region Form title

		private void SetReportName(string reportName)
		{
			_reportType = arDesigner.ReportType;
			_reportName = string.IsNullOrEmpty(reportName) ? Resources.DefaultReportNameRdlx : reportName;
			Text = Resources.SampleNameTitle + Path.GetFileName(_reportName) + (arDesigner.IsDirty ? Resources.DirtySign : string.Empty);
		}

		private bool IsDirty
		{
			get { return arDesigner.IsDirty || _isDirty; }
		}

		private string GetDefaultReportName(DesignerReportType reportType)
		{
			switch (reportType)
			{
				case DesignerReportType.Section:
					return Resources.DefaultReportNameRpx;
				case DesignerReportType.Rdl:
					return GetIsMaster() ? Resources.DefaultReportNameRdlxMaster : Resources.DefaultReportNameRdl;
				case DesignerReportType.Page:
					return Resources.DefaultReportNameRdlx;
			}

			throw new ApplicationException("Unsupported report type: " + reportType);
		}

		#endregion
	}
}
