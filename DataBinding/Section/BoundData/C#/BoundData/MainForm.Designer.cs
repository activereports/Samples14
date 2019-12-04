using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.BoundData
{
	public partial class MainForm
	{
		private System.Windows.Forms.Panel pnlOptions;
private GrapeCity.ActiveReports.Viewer.Win.Viewer arvMain;
private System.Windows.Forms.TabControl tabDataBinding;
private System.Windows.Forms.TabPage tabDataSet;
private System.Windows.Forms.TabPage tabDataTable;
private System.Windows.Forms.TabPage tabDataView;
private System.Windows.Forms.TabPage tabDataReader;
private System.Windows.Forms.TabPage tabSqlServer;
private System.Windows.Forms.TabPage tabOleDb;
private System.Windows.Forms.TabPage tabXML;
private System.Windows.Forms.Label lblDataSet;
private System.Windows.Forms.Button btnDataTable;
private System.Windows.Forms.Label lblDataTable;
private System.Windows.Forms.Button btnDataView;
private System.Windows.Forms.Label lblDataViewOption;
private System.Windows.Forms.ComboBox cbCompanyName;
private System.Windows.Forms.Label lblDataView;
private System.Windows.Forms.Label lblDataReader;
private System.Windows.Forms.Button btnDataReader;
private System.Windows.Forms.Label lblOleDb;
private System.Windows.Forms.Button btnOleDb;
private System.Windows.Forms.ComboBox cbSqlServerList;
private System.Windows.Forms.Label lblSqlServerList;
private System.Windows.Forms.Label lblSqlServer;
private System.Windows.Forms.Button btnSqlServer;
private System.Windows.Forms.Label lblXML;
private System.Windows.Forms.Button btnGenerateXML;
private System.Windows.Forms.SaveFileDialog dlgSave;
private System.Windows.Forms.OpenFileDialog dlgOpen;
private System.Windows.Forms.Button btnXML;
private System.Windows.Forms.Button btnDataSet;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pnlOptions = new System.Windows.Forms.Panel();
			this.tabDataBinding = new System.Windows.Forms.TabControl();
			this.tabDataSet = new System.Windows.Forms.TabPage();
			this.btnDataSet = new System.Windows.Forms.Button();
			this.lblDataSet = new System.Windows.Forms.Label();
			this.tabDataReader = new System.Windows.Forms.TabPage();
			this.btnDataReader = new System.Windows.Forms.Button();
			this.lblDataReader = new System.Windows.Forms.Label();
			this.tabDataView = new System.Windows.Forms.TabPage();
			this.cbCompanyName = new System.Windows.Forms.ComboBox();
			this.lblDataViewOption = new System.Windows.Forms.Label();
			this.btnDataView = new System.Windows.Forms.Button();
			this.lblDataView = new System.Windows.Forms.Label();
			this.tabDataTable = new System.Windows.Forms.TabPage();
			this.lblDataTable = new System.Windows.Forms.Label();
			this.btnDataTable = new System.Windows.Forms.Button();
			this.tabSqlServer = new System.Windows.Forms.TabPage();
			this.btnSqlServer = new System.Windows.Forms.Button();
			this.lblSqlServerList = new System.Windows.Forms.Label();
			this.cbSqlServerList = new System.Windows.Forms.ComboBox();
			this.lblSqlServer = new System.Windows.Forms.Label();
			this.tabOleDb = new System.Windows.Forms.TabPage();
			this.btnOleDb = new System.Windows.Forms.Button();
			this.lblOleDb = new System.Windows.Forms.Label();
			this.tabXML = new System.Windows.Forms.TabPage();
			this.btnXML = new System.Windows.Forms.Button();
			this.btnGenerateXML = new System.Windows.Forms.Button();
			this.lblXML = new System.Windows.Forms.Label();
			this.tabCSV = new System.Windows.Forms.TabPage();
			this.btnCSV = new System.Windows.Forms.Button();
			this.rbtnHeader = new System.Windows.Forms.RadioButton();
			this.rbtnNoHeader = new System.Windows.Forms.RadioButton();
			this.rbtnHeaderTab = new System.Windows.Forms.RadioButton();
			this.rbtnNoHeaderComma = new System.Windows.Forms.RadioButton();
			this.lblFixWData = new System.Windows.Forms.Label();
			this.lblCSVDelData = new System.Windows.Forms.Label();
			this.lblCSV = new System.Windows.Forms.Label();
			this.arvMain = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			this.dlgSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
			this.pnlOptions.SuspendLayout();
			this.tabDataBinding.SuspendLayout();
			this.tabDataSet.SuspendLayout();
			this.tabDataReader.SuspendLayout();
			this.tabDataView.SuspendLayout();
			this.tabDataTable.SuspendLayout();
			this.tabSqlServer.SuspendLayout();
			this.tabOleDb.SuspendLayout();
			this.tabXML.SuspendLayout();
			this.tabCSV.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlOptions
			// 
			this.pnlOptions.Controls.Add(this.tabDataBinding);
			resources.ApplyResources(this.pnlOptions, "pnlOptions");
			this.pnlOptions.Name = "pnlOptions";
			// 
			// tabDataBinding
			// 
			this.tabDataBinding.Controls.Add(this.tabDataSet);
			this.tabDataBinding.Controls.Add(this.tabDataReader);
			this.tabDataBinding.Controls.Add(this.tabDataView);
			this.tabDataBinding.Controls.Add(this.tabDataTable);
			this.tabDataBinding.Controls.Add(this.tabSqlServer);
			this.tabDataBinding.Controls.Add(this.tabOleDb);
			this.tabDataBinding.Controls.Add(this.tabXML);
			this.tabDataBinding.Controls.Add(this.tabCSV);
			resources.ApplyResources(this.tabDataBinding, "tabDataBinding");
			this.tabDataBinding.Name = "tabDataBinding";
			this.tabDataBinding.SelectedIndex = 0;
			this.tabDataBinding.SelectedIndexChanged += new System.EventHandler(this.tabDataBinding_SelectedIndexChanged);
			// 
			// tabDataSet
			// 
			this.tabDataSet.Controls.Add(this.btnDataSet);
			this.tabDataSet.Controls.Add(this.lblDataSet);
			resources.ApplyResources(this.tabDataSet, "tabDataSet");
			this.tabDataSet.Name = "tabDataSet";
			// 
			// btnDataSet
			// 
			resources.ApplyResources(this.btnDataSet, "btnDataSet");
			this.btnDataSet.Name = "btnDataSet";
			this.btnDataSet.Click += new System.EventHandler(this.btnDataSet_Click);
			// 
			// lblDataSet
			// 
			resources.ApplyResources(this.lblDataSet, "lblDataSet");
			this.lblDataSet.Name = "lblDataSet";
			// 
			// tabDataReader
			// 
			this.tabDataReader.Controls.Add(this.btnDataReader);
			this.tabDataReader.Controls.Add(this.lblDataReader);
			resources.ApplyResources(this.tabDataReader, "tabDataReader");
			this.tabDataReader.Name = "tabDataReader";
			// 
			// btnDataReader
			// 
			resources.ApplyResources(this.btnDataReader, "btnDataReader");
			this.btnDataReader.Name = "btnDataReader";
			this.btnDataReader.Click += new System.EventHandler(this.btnDataReader_Click);
			// 
			// lblDataReader
			// 
			resources.ApplyResources(this.lblDataReader, "lblDataReader");
			this.lblDataReader.Name = "lblDataReader";
			// 
			// tabDataView
			// 
			this.tabDataView.Controls.Add(this.cbCompanyName);
			this.tabDataView.Controls.Add(this.lblDataViewOption);
			this.tabDataView.Controls.Add(this.btnDataView);
			this.tabDataView.Controls.Add(this.lblDataView);
			resources.ApplyResources(this.tabDataView, "tabDataView");
			this.tabDataView.Name = "tabDataView";
			// 
			// cbCompanyName
			// 
			resources.ApplyResources(this.cbCompanyName, "cbCompanyName");
			this.cbCompanyName.Name = "cbCompanyName";
			this.cbCompanyName.DropDown += new System.EventHandler(this.cbCompanyName_DropDown);
			// 
			// lblDataViewOption
			// 
			resources.ApplyResources(this.lblDataViewOption, "lblDataViewOption");
			this.lblDataViewOption.Name = "lblDataViewOption";
			// 
			// btnDataView
			// 
			resources.ApplyResources(this.btnDataView, "btnDataView");
			this.btnDataView.Name = "btnDataView";
			this.btnDataView.Click += new System.EventHandler(this.btnDataView_Click);
			// 
			// lblDataView
			// 
			resources.ApplyResources(this.lblDataView, "lblDataView");
			this.lblDataView.Name = "lblDataView";
			// 
			// tabDataTable
			// 
			this.tabDataTable.Controls.Add(this.lblDataTable);
			this.tabDataTable.Controls.Add(this.btnDataTable);
			resources.ApplyResources(this.tabDataTable, "tabDataTable");
			this.tabDataTable.Name = "tabDataTable";
			// 
			// lblDataTable
			// 
			resources.ApplyResources(this.lblDataTable, "lblDataTable");
			this.lblDataTable.Name = "lblDataTable";
			// 
			// btnDataTable
			// 
			resources.ApplyResources(this.btnDataTable, "btnDataTable");
			this.btnDataTable.Name = "btnDataTable";
			this.btnDataTable.Click += new System.EventHandler(this.btnDataTable_Click);
			// 
			// tabSqlServer
			// 
			this.tabSqlServer.Controls.Add(this.btnSqlServer);
			this.tabSqlServer.Controls.Add(this.lblSqlServerList);
			this.tabSqlServer.Controls.Add(this.cbSqlServerList);
			this.tabSqlServer.Controls.Add(this.lblSqlServer);
			resources.ApplyResources(this.tabSqlServer, "tabSqlServer");
			this.tabSqlServer.Name = "tabSqlServer";
			// 
			// btnSqlServer
			// 
			resources.ApplyResources(this.btnSqlServer, "btnSqlServer");
			this.btnSqlServer.Name = "btnSqlServer";
			this.btnSqlServer.Click += new System.EventHandler(this.btnSqlServer_Click);
			// 
			// lblSqlServerList
			// 
			resources.ApplyResources(this.lblSqlServerList, "lblSqlServerList");
			this.lblSqlServerList.Name = "lblSqlServerList";
			// 
			// cbSqlServerList
			// 
			resources.ApplyResources(this.cbSqlServerList, "cbSqlServerList");
			this.cbSqlServerList.Name = "cbSqlServerList";
			this.cbSqlServerList.DropDown += new System.EventHandler(this.cbSqlServerList_DropDown);
			// 
			// lblSqlServer
			// 
			resources.ApplyResources(this.lblSqlServer, "lblSqlServer");
			this.lblSqlServer.Name = "lblSqlServer";
			// 
			// tabOleDb
			// 
			this.tabOleDb.Controls.Add(this.btnOleDb);
			this.tabOleDb.Controls.Add(this.lblOleDb);
			resources.ApplyResources(this.tabOleDb, "tabOleDb");
			this.tabOleDb.Name = "tabOleDb";
			// 
			// btnOleDb
			// 
			resources.ApplyResources(this.btnOleDb, "btnOleDb");
			this.btnOleDb.Name = "btnOleDb";
			this.btnOleDb.Click += new System.EventHandler(this.btnOleDb_Click);
			// 
			// lblOleDb
			// 
			resources.ApplyResources(this.lblOleDb, "lblOleDb");
			this.lblOleDb.Name = "lblOleDb";
			// 
			// tabXML
			// 
			this.tabXML.Controls.Add(this.btnXML);
			this.tabXML.Controls.Add(this.btnGenerateXML);
			this.tabXML.Controls.Add(this.lblXML);
			resources.ApplyResources(this.tabXML, "tabXML");
			this.tabXML.Name = "tabXML";
			// 
			// btnXML
			// 
			resources.ApplyResources(this.btnXML, "btnXML");
			this.btnXML.Name = "btnXML";
			this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
			// 
			// btnGenerateXML
			// 
			resources.ApplyResources(this.btnGenerateXML, "btnGenerateXML");
			this.btnGenerateXML.Name = "btnGenerateXML";
			this.btnGenerateXML.Click += new System.EventHandler(this.btnGenerateXML_Click);
			// 
			// lblXML
			// 
			resources.ApplyResources(this.lblXML, "lblXML");
			this.lblXML.Name = "lblXML";
			// 
			// tabCSV
			// 
			this.tabCSV.BackColor = System.Drawing.SystemColors.Control;
			this.tabCSV.Controls.Add(this.btnCSV);
			this.tabCSV.Controls.Add(this.rbtnHeader);
			this.tabCSV.Controls.Add(this.rbtnNoHeader);
			this.tabCSV.Controls.Add(this.rbtnHeaderTab);
			this.tabCSV.Controls.Add(this.rbtnNoHeaderComma);
			this.tabCSV.Controls.Add(this.lblFixWData);
			this.tabCSV.Controls.Add(this.lblCSVDelData);
			this.tabCSV.Controls.Add(this.lblCSV);
			resources.ApplyResources(this.tabCSV, "tabCSV");
			this.tabCSV.Name = "tabCSV";
			// 
			// btnCSV
			// 
			resources.ApplyResources(this.btnCSV, "btnCSV");
			this.btnCSV.Name = "btnCSV";
			this.btnCSV.UseVisualStyleBackColor = true;
			this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
			// 
			// rbtnHeader
			// 
			resources.ApplyResources(this.rbtnHeader, "rbtnHeader");
			this.rbtnHeader.Name = "rbtnHeader";
			this.rbtnHeader.UseVisualStyleBackColor = true;
			// 
			// rbtnNoHeader
			// 
			resources.ApplyResources(this.rbtnNoHeader, "rbtnNoHeader");
			this.rbtnNoHeader.Name = "rbtnNoHeader";
			this.rbtnNoHeader.UseVisualStyleBackColor = true;
			// 
			// rbtnHeaderTab
			// 
			resources.ApplyResources(this.rbtnHeaderTab, "rbtnHeaderTab");
			this.rbtnHeaderTab.Name = "rbtnHeaderTab";
			this.rbtnHeaderTab.UseVisualStyleBackColor = true;
			// 
			// rbtnNoHeaderComma
			// 
			resources.ApplyResources(this.rbtnNoHeaderComma, "rbtnNoHeaderComma");
			this.rbtnNoHeaderComma.Checked = true;
			this.rbtnNoHeaderComma.Name = "rbtnNoHeaderComma";
			this.rbtnNoHeaderComma.TabStop = true;
			this.rbtnNoHeaderComma.UseVisualStyleBackColor = true;
			// 
			// lblFixWData
			// 
			resources.ApplyResources(this.lblFixWData, "lblFixWData");
			this.lblFixWData.Name = "lblFixWData";
			// 
			// lblCSVDelData
			// 
			resources.ApplyResources(this.lblCSVDelData, "lblCSVDelData");
			this.lblCSVDelData.Name = "lblCSVDelData";
			// 
			// lblCSV
			// 
			resources.ApplyResources(this.lblCSV, "lblCSV");
			this.lblCSV.Name = "lblCSV";
			// 
			// arvMain
			// 
			this.arvMain.BackColor = System.Drawing.SystemColors.Control;
			this.arvMain.CurrentPage = 0;
			resources.ApplyResources(this.arvMain, "arvMain");
			this.arvMain.Name = "arvMain";
			this.arvMain.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.arvMain.Sidebar.ParametersPanel.ContextMenu = null;
			this.arvMain.Sidebar.ParametersPanel.Enabled = false;
			this.arvMain.Sidebar.ParametersPanel.Visible = false;
			this.arvMain.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.SearchPanel.ContextMenu = null;
			this.arvMain.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.arvMain.Sidebar.ThumbnailsPanel.Width = 200;
			this.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.arvMain.Sidebar.TocPanel.ContextMenu = null;
			this.arvMain.Sidebar.TocPanel.Enabled = false;
			this.arvMain.Sidebar.TocPanel.Expanded = true;
			this.arvMain.Sidebar.TocPanel.Visible = false;
			this.arvMain.Sidebar.TocPanel.Width = 200;
			this.arvMain.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.arvMain);
			this.Controls.Add(this.pnlOptions);
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.pnlOptions.ResumeLayout(false);
			this.tabDataBinding.ResumeLayout(false);
			this.tabDataSet.ResumeLayout(false);
			this.tabDataReader.ResumeLayout(false);
			this.tabDataView.ResumeLayout(false);
			this.tabDataTable.ResumeLayout(false);
			this.tabSqlServer.ResumeLayout(false);
			this.tabOleDb.ResumeLayout(false);
			this.tabXML.ResumeLayout(false);
			this.tabCSV.ResumeLayout(false);
			this.tabCSV.PerformLayout();
			this.ResumeLayout(false);

		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private TabPage tabCSV;
		private Button btnCSV;
		private RadioButton rbtnHeader;
		private RadioButton rbtnNoHeader;
		private RadioButton rbtnHeaderTab;
		private RadioButton rbtnNoHeaderComma;
		private Label lblFixWData;
		private Label lblCSVDelData;
		private Label lblCSV;
	}
}
