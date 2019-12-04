using System;
using System.Collections.Generic;
using System.Linq;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.PageReportModel;
using DataElementOutput = GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput;

namespace GrapeCity.ActiveReports.Calendar.Components
{
	/// <summary>
	/// Implements the boilerplate functionality of <see cref="IReportItem" />.
	/// </summary>
	public abstract class ReportItemBase : IReportItem, IToggleReceiver
	{
		private IDataScope _dataScope;
		private IPropertyBag _propertyBag;


		/// <summary>
		/// Gets <see cref="IDataScope" /> that this report item is in.
		/// </summary>
		protected IDataScope DataScope
		{
			get { return _dataScope; }
		}

		/// <summary>
		/// Gets <see cref="IPropertyBag" /> that associated with this report item.
		/// </summary>
		protected IPropertyBag PropertyBag
		{
			get { return _propertyBag; }
		}

		#region IServiceProvider Members

		/// <summary>
		/// Gets the service object of the specified type.
		/// </summary>
		/// <param name="serviceType">An object that specifies the type of service object to get.</param>
		/// <returns>A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
		public object GetService(Type serviceType)
		{
			var report = PropertyBag.GetValue("Report") as IReport;
			if (serviceType == typeof(IReport)) return report;
			return report != null ? report.GetService(serviceType) : null;
		}

		#endregion

		#region IRenderComponent Members

		/// <summary>
		/// Returns a collection of render components.
		/// </summary>
		public IEnumerable<IRenderComponent> RenderComponents
		{
			get { return Enumerable.Empty<IRenderComponent>(); }
		}

		#endregion

		#region IReportItem implementation

		/// <summary>
		/// Initializes the report item.
		/// </summary>
		/// <param name="dataContext">Data context associated with this report item.</param>
		/// <param name="properties">Property bag associated with this report item.</param>
		public virtual void Initialize(IDataScope dataContext, IPropertyBag properties)
		{
			if (properties == null)
				throw new ArgumentNullException("properties");

			_dataScope = dataContext;
			_propertyBag = properties;
		}

		/// <summary>
		/// Wrapper for the OnClick event.
		/// </summary>
		/// <param name="reportItem">The report item clicked.</param>
		/// <param name="xPosition">The x position of the click.</param>
		/// <param name="yPosition">The y position of the click.</param>
		/// <param name="imageMapId">The image map ID.</param>
		/// <param name="button">The button that was clicked.</param>
		/// <returns>The resulting change of the button click.</returns>
		public virtual ChangeResult OnClick(IReportItem reportItem, int xPosition, int yPosition, string imageMapId,
			MouseButton button)
		{
			return ChangeResult.None;
		}
		
		/// <summary>
		/// Gets the name of this report item.
		/// </summary>
		public string Name
		{
			get { return (string)this.PropertyBag.GetValue("Name"); }
		}

		/// <summary>
		/// The name to use for the data element or attribute for this report item when rendered via a data oriented rendering extension (e.g. XML).
		/// </summary>
		/// <remarks>
		/// By default this is the name of this report item.
		/// </remarks>
		public string DataElementName
		{
			get { return (string)this.PropertyBag.GetValue("DataElementName"); }
		}

		/// <summary>
		/// Indicates how this item should appear when rendered with a data oriented rendering device.
		/// </summary>
		public DataElementOutput DataElementOutput
		{
			get { return (DataElementOutput)this.PropertyBag.GetValue("DataElementOutput"); }
		}

		/// <summary>
		/// Gets the distance of the item from the top of the containing object.
		/// </summary>
		public Length Top
		{
			get { return (Length)this.PropertyBag.GetValue("Top"); }
		}

		/// <summary>
		/// Gets the distance of the item from the left of the containing object.
		/// </summary>
		public Length Left
		{
			get { return  (Length)this.PropertyBag.GetValue("Left"); }
		}

		/// <summary>
		/// Gets the width of the item.
		/// </summary>
		public Length Width
		{
			get { return  (Length)this.PropertyBag.GetValue("Width"); }
		}

		/// <summary>
		/// Gets the height of the item.
		/// </summary>
		public Length Height
		{
			get { return (Length)this.PropertyBag.GetValue("Height"); }
		}

		/// <summary>
		/// Gets a bookmark that can be linked to via a Bookmark action.
		/// </summary>
		public string Bookmark
		{
			get { return Convert.ToString(PropertyBag.GetValue("Bookmark")); }
		}

		/// <summary>
		/// Gets a textual label for the item.
		/// </summary>
		public string ToolTip
		{
			get { return Convert.ToString(PropertyBag.GetValue("ToolTip")); }
		}

		/// <summary>
		/// Provides a user friendly label for an instance of an item.
		/// </summary>
		public string Label
		{
			get { return Convert.ToString(PropertyBag.GetValue("Label")); }
		}

		/// <summary>
		/// Specifies drawing order of the report item within the containing object.
		/// </summary>
		public int ZIndex
		{
			get { return (int)this.PropertyBag.GetValue("ZIndex"); }
		}

		/// <summary>
		/// Gets the style of the report item.
		/// </summary>
		public IStyle Style
		{
			get { return (IStyle)this.PropertyBag.GetValue("Style"); }
		}

		/// <summary>
		/// Gets the original state of visibility.
		/// </summary>
		private bool GetOriginalVisibility()
		{
			return Convert.ToBoolean(this.PropertyBag.GetValue("Hidden"));
		}

		/// <summary>
		/// Indicates if this report item should be hidden.
		/// </summary>
		public bool Hidden
		{
			get
			{
				var hiddenFunc = PropertyBag.GetValue("HiddenFunc") as Func<bool>;

				if (hiddenFunc != null)
				{
					return hiddenFunc();
				}

				if (!_hidden.HasValue)
				{
					_hidden = GetOriginalVisibility();
				}
				return _hidden.Value;
			}
			set
			{
				_hidden = value;
			}
		}
		private bool? _hidden;

		/// <summary>
		/// The Action object of this report item.
		/// </summary>
		public virtual IAction Action
		{
			get { return null; }
		}

		/// <summary>
		/// Specifies a report item that users can click toggle the visibility of this item.
		/// </summary>
		public string ToggleItem
		{
			get { return (string)this.PropertyBag.GetValue("ToggleItem"); }
		}

		/// <summary>
		/// Determines if this item has dynamic visiblity and is hidden.
		/// </summary>
		public bool IsDynamicallyHidden
		{
			get { return (bool)PropertyBag.GetValue("IsDynamicallyHidden"); }
		}

		/// <summary>
		/// Gets style name
		/// </summary>
		public string StyleName
		{
			get { return (string)PropertyBag.GetValue("StyleName"); }
		}

		/// <summary>
		/// Defines output kinds in which report item is present (Screen, Export tec.)
		/// </summary>
		public TargetDeviceKind TargetDevice
		{
			get
			{
				return (TargetDeviceKind)PropertyBag.GetValue("Target");
			}
		}

		/// <summary>
		/// Gets a value indicating whether the report item is kept together on one page if possible.
		/// </summary>
		public bool KeepTogether
		{
			get { return (bool)PropertyBag.GetValue("KeepTogether"); }
		}

		#endregion
	}
}
