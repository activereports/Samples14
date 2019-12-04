
namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Specifies the style of the border on a report item.
	/// </summary>
	////[DoNotObfuscateType]
	public enum BorderStyle
	{
		/// <summary>
		/// Indicates no border line.
		/// </summary>
		None = 0,
		/// <summary>
		/// Indicates a dotted border line.
		/// </summary>
		Dotted,
		/// <summary>
		/// Indicates a dashed border line.
		/// </summary>
		Dashed,
		/// <summary>
		/// Indicates a solid border line.
		/// </summary>
		Solid,
	}

	/// <summary>
	/// Specifies the horizontal alignment of the text.
	/// </summary>
	/// <remarks>Don't change the integer values. The values are used in RenderUtils.GetAlignmentString().</remarks>
	////[DoNotObfuscateType]
	public enum TextAlign
	{
		/// <summary>
		/// Indicates aligment will be based off of the content type.
		/// </summary>
		General = 0,
		/// <summary>
		/// Indicates align to the left.
		/// </summary>
		Left = 1,
		/// <summary>
		/// Indicates align centered.
		/// </summary>
		Center = 2,
		/// <summary>
		/// Indicates align to the right.
		/// </summary>
		Right = 3
	}

	/// <summary>
	/// Specifies the vertical alignment of the text.
	/// </summary>
	/// <remarks>Don't change the integer values. The values are used in RenderUtils.GetAlignmentString().</remarks>
	////[DoNotObfuscateType]
	public enum VerticalAlign
	{
		/// <summary>
		/// Indicates align to the top.
		/// </summary>
		Top = 0,
		/// <summary>
		/// Indicates align to the middle.
		/// </summary>
		Middle = 1,
		/// <summary>
		/// Indicates align to the bottom.
		/// </summary>
		Bottom = 2
	}

	/// <summary>
	/// Specifies the kind of day of week.
	/// </summary>
	/// <remarks>We should keep the order. It's used to sort day for rendering.</remarks>
	////[DoNotObfuscateType]
	public enum DayKind
	{
		/// <summary>
		/// The 'filler' days in a month.
		/// </summary>
		Filler = 0,
		/// <summary>
		/// The common kind of days.
		/// </summary>
		General,
		/// <summary>
		/// Weekend days.
		/// </summary>
		Weekend,
		/// <summary>
		/// Holidays.
		/// </summary>
		Holidays
	}

	/// <summary>
	/// Specifies whether the text is written from left to right or right to left.
	/// </summary>
	////[DoNotObfuscateType]
	public enum Direction
	{
		/// <summary>
		/// Indicates left to right.
		/// </summary>
		LTR = 0,
		/// <summary>
		/// Indicates right to left.
		/// </summary>
		RTL
	}
}
