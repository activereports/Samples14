using System;
using System.IO;
using System.Windows.Forms;
﻿using GrapeCity.ActiveReports.Samples.CustomDataProviderUI.Properties;

namespace GrapeCity.ActiveReports.Samples.CustomDataProviderUI
{
	public partial class CSVFileSelector : UserControl
	{
		private string _selectedFileName="";
		
		public string CSVFileName
		{
			get { return _selectedFileName; }
		}
		
		public CSVFileSelector()
		{
			InitializeComponent();
		}

		private void btnSelectFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = Resources.CSVFilter;
			string CombinedPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\");
			openFile.InitialDirectory = Path.GetFullPath(CombinedPath);
			openFile.Title = Resources.CSVTitle;

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				_selectedFileName = openFile.FileName;
			}
		}
	}
}
