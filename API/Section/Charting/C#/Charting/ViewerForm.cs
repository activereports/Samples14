using GrapeCity.ActiveReports.SectionReportModel;
using System;
using System.Windows.Forms;
using System.Xml;

namespace GrapeCity.ActiveReports.Samples.Charting
{
	public partial class ViewerForm : System.Windows.Forms.Form
	{
		public ViewerForm()
		{
			//
			//Required for Windows Form Designer support
			//
			InitializeComponent();

			// TODO: InitializeComponent After a call, it is a constructor. Please add a code.
			//
		}
		
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ViewerForm());
		}

		/// <summary>
		/// Load a report into the Viewer control on the ViewerForm.
		/// Load event occurs when the Viewer control has been loaded to the ViewerForm instance.
		/// </summary>
		private void ViewerForm_Load(object sender, EventArgs e)
		{
			cboStyle.Items.Add(Properties.Resources.TwoDBarChart);
			cboStyle.Items.Add(Properties.Resources.ThreeDPieChart);
			cboStyle.Items.Add(Properties.Resources.ThreeDBarChart);
			cboStyle.Items.Add(Properties.Resources.FinanceChart);
			cboStyle.Items.Add(Properties.Resources.StackedAreaChart);

			// Sets the state of the initial selection of the combo box "chart type".
			cboStyle.SelectedIndex = 0;
		}

		private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Enable the custom property combo only when you select "line graph".
			SetCustomProperties(cboStyle.SelectedIndex);
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			SectionReport rpt = new SectionReport();

			try
			{
				//Display the preview according to the "chart type" combobox.
				switch (cboStyle.SelectedIndex)
				{
					case 0: // 2D bar chart 
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rpt2DBar));
						break;
					case 1: // 3D pie chart 
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rpt3DPie));
						//Set the direction of rotation.
						if (cboCustom.SelectedIndex == 0)
						{
							((ChartControl)(rpt.Sections["Detail"].Controls["ChartSalesCategories"])).Series[0].Properties["Clockwise"] = true;
						}
						else
						{
							((ChartControl)(rpt.Sections["Detail"].Controls["ChartSalesCategories"])).Series[0].Properties["Clockwise"] = false;
						}
						break;
					case 2: //3D bar chart 
						rpt.LoadLayout(XmlReader.Create (Properties.Resources.rpt3DBar));
						break;
					case 3: // Finance chart 
						rpt.LoadLayout(XmlReader.Create(Properties.Resources.rptCandle));
						break;
					case 4: // Stacked area chart 
						rpt.LoadLayout(XmlReader.Create (Properties.Resources.rptStackedArea));
						break;
				}

				arvMain.LoadDocument(rpt ?? new SectionReport());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void SetCustomProperties(Int32 iGraphStype)
		{
			//To clear the selected candidate.
			cboCustom.Items.Clear();

			//Add a selection candidate and set it in the selected state.
			switch (iGraphStype) 
			{
				case 1:	 //2D pie chart 
					//Change the Visible State
					cboCustom.Visible = true;
					lblCustom.Visible = true;

					cboCustom.Items.Add(Properties.Resources.Clockwise);
					cboCustom.Items.Add(Properties.Resources.Counterclockwise);

					cboCustom.SelectedIndex = 1;

					//To set a label
					lblCustom.Text = Properties.Resources.DirectionOfRotation;
					break;
					
				default:  // Other 
					//To make invisible
					cboCustom.Visible = false;
					lblCustom.Visible = false;
					break;
			}

			Application.DoEvents();
		}
	}
}
