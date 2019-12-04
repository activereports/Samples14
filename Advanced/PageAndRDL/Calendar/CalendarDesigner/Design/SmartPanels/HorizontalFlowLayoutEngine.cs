using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels
{
	/// <summary>
	/// Represents the class that performs a simple horizontal layout engine.
	/// </summary>
	internal sealed class HorizontalFlowLayoutEngine : LayoutEngine
	{
		public static readonly HorizontalFlowLayoutEngine Instance;

		static HorizontalFlowLayoutEngine()
		{
			Instance = new HorizontalFlowLayoutEngine();
		}

		private HorizontalFlowLayoutEngine()
		{
		}

		public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			Control parent = (Control)container;

			// use DisplayRectangle so that parent.Padding is honored
			System.Drawing.Rectangle parentDisplayRectangle = parent.DisplayRectangle;
			Point nextControlLocation = parentDisplayRectangle.Location;

			int controlWidth = parentDisplayRectangle.Width;
			if (parent.Controls.Count > 0)
			{
				controlWidth /= parent.Controls.Count;
			}

			foreach (Control control in parent.Controls)
			{
				// only apply layout to visible controls
				if (!control.Visible) continue;

				// respect the margin of the control: shift over the left and the top.
				nextControlLocation.Offset(control.Margin.Left, control.Margin.Top);

				// set the autosized controls to their autosized heights.
				Size controlSize = control.Size;
				if (control.AutoSize)
				{
					int width = controlWidth - control.Margin.Horizontal;
					int height = parentDisplayRectangle.Size.Height - control.Margin.Vertical;
					Size size = control.GetPreferredSize(new Size(width, height));

					controlSize = new Size(
						Math.Min(size.Width, width),
						Math.Min(size.Height, height));
				}

				// sets the calculated bounds of the control
				control.SetBounds(nextControlLocation.X, nextControlLocation.Y,
					controlSize.Width, controlSize.Height);

				// increment X by the width of the control and the right margin.
				nextControlLocation.X += control.Width + control.Margin.Right;

				// move Y back to the display rectangle origin.
				nextControlLocation.Y = parentDisplayRectangle.Y;
			}

			// Optional: Return whether or not the container's 
			// parent should perform layout as a result of this 
			// layout. Some layout engines return the value of 
			// the container's AutoSize property.
			return false;
		}
	}
}
