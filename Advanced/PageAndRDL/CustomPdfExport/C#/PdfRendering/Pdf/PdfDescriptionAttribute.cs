using System.ComponentModel;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.Pdf
{
	internal class PdfDescriptionAttribute : DescriptionAttribute
	{
		public PdfDescriptionAttribute(string desctiption) : base()
		{
			DescriptionValue = Resources.ResourceManager.GetString(desctiption);
		}
	}
}
