using System.Resources;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	partial class SelectOutputFields
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectOutputFields));
			this.label1 = new System.Windows.Forms.Label();
			this.availableOutputFields = new GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox();
			this.addOutputField = new System.Windows.Forms.Button();
			this.removeOutputField = new System.Windows.Forms.Button();
			this.selectedOutputFields = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// availableOutputFields
			// 
			this.availableOutputFields.FormattingEnabled = true;
			resources.ApplyResources(this.availableOutputFields, "availableOutputFields");
			this.availableOutputFields.Name = "availableOutputFields";
			this.availableOutputFields.GetDataObject += new System.EventHandler<GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox.DataObjectEventArgs>(this.availableOutputFields_GetDataObject);
			this.availableOutputFields.GetDragEffects += new System.EventHandler<GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox.DragDropEffectsEventArgs>(this.availableOutputFields_GetDragEffects);
			this.availableOutputFields.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
			// 
			// addOutputField
			// 
			resources.ApplyResources(this.addOutputField, "addOutputField");
			this.addOutputField.Name = "addOutputField";
			this.addOutputField.UseVisualStyleBackColor = true;
			this.addOutputField.Click += new System.EventHandler(this.addOutputField_Click);
			// 
			// removeOutputField
			// 
			resources.ApplyResources(this.removeOutputField, "removeOutputField");
			this.removeOutputField.Name = "removeOutputField";
			this.removeOutputField.UseVisualStyleBackColor = true;
			this.removeOutputField.Click += new System.EventHandler(this.removeOutputField_Click);
			// 
			// selectedOutputFields
			// 
			this.selectedOutputFields.AllowDrop = true;
			this.selectedOutputFields.FormattingEnabled = true;
			resources.ApplyResources(this.selectedOutputFields, "selectedOutputFields");
			this.selectedOutputFields.Name = "selectedOutputFields";
			this.selectedOutputFields.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
			this.selectedOutputFields.DragDrop += new System.Windows.Forms.DragEventHandler(this.selectedOutputFields_DragDrop);
			this.selectedOutputFields.DragEnter += new System.Windows.Forms.DragEventHandler(this.selectedOutputFields_DragEnter);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// SelectOutputFields
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.label2);
			this.Controls.Add(this.selectedOutputFields);
			this.Controls.Add(this.removeOutputField);
			this.Controls.Add(this.addOutputField);
			this.Controls.Add(this.availableOutputFields);
			this.Controls.Add(this.label1);

			this.Description = resources.GetString("Description");
			this.Name = "SelectOutputFields";
			this.Title = resources.GetString("Title");

			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Label label1;
		private DragDropListBox availableOutputFields;
		private System.Windows.Forms.Button addOutputField;
		private System.Windows.Forms.Button removeOutputField;
		private System.Windows.Forms.ListBox selectedOutputFields;
		private System.Windows.Forms.Label label2;
	}
}
