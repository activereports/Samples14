using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using GrapeCity.ActiveReports.Design;
using System.Collections;
using System.Xml;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using GrapeCity.ActiveReports.Calendar.Properties;

namespace GrapeCity.ActiveReports.Calendar
{
	public partial class DesignerForm : Form
	{
		static readonly string FolderPath = "";
		private string _reportName;

		static DesignerForm()
		{
			XDocument loaded = XDocument.Load("TestDesignerPro.config");
			FolderPath = loaded.Descendants("FolderPath").Select(t => t.Value.ToString()).ToList()[0];
		}

		public DesignerForm()
		{
			InitializeComponent();
			//Populating the ToolBox, ReportExplorer and PropertyGrid.
			reportDesigner.Toolbox = reportToolbox;//Attaches the toolbox to the report designer.
			reportDesigner.LayoutChanged += OnDesignerLayoutChanged;
			reportExplorer.ReportDesigner = reportDesigner;//Attaches the report explorer to the report designer.
			reportDesigner.PropertyGrid = reportPropertyGrid;//Attaches the Property Grid to the report designer.
			groupEditor.ReportDesigner = reportDesigner;
			ToolStrip toolStrip = reportDesigner.CreateToolStrips(DesignerToolStrips.Menu)[0];
			toolStrip.Items.RemoveAt(2);
			ToolStripDropDownItem fileMenu = (ToolStripDropDownItem)toolStrip.Items[0];
			CreateFileMenu(fileMenu);
			AppendToolStrips(0, new ToolStrip[]
				{
					toolStrip
				});
			AppendToolStrips(1, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
				{
					DesignerToolStrips.Edit, 
					DesignerToolStrips.Undo, 
					DesignerToolStrips.Zoom
				}));
			AppendToolStrips(2, reportDesigner.CreateToolStrips(new DesignerToolStrips[]
	            {
					DesignerToolStrips.Format, 
					DesignerToolStrips.Layout
	            }));

			SetReportName(null);

			reportDesigner.ReportChanged += (_, __) => UpdateReportName();
			InitGroupEditorToggle();
		}

		private void SetReportName(string reportName)
		{
			if (string.IsNullOrEmpty(reportName))
			{
				reportDesigner.IsDirty = false;
				_reportName = reportDesigner.Report is PageReport ? Resources.DefaultReportNameRdlx : Resources.DefaultReportNameRpx;
			}
			else
			{
				_reportName = reportName;
			}

			Text = Resources.SampleNameTitle + Path.GetFileName(_reportName) + (reportDesigner.IsDirty ? Resources.DirtySign : string.Empty);
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

			groupEditor.VisibleChanged += (sender, args) => GroupPanelVisibility.SetToolTip(GroupEditorToggleButton,
																										groupEditor.Visible
																											? Resources.HideGroupPanelToolTip
																											: Resources.ShowGroupPanelToolTip);
		}

		private void CreateFileMenu(ToolStripDropDownItem fileMenu)
		{
			fileMenu.DropDownItems.Clear();
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuNew, Resources.CmdNewReport, new EventHandler(OnNew), (Keys.Control | Keys.N)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuOpen, Resources.CmdOpen, new EventHandler(OnOpen), (Keys.Control | Keys.O)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuSave, Resources.CmdSave, new EventHandler(OnSave), (Keys.Control | Keys.S)));
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuSaveAs, Resources.CmdSaveAs, new EventHandler(OnSaveAs)));
			fileMenu.DropDownItems.Add(new ToolStripSeparator());
			fileMenu.DropDownItems.Add(new ToolStripMenuItem(Resources.MenuExit, null, new EventHandler(OnExit)));
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
				openDialog.Filter = Resources.RdlxFilter;
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

		private string GetSaveFilter()
		{
			switch (reportDesigner.ReportType)
			{
				case DesignerReportType.Section:
					return Resources.RpxFilter;
				case DesignerReportType.Page:
				case DesignerReportType.Rdl:
					return Resources.RdlxFilter;
				default:
					return Resources.RpxFilter;
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
				DialogResult result = MessageBox.Show(Resources.SaveConformation, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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

		private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
		{
			ToolStripPanel topToolStripPanel = toolStripContainer.TopToolStripPanel;
			int num = toolStrips.Count;
			while (--num >= 0)
			{
				topToolStripPanel.Join(toolStrips[num], row);
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
					if (e.Node.Parent.Parent != null)
					{
						MessageBox.Show(Properties.Resources.InvalidFileText);
					}
				}
			}
		}

		private void DesignerForm_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(FolderPath))
			{
				var rootDirectoryInfo = new DirectoryInfo(FolderPath);
				treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
			}
			FolderLocalization();
		}

		// Traverse Samples Folder and create tree
		private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			var directoryNode = new TreeNode(Properties.Resources.ReportsNode);
			foreach (var file in directoryInfo.GetFiles("*.rdlx"))
			{
				TreeNode reportFileNode = new TreeNode(file.Name);
				reportFileNode.ImageIndex = 2;
				reportFileNode.SelectedImageIndex = 2;
				reportFileNode.ForeColor = Color.Brown;
				reportFileNode.Tag = file.FullName;
				directoryNode.Nodes.Add(reportFileNode);
			}
			return directoryNode;
		}

		private void FolderLocalization()
		{
			Hashtable strReplace = new Hashtable();
			StreamReader reader = new StreamReader(new FileStream(@"TestDesignerPro.config", FileMode.Open, FileAccess.Read, FileShare.Read));
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
	}
}
