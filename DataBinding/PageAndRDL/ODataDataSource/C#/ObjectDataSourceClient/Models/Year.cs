using System;
using System.Collections.Generic;

namespace ObjectDataSource.Models
{
	internal class Year
	{
	
		public int YearReleased { get; private set; }

		public IList<Movie> Movies { get; private set; }

		public Year(int yearReleased)
		{
			YearReleased = yearReleased;
			Movies = new List<Movie>();
		}
	}
}
