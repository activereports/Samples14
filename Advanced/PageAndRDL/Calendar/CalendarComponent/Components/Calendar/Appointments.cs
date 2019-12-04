using System;
using System.Collections.Generic;
using System.Drawing;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.PageReportModel;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Stores the information about an appointment.
	/// </summary>
	public sealed class Appointment : ICloneable
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		private readonly object _value;
		private Color _backcolor;
		private Color _borderColor;
		private string _fontFamily;
		private Length _fontSize;
		private FontStyle _fontStyle;
		private FontWeight _fontWeight;
		private FontDecoration _fontDecoration;
		private Color _fontColor;
		private TextAlign _textAlign;
		private string _format;
		private ImageSource _imageSource;
		private object _imageValue;
		private string _mimeType;
		private Action _action;

		/// <summary>
		/// Creates a new instance of <see cref="Appointment"/>.
		/// </summary>
		/// <param name="startDate">Appointment's start time</param>
		/// <param name="endDate">Appointment's end time</param>
		/// <param name="value">Appointment's value</param>
		private Appointment(DateTime startDate, DateTime endDate, object value)
		{
			if (endDate < startDate)
				throw new ArgumentException("Date order is incorrect.");
			_startDate = startDate;
			_endDate = endDate;
			if (value == null)
				_value = string.Empty;
			else
				_value = value;

			// init appearance values by default
			_backcolor = CalendarData.DefaultAppointmentBackcolor;
			_borderColor = CalendarData.DefaultAppointmentBorderColor;
			_fontFamily = CalendarData.DefaultAppointmentFontFamily;
			_fontSize = CalendarData.DefaultAppointmentFontSize;
			_fontStyle = CalendarData.DefaultAppointmentFontStyle;
			_fontWeight = CalendarData.DefaultAppointmentFontWeight;
			_fontDecoration = CalendarData.DefaultAppointmentFontDecoration;
			_fontColor = CalendarData.DefaultAppointmentFontColor;
			_textAlign = CalendarData.DefaultAppointmentTextAlign;
			_format = CalendarData.DefaultAppointmentFormat;
			_imageSource = CalendarData.DefaultAppointmentImageSource;
			_imageValue = CalendarData.DefaultAppointmentImageValue;
			_mimeType = CalendarData.DefaultAppointmentMimeType;
		}

		/// <summary>
		/// Creates the appointment specified by start date and value.
		/// </summary>
		public static Appointment Create(DateTime startDate, object value)
		{
			// make EndDate the end of the StartDate day, by default
			return new Appointment(startDate, startDate.AddDays(1).AddTicks(-1), value);
		}

		/// <summary>
		/// Creates the appointment specified by start and end dates, value.
		/// </summary>
		public static Appointment Create(DateTime startDate, DateTime endDate, object value)
		{
			return new Appointment(startDate, endDate, value);
		}

		/// <summary>
		/// Gets the start date of appointment.
		/// </summary>
		public DateTime StartDate
		{
			get { return _startDate; }
		}

		/// <summary>
		/// Gets the end date of appointment.
		/// </summary>
		public DateTime EndDate
		{
			get { return _endDate; }
		}

		/// <summary>
		/// Gets the value of appointment.
		/// </summary>
		public object Value
		{
			get { return _value; }
		}

		/// <summary>
		/// Gets or sets the action of appointment
		/// </summary>
		public Action Action
		{
			get { return _action; }
			set { _action = value; }
		}

		/// <summary>
		/// Gets or sets the color to paint the background.
		/// </summary>
		public Color Backcolor
		{
			get { return _backcolor; }
			set { _backcolor = value; }
		}

		/// <summary>
		/// Gets or sets the border color.
		/// </summary>
		public Color BorderColor
		{
			get { return _borderColor; }
			set { _borderColor = value; }
		}

		/// <summary>
		/// Gets or sets the font family.
		/// </summary>
		public string FontFamily
		{
			get { return _fontFamily; }
			set { _fontFamily = value; }
		}

		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		public Length FontSize
		{
			get { return _fontSize; }
			set { _fontSize = value; }
		}

		/// <summary>
		/// Gets or sets the font style.
		/// </summary>
		public FontStyle FontStyle
		{
			get { return _fontStyle; }
			set { _fontStyle = value; }
		}

		/// <summary>
		/// Gets or sets the font weight.
		/// </summary>
		public FontWeight FontWeight
		{
			get { return _fontWeight; }
			set { _fontWeight = value; }
		}

		/// <summary>
		/// Gets or sets the font decoration.
		/// </summary>
		public FontDecoration FontDecoration
		{
			get { return _fontDecoration; }
			set { _fontDecoration = value; }
		}

		/// <summary>
		/// Gets or sets the font color.
		/// </summary>
		public Color FontColor
		{
			get { return _fontColor; }
			set { _fontColor = value; }
		}

		/// <summary>
		/// Gets or sets the text align.
		/// </summary>
		public TextAlign TextAlign
		{
			get { return _textAlign; }
			set { _textAlign = value; }
		}

		/// <summary>
		/// Gets or sets the format string.
		/// </summary>
		public string Format
		{
			get { return _format; }
			set { _format = value; }
		}

		/// <summary>
		/// Gets or sets the source of image should be drawn at the start of the range.
		/// </summary>
		public ImageSource ImageSource
		{
			get { return _imageSource; }
			set { _imageSource = value; }
		}

		/// <summary>
		/// Gets or sets the value of image should be drawn at the start of the range.
		/// </summary>
		public object ImageValue
		{
			get { return _imageValue; }
			set { _imageValue = value; }
		}

		/// <summary>
		/// Gets or sets mime type of image should be drawn at the start of the range.
		/// </summary>
		public string MimeType
		{
			get { return _mimeType; }
			set { _mimeType = value; }
		}

		/// <summary>
		/// Gets the start date of appointment arranged to specified one.
		/// </summary>
		public DateTime ArrangeStartDate(DateTime startDate)
		{
			DateTime arrangedStartDate = StartDate.Date;

			if (startDate > arrangedStartDate)
				arrangedStartDate = startDate;
			return arrangedStartDate;
		}

		/// <summary>
		/// Gets the end date of appointment arranged to specified one.
		/// </summary>
		private DateTime ArrangeEndDate(DateTime endDate)
		{
			DateTime arrangedEndDate = EndDate.Date;

			if (endDate < arrangedEndDate)
				arrangedEndDate = endDate;
			return arrangedEndDate;
		}

		/// <summary>
		/// Gets the count of the days within the date range that appointment should span over
		/// </summary>
		public int GetDurationInPeriod(DateTime startDate, DateTime endDate)
		{
			int duration = 0;

			DateTime arrangedStartDate = ArrangeStartDate(startDate);
			DateTime arrangedEndDate = ArrangeEndDate(endDate);

			if (arrangedStartDate <= arrangedEndDate)
			{
				duration = new TimeSpan(arrangedEndDate.Ticks - arrangedStartDate.Ticks).Days + 1;
			}

			return duration;
		}

		#region Comparer

		public class Comparer : IComparer<Appointment>
		{
			///<summary>
			/// Compares two objects and returns a value indicating whether one is less than, 
			/// equal to, or greater than the other.
			///</summary>
			///<param name="y">The second object to compare.</param>
			///<param name="x">The first object to compare.</param>
			///<returns>
			/// Value Condition Less than zero x is less than y. Zero x equals y. Greater than zero x is greater than y.
			///</returns>
			public virtual int Compare(Appointment x, Appointment y)
			{
				if (ReferenceEquals(x, y)) return 0;
				if (ReferenceEquals(x, null)) return -1;
				if (ReferenceEquals(y, null)) return 1;

				if (x.StartDate == y.StartDate && x.EndDate == y.EndDate)
					return 0;

				if (x.StartDate == y.StartDate)
					return x.EndDate.CompareTo(y.EndDate);

				return x.StartDate.CompareTo(y.StartDate);
			}
		}

		#endregion

		#region IClonable members

		///<summary>
		/// Creates a new object that is a copy of the current instance.
		///</summary>
		///<returns>
		/// A new object that is a copy of this instance.
		///</returns>
		public object Clone()
		{
			Appointment clone = new Appointment(StartDate, EndDate, Value);
			clone.Backcolor = Backcolor;
			clone.BorderColor = BorderColor;
			clone.FontFamily = FontFamily;
			clone.FontSize = FontSize;
			clone.FontStyle = FontStyle;
			clone.FontWeight = FontWeight;
			clone.FontDecoration = FontDecoration;
			clone.FontColor = FontColor;
			clone.TextAlign = TextAlign;
			clone.Format = Format;
			clone.ImageSource = ImageSource;
			clone.ImageValue = ImageValue;
			clone.MimeType = MimeType;

			return clone;
		}

		#endregion
	}
}
