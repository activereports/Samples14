using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using GrapeCity.ActiveReports.Extensibility.Rendering;
using GrapeCity.ActiveReports.PageReportModel;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.Pdf
{
	/// <summary>
	/// PDF settings.
	/// </summary>
	public sealed class PdfSharpSettings : ISettings
	{
		private static readonly string _embedFonts = Resources.EmbedFonts;
		private  readonly string _startPage = Resources.StartPage;
		private  readonly string _endPage = Resources.EndPage;

		/// <summary>
		/// Initializes a new instance of the <see cref="PdfSharpSettings"/> class.
		/// </summary>
		public PdfSharpSettings()
		{
			EmbedFonts = true;
			EndPage = -1;
			Target = TargetDeviceKind.Export;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Settings"/> class.
		/// </summary>
		public PdfSharpSettings(NameValueCollection settings) : this()
		{
			((ISettings)this).ApplySettings(settings);
		}

		/// <summary>
		/// Embeds fonts or not.
		/// </summary>
		[DefaultValue(true)]
		[PdfDisplayName("EmbedFonts")]
		[PdfDescription("EmbedFontsDesc")]
		public bool EmbedFonts { get; set; }

		/// <summary>
		/// Start page number.
		/// </summary>
		[DefaultValue(0)]
		[PdfDisplayName("StartPage")]
		[PdfDescription("StartPageDesc")]
		public int StartPage { get; set; }

		/// <summary>
		/// End page number.
		/// </summary>
		[DefaultValue(-1)]
		[PdfDisplayName("EndPage")]
		[PdfDescription("EndPageDesc")]
		public int EndPage { get; set; }

		// just parsing
		internal TargetDeviceKind Target { get; private set; }

		#region ISettings implementation

		/// <summary>
		/// Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing the settings for the rendering extension.
		/// </summary>        
		NameValueCollection ISettings.GetSettings()
		{
			var settings = new NameValueCollection();
			if (!EmbedFonts)
				settings["EmbedFonts"] = bool.FalseString;
			if (StartPage != 0)
				settings["StartPage"] = StartPage.ToString(CultureInfo.InvariantCulture);
			if (EndPage != -1)
				settings["EndPage"] = EndPage.ToString(CultureInfo.InvariantCulture);
			if (Target != TargetDeviceKind.Export)
				settings["Target"] = Target.ToString();
			return settings;
		}

		/// <summary>
		/// Apply settings for the rendering extension.
		/// </summary>
		/// <param name="settings">Settings for the rendering extension.</param>
		void ISettings.ApplySettings(NameValueCollection settings)
		{
			settings = settings ?? new NameValueCollection();
			EmbedFonts = string.IsNullOrEmpty(settings.Get("EmbedFonts")) || settings.Get("EmbedFonts").Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase);
			int page;
			StartPage = int.TryParse(settings.Get("StartPage") ?? string.Empty, out page) ? page : 0;
			EndPage = int.TryParse(settings.Get("EndPage") ?? string.Empty, out page) ? page : -1;
			// just parsing
			Target = string.IsNullOrEmpty(settings.Get("Target")) ? TargetDeviceKind.Export : (TargetDeviceKind)Enum.Parse(typeof(TargetDeviceKind), settings.Get("Target"), true);
		}

		#endregion
	}
}
