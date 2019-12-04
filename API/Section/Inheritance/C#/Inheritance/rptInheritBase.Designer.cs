using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Sample.Inheritance
{
	public partial class rptInheritBase
	{
		#region ActiveReports Designer generated code
	   
		
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptInheritBase));
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// rptInheritBase
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
						"l; font-size: 10pt; color: Black; ddo-char-set: 186", "Normal"));
		   
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}
	}
}
