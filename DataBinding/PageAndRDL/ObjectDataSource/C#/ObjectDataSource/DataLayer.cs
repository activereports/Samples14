using System.Collections.Generic;
using System.Data.OleDb;

namespace GrapeCity.ActiveReports.Samples.ObjectDataSource
{
	internal sealed class DataLayer
	{
		// Returns IEnumerable data object.
		public static IList<Year> LoadData()
		{
			var years = new List<Year>();

			var connStr = Properties.Resources.ConnectionString;
			var conn = new OleDbConnection(connStr);
			conn.Open();
			var cmd = new OleDbCommand("SELECT Movie.YearReleased, Movie.MovieID, Movie.Title, Movie.MPAA FROM Movie ORDER BY Movie.YearReleased", conn);
			var dataReader = cmd.ExecuteReader();
			while (dataReader.Read())
			{
				int year = int.Parse(dataReader.GetValue(0).ToString());
				if (years.Count == 0 || years[years.Count - 1].YearReleased != year)
					years.Add(new Year(year));
				var movie = new Movie(int.Parse(dataReader.GetValue(1).ToString()), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString());
				years[years.Count - 1].Movies.Add(movie);
			}
			conn.Close();

			return years;
		}
	}

	internal sealed class Year
	{
		public int YearReleased
		{
			get;
			private set;
		}
		public IList<Movie> Movies
		{
			get;
			private set;
		}
		public Year(int yearReleased)
		{
			YearReleased = yearReleased;
			Movies = new List<Movie>();
		}
	}

	internal sealed class Movie
	{
		public int MovieID
		{
			get;
			private set;
		}
		public string Title
		{
			get;
			private set;
		}
		public string MPAA
		{
			get;
			private set;
		}
		public Movie(int id, string title, string mpaa)
		{
			MovieID = id;
			Title = title;
			MPAA = mpaa;
		}
	}
}
