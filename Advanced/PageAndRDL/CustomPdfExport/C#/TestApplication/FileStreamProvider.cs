using System;
using System.IO;
using GrapeCity.ActiveReports.Extensibility.Rendering.IO;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering
{
	internal sealed class FileStreamProvider : StreamProvider
	{
		private readonly string _outputPath;
		private StreamInfo _primaryStream;

		public FileStreamProvider(string outputPath)
		{
			_outputPath = outputPath;
		}

		#region Overrides of StreamProvider

		public override StreamInfo CreatePrimaryStream(string mimeType, string suggestedFileExtension)
		{
			return _primaryStream = new FileStreamInfo(new Uri(_outputPath, UriKind.RelativeOrAbsolute), mimeType);
		}

		public override StreamInfo CreateSecondaryStream(string relativeName, string mimeType, string suggestedFileExtension)
		{
			throw new NotSupportedException();
		}

		public override StreamInfo GetPrimaryStream()
		{
			return _primaryStream;
		}

		public override StreamInfo[] GetSecondaryStreams()
		{
			throw new NotSupportedException();
		}

		#endregion
		
		private sealed class FileStreamInfo : StreamInfo
		{
			public FileStreamInfo(Uri uri, string mimeType) : base(uri, mimeType)
			{
			}
			
			public override Stream OpenStream()
			{
				return new FileStream(Uri.OriginalString, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			}
		}
	}
}
