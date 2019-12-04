
namespace ObjectDataSource.Models
{
	internal class Movie
	{
		public int MovieID { get; set; }
		public string Title { get; set; }
		public string MPAA { get; set; }

		public Movie(int id, string title, string mpaa)
		{
			MovieID = id;
			Title = title;
			MPAA = mpaa;
		}
	}
}
