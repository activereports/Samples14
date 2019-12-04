using GrapeCity.ActiveReports.Samples.ReportWizard.UI;
using System.Resources;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	partial class SelectMasterReport
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMasterReport));
			this.label1 = new System.Windows.Forms.Label();
			this.reportList = new System.Windows.Forms.ListBox();
			this.tipControl1 = new GrapeCity.ActiveReports.Samples.ReportWizard.UI.TipControl();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// reportList
			// 
			this.reportList.DisplayMember = "Title";
			this.reportList.FormattingEnabled = true;
			resources.ApplyResources(this.reportList, "reportList");
			this.reportList.Name = "reportList";
			this.reportList.SelectedIndexChanged += new System.EventHandler(this.reportList_SelectedIndexChanged);
			// 
			// tipControl1
			// 
			resources.ApplyResources(this.tipControl1, "tipControl1");
			this.tipControl1.Name = "tipControl1";
			// 
			// SelectMasterReport
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tipControl1);
			this.Controls.Add(this.reportList);
			this.Controls.Add(this.label1);

			this.Description = resources.GetString("Description");
			this.Name = "SelectMasterReport";
			this.Title = resources.GetString("Title");
			
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox reportList;
		private TipControl tipControl1;
	}
}
