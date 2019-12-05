using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI.Controls;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// Represents typed value editor to use in smart panels pages.
	/// </summary>
	internal class TypedValueEditor : VerticalPanel, IPresenter
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly string _labelText;
		private readonly ComponentProperty _property;
		private readonly IUserMessageProvider _userMessageProvider;
		private readonly IEditControlView _editControl;
		private readonly ReportItem _reportItem;
		private readonly PropertyDescriptor _reportItemDescriptor;

		public TypedValueEditor(IServiceProvider serviceProvider, object component, string propertyName)
			: this(serviceProvider, null, ComponentProperty.Create(component, propertyName))
		{
		}

		public TypedValueEditor(IServiceProvider serviceProvider, string labelText, object component, string propertyName)
			: this(serviceProvider, labelText, ComponentProperty.Create(component, propertyName))
		{
		}

		public TypedValueEditor(IServiceProvider serviceProvider, string labelText, ComponentProperty property, ReportItem reportItem, PropertyDescriptor reportItemDescriptor) : this(serviceProvider, labelText, property)
		{
			_reportItem = reportItem;
			_reportItemDescriptor = reportItemDescriptor;
		}

		public TypedValueEditor(IServiceProvider serviceProvider, string labelText, ComponentProperty property)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");
			_serviceProvider = serviceProvider;
			_labelText = labelText;
			_property = property;
			_userMessageProvider = _serviceProvider.GetService(typeof(IUserMessageProvider)) as IUserMessageProvider;

			_editControl = TypedValueEditControlFactory.CreateControl(this, TypeDescriptorContext, _property);
			if (_editControl.GetType().IsSubclassOf(typeof(CheckBoxEx)))
				_editControl.Control.Text = _labelText;

			InitializeComponent();

			// synchronize the value
			Value = _property.GetValue();
		}

		private void InitializeComponent()
		{
			using (new SuspendLayoutTransaction(this))
			{
				AutoSize = true;

				//
				// label
				//
				if (_labelText != null && !_editControl.GetType().IsSubclassOf(typeof(CheckBoxEx)))
				{
					Label label = new Label();
					label.Dock = DockStyle.Top;
					label.Text = _labelText;
					label.TextAlign = ContentAlignment.BottomLeft;
					label.MinimumSize = new Size(label.MinimumSize.Width, 17);
					Controls.Add(label);
				}

				//
				// _editControl
				//
				_editControl.Control.Dock = DockStyle.Top;
				//_editControl.Control.AutoSize = true;
				Controls.Add(_editControl.Control);
			}
		}

		#region IPresenter Implementation

		/// <summary>
		/// Implements <see cref="IPresenter.ConvertFromSafely"/>
		/// </summary>
		public object ConvertFromSafely(object value, out string errorMessage)
		{
			if (TypeConverter == null)
			{
				errorMessage = "";
				return value;//?Why would this happen?
			}

			if (value != null && value.GetType() == ComponentProperty.PropertyType)
			{
				// no conversion necessary.
				errorMessage = "";
				return value;
			}
			try
			{
				errorMessage = "";
				return TypeConverter.ConvertFrom(TypeDescriptorContext, CultureInfo.InvariantCulture, value);
			}
			catch (Exception eConvertFrom)
			{
				// I'm not sure why this would happen. Please comment to explain why if you know.
				//Debug.Fail("TypeConverter failed: " + eConvertFrom.ToString());
				errorMessage = eConvertFrom.Message;
				return value;
			}
		}

		/// <summary>
		/// Implements <see cref="IPresenter.ConvertToSafely"/>
		/// </summary>
		public object ConvertToSafely(object value, Type destinationType)
		{
			if (TypeConverter == null)
			{
				return value;//?Why would this happen?
			}
			if (value != null && value.GetType() == destinationType)
				return value;
			try
			{
				return TypeConverter.ConvertTo(TypeDescriptorContext, CultureInfo.InvariantCulture, value, destinationType);
			}
			catch (Exception eConvertTo)
			{
				// I'm not sure why this would happen. Please comment to explain why if you know.
				Debug.Fail("TypeConverter failed: " + eConvertTo);
				return value;
			}
		}

		string IPresenter.GetUILabel()
		{
			return _labelText;
		}

		/// <summary>
		/// Implements <see cref="IPresenter.SetValueFromUI"/>
		/// </summary>

		bool IPresenter.SetValueFromUI(IEditControlView sender, ref object newValue, out bool errorOccured)
		{
			ComponentProperty propertyToSet = ComponentProperty;
			bool valueChanged = false;
			string errorMessage;

			// update erro provider
			object value = ConvertFromSafely(newValue, out errorMessage);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				newValue = propertyToSet.GetValue();
			}
			else
			{
				// we converted the value succesfully...
				if (value != null)// we don't like null values? Why??
				{
					object existingValue = null;
					try
					{
						existingValue = propertyToSet.GetValue();
					}
					catch (Exception eSet)
					{
						//DiagContext.Designer.TraceError("Error getting property value (this shouldn't really happen, but can probably be safely ignored).");
						errorMessage = eSet.Message;
					}
					// Only set the value if it is different.
					if (0 != Comparer.Default.Compare(existingValue, value))
					{
						try
						{
							propertyToSet.SetValue(value);
							if (_reportItemDescriptor != null && _reportItem != null)
							{
								_reportItemDescriptor.SetValue(_reportItem, propertyToSet.Component);
							}
							valueChanged = true;
						}
						catch (Exception eSet2)
						{
							//NOTE: Can happen when a property in the ROM throws due to out of range (e.g. Report.GridSpacing) ...
							//Fix for CR# 21451. The value was technically valid for the property type but outside the bounds.
							errorMessage = eSet2.Message;
						}
					}
				}
			}
			//display errors regardless wether we changed the value or not.	
			if (string.IsNullOrEmpty(errorMessage))
			{
				errorOccured = false;
				// Handles the case when the typeconverter changes the string representation of the value (e.g. user enters 1, typeconverter changes it to 1in)
				if (propertyToSet.TypeConverter != null)
				{
					newValue = ConvertToSafely(value, sender.DisplayedUIValueType);
				}
			}
			else
			{
				errorOccured = true;
			}
			// set the error message regardless. If it's empty it clears the message.
			((IPresenter)this).SetErrorMessage(errorMessage);

			// if we did change the value make sure we raise our own event.
			if (valueChanged)
			{
				OnValueChanged();
			}
			return valueChanged;
		}

		void IPresenter.SetErrorMessage(string errorMessage)
		{
			if (_userMessageProvider == null)
				return;

			MessageKind messageKind = MessageKind.None;
			if (errorMessage.Length > 0)
				messageKind = MessageKind.Error;
			UserMessage userMessage = new UserMessage(messageKind, errorMessage, _editControl.Control);
			_userMessageProvider.SetMessage(userMessage);
			_userMessageProvider.MessageClosed += OnMessageClosed;
		}

		void OnMessageClosed(object sender, EventArgs e)
		{
			_editControl.SetErrorState(false);
		}

		IServiceProvider IPresenter.GetServiceProvider()
		{
			return _serviceProvider;
		}

		object IPresenter.UITypeEditorEditValue(IServiceProvider serviceProvider, object value)
		{
			if (ComponentProperty.UITypeEditor != null)
				return ComponentProperty.UITypeEditor.EditValue(TypeDescriptorContext, serviceProvider, value);
			return value;
		}

		TypeConverter.StandardValuesCollection IPresenter.GetStandardValues()
		{
			if (TypeConverter != null)
				return TypeConverter.GetStandardValues(TypeDescriptorContext);
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}

		bool IPresenter.GetStandardValuesExclusive()
		{
			if (TypeConverter != null)
				return TypeConverter.GetStandardValuesExclusive(TypeDescriptorContext);
			return false;
		}

		bool IPresenter.GetStandardValuesSupported()
		{
			if (TypeConverter != null)
				return TypeConverter.GetStandardValuesSupported(TypeDescriptorContext);
			return false;
		}

		#endregion

		/// <summary>
		/// Gets <see cref="ControlInfo"/> related to underlying control.
		/// </summary>
		public ControlInfo ControlInfo
		{
			get { return _editControl.ControlInfo; }
		}

		/// <summary>
		/// Gets or sets the value currently being edited.
		/// This is the value in the type that is stored in the property.
		/// For example if hte property type is <see cref="Point"/>, then the result will be <see cref="Point"/> (not the string which may be in the UI).
		/// </summary>
		public object Value
		{
			get
			{
				object uiValue = _editControl.DisplayedUIValue;
				//FIX:CR22534: Use ConvertFromSafely instead of accessing TypeConverter directly...
				string errorMessage;
				object convertedValue = ConvertFromSafely(uiValue, out errorMessage);
				Debug.Assert(string.IsNullOrEmpty(errorMessage), "Error converting value.");
				return convertedValue;
			}
			set
			{
				//Compare both values to ensure the component update only gets called when the value is changed.
				string errorMessage = string.Empty;
				bool hasValueChanged = false;
				object currentValue = _property.GetValue();
				if (value != null && Comparer.Default.Compare(value, currentValue) != 0)
				{
					try
					{
						_property.SetValue(currentValue);
						hasValueChanged = true;
					}
					catch (Exception ex)
					{
						//Fix for CR# 21451. The value was technically valid for the property type but outside the bounds.
						value = currentValue;
						errorMessage = ex.Message;
					}
				}

				((IPresenter)this).SetErrorMessage(errorMessage);

				//NOTE: Probably everything below here can be done with ConvertToSafely instead. I'm not changing it now because I don't know of any problems with teh code per se. But if you run into a problem with it, revise ConvertToSafely if needed and use it instead. -- ScottW
				Type uiDataType = _editControl.DisplayedUIValueType;
				object convertedUIValue;
				if (value != null && value.GetType() == uiDataType)
				{
					// we don't need the converstion
					convertedUIValue = value;
				}
				else
				{
					if (TypeConverter != null && TypeConverter.CanConvertTo(TypeDescriptorContext, uiDataType))
					{
						convertedUIValue = TypeConverter.ConvertTo(TypeDescriptorContext, CultureInfo.InvariantCulture, value, uiDataType);
					}
					else
					{
						Debug.Fail("TypeConverter does not support hte requested type!");
						// this is an error really, but we have no choice at this point:
						convertedUIValue = _editControl.DisplayedUIValue;
					}
				}

				_editControl.DisplayedUIValue = convertedUIValue;
				if (hasValueChanged)
					OnValueChanged();
			}
		}

		/// <summary>
		/// Gets or sets the value being edited as an <see cref="ExpressionInfo"/>. Does not return null.
		/// </summary>
		public ExpressionInfo ValueAsExpression
		{
			get
			{
				ExpressionInfo expression = Value as ExpressionInfo ?? ExpressionInfo.Parse("", ExpressionResultType.Variant);

				return expression;
			}
			set
			{
				Value = value;
			}
		}

		private TypeConverter TypeConverter
		{
			get { return ComponentProperty.TypeConverter; }
		}

		private ComponentProperty ComponentProperty
		{
			get { return _property; }
		}

		public ITypeDescriptorContext TypeDescriptorContext
		{
			get
			{
				return _typeDescriptorContext ?? (_typeDescriptorContext = new TypeEditorTypeDescriptorContext(_serviceProvider, _property));
			}
		}
		private ITypeDescriptorContext _typeDescriptorContext;

		/// <summary>
		/// Raised when the value of <see cref="ValueAsExpression"/> has changed.
		/// </summary>
		public event EventHandler ValueChanged;

		/// <summary>
		/// Raises the <see cref="ValueChanged"/> event.
		/// </summary>
		protected virtual void OnValueChanged()
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}
	}

	/// <summary>
	/// Represents a presenter (Model-View-Presenter Pattern) used with <see cref="TypedValueEditor"/>.
	/// </summary>
	internal interface IPresenter
	{
		/// <summary>
		/// Sets a new value for the property/model from the UI control.
		/// </summary>
		/// <param name="newValue">The new value from the UI control as the type specified by <see cref="IEditControlView.DisplayedUIValueType"/> by the sender.</param>
		/// <param name="errorOccured">Indicates if an error occured trying to set this value into the current property.</param>
		/// <param name="sender">The <see cref="IEditControlView"/> that is specifying the new value.</param>
		/// <returns>True if the value was set, otherwise false. The value may not be set if it is the same as the current value of the property or an error occued.</returns>
		/// <remarks>
		/// If the value was set, you should update the textbox/UI control with the new value in <paramref name="newValue"/> as the <see cref="TypeConverter"/> may have changed it.
		/// </remarks>
		bool SetValueFromUI(IEditControlView sender, ref object newValue, out bool errorOccured);
		/// <summary>
		/// Converts the specified value from a string to the type used by the property being currently edited.
		/// Uses <see cref="System.ComponentModel.TypeConverter.ConvertFrom(ITypeDescriptorContext,CultureInfo,object)"/> but handles any exceptions.
		/// </summary>
		object ConvertFromSafely(object value, out string errorMessage);
		/// <summary>
		/// Converts the specified value to the specified type. Simple wrapper around Uses <see cref="System.ComponentModel.TypeConverter.ConvertTo(ITypeDescriptorContext,CultureInfo,object,Type)"/> but handles any exceptions.
		/// </summary>
		object ConvertToSafely(object value, Type destinationType);
		/// <summary>
		/// Gets a label string associated with the view control.
		/// </summary>
		string GetUILabel();
		/// <summary>
		/// Used by the UI control when it encounters an error.
		/// </summary>
		void SetErrorMessage(string message);
		/// <summary>
		/// Gets an <see cref="IServiceProvider"/>.
		/// </summary>
		IServiceProvider GetServiceProvider();//TODO: Try and get rid of this.
											  /// <summary>
											  /// Encapsulates <see cref="UITypeEditor.EditValue(IServiceProvider,object)"/>
											  /// </summary>
		object UITypeEditorEditValue(IServiceProvider serviceProvider, object value);
		/// <summary>
		/// Encapsulates <see cref="TypeConverter.GetStandardValues()"/>
		/// </summary>
		TypeConverter.StandardValuesCollection GetStandardValues();
		/// <summary>
		/// Encapsulates <see cref="TypeConverter.GetStandardValuesExclusive()"/>
		/// </summary>
		bool GetStandardValuesExclusive();
		/// <summary>
		/// Encapsulates <see cref="TypeConverter.GetStandardValuesSupported()"/>
		/// </summary>
		bool GetStandardValuesSupported();
	}

	/// <summary>
	/// Defines an interface for a control that dispalys the UI for a value of an arbitrary type.
	/// Use <see cref="TypedValueEditControlFactory"/> to get create objects of this type.
	/// </summary>
	internal interface IEditControlView
	{
		/// <summary>
		/// Sets or gets the value displayed in the UI control. The type of the value will be the type specified in <see cref="DisplayedUIValueType"/>.
		/// </summary>
		object DisplayedUIValue
		{
			get;
			set;
		}
		/// <summary>
		/// Specifies the <see cref="Type"/> that <see cref="DisplayedUIValue"/> should be in.
		/// </summary>
		/// <remarks>
		/// For example a textbox editor may want <c>string</c>, but the checkbox editor may want <c>bool</c>.
		/// NOTE: The typeconverter for the property must support the converstion!
		/// </remarks>
		Type DisplayedUIValueType { get; }
		/// <summary>
		/// Gets the actual S.W.F. control that will be added to the container and positioned/sized.
		/// </summary>
		Control Control { get; }
		/// <summary>
		/// Returns the desired height of the edit control
		/// </summary>
		int DesiredHeight { get; }
		/// <summary>
		/// Returns <see cref="ControlInfo"/> related to underlying control. 
		/// </summary>
		ControlInfo ControlInfo { get; }
		/// <summary>
		/// Indicates that control has error value
		/// </summary>
		void SetErrorState(bool errorOccured);
	}

	internal struct ComponentProperty
	{
		private readonly PropertyDescriptorEx _property;
		private readonly TypeConverter _typeConverter;
		private readonly UITypeEditor _editor;
		private readonly object _component;
		private List<ComponentProperty> _additionalComponentsToNotify;

		private ComponentProperty(PropertyDescriptor property, object component/*, TypeConverter typeConverter*/)
		{
			_property = null;
			_component = component;
			_editor = property.GetEditor(typeof(UITypeEditor)) as UITypeEditor;
			_additionalComponentsToNotify = null;

			// If typeConverter was not specified, attempt to extract it from the property.
			_typeConverter = property.Converter;
			// If a property is not setup in the designer the attributes may not be set on it. 
			Attribute[] attributes = new Attribute[property.Attributes.Count];
			property.Attributes.CopyTo(attributes, 0);
			foreach (Attribute attribute in attributes)
			{
				TypeConverterAttribute converterAttribute = attribute as TypeConverterAttribute;
				if (converterAttribute != null)
				{
					if (_typeConverter.GetType().Name != converterAttribute.ConverterTypeName)
					{
						try
						{
							_typeConverter = (TypeConverter)Activator.CreateInstance(Type.GetType(converterAttribute.ConverterTypeName));
						}
						catch
						{
							//Enum converter require a type constructor parameter.
							_typeConverter = (TypeConverter)Activator.CreateInstance(Type.GetType(converterAttribute.ConverterTypeName), new object[] { property.PropertyType });
						}
					}
				}

				EditorAttribute editorAttribute = attribute as EditorAttribute;
				if (_editor == null && editorAttribute != null)
				{
					_editor = (UITypeEditor)Activator.CreateInstance(Type.GetType(editorAttribute.EditorTypeName));
				}
			}

			_property = new PropertyDescriptorEx(property, _typeConverter, attributes);
		}

		public static ComponentProperty Create(PropertyDescriptor property, object component)
		{
			return new ComponentProperty(property, component);
		}

		/// <summary>
		/// Initializes an instance of the <see cref="ComponentProperty"/> struct with the specified component and property.
		/// </summary>
		/// <param name="component">The initial value for <see cref="ComponentProperty.Component"/>.</param>
		/// <param name="propertyName">The name of the <see cref="PropertyDescriptor"/> from the provided component that is to be used for the initial value for <see cref="ComponentProperty.Property"/>.</param>
		/// <exception cref="ArgumentNullException">Raised when either argument is null.</exception>
		/// <exception cref="ArgumentException">Raised when the <see cref="PropertyDescriptor"/> specified by <paramref name="propertyName"/> cannot be found.</exception>
		public static ComponentProperty Create(object component, string propertyName)
		{
			if (component == null)
				throw new ArgumentNullException("component");
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException("propertyName");

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			PropertyDescriptor property = properties[propertyName];
			if (property == null)
			{
				//The DesignAction properties where not coming through when using the component, using the Type made them available.
				properties = TypeDescriptor.GetProperties(component.GetType());
				property = properties[propertyName];
				if (property == null)
					throw new ArgumentException("Property '" + propertyName + "' does not exist.");
			}
			return new ComponentProperty(property, component);
		}

		/// <summary>
		/// NOTE: USE <see cref="SetValue"/> to set the value of this <see cref="PropertyDescriptor"/>!
		/// </summary>
		public PropertyDescriptorEx Property
		{
			get { return _property; }
		}

		/// <summary>
		/// Returns the type of the underlying property.
		/// </summary>
		public Type PropertyType
		{
			get { return _property.PropertyType; }
		}

		public object Component
		{
			get { return _component; }
		}

		public TypeConverter TypeConverter
		{
			get { return _typeConverter; }
		}

		public UITypeEditor UITypeEditor
		{
			get { return _editor; }
		}

		/// <summary>
		/// Sets the value of the property this object represents.
		/// </summary>
		/// <param name="value">The value to set to the property.</param>
		public void SetValue(object value)
		{
			_property.SetValue(Component, value);
		}

		/// <summary>
		/// Returns the value of the property this object represents.
		/// </summary>
		/// <returns>The value of <see cref="Property"/> for the <see cref="Component"/> instance.</returns>
		public object GetValue()
		{
			return _property != null ? _property.GetValue(Component) : null;
		}

		/// <summary>
		/// Adds an additional component property that will be updated when this property's value changes.
		/// </summary>
		/// <param name="component">The <see cref="IComponent"/> that the additional change notification needs raised for.</param>
		/// <param name="propertyDescriptor">The property of the specified <paramref name="component"/> that the change notification should be raised for.</param>
		/// <remarks>
		/// This is useful when <see cref="TypedValueEditor"/> is bound to a property of an inner objects (e.g. DataSet, Textbox.ToggleImage, etc.) that are not sited components (as when a component is not sited, the PropertyDescriptor cannot raise ComponentChange.
		/// This allows a proper ComponentChange notification to be raised for a real/outter component.
		/// </remarks>
		public void AddComponentUpdateNotification(IComponent component, PropertyDescriptor propertyDescriptor)
		{
			ComponentProperty componentProperty = new ComponentProperty(propertyDescriptor, component);

			if (_additionalComponentsToNotify == null)
				_additionalComponentsToNotify = new List<ComponentProperty>();

			_additionalComponentsToNotify.Add(componentProperty);
		}
	}

	/// <summary>
	/// A property descriptor wrapper that allows for setting a specific TypeConverter. Some of the properties used in the smart panels do not have their property attributes automatically so it is being 
	/// done in the ComponentProperty constructor. This wrapper allows the UITypeEditor to get the correct typeconverter.
	/// </summary>
	internal sealed class PropertyDescriptorEx : PropertyDescriptor
	{
		private readonly PropertyDescriptor _propertyDescriptor;
		private readonly TypeConverter _converter;
		public PropertyDescriptorEx(PropertyDescriptor propertyDescriptor, TypeConverter converter, Attribute[] attributes)
			: base(propertyDescriptor.Name, attributes)
		{
			_propertyDescriptor = propertyDescriptor;
			_converter = converter;
		}
		public override bool CanResetValue(object component)
		{
			return _propertyDescriptor.CanResetValue(component);
		}

		public override object GetValue(object component)
		{
			return _propertyDescriptor.GetValue(component);
		}

		public override void ResetValue(object component)
		{
			_propertyDescriptor.ResetValue(component);
		}

		public override void SetValue(object component, object value)
		{
			_propertyDescriptor.SetValue(component, value);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return _propertyDescriptor.ShouldSerializeValue(component);
		}

		public override Type ComponentType
		{
			get { return _propertyDescriptor.ComponentType; }
		}

		public override bool IsReadOnly
		{
			get { return _propertyDescriptor.IsReadOnly; }
		}

		public override Type PropertyType
		{
			get { return _propertyDescriptor.PropertyType; }
		}

		public override TypeConverter Converter
		{
			get { return _converter; }
		}

	}

	/// <summary>
	/// Provides a specialized context for the <see cref="TypedValueEditor"/> to use.
	/// </summary>
	internal class TypeEditorTypeDescriptorContext : ITypeDescriptorContext
	{
		private readonly IContainer _container;
		private readonly IServiceProvider _serviceProvider;
		private ComponentProperty _componentProperty;
		private object _instance;
		public TypeEditorTypeDescriptorContext(IServiceProvider serviceProvider, ComponentProperty componentProperty)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");

			_serviceProvider = serviceProvider;
			IDesignerHost designerHost = _serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost == null)
				Debug.WriteLine("Designer host not found. Expected designer host to be available for the TypeEditorTypeDescriptorContext.");
			else
				_container = designerHost.Container;
			_componentProperty = componentProperty;

		}
		public bool OnComponentChanging()
		{
			throw new NotImplementedException();
		}

		public void OnComponentChanged()
		{
			throw new NotImplementedException();
		}

		public IContainer Container
		{
			get { return _container; }
		}

		public object Instance
		{
			get
			{
				//Fix for CR# 20731. The Instance being returned was not the report item but the wrapped class.
				return _instance ?? (_instance = GetReportItem());
			}
		}

		private object GetReportItem()
		{
			if (_componentProperty.Component is ReportItem || _componentProperty.Component is IReportItemChild)
				return _componentProperty.Component;

			ISelectionService selectionService = _serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService == null)
			{
				Debug.Fail(typeof(ISelectionService).Name + " is unavailable.");
				return _componentProperty.Component;
			}

			ReportItem reportItem = selectionService.PrimarySelection as ReportItem;
			if (reportItem != null)
				return reportItem;
			IReportItemChild childReportItem = selectionService.PrimarySelection as IReportItemChild;
			if (childReportItem != null)
				reportItem = childReportItem.Parent;

			if (reportItem == null)
			{
				Debug.Fail("No primary selection.");
			}

			return _componentProperty.Component;
		}

		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return _componentProperty.Property;
			}
		}

		public object GetService(Type serviceType)
		{
			return _serviceProvider.GetService(serviceType);
		}
	}

	/// <summary>
	/// A factory for <see cref="IEditControlView"/> objects.
	/// This creates UI edit controls based on information from a provided <see cref="TypeConverter"/> and an optional <see cref="UITypeEditor"/> (a specialized combobox or textbox).
	/// </summary>
	internal static class TypedValueEditControlFactory
	{
		/// <summary>
		/// Initializes a <see cref="IEditControlView"/>.
		/// </summary>
		/// <param name="presenter">The <see cref="IPresenter"/> to initialize edit control from.</param>
		/// <param name="context">A <see cref="ITypeDescriptorContext"/> that can be passed to the provided <see cref="TypeConverter"/> and <see cref="UITypeEditor"/>.</param>
		/// <param name="componentProperty">A <see cref="ComponentProperty"/> containing the component and property information to use with the control.</param>
		public static IEditControlView CreateControl(IPresenter presenter, ITypeDescriptorContext context, ComponentProperty componentProperty)
		{
			if (componentProperty.PropertyType == typeof(bool))
			{
				return new CheckBoxControl(presenter);
			}

			if (componentProperty.UITypeEditor != null)
			{
				UITypeEditorEditStyle style = componentProperty.UITypeEditor.GetEditStyle(context);
				if (style == UITypeEditorEditStyle.DropDown)
				{
					UIEditorComboBoxControl combo = new UIEditorComboBoxControl(presenter);
					return combo;
				}
				Debug.Fail("Unexpected UITypeEditor style.");
			}

			if (componentProperty.TypeConverter.GetStandardValuesSupported(context))
			{
				ComboBoxControl combo = new ComboBoxControl(presenter);
				return combo;
			}

			TextBoxControl textBox = new TextBoxControl(presenter);
			return textBox;
		}

		#region UIEditorComboBoxControl
		/// <summary>
		/// Custom control to simulate a combobox that will display the UITypeEditor when the combobox button is clicked.
		/// </summary>
		internal sealed class UIEditorComboBoxControl : Panel, IEditControlView
		{
			private ComboTextBox _textBox;
			private ComboButtonEx _comboButton;
			private bool _isInitialized;
			private Size _cachedSize = Size.Empty;
			private const int _textBoxXPadding = 2;
			private const int _textBoxYPadding = 3;
			private const int _comboButtonWidth = 18;
			private const int _comboButtonHeightAdjustment = 2;
			private readonly ControlRendering _controlRendering;
			private bool _hasActiveUITypeEditor;
			private bool _isMouseOver;
			private bool _hasFocus;
			private readonly IPresenter _presenter;
			private readonly int _comboBoxHeight;
			private readonly ControlInfo _comboControlInfo;
			private int _indentions;
			private string _oldValue;

			public UIEditorComboBoxControl(IPresenter presenter)
			{
				if (presenter == null)
					throw new ArgumentNullException("presenter");
				_presenter = presenter;
				//TODO: add ControlRendering to Presenter!
				_controlRendering = new ControlRendering(presenter.GetServiceProvider());
				InitializeComponent();
				_comboControlInfo = _controlRendering.ControlInfo(typeof(ComboBox));
				_comboBoxHeight = _comboControlInfo.Height;

			}

			/// <summary>
			/// Gets the <see cref="ControlInfo"/> for the <see cref="UIEditorComboBoxControl"/>.
			/// </summary>
			public ControlInfo ControlInfo
			{
				get { return _comboControlInfo; }
			}

			void IEditControlView.SetErrorState(bool errorOccured)
			{
				_comboButton.HasError = errorOccured;
				if (!errorOccured && OldValue != null)
				{
					DisplayedUIValue = OldValue;
				}
			}

			private string OldValue
			{
				get { return _oldValue; }
				set { _oldValue = value; }
			}

			/// <summary>
			/// Gets or sets the number of indentions the control should have when being laid out.
			/// </summary>
			public int Indentions
			{
				get { return _indentions; }
				set { _indentions = value; }
			}

			private void InitializeComponent()
			{
				_isInitialized = false;

				_textBox = new ComboTextBox(_presenter.GetServiceProvider());
				_comboButton = new ComboButtonEx(_presenter.GetServiceProvider());
				using (new SuspendLayoutTransaction(this))
				{
					//TODO: Consider RTL changes that need to be made.

					Controls.Clear();
					//
					//_textBox
					//
					_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
					_textBox.AutoSize = false;
					_textBox.MouseEnter += TextBoxMouseEnter;
					_textBox.MouseLeave += TextBoxMouseLeave;
					_textBox.Validating += TextBoxValidating;
					_textBox.AltDown += TextBoxAltDown;
					_textBox.GotFocus += TextBoxGotFocus;
					_textBox.LostFocus += TextBoxLostFocus;
					//
					//_comboButton
					_comboButton.Width = _comboButtonWidth;
					_comboButton.Click += ComboButtonClick;
					_comboButton.MouseEnter += ComboButtonMouseEnter;
					_comboButton.MouseLeave += ComboButtonMouseLeave;
					_comboButton.TabStop = false;
					_comboButton.Height = _comboBoxHeight - _comboButtonHeightAdjustment;
					//
					//UIEditorComboBoxControl
					this.Height = _comboBoxHeight;
					this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
					this.BorderStyle = System.Windows.Forms.BorderStyle.None;
					this.BackColor = _textBox.BackColor;
					this.DockPadding.All = 1;
					this.TabStop = false;
					Controls.Add(_textBox);
					Controls.Add(_comboButton);
				}

				_isInitialized = true;
			}


			/// <summary>
			/// Correctly positions the textbox inside the bounds of the control.
			/// </summary>
			private void SetTextboxPosition()
			{
				_textBox.Top = _textBoxYPadding;
				_textBox.Left = _textBoxXPadding;
				_textBox.Width = this.Width - (_comboButtonWidth + _textBoxYPadding + 1);
				_textBox.Height = _comboBoxHeight - (_textBoxYPadding * 2);
			}

			#region Events
			private void ComboButtonClick(object sender, EventArgs e)
			{

				if (!_isInitialized && _hasActiveUITypeEditor)
					return;
				_comboButton.HasFocus = true;
				_hasFocus = true;
				_comboButton.IsMouseDown = true;
				ShowUITypeEditor();
			}

			private void ComboButtonMouseLeave(object sender, EventArgs e)
			{
				if (!_isInitialized)
					return;
				_isMouseOver = false;
				_comboButton.IsMouseOver = false;
				Invalidate();
			}

			private void ComboButtonMouseEnter(object sender, EventArgs e)
			{
				if (!_isInitialized)
					return;
				_isMouseOver = true;
				_comboButton.IsMouseOver = true;
				Invalidate();
			}

			private void TextBoxMouseEnter(object sender, EventArgs e)
			{
				if (!_isInitialized)
					return;
				_isMouseOver = true;
				_comboButton.IsMouseOver = true;
				Invalidate();
			}

			private void TextBoxMouseLeave(object sender, EventArgs e)
			{
				if (!_isInitialized)
					return;
				_isMouseOver = false;
				_comboButton.IsMouseOver = false;
				Invalidate();
			}

			private void TextBoxValidating(object sender, CancelEventArgs e)
			{
				if (!_isInitialized)
					return;

				bool hasError;

				OnValueChanged(this._textBox.Text, out hasError);
				_comboButton.HasError = hasError;
			}
			private void TextBoxAltDown(object sender, EventArgs e)
			{
				if (!_isInitialized)
					return;
				ShowUITypeEditor();
			}

			private void TextBoxGotFocus(object sender, EventArgs e)
			{
				_comboButton.HasFocus = true;
				_hasFocus = true;
				Invalidate();
			}

			private void TextBoxLostFocus(object sender, EventArgs e)
			{
				_comboButton.HasFocus = false;
				_hasFocus = false;
				Invalidate();
			}
			#endregion

			#region member overrides
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				_isMouseOver = true;
				_comboButton.IsMouseOver = true;
				Invalidate();
			}
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				_isMouseOver = false;
				_comboButton.IsMouseOver = false;
				Invalidate();
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

				Color color = BackColor;
				if (!Enabled)
					color = _controlRendering.Colors.ControlBackground;

				using (SolidBrush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rect);
				}

				Rectangle buttonRect = rect;
				buttonRect.Width -= SystemInformation.HorizontalScrollBarArrowWidth;

				if (this.BorderStyle == System.Windows.Forms.BorderStyle.None)
				{
					color = _controlRendering.Colors.ControlDark;
					if (_isMouseOver || _hasFocus)
						color = _controlRendering.Colors.SelectedText;

					using (Pen pen = new Pen(color))
					{
						e.Graphics.DrawRectangle(pen, rect.X, rect.Y,
							rect.Width, rect.Height);

					}
				}
			}

			#endregion

			#region Layout Members

			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				base.SetBoundsCore(x, y, width, height, specified);
				DoLayout();
			}

			protected override void OnLayout(LayoutEventArgs levent)
			{
				base.OnLayout(levent);
				_cachedSize = Size.Empty;
				DoLayout();
			}

			private Size DoLayout()
			{

				if (_isInitialized && _cachedSize == Size.Empty)
				{
					using (new SuspendLayoutTransaction(this, true))
					{
						int comboBoxHeight = _comboBoxHeight;

						const BoundsSpecified boundsSpecified = BoundsSpecified.Location | BoundsSpecified.Width;

						this.SetBounds(this.Location.X, this.Location.Y, this.Width, comboBoxHeight);

						int height = comboBoxHeight - _textBoxYPadding;
						int width = this.Width - (_comboButton.Width + _textBoxXPadding);
						_textBox.SetBounds(_textBoxXPadding, _textBoxYPadding, width, height, boundsSpecified);

						//offset the button and decrease it's hight so it fits inside the control's border
						const int comboButtonOffset = 1;
						int x = this.Width - (_comboButton.Width + comboButtonOffset);
						const int y = comboButtonOffset;
						int buttonHeight = this.Height - _comboButtonHeightAdjustment;
						_comboButton.SetBounds(x, y, _comboButton.Width, buttonHeight);

						SetTextboxPosition();

						_cachedSize = new Size(this.Width, comboBoxHeight);
					}
				}
				return _cachedSize;
			}

			public Size GetDesiredSize(Size availableSize)
			{
				return DoLayout();
			}

			#endregion

			#region IEditControlView members
			/// <summary>
			/// Implements <see cref="IEditControlView.DisplayedUIValue"/>
			/// </summary>
			public object DisplayedUIValue
			{
				set
				{
					Debug.Assert(value is string, "unexpected type from presenter.");

					string valueString = value as string;

					if (valueString == null)
						valueString = "";
					if (String.Compare(_textBox.Text, valueString, false, CultureInfo.CurrentUICulture) != 0)
					{
						_textBox.Text = valueString;
						OldValue = valueString;
					}
				}
				get
				{
					return _textBox.Text;
				}
			}

			Type IEditControlView.DisplayedUIValueType
			{
				get { return typeof(string); }
			}

			public Control Control
			{
				get { return this; }
			}

			public int DesiredHeight
			{
				get
				{
					return ControlInfo.Height;
				}
			}

			#endregion

			private void OnValueChanged(string textValue, out bool hasError)
			{
				object newValue = textValue;
				if (!_presenter.SetValueFromUI(this, ref newValue, out hasError)) return;

				// Handles the case when the typeconverter changes the string representation of hte value (e.g. user enters 1, typeconverter changes it to 1in)
				Debug.Assert(newValue is string, "unexpected type from presenter.");
				_textBox.Text = (string)newValue;
				OldValue = _textBox.Text;
			}

			private void ShowUITypeEditor()
			{
				IServiceProvider serviceProvider = _presenter.GetServiceProvider();
				IServiceContainer parentContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
				if (parentContainer == null)
				{
					Debug.Fail("Failed to obtain IServiceContainer service");
					return;
				}
				IServiceContainer localContainer = new System.ComponentModel.Design.ServiceContainer(parentContainer);
				localContainer.AddService(typeof(IWindowsFormsEditorService), new EditorService(this), false);

				string errorMessage;
				object tempValue = null;
				try
				{
					object valueFromString = _presenter.ConvertFromSafely(_textBox.Text, out errorMessage);
					tempValue = _presenter.UITypeEditorEditValue(localContainer, valueFromString);
					_hasActiveUITypeEditor = false;
				}
				catch (Exception ex)
				{
					errorMessage = ex.Message;
					_presenter.SetErrorMessage(errorMessage);
				}
				finally
				{
					string uiValue = _presenter.ConvertToSafely(tempValue, typeof(string)) as string;
					if (!string.IsNullOrEmpty(uiValue))
					{
						this.DisplayedUIValue = uiValue;
						bool error;
						OnValueChanged(uiValue, out error);
					}
					_comboButton.IsMouseDown = false;
					_textBox.Focus();
				}
			}

			#region ComboTextBox
			[ToolboxItem(false)] // hide it from the VS.NET 2003/2005 toolbox
			public class ComboTextBox : TextBoxEx
			{
				public ComboTextBox(IServiceProvider serviceProvider)
					: base(serviceProvider)
				{ }

				public event EventHandler AltDown;

				private void OnAltDown()
				{
					if (AltDown != null)
						AltDown(this, EventArgs.Empty);
				}

				protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
				{
					Keys keySet = Keys.Alt | Keys.Down;
					if (keyData == keySet)
					{
						OnAltDown();
						return true;
					}
					return base.ProcessCmdKey(ref msg, keyData);
				}

			}
			#endregion

			#region ComboButtonEx
			private class ComboButtonEx : ButtonEx
			{
				private bool _isMouseDown;
				private bool _isMouseOver;
				private bool _hasFocus;
				private Image _buttonBitmap;
				private bool _hasError = false;
				private readonly ControlRendering _controlRendering;

				public ComboButtonEx(IServiceProvider serviceProvider)
					: base(serviceProvider)
				{
					this.SetStyle(ControlStyles.ResizeRedraw, true);
					this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
					this.SetStyle(ControlStyles.UserPaint, true);
					this.FlatStyle = FlatStyle.Flat;
					_controlRendering = new ControlRendering(serviceProvider);

					try
					{
						_buttonBitmap = Resources.ComboDown;
					}
					catch
					{
						Debug.WriteLine("Unable to locate the ComboBox dropdown image.");
					}
				}

				protected override void Dispose(bool disposing)
				{
					if (disposing && _buttonBitmap != null)
					{
						_buttonBitmap.Dispose();
					}
					base.Dispose(disposing);
				}

				#region Properties
				/// <summary>
				/// Sets whether the mouse is over the control. Used to updating the button painting when the parent controls is moused over.
				/// </summary>
				public bool IsMouseOver
				{
					set
					{
						_isMouseOver = value;
						Invalidate();
					}
				}

				/// <summary>
				/// Gets or sets whether the control has focus. Used to keep the focus painting while they UITypedEditor form is open
				/// </summary>
				public bool HasFocus
				{
					get { return _hasFocus; }
					set
					{
						_hasFocus = value;
						Invalidate();
					}
				}

				/// <summary>
				/// Gets or sets whether the mouse is down. Use to keep the pressed painting style until the UITypeEditor form is closed.
				/// </summary>
				public bool IsMouseDown
				{
					get { return _isMouseDown; }
					set
					{
						_isMouseDown = value;
						Invalidate();
					}
				}

				public bool HasError
				{
					get { return _hasError; }
					set { _hasError = value; }
				}
				#endregion

				#region Handlers
				protected override void OnMouseLeave(EventArgs e)
				{
					base.OnMouseLeave(e);
					_isMouseOver = false;
					Invalidate();
				}

				protected override void OnMouseEnter(EventArgs e)
				{
					base.OnMouseEnter(e);
					_isMouseOver = true;
					Invalidate();
				}

				public const int WM_PAINT = 0x000F;

				protected override void WndProc(ref Message m)
				{
					if (m.Msg == WM_PAINT)
					{
						base.DefWndProc(ref m);
						if (FlatStyle == FlatStyle.Flat)
							Draw();
					}
					else base.WndProc(ref m);
				}

				protected override void OnClick(EventArgs e)
				{
					if (!HasError)
						base.OnClick(e);
				}
				#endregion

				private void Draw()
				{
					using (Graphics g = Graphics.FromHwnd(this.Handle))
					{
						Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
						if (_buttonBitmap != null)
						{
							_controlRendering.DrawButton(g, rect, _buttonBitmap, IsMouseDown, HasFocus | _isMouseOver,
								_isMouseOver, Enabled, FlatStyle == FlatStyle.Flat, _controlRendering.Colors);

							//							g.FillRectangle(SystemBrushes.Control, rect);
							//							Brush brush = this.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
							//							Point point = new Point(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2));
							//							if (this.RightToLeft == RightToLeft.Yes)
							//							{
							//								point.X -= rect.Width % 2;
							//							}
							//							else
							//							{
							//								point.X += rect.Width % 2;
							//							}
							//							Point[] points = new Point[] { new Point(point.X - 2, point.Y - 1), new Point(point.X + 3, point.Y - 1), new Point(point.X, point.Y + 2) };
							//							g.FillPolygon(brush, points);
						}
						if (FlatStyle == FlatStyle.Flat)
						{
							Color color = _controlRendering.Colors.ControlDark;
							if (_isMouseOver || HasFocus)
								color = _controlRendering.Colors.SelectedText;

							using (Pen pen = new Pen(color))
							{
								g.DrawLine(pen, rect.X, rect.Y,
									rect.X, rect.Height - rect.Y);

							}
						}

					}
				}


			}
			#endregion

			#region EditorService
			private sealed class EditorService : IWindowsFormsEditorService
			{
				private readonly UIEditorComboBoxControl _parent;
				private HostForm _hostForm;
				private bool closingDropDown;
				public EditorService(UIEditorComboBoxControl parent)
				{
					_parent = parent;
				}
				public void CloseDropDown()
				{
					if (closingDropDown)
						return;
					try
					{
						closingDropDown = true;
						if (_hostForm != null && _hostForm.Visible)
						{
							_hostForm.SetComponent(null);
							_hostForm.Visible = false;

							if (_parent.Visible)
								_parent.Focus();
						}
					}
					finally
					{
						closingDropDown = false;
					}
				}

				public void DropDownControl(Control control)
				{
					if (_hostForm == null)
						_hostForm = new HostForm(this);


					_hostForm.Visible = false;
					_hostForm.SetComponent(control);

					Size size = _hostForm.Size;

					Point location = _parent.PointToScreen(new Point(0, 0));

					_hostForm.SetBounds(location.X, location.Y + _parent.Height,
						_parent.Width, size.Height);

					_hostForm.Show(_parent);
					control.Focus();


					while (_hostForm.Visible)
					{
						Application.DoEvents();
					}
				}

				public DialogResult ShowDialog(Form dialog)
				{
					dialog.ShowDialog(_parent);
					return dialog.DialogResult;
				}

				#region HostForm
				private sealed class HostForm : Form
				{
					private Control currentControl;
					private readonly IWindowsFormsEditorService _service;
					public HostForm(IWindowsFormsEditorService service)
					{
						StartPosition = FormStartPosition.Manual;
						currentControl = null;
						ShowInTaskbar = false;
						ControlBox = false;
						MinimizeBox = false;
						MaximizeBox = false;
						Text = "";
						FormBorderStyle = FormBorderStyle.FixedToolWindow;
						Visible = false;
						_service = service;

					}

					protected override void OnClosed(EventArgs args)
					{
						if (Visible)
							_service.CloseDropDown();
						base.OnClosed(args);
					}

					protected override void OnDeactivate(EventArgs args)
					{
						if (Visible)
							_service.CloseDropDown();
						base.OnDeactivate(args);
					}

					protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
					{
						if (currentControl != null)
						{
							height = currentControl.Height;
							if (currentControl is ListBox)
							{
								height = ((ListBox)currentControl).ItemHeight * Math.Min(10, ((ListBox)currentControl).Items.Count);
							}
							currentControl.Height = height;
							currentControl.SetBounds(0, 0, width, height);
						}

						base.SetBoundsCore(x, y, width, height, specified);
					}

					public void SetComponent(Control value)
					{
						if (currentControl != null)
						{
							Controls.Remove(currentControl);
							currentControl = null;
						}
						if (value != null)
						{
							currentControl = value;
							//Set draw mode to activate custom drawing only in the smart panels.
							if (currentControl is ListBoxEx)
								((ListBoxEx)currentControl).DrawMode = DrawMode.OwnerDrawFixed;
							Controls.Add(currentControl);
							Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
							currentControl.Location = new Point(0, 0);
							currentControl.Visible = true;
						}
						Enabled = currentControl != null;
					}
				}
				#endregion
			}
			#endregion
		}

		#endregion

		#region class ComboBoxControl

		internal sealed class ComboBoxControl : ComboBoxEx, IEditControlView
		{
			private object _value;
			private readonly IPresenter _presenter;
			private string _oldValue;

			public ComboBoxControl(IPresenter presenter)
				: base(presenter.GetServiceProvider())
			{
				if (presenter == null)
					throw new ArgumentNullException("presenter");
				_presenter = presenter;
			}

			private void PopulateCombo()
			{
				if (this.Items.Count > 0)
					return;

				this.BeginUpdate();
				string currentValue = this.Text;

				if (_presenter.GetStandardValuesSupported())
				{
					if (_presenter.GetStandardValuesExclusive())
						this.DropDownStyle = ComboBoxStyle.DropDownList;
					else
						this.DropDownStyle = ComboBoxStyle.DropDown;

					//setting the style causes the combo to get populated
					if (this.Items.Count == 0)
					{
						TypeConverter.StandardValuesCollection values = _presenter.GetStandardValues();
						if (values != null && values.Count > 0)
						{
							foreach (Object oValue in values)
							{
								//stringValue = _typeConverter.ConvertTo(_typeDescriptorContext, CultureInfo.InvariantCulture, oValue, typeof (string)) as string;
								string stringValue = _presenter.ConvertToSafely(oValue, typeof(string)) as string;
								if (stringValue == null)
								{
									Debug.Fail("StandardValue cannot be converted to string?");
									stringValue = "";
								}
								this.Items.Add(stringValue);
							}
						}
					}
				}
				Text = currentValue;
				OldValue = currentValue;
				EndUpdate();
			}

			Control IEditControlView.Control
			{
				get { return this; }
			}

			int IEditControlView.DesiredHeight
			{
				get
				{
					return ControlInfo.Height;
				}
			}

			void IEditControlView.SetErrorState(bool errorOccured)
			{
				if (!errorOccured && OldValue != null)
				{
					((IEditControlView)this).DisplayedUIValue = OldValue;
				}
			}

			private string OldValue
			{
				get { return _oldValue; }
				set { _oldValue = value; }
			}

			object IEditControlView.DisplayedUIValue
			{
				get
				{
					return _value;
				}
				set
				{
					if (_value == value)
						return;

					_value = value;
					// now specify the equivelent string value in the UI:
					string valueString = "";
					try
					{
						valueString = _presenter.ConvertToSafely(value, typeof(string)) as string;
					}
					catch (Exception exception) //doing "catch all" here because we'll throw unhandled to user since we're in the UI here.
					{
						Debug.Fail("Failed to convert value to string:" + exception.Message);
					}
					if (valueString == null)
						valueString = "";
					Text = valueString;
					OldValue = valueString;
					//CR# 20671. The actual property value wasn't getting changed when the TypedValueEditor.Value property was used.
					bool hasError;
					OnValueChanged(valueString, out hasError);
				}
			}

			Type IEditControlView.DisplayedUIValueType
			{
				get { return typeof(string); }
			}

			private void OnValueChanged(string textValue, out bool hasError)
			{
				object refValue = textValue;
				if (_presenter.SetValueFromUI(this, ref refValue, out hasError))
				{
					//TODO: What if TypeConverter changed the display? I guess it shouldhn't happen if we have standardValues exclusive?
				}
			}

			protected override void OnEnter(EventArgs e)
			{
				//Fix CR# 20559. The clearing and re-adding the items was causing the text to get lost. Also, using the OnDropDown event was causing the combo to not dropdown the first time you clicked on it.
				PopulateCombo();
				base.OnEnter(e);
			}

			protected override void OnValidating(CancelEventArgs e)
			{
				base.OnValidating(e);
				bool hasError;
				OnValueChanged(Text, out hasError);
				e.Cancel = hasError;
			}

			protected override void OnSelectionChangeCommitted(EventArgs e)
			{
				base.OnSelectionChangeCommitted(e);
				bool hasError;
				OnValueChanged(SelectedItem as string, out hasError);
			}
		}

		#endregion

		#region class TextBoxControl

		internal sealed class TextBoxControl : TextBoxEx, IEditControlView
		{
			private readonly IPresenter _presenter;

			public TextBoxControl(IPresenter presenter)
				: base(presenter.GetServiceProvider())
			{
				if (presenter == null)
					throw new ArgumentNullException("presenter");
				_presenter = presenter;
			}

			private IPresenter Presenter
			{
				get { return _presenter; }
			}

			Control IEditControlView.Control
			{
				get { return this; }
			}

			public int DesiredHeight
			{
				get { return ControlInfo.Height; }
			}

			void IEditControlView.SetErrorState(bool errorOccured)
			{
			}

			protected override void OnValidating(CancelEventArgs e)
			{
				base.OnValidating(e);
				bool hasError;
				object newValue = Text;
				if (Presenter.SetValueFromUI(this, ref newValue, out hasError))
				{
					// Handles the case when the typeconverter changes the string representation of hte value (e.g. user enters 1, typeconverter changes it to 1in)
					Debug.Assert(newValue is string, "unexpected type returned from presenter.");
					Text = (string)newValue;
				}
				e.Cancel = hasError;
			}

			object IEditControlView.DisplayedUIValue
			{
				set
				{
					Debug.Assert(value != null && value is string);
					Text = (string)value;
				}
				get
				{
					return Text;
				}
			}

			Type IEditControlView.DisplayedUIValueType
			{
				get { return typeof(string); }
			}
		}

		#endregion

		#region class CheckBoxControl

		internal sealed class CheckBoxControl : CheckBoxEx, IEditControlView
		{
			private readonly IPresenter _presenter;

			public CheckBoxControl(IPresenter presenter)
				: base(presenter.GetServiceProvider())
			{
				_presenter = presenter;
				Text = string.Empty;
			}

			Type IEditControlView.DisplayedUIValueType
			{
				get { return typeof(bool); }
			}

			object IEditControlView.DisplayedUIValue
			{
				set
				{
					if (value != null && value is bool)
					{
						Checked = (bool)value;
					}
					else
					{
						Debug.Fail("Unexpected value type!");
					}
				}
				get
				{
					return Checked;
				}
			}

			Control IEditControlView.Control
			{
				get { return this; }
			}

			int IEditControlView.DesiredHeight
			{
				get { return ControlInfo.Height; }
			}

			void IEditControlView.SetErrorState(bool errorOccured)
			{
			}

			private void NotifyCheckedChanged()
			{
				object newValue = Checked;
				bool errorOccured;
				if (_presenter.SetValueFromUI(this, ref newValue, out errorOccured))
				{
					Debug.Assert(newValue is bool, "unexpected type returned from presenter!");
					Checked = (bool)newValue;
				}
			}

			protected override void OnCheckedChanged(EventArgs e)
			{
				base.OnCheckedChanged(e);
				NotifyCheckedChanged();
			}

			//TODO: why would we need bot OnValidated and OnCheckedChanged ??
			protected override void OnValidated(EventArgs e)
			{
				base.OnValidated(e);
				NotifyCheckedChanged();
			}
		}

		#endregion
	}
}
