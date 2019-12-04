using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using GrapeCity.ActiveReports.Design;
using System.Collections;
using GrapeCity.ActiveReports.Samples.ReportsGallery.Properties;

namespace GrapeCity.ActiveReports.Samples.ReportsGallery
{
	public partial class ReportsForm : Form
	{
		private string _reportName;
		static readonly string FolderPath = "";
		static readonly List<string> ExcludeFilesList = new List<string>();
		static readonly List<string> ExcludeFoldersList = new List<string>();

		public ReportsForm()
		{
			InitializeComponent();
		   

			//Populate the ToolBox, ReportExplorer and PropertyGrid
			reportDesigner.Toolbox = reportToolbox;//Attach the Toolbox to the report designer
			reportDesigner.LayoutChanged += OnDesignerLayoutChanged;
			reportExplorer.ReportDesigner = reportDesigner;//Attach the Report Explorer to the report designer
			reportDesigner.PropertyGrid = reportPropertyGrid;//Attach the Property Grid to the report designer
			layerList.ReportDesigner = reportDesigner;
			groupEditor.ReportDesigner = reportDesigner;

			//Create a toolstrip to be used as a menu.
			ToolStrip toolstrip = reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Menu
				})[0];
			toolstrip.Items.Remove(toolstrip.Items[2]);

			ToolStripDropDownItem fileMenu = (ToolStripDropDownItem)toolstrip.Items[0];
			CreateFileMenu(fileMenu);
			
