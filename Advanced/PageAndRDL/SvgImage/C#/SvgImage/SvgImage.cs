using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.PageReportModel;
using Svg;

namespace GrapeCity.ActiveReports.Samples.Svg
{
	public sealed class SvgImage : IReportItem, ICustomReportItem, IStaticItem, IReportItemRenderersFactory
	{
		#region Rendering

		TRenderer IReportItemRenderersFactory.GetRenderer<TRenderer>()
		{
			return typeof(TRenderer) == typeof(IGraphicsRenderer) ? (TRenderer)(IGraphicsRenderer)new SvgRendererBridge() : null;
		}

		private sealed class SvgRendererBridge : IGraphicsRenderer
		{
			void IGraphicsRenderer.Render(GraphicsRenderContext context, ILayoutArea area)
			{
				var reportItem = (SvgImage)area.ReportItem;
				string svg = Convert.ToString(reportItem._properties.GetValue("Svg")).Trim();

				var doc = new XmlDocument();
				try
				{
					doc.LoadXml(svg);
				}
				catch (XmlException)
				{
					throw new Exception(Properties.Resources.ExceptionMessage);
				}

				var svgDocument = SvgDocument.Open(doc);
				using (var renderer = new SvgRenderer(context, new RectangleF(area.Left, area.Top, area.Width, area.Height)))
					svgDocument.Draw(renderer);
			}
		}

		#endregion

		#region Default CRI implementation

		private IPropertyBag _properties;
		private IDataScope _dataScope;

		GrapeCity.ActiveReports.Extensibility.Rendering.CustomData ICustomReportItem.CustomData
		{
			get { return _dataScope as GrapeCity.ActiveReports.Extensibility.Rendering.CustomData; }
		}

		string ICustomReportItem.DataElementValue
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

		GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput IReportItem.DataElementOutput
		{
			get { return (GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput)(_properties.GetValue("DataElementOutput") ?? GrapeCity.ActiveReports.Extensibility.Rendering.Components.DataElementOutput.NoOutput); }
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
