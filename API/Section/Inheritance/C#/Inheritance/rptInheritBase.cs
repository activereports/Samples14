using System;
using System.IO;
using System.Resources;

namespace GrapeCity.ActiveReports.Sample.Inheritance
{
	
	/// Base class that implements the ability to load the csv file.
	public partial class rptInheritBase : GrapeCity.ActiveReports.SectionReport
	{
		//Path to the csv file.
		private String _csvPath;

		//Stream to load the csv file.
		private StreamReader _invoiceFileStream;

		//String array to store the data.
		private string [] _fieldNameArray;

		ResourceManager _resource;
		
		public rptInheritBase()
		{
			_resource = new ResourceManager(typeof(rptInheritBase));
			// 
			// TODO:Please add a constructor logic here
			//

			//Adds an event handler.
			DataInitialize += new EventHandler(BaseReport_DataInitialize);
			FetchData += new FetchEventHandler(BaseReport_FetchData);
			ReportStart += new System.EventHandler(this.rptInheritBase_ReportStart);
		}

		//CsvPath property
		protected string CsvPath
		{
			set
			{
				_csvPath = value;
			}
		}

		protected void BaseReport_DataInitialize(object sender, System.EventArgs eArgs)
		{

			// Load CSV to stream.
			StreamReader invoiceFileStream = new StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))));
			//Read a line from the stream, and create an array string.
			string currentLine = invoiceFileStream.ReadLine();
			_fieldNameArray = currentLine.Split(new char[] { ',' });

			//Field only to create a number of the array.
			for (int i = 0; i < _fieldNameArray.Length; i++)
				Fields.Add(_fieldNameArray[i]);
		   
		}

		protected void BaseReport_FetchData(object sender, FetchEventArgs eArgs)
		{
			try
			{
				if (_invoiceFileStream.Peek() >= 0)
				{
					//Read a line from the stream, and creats an array string.
					string _currentLine = _invoiceFileStream.ReadLine();
					string[] _currentArray = _currentLine.Split(new char[] { ',' });

					//Store the Value property of Field object number of the array.
					for (int i = 0; i < _currentArray.Length; i++)
						Fields[_fieldNameArray[i]].Value = _currentArray[i];

					//Set EOF to false and continue to read the data.
					eArgs.EOF = false;
				}
				else
				{
					_invoiceFileStream.Close();
					eArgs.EOF = true;
				}
			}
			catch
			{
				//Close the stream when the it has exceeded the time to read the last line.
				_invoiceFileStream.Close();

				//Set EOF to true and quit reading the data.
				eArgs.EOF = true;
			}
		}

		protected void rptInheritBase_ReportStart(object sender, EventArgs e)
		{
			// Load CSV to stream.
			_invoiceFileStream = new StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))));
			_invoiceFileStream.ReadLine();
		}
	}
}
