using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TestViewer
{
	public partial class ViewerForm : Form
	{
		private string _path = "../../../Reports";
		public ViewerForm()
		{
			InitializeComponent();
			var directoryInfo = new DirectoryInfo(_path);
			var files = directoryInfo.GetFiles();
			var list = new List<string>();
			foreach (var file in files)
			{
				comboBox1.Items.Add(file.Name);
			}

			comboBox1.SelectedIndex = 0;
			splitContainer1.IsSplitterFixed = true;
			splitContainer1.FixedPanel = FixedPanel.Panel1;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string fileName = (string)comboBox1.SelectedItem;
			viewer1.LoadDocument(Path.Combine(_path, fileName));
		}
	}
}
