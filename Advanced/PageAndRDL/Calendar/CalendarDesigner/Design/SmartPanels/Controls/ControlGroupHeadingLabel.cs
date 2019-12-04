using System;
using System.Drawing;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// A heading label used for control groups inside the smart panels.
	/// </summary>
	/// <remarks>This control should only be used with controls groups to create sub-sections.</remarks>
	internal sealed class ControlGroupHeadingLabel : LabelEx
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControlGroupHeadingLabel"/>
		/// </summary>
		public ControlGroupHeadingLabel(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			ControlRendering controlRendering = new ControlRendering(serviceProvider);
			ForeColor = controlRendering.Colors.ControlText;
			TextAlign = ContentAlignment.MiddleLeft;
			Font = new Font(ControlInfo.Font.FontFamily, ControlInfo.Font.Size, FontStyle.Bold);
			BorderEdgeStyle = EdgeStyle.Default;
			BorderEdges = GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Edges.Bottom;
		}
	}
}
