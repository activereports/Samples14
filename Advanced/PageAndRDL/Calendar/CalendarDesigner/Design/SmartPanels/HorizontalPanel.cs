using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI.Controls;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels
{
	/// <summary>
	/// Represents a <see cref="PanelEx"/> with horizontal flow layout for nested controls.
	/// </summary>
	internal sealed class HorizontalPanel : PanelEx
	{
		public HorizontalPanel()
		{
			Padding = System.Windows.Forms.Padding.Empty;
		}

		public override LayoutEngine LayoutEngine
		{
			get { return HorizontalFlowLayoutEngine.Instance; }
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			int width = proposedSize.Width;
			int height = 0;

			// flow horizontal layout is presumed
			foreach (Control control in Controls)
			{
				Size controlSize = control.GetPreferredSize(proposedSize);
				controlSize.Height = Math.Max(controlSize.Height, control.Height);
				height = Math.Max(height, controlSize.Height + control.Margin.Vertical);
			}

			return new Size(width, height);
		}
	}
}
