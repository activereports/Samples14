namespace Export
{
	partial class ExportForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
			this.reportsNames = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.exportTypes = new System.Windows.Forms.ComboBox();
			this.exportButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// reportsNames
			// 
			this.reportsNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.reportsNames.FormattingEnabled = true;
			this.reportsNames.Items.AddRange(new object[] {
            resources.GetString("reportsNames.Items"),
            resources.GetString("reportsNames.Items1")});
			resources.ApplyResources(this.reportsNames, "reportsNames");
			this.reportsNames.Name = "reportsNames";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// exportTypes
			// 
			resources.ApplyResources(this.exportTypes, "exportTypes");
			this.exportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.exportTypes.FormattingEnabled = true;
			this.exportTypes.Items.AddRange(new object[] {
            resources.GetString("exportTypes.Items"),
            resources.GetString("exportTypes.Items1"),
            resources.GetString("exportTypes.Items2"),
            resources.GetString("exportTypes.Items3"),
            resources.GetString("exportTypes.Items4")});
			this.exportTypes.Name = "exportTypes";
			// 
			// exportButton
			// 
			resources.ApplyResources(this.exportButton, "exportButton");
			this.exportButton.Name = "exportButton";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
			// 
			// ExportForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.exportTypes);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.reportsNames);
			this.Name = "ExportForm";
			this.Load += new System.EventHandler(this.ExportForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox reportsNames;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox exportTypes;
		private System.Windows.Forms.Button exportButton;
	}
}
