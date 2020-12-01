using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Samples.Rtf.Rendering;
using GrapeCity.ActiveReports.Samples.Rtf.Rendering.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;

namespace GrapeCity.ActiveReports.Samples.Rtf.Control
{
	[LayoutManager(typeof(RtfControlLayoutManager))]
	public sealed class RtfControl : ICustomReportItem, IStaticItem, IReportItemRenderersFactory
	{
		#region Rendering

		TRenderer IReportItemRenderersFactory.GetRenderer<TRenderer>()
		{
			return typeof(TRenderer) == typeof(IImageRenderer) ? (TRenderer)(IImageRenderer)new RtfRendererBridge() : null;
		}


		private sealed class RtfRendererBridge : IImageRenderer
		{
			public ImageInfo Render(ILayoutArea area, string mimeType, SizeF dpi)
			{
				var content = area.ContentRange as RtfControlContentRange;
				var rtf = content.Info.Rtf;

				var sizeInInches = new SizeF(area.Width / 1440, area.Height / 1440);
				var stream = RtfRenderer.RenderToStream(rtf, sizeInInches);

				return new ImageInfo(stream, "image/emf");
			}

			public ImageInfo RenderBackground(ILayoutArea layoutArea, string mimeType, SizeF dpi)
			{
				return null;
			}
		}

		#endregion

		#region Default CRI implementation

		private IPropertyBag _properties;
		private IDataScope _dataScope;

		public string Rtf
		{
			get { return _properties.GetValue("Rtf")?.ToString() ?? string.Empty; }
		}

		public bool CanGrow
		{
			get { return Convert.ToBoolean(_properties.GetValue("CanGrow")); }
		}

		public bool CanShrink
		{
			get { return Convert.ToBoolean(_properties.GetValue("CanShrink")); }
		}

		Extensibility.Rendering.CustomData ICustomReportItem.CustomData
		{
			get { return _dataScope as Extensibility.Rendering.CustomData; }
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
