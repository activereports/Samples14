using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using GrapeCity.ActiveReports.Rendering;
using GrapeCity.ActiveReports.Drawing.Gdi;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.Behavior;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.ActiveReports.Samples.Rtf.Rendering;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Samples.Rtf
{
	[Guid("2FE2AE6A-254A-4015-BF62-C2E669C67F39")]
	[ToolboxBitmap(typeof(RtfDesigner), "RtfIcon.bmp")]
	[DisplayName("RichTextBox")]
	public sealed class RtfDesigner : CustomReportItemDesigner, IValidateable
	{
		private const string RTF_FIELD_NAME = "Rtf";
		private const string CAN_GROW_FIELD_NAME = "CanGrow";
		private const string CAN_SHRINK_FIELD_NAME = "CanShrink";

		private DesignerVerbCollection _designerVerbCollection;
		private readonly RtfEditor _editor;
		private bool IsActive
		{
			get { return _editor.IsActive; }
		}

		public RtfDesigner()
		{
			AddProperty(this, x => x.Rtf,
				CategoryAttribute.Data,
				new DisplayNameAttribute(Properties.Resources.PropertyRtf),
				new DescriptionAttribute(Properties.Resources.PropertyRtfDescription),
				new EditorAttribute(typeof(MultilineStringEditor),
				typeof(UITypeEditor))
			);
			
			AddProperty(this, x => x.CanGrow,
				CategoryAttribute.Layout,
				new DisplayNameAttribute(Properties.Resources.PropertyCanGrow),
				new DescriptionAttribute(Properties.Resources.PropertyCanGrowDescription)
			);
			
			AddProperty(this, x => x.CanShrink,
				CategoryAttribute.Layout,
				new DisplayNameAttribute(Properties.Resources.PropertyCanShrink),
				new DescriptionAttribute(Properties.Resources.PropertyCanShrinkDescription)
			);

			_editor = new RtfEditor(this)
			{
				Name = "RTBFEditor",
				Multiline = true,
				AllowDrop = false,
				BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
				ScrollBars = RichTextBoxScrollBars.Both
			};
		}

		public new CustomReportItem ReportItem
		{
			get { return base.ReportItem; }
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
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

		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			InitializeShadowProperties();
			InitializeCustomProperties();

			SetRtf(ReportItem.GetCustomPropertyAsString(RTF_FIELD_NAME));
		}
		
		protected override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			base.OnComponentChanged(sender, e);

			var memberNotNull = e != null && e.Member != null;

			if (UndoService.UndoInProgress || memberNotNull && e.Member.Name == "ComponentSize")
				_metafile = null;

			if (memberNotNull && e.Member.Name == "Rtf")
				SetRtf(Rtf);
		}

		#region Public properties
		
		public string Rtf { get; set; }
		
		public string GetRtf()
		{
			var prop = TypeDescriptor.GetProperties(Component)[RTF_FIELD_NAME];
			var rtf = prop.GetValue(Component);

			if (rtf != null)
				return rtf.ToString();

			return string.Empty;
		}

		public void SetRtf(string rtf)
		{
			_metafile = null;

			var prop = TypeDescriptor.GetProperties(Component)[RTF_FIELD_NAME];
			var result = string.Equals(rtf, ExpressionInfo.EmptyString.ToString()) ? string.Empty : rtf;
			
			prop.SetValue(Component, result);
			ReportItem.CustomProperties[RTF_FIELD_NAME].Value = result;
		}
		
		[DefaultValue(false)]
		public bool CanGrow
		{
			get { return ReportItem.GetCustomPropertyAsBoolean(CAN_GROW_FIELD_NAME, false); }
			set { ReportItem.SetCustomProperty(CAN_GROW_FIELD_NAME, value.ToString()); }
		}

		[DefaultValue(false)]
		public bool CanShrink
		{
			get { return ReportItem.GetCustomPropertyAsBoolean(CAN_SHRINK_FIELD_NAME, false); }
			set { ReportItem.SetCustomProperty(CAN_SHRINK_FIELD_NAME, value.ToString()); }
		}
		
		#endregion

		#region Design-time rendering

		private System.Drawing.Image _metafile;
		private RtfControlGlyph _controlGlyph;

		internal System.Drawing.Image RenderedRtf
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Rtf))
					return null;

				if (_metafile != null)
					return _metafile;

				_metafile = RtfRenderer.RenderMetafile(Rtf, new SizeF(ReportItem.Width.ToInches(), ReportItem.Height.ToInches()));

				return _metafile;
			}
		}

		public override Glyph ControlGlyph
		{
			get { return _controlGlyph ?? (_controlGlyph = new RtfControlGlyph(ReportItem, this)); }
		}

		public void RePaint()
		{
			_metafile = null;
		}

		private sealed class RtfControlGlyph : ControlBodyGlyph
		{
			private readonly ReportItem _reportItem;

			public RtfControlGlyph(ReportItem reportItem, RtfDesigner designer)
				: base(designer.BehaviorService, designer)
			{
				_reportItem = reportItem;
				Behavior = new RtfBehavior(reportItem, this, designer);
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
					var designer = (RtfDesigner)ComponentDesigner;
					var meta = designer.RenderedRtf;

					if (meta != null)
					{
						pe.Graphics.IntersectClip(Bounds);
						pe.Graphics.DrawImage(meta, new RectangleF(Bounds.X, Bounds.Y,
							_reportItem.Width.ToInches() * SafeGraphics.HorizontalResolution * designer.ConversionService.ScalingFactor,
							_reportItem.Height.ToInches() * SafeGraphics.VerticalResolution * designer.ConversionService.ScalingFactor));
					}
				}
				catch (Exception ex)
				{
					pe.Graphics.DrawString(ex.Message, SystemFonts.DefaultFont, SystemBrushes.ControlText, Bounds, StringFormat.GenericTypographic);
				}

				base.Paint(pe);
			}

			public override bool ContainsFocusedEditor(Point point)
			{
				var designer = ComponentDesigner as RtfDesigner;

				return designer.Controls.Count == 1 &&
						designer.Controls[0].Focused &&
						designer.ControlGlyph.Bounds.Contains(point);
			}

			private sealed class RtfBehavior : DefaultControlBehavior
			{
				private readonly ReportItem _reportItem;
				private readonly Glyph _glyph;
				private readonly RtfDesigner _designer;

				private static readonly ICollection<Keys> _unTrackedKeys = new[]
				{
					Keys.Escape, Keys.Delete, Keys.ControlKey, Keys.ShiftKey,
					Keys.Apps, Keys.Back, Keys.Insert, Keys.LWin, Keys.RWin,
					Keys.PageDown, Keys.PageUp, Keys.Home, Keys.End,
					Keys.Left, Keys.Up, Keys.Right, Keys.Down
				};

				public RtfBehavior(ReportItem reportItem, Glyph glyph, RtfDesigner designer)
				{
					_reportItem = reportItem;
					_glyph = glyph;
					_designer = designer;
				}

				public override Cursor Cursor
				{
					get { return CanMoveGlyph(_glyph) && IsActiveLayerItem(_reportItem) ? Cursors.SizeAll : _designer.IsActive ? _designer._editor.Cursor : base.Cursor; }
				}

				public override bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point location)
				{
					if (button == MouseButtons.Left && g.Bounds.Contains(location) && _designer.Focused)
						_designer._editor.Activate();

					return base.OnMouseDoubleClick(g, button, location);
				}

				public override void OnKeyDown(Glyph g, KeyEventArgs e)
				{
					base.OnKeyDown(g, e);

					var editor = _designer._editor;

					if (editor.IsActive || _unTrackedKeys.Contains(e.KeyCode))
						return;

					editor.Activate();
					e.Handled = true;
				}

				protected override bool DisableSizeMoveCapabilities(Glyph glyph, ReportComponentDesigner designer)
				{
					return _designer.IsActive;
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

		#region Helper methods

		private void InitializeCustomProperties()
		{
			InitializeCustomProperty(RTF_FIELD_NAME);
			InitializeCustomProperty(CAN_GROW_FIELD_NAME, bool.FalseString);
			InitializeCustomProperty(CAN_SHRINK_FIELD_NAME, bool.FalseString);
		}

		private void InitializeCustomProperty(string propertyName, string defaultValue = "")
		{
			var customProp = ReportItem.CustomProperties[propertyName];
			if (customProp != null)
				return;
			
			customProp = new CustomPropertyDefinition(propertyName, defaultValue);
			ReportItem.CustomProperties.Add(customProp);
		}

		#endregion
	}
}
