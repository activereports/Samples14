namespace GrapeCity.ActiveReports.Samples.TestDesignerPro
{
	partial class HelperForm
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


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelperForm));
			this.rtfHelp = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtfHelp
			// 
			resources.ApplyResources(this.rtfHelp, "rtfHelp");
			this.rtfHelp.Name = "rtfHelp";
			this.rtfHelp.ReadOnly = true;
			// 
			// HelperForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.rtfHelp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "HelperForm";
			this.TopMost = true;
			this.ResumeLayout(false);

		}


		private System.Windows.Forms.RichTextBox rtfHelp;
	}
}
