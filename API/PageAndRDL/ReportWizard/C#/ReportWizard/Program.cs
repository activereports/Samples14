using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;
using GrapeCity.ActiveReports.Samples.ReportWizard.UI;
using GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps;

namespace GrapeCity.ActiveReports.Samples.ReportWizard
{
	static class Program
	{
		private static List<ReportMetaData> availableReports;

		public static IEnumerable<ReportMetaData> AvailableReportTemplates
		{
			get { return availableReports; }
		}

		private static ReportWizardState state;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SetupTemplates();
			WizardDialog wizard = CreateWizard();
			Application.Run(wizard);
			if (wizard.DialogResult == DialogResult.OK)
			{
				PageReport def = LayoutBuilder.BuildReport(state);
				ReportsForm frm = new ReportsForm(def);
				Application.Run(frm);
			}
		}

		private static void SetupTemplates()
		{
			availableReports = new List<ReportMetaData>();
			XmlDocument doc = new XmlDocument();
			doc.Load( "Reports.xml" );
			foreach( XmlNode reportNode in doc.SelectNodes( "//reports/report" ))
			{
				availableReports.Add( LoadReportMetaData( reportNode ) );
			}
		}

		private static ReportMetaData LoadReportMetaData( XmlNode node )
		{
			ReportMetaData data = new ReportMetaData();
			data.Title = node.Attributes["title"].Value;
			data.MasterReportFile = node.Attributes["filename"].Value;
			XmlNodeList fields = node.SelectNodes( "fields/field" );
			foreach( XmlNode fieldNode in fields )
			{
				FieldMetaData field = LoadFieldMetaData( fieldNode );
				data.Fields.Add( field.Name, field );
			}
			return data;
		}

		private static FieldMetaData LoadFieldMetaData( XmlNode node )
		{
			FieldMetaData data = new FieldMetaData();
			data.Name = node.Attributes["name"].Value;
			data.Title = node.Attributes["title"].Value;
			data.PreferredWidth = node.Attributes["width"].Value;
			data.IsNumeric = node.Attributes["datatype"].Value == "number";
			XmlAttribute attr = node.Attributes["format"];
			if( attr != null )
				data.FormatString = attr.Value;
			attr = node.Attributes["summarizable"];
			if( attr != null )
				data.AllowTotaling = Convert.ToBoolean( attr.Value );
			attr = node.Attributes["summaryFunction"];
			if (attr != null)
				data.SummaryFunction = attr.Value;
			return data;
		}

		private static WizardDialog CreateWizard()
		{
			state = new ReportWizardState();
			state.AvailableMasterReports.AddRange( Program.AvailableReportTemplates );
			WizardDialog dlg = new WizardDialog( state );
			dlg.Steps.Add( new SelectMasterReport() );
			dlg.Steps.Add( new SelectGroupingFields() );
			dlg.Steps.Add( new SelectOutputFields() );
			dlg.Steps.Add( new SelectSummaryOptions() );
			return dlg;
		}
	}
}
