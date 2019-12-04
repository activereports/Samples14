using GrapeCity.ActiveReports.Design.DdrDesigner.Behavior;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Cursor = System.Windows.Forms.Cursor;

namespace GrapeCity.ActiveReports.Samples.Radar
{
	[Guid("2F06C3C6-A794-4803-A529-B43DC96019B4")]
	[ToolboxBitmap(typeof(RadarDesigner), "RadarIcon.png")]
	[DisplayName("RadarChart")]
	public sealed class RadarDesigner : CustomReportItemDesigner, IValidateable
	{
		private const string SeriesValueName = "SeriesValue";
		private DesignerVerbCollection _designerVerbCollection;
		public RadarDesigner()
		{
			// add new data set property
			AddProperty(this, x => x.DataSetName, CategoryAttribute.Data,
				new DescriptionAttribute(Properties.Resources.DataSetDescription), new DisplayNameAttribute(Properties.Resources.DataSetDisplayName),
				new TypeConverterAttribute(typeof(DataSetNamesConverter))
			);
			// add new series value property
			AddProperty(this, x => x.SeriesValue, CategoryAttribute.Data,
				new DescriptionAttribute(Properties.Resources.SeriesValueDescription), new DisplayNameAttribute(Properties.Resources.SeriesValueDisplayName),
				new TypeConverterAttribute(typeof(RadarValuesConverter))
			);
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			// create custom data if it's required, e.g. for a new item
			if (ReportItem.CustomData == null)
				ReportItem.CustomData = new CustomData();
			// we have to add a data grouping because it's required to edit group expressions and series value in smart panels.
			if (ReportItem.CustomData.DataRowGroupings.Count == 0)
				ReportItem.CustomData.DataRowGroupings.Add(new DataGrouping());
		}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (_designerVerbCollection == null)
				{
					_designerVerbCollection = new DesignerVerbCollection();
				}
				return _designerVerbCollection;
			}
		}

		ValidationEntry[] IValidateable.Validate(ValidationContext context)
		{
			return new ValidationEntry[0];
		}
		
		#region Public properties

		public string DataSetName
		{
			get
			{
				var customProperty = ReportItem.CustomProperties["DataSetName"];
				if (customProperty != null)
				{
					var expValue = customProperty.Value;
					if (expValue.IsConstant)
						return expValue.ToString();
				}
				return string.Empty;
			}
			set
			{
				var customProperty = ReportItem.CustomProperties["DataSetName"];
				if (customProperty != null)
					ReportItem.CustomProperties.Remove(customProperty);
				customProperty = new CustomPropertyDefinition("DataSetName", value);
				ReportItem.CustomProperties.Add(customProperty);
			}
		}

		public ExpressionInfo SeriesValue
		{
			get
			{
				if (ReportItem.CustomData.DataRowGroupings.Count > 0)
				{
					var property = ReportItem.CustomData.DataRowGroupings[0].CustomProperties[SeriesValueName];
					if (property != null)
						return property.Value;
				}
				return ExpressionInfo.FromString(string.Empty);
			}
			set
			{
				// find series value property
				var grouping = ReportItem.CustomData.DataRowGroupings[0];
				var property = grouping.CustomProperties[SeriesValueName];
				if (value == null || ExpressionInfo.FromString(string.Empty) == value) // default value: empty expression
				{
					// if value is empty expression then we should reset the property.
					if (property != null)
						ReportItem.CustomData.DataRowGroupings[0].CustomProperties.Remove(property);
				}
				else // otherwise we should set the custom property
				{
					// if it's required then the property must be created
					if (property == null)
					{
						property = new CustomPropertyDefinition(SeriesValueName, value);
						grouping.CustomProperties.Add(property);
					}
					// set the value to the property
					property.Value = value;
				}
			}
		}
		
		private static object GetServiceFromTypeDescriptorContext(Type serviceType, ITypeDescriptorContext context)
		{
			var service = context.GetService(serviceType);
			IDesignerHost host;
			if (service == null && (host = context.Container as IDesignerHost) != null)
				service = host.GetService(serviceType);
			var component = context.Instance as IComponent;
			if (service == null && component != null && component.Site != null)
				service = component.Site.GetService(serviceType);
			return service;
		}

		private static Report GetReportFromServiceProvider(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
				return null;
			var host = serviceProvider as IDesignerHost ?? serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (host == null)
				return null;
			var reportDef = host.RootComponent as PageReport;
			return reportDef == null ? null : reportDef.Report;
		}

		public class RadarValuesConverter : StringConverter
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				var stringCollection = new StringCollection();
				if (context == null)
					return new StandardValuesCollection(stringCollection);
				var host = GetServiceFromTypeDescriptorContext(typeof(IDesignerHost), context) as IDesignerHost;
				if (host == null)
					return new StandardValuesCollection(stringCollection);
				var report = GetReportFromServiceProvider(host);
				if (report == null)
					return new StandardValuesCollection(stringCollection);
				var selectionService = GetServiceFromTypeDescriptorContext(typeof(ISelectionService), context) as ISelectionService;
				if (selectionService == null)
					return new StandardValuesCollection(stringCollection);
				var component = selectionService.PrimarySelection as IReportComponent;
				if (component == null)
					return new StandardValuesCollection(stringCollection);
				var radar = component as CustomReportItem;
				if (radar == null)
					return new StandardValuesCollection(stringCollection);
				var dataSetName = radar.CustomProperties["DataSetName"];
				if (dataSetName == null || string.IsNullOrEmpty(dataSetName.Value))
					return new StandardValuesCollection(stringCollection);
				foreach (DataSet dataSet in report.DataSets)
					if (dataSet.Name == dataSetName.Value)
						foreach (Field field in dataSet.Fields)
							stringCollection.Add(ExpressionInfo.FromString(string.Format(CodeGenerator.IsValidLanguageIndependentIdentifier(field.Name) ? "=Fields!{0}.Value" : "=Fields.Item(\"{0}\").Value", field.Name)));
				return new StandardValuesCollection(stringCollection);
			}
			
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				if (sourceType == typeof(string))
					return true;
				return base.CanConvertFrom(context, sourceType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
					return ExpressionInfo.FromString(value as string);
				return base.ConvertFrom(context, culture, value);
			}
		}

		private sealed class DataSetNamesConverter : StringConverter
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				var stringCollection = new StringCollection();
				if (context == null)
					return new StandardValuesCollection(stringCollection);
				var host = GetServiceFromTypeDescriptorContext(typeof(IDesignerHost), context) as IDesignerHost;
				if (host == null)
					return new StandardValuesCollection(stringCollection);
				var report = GetReportFromServiceProvider(host);
				if (report == null)
					return new StandardValuesCollection(stringCollection);
				foreach (DataSet dataSet in report.DataSets)
					stringCollection.Add(dataSet.Name);
				return new StandardValuesCollection(stringCollection);
			}
		}

		#endregion

		#region Design-time rendering

		private RadarControlGlyph _controlGlyph;

		public override Glyph ControlGlyph
		{
			get { return _controlGlyph ?? (_controlGlyph = new RadarControlGlyph(ReportItem, this)); }
		}

		private sealed class RadarControlGlyph : ControlBodyGlyph
		{
			public RadarControlGlyph(ReportItem reportItem, RadarDesigner designer)
				: base(designer.BehaviorService, designer)
			{
				Behavior = new MovableBehavior(reportItem, this);
			}

			public override void Paint(PaintEventArgs pe)
			{
				if (PaintSelectionOnly(pe))
				{
					base.Paint(pe);
					return;
				}

				pe.Graphics.FillRectangle(BackgroundBrush, Bounds);
				using (var nonScaledImage = new Bitmap(Bounds.Width, Bounds.Height))
				{
					nonScaledImage.SetResolution(pe.Graphics.DpiX, pe.Graphics.DpiY);
					using (var chart = new System.Windows.Forms.DataVisualization.Charting.Chart { Dock = DockStyle.Fill, Size = Bounds.Size })
					{
						var chartArea = new ChartArea("Main");
						chart.ChartAreas.Add(chartArea);

						// Create series and add it to the chart
						var seriesColumns = new Series("RandomColumns");
						// Add 10 random values to the series
						var random = new Random(0);
						for (int i = 0; i < 10; i++)
							seriesColumns.Points.Add(random.Next(100));
						seriesColumns.ChartType = SeriesChartType.Radar;

						chart.Series.Add(seriesColumns);
						chart.DrawToBitmap(nonScaledImage, new Rectangle(Point.Empty, Bounds.Size));
					}
					pe.Graphics.DrawImageUnscaled(nonScaledImage, Bounds.Location);
				}
				base.Paint(pe);
			}

			private sealed class MovableBehavior : DefaultControlBehavior
			{
				private readonly ReportItem _reportItem;
				private readonly Glyph _glyph;

				public MovableBehavior(ReportItem reportItem, Glyph glyph)
				{
					_reportItem = reportItem;
					_glyph = glyph;
				}

				public override Cursor Cursor
				{
					get
					{
						return CanMoveGlyph(_glyph) && IsActiveLayerItem(_reportItem) ? Cursors.SizeAll : base.Cursor;
					}
				}

				private static bool IsActiveLayerItem(ReportItem item)
				{
					if (item == null || item.Site == null)
						return true;
					var host = item.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (host == null)
						return true;
					var reportDef = host.RootComponent as PageReport;
					if (reportDef == null)
						return true;
					var reportDesigner = host.GetDesigner(reportDef.Report) as ReportDesigner;
					if (reportDesigner == null)
						return true;
					var activeLayer = reportDesigner.ActiveLayer;
					return string.Equals(activeLayer, item.LayerName, StringComparison.InvariantCultureIgnoreCase);
				}
			}
		}

		#endregion
	}
}
