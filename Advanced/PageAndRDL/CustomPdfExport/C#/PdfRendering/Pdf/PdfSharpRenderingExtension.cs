using System;
using System.Collections.Specialized;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;
using GrapeCity.ActiveReports.Rendering.Export;
using GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.Pdf
{
	/// <summary>
	/// <see cref="IRenderingExtension" /> implementation used to create a PDF.
	/// </summary>
	public sealed class PdfSharpRenderingExtension : IRenderingExtension, IConfigurable
	{
		#region IRenderingExtension implementation

		/// <summary>
		/// Renders report.
		/// </summary>
		void IRenderingExtension.Render(IReport report, StreamProvider streamProvider)
		{
			((IRenderingExtension)this).Render(report, streamProvider, null);
		}

		/// <summary>
		/// Renders report.
		/// </summary>
		void IRenderingExtension.Render(IReport report, StreamProvider streamProvider, NameValueCollection settings)
		{
			if (report == null)
				throw new ArgumentNullException("report");
			if (streamProvider == null)
				throw new ArgumentNullException("streamProvider");

			settings = settings ?? new NameValueCollection();
			var pdfSettings = new PdfSharpSettings(settings);

			try
			{
				var primaryStream = streamProvider.GetPrimaryStream() ?? streamProvider.CreatePrimaryStream("application/pdf", ".pdf");
				using (var outputStream = primaryStream.OpenStream())
				using (var generator = new PdfGenerator(outputStream, pdfSettings.EmbedFonts))
				{
					var renderingCore = new DocumentRenderer(report, generator);
					int startPage = pdfSettings.StartPage;
					int endPage = Math.Max(startPage, pdfSettings.EndPage);
					if (startPage <= 0)
						startPage = 1;
					if (endPage <= 0)
						endPage = int.MaxValue;
					renderingCore.Render(pdfSettings.Target, false, new ImageRenderers.PageControl.PageControllerSettings() { EndPage = endPage, StartPage = startPage });
				}
			}
			catch
			{
				streamProvider.CleanUpOnError();
				throw;
			}
		}

		#endregion

		#region IConfigurable implementation

		/// <summary>
		/// Gets settings.
		/// </summary>
		ISettings IConfigurable.GetSupportedSettings()
		{
			return new PdfSharpSettings();
		}

		/// <summary>
		/// Gets settings.
		/// </summary>
		ISettings IConfigurable.GetSupportedSettings(bool fpl)
		{
			return new PdfSharpSettings();
		}

		#endregion
	}
}
