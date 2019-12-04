using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using System.Drawing;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represents the type of a line style.
	/// </summary>
	public sealed class LineStyle
	{
		private const string DefaultWidth = "1pt";
		private const BorderStyle DefaultDashStyle = BorderStyle.Solid;

		/// <summary>
		/// Creates line with specified color.
		/// </summary>
		public LineStyle(Color color)
			: this(color, DefaultWidth, DefaultDashStyle)
		{
		}

		/// <summary>
		/// Creates solid line with specified color and width.
		/// </summary>
		public LineStyle(Color color, Length width, BorderStyle style)
		{
			_color = color;
			_width = width.IsValid ? width : DefaultWidth;
			_style = style;
		}

		/// <summary>
		/// Gets the color of current line.
		/// </summary>
		public Color LineColor
		{
			get { return _color; }
		}

		/// <summary>
		/// Gets the width of current line.
		/// </summary>
		public Length LineWidth
		{
			get { return _width; }
		}

		/// <summary>
		/// Gets the dash style of current line.
		/// </summary>
		public BorderStyle DashStyle
		{
			get { return _style; }
		}

		/// <summary>
		/// Creates the pen to draw by specifed line style.
		/// </summary>
		public static PenEx CreatePen(IDrawingCanvas canvas, LineStyle style)
		{
			PenEx pen = canvas.CreatePen(style.LineColor, style.LineWidth.ToTwips());

			switch (style.DashStyle)
			{
				case BorderStyle.None:
					pen.Color = Color.FromArgb(0, 0, 0, 0); // Transparent color
					break;
				case BorderStyle.Dotted:
					pen.DashStyle = PenStyleEx.Dot;
					pen.StartCap = LineCap.Round;
					pen.EndCap = LineCap.Round;
					break;
				case BorderStyle.Dashed:
					pen.DashStyle = PenStyleEx.Dash;
					break;
			}
			return pen;
		}

		private readonly Color _color;
		private readonly Length _width;
		private readonly BorderStyle _style;

		internal const string ColorPropertyName = "LineColor";
		internal const string WidthPropertyName = "LineWidth";
		internal const string StylePropertyName = "LineStyle";

		public static readonly Length MinWidth = new Length("0.25pt");
		public static readonly Length MaxWidth = new Length("20pt");
	}
}
