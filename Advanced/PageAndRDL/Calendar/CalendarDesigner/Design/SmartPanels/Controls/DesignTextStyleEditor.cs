using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// Represents the editor of <see cref="DesignTextStyle"/> to use in smart panels pages.
	/// </summary>
	internal class DesignTextStyleEditor : VerticalPanel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly object _objectItem;
		private readonly ReportItem _reportItem;
		private readonly PropertyDescriptor _textStyleProperty;
		private readonly PropertyDescriptor _reportItemDescriptor;
		private TypedValueEditor familyEditor;
		private TypedValueEditor sizeEditor;
		private TypedValueEditor styleEditor;
		private TypedValueEditor weightEditor;
		private TypedValueEditor decorationEditor;
		private TypedValueEditor colorEditor;
		private bool isInitialized;

		/// <summary>
		/// Creates the editor for <see cref="DesignTextStyle"/>.
		/// </summary>
		public DesignTextStyleEditor(IServiceProvider serviceProvider, object objectItem, ReportItem reportItem, PropertyDescriptor reportItemDescriptor, PropertyDescriptor designTextStyleProperty)
			: this(serviceProvider, objectItem, designTextStyleProperty)
		{ // supports composite style properties
			_reportItem = reportItem;
			_reportItemDescriptor = reportItemDescriptor;
		}

		/// <summary>
		/// Creates the editor for <see cref="DesignTextStyle"/>.
		/// </summary>
		public DesignTextStyleEditor(IServiceProvider serviceProvider, object objectItem, PropertyDescriptor designTextStyleProperty)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");
			if (objectItem == null)
				throw new ArgumentNullException("objectItem");
			if (designTextStyleProperty == null)
				throw new ArgumentNullException("designTextStyleProperty");
			_serviceProvider = serviceProvider;
			_objectItem = objectItem;
			_textStyleProperty = designTextStyleProperty;

			InitializeComponent();
		}

		private void InitializeComponent()
		{
			// resolve property descriptor to a value
			object textStyleValue = _textStyleProperty.GetValue(_objectItem);
			// get inner properties using converter to get proper property type converters
			PropertyDescriptorCollection properties = _textStyleProperty.Converter.GetProperties(textStyleValue);
			PropertyDescriptor familyProp = properties[DesignTextStyle.FontFamilyPropertyName];
			PropertyDescriptor sizeProp = properties[DesignTextStyle.FontSizePropertyName];
			PropertyDescriptor styleProp = properties[DesignTextStyle.FontStylePropertyName];
			PropertyDescriptor weightProp = properties[DesignTextStyle.FontWeightPropertyName];
			PropertyDescriptor decorationProp = properties[DesignTextStyle.FontDecorationPropertyName];
			PropertyDescriptor colorProp = properties[DesignTextStyle.FontColorPropertyName];

			// panels
			HorizontalPanel mainPanel = new HorizontalPanel();
			VerticalPanel panel1 = new VerticalPanel();
			VerticalPanel panel2 = new VerticalPanel();

			using (new SuspendLayoutTransaction(this))
			using (new SuspendLayoutTransaction(mainPanel))
			using (new SuspendLayoutTransaction(panel1))
			using (new SuspendLayoutTransaction(panel2))
			{
				AutoSize = true;
				mainPanel.AutoSize = true;
				panel1.AutoSize = true;
				panel2.AutoSize = true;
				mainPanel.Margin = System.Windows.Forms.Padding.Empty;
				panel1.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, _serviceProvider, 1);
				panel2.Margin = System.Windows.Forms.Padding.Empty;

				//
				// headLabel
				//
				ControlGroupHeadingLabel headLabel = new ControlGroupHeadingLabel(_serviceProvider);
				headLabel.Dock = DockStyle.Top;
				headLabel.Text = Resources.CalendarSmartPanelFontLabel;
				//
				// familyEditor
				//
				familyEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontFamilyLabel, ComponentProperty.Create(familyProp, textStyleValue));
				familyEditor.Validated += OnTextStyleEditorChanged;
				familyEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// sizeEditor
				//
				sizeEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontSizeLabel, ComponentProperty.Create(sizeProp, textStyleValue));
				sizeEditor.Validated += OnTextStyleEditorChanged;
				sizeEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// styleEditor
				//
				styleEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontStyleLabel, ComponentProperty.Create(styleProp, textStyleValue));
				styleEditor.Validated += OnTextStyleEditorChanged;
				styleEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// colorEditor
				//
				colorEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontColorLabel, ComponentProperty.Create(colorProp, textStyleValue));
				colorEditor.Validated += OnTextStyleEditorChanged;
				colorEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// weight editor
				//
				weightEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontWeightLabel, ComponentProperty.Create(weightProp, textStyleValue));
				weightEditor.Validated += OnTextStyleEditorChanged;
				weightEditor.Margin = System.Windows.Forms.Padding.Empty;
				//
				// decoration editor
				//
				decorationEditor = new TypedValueEditor(_serviceProvider,
					Resources.CalendarSmartPanelFontDecorationLabel, ComponentProperty.Create(decorationProp, textStyleValue));
				decorationEditor.Validated += OnTextStyleEditorChanged;
				decorationEditor.Margin = System.Windows.Forms.Padding.Empty;
				// panel1
				//
				panel1.Controls.Add(familyEditor);
				panel1.Controls.Add(styleEditor);
				panel1.Controls.Add(colorEditor);
				//
				// panel2
				//
				panel2.Controls.Add(sizeEditor);
				panel2.Controls.Add(weightEditor);
				panel2.Controls.Add(decorationEditor);

				//
				// mainPanel
				//
				mainPanel.Controls.Add(panel1);
				mainPanel.Controls.Add(panel2);

				Controls.Add(headLabel);
				Controls.Add(mainPanel);
			}

			isInitialized = true;
		}

		void OnTextStyleEditorChanged(object sender, EventArgs e)
		{
			if (!isInitialized) return;

			DesignTextStyle textStyle = (DesignTextStyle)_textStyleProperty.GetValue(_objectItem);

			if (textStyle.Family == familyEditor.ValueAsExpression
				&& textStyle.Size == sizeEditor.ValueAsExpression
				&& textStyle.Style == styleEditor.ValueAsExpression
				&& textStyle.Weight == weightEditor.ValueAsExpression
				&& textStyle.Decoration == decorationEditor.ValueAsExpression
				&& textStyle.Color == colorEditor.ValueAsExpression
				)
				return;

			DesignTextStyle newStyle = new DesignTextStyle(
				familyEditor.ValueAsExpression, sizeEditor.ValueAsExpression, styleEditor.ValueAsExpression,
				weightEditor.ValueAsExpression, decorationEditor.ValueAsExpression, colorEditor.ValueAsExpression);
			_textStyleProperty.SetValue(_objectItem, newStyle);
			if (_reportItemDescriptor != null && _reportItem != null)
				_reportItemDescriptor.SetValue(_reportItem, _objectItem);
		}
	}
}
