using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	internal sealed class DesignImageStyleEditor : VerticalPanel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly object _objectItem;
		private readonly ReportItem _reportItem;
		private readonly PropertyDescriptor _imageStyleProperty;
		private readonly PropertyDescriptor _reportItemDescriptor;
		private TypedValueEditor sourceEditor;
		private TypedValueEditor valueEditor;
		private TypedValueEditor mimeTypeEditor;
		private bool isInitialized;

		public DesignImageStyleEditor(IServiceProvider serviceProvider, object objectItem, ReportItem reportItem, PropertyDescriptor reportItemDescriptor, PropertyDescriptor imageStyleProperty)
			: this(serviceProvider, objectItem, imageStyleProperty)
		{
			_reportItem = reportItem;
			_reportItemDescriptor = reportItemDescriptor;
		}

		public DesignImageStyleEditor(IServiceProvider serviceProvider, object reportItem, PropertyDescriptor imageStyleProperty)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");
			if (reportItem == null)
				throw new ArgumentNullException("reportItem");
			if (imageStyleProperty == null)
				throw new ArgumentNullException("imageStyleProperty");
			_serviceProvider = serviceProvider;
			_objectItem = reportItem;
			_imageStyleProperty = imageStyleProperty;

			InitializeComponent();
		}

		private void InitializeComponent()
		{
			// resolve property descriptor to a value
			object appointmentImage = _imageStyleProperty.GetValue(_objectItem);
			// get inner properties using converter to get proper property type converters
			PropertyDescriptorCollection properties = _imageStyleProperty.Converter.GetProperties(appointmentImage);
			PropertyDescriptor source = properties[ImageStyle.SourcePropertyName];
			PropertyDescriptor value = properties[ImageStyle.ValuePropertyName];
			PropertyDescriptor mimeType = properties[ImageStyle.MimeTypePropertyName];

			HorizontalPanel panel1 = new HorizontalPanel();
			using (new SuspendLayoutTransaction(this))
			using (new SuspendLayoutTransaction(panel1))
			{
				panel1.AutoSize = AutoSize = true;
				panel1.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, _serviceProvider, 1);
				//
				// _imageStyleLabel
				//
				ControlGroupHeadingLabel _imageStyleLabel = new ControlGroupHeadingLabel(_serviceProvider);
				_imageStyleLabel.Dock = DockStyle.Top;
				_imageStyleLabel.Text = Resources.ImageLabel;
				//
				// sourceEditor
				//
				sourceEditor =
					new TypedValueEditor(_serviceProvider, Resources.SourceLabel,
						ComponentProperty.Create(source, appointmentImage));
				sourceEditor.Validated += OnImageEditorChanged;
				sourceEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// value Value
				//
				valueEditor =
					new TypedValueEditor(_serviceProvider, Resources.ValueTypeLabel,
						ComponentProperty.Create(value, appointmentImage));
				valueEditor.Validated += OnImageEditorChanged;
				valueEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, valueEditor.ControlInfo, 1);
				//
				//style Value
				//
				mimeTypeEditor =
					new TypedValueEditor(_serviceProvider, Resources.MimeTypeLabel,
						ComponentProperty.Create(mimeType, appointmentImage));
				mimeTypeEditor.Validated += OnImageEditorChanged;
				mimeTypeEditor.Margin = System.Windows.Forms.Padding.Empty;

				panel1.Controls.Add(sourceEditor);
				panel1.Controls.Add(mimeTypeEditor);

				Controls.Add(_imageStyleLabel);
				Controls.Add(panel1);
				Controls.Add(valueEditor);
			}
			isInitialized = true;
		}

		void OnImageEditorChanged(object sender, EventArgs e)
		{
			if (!isInitialized) return;

			DesignImage image = (DesignImage)_imageStyleProperty.GetValue(_objectItem);

			if (image.ImageSource == sourceEditor.ValueAsExpression
				&& image.ImageValue == valueEditor.ValueAsExpression
				&& image.MimeType == mimeTypeEditor.ValueAsExpression
				)
				return;

			DesignImage newStyle = new DesignImage(sourceEditor.ValueAsExpression, valueEditor.ValueAsExpression, mimeTypeEditor.ValueAsExpression);
			_imageStyleProperty.SetValue(_objectItem, newStyle);
			if (_reportItemDescriptor != null && _reportItem != null)
				_reportItemDescriptor.SetValue(_reportItem, _objectItem);
		}
	}
}
