using GrapeCity.ActiveReports.Drawing;
using PdfSharp.Drawing;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	partial class PdfContentGenerator
	{
		private class Font
		{
			private readonly XFont _fontInfo;

			public Font(XFont fontInfo)
			{
				_fontInfo = fontInfo;
			}
			
			public object Clone()
			{
				return new Font(_fontInfo);
			}

			public static implicit operator XFont(Font font)
			{
				return font._fontInfo;
			}
		}
	}
}
