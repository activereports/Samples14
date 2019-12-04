
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.OData;
using ODataDataSource.Models;

namespace ODataDataSource.Controllers
{
	/// <summary>
	/// Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
	/// </summary>
	public class MoviesController : ODataController
	{
		public  IList<Movie> Get()
		{
			var movies = new List<Movie>();

			var connStr = Utility.UpdateConnectionString(Properties.Resource.Reels);
			var conn = new OleDbConnection(connStr);
			conn.Open();
			var cmd = new OleDbCommand("SELECT Movie.MovieID, Movie.Title, Movie.MPAA, Movie.YearReleased FROM Movie ORDER BY Movie.YearReleased", conn);
			var dataReader = cmd.ExecuteReader();
			while (dataReader.Read())
			{
				movies.Add(new Movie() { Id = (int)dataReader.GetValue(0), Title = dataReader.GetValue(1).ToString(), MPAA = dataReader.GetValue(2).ToString() , YearReleased = (int)dataReader.GetValue(3) });
			}
			conn.Close();

			return movies;
		}

	}
}