			AppendToolStrips(0, new ToolStrip[]
				{
					toolstrip
				});
			AppendToolStrips(1, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Edit, 
					DesignerToolStrips.Undo, 
					DesignerToolStrips.Zoom
				}));
			AppendToolStrips(1, new ToolStrip[]
				{
					CreateReportToolbar()
				});
			AppendToolStrips(2, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Format, 
					DesignerToolStrips.Layout
				}));

			SetReportName(null);
			reportDesigner.ReportChanged += (_, __) => UpdateReportName();
			reportDesigner.NewReport(DesignerReportType.Page);
			InitGroupEditorToggle();
		}

		private void SetReportName(string reportName)
		{
			if (string.IsNullOrEmpty(reportName))
			{
				_reportName = reportDesigner.Report is PageReport ? Properties.Resources.DefaultReportNameRdlx : Properties.Resources.DefaultReportNameRpx;
			}
			else
			{
				_reportName = reportName;
			}
			Text = Properties.Resources.SampleNameTitle + Path.GetFileName(_reportName) + (reportDesigner.IsDirty ? Properties.Resources.DirtySign : string.Empty);
		}

		private void UpdateReportName()
		{
			SetReportName(_reportName);
		}

		private int _groupEditorSize;
		private void InitGroupEditorToggle()
		{
			GroupEditorToggleButton.Image = Properties.Resources.GroupEditorHide;
			GroupEditorToggleButton.MouseEnter += (sender, args) => { GroupEditorToggleButton.BackColor = Color.LightGray; };
			GroupEditorToggleButton.MouseLeave += (sender, args) => { GroupEditorToggleButton.BackColor = Color.Gainsboro; };
			GroupEditorToggleButton.Click += (sender, args) =>
			{
				if (groupEditor.Visible)
				{
					GroupEditorToggleButton.Image = Properties.Resources.GroupEditorShow;
					_groupEditorSize = splitContainer1.ClientSize.Height - splitContainer1.SplitterDistance;
					splitContainer1.SplitterDistance = splitContainer1.ClientSize.Height - GroupEditorSeparator.Height - GroupEditorContainer.Padding.Vertical - splitContainer1.Panel2.Padding.Vertical - splitContainer1.SplitterWidth;
					splitContainer1.IsSplitterFixed = true;
					groupEditor.Visible = false;
				}
				else
				{
					GroupEditorToggleButton.Image = Properties.Resources.GroupEditorHide;
					splitContainer1.SplitterDistance = _groupEditorSize < splitContainer1.ClientSize.Height ? splitContainer1.ClientSize.Height - _groupEditorSize : splitContainer1.ClientSize.Height * 2 / 3;
					splitContainer1.IsSplitterFixed = false;
					groupEditor.Visible = true;
				}
			};

			groupEditor.VisibleChanged += (sender, args) => GroupPanelVisibility.SetToolTip(GroupEditorToggleButton, groupEditor.Visible ? Resources.HideGroupPanelToolTip : Resources.ShowGroupPanelToolTip);
		}

		//Adding DropDownItems to the ToolStripDropDownItem
		private void CreateFileMenu(ToolStripDropDownItem fileMenu)
		{
			fileMenu.DropDownItems.Clear();
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Properties.Resources.NewText, Properties.Resources.CmdNewReport, new EventHandler(OnNew), (Keys.Control | Keys.N)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Properties.Resources.OpenText, Properties.Resources.CmdOpen, new EventHandler(OnOpen), (Keys.Control | Keys.O)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Properties.Resources.SaveText, Properties.Resources.CmdSave, new EventHandler(this.OnSave), (Keys.Control | Keys.S)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Properties.Resources.SaveAsText, Properties.Resources.CmdSaveAs, new EventHandler(OnSaveAs)));
			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Properties.Resources.ExitText, null, new EventHandler(OnExit)));
		}

		private ToolStrip CreateReportToolbar()
		{
			return new ToolStrip(new ToolStripButton[]
			{
				CreateToolStripButton(Properties.Resources.NewText,Properties.Resources.CmdNewReport,new EventHandler(OnNew),Properties.Resources.NewText),
				CreateToolStripButton(Properties.Resources.OpenText,Properties.Resources.CmdOpen,new EventHandler(OnOpen),Properties.Resources.OpenText),
				CreateToolStripButton(Properties.Resources.SaveText,Properties.Resources.CmdSave,new EventHandler(OnSave),Properties.Resources.SaveText)
			});
		}


		private void OnExit(object sender, EventArgs e)
		{
			Close();
		}

		//Getting the Designer to open a new report on "New" menu item click.
		private void OnNew(object sender, EventArgs e)
		{
			if (ConfirmSaveChanges())
			{
				reportDesigner.ReportChanged -= (_, __) => UpdateReportName();
				reportDesigner.ExecuteAction(DesignerAction.NewReport);
				reportDesigner.IsDirty = false;
				SetReportName(null);
				reportDesigner.ReportChanged += (_, __) => UpdateReportName();
			}
			ShowHideGroupEditor();
		}

		//Getting the Designer to open a report on "Open" menu item click.
		private void OnOpen(object sender, EventArgs e)
		{
			if (!ConfirmSaveChanges())
			{
				return;
			}

			using (var openDialog = new OpenFileDialog())
			{
				openDialog.FileName = string.Empty;
				openDialog.Filter = Properties.Resources.RdlxFilter;
				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					_reportName = openDialog.FileName;
					reportDesigner.LoadReport(new FileInfo(_reportName));
				}
			}

			ShowHideGroupEditor();
		}

		private void OnDesignerLayoutChanged(object sender, LayoutChangedArgs e)
		{
			// load or new report events
			if (e.Type == LayoutChangeType.ReportLoad || e.Type == LayoutChangeType.ReportClear)
			{
				reportToolbox.Reorder(reportDesigner);
				reportToolbox.EnsureCategories(); //check Data tools availability
				reportToolbox.Refresh();
			}
		}

		private void ShowHideGroupEditor()
		{
			if (reportDesigner.ReportType == DesignerReportType.Section)
			{
				splitContainer1.Panel2Collapsed = true;
			}
			else
			{
				splitContainer1.Panel2Collapsed = false;
			}
		}

		private string GetSaveFilter()
		{
			switch (reportDesigner.ReportType)
			{
				case DesignerReportType.Section:
					return Properties.Resources.RpxFilter;
				case DesignerReportType.Page:
				case DesignerReportType.Rdl:
					return Properties.Resources.RdlxFilter;
				default:
					return Properties.Resources.RpxFilter;
			}
		}

		//Getting the Designer to open a report on "Save" menu item click.
		private void OnSave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_reportName)
				|| string.IsNullOrEmpty(Path.GetDirectoryName(_reportName))
				|| !File.Exists(_reportName))
			{
				if (PerformSaveAs())
					reportDesigner.SaveReport(new FileInfo(_reportName));
			}
			else
			{
				reportDesigner.SaveReport(new FileInfo(_reportName));
			}
			SetReportName(_reportName);
		}

		//Getting the Designer to open a report on "Save As" menu item click.
		private void OnSaveAs(object sender, EventArgs e)
		{
			PerformSaveAs();
		}

		private bool PerformSaveAs()
		{
			using (var saveDialog = new SaveFileDialog())
			{
				saveDialog.Filter = GetSaveFilter();
				saveDialog.FileName = Path.GetFileName(_reportName);
				saveDialog.DefaultExt = ".rdlx";
				saveDialog.InitialDirectory = new DirectoryInfo(Application.ExecutablePath).Parent.Parent.Parent.FullName;
				if (saveDialog.ShowDialog() == DialogResult.OK)
				{
					_reportName = saveDialog.FileName;
					reportDesigner.SaveReport(new FileInfo(_reportName));
					reportDesigner.IsDirty = false;
					return true;
				}
			}

			return false;
		}

		//Checking whether modifications have been made to the report loaded to the designer
		private bool ConfirmSaveChanges()
		{
			if (reportDesigner.IsDirty)
			{
				DialogResult result = MessageBox.Show(Properties.Resources.ReportDirtyMessage, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Cancel)
				{
					return false;
				}
				if (result == DialogResult.Yes)
				{
					return PerformSaveAs();

				}
			}
			return true;
		}


		// Append toolstrips to the ToolStrip Panel
		private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
		{
			ToolStripPanel topToolStripPanel = toolStripContainer.TopToolStripPanel;
			int num = toolStrips.Count;
			while (--num >= 0)
			{
				topToolStripPanel.Join(toolStrips[num], row);
			}
		}

		// Create a ToolStrip Button
		private static ToolStripButton CreateToolStripButton(string text, Image image, EventHandler handler, string toolTip)
		{
			return new ToolStripButton(text, image, handler)
			{
				DisplayStyle = ToolStripItemDisplayStyle.Image,
				ToolTipText = toolTip,
				DoubleClickEnabled = true
			};
		}

		private void CreateReportExplorer()
		{
			if (reportDesigner.ReportType == DesignerReportType.Section)
			{
				if (explorerPropertyGridContainer.Panel1.Controls.Contains(reportExplorerTabControl))
				{
					reportExplorerTabControl.TabPages[0].SuspendLayout();
					explorerPropertyGridContainer.Panel1.SuspendLayout();				
					explorerPropertyGridContainer.Panel1.Controls.Remove(reportExplorerTabControl);
					explorerPropertyGridContainer.Panel1.Controls.Add(reportExplorer);
					reportExplorerTabControl.TabPages[0].ResumeLayout();
					explorerPropertyGridContainer.Panel1.ResumeLayout();
				}
			}
			else if (!explorerPropertyGridContainer.Panel1.Controls.Contains(reportExplorerTabControl))
			{

				reportExplorerTabControl.TabPages[0].SuspendLayout();
				explorerPropertyGridContainer.Panel1.SuspendLayout();
				explorerPropertyGridContainer.Panel1.Controls.Remove(reportExplorer);
				reportExplorerTabControl.TabPages[0].Controls.Add(reportExplorer);
				explorerPropertyGridContainer.Panel1.Controls.Add(reportExplorerTabControl);
				reportExplorerTabControl.TabPages[0].ResumeLayout();
				explorerPropertyGridContainer.Panel1.ResumeLayout();
			}
		}

		private void reportDesigner_LayoutChanged(object sender, LayoutChangedArgs e)
		{
			CreateReportExplorer();
		}
		
		//Load Settings from Config File
		static ReportsForm()
		{
			XDocument loaded = XDocument.Load("ReportsGallery.config");
			FolderPath = loaded.Descendants("FolderPath").Select(t => t.Value.ToString()).ToList()[0];
			DirectoryInfo reportbasefolder = new DirectoryInfo(FolderPath);
			ExcludeFilesList = loaded.Descendants("ExcludeFiles").ToList()[0].Descendants("File").Select(t => reportbasefolder.FullName + "\\" + t.Value.ToString()).ToList<string>();
			ExcludeFoldersList = loaded.Descendants("ExcludeFolders").ToList()[0].Descendants("Folder").Select(t => reportbasefolder.FullName + "\\" + t.Value.ToString()).ToList<string>();
		}

		// Form Load Event
		private void ReportsForm_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(FolderPath))
			{
				ListDirectory(treeView, FolderPath);
			}
			FolderLocalization();
			treeView.Nodes[1].Expand();
			treeView.Nodes[1].Nodes[0].Expand();
			var report = new FileInfo(treeView.Nodes[1].Nodes[0].Nodes[0].Tag.ToString());
			reportDesigner.LoadReport(report);
			_reportName = report.Name;
			UpdateReportName();
		}

		// Add nodes to tree view
		private void ListDirectory(TreeView treeView, string path)
		{
			treeView.Nodes.Clear();
			var rootDirectoryInfo = new DirectoryInfo(path);
			foreach (var directory in rootDirectoryInfo.GetDirectories())
			{
				treeView.Nodes.Add(CreateDirectoryNode(directory));
			}
		}

		// Traverse Samples Folder and create tree
		private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(directoryInfo.Name);
			foreach (var directory in directoryInfo.GetDirectories())
			{
				if (!ExcludeFoldersList.Exists(t => t.ToString() == directory.FullName))
				{
					directoryNode.Nodes.Add(CreateDirectoryNode(directory));
				}
			}
			foreach (var file in directoryInfo.GetFiles("*.rpx").Concat(directoryInfo.GetFiles("*.rdlx")))
			{
				if (!ExcludeFilesList.Exists(t => t.ToString() == file.FullName))
				{
					TreeNode reportFileNode = new TreeNode(file.Name);
					reportFileNode.ImageIndex = 2;
					reportFileNode.SelectedImageIndex = 2;
					reportFileNode.ForeColor = Color.Brown;
					reportFileNode.Tag = file.FullName;
					directoryNode.Nodes.Add(reportFileNode);
				}
			}
			return directoryNode;
		}

		//Folder Localization
		private void FolderLocalization()
		{

			Hashtable strReplace = new Hashtable();
			StreamReader reader = new StreamReader(new FileStream(@"..\..\ReportsGallery.config", FileMode.Open, FileAccess.Read, FileShare.Read));
			XmlDocument doc = new XmlDocument();
			string xmlIn = reader.ReadToEnd();
			reader.Close();
			doc.LoadXml(xmlIn);
			foreach (XmlNode child in doc.ChildNodes[1].ChildNodes)
				if (child.Name.Equals("Localization"))
					foreach (XmlNode node in child.ChildNodes)
						if (node.Name.Equals("ReplaceName"))
							strReplace.Add
							(
								node.Attributes["OriginalName"].Value,
								node.Attributes["ReplaceWith"].Value
							);

			for (int i = 0; i < treeView.Nodes.Count; i++)
			{
				foreach (DictionaryEntry entry in strReplace)
				{
					if (treeView.Nodes[i].Text.Equals(entry.Key.ToString()))
					{
						treeView.Nodes[i].Text = entry.Value.ToString();
					}
				}
			}
		}

		// Handle Tree View collapse.
		private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 0;
			e.Node.SelectedImageIndex = 0;
		}

		//Handle expansion of Tree View
		private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			e.Node.ImageIndex = 1;
			e.Node.SelectedImageIndex = 1;
		}

		//Handle click on Tree Node
		private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if ((e.Node.Text.ToLower().Contains(".rdlx")) || (e.Node.Text.ToLower().Contains(".rpx")))
			{
				e.Node.ImageIndex = 2;
				treeView.SelectedNode = e.Node;
				FileInfo reportFile = new FileInfo(e.Node.Tag.ToString());
				_reportName = reportFile.FullName;
				reportDesigner.LoadReport(reportFile);
				reportToolbox.PerformLayout();
			}
			else
			{
				if (e.Node.Parent != null)
				{
					if(e.Node.Parent.Parent!=null)
					{
						MessageBox.Show(Properties.Resources.InvalidFileText);
					}
				}
			}
		}
	}
}
