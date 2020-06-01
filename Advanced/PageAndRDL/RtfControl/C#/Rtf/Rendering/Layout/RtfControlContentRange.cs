﻿using System;
using System.Drawing;

using GrapeCity.ActiveReports.Extensibility.Layout;
using GrapeCity.ActiveReports.Extensibility.Layout.Internal;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;

namespace GrapeCity.ActiveReports.Samples.Rtf.Rendering.Layout
{
	public class RtfControlContentRange : ContentRange, IStaticContentRange, ICustomContentRange
	{
		private readonly ICustomReportItem _owner;
		public readonly RtfContentInfo Info;

		public RtfControlContentRange(ICustomReportItem owner, RtfContentInfo info)
		{
			_owner = owner;
			Info = info;
		}

		public RtfControlContentRange GetNextRange(float deltaHeight, string rtf)
		{
			var newInfo = new RtfContentInfo(rtf, Info.Area, Info.TotalSize);
			var newContent = new RtfControlContentRange(_owner, newInfo);
			
			newContent.StartVertRange += deltaHeight;
			newContent.EndVertRange += deltaHeight;

			return newContent;
		}

		public bool isLastPage
		{
			get { return EndVertRange >= Info.TotalSize.Height; }
		}
			
		#region ContentRange members

		public override IReportItem Owner
		{
			get { return _owner; }
		}

		#endregion

		#region ICustomContentRange

		public IContentRange Fork(ICustomReportItem reportItem)
		{
			return new RtfControlContentRange(_owner, Info);
		}

		#endregion

		#region IStaticContentRange members

		public float StartVertRange
		{
			get { return Info.Area.Top; }
			set { Info.Area.Top = Math.Min(value, Info.TotalSize.Height); }
		}

		public float EndVertRange
		{
			get { return Info.Area.Bottom; }
			set { Info.Area.Bottom = Math.Min(value, Info.TotalSize.Height); }
		}

		public float StartHorzRange
		{
			get { return Info.Area.Left; }
			set { Info.Area.Left = Math.Min(value, Info.TotalSize.Width); }
		}

		public float EndHorzRange
		{
			get { return Info.Area.Right; }
			set { Info.Area.Right = Math.Min(value, Info.TotalSize.Width); }
		}

		public bool CompleteItemHorizontally
		{
			get { return StartHorzRange >= EndHorzRange; }
		}

		public bool CompleteItemVertically
		{
			get { return StartVertRange >= EndVertRange; }
		}
		
		public int ItemWidth
		{
			get { return (int)Math.Round(EndHorzRange - StartHorzRange); }
		}
		public int ItemHeight
		{
			get { return (int)Math.Round(EndVertRange - StartVertRange); }
		}

		#endregion
	}
	
	#region Helper classes
	
	public class RtfContentInfo
	{
		public readonly string Rtf;
		public readonly Range Area;
		public readonly SizeF TotalSize;

		public RtfContentInfo(string rtf, Range range, SizeF totalSize)
		{
			Rtf = rtf;
			Area = range;
			TotalSize = totalSize;
		}
	}
		
	public class Range
	{
		public float Left { get; set; }
		public float Right { get; set; }
		public float Top { get; set; }
		public float Bottom { get; set; }

		public Range(float top, float bottom, float left, float right)
		{
			Top = top;
			Bottom = bottom;
			Left = left;
			Right = right;
		}
	}
	
	#endregion
}
