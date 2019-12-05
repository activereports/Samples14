using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;

namespace ODataDataSource
{
	internal class DataLayer
	{
		private static readonly string[] _sources = 
			{
			@"http://localhost:55749/Customers?$format=application/json;odata.metadata=none",
			@"https://services.odata.org/V4/Northwind/Northwind.svc/Customers?$select=CustomerID,%20CompanyName,%20ContactName,%20Address%20&%20$format=application/json;odata.metadata=none"
			};

		public static string CreateData(Service service)
		{
			string source_url = _sources[(int)service];
			try
			{
				using (var webClient = new WebClient())
				{
					webClient.Encoding = Encoding.UTF8;
					var json = webClient.DownloadString(source_url);
					var jObject = (JObject)JsonConvert.DeserializeObject(json);
					foreach (var obj in jObject)
					{
						if (obj.Key == "value")
						{
							return "{" + obj.Key + ":" + obj.Value + "}";
						}
					}
					return "";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
