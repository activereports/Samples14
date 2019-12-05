using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Design.DdrDesigner.Behavior;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.ActiveReports.PageReportModel;
using System.Runtime.InteropServices;
using System.Xml;
using GrapeCity.ActiveReports.Drawing.Gdi;
using Svg;

namespace GrapeCity.ActiveReports.Samples.Svg
{
	[Guid("ED362C95-D11C-4D98-ACF2-060F9A7FE68C")]
	[ToolboxBitmap(typeof(SvgDesigner), "SvgIcon.png")]
	[DisplayName("SvgImage")]
	public sealed class SvgDesigner : CustomReportItemDesigner, IValidateable
	{
		private DesignerVerbCollection _designerVerbCollection;

		public SvgDesigner()
		{
			AddProperty(this, x => x.Svg, CategoryAttribute.Data,
				new DescriptionAttribute(Properties.Resources.SvgDescription), new DisplayNameAttribute(Properties.Resources.SvgDisplayName),
				new EditorAttribute(typeof(MultilineStringEditor), typeof(UITypeEditor))
			);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_graphics.Dispose();
				if (_metafile != null)
					_metafile.Dispose();
				_metafile = null;
			}
			base.Dispose(disposing);
		}

		ValidationEntry[] IValidateable.Validate(ValidationContext context)
		{
			return new ValidationEntry[0];
		}

		public override DesignerVerbCollection Verbs
		{
			get { return _designerVerbCollection ?? (_designerVerbCollection = new DesignerVerbCollection()); }
		}

		#region Public properties

		public string Svg
		{
			get
			{
				var customProperty = ReportItem.CustomProperties["Svg"];
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
				_metafile = null;
				var customProperty = ReportItem.CustomProperties["Svg"];
				if (customProperty != null)
					ReportItem.CustomProperties.Remove(customProperty);
				customProperty = new CustomPropertyDefinition("Svg", value);
				ReportItem.CustomProperties.Add(customProperty);
			}
		}

		#endregion

		#region Design-time rendering

		private readonly Graphics _graphics = SafeGraphics.CreateReferenceGraphics();
		private Metafile _metafile;
		
		internal Metafile RenderedSvg
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Svg))
					return null;

				if (_metafile != null)
					return _metafile;
				
				var doc = new XmlDocument();
				doc.LoadXml(Svg);
				var svgDocument = SvgDocument.Open(doc);
					
				_metafile = new Metafile(_graphics.GetHdc(), ((ISvgBoundable)svgDocument).Bounds, MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
				try
				{
					using (var gfx = Graphics.FromImage(_metafile))
						svgDocument.Draw(gfx);
				}
				finally
				{
					_graphics.ReleaseHdc();
				}
				return _metafile;
			}
		}

		private SvgControlGlyph _controlGlyph;

		public override Glyph ControlGlyph
		{
			get { return _controlGlyph ?? (_controlGlyph = new SvgControlGlyph(ReportItem, this)); }
		}

		private sealed class SvgControlGlyph : ControlBodyGlyph
		{
			public SvgControlGlyph(ReportItem reportItem, SvgDesigner designer)
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
				try
				{
					var svg = ((SvgDesigner) ComponentDesigner).RenderedSvg;
					if (svg != null)
						pe.Graphics.DrawImage(svg, Bounds);
				}
				catch (Exception ex)
				{
					pe.Graphics.DrawString(ex.Message, SystemFonts.DefaultFont, SystemBrushes.ControlText, Bounds, StringFormat.GenericTypographic);
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
