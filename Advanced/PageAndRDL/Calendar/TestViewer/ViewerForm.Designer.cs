namespace TestViewer
{
	partial class ViewerForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.viewer1);
			this.splitContainer1.Size = new System.Drawing.Size(849, 450);
			this.splitContainer1.SplitterDistance = 37;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.comboBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(849, 37);
			this.panel1.TabIndex = 2;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(107, 7);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(242, 24);
			this.comboBox1.TabIndex = 2;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select Report:";
			// 
			// viewer1
			// 
			this.viewer1.CurrentPage = 0;
			this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewer1.Location = new System.Drawing.Point(0, 0);
			this.viewer1.Name = "viewer1";
			this.viewer1.PreviewPages = 0;
			// 
			// 
			// 
			// 
			// 
			// 
			this.viewer1.Sidebar.ParametersPanel.ContextMenu = null;
			this.viewer1.Sidebar.ParametersPanel.Text = "Parameters";
			this.viewer1.Sidebar.ParametersPanel.Width = 200;
			// 
			// 
			// 
			this.viewer1.Sidebar.SearchPanel.ContextMenu = null;
			this.viewer1.Sidebar.SearchPanel.Text = "Search results";
			this.viewer1.Sidebar.SearchPanel.Width = 200;
			// 
			// 
			// 
			this.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = null;
			this.viewer1.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
			this.viewer1.Sidebar.ThumbnailsPanel.Width = 200;
			this.viewer1.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
			// 
			// 
			// 
			this.viewer1.Sidebar.TocPanel.ContextMenu = null;
			this.viewer1.Sidebar.TocPanel.Expanded = true;
			this.viewer1.Sidebar.TocPanel.Text = "Document map";
			this.viewer1.Sidebar.TocPanel.Width = 200;
			this.viewer1.Sidebar.Width = 200;
			this.viewer1.Size = new System.Drawing.Size(849, 409);
			this.viewer1.TabIndex = 3;
			// 
			// ViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(849, 450);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ViewerForm";
			this.Text = "Calendar";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
	}
}
