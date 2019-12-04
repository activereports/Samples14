namespace GrapeCity.ActiveReports.Samples.CustomResourceLocator
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
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false. </param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

	
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.listView = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.showReport = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			resources.ApplyResources(this.richTextBox1, "richTextBox1");
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			// 
			// listView
			// 
			resources.ApplyResources(this.listView, "listView");
			this.listView.HideSelection = false;
			this.listView.LargeImageList = this.imageList1;
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			resources.ApplyResources(this.imageList1, "imageList1");
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// showReport
			// 
			resources.ApplyResources(this.showReport, "showReport");
			this.showReport.Name = "showReport";
			this.showReport.UseVisualStyleBackColor = true;
			this.showReport.Click += new System.EventHandler(this.showReport_Click);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.showReport);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.richTextBox1);
			this.Name = "MainForm";
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.Button showReport;
		private System.Windows.Forms.ImageList imageList1;
	}
}
