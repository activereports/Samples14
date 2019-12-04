namespace ODataDataSource
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.reportPreview = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.reportPreview);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1")});
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// reportPreview
			// 
			this.reportPreview.CurrentPage = 0;
			resources.ApplyResources(this.reportPreview, "reportPreview");
			this.reportPreview.Name = "reportPreview";
			this.reportPreview.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.reportPreview.Sidebar.ParametersPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ParametersPanel.Text = "Parameters";
			this.reportPreview.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.SearchPanel.ContextMenu = null;
			this.reportPreview.Sidebar.SearchPanel.Text = "Search results";
			this.reportPreview.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.reportPreview.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.reportPreview.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.reportPreview.Sidebar.ThumbnailsPanel.Width = 200;
			this.reportPreview.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.reportPreview.Sidebar.TocPanel.ContextMenu = null;
			this.reportPreview.Sidebar.TocPanel.Expanded = true;
			this.reportPreview.Sidebar.TocPanel.Text = "Document map";
			this.reportPreview.Sidebar.TocPanel.Width = 200;
			this.reportPreview.Sidebar.Width = 200;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer reportPreview;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
	}
}
