using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;


namespace GrapeCity.ActiveReports.Samples.JsonDataSource
{
	// Provides the data used in the sample.
	internal sealed class DataLayer
	{
		public String  CreateData()
		{
			string source_url = @"http://localhost:30187/Service.asmx/GetJson";
			string responseText = null;

			using (var webClient = new WebClient())
			{
				webClient.Headers[HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("admin:1")); // username:password 
				webClient.Headers[HttpRequestHeader.ContentType] = "application/json;";
				webClient.Encoding = Encoding.UTF8;

				var responseJson = webClient.DownloadString(source_url);
				Dictionary<string, string> values = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(responseJson);
				if (values.ContainsKey("d"))
				{
					responseText = values["d"];
				}
			}

			return responseText;
		}
	}
}
