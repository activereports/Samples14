namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI
{
	partial class TipControl
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
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipControl));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tipText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::GrapeCity.ActiveReports.Samples.ReportWizard.Properties.Resources.Info_02;
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			// 
			// tipText
			// 
			resources.ApplyResources(this.tipText, "tipText");
			this.tipText.Name = "tipText";
			// 
			// TipControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tipText);
			this.Controls.Add(this.pictureBox1);
			this.Name = "TipControl";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label tipText;
	}
}
