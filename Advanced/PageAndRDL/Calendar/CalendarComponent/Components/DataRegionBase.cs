using System;
using System.Diagnostics;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;

namespace GrapeCity.ActiveReports.Calendar.Components
{
	/// <summary>
	/// Implements the boilerplate functionality of <see cref="IDataRegion" />.
	/// </summary>
	public abstract class DataRegionBase : ReportItemBase, IDataRegion
	{
		private CustomData _customData;

		/// <summary>
		/// Creates a new instance of <see cref="DataRegionBase" />.
		/// </summary>
		protected DataRegionBase() { }

		/// <summary>
		/// Provides a cached reference to the <see cref="GrapeCity.ActiveReports.Extensibility.Rendering.CustomData"/> value obtained via <see cref="Initialize"/>
		/// </summary>
		internal CustomData CustomData
		{
			get { return _customData; }
		}

		/// <summary>
		/// Initializes the report item.
		/// </summary>
		/// <param name="dataContext">Data context associated with this report item.</param>
		/// <param name="properties">Property bag associated with this report item.</param>
		public override void Initialize(IDataScope dataContext, IPropertyBag properties)
		{
			base.Initialize(dataContext, properties);

			Debug.Assert(dataContext is CustomData, "Expected IDataScope to be CustomData!");
			_customData = dataContext as CustomData;
		}

		#region IDataRegion implementation

		/// <summary>
		/// Gets which data set to use for this data region.
		/// </summary>
		public string DataSetName
		{
			get { return (string)PropertyBag.GetValue("DataSetName"); }
		}

		/// <summary>
		/// Gets message to display in the DataRegion when no rows of data are available.
		/// </summary>
		public string NoRowsMessage
		{
			get { return (string)PropertyBag.GetValue("NoRowsMessage"); }
		}

		/// <summary>
		/// Indicates if the DataRegion has no rows available.
		/// </summary>
		public bool NoRows
		{
			get { return GetValue("NoRows", false); }
		}

		/// <summary>
		/// Indicates the entire data region should be kept together on one page if possible.
		/// </summary>
		public new bool KeepTogether
		{
			get { return GetValue("KeepTogether", false); }
		}

		/// <summary>
		/// Indicates the report should page break at the start of the data region.
		/// </summary>
		public bool PageBreakAtStart
		{
			get { return GetValue("PageBreakAtStart", false); }
		}

		/// <summary>
		/// Indicates the report should page break at the end of the data region.
		/// </summary>
		public bool PageBreakAtEnd
		{
			get { return GetValue("PageBreakAtEnd", false); }
		}

		/// <summary>
		/// Returns a renderable textbox suitable for displaying <see cref="NoRowsMessage"/> for this instance.
		/// </summary>
		public ITextBox NoRowsTextBox
		{
			get { return null; }
		}

		/// <summary>
		/// Indicating whether a region is in it's own section.
		/// </summary>
		public bool NewSection
		{
			get { return GetValue("NewSection", false); }
		}

		/// <summary>
		/// Overflow name.
		/// </summary>
		public abstract string OverflowName { get; }

		#endregion

		private bool GetValue(string name, bool defaultValue)
		{
			object value = PropertyBag.GetValue(name);
			if (value != null)
			{
				if (value is bool)
				{
					return (bool)value;
				}
				string s = value as string;
				if (s != null)
				{
					if (string.Equals(s, "true", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
					if (string.Equals(s, "false", StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			return defaultValue;
		}
	}
}
