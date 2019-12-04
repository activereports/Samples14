using GrapeCity.ActiveReports;
using ObjectDataSource.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ODataDataSource
{
	/// <summary>
	/// Client is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-client-app
	/// </summary>
	public partial class MainForm : Form
	{
		private readonly IList<Year> _dataList;
		private const string serviceUri = "http://localhost:55749/";
		public MainForm()
		{
			InitializeComponent();
			var container = new ObjectDataSource.Default.Container(new Uri(serviceUri));
			_dataList = GetYears(container);
		}

		private IList<Year> GetYears(ObjectDataSource.Default.Container container)
		{
			var years = new List<Year>();
			foreach (var movie in container.Movies)
			{
				int year = movie.YearReleased;
				if (years.Count == 0 || years[years.Count - 1].YearReleased != year)
					years.Add(new Year(year));
				var newMovie = new Movie(movie.Id, movie.Title, movie.MPAA);
				years[years.Count - 1].Movies.Add(newMovie);
			}
			return years;
		}

		private void OnLocateDataSource(object sender, LocateDataSourceEventArgs args)
		{
			if (args.DataSet.Name == "Years") // ObjectsReport :bind collection to report
			{
				args.Data = _dataList;
			}
			else if (args.DataSet.Name == "Movies") // SubObjectsReport :bind subcollection to subreport
			{
				var yearReleased = 1970;
				foreach (var param in args.Parameters)
				{
					if (param.Name == "YearReleased")
					{
						yearReleased = int.Parse(param.Value.ToString());
						break;
					}
				}

				for (int i = 0; i < _dataList.Count; i++)
					if (_dataList[i].YearReleased == yearReleased)
					{
						args.Data = _dataList[i].Movies;
						break;
					}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var rptPath = new FileInfo(@"..\..\..\..\Reports\ObjectsReport.rdlx");
			var pageReport = new PageReport(rptPath);
			pageReport.Document.LocateDataSource += new LocateDataSourceEventHandler (OnLocateDataSource);
			reportPreview.LoadDocument(pageReport.Document);
		}
	}
}
