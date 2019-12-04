using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace WebService
{
	public class BasicAuthHttpModule : IHttpModule
	{
		private const string Realm = "My Realm";

		public void Init(HttpApplication context)
		{
			// Register event handlers
			context.AuthenticateRequest += OnApplicationAuthenticateRequest;
			context.EndRequest += OnApplicationEndRequest;
		}

		private static void SetPrincipal(IPrincipal principal)
		{
			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
		}

		// TODO: Here is where you would validate the username and password.
		private static bool CheckPassword(string username, string password)
		{
			return username == "admin" && password == "1";
		}

		private static void AuthenticateUser(string credentials)
		{
			try
			{
				var encoding = Encoding.GetEncoding("iso-8859-1");
				credentials = encoding.GetString(Convert.FromBase64String(credentials));

				int separator = credentials.IndexOf(':');
				string name = credentials.Substring(0, separator);
				string password = credentials.Substring(separator + 1);

				if (CheckPassword(name, password))
				{
					var identity = new GenericIdentity(name);
					SetPrincipal(new GenericPrincipal(identity, null));
				}
				else
				{
					// Invalid username or password.
					HttpContext.Current.Response.StatusCode = 403;
				}
			}
			catch (FormatException)
			{
				// Credentials were not formatted correctly.
				HttpContext.Current.Response.StatusCode = 401;
			}
		}

		// http://cacheandquery.com/blog/2011/03/customizing-asp-net-mvc-basic-authentication/
		private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
		{
			var request = HttpContext.Current.Request;
			System.IO.FileInfo info = new System.IO.FileInfo(request.Url.AbsolutePath);
			if (!info.Name.Equals("GetJson")) return;

			var authHeader = request.Headers["Authorization"];
			// RFC 2617 sec 1.2, "scheme" name is case-insensitive
			if (authHeader != null && authHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase) && authHeader.Length > 6)
			{ 
				AuthenticateUser(authHeader.Substring(6));
			}
			else
			{
				HttpContext.Current.Response.StatusCode = 401;
			}
		}

		// If the request was unauthorized, add the WWW-Authenticate header 
		// to the response.
		private static void OnApplicationEndRequest(object sender, EventArgs e)
		{
			var response = HttpContext.Current.Response;
			if (response.StatusCode == 401)
			{
				response.Headers.Add("WWW-Authenticate",
					string.Format("Basic realm=\"{0}\"", Realm));
			}
		}

		public void Dispose()
		{
		}
	}
}
