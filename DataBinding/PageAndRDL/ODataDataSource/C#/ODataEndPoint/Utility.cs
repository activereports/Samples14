using System.Web;

namespace ODataDataSource
{
	public static class Utility
	{
		public static string UpdateConnectionString(string connectionString)
		{
			return connectionString.Replace("$appPath$",
				HttpContext.Current.Server.MapPath("~"));
		}
	}
}
