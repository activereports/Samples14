using System;
using System.Drawing.Design;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.Design;

namespace GrapeCity.ActiveReports.Samples.CustomDataProviderUI
{
	public sealed class QueryEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}
		
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			CSVFileSelector fileSelector = new CSVFileSelector();
			edSvc.DropDownControl(fileSelector);
			string csvFileName =  fileSelector.CSVFileName;
			if(string.IsNullOrEmpty(csvFileName)) return string.Empty;
			if(!File.Exists(csvFileName)) return string.Empty;
			return GetCSVQuery(csvFileName);
		}
		
		/// <summary>
		/// Reads the content of the specified file and builds the CSV data provider query string.
		/// </summary>
		/// <param name="csvFileName">The full path of the CSV file.</param>
		/// <returns>CSV data provider query string.</returns>
		/// <remarks>
		/// This method just reads the file lines one by one and adds them to the query string.
		/// The first line of CSV query string should have the columns names and columns data types,
		/// so we run the special process for the first line.
		/// </remarks>
		private static string GetCSVQuery(string csvFileName)
		{
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(csvFileName);
				string ret = string.Empty;
				string currentLine;
				int line = 0;
				while ((currentLine = sr.ReadLine()) != null)
				{
					if (line == 0)
						ret += ProcessColumnsDefinition(currentLine) + "\r\n";
					else
						ret += currentLine + "\r\n";
					line++;
				}
				return ret;
			}
			catch (IOException)
			{
				return string.Empty;
			}
			finally
			{
				if (sr != null)
					sr.Close();
			}
		}

		/// <summary>
		///Reads the CSV data columns definition from the specified string and adjusts it if necessary.
		/// </summary>
		/// <param name="line">The string that contains the CSV data columns definition.</param>
		/// <returns>The CSV data columns definition that includes the data types definition.</returns>
		private static string ProcessColumnsDefinition(string line)
		{
			const string ColumnWithDataTypeRegex = @"[""]?\w+[\""]?\(.+\)";
			string[] columns = line.Split(new string[] {","}, StringSplitOptions.None);
			string ret = null;
			foreach(string column in columns)
			{
				if(!string.IsNullOrEmpty(ret))
					ret+= ",";
				if(!Regex.Match(column, ColumnWithDataTypeRegex).Success)
				{
					ret += column + "(string)";
				}
				else
				{
					ret += column;
				}
			}
			return ret;
		}
	}
}
