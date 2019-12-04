using System;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

namespace WebService
{
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
		public string GetJson()
		{
			return Properties.Resource.customers;
		}
	}
}
