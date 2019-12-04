using GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps;
namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI 
{
	partial class WizardDialog
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
			foreach( BaseStep step in steps )
			{
				step.Dispose();
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardDialog));
			this.stepPanel = new System.Windows.Forms.Panel();
			this.next = new System.Windows.Forms.Button();
			this.back = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.stepDescription = new System.Windows.Forms.Label();
			this.stepTitle = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
	 
			this.SuspendLayout();
			// 
			// stepPanel
			// 
			resources.ApplyResources(this.stepPanel, "stepPanel");
			this.stepPanel.Name = "stepPanel";
			// 
			// next
			// 
			resources.ApplyResources(this.next, "next");
			this.next.Name = "next";
			this.next.UseVisualStyleBackColor = true;
			this.next.Click += new System.EventHandler(this.next_Click);
			// 
			// back
			// 
			resources.ApplyResources(this.back, "back");
			this.back.Name = "back";
			this.back.UseVisualStyleBackColor = true;
			this.back.Click += new System.EventHandler(this.back_Click);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.BackgroundImage = global::GrapeCity.ActiveReports.Samples.ReportWizard.Properties.Resources.Bg_Blue;
			this.panel1.Controls.Add(this.stepDescription);
			this.panel1.Controls.Add(this.stepTitle);
			this.panel1.Name = "panel1";
			// 
			// stepDescription
			// 
			resources.ApplyResources(this.stepDescription, "stepDescription");
			this.stepDescription.Name = "stepDescription";
			this.stepDescription.UseMnemonic = false;
			// 
			// stepTitle
			// 
			resources.ApplyResources(this.stepTitle, "stepTitle");
			this.stepTitle.Name = "stepTitle";
			this.stepTitle.UseMnemonic = false;
			// 
			// WizardDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.back);
			this.Controls.Add(this.next);
			this.Controls.Add(this.stepPanel);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WizardDialog";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
	   
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel stepPanel;
		private System.Windows.Forms.Button next;
		private System.Windows.Forms.Button back;
		private System.Windows.Forms.Label stepDescription;
		private System.Windows.Forms.Label stepTitle;
	}
}
