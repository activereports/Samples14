using System;
using System.IO;
using GrapeCity.ActiveReports.Document;

namespace GrapeCity.ActiveReports.Calendar.Tests
{
	/// <summary>
	/// Provides some helper methods for testing Calendar
	/// </summary>
	public static class TestHelper
	{
		/// <summary>
		/// Loads and returns a <see cref="PageReport"/> for the specified rdl filename.
		/// </summary>
		/// <param name="rdlFileName">The name of the rdl file. The filename should be relative to the <see cref="TestSettings.RDLDirectory"/>.</param>
		/// <returns>The specified <see cref="PageReport"/>.</returns>
		public static PageReport GetPageReport(string rdlFileName)
		{
			FileInfo rdlFile = GetRdlFile(rdlFileName);
			if (rdlFile != null)
			{
				PageReport reportDef = new PageReport(rdlFile);
				return reportDef;
			}
			return null;
		}

		/// <summary>
		/// Returns the <see cref="PageDocument"/> to use in a rendering extension.
		/// </summary>
		public static PageDocument GetReport(string reportName)
		{
			PageReport reportDef = GetPageReport(reportName);
			return new PageDocument(reportDef);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rdlFileName"></param>
		/// <returns></returns>
		private static FileInfo GetRdlFile(string rdlFileName)
		{
			return new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "//..//..//Resources//"+ rdlFileName);
		}
	}
}
