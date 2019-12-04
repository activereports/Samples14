using System;
using System.IO;
using System.Net;

namespace GrapeCity.ActiveReports.Samples.CustomTileProviders
{
	internal static class WebRequestHelper
	{
		/// <summary>
		/// Load raw data into MemoryStream from specified Url.
		/// </summary>
		/// <param name="url">Data source Url</param>
		/// <param name="timeoutMilliseconds">Timeout in milliseconds. If -1 the default timeout will  used.</param>
		/// <returns></returns>
		public static Stream DownloadData(string url, int timeoutMilliseconds)
		{
			var request = WebRequest.Create(url);
			if (timeoutMilliseconds > 0)
			{
				request.Timeout = timeoutMilliseconds;
			}
			
			var response = request.GetResponse();

			//Copy data from buffer (It must be done, otherwise the buffer overflow and we stop to get repsonses).
			var stream = new MemoryStream();
			response.GetResponseStream().CopyTo(stream);
			return stream;
		}

		/// <summary>
		/// Load raw data into MemoryStream from specified Url.
		/// </summary>
		/// <param name="url">Data source Url</param>
		/// <param name="timeoutMilliseconds">Timeout in milliseconds. If -1 the default timeout will  used.</param>
		/// <returns></returns>
		public static void DownloadDataAsync(string url, int timeoutMilliseconds, Action<MemoryStream> success, Action<Exception> error)
		{
			var request = WebRequest.Create(url);
			if (timeoutMilliseconds > 0)
			{
				request.Timeout = timeoutMilliseconds;
			}

			request.BeginGetResponse(ar =>
			{
				try
				{
					var response = request.GetResponse();

					//Copy data from buffer (It must be done, otherwise the buffer overflow and we stop to get repsonses).
					var stream = new MemoryStream();
					response.GetResponseStream().CopyTo(stream);
					success(stream);
				}
				catch (Exception exception)
				{
					error(exception);
				}
			}, null);
		}

		public static void CopyTo(this Stream input, Stream output)
		{
			var buffer = new byte[16 * 1024];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				output.Write(buffer, 0, read);
		}
	}
}
