using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using GrapeCity.ActiveReports.Extensions;


namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Defines a common base class for shadow properties descriptors
	/// </summary>
	////[DoNotObfuscateType]
	internal class ShadowProperty : PropertyDescriptor
	{
		private readonly PropertyDescriptor wrappedDescriptor;
		private readonly AttributeCollection attributes;
		private bool? _hasDefaultValue;
		private object _defaultValue;
		private readonly bool _useTransaction;

		/// <summary>
		/// Allows creation of a new ShadowProperty using specified parameters
		/// </summary>
		public ShadowProperty(string displayName, PropertyDescriptor fromDescriptor, bool useTransaction = false)
			: base(fromDescriptor)
		{
			this.wrappedDescriptor = fromDescriptor;
			this.displayName = displayName;
			this.attributes = fromDescriptor.Attributes;
			_useTransaction = useTransaction;
		}

		public override AttributeCollection Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>
		/// Gets the name of the property
		/// </summary>
		public override string DisplayName { get { return displayName; } }
		private readonly String displayName;

		/// <summary>
		/// Specifies that this property is not read-only
		/// </summary>
		public override bool IsReadOnly { get { return this.wrappedDescriptor.IsReadOnly; } }

		/// <summary>
		/// Specifies if resetting an object changes its value.
		/// </summary>
		public override bool CanResetValue(object component)
		{
			if (HasDefaultValue)
			{
				return !Equals(DefaultValue, this.GetValue(component));
			}
			return this.wrappedDescriptor.CanResetValue(component);
		}

		/// <summary>
		/// Sets the value of the component property to a different value
		/// </summary>
		public override void SetValue(object component, object value)
		{
			var transaction = _useTransaction ? CreateTransaction(component as IComponent) : null;

			this.wrappedDescriptor.SetValue(component, value);

			if (transaction != null)
				transaction.Commit();
		}

		/// <summary>
		/// Gets a current value of the property on component
		/// </summary>
		public override object GetValue(object component)
		{
			return this.wrappedDescriptor.GetValue(component);
		}

		/// <summary>
		/// Gets a type of the component this property is bound to
		/// </summary>
		public override Type ComponentType
		{
			get
			{
				return this.wrappedDescriptor.ComponentType;
			}
		}

		/// <summary>
		/// Gets the property type
		/// </summary>
		public override Type PropertyType
		{
			get
			{
				return this.wrappedDescriptor.PropertyType;
			}
		}

		/// <summary>
		/// Determines if the value of the property needs to be persisted
		/// </summary>
		public override bool ShouldSerializeValue(object component)
		{
			if (HasDefaultValue)
			{
				return !Equals(DefaultValue, this.GetValue(component));
			}
			return this.wrappedDescriptor.ShouldSerializeValue(component);
		}

		/// <summary>
		/// Returns true if this property specifies a default value.
		/// </summary>
		private bool HasDefaultValue
		{
			get
			{
				if (!_hasDefaultValue.HasValue)
				{
					DefaultValueAttribute defaultValueAtt = this.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
					if (defaultValueAtt != null)
						_hasDefaultValue = true;
				}
				return _hasDefaultValue.HasValue && _hasDefaultValue.Value;
			}
		}

		/// <summary>
		/// When <see cref="HasDefaultValue"/> is true, returns the default value of this property.
		/// </summary>
		private object DefaultValue
		{
			get
			{
				if (_defaultValue == null)
				{
					DefaultValueAttribute defaultValueAtt = this.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
					if (defaultValueAtt != null)
						_defaultValue = defaultValueAtt.Value;
				}
				return _defaultValue;
			}
		}

		private static DesignerTransaction CreateTransaction(IComponent component)
		{
			var host = component != null && component.Site != null ? component.Site.GetService<IDesignerHost>() : null;
			return host != null ? host.CreateTransaction() : null;
		}

		/// <summary>
		/// Resets the value of the component property to a default value
		/// </summary>
		public override void ResetValue(object component)
		{
			if (HasDefaultValue)
			{
				this.wrappedDescriptor.SetValue(component, this.DefaultValue);
				return;
			}
			this.wrappedDescriptor.ResetValue(component);
		}
	}
}
