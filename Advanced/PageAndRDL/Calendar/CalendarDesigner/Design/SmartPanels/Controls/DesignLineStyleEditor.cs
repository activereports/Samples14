using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// Represents the editor for <see cref="DesignLineStyle"/>.
	/// </summary>
	internal sealed class DesignLineStyleEditor : VerticalPanel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly object _objectItem;
		private readonly ReportItem _reportItem;
		private readonly PropertyDescriptor _lineStyleProperty;
		private readonly PropertyDescriptor _reportItemDescriptor;
		private TypedValueEditor colorEditor;
		private TypedValueEditor widthEditor;
		private TypedValueEditor styleEditor;
		private bool isInitialized;

		public DesignLineStyleEditor(IServiceProvider serviceProvider, object objectItem, ReportItem reportItem, PropertyDescriptor reportItemDescriptor, PropertyDescriptor imageStyleProperty) : this(serviceProvider, objectItem, imageStyleProperty)
		{
			_reportItem = reportItem;
			_reportItemDescriptor = reportItemDescriptor;
		}

		public DesignLineStyleEditor(IServiceProvider serviceProvider, object objectItem, PropertyDescriptor imageStyleProperty)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");
			if (objectItem == null)
				throw new ArgumentNullException("objectItem");
			if (imageStyleProperty == null)
				throw new ArgumentNullException("imageStyleProperty");
			_serviceProvider = serviceProvider;
			_objectItem = objectItem;
			_lineStyleProperty = imageStyleProperty;

			InitializeComponent();
		}

		private void InitializeComponent()
		{
			// resolve property descriptor to a value
			object appointmentImage = _lineStyleProperty.GetValue(_objectItem);
			// get inner properties using converter to get proper property type converters
			PropertyDescriptorCollection properties = _lineStyleProperty.Converter.GetProperties(appointmentImage);
			PropertyDescriptor color = properties[DesignLineStyle.LineColorPropertyName];
			PropertyDescriptor width = properties[DesignLineStyle.LineWidthPropertyName];
			PropertyDescriptor style = properties[DesignLineStyle.LineStylePropertyName];

			using (new SuspendLayoutTransaction(this))
			{
				this.AutoSize = true;
				//
				// _imageStyleLabel
				//
				ControlGroupHeadingLabel lineStyleLabel = new ControlGroupHeadingLabel(_serviceProvider);
				lineStyleLabel.Dock = DockStyle.Top;
				lineStyleLabel.Text = Resources.LineStyleLabel;
				Controls.Add(lineStyleLabel);

				HorizontalPanel horzPanel = new HorizontalPanel();
				horzPanel.AutoSize = true;
				horzPanel.Dock = DockStyle.Top;
				horzPanel.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, _serviceProvider, 1);
				Controls.Add(horzPanel);
				//
				// styleEditor
				//
				styleEditor =
					new TypedValueEditor(_serviceProvider, Resources.LineStyle,
						ComponentProperty.Create(style, appointmentImage));
				styleEditor.Validated += OnLineStyleEditorChanged;
				styleEditor.Margin = System.Windows.Forms.Padding.Empty;
				horzPanel.Controls.Add(styleEditor);
				//
				// widthEditor
				//
				widthEditor =
					new TypedValueEditor(_serviceProvider, Resources.LineWidth,
						ComponentProperty.Create(width, appointmentImage));
				widthEditor.Validated += OnLineStyleEditorChanged;
				widthEditor.Margin = System.Windows.Forms.Padding.Empty;
				horzPanel.Controls.Add(widthEditor);
				//
				// colorEditor
				//
				colorEditor =
					new TypedValueEditor(_serviceProvider, Resources.LineColor,
						ComponentProperty.Create(color, appointmentImage));
				colorEditor.Validated += OnLineStyleEditorChanged;
				colorEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, colorEditor.ControlInfo, 1);
				Controls.Add(colorEditor);
			}
			isInitialized = true;
		}

		void OnLineStyleEditorChanged(object sender, EventArgs e)
		{
			if (!isInitialized) return;

			DesignLineStyle lineStyle = (DesignLineStyle)_lineStyleProperty.GetValue(_objectItem);

			if (lineStyle.LineColor == colorEditor.ValueAsExpression
				&& lineStyle.LineWidth == widthEditor.ValueAsExpression
				&& lineStyle.LineStyle == styleEditor.ValueAsExpression
				)
				return;

			DesignLineStyle newStyle = new DesignLineStyle(colorEditor.ValueAsExpression, widthEditor.ValueAsExpression, styleEditor.ValueAsExpression);
			_lineStyleProperty.SetValue(_objectItem, newStyle);
			if (_reportItemDescriptor != null && _reportItem != null)
				_reportItemDescriptor.SetValue(_reportItem, _objectItem);
		}
	}
}
