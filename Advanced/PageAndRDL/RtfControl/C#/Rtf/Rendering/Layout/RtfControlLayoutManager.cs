using System;
using System.Drawing;
using System.Windows.Forms;

using GrapeCity.ActiveReports.Drawing.Gdi;
using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Samples.Rtf.Control;

namespace GrapeCity.ActiveReports.Samples.Rtf.Rendering.Layout
{
	public class RtfControlLayoutManager : ILayoutManager
	{
		private IReportItem _item;
		private RtfControl _control;
		private LayoutStatus _status = LayoutStatus.None;
		private SizeF _computedSize;
		
		#region ILayoutManager members

		public LayoutCapabilities Capabilities
		{
			get { return LayoutCapabilities.CanGrowVertically | LayoutCapabilities.CanShrinkVertically; }
		}
		
		public void Initialize(IReportItem forReportItem, ITargetDevice targetDevice)
		{
			_item = forReportItem;
			_control = _item as RtfControl;
			_computedSize = new SizeF(_item.Width.ToTwips(), _item.Height.ToTwips());
	
			ProcessGrow();
			ProcessShrink();
		}

		public LayoutResult Measure(LayoutContext ctx)
		{
			var content = ctx.ContentRange as RtfControlContentRange;
			
			if (ctx.VerticalLayout)
			{
				if (content == null)
				{
					var fitHeight = Math.Min(ctx.AvailableSize.Height, _computedSize.Height);
					var range = new Range(0, fitHeight, 0, -1);
					var rtf = GetNextRtf(range.Bottom);
					var info = new RtfContentInfo(rtf, range, _computedSize);

					content = new RtfControlContentRange(_control, info);
					_computedSize.Height = fitHeight;
				}
				else
				{
					var restHeight = content.Info.TotalSize.Height - content.EndVertRange;
					var fitHeight = Math.Min(ctx.AvailableSize.Height, restHeight);
					var rtf = GetNextRtf(content.EndVertRange + fitHeight);

					content = content.GetNextRange(fitHeight, rtf);
					_computedSize.Height = fitHeight;
				}
				
				_status |= LayoutStatus.ContinueVertically | LayoutStatus.SomeContent;
				
				if (content.isLastPage)
					return new LayoutResult(content, LayoutStatus.SomeContent | LayoutStatus.Complete, _computedSize);
			}

			return new LayoutResult(content, _status, _computedSize);
		}

		public LayoutResult Layout(LayoutContext ctx)
		{
			var content = ctx.ContentRange as RtfControlContentRange;
			return new LayoutResult(content, _status, _computedSize);
		}
		
		#endregion

		#region Helper methods
		
		private void ResizeBoxToContent(object sender, ContentsResizedEventArgs e)
		{
			var rtb = sender as RichTextBox;
			rtb.Height = e.NewRectangle.Height;
		}
		
		private SizeF GetNeededSize()
		{
			var rtf = _control.Rtf;
			var width = TwipsToPixels(_computedSize.Width);

			using (var box = new RichTextBoxFixed())
			{
				box.ContentsResized += ResizeBoxToContent;
				box.Width = width;
				box.Rtf = rtf;

				return new SizeF(PixelsToTwips(box.Size.Width), PixelsToTwips(box.Size.Height));
			}
		}
		
		private void ProcessGrow()
		{
			if (!_control.CanGrow)
				return;

			var actualSize = GetNeededSize();

			if (_computedSize.Height < actualSize.Height)
				_computedSize.Height = actualSize.Height;
		}

		private void ProcessShrink()
		{
			if (!_control.CanShrink)
				return;
			
			var actualSize = GetNeededSize();
			
			if (_computedSize.Height > actualSize.Height)
				_computedSize.Height = actualSize.Height;
		}

		private int _firstChar;
		private int _lastChar;
		private string GetNextRtf(float to)
		{
			var bottom = TwipsToPixels(to);

			using (var rtb = new RichTextBoxFixed())
			{
				rtb.ContentsResized += ResizeBoxToContent;
				rtb.Width = TwipsToPixels(_computedSize.Width);
				rtb.Rtf = _control.Rtf;

				_lastChar = rtb.GetCharIndexFromPosition(new Point(0, bottom + rtb.PreferredHeight));
				rtb.Select(_firstChar, _lastChar - _firstChar);

				_firstChar = _lastChar;
				
				return rtb.SelectedRtf;
			}
		}
		
		#endregion

		#region Conversion methods

		private readonly float TWIPS_PER_PIXEL = GlobalConstants.FTwipsPerInch / SafeGraphics.VerticalResolution;
		
		private int TwipsToPixels(float twips)
		{
			return (int) Math.Ceiling(twips / TWIPS_PER_PIXEL);
		}

		private float PixelsToTwips(int pixels)
		{
			return pixels * TWIPS_PER_PIXEL;
		}

		#endregion
	}
}
