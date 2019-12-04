using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels
{
	/// <summary>
	/// Represents the class that performs a simple vertical layout engine.
	/// </summary>
	internal sealed class VerticalFlowLayoutEngine : LayoutEngine
	{
		public static readonly VerticalFlowLayoutEngine Instance;

		static VerticalFlowLayoutEngine()
		{
			Instance = new VerticalFlowLayoutEngine();
		}

		private VerticalFlowLayoutEngine()
		{
		}

		public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			Control parent = (Control)container;

			// use DisplayRectangle so that parent.Padding is honored
			System.Drawing.Rectangle parentDisplayRectangle = parent.DisplayRectangle;
			Point nextControlLocation = parentDisplayRectangle.Location;

			foreach (Control control in parent.Controls)
			{
				// only apply layout to visible controls
				if (!control.Visible) continue;

				// respect the margin of the control: shift over the left and the top.
				nextControlLocation.Offset(control.Margin.Left, control.Margin.Top);

				// set the autosized controls
				Size controlSize = control.Size;
				int width = parentDisplayRectangle.Size.Width - control.Margin.Horizontal;
				int height = parentDisplayRectangle.Size.Height - control.Margin.Vertical - nextControlLocation.Y;
				Size preferredSize = control.GetPreferredSize(new Size(width, height));
				if (control.Dock == DockStyle.Top)
				{
					controlSize = new Size(width, Math.Min(preferredSize.Height, height));
				}
				else if (control.Dock == DockStyle.Fill)
				{
					controlSize = new Size(width, height);
				}
				else if (control.AutoSize)
				{
					controlSize = new Size(
						preferredSize.Width,
						preferredSize.Height);
				}

				// NOTE: Control.SetBounds implementation will not set size if it's the same
				// sets the calculated bounds of the control
				control.SetBounds(nextControlLocation.X, nextControlLocation.Y,
					controlSize.Width, controlSize.Height);

				// move X back to the display rectangle origin.
				nextControlLocation.X = parentDisplayRectangle.X;

				// increment Y by the height of the control and the bottom margin.
				nextControlLocation.Y += control.Height + control.Margin.Bottom;
			}

			// Optional: Return whether or not the container's 
			// parent should perform layout as a result of this 
			// layout. Some layout engines return the value of 
			// the container's AutoSize property.
			return false;
		}
	}
}
