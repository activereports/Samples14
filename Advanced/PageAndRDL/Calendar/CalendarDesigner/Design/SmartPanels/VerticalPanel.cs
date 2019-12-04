using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI.Controls;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels
{
	/// <summary>
	/// Represents a <see cref="PanelEx"/> with vertical flow layout for nested controls.
	/// </summary>
	internal class VerticalPanel : PanelEx
	{
		public VerticalPanel()
		{
			Padding = System.Windows.Forms.Padding.Empty;
		}

		public override LayoutEngine LayoutEngine
		{
			get { return VerticalFlowLayoutEngine.Instance; }
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			int width = proposedSize.Width;
			int height = 0;

			Size sizeToAllocate = proposedSize;

			// flow vertical layout is presumed
			foreach (Control control in Controls)
			{
				Size controlSize = control.Size;
				if (control.AutoSize || (control.Dock != DockStyle.None))
				{
					controlSize = control.GetPreferredSize(sizeToAllocate);
				}

				int controlHeight = controlSize.Height + control.Margin.Vertical;
				sizeToAllocate.Height -= controlHeight;
				height += controlHeight;
			}

			return new Size(width, height);
		}
	}
}
