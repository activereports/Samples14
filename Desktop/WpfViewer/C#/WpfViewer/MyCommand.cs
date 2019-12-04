using System;
using System.Windows.Input;
using System.Windows;
using GrapeCity.ActiveReports.Samples.WPFViewer.Properties;

namespace GrapeCity.ActiveReports.Samples.WPFViewer
{
	class MyCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			MessageBox.Show(Resources.AboutUsText, Resources.AboutUsCaption, MessageBoxButton.OK);
		}

		public event EventHandler CanExecuteChanged { add { } remove { } }
	}
}
