using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.PageReportModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GrapeCity.ActiveReports.Samples.Radar
{
	public sealed class RadarChart : IDataRegion, ICustomReportItem, IStaticItem, IReportItemRenderersFactory
	{
		private const string SeriesValueName = "SeriesValue";

		#region Rendering

		TRenderer IReportItemRenderersFactory.GetRenderer<TRenderer>()
		{
			return typeof(TRenderer) == typeof(IImageRenderer) ? (TRenderer)(IImageRenderer)new ImageRenderer() : null;
		}

		private sealed class ImageRenderer : IImageRenderer
		{
			ImageInfo IImageRenderer.Render(ILayoutArea layoutArea, string mimeType, SizeF dpi)
			{
				var reportItem = layoutArea.ReportItem;

				var customData = ((ICustomReportItem) reportItem).CustomData;
				if (customData == null)
					return new ImageInfo();
				if (customData.DataRowGroupings == null || customData.DataRowGroupings.Count != 1)
					return new ImageInfo();
				var values = new List<double>(customData.DataRowGroupings[0].Count);
				// read the values from nested data members
				foreach (DataMember member in customData.DataRowGroupings[0])
				{
					double value;
					if (ReadValue(member, out value)) // we have to check that there is corresponding property to read series value
						values.Add(value);
				}

				var bounds = GetImageBounds(reportItem, layoutArea);
				if (dpi.IsEmpty) dpi = new SizeF(Resolution.Width, Resolution.Height);
				var imageSize = new Size(FromTwipsToPixels(bounds.Width, dpi.Width), FromTwipsToPixels(bounds.Height, dpi.Height));
				if (imageSize.Width == 0 || imageSize.Height == 0)
					return new ImageInfo();

				using (var nonScaledImage = new Bitmap(imageSize.Width, imageSize.Height))
				{
					nonScaledImage.SetResolution(dpi.Width, dpi.Height);
					using (var chart = new System.Windows.Forms.DataVisualization.Charting.Chart { Dock = DockStyle.Fill, Size = imageSize })
					{
						var chartArea = new ChartArea("Main");
						chart.ChartAreas.Add(chartArea);

						// Create series and add it to the chart
						var seriesColumns = new Series("Columns");
						foreach (var val in values)
							seriesColumns.Points.Add(Convert.ToDouble(val));
						seriesColumns.ChartType = SeriesChartType.Radar;

						chart.Series.Add(seriesColumns);
						chart.DrawToBitmap(nonScaledImage, new Rectangle(Point.Empty, imageSize));
					}
					var stream = new MemoryStream();
					nonScaledImage.Save(stream, ImageFormat.Png);
					stream.Flush();
					stream.Position = 0;
					return new ImageInfo(stream, "image/png");
				}
			}
			
			ImageInfo IImageRenderer.RenderBackground(ILayoutArea layoutArea, string mimeType, SizeF dpi)
			{
				return null;
			}

			private static bool ReadValue(DataMember dataMember, out double value)
			{
				if (dataMember == null)
					throw new ArgumentNullException("dataMember");
				value = 0;
				var labelProperty = dataMember.CustomProperties[SeriesValueName];
				if (labelProperty != null)
				{
					value = Convert.ToDouble(labelProperty.Value, CultureInfo.InvariantCulture);
					return true;
				}
				return false;
			}

			private static RectangleF GetImageBounds(IReportItem reportItem, ILayoutArea layoutArea)
			{
				return layoutArea != null
					? new RectangleF(layoutArea.Left, layoutArea.Top, layoutArea.Width, layoutArea.Height)
					: new RectangleF(reportItem.Left.ToTwips(), reportItem.Top.ToTwips(), reportItem.Width.ToTwips(), reportItem.Height.ToTwips());
			}

			private static int FromTwipsToPixels(float twips, float dpi)
			{
				const float TwipsPerInch = 1440f;
				return Convert.ToInt32(twips * dpi / TwipsPerInch);
			}
			
			private static SizeF? _resolution;

			private static SizeF Resolution
			{
				get
				{
					if (!_resolution.HasValue)
						using (var bitmap = new Bitmap(1, 1))
							_resolution = new SizeF(bitmap.HorizontalResolution, bitmap.VerticalResolution);
					return _resolution.Value;
				}
			}
		}

		#endregion

		#region Default CRI implementation

		private IPropertyBag _properties;
		private IDataScope _dataScope;

		Extensibility.Rendering.CustomData ICustomReportItem.CustomData
		{
			get { return _dataScope as Extensibility.Rendering.CustomData; }
		}

		string ICustomReportItem.DataElementValue
		{
			get { return null; }
		}

		string IOverflowItem.OverflowName
		{
			get { return (_properties.GetValue("OverflowName") ?? string.Empty).ToString(); }
		}

		bool ISectionRegion.NewSection
		{
			get { return Convert.ToBoolean(_properties.GetValue("NewSection") ?? false); }
		}

		bool IPageRegion.PageBreakAtStart
		{
			get { return Convert.ToBoolean(_properties.GetValue("PageBreakAtStart") ?? false); }
		}

		bool IPageRegion.PageBreakAtEnd
		{
			get { return Convert.ToBoolean(_properties.GetValue("PageBreakAtEnd") ?? false); }
		}

		string IDataRegion.DataSetName
		{
			get { return (_properties.GetValue("DataSetName") ?? string.Empty).ToString(); }
		}

		string IDataRegion.NoRowsMessage
		{
			get { return (_properties.GetValue("NoRowsMessage") ?? string.Empty).ToString(); }
		}

		bool IDataRegion.NoRows
		{
			get { return Convert.ToBoolean(_properties.GetValue("NoRows") ?? false); }
		}

		ITextBox IDataRegion.NoRowsTextBox
		{
			get { return null; }
		}

		void IReportItem.Initialize(IDataScope dataContext, IPropertyBag properties)
		{
			_dataScope = dataContext;
			_properties = properties;
		}

		ChangeResult IReportItem.OnClick(IReportItem reportItem, int xPosition, int yPosition, string imageMapId, MouseButton button)
		{
			return ChangeResult.None;
		}
		
		IAction IReportItem.Action
		{
			get { return null; }
		}

		string IReportItem.ToggleItem
		{
			get { return (_properties.GetValue("ToggleItem") ?? string.Empty).ToString(); }
		}

		int IReportItem.ZIndex
		{
			get { return Convert.ToInt32(_properties.GetValue("ZIndex") ?? 0); }
		}

		string IReportItem.Bookmark
		{
			get { return (_properties.GetValue("Bookmark") ?? string.Empty).ToString(); }
		}

		string IReportItem.Name
		{
			get { return (_properties.GetValue("Name") ?? string.Empty).ToString(); }
		}

		Length IReportItem.Width
		{
			get { return (Length)(_properties.GetValue("Width") ?? Length.Empty); }
		}

		Length IReportItem.Height
		{
			get { return (Length)(_properties.GetValue("Height") ?? Length.Empty); }
		}

		Length IReportItem.Top
		{
			get { return (Length)(_properties.GetValue("Top") ?? Length.Empty); }
		}

		Length IReportItem.Left
		{
			get { return (Length)(_properties.GetValue("Left") ?? Length.Empty); }
		}

		Extensibility.Rendering.Components.DataElementOutput IReportItem.DataElementOutput
		{
			get { return (Extensibility.Rendering.Components.DataElementOutput)(_properties.GetValue("DataElementOutput") ?? Extensibility.Rendering.Components.DataElementOutput.NoOutput); }
		}

		string IReportItem.DataElementName
		{
			get { return (_properties.GetValue("DataElementName") ?? string.Empty).ToString(); }
		}

		IStyle IReportItem.Style
		{
			get { return (IStyle)_properties.GetValue("Style"); }
		}

		string IReportItem.StyleName
		{
			get { return (_properties.GetValue("StyleName") ?? string.Empty).ToString(); }
		}

		bool IReportItem.Hidden
		{
			get { return Convert.ToBoolean(_properties.GetValue("Hidden") ?? false); }
		}

		bool IReportItem.IsDynamicallyHidden
		{
			get { return Convert.ToBoolean(_properties.GetValue("IsDynamicallyHidden") ?? false); }
		}

		string IReportItem.ToolTip
		{
			get { return (_properties.GetValue("ToolTip") ?? string.Empty).ToString(); }
		}

		TargetDeviceKind IReportItem.TargetDevice
		{
			get { return (TargetDeviceKind)(_properties.GetValue("Target") ?? TargetDeviceKind.All); }
		}

		bool IReportItem.KeepTogether
		{
			get { return Convert.ToBoolean(_properties.GetValue("KeepTogether") ?? false); }
		}

		IEnumerable<IRenderComponent> IRenderComponent.RenderComponents
		{
			get { return Enumerable.Empty<IRenderComponent>(); }
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			var definition = _properties.GetValue("ReportItemDefinition") as IServiceProvider;
			if (definition != null)
				return definition.GetService(serviceType);

			return null;
		}

		string IDocumentMapItem.Label
		{
			get { return _properties.GetValue("Label").ToString(); }
		}

		#endregion
	}
}
