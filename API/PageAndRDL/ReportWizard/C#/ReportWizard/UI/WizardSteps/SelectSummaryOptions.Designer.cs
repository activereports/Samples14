namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	partial class SelectSummaryOptions
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSummaryOptions));
			this.label1 = new System.Windows.Forms.Label();
			this.selectedGroups = new System.Windows.Forms.ListBox();
			this.outputFields = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.masterReport = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.grandTotal = new System.Windows.Forms.CheckBox();
			this.subtotals = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// selectedGroups
			// 
			resources.ApplyResources(this.selectedGroups, "selectedGroups");
			this.selectedGroups.FormattingEnabled = true;
			this.selectedGroups.Name = "selectedGroups";
			this.selectedGroups.SelectionMode = System.Windows.Forms.SelectionMode.None;
			// 
			// outputFields
			// 
			resources.ApplyResources(this.outputFields, "outputFields");
			this.outputFields.FormattingEnabled = true;
			this.outputFields.Name = "outputFields";
			this.outputFields.SelectionMode = System.Windows.Forms.SelectionMode.None;
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// masterReport
			// 
			resources.ApplyResources(this.masterReport, "masterReport");
			this.masterReport.Name = "masterReport";
			this.masterReport.ReadOnly = true;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// grandTotal
			// 
			resources.ApplyResources(this.grandTotal, "grandTotal");
			this.grandTotal.Name = "grandTotal";
			this.grandTotal.UseVisualStyleBackColor = true;
			// 
			// subtotals
			// 
			resources.ApplyResources(this.subtotals, "subtotals");
			this.subtotals.Name = "subtotals";
			this.subtotals.UseVisualStyleBackColor = true;
			// 
			// SelectSummaryOptions
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.subtotals);
			this.Controls.Add(this.grandTotal);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.masterReport);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.outputFields);
			this.Controls.Add(this.selectedGroups);
			this.Controls.Add(this.label1);
			this.Description = resources.GetString("Description");
			this.Name = "SelectSummaryOptions";
			this.Title = resources.GetString("Title");

			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox selectedGroups;
		private System.Windows.Forms.ListBox outputFields;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox masterReport;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox grandTotal;
		private System.Windows.Forms.CheckBox subtotals;
	}
}
