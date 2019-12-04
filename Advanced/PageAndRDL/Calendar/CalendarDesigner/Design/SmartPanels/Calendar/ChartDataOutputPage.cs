using System;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.ActiveReports.Calendar.Design.Designers;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels
{
	/// <summary>
	/// Defines data output page for bullet chart wizard.
	/// </summary>
	internal sealed class CalendarDataOutputPage : CalendarPageBase
	{
		public CalendarDataOutputPage(CalendarDesigner designer)
			: base(designer)
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			using (new SuspendLayoutTransaction(this))
			{
				//
				// elementNameAndOutputEditor
				//
				ElementNameAndOutputEditor elementNameAndOutputEditor = new ElementNameAndOutputEditor(ServiceProvider, Designer, true);
				Controls.Add(elementNameAndOutputEditor);
			}
		}

		#region ElementNameAndOutputEditor

		/// <summary>
		/// Defines the ElementNameAndOutputEditor. 
		/// </summary>
		private sealed class ElementNameAndOutputEditor : VerticalPanel
		{
			private RadioButtonEx autoOutputButton;
			private RadioButtonEx yesOutputButton;
			private RadioButtonEx noOutputButton;
			private TypedValueEditor nameEditor;

			private readonly IServiceProvider _serviceProvider;
			private readonly CalendarDesigner _designer;
			private readonly ComponentProperty elementOutput;
			private readonly bool hasAuto;

			public ElementNameAndOutputEditor(IServiceProvider serviceProvider, CalendarDesigner designer, bool hasAutoOption)
			{
				if (serviceProvider == null)
					throw new ArgumentNullException("serviceProvider");
				if (designer == null)
					throw new ArgumentNullException("designer");
				_serviceProvider = serviceProvider;
				_designer = designer;
				hasAuto = hasAutoOption;
				elementOutput = ComponentProperty.Create(_designer.ReportItem, ReportItemDesignerBase.DataElementOutputPropertyName);
				InitializeComponent();
			}

			private void InitializeComponent()
			{
				using (new SuspendLayoutTransaction(this))
				{
					AutoSize = true;

					//
					// nameEditor
					//
					nameEditor = new TypedValueEditor(_serviceProvider,
						Resources.ElementNameCaption, _designer.ReportItem, ReportItemDesignerBase.DataElementNamePropertyName);
					Controls.Add(nameEditor);
					//
					// outputTitleLabel
					//
					LabelEx outputTitleLabel = new LabelEx(_serviceProvider);
					outputTitleLabel.Text = Resources.OutputCaption;
					Controls.Add(outputTitleLabel);
					//
					// autoOutputButton
					//
					if (hasAuto)
					{
						autoOutputButton = new RadioButtonEx(_serviceProvider);
						autoOutputButton.Text = Resources.AutoLabel;
						autoOutputButton.Checked = Output == DataElementOutput.Auto;
						autoOutputButton.CheckedChanged += OutputChangedHandler;
						autoOutputButton.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, autoOutputButton.ControlInfo, 1);
						Controls.Add(autoOutputButton);
					}
					//
					// yesOutputButton
					//
					yesOutputButton = new RadioButtonEx(_serviceProvider);
					yesOutputButton.Text = Resources.YesLabel;
					yesOutputButton.Checked = Output == DataElementOutput.Output;
					yesOutputButton.CheckedChanged += OutputChangedHandler;
					yesOutputButton.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, yesOutputButton.ControlInfo, 1);
					Controls.Add(yesOutputButton);
					//
					// noOutputButton
					//
					noOutputButton = new RadioButtonEx(_serviceProvider);
					noOutputButton.Text = Resources.NoLabel;
					noOutputButton.Checked = Output == DataElementOutput.NoOutput;
					noOutputButton.CheckedChanged += OutputChangedHandler;
					noOutputButton.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, noOutputButton.ControlInfo, 1);
					Controls.Add(noOutputButton);
				}
			}

			private DataElementOutput Output
			{
				get { return (DataElementOutput)elementOutput.GetValue(); }
				set { elementOutput.SetValue(value); }
			}

			private void OutputChangedHandler(object sender, EventArgs e)
			{
				if (yesOutputButton.Checked)
					Output = DataElementOutput.Output;
				else if (noOutputButton.Checked)
					Output = DataElementOutput.NoOutput;
				else if (autoOutputButton != null && autoOutputButton.Checked)
					Output = DataElementOutput.Auto;
			}
		}

		#endregion
	}
}
