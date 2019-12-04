using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Represents the data page of the calendar smart panel.
	/// </summary>
	internal sealed class CalendarDataPage : CalendarPageBase, ILayoutElement
	{
		private Control _bottomControl;

		public CalendarDataPage(CalendarDesigner designer) : base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this, true))
			{
				DataSetNameEditor dataSetEditor = new DataSetNameEditor(ServiceProvider, Designer.ReportItem);

				ControlGroupHeadingLabel eventSettingsLabel = new ControlGroupHeadingLabel(ServiceProvider);
				eventSettingsLabel.Text = Resources.CalendarSmartPanelEventSettingsLabel;
				eventSettingsLabel.Dock = DockStyle.Top;

				TypedValueEditor startDateEditor = new TypedValueEditor(ServiceProvider, Resources.CalendarSmartPanelStartDateLabel, Designer.ReportItem, "AppointmentStartDate");
				TypedValueEditor endDateEditor = new TypedValueEditor(ServiceProvider, Resources.CalendarSmartPanelEndDateLabel, Designer.ReportItem, "AppointmentEndDate");
				TypedValueEditor valueEditor = new TypedValueEditor(ServiceProvider, Resources.CalendarSmartPanelValueLabel, Designer.ReportItem, "AppointmentValue");

				_bottomControl = valueEditor;

				Controls.AddRange(new Control[]
									  {
										  dataSetEditor, eventSettingsLabel, startDateEditor, endDateEditor, valueEditor
									  });
			}
		}

		public Size GetDesiredSize(Size availableSize)
		{
			return new Size(availableSize.Width, _bottomControl.Bottom);
		}

		#region DataSetNameEditor class

		/// <summary>
		/// A composite editor for a DataSet name of a data region in the task pane wizard UI.
		/// </summary>
		private sealed class DataSetNameEditor : VerticalPanel
		{
			private LabelEx lblDataSetName;
			private ComboBoxEx cbDataSetName;
			private readonly IServiceProvider _serviceProvider;
			private readonly CustomReportItem _reportItem;

			#region Initialization & Cleanup

			/// <summary>
			/// Initializes a new <see cref="DataSetNameEditor"/>.
			/// </summary>
			public DataSetNameEditor(IServiceProvider serviceProvider, CustomReportItem reportItem)
			{
				if (serviceProvider == null)
					throw new ArgumentNullException("serviceProvider");
				if (reportItem == null)
					throw new ArgumentNullException("reportItem");
				_serviceProvider = serviceProvider;
				_reportItem = reportItem;
				InitializeComponent();
				InitializeDataSetsCombo();
			}

			private void InitializeComponent()
			{
				using (new SuspendLayoutTransaction(this))
				{
					AutoSize = true;

					// 
					// lblDataSetName
					// 
					lblDataSetName = new LabelEx(_serviceProvider);
					lblDataSetName.Dock = DockStyle.Top;
					lblDataSetName.Text = Resources.CalendarSmartPanelDatasetNameLabel;
					// 
					// cbDataSetName
					// 
					cbDataSetName = new ComboBoxEx(_serviceProvider);
					cbDataSetName.Dock = DockStyle.Fill;
					cbDataSetName.Validated += cbDataSetName_Validated;
					cbDataSetName.SelectedIndexChanged += cbDataSetName_SelectedIndexChanged;
					// 
					// DataSetNameEditor
					// 
					Controls.Add(lblDataSetName);
					Controls.Add(cbDataSetName);
				}
			}

			/// <summary>
			/// Initializes the dataset combo to have the list of datasets from the report and the proper one will be selected.
			/// </summary>
			private void InitializeDataSetsCombo()
			{
				string dsName = _reportItem.CustomData.DataSetName;
				PageReport reportDef = GetCurrentReport();

				using (new SuspendLayoutTransaction(cbDataSetName))
				{
					if (reportDef != null && reportDef.Report != null && reportDef.Report.DataSets != null)
					{
						for (int index = 0; index < reportDef.Report.DataSets.Count; index++)
						{
							IDataSet ds = reportDef.Report.DataSets[index];
							cbDataSetName.Items.Add(ds.Name);
						}
					}
					cbDataSetName.Text = dsName ?? string.Empty;
				}
			}

			#endregion

			private void cbDataSetName_Validated(object sender, EventArgs e)
			{
				SetDataSetNameOnChart();
			}

			private void cbDataSetName_SelectedIndexChanged(object sender, EventArgs e)
			{
				SetDataSetNameOnChart();
			}

			private void SetDataSetNameOnChart()
			{
				_reportItem.CustomData.DataSetName = cbDataSetName.Text;
			}

			/// <summary>
			/// Returns the Current PageReport or null if it cannot be found (no report is open? Designer is in a foobared state?)
			/// </summary>
			private PageReport GetCurrentReport()
			{
				IDesignerHost host = _serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (host == null)
				{
					Debug.Fail("DesignerHost not available.");
					return null;
				}
				PageReport report = host.RootComponent as PageReport;
				if (report == null)
				{
					Debug.Fail("RootComponent not PageReport");
					return null;
				}
				return report;
			}
		}

		#endregion
	}
}
