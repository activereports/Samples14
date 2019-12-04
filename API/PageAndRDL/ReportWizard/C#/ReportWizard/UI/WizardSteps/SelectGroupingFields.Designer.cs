using System.Resources;
using System.Windows.Forms;
namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	partial class SelectGroupingFields
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectGroupingFields));
			this.availableGroupsLabel = new System.Windows.Forms.Label();
			this.addGroup = new System.Windows.Forms.Button();
			this.removeGroup = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.isLastGroupDetail = new System.Windows.Forms.CheckBox();
			this.selectedGroups = new System.Windows.Forms.TreeView();
			this.availableGroups = new GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox();
			this.SuspendLayout();
			// 
			// availableGroupsLabel
			// 
			resources.ApplyResources(this.availableGroupsLabel, "availableGroupsLabel");
			this.availableGroupsLabel.Name = "availableGroupsLabel";
			// 
			// addGroup
			// 
			resources.ApplyResources(this.addGroup, "addGroup");
			this.addGroup.Name = "addGroup";
			this.addGroup.UseVisualStyleBackColor = true;
			this.addGroup.Click += new System.EventHandler(this.addGroup_Click);
			// 
			// removeGroup
			// 
			resources.ApplyResources(this.removeGroup, "removeGroup");
			this.removeGroup.Name = "removeGroup";
			this.removeGroup.UseVisualStyleBackColor = true;
			this.removeGroup.Click += new System.EventHandler(this.removeGroup_Click);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// isLastGroupDetail
			// 
			resources.ApplyResources(this.isLastGroupDetail, "isLastGroupDetail");
			this.isLastGroupDetail.Name = "isLastGroupDetail";
			this.isLastGroupDetail.UseVisualStyleBackColor = true;
			// 
			// selectedGroups
			// 
			this.selectedGroups.AllowDrop = true;
			resources.ApplyResources(this.selectedGroups, "selectedGroups");
			this.selectedGroups.Name = "selectedGroups";
			this.selectedGroups.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.selectedGroups_ItemDrag);
			this.selectedGroups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.selectedGroups_AfterSelect);
			this.selectedGroups.DragDrop += new System.Windows.Forms.DragEventHandler(this.selectedGroups_DragDrop);
			this.selectedGroups.DragEnter += new System.Windows.Forms.DragEventHandler(this.selectedGroups_DragEnter);
			this.selectedGroups.DragOver += new System.Windows.Forms.DragEventHandler(this.selectedGroups_DragOver);
			this.selectedGroups.DragLeave += new System.EventHandler(this.selectedGroups_DragLeave);
			// 
			// availableGroups
			// 
			resources.ApplyResources(this.availableGroups, "availableGroups");
			this.availableGroups.Name = "availableGroups";
			this.availableGroups.GetDataObject += new System.EventHandler<GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox.DataObjectEventArgs>(this.availableGroups_GetDataObject);
			this.availableGroups.GetDragEffects += new System.EventHandler<GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox.DragDropEffectsEventArgs>(this.availableGroups_GetDragEffects);
			// 
			// SelectGroupingFields
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.availableGroups);
			this.Controls.Add(this.selectedGroups);
			this.Controls.Add(this.isLastGroupDetail);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.removeGroup);
			this.Controls.Add(this.addGroup);
			this.Controls.Add(this.availableGroupsLabel);

			this.Description = resources.GetString("Description");
			this.Name = "SelectGroupingFields";
			this.Title = resources.GetString("Title");

			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Label availableGroupsLabel;
		private System.Windows.Forms.Button addGroup;
		private System.Windows.Forms.Button removeGroup;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox isLastGroupDetail;
		private System.Windows.Forms.TreeView selectedGroups;
		private GrapeCity.ActiveReports.Samples.ReportWizard.UI.DragDropListBox availableGroups;
	}
}
