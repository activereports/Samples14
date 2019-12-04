using System.ComponentModel.DataAnnotations;

namespace ODataDataSource.Models
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string MPAA { get; set; }
		public int YearReleased { get; set; }
	}
}
