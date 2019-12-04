using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.XML
{
	public partial class StartForm
	{
	  
	  
		private System.Windows.Forms.Button btnCustomers;
private System.Windows.Forms.RadioButton radioAll;
private System.Windows.Forms.RadioButton radioID;
private System.Windows.Forms.RadioButton radioEmail;
private System.Windows.Forms.Label lblDescription;
private System.Windows.Forms.GroupBox CustomersOrdersGroup;
private System.Windows.Forms.Label lblDescriptionLeveled;
private System.Windows.Forms.Button btnCustomersLeveled;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
			this.btnCustomers = new System.Windows.Forms.Button();
			this.lblDescription = new System.Windows.Forms.Label();
			this.CustomersOrdersGroup = new System.Windows.Forms.GroupBox();
			this.radioEmail = new System.Windows.Forms.RadioButton();
			this.radioID = new System.Windows.Forms.RadioButton();
			this.radioAll = new System.Windows.Forms.RadioButton();
			this.btnCustomersLeveled = new System.Windows.Forms.Button();
			this.lblDescriptionLeveled = new System.Windows.Forms.Label();
			this.CustomersOrdersGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCustomers
			// 
			resources.ApplyResources(this.btnCustomers, "btnCustomers");
			this.btnCustomers.Name = "btnCustomers";
			this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
			// 
			// lblDescription
			// 
			resources.ApplyResources(this.lblDescription, "lblDescription");
			this.lblDescription.Name = "lblDescription";
			// 
			// CustomersOrdersGroup
			// 
			this.CustomersOrdersGroup.Controls.Add(this.radioEmail);
			this.CustomersOrdersGroup.Controls.Add(this.radioID);
			this.CustomersOrdersGroup.Controls.Add(this.radioAll);
			this.CustomersOrdersGroup.Controls.Add(this.lblDescription);
			this.CustomersOrdersGroup.Controls.Add(this.btnCustomers);
			resources.ApplyResources(this.CustomersOrdersGroup, "CustomersOrdersGroup");
			this.CustomersOrdersGroup.Name = "CustomersOrdersGroup";
			this.CustomersOrdersGroup.TabStop = false;
			// 
			// radioEmail
			// 
			resources.ApplyResources(this.radioEmail, "radioEmail");
			this.radioEmail.Name = "radioEmail";
			// 
			// radioID
			// 
			resources.ApplyResources(this.radioID, "radioID");
			this.radioID.Name = "radioID";
			// 
			// radioAll
			// 
			this.radioAll.Checked = true;
			resources.ApplyResources(this.radioAll, "radioAll");
			this.radioAll.Name = "radioAll";
			this.radioAll.TabStop = true;
			// 
			// btnCustomersLeveled
			// 
			resources.ApplyResources(this.btnCustomersLeveled, "btnCustomersLeveled");
			this.btnCustomersLeveled.Name = "btnCustomersLeveled";
			this.btnCustomersLeveled.Click += new System.EventHandler(this.btnCustomersLeveled_Click);
			// 
			// lblDescriptionLeveled
			// 
			resources.ApplyResources(this.lblDescriptionLeveled, "lblDescriptionLeveled");
			this.lblDescriptionLeveled.Name = "lblDescriptionLeveled";
			// 
			// StartForm
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.lblDescriptionLeveled);
			this.Controls.Add(this.btnCustomersLeveled);
			this.Controls.Add(this.CustomersOrdersGroup);
			this.Name = "StartForm";
			this.CustomersOrdersGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	}
}
