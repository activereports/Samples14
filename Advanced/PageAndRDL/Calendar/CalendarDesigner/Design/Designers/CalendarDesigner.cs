using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Components.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Properties;
using GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Design;
using GrapeCity.ActiveReports.Design.DdrDesigner;
using GrapeCity.ActiveReports.Design.DdrDesigner.Actions;
using GrapeCity.ActiveReports.Design.DdrDesigner.Behavior;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.ActiveReports.Design.DdrDesigner.Editors.ColorEditor;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.Data;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.Infrastructure;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Action = GrapeCity.ActiveReports.PageReportModel.Action;
using BorderStyle = GrapeCity.ActiveReports.Calendar.Components.Calendar.BorderStyle;
using DesignerActionItemCollection = GrapeCity.ActiveReports.Design.DdrDesigner.Actions.DesignerActionItemCollection;
using DesignerActionList = GrapeCity.ActiveReports.Design.DdrDesigner.Actions.DesignerActionList;
using DesignerActionUIService = GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUIService;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;
using Direction = GrapeCity.ActiveReports.Calendar.Components.Calendar.Direction;
using Drillthrough = GrapeCity.ActiveReports.PageReportModel.Drillthrough;
using TextAlign = GrapeCity.ActiveReports.Calendar.Components.Calendar.TextAlign;
using VerticalAlign = GrapeCity.ActiveReports.Calendar.Components.Calendar.VerticalAlign;
using FontWeight = GrapeCity.ActiveReports.Drawing.FontWeight;
using System.Runtime.InteropServices;
using GrapeCity.ActiveReports.Drawing.Gdi;

namespace GrapeCity.ActiveReports.Calendar.Design.Designers
{
	/// <summary>
	/// Represents the designer for <see cref="CalendarDataRegion" />.
	/// </summary>
	////[DoNotObfuscateType]
	[Guid("235168EF-8C7A-4899-8FA2-60CB35270B7A")]
	[ToolboxBitmap(typeof(CalendarDesigner), "CalendarToolboxIcon.bmp")]
	[ResourceDisplayName("CalendarDisplayName")]
	public sealed class CalendarDesigner : ReportItemDesignerBase, ICalendar, IWizardProvider, IValidateable, IFixedSizeDesigner
	{
		private CalendarCulture _calendarCultureService;
		private DesignerImageLocatorService _imageService;
		private SmartPanelTarget _smartPanelTarget = SmartPanelTarget.Main;
		private FixedSizeGlyph _fixedSizeGlyph;
		private bool _disposed;
		private CoreNotificationService _notificationService;

		public CalendarDesigner()
		{
		}

		#region Designer Properties

		/// <summary>
		/// Adds custom properties to the property collection.
		/// </summary>
		protected override void AddCustomProperties()
		{
			// month title style props
			PropertyDescriptors.Add(new ShadowProperty(
				"MonthTitleStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				MonthTitlePropertyName,
				typeof(MonthTitleStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));

			// filler day style props
			PropertyDescriptors.Add(new ShadowProperty(
				"FillerDayStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				FillerDayPropertyName,
				typeof(FillerDayStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));

			// day style props
			PropertyDescriptors.Add(new ShadowProperty(
				"DayStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				DayPropertyName,
				typeof(DayStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));

			// weekend style props
			PropertyDescriptors.Add(new ShadowProperty(
				"WeekendStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				WeekendPropertyName,
				typeof(WeekendStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));

			#region appoitment props

			// start date
			PropertyDescriptors.Add(new ShadowProperty(
				EventStartDatePropertyName,
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "AppointmentStartDate", typeof(ExpressionInfo),
				new ResourceCategoryAttribute(EventCategory))));

			// end date
			PropertyDescriptors.Add(new ShadowProperty(
				EventEndDatePropertyName,
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "AppointmentEndDate", typeof(ExpressionInfo),
				new ResourceCategoryAttribute(EventCategory))));

			// value
			PropertyDescriptors.Add(new ShadowProperty(
				EventValuePropertyName,
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "AppointmentValue", typeof(ExpressionInfo),
				new ResourceCategoryAttribute(EventCategory))));

			// appointment style property
			PropertyDescriptors.Add(new ShadowProperty(
				"EventStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				EventPropertyName,
				typeof(AppointmentStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));
			#endregion

			// day headers properties
			PropertyDescriptors.Add(new ShadowProperty(
				"DayHeadersStyle",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner),
				DayHeadersPropertyName,
				typeof(DayHeadersStyle),
				new RefreshPropertiesAttribute(RefreshProperties.All),
				new CategoryAttribute("Appearance"))));

			PropertyDescriptors.Add(new ShadowProperty(
				"Action",
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "Action", typeof(Action),
				new DefaultValueAttribute(new Action()))));

			PropertyDescriptors.Add(new ShadowProperty(
			   Resources.PropertySize,
			   TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "ComponentSize", typeof(DesignSize),
				   new CategoryAttribute("Layout"),
				   new DescriptionAttribute(Resources.PropertySizeDescription),
				   new TypeConverterAttribute(typeof(SizeRootConverter)),
				   new RefreshPropertiesAttribute(RefreshProperties.All)
				   ), true));

			PropertyDescriptors.Add(new ShadowProperty(
				Resources.PropertyFixedSize,
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "FixedSize", typeof(DesignSize),
					new CategoryAttribute("Layout"),
					new DescriptionAttribute(Resources.PropertyFixedSizeDescription),
					new TypeConverterAttribute(typeof(SizeRootConverter)),
					new FplOnlyAttribute()
					)));

			PropertyDescriptors.Add(new ShadowProperty(
				Resources.PropertyOverflowName,
				TypeDescriptor.CreateProperty(typeof(CalendarDesigner), "OverflowName", typeof(string),
					new CategoryAttribute("Data"),
					new DescriptionAttribute(Resources.PropertyOverflowNameDescription),
					new TypeConverterAttribute(typeof(OverflowNameConverter)),
					new FplOnlyAttribute()
					)));
		}

		// ReSharper disable UnusedMember.Local
		/// <summary>
		/// Gets or sets the size of the report item.
		/// </summary>
		[ShadowProperty("ComponentSize", "")]
		protected override DesignSize ComponentSize
		{
			get { return base.ComponentSize; }
			set
			{
				if (!UndoService.UndoInProgress)
				{
					value = Utils.RestrictAtMaxSize(this, value, MaximalSize);
					value = Utils.RestrictAtMinSize(value, MinimalSize);
				}
				var fixedSize = UpdateFixedSize(FixedSize, value);
				if (!UndoService.UndoInProgress)
				{
					var fsProperty = TypeDescriptor.GetProperties(ReportItem).Cast<PropertyDescriptor>().FirstOrDefault(p => p.Name == "FixedSize");
					if (fsProperty != null)
						fsProperty.SetValue(ReportItem, fixedSize);
				}
				else
				{
					FixedSize = fixedSize;
				}
				base.ComponentSize = value;
			}
		}

		[ShadowProperty("FixedSize", "")] // REQUIRED! Do not remove!
		private DesignSize FixedSize
		{
			get
			{
				var fixedWidthValue = GetCustomProperty(ReportItem.CustomProperties, CalendarData.FixedWidthPropertyName, null);
				Length fixedWidth = Length.Empty;
				if (fixedWidthValue != null)
					fixedWidth = new Length(fixedWidthValue.ToString());

				var fixedHeightValue = GetCustomProperty(ReportItem.CustomProperties, CalendarData.FixedHeightPropertyName, null);
				Length fixedHeight = Length.Empty;
				if (fixedHeightValue != null)
					fixedHeight = new Length(fixedHeightValue.ToString());

				return GetComponentFixedSize(fixedWidth, fixedHeight);
			}
			set
			{
				value = RestrictFixedSize(value);

				SetCustomProperty(ReportItem.CustomProperties, CalendarData.FixedWidthPropertyName,
								  value.Width.ToString(), Length.Empty.ToString());
				SetCustomProperty(ReportItem.CustomProperties, CalendarData.FixedHeightPropertyName,
								  value.Height.ToString(), Length.Empty.ToString());
			}
		}

		/// <summary>
		/// Expose ReiszeParent to Calendar assembly
		/// </summary>
		internal new void ResizeParents(DesignSize value)
		{
			base.ResizeParents(value);
		}

		private string OverflowName
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties, CalendarData.OverflowNamePropertyName, null);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties, CalendarData.OverflowNamePropertyName, value, null);
			}
		}

		// ReSharper enable UnusedMember.Local

		#region Common Properties

		private static readonly Styles[] StylesArray = { Styles.Language, Styles.Direction };

		/// <summary>
		/// Gets a <see cref="Styles"/> array for this CalendarDesigner
		/// </summary>
		public override Styles[] StyleProperties
		{
			get { return StylesArray; }
		}

		[CategoryAttribute("Data")]
		[ShadowProperty("DataSetName", "The dataset to use for this data region.")]
		[TypeConverter(typeof(DataSetNameConverter))]
		[ResourceDescription("DataSetNamePropertyDescription")]
		internal string DataSetName
		{
			get { return ReportItem.CustomData.DataSetName; }
			set { ReportItem.CustomData.DataSetName = value; }
		}

		#endregion

		#region Month Title Properties

		/// <summary>
		/// Gets or sets the backcolor expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleBackcolor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBackcolorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBorderColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleBorderColor)));
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderWidthPropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleBorderWidth)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderWidthPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleBorderWidth)));
			}
		}

		/// <summary>
		/// Gets or sets the border style expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleBorderStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleBorderStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleBorderStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontFamilyPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFontFamily));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontFamilyPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFontFamily));
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontSizePropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleFontSize)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontSizePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultMonthTitleFontSize)));
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontWeightPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontWeight)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontWeightPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontWeight)));
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontDecorationPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontDecoration)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontDecorationPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleFontDecoration)));
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleFontColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultMonthTitleFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleTextAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleTextAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleTextAlign)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleTextAlignPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultMonthTitleTextAlign)));
			}
		}

		/// <summary>
		/// Gets or sets the format expression of month title.
		/// </summary>
		internal ExpressionInfo MonthTitleFormat
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFormatPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFormat));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.MonthTitleFormatPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultMonthTitleFormat));
			}
		}

		/// <summary>
		/// Gets the compound object to edit border style of month title.
		/// </summary>
		internal DesignLineStyle MonthTitleBorder
		{
			get { return new DesignLineStyle(MonthTitleBorderColor, MonthTitleBorderWidth, MonthTitleBorderStyle); }
			set
			{
				value.Validate(EvaluatorService);
				MonthTitleBorderColor = value.LineColor;
				MonthTitleBorderWidth = value.LineWidth;
				MonthTitleBorderStyle = value.LineStyle;
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of month title.
		/// </summary>
		internal DesignTextStyle MonthTitleFont
		{
			get
			{
				return new DesignTextStyle(
					MonthTitleFontFamily, MonthTitleFontSize, MonthTitleFontStyle,
					MonthTitleFontWeight, MonthTitleFontDecoration, MonthTitleFontColor);
			}
			set
			{
				value.Validate(EvaluatorService);
				MonthTitleFontFamily = value.Family;
				MonthTitleFontSize = value.Size;
				MonthTitleFontStyle = value.Style;
				MonthTitleFontWeight = value.Weight;
				MonthTitleFontDecoration = value.Decoration;
				MonthTitleFontColor = value.Color;
			}
		}

		internal MonthTitleStyle MonthTitleStyle
		{
			get { return new MonthTitleStyle(MonthTitleBackcolor, MonthTitleBorder, MonthTitleFont, MonthTitleTextAlign, MonthTitleFormat); }
			set
			{
				MonthTitleBackcolor = value.MonthTitleBackcolor;
				MonthTitleBorder = value.MonthTitleBorder;
				MonthTitleFont = value.MonthTitleFont;
				MonthTitleTextAlign = value.MonthTitleTextAlign;
				MonthTitleFormat = value.MonthTitleFormat;
			}
		}
		#endregion

		#region Filler Day Properties

		/// <summary>
		/// Gets or sets the backcolor expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayBackcolor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultFillerDayBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBackcolorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultFillerDayBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderColorPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderColorPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderWidthPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderWidthPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the border style expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderStylePropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayBorderStylePropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontFamilyPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontFamilyPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontSizePropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontSizePropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontStylePropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontStylePropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontWeightPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontWeightPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontDecorationPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontDecorationPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultFillerDayFontColor)));
			}
			set
			{
				if (EvaluatorService.IsEmptyExpression(value))
				{
					value = ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultFillerDayFontColor));
				}
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultFillerDayFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayTextAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayTextAlignPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayTextAlignPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the vertical align expression of filler day.
		/// </summary>
		internal ExpressionInfo FillerDayVerticalAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayVerticalAlignPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.FillerDayVerticalAlignPropertyName,
					value,
					EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets the compound object to edit border style of filler day.
		/// </summary>
		internal DesignLineStyle FillerDayBorder
		{
			get { return new DesignLineStyle(FillerDayBorderColor, FillerDayBorderWidth, FillerDayBorderStyle, true); }
			set
			{
				value.Validate(EvaluatorService);
				FillerDayBorderColor = value.LineColor;
				FillerDayBorderWidth = value.LineWidth;
				FillerDayBorderStyle = value.LineStyle;
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of filler day.
		/// </summary>
		internal DesignTextStyle FillerDayFont
		{
			get
			{
				return new DesignTextStyle(
					FillerDayFontFamily, FillerDayFontSize, FillerDayFontStyle,
					FillerDayFontWeight, FillerDayFontDecoration, FillerDayFontColor, true);
			}
			set
			{
				value.Validate(EvaluatorService);
				FillerDayFontFamily = value.Family;
				FillerDayFontSize = value.Size;
				FillerDayFontStyle = value.Style;
				FillerDayFontWeight = value.Weight;
				FillerDayFontDecoration = value.Decoration;
				FillerDayFontColor = value.Color;
			}
		}

		internal FillerDayStyle FillerDayStyle
		{
			get
			{
				return new FillerDayStyle(FillerDayBackcolor, FillerDayBorder, FillerDayFont, FillerDayTextAlign, FillerDayVerticalAlign);
			}
			set
			{
				FillerDayBackcolor = value.FillerDayBackcolor;
				FillerDayBorder = value.FillerDayBorder;
				FillerDayFont = value.FillerDayFont;
				FillerDayTextAlign = value.FillerDayTextAlign;
				FillerDayVerticalAlign = value.FillerDayVerticalAlign;
			}
		}

		#endregion

		#region General Day Properties

		/// <summary>
		/// Gets or sets the backcolor expression of general day.
		/// </summary>
		internal ExpressionInfo DayBackcolor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBackcolorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of general day.
		/// </summary>
		internal ExpressionInfo DayBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayBorderColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayBorderColor)));
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of general day.
		/// </summary>
		internal ExpressionInfo DayBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderWidthPropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayBorderWidth)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderWidthPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayBorderWidth)));
			}
		}

		/// <summary>
		/// Gets or sets the border style expression of general day.
		/// </summary>
		internal ExpressionInfo DayBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayBorderStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayBorderStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayBorderStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontFamilyPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultDayFontFamily));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontFamilyPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultDayFontFamily));
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontSizePropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayFontSize)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontSizePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayFontSize)));
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontWeightPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontWeight)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontWeightPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontWeight)));
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontDecorationPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontDecoration)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontDecorationPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayFontDecoration)));
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of general day.
		/// </summary>
		internal ExpressionInfo DayFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayFontColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of general day.
		/// </summary>
		internal ExpressionInfo DayTextAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayTextAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayTextAlign)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayTextAlignPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayTextAlign)));
			}
		}

		/// <summary>
		/// Gets or sets the vertical align expression of general day.
		/// </summary>
		internal ExpressionInfo DayVerticalAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayVerticalAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayVerticalAlign)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayVerticalAlignPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayVerticalAlign)));
			}
		}

		/// <summary>
		/// Gets the compound object to edit border style of general day.
		/// </summary>
		internal DesignLineStyle BorderStyle
		{
			get { return new DesignLineStyle(DayBorderColor, DayBorderWidth, DayBorderStyle); }
			set
			{
				value.Validate(EvaluatorService);
				DayBorderColor = value.LineColor;
				DayBorderWidth = value.LineWidth;
				DayBorderStyle = value.LineStyle;
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of general day.
		/// </summary>
		internal DesignTextStyle FontStyle
		{
			get
			{
				return new DesignTextStyle(
					DayFontFamily, DayFontSize, DayFontStyle,
					DayFontWeight, DayFontDecoration, DayFontColor);
			}
			set
			{
				value.Validate(EvaluatorService);
				DayFontFamily = value.Family;
				DayFontSize = value.Size;
				DayFontStyle = value.Style;
				DayFontWeight = value.Weight;
				DayFontDecoration = value.Decoration;
				DayFontColor = value.Color;
			}
		}

		internal DayStyle DayStyle
		{
			get
			{
				return new DayStyle(DayBackcolor, BorderStyle, FontStyle, DayTextAlign, DayVerticalAlign);
			}
			set
			{
				DayBackcolor = value.DayBackcolor;
				BorderStyle = value.BorderStyle;
				FontStyle = value.FontStyle;
				DayTextAlign = value.DayTextAlign;
				DayVerticalAlign = value.DayVerticalAlign;
			}
		}
		#endregion

		#region Weekend Day Properties

		/// <summary>
		/// Gets or sets the backcolor expression of weekend day.
		/// </summary>
		[TypeConverter(typeof(ColorExpressionInfoConverter))]
		[Editor(typeof(ColorTypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo WeekendBackcolor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultWeekendBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBackcolorPropertyName,
					value, ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultWeekendBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties, CalendarData.WeekendBorderColorPropertyName,
					EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties, CalendarData.WeekendBorderColorPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBorderWidthPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBorderWidthPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the border style expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBorderStylePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendBorderStylePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontFamilyPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontFamilyPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontSizePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontSizePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontStylePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontStylePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontWeightPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontWeightPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontDecorationPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontDecorationPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontColorPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendFontColorPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendTextAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendTextAlignPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendTextAlignPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the vertical align expression of weekend day.
		/// </summary>
		internal ExpressionInfo WeekendVerticalAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendVerticalAlignPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.WeekendVerticalAlignPropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets the compound object to edit border style of weekend day.
		/// </summary>
		internal DesignLineStyle WeekendBorder
		{
			get { return new DesignLineStyle(WeekendBorderColor, WeekendBorderWidth, WeekendBorderStyle, true); }
			set
			{
				value.Validate(EvaluatorService);
				WeekendBorderColor = value.LineColor;
				WeekendBorderWidth = value.LineWidth;
				WeekendBorderStyle = value.LineStyle;
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of weekend day.
		/// </summary>
		internal DesignTextStyle WeekendFont
		{
			get
			{
				return new DesignTextStyle(
					WeekendFontFamily, WeekendFontSize, WeekendFontStyle,
					WeekendFontWeight, WeekendFontDecoration, WeekendFontColor, true);
			}
			set
			{
				value.Validate(EvaluatorService);
				WeekendFontFamily = value.Family;
				WeekendFontSize = value.Size;
				WeekendFontStyle = value.Style;
				WeekendFontWeight = value.Weight;
				WeekendFontDecoration = value.Decoration;
				WeekendFontColor = value.Color;
			}
		}

		internal WeekendStyle WeekendStyle
		{
			get
			{
				return new WeekendStyle(WeekendBackcolor, WeekendBorder, WeekendFont, WeekendTextAlign, WeekendVerticalAlign);
			}
			set
			{
				WeekendBackcolor = value.WeekendBackcolor;
				WeekendBorder = value.WeekendBorder;
				WeekendFont = value.WeekendFont;
				WeekendTextAlign = value.WeekendTextAlign;
				WeekendVerticalAlign = value.WeekendVerticalAlign;
			}
		}
		#endregion

		#region Holiday Properties

		/// <summary>
		/// Gets or sets the backcolor expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayBackcolor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBackcolorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayBorderColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayBorderColor)));
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderWidthPropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultHolidayBorderWidth)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderWidthPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultHolidayBorderWidth)));
			}
		}

		/// <summary>
		/// Gets or sets the border style expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayBorderStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayBorderStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayBorderStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontFamilyPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultHolidayFontFamily));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontFamilyPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultHolidayFontFamily));
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontSizePropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultHolidayFontSize)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontSizePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultHolidayFontSize)));
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontWeightPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontWeight)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontWeightPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontWeight)));
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontDecorationPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontDecoration)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontDecorationPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayFontDecoration)));
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayFontColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultHolidayFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayTextAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayTextAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayTextAlign)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayTextAlignPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayTextAlign)));
			}
		}

		/// <summary>
		/// Gets or sets the vertical align expression of holiday.
		/// </summary>
		internal ExpressionInfo HolidayVerticalAlign
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayVerticalAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayVerticalAlign)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.HolidayVerticalAlignPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultHolidayVerticalAlign)));
			}
		}

		#endregion

		#region Appointment Properties

		/// <summary>
		/// Gets or sets the start date expression of appointment.
		/// </summary>
		[TypeConverter(typeof(FieldsListVariantExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentStartDate
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentStartDatePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentStartDatePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the end date expression of appointment.
		/// </summary>
		[TypeConverter(typeof(FieldsListVariantExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentEndDate
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentEndDatePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentEndDatePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the value expression of appointment.
		/// </summary>
		[TypeConverter(typeof(FieldsListVariantExpressionInfoConverter))]
		[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
		internal ExpressionInfo AppointmentValue
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentValuePropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentValuePropertyName,
					value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets the backcolor expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentBackcolor
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentBackcolorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentBackcolor)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentBackcolorPropertyName, value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentBorderColor
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentBorderColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentBorderColor)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentBorderColorPropertyName, value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentBorderColor)));
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontFamily
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontFamilyPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentFontFamily));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontFamilyPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentFontFamily));
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontSize
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontSizePropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultAppointmentFontSize)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontSizePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultAppointmentFontSize)));
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontStyle
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontStyle)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontWeight
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontWeightPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontWeight)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontWeightPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontWeight)));
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontDecoration
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontDecorationPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontDecoration)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontDecorationPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentFontDecoration)));
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFontColor
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentFontColor)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultAppointmentFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the text align expression of appointment.
		/// </summary>    	
		internal ExpressionInfo AppointmentTextAlign
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentTextAlignPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentTextAlign)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentTextAlignPropertyName, value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentTextAlign)));
			}
		}

		/// <summary>
		/// Gets or sets the format expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentFormat
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFormatPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentFormat));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentFormatPropertyName, value,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentFormat));
			}
		}

		/// <summary>
		/// Gets or sets the image source expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentImageSource
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentImageSourcePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentImageSource)));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentImageSourcePropertyName, value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultAppointmentImageSource)));
			}
		}

		/// <summary>
		/// Gets or sets the image value expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentImageValue
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentImageValuePropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentImageValue));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentImageValuePropertyName, value,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentImageValue));
			}
		}

		/// <summary>
		/// Gets or sets the image mime type expression of appointment.
		/// </summary>
		internal ExpressionInfo AppointmentImageMimeType
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentMimeTypePropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentMimeType));
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.AppointmentMimeTypePropertyName, value,
					ExpressionInfo.FromString(CalendarData.DefaultAppointmentMimeType));
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of appointment.
		/// </summary>
		internal DesignTextStyle AppointmentFont
		{
			get
			{
				return new DesignTextStyle(
					AppointmentFontFamily, AppointmentFontSize, AppointmentFontStyle,
					AppointmentFontWeight, AppointmentFontDecoration, AppointmentFontColor);
			}
			set
			{
				value.Validate(EvaluatorService);
				AppointmentFontFamily = value.Family;
				AppointmentFontSize = value.Size;
				AppointmentFontStyle = value.Style;
				AppointmentFontWeight = value.Weight;
				AppointmentFontDecoration = value.Decoration;
				AppointmentFontColor = value.Color;
			}
		}

		/// <summary>
		/// Gets the compound object to edit image of appointment.
		/// </summary>
		internal DesignImage AppointmentImage
		{
			get
			{
				return new DesignImage(AppointmentImageSource, AppointmentImageValue, AppointmentImageMimeType);
			}
			set
			{
				AppointmentImageSource = value.ImageSource;
				AppointmentImageValue = value.ImageValue;
				AppointmentImageMimeType = value.MimeType;
			}
		}

		/// <summary>
		/// Gets appointment custom property collection.
		/// </summary>
		internal CustomPropertyDefinitionCollection AppointmentCustomProperties
		{
			get
			{
				Debug.Assert(ReportItem.CustomData != null, "CustomData should be initialized.");
				Debug.Assert(ReportItem.CustomData.DataRowGroupings.Count == 1, "We should have one grouping.");

				return ReportItem.CustomData.DataRowGroupings[0].CustomProperties;
			}
		}

		/// <summary>
		/// Gets the dummy appointment collection for design time rendering.
		/// </summary>
		private static IEnumerable<Appointment> DummyAppointments
		{
			get
			{
				if (_dummyAppointments == null)
				{
					_dummyAppointments = new Collection<Appointment>();

					const int DummyAppCount = 3;

					DateTime today = DateTime.Now;
					int maxDay = DateTime.DaysInMonth(today.Year, today.Month);

					Random rand = new Random();
					for (int i = 0; i < DummyAppCount; i++)
					{
						DateTime date = new DateTime(today.Year, today.Month, rand.Next(1, maxDay));
						_dummyAppointments.Add(Appointment.Create(date, string.Format("App. on {0}", date.Day)));
					}
				}
				return _dummyAppointments;
			}
		}

		private static Collection<Appointment> _dummyAppointments;

		internal AppointmentStyle EventStyle
		{
			get
			{
				return new AppointmentStyle(AppointmentBackcolor, AppointmentBorderColor, AppointmentFont, AppointmentTextAlign, AppointmentFormat, AppointmentImage);
			}
			set
			{
				AppointmentBackcolor = value.AppointmentBackcolor;
				AppointmentBorderColor = value.AppointmentBorderColor;
				AppointmentFont = value.AppointmentFont;
				AppointmentTextAlign = value.AppointmentTextAlign;
				AppointmentFormat = value.AppointmentFormat;
				AppointmentImage = value.AppointmentImage;
				// force to refresh appointments
				RefreshAppointments();
			}
		}
		#endregion

		#region Day Headers Properties
		/// <summary>
		/// Gets or sets the border style expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersBorderStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersBorderStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersBorderStyle)));
			}
		}

		/// <summary>
		/// Gets or sets the border color expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersBorderColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBorderColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBorderColor)));
			}
		}

		/// <summary>
		/// Gets or sets the border width expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersBorderWidth
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderWidthPropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersBorderWidth)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBorderWidthPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersBorderWidth)));
			}
		}

		/// <summary>
		/// Gets or sets the backcolor expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersBackColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBackColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBackcolor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersBackColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersBackcolor)));
			}
		}

		/// <summary>
		/// Gets or sets the font weight expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontWeight
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontWeightPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontWeight)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontWeightPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontWeight)));
			}
		}

		/// <summary>
		/// Gets or sets the font color expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontColor
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontColorPropertyName,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersFontColor)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontColorPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertColorToString(CalendarData.DefaultDayHeadersFontColor)));
			}
		}

		/// <summary>
		/// Gets or sets the font decoration expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontDecoration
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontDecorationPropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontDecoration)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontDecorationPropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontDecoration)));
			}
		}

		/// <summary>
		/// Gets or sets the font family expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontFamily
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontFamilyPropertyName,
					ExpressionInfo.FromString(CalendarData.DefaultDayHeadersFontFamily));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontFamilyPropertyName,
					value,
					ExpressionInfo.FromString(CalendarData.DefaultDayHeadersFontFamily));
			}
		}

		/// <summary>
		/// Gets or sets the font size expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontSize
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontSizePropertyName,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersFontSize)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontSizePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertLengthToString(CalendarData.DefaultDayHeadersFontSize)));
			}
		}

		/// <summary>
		/// Gets or sets the font style expression of the day headers.
		/// </summary>
		internal ExpressionInfo DayHeadersFontStyle
		{
			get
			{
				return GetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontStylePropertyName,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontStyle)));
			}
			set
			{
				SetCustomProperty(ReportItem.CustomProperties,
					CalendarData.DayHeadersFontStylePropertyName,
					value,
					ExpressionInfo.FromString(Utils.ConvertToString(CalendarData.DefaultDayHeadersFontStyle)));
			}
		}

		/// <summary>
		/// Gets the compound object to edit border style of weekend day.
		/// </summary>
		internal DesignLineStyle DayHeadersBorder
		{
			get { return new DesignLineStyle(DayHeadersBorderColor, DayHeadersBorderWidth, DayHeadersBorderStyle); }
			set
			{
				value.Validate(EvaluatorService);
				DayHeadersBorderColor = value.LineColor;
				DayHeadersBorderWidth = value.LineWidth;
				DayHeadersBorderStyle = value.LineStyle;
			}
		}

		/// <summary>
		/// Gets the compound object to edit font style of weekend day.
		/// </summary>
		internal DesignTextStyle DayHeadersFont
		{
			get
			{
				return new DesignTextStyle(
					DayHeadersFontFamily, DayHeadersFontSize, DayHeadersFontStyle,
					DayHeadersFontWeight, DayHeadersFontDecoration, DayHeadersFontColor);
			}
			set
			{
				value.Validate(EvaluatorService);
				DayHeadersFontFamily = value.Family;
				DayHeadersFontSize = value.Size;
				DayHeadersFontStyle = value.Style;
				DayHeadersFontWeight = value.Weight;
				DayHeadersFontDecoration = value.Decoration;
				DayHeadersFontColor = value.Color;
			}
		}

		internal DayHeadersStyle DayHeaderStyle
		{
			get { return new DayHeadersStyle(DayHeadersBackColor, DayHeadersBorder, DayHeadersFont); }
			set
			{
				DayHeadersBorder = value.DayHeadersBorder;
				DayHeadersBackColor = value.DayHeadersBackColor;
				DayHeadersFont = value.DayHeadersFont;
			}
		}
		#endregion

		#region Action Properties

		/// <summary>
		/// Gets or sets bookmark link expression of the appointment's action
		/// </summary>
		internal ExpressionInfo BookmarkLink
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.ActionBookmarkPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.ActionBookmarkPropertyName, value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets hyperlink expression of the appointment's action
		/// </summary>
		internal ExpressionInfo Hyperlink
		{
			get
			{
				return GetCustomProperty(AppointmentCustomProperties,
					CalendarData.ActionHyperlinkPropertyName, EvaluatorService.EmptyExpression);
			}
			set
			{
				SetCustomProperty(AppointmentCustomProperties,
					CalendarData.ActionHyperlinkPropertyName, value, EvaluatorService.EmptyExpression);
			}
		}

		/// <summary>
		/// Gets or sets drillthrough object of the appointment's action
		/// </summary>
		internal Drillthrough Drillthrough
		{
			get
			{
				return GetDrillthrough();
			}
			set
			{
				SetDrillthrough(value);
			}
		}

		/// <summary>
		/// Gets appointment's action. Used for editing appointment's action via designer grid
		/// </summary>
		[TypeConverter(typeof(ActionRootConverter))]
		[Editor(typeof(ActionUITypeEditor), typeof(UITypeEditor))]
		internal Action Action
		{
			get
			{
				if (_currentAction == null)
				{
					_currentAction = new Action();
				}

				_currentAction.BookmarkLink = EvaluatorService.EmptyExpression;
				_currentAction.Hyperlink = EvaluatorService.EmptyExpression;
				_currentAction.Drillthrough.ReportName = string.Empty;

				if (!EvaluatorService.IsEmptyExpression(BookmarkLink))
				{
					_currentAction.BookmarkLink = BookmarkLink;
				}
				else if (!EvaluatorService.IsEmptyExpression(Hyperlink))
				{
					_currentAction.Hyperlink = Hyperlink;
				}
				else if (!string.IsNullOrEmpty(Drillthrough.ReportName))
				{
					_currentAction.Drillthrough.ReportName = Drillthrough.ReportName;
				}

				return _currentAction;
			}
		}

		private Action _currentAction;

		#endregion

		#endregion

		#region Overridden Designer Members

		/// <summary>
		/// Static constructor for internal initialization
		/// </summary>
		static CalendarDesigner()
		{
			DesignerCultureService cultureService = GlobalServices.Instance.GetService(typeof(DesignerCultureService)) as DesignerCultureService;
			if (cultureService != null)
			{
				cultureService.AddPreferableComponentSize(typeof(CalendarDesigner),
					new DesignSize(cultureService.CalendarDefaultWidth, cultureService.CalendarDefaultHeight));
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (_disposed) return;
			try
			{
				if (disposing)
				{
					if (Utils.IsFplReport(Component))
					{
						if (_notificationService != null) _notificationService.SelectionChanged -= SelectionChangedHandler;
						BehaviorService.RemoveGlyph(_fixedSizeGlyph);
					}
				}
				_disposed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>
		/// Initializes the designer with the specified component.
		/// </summary>
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			if (ReportItem.CustomData == null)
			{
				ReportItem.CustomData = new CustomData();
			}
			if (ReportItem.CustomData.DataRowGroupings.Count == 0)
			{
				ReportItem.CustomData.DataRowGroupings.Add(new DataGrouping());
			}

			// we should initialize custom properties after data grouping creating
			AddCustomProperties();

			// Actually we set item behavior to container mode!
			SupportsComposition = true; // we should add commands (like OnProperties) from base class

			// create chart glyph
			_glyph = new CalendarControlGlyph(this);

			ActionLists.Add(new CalendarActionList(this));

			RefreshAppointments();

			_fixedSizeGlyph = new FixedSizeGlyph(BehaviorService, this, FixedSizeGlyphBehavior.AdjustWidthAndHeight);

			_notificationService = GetService(typeof(CoreNotificationService)) as CoreNotificationService;
			if (_notificationService == null)
				Debug.Fail("Failed to obtain NotificationService");
			else
				_notificationService.SelectionChanged += SelectionChangedHandler;
		}

		private void SelectionChangedHandler(object sender, EventArgs e)
		{
			if (SelectionService.GetComponentSelected(Component) && SelectionService.SelectionCount == 1)
				BehaviorService.AddToDefaultAdorner(_fixedSizeGlyph);
			else
				BehaviorService.RemoveGlyph(_fixedSizeGlyph);

			BehaviorService.Invalidate();
		}

		/// <summary>
		/// Checks if this composite designer can accept the specified data
		/// </summary>
		public override CompositionCapabilities CanAccept(object data)
		{
			IDataFieldValueResolver navigator = data as IDataFieldValueResolver;
			if (navigator != null && navigator.IsDataField())
			{
				// accept the data fields by default
				return new CompositionCapabilities(true);
			}
			return base.CanAccept(data);
		}

		/// <summary>
		/// Creates a new <see cref="ReportItemDesigner" /> at a specified location with specified size and adds it to the collection of children designers.
		/// </summary>
		/// <remarks>We should override it because SupportsComposition was set to true, but report item is not a container.</remarks>
		public override ReportItem Add(Type reportItemType, DesignPoint location, DesignSize size)
		{
			return null;
		}

		/// <summary>
		/// Called when the associated control changes.
		/// </summary>
		protected override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e != null && (e.Component == Component || IsComponentStyleDesigner(e.Component)))
			{
				// refresh the appointments
				if (e.Member != null && (e.Member.Attributes.Cast<Attribute>().Any(x => x is ResourceCategoryAttribute)))
				{
					RefreshAppointments();
				}

				_glyph.Invalidate();
			}

			base.OnComponentChanged(sender, e);
		}

		/// <summary>
		/// Gets glyph. For internal use.
		/// </summary>
		FixedSizeGlyph IFixedSizeDesigner.FixedSizeGlyph
		{
			get { return _fixedSizeGlyph; }
		}

		/// <summary>
		/// Gets designer location. For internal use.
		/// </summary>
		DesignPoint IFixedSizeDesigner.CurrentLocation
		{
			get { return base.CurrentLocation; }
		}

		#endregion

		#region ICalendar members

		/// <summary>
		/// Gets the designer as <see cref="ICalendar"/> instance.
		/// </summary>
		private ICalendar Calendar
		{
			get { return this; }
		}

		#region Month Title

		/// <summary>
		/// Gets the backcolor of month title.
		/// </summary>
		Color ICalendar.MonthTitleBackcolor
		{
			get { return EvaluateColorpression(MonthTitleBackcolor, CalendarData.DefaultMonthTitleBackcolor); }
		}

		/// <summary>
		/// Gets the border color of month title.
		/// </summary>
		Color ICalendar.MonthTitleBorderColor
		{
			get { return EvaluateColorpression(MonthTitleBorderColor, CalendarData.DefaultMonthTitleBorderColor); }
		}

		/// <summary>
		/// Gets the border width of month title.
		/// </summary>
		Length ICalendar.MonthTitleBorderWidth
		{
			get { return EvaluateLengthExpression(MonthTitleBorderWidth, CalendarData.DefaultMonthTitleBorderWidth); }
		}

		/// <summary>
		/// Gets the border style of month title.
		/// </summary>
		BorderStyle ICalendar.MonthTitleBorderStyle
		{
			get { return EvaluateExpression(MonthTitleBorderStyle, CalendarData.DefaultMonthTitleBorderStyle); }
		}

		/// <summary>
		/// Gets the font family of month title.
		/// </summary>
		string ICalendar.MonthTitleFontFamily
		{
			get { return EvaluateStringExpression(MonthTitleFontFamily, CalendarData.DefaultMonthTitleFontFamily); }
		}

		/// <summary>
		/// Gets the font size of month title.
		/// </summary>
		Length ICalendar.MonthTitleFontSize
		{
			get { return EvaluateLengthExpression(MonthTitleFontSize, CalendarData.DefaultMonthTitleFontSize); }
		}

		/// <summary>
		/// Gets the font style of month title.
		/// </summary>
		FontStyle ICalendar.MonthTitleFontStyle
		{
			get { return EvaluateExpression(MonthTitleFontStyle, CalendarData.DefaultMonthTitleFontStyle); }
		}

		/// <summary>
		/// Gets the font weight of month title.
		/// </summary>
		FontWeight ICalendar.MonthTitleFontWeight
		{
			get { return EvaluateExpression(MonthTitleFontWeight, CalendarData.DefaultMonthTitleFontWeight); }
		}

		/// <summary>
		/// Gets the font decoration of month title.
		/// </summary>
		FontDecoration ICalendar.MonthTitleFontDecoration
		{
			get { return EvaluateExpression(MonthTitleFontDecoration, CalendarData.DefaultMonthTitleFontDecoration); }
		}

		/// <summary>
		/// Gets the font color of month title.
		/// </summary>
		Color ICalendar.MonthTitleFontColor
		{
			get { return EvaluateColorpression(MonthTitleFontColor, CalendarData.DefaultMonthTitleFontColor); }
		}

		/// <summary>
		/// Gets the text align of month title.
		/// </summary>
		TextAlign ICalendar.MonthTitleTextAlign
		{
			get { return EvaluateExpression(MonthTitleTextAlign, CalendarData.DefaultMonthTitleTextAlign); }
		}

		/// <summary>
		/// Gets the format of month title.
		/// </summary>
		string ICalendar.MonthTitleFormat
		{
			get { return EvaluateStringExpression(MonthTitleFormat, CalendarData.DefaultMonthTitleFormat); }
		}

		#endregion

		#region Filler Days

		/// <summary>
		/// Gets the backcolor of filler day.
		/// </summary>
		Color ICalendar.FillerDayBackcolor
		{
			get { return EvaluateColorpression(FillerDayBackcolor, CalendarData.DefaultFillerDayBackcolor); }
		}

		/// <summary>
		/// Gets the border color of filler day.
		/// </summary>
		Color ICalendar.FillerDayBorderColor
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayBorderColor) ? DayBorderColor : FillerDayBorderColor;
				return EvaluateColorpression(style, CalendarData.DefaultFillerDayBorderColor);
			}
		}

		/// <summary>
		/// Gets the border width of filler day.
		/// </summary>
		Length ICalendar.FillerDayBorderWidth
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayBorderWidth) ? DayBorderWidth : FillerDayBorderWidth;
				return EvaluateLengthExpression(style, CalendarData.DefaultFillerDayBorderWidth);
			}
		}

		/// <summary>
		/// Gets the border style of filler day.
		/// </summary>
		BorderStyle ICalendar.FillerDayBorderStyle
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayBorderStyle) ? DayBorderStyle : FillerDayBorderStyle;
				return EvaluateExpression(style, CalendarData.DefaultFillerDayBorderStyle);
			}
		}

		/// <summary>
		/// Gets the font family of filler day.
		/// </summary>
		string ICalendar.FillerDayFontFamily
		{
			get
			{
				ExpressionInfo family = EvaluatorService.IsEmptyExpression(FillerDayFontFamily) ? DayFontFamily : FillerDayFontFamily;
				return EvaluateStringExpression(family, CalendarData.DefaultFillerDayFontFamily);
			}
		}

		/// <summary>
		/// Gets the font size of filler day.
		/// </summary>
		Length ICalendar.FillerDayFontSize
		{
			get
			{
				ExpressionInfo size = EvaluatorService.IsEmptyExpression(FillerDayFontSize) ? DayFontSize : FillerDayFontSize;
				return EvaluateLengthExpression(size, CalendarData.DefaultFillerDayFontSize);
			}
		}

		/// <summary>
		/// Gets the font style of filler day.
		/// </summary>
		FontStyle ICalendar.FillerDayFontStyle
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayFontStyle) ? DayFontStyle : FillerDayFontStyle;
				return EvaluateExpression(style, CalendarData.DefaultFillerDayFontStyle);
			}
		}

		/// <summary>
		/// Gets the font weight of filler day.
		/// </summary>
		FontWeight ICalendar.FillerDayFontWeight
		{
			get
			{
				ExpressionInfo weight = EvaluatorService.IsEmptyExpression(FillerDayFontWeight) ? DayFontWeight : FillerDayFontWeight;
				return EvaluateExpression(weight, CalendarData.DefaultFillerDayFontWeight);
			}
		}

		/// <summary>
		/// Gets the font decoration of filler day.
		/// </summary>
		FontDecoration ICalendar.FillerDayFontDecoration
		{
			get
			{
				ExpressionInfo decoration = EvaluatorService.IsEmptyExpression(FillerDayFontDecoration) ? DayFontDecoration : FillerDayFontDecoration;
				return EvaluateExpression(decoration, CalendarData.DefaultFillerDayFontDecoration);
			}
		}

		/// <summary>
		/// Gets the font color of filler day.
		/// </summary>
		Color ICalendar.FillerDayFontColor
		{
			get
			{
				return EvaluateColorpression(FillerDayFontColor, CalendarData.DefaultFillerDayFontColor);
			}
		}

		/// <summary>
		/// Gets the text align of filler day.
		/// </summary>
		TextAlign ICalendar.FillerDayTextAlign
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayTextAlign) ? DayTextAlign : FillerDayTextAlign;
				return EvaluateExpression(style, CalendarData.DefaultFillerDayTextAlign);
			}
		}

		/// <summary>
		/// Gets the vertical align of filler day.
		/// </summary>
		VerticalAlign ICalendar.FillerDayVerticalAlign
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(FillerDayVerticalAlign) ? DayVerticalAlign : FillerDayVerticalAlign;
				return EvaluateExpression(style, CalendarData.DefaultFillerDayVerticalAlign);
			}
		}

		#endregion

		#region General Days

		/// <summary>
		/// Gets the backcolor of general day.
		/// </summary>
		Color ICalendar.DayBackcolor
		{
			get { return EvaluateColorpression(DayBackcolor, CalendarData.DefaultDayBackcolor); }
		}

		/// <summary>
		/// Gets the border color of general day.
		/// </summary>
		Color ICalendar.DayBorderColor
		{
			get { return EvaluateColorpression(DayBorderColor, CalendarData.DefaultDayBorderColor); }
		}

		/// <summary>
		/// Gets the border width of general day.
		/// </summary>
		Length ICalendar.DayBorderWidth
		{
			get { return EvaluateLengthExpression(DayBorderWidth, CalendarData.DefaultDayBorderWidth); }
		}

		/// <summary>
		/// Gets the border style of general day.
		/// </summary>
		BorderStyle ICalendar.DayBorderStyle
		{
			get { return EvaluateExpression(DayBorderStyle, CalendarData.DefaultDayBorderStyle); }
		}

		/// <summary>
		/// Gets the font family of general day.
		/// </summary>
		string ICalendar.DayFontFamily
		{
			get { return EvaluateStringExpression(DayFontFamily, CalendarData.DefaultDayFontFamily); }
		}

		/// <summary>
		/// Gets the font size of general day.
		/// </summary>
		Length ICalendar.DayFontSize
		{
			get { return EvaluateLengthExpression(DayFontSize, CalendarData.DefaultDayFontSize); }
		}

		/// <summary>
		/// Gets the font style of general day.
		/// </summary>
		FontStyle ICalendar.DayFontStyle
		{
			get { return EvaluateExpression(DayFontStyle, CalendarData.DefaultDayFontStyle); }
		}

		/// <summary>
		/// Gets the font weight of general day.
		/// </summary>
		FontWeight ICalendar.DayFontWeight
		{
			get { return EvaluateExpression(DayFontWeight, CalendarData.DefaultDayFontWeight); }
		}

		/// <summary>
		/// Gets the font decoration of general day.
		/// </summary>
		FontDecoration ICalendar.DayFontDecoration
		{
			get { return EvaluateExpression(DayFontDecoration, CalendarData.DefaultDayFontDecoration); }
		}

		/// <summary>
		/// Gets the font color of general day.
		/// </summary>
		Color ICalendar.DayFontColor
		{
			get { return EvaluateColorpression(DayFontColor, CalendarData.DefaultDayFontColor); }
		}

		/// <summary>
		/// Gets the text align of general day.
		/// </summary>
		TextAlign ICalendar.DayTextAlign
		{
			get { return EvaluateExpression(DayTextAlign, CalendarData.DefaultDayTextAlign); }
		}

		/// <summary>
		/// Gets the vertical align of general day.
		/// </summary>
		VerticalAlign ICalendar.DayVerticalAlign
		{
			get { return EvaluateExpression(DayVerticalAlign, CalendarData.DefaultDayVerticalAlign); }
		}

		#endregion

		#region Weekend Days

		/// <summary>
		/// Gets the backcolor of weekend day.
		/// </summary>
		Color ICalendar.WeekendBackcolor
		{
			get { return EvaluateColorpression(WeekendBackcolor, CalendarData.DefaultWeekendBackcolor); }
		}

		/// <summary>
		/// Gets the border color of weekend day.
		/// </summary>
		Color ICalendar.WeekendBorderColor
		{
			get
			{
				ExpressionInfo borderColor = EvaluatorService.IsEmptyExpression(WeekendBorderColor) ? DayBorderColor : WeekendBorderColor;
				return EvaluateColorpression(borderColor, CalendarData.DefaultWeekendBorderColor);
			}
		}

		/// <summary>
		/// Gets the border width of weekend day.
		/// </summary>
		Length ICalendar.WeekendBorderWidth
		{
			get
			{
				ExpressionInfo width = EvaluatorService.IsEmptyExpression(WeekendBorderWidth) ? DayBorderColor : WeekendBorderWidth;
				return EvaluateLengthExpression(width, CalendarData.DefaultWeekendBorderWidth);
			}
		}

		/// <summary>
		/// Gets the border style of weekend day.
		/// </summary>
		BorderStyle ICalendar.WeekendBorderStyle
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(WeekendBorderStyle) ? DayBorderStyle : WeekendBorderStyle;
				return EvaluateExpression(style, CalendarData.DefaultWeekendBorderStyle);
			}
		}

		/// <summary>
		/// Gets the font family of weekend day.
		/// </summary>
		string ICalendar.WeekendFontFamily
		{
			get
			{
				ExpressionInfo family = EvaluatorService.IsEmptyExpression(WeekendFontFamily) ? DayFontFamily : WeekendFontFamily;
				return EvaluateStringExpression(family, CalendarData.DefaultWeekendFontFamily);
			}
		}

		/// <summary>
		/// Gets the font size of weekend day.
		/// </summary>
		Length ICalendar.WeekendFontSize
		{
			get
			{
				ExpressionInfo size = EvaluatorService.IsEmptyExpression(WeekendFontSize) ? DayFontSize : WeekendFontSize;
				return EvaluateLengthExpression(size, CalendarData.DefaultWeekendFontSize);
			}
		}

		/// <summary>
		/// Gets the font style of weekend day.
		/// </summary>
		FontStyle ICalendar.WeekendFontStyle
		{
			get
			{
				ExpressionInfo style = EvaluatorService.IsEmptyExpression(WeekendFontStyle) ? DayFontStyle : WeekendFontStyle;
				return EvaluateExpression(style, CalendarData.DefaultWeekendFontStyle);
			}
		}

		/// <summary>
		/// Gets the font weight of weekend day.
		/// </summary>
		FontWeight ICalendar.WeekendFontWeight
		{
			get
			{
				ExpressionInfo weight = EvaluatorService.IsEmptyExpression(WeekendFontWeight) ? DayFontWeight : WeekendFontWeight;
				return EvaluateExpression(weight, CalendarData.DefaultWeekendFontWeight);
			}
		}

		/// <summary>
		/// Gets the font decoration of weekend day.
		/// </summary>
		FontDecoration ICalendar.WeekendFontDecoration
		{
			get
			{
				ExpressionInfo decoration = EvaluatorService.IsEmptyExpression(WeekendFontDecoration) ? DayFontDecoration : WeekendFontDecoration;
				return EvaluateExpression(decoration, CalendarData.DefaultWeekendFontDecoration);
			}
		}

		/// <summary>
		/// Gets the font color of weekend day.
		/// </summary>
		Color ICalendar.WeekendFontColor
		{
			get
			{
				ExpressionInfo color = EvaluatorService.IsEmptyExpression(WeekendFontColor) ? DayFontColor : WeekendFontColor;
				return EvaluateColorpression(color, CalendarData.DefaultWeekendFontColor);
			}
		}

		/// <summary>
		/// Gets the text align of weekend day.
		/// </summary>
		TextAlign ICalendar.WeekendTextAlign
		{
			get
			{
				ExpressionInfo align = EvaluatorService.IsEmptyExpression(WeekendTextAlign) ? DayTextAlign : WeekendTextAlign;
				return EvaluateExpression(align, CalendarData.DefaultWeekendTextAlign);
			}
		}

		/// <summary>
		/// Gets the vertical align of weekend day.
		/// </summary>
		VerticalAlign ICalendar.WeekendVerticalAlign
		{
			get
			{
				ExpressionInfo align = EvaluatorService.IsEmptyExpression(WeekendVerticalAlign) ? DayVerticalAlign : WeekendVerticalAlign;
				return EvaluateExpression(align, CalendarData.DefaultWeekendVerticalAlign);
			}
		}

		#endregion

		#region Holidays

		/// <summary>
		/// Gets the backcolor of holiday.
		/// </summary>
		Color ICalendar.HolidayBackcolor
		{
			get { return EvaluateColorpression(HolidayBackcolor, CalendarData.DefaultHolidayBackcolor); }
		}

		/// <summary>
		/// Gets the border color of holiday.
		/// </summary>
		Color ICalendar.HolidayBorderColor
		{
			get { return EvaluateColorpression(HolidayBorderColor, CalendarData.DefaultHolidayBorderColor); }
		}

		/// <summary>
		/// Gets the border width of holiday.
		/// </summary>
		Length ICalendar.HolidayBorderWidth
		{
			get { return EvaluateLengthExpression(HolidayBorderWidth, CalendarData.DefaultHolidayBorderWidth); }
		}

		/// <summary>
		/// Gets the border style of holiday.
		/// </summary>
		BorderStyle ICalendar.HolidayBorderStyle
		{
			get { return EvaluateExpression(HolidayBorderStyle, CalendarData.DefaultHolidayBorderStyle); }
		}

		/// <summary>
		/// Gets the font family of holiday.
		/// </summary>
		string ICalendar.HolidayFontFamily
		{
			get { return EvaluateStringExpression(HolidayFontFamily, CalendarData.DefaultHolidayFontFamily); }
		}

		/// <summary>
		/// Gets the font size of holiday.
		/// </summary>
		Length ICalendar.HolidayFontSize
		{
			get { return EvaluateLengthExpression(HolidayFontSize, CalendarData.DefaultHolidayFontSize); }
		}

		/// <summary>
		/// Gets the font style of holiday.
		/// </summary>
		FontStyle ICalendar.HolidayFontStyle
		{
			get { return EvaluateExpression(HolidayFontStyle, CalendarData.DefaultHolidayFontStyle); }
		}

		/// <summary>
		/// Gets the font weight of holiday.
		/// </summary>
		FontWeight ICalendar.HolidayFontWeight
		{
			get { return EvaluateExpression(HolidayFontWeight, CalendarData.DefaultHolidayFontWeight); }
		}

		/// <summary>
		/// Gets the font decoration of holiday.
		/// </summary>
		FontDecoration ICalendar.HolidayFontDecoration
		{
			get { return EvaluateExpression(HolidayFontDecoration, CalendarData.DefaultHolidayFontDecoration); }
		}

		/// <summary>
		/// Gets the font color of holiday.
		/// </summary>
		Color ICalendar.HolidayFontColor
		{
			get { return EvaluateColorpression(HolidayFontColor, CalendarData.DefaultHolidayFontColor); }
		}

		/// <summary>
		/// Gets the text align of holiday.
		/// </summary>
		TextAlign ICalendar.HolidayTextAlign
		{
			get { return EvaluateExpression(HolidayTextAlign, CalendarData.DefaultHolidayTextAlign); }
		}

		/// <summary>
		/// Gets the vertical align of holiday.
		/// </summary>
		VerticalAlign ICalendar.HolidayVerticalAlign
		{
			get { return EvaluateExpression(HolidayVerticalAlign, CalendarData.DefaultHolidayVerticalAlign); }
		}


		#endregion

		#region Day Headers
		/// <summary>
		/// Gets the backcolor of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersBackcolor
		{
			get { return EvaluateColorpression(DayHeadersBackColor, CalendarData.DefaultDayHeadersBackcolor); }
		}

		/// <summary>
		/// Gets the border color of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersBorderColor
		{
			get { return EvaluateColorpression(DayHeadersBorderColor, CalendarData.DefaultDayHeadersBorderColor); }
		}

		/// <summary>
		/// Gets the border width of the day Headers.
		/// </summary>
		Length ICalendar.DayHeadersBorderWidth
		{
			get { return EvaluateLengthExpression(DayHeadersBorderWidth, CalendarData.DefaultDayHeadersBorderWidth); }
		}

		/// <summary>
		/// Gets the border style of the day Headers.
		/// </summary>
		BorderStyle ICalendar.DayHeadersBorderStyle
		{
			get { return EvaluateExpression(DayHeadersBorderStyle, CalendarData.DefaultDayHeadersBorderStyle); }
		}

		/// <summary>
		/// Gets the font family of the day Headers.
		/// </summary>
		string ICalendar.DayHeadersFontFamily
		{
			get { return EvaluateStringExpression(DayHeadersFontFamily, CalendarData.DefaultDayHeadersFontFamily); }
		}

		/// <summary>
		/// Gets the font size of the day Headers.
		/// </summary>
		Length ICalendar.DayHeadersFontSize
		{
			get { return EvaluateLengthExpression(DayHeadersFontSize, CalendarData.DefaultDayHeadersFontSize); }
		}

		/// <summary>
		/// Gets the font style of the day Headers.
		/// </summary>
		FontStyle ICalendar.DayHeadersFontStyle
		{
			get { return EvaluateExpression(DayHeadersFontStyle, CalendarData.DefaultDayHeadersFontStyle); }
		}

		/// <summary>
		/// Gets the font weight of the day Headers.
		/// </summary>
		FontWeight ICalendar.DayHeadersFontWeight
		{
			get { return EvaluateExpression(DayHeadersFontWeight, CalendarData.DefaultDayHeadersFontWeight); }
		}

		/// <summary>
		/// Gets the font decoration of the day Headers.
		/// </summary>
		FontDecoration ICalendar.DayHeadersFontDecoration
		{
			get { return EvaluateExpression(DayHeadersFontDecoration, CalendarData.DefaultDayHeadersFontDecoration); }
		}

		/// <summary>
		/// Gets the font color of the day Headers.
		/// </summary>
		Color ICalendar.DayHeadersFontColor
		{
			get { return EvaluateColorpression(DayHeadersFontColor, CalendarData.DefaultDayHeadersFontColor); }
		}

		#endregion

		/// <summary>
		/// Gets the ICalendarCultureService for the calendar designer.
		/// </summary>
		CalendarCulture ICalendar.CalendarCultureService
		{
			get { return _calendarCultureService ?? (_calendarCultureService = new CalendarDesignerCulture(ReportItem)); }
		}

		/// <summary>
		/// Obtains and returns the <see cref="System.Drawing.Image"/> by image style definition.
		/// </summary>
		/// <param name="imageStyle"></param>
		/// <returns></returns>
		System.Drawing.Image ICalendar.GetImage(ImageStyle imageStyle)
		{
			if (_imageService == null)
				_imageService = new DesignerImageLocatorService(this);
			return _imageService.GetImage(imageStyle);
		}

		/// <summary>
		/// Specifies the direction of text.
		/// </summary>
		Direction ICalendar.Direction
		{
			get { return EvaluateExpression(ReportItem.Style.Direction, CalendarData.DefaultDirection); }
		}

		/// <summary>
		/// Gets the collection of calendar appointments.
		/// </summary>
		Collection<Appointment> ICalendar.Appointments
		{
			get
			{
				if (_appointments == null)
				{
					_appointments = new Collection<Appointment>();
					foreach (Appointment appointment in DummyAppointments)
					{
						Appointment clone = (Appointment)appointment.Clone();
						_appointments.Add(clone);
					}
				}
				return _appointments;
			}
		}
		private Collection<Appointment> _appointments;

		/// <summary>
		/// Gets the gap between appointment along y-axis.
		/// </summary>
		/// <remarks>Using 2 pixels to take into account overlap of 1 pixel border on the bottom and top of events bordering the gap.</remarks>
		Length ICalendar.AppointmentGap
		{
			get { return _appointmentGap; }
		}
		private readonly Length _appointmentGap = Length.FromTwips(RenderUtils.ConvertPixelsToTwips(2), Length.Unit.Points);

		private void RefreshAppointments()
		{
			// backcolor
			Color backcolor = EvaluateColorpression(AppointmentBackcolor, CalendarData.DefaultAppointmentBackcolor);
			// border color
			Color bordercolor = EvaluateColorpression(AppointmentBorderColor, CalendarData.DefaultAppointmentBorderColor);
			// font family
			string family = EvaluateStringExpression(AppointmentFontFamily, CalendarData.DefaultAppointmentFontFamily);
			// font size
			Length size = EvaluateLengthExpression(AppointmentFontSize, CalendarData.DefaultAppointmentFontSize);
			// font style
			FontStyle style = EvaluateExpression(AppointmentFontStyle, CalendarData.DefaultAppointmentFontStyle);
			// font weight
			FontWeight weight = EvaluateExpression(AppointmentFontWeight, CalendarData.DefaultAppointmentFontWeight);
			// font decoration
			FontDecoration decoration = EvaluateExpression(AppointmentFontDecoration, CalendarData.DefaultAppointmentFontDecoration);
			// font color
			Color fontcolor = EvaluateColorpression(AppointmentFontColor, CalendarData.DefaultAppointmentFontColor);
			// text align
			TextAlign textAlign = EvaluateExpression(AppointmentTextAlign, CalendarData.DefaultAppointmentTextAlign);
			// format
			string format = EvaluateStringExpression(AppointmentFormat, CalendarData.DefaultAppointmentFormat);
			// image source
			ImageSource imageSource = EvaluateExpression(AppointmentImageSource, CalendarData.DefaultAppointmentImageSource);
			// image value
			string imageValue = EvaluateStringExpression(AppointmentImageValue, CalendarData.DefaultAppointmentImageValue);
			// image mime type
			string mimeType = EvaluateStringExpression(AppointmentImageMimeType, CalendarData.DefaultAppointmentMimeType);

			// update the appointments
			foreach (Appointment appointment in Calendar.Appointments)
			{
				appointment.Backcolor = backcolor;
				appointment.BorderColor = bordercolor;
				appointment.FontFamily = family;
				appointment.FontSize = size;
				appointment.FontStyle = style;
				appointment.FontWeight = weight;
				appointment.FontDecoration = decoration;
				appointment.FontColor = fontcolor;
				appointment.TextAlign = textAlign;
				appointment.Format = format;
				appointment.ImageSource = imageSource;
				appointment.ImageValue = imageValue;
				appointment.MimeType = mimeType;
			}
		}

		#endregion

		#region IWizardProvider Members

		/// <summary>
		/// Creates the a WizardGroup that should be displayed for the designer by the Designer Action service.
		/// </summary>
		/// <returns>The <see cref="IWizardNavigator"/> to be displayed or null to not display a UI.</returns>
		IWizardNavigator IWizardProvider.CreateWizard()
		{
			switch (_smartPanelTarget)
			{
				case SmartPanelTarget.Main:
					return CalendarSmartPanelBuilder.CreateHomePageWizard(this);
				case SmartPanelTarget.Action:
					return CalendarSmartPanelBuilder.CreateCalendarNavigationPage(this);
				default:
					return CalendarSmartPanelBuilder.CreateDataPage(this);
			}
		}

		#endregion

		#region Glyph Drawing

		/// <summary>
		/// Glyph painting
		/// </summary>
		protected override void DrawGlyph(Graphics graphics, System.Drawing.Rectangle bounds)
		{
			// recalibrate graphics to twips
			RectangleF rect = bounds;
			ScaleToTwipsGraphicsAndBound(graphics, ref rect);

			// create gdi canvas wrapper to use in renderer
			IDrawingCanvas canvas = GraphicsCanvasFactory.Create(graphics);

			// render calendar to canvas
			CalendarRenderer.Instance.Render(this, new MonthInfo(DateTime.Now), canvas, rect);
		}

		/// <summary>
		/// Translates graphics measure units to twips and re-measures bound rectangle.
		/// </summary>
		public static void ScaleToTwipsGraphicsAndBound(Graphics graphics, ref RectangleF bounds)
		{
			// scale graphics to twips
			graphics.PageUnit = GraphicsUnit.Point;
			graphics.PageScale = 0.05f; // 1/20
										// scale boundaries to twips
			float twipsPerPixelX = 1440f / graphics.DpiX;
			float twipsPerPixelY = 1440f / graphics.DpiY;

			bounds.X = bounds.X * twipsPerPixelX;
			bounds.Y = bounds.Y * twipsPerPixelY;
			bounds.Width = bounds.Width * twipsPerPixelX;
			bounds.Height = bounds.Height * twipsPerPixelY;
		}

		/// <summary>
		/// Specifies a glyph for this component designer.
		/// </summary>
		public override Glyph ControlGlyph
		{
			get { return _glyph; }
		}

		private CalendarControlGlyph _glyph;

		#region CalendarControlGlyph class

		/// <summary>
		/// Represents control glyph for bullet chart.
		/// </summary>
		private sealed class CalendarControlGlyph : ControlBodyGlyph
		{
			private readonly CalendarDesigner _designer;

			public CalendarControlGlyph(CalendarDesigner regionDesigner)
				: base(regionDesigner.BehaviorService, regionDesigner)
			{
				_designer = regionDesigner;
				Behavior = new CalendarBehavior(_designer);
			}

			public override Rectangle ClipBounds
			{
				get
				{
					Rectangle clipArea = ControlBounds;
					// copy-pasted from base class, no idea why += 1 is here
					clipArea.Width += 1;
					clipArea.Height += 1;
					return clipArea;
				}
			}

			public override Rectangle Bounds
			{
				get
				{
					if (_designer == null || _designer._fixedSizeGlyph == null) return base.Bounds;
					return _designer._fixedSizeGlyph.GetFixedSizeBounds();
				}
			}

			protected override Rectangle ControlBounds
			{
				get { return base.Bounds; }
			}

			/// <summary>
			/// Provides paint logic.
			/// </summary>
			public override void Paint(PaintEventArgs pe)
			{
				if (_designer._fixedSizeGlyph != null)
					_designer._fixedSizeGlyph.RenderFixedSizeFrame(pe.Graphics);

				// base Paint() should be called at first to avoid rendering of control borders 
				base.Paint(pe);
				if (!PaintSelectionOnly(pe))
				{
					_designer.OnItemPaint(pe);
				}
			}

			private Rectangle GetFixedSizeBounds(DesignSize fixedSize, Rectangle bounds)
			{
				bounds.Width = Math.Max(bounds.Width, _designer.ConversionService.ToDisplayWidth(fixedSize.Width));
				bounds.Height = Math.Max(bounds.Height, _designer.ConversionService.ToDisplayHeight(fixedSize.Height));
				return bounds;
			}

			private static void RenderFixedSizeFrame(Graphics graphics, Rectangle bounds, Rectangle fixedSizeBounds, Color color)
			{
				GraphicsState state = graphics.Save();
				Region clip = graphics.Clip;
				bounds.Inflate(1, 1);
				clip.Exclude(bounds);
				graphics.Clip = clip;

				using (Brush brush = new HatchBrush(HatchStyle.WideUpwardDiagonal, color, Color.Transparent))
				{
					graphics.FillRectangle(brush, fixedSizeBounds);
				}
				using (Pen pen = new Pen(color))
				{
					graphics.DrawRectangle(pen, fixedSizeBounds);
				}

				graphics.Restore(state);
			}

			/// <summary>
			/// Invalidates the calendar glyph.
			/// </summary>
			public void Invalidate()
			{
				BehaviorService.Invalidate();
			}

			public override Guid UIContext
			{
				get
				{
					return new Guid("30D85F8F-BC3E-45de-A376-206FEAE819A5");
				}
			}
		}

		#endregion

		#endregion

		#region CalendarBehavior class

		/// <summary>
		/// Implements behavior for bullet chart glyphs.
		/// </summary>
		private sealed class CalendarBehavior : DefaultControlBehavior
		{
			public CalendarBehavior(CalendarDesigner regionDesigner)
			{
				if (regionDesigner == null)
				{
					throw new ArgumentNullException("Invalid regionDesigner");
				}
			}

			/// <summary>
			/// Permits handling of drag over.
			/// </summary>
			public override void OnDragOver(Glyph glyph, DragEventArgs e)
			{
				if (e == null) return;
				if (!TrackingInfo.TrackMove || e.Data == null)
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				DragDropInfo newDropInfo = new DragDropInfo(this, glyph, e);
				if (newDropInfo.DataFieldValueResolver == null || !newDropInfo.DataFieldValueResolver.IsDataField())
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				DragDrop = newDropInfo;
				e.Effect = DragDrop.Effects;
			}

			/// <summary>
			/// Hadles drag-and-drop at specified location.
			/// </summary>
			public override void OnDragDrop(Glyph glyph, DragEventArgs e)
			{
				if (!DragDrop.Dragging || !TrackingInfo.TrackMove || e == null || (e.AllowedEffect & (DragDropEffects.Copy | DragDropEffects.Move)) == 0)
				{
					base.OnDragDrop(glyph, e);
					return;
				}
				if (glyph == null || e.Data == null)
				{
					Debug.Fail("Unexpected call to OnDragDrop");
					base.OnDragDrop(glyph, e);
					return;
				}
				DragDrop = new DragDropInfo(this, glyph, e);
				DropCalendarData();
				TrackingInfo = new MouseTrackingInfo();
				DragDrop = new DragDropInfo();
				base.OnDragDrop(glyph, e);
			}

			/// <summary>
			/// Helper method performing drop of the dragged fields to the underlying data adorner glyph
			/// </summary>
			private void DropCalendarData()
			{
				if (DragDrop.DataFieldValueResolver == null || !DragDrop.DataFieldValueResolver.IsDataField())
				{
					Trace.TraceInformation("Invalid data to drop");
				}
			}
		}

		#endregion

		#region Calendar Action List

		private sealed class CalendarActionList : DesignerActionList
		{
			private const string EditDataCommandText = "Edit Data...";
			private const string EditActionCommandText = "Edit Action...";
			private readonly CalendarDesigner _designer;

			public CalendarActionList(CalendarDesigner designer) : base(designer.Component)
			{
				Debug.Assert(designer != null && designer.Component != null);
				_designer = designer;
			}

			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection items = new DesignerActionItemCollection();
				InferCommandItems(this, items);
				return items;
			}

			// ReSharper disable UnusedMember.Local
			// ReSharper disable UnusedParameter.Local

			#region Standard commands

			[UICommand(Utils.VSStandardMenuGroupGuidString, Utils.cmdidPaste, "UpdateStatus")]
			private CommandStatus OnPaste(ICollection paramsIn, out ICollection paramsOut)
			{
				// The command processing was added to avoid unwanted operations because they will be done improperly
				paramsOut = null;
				return CommandStatus.UnsupportedCommand;
			}

			#endregion

			[UICommand(Utils.ReportsUICommandsGroupGuidString, Utils.cmdidEditCalendarData, null, EditDataCommandText, true, false, "")]
			private CommandStatus OnEditCalendarData(ICollection paramsIn, out ICollection paramsOut)
			{
				paramsOut = null;
				DesignerActionUIService actionUI = null;
				_designer._smartPanelTarget = SmartPanelTarget.Data;
				if (Component != null && Component.Site != null)
					actionUI = Component.Site.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
				if (actionUI != null)
					actionUI.ShowUI(Component);
				else
					Trace.TraceError("DesignerActionUIService is not available.");
				_designer._smartPanelTarget = SmartPanelTarget.Main;
				return CommandStatus.NoError;
			}

			[UICommand(Utils.ReportsUICommandsGroupGuidString, Utils.cmdidEditAction, null, EditActionCommandText, true, false, "")]
			private CommandStatus OnEditCalendarAction(ICollection paramsIn, out ICollection paramsOut)
			{
				paramsOut = null;
				DesignerActionUIService actionUI = null;
				_designer._smartPanelTarget = SmartPanelTarget.Action;
				if (Component != null && Component.Site != null)
					actionUI = Component.Site.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
				if (actionUI != null)
					actionUI.ShowUI(Component);
				else
					Trace.TraceError("DesignerActionUIService is not available.");
				_designer._smartPanelTarget = SmartPanelTarget.Main;
				return CommandStatus.NoError;
			}

			/// <summary>
			/// Used to handle command updating status for <see cref="OnPaste"/>. 
			/// See the <see cref="UICommandAttribute"/> arguments for how we specify this.
			/// </summary>
			private CommandState UpdateStatus(int cookie)
			{
				return new CommandState(false, false);
			}

			// ReSharper restore UnusedParameter.Local
			// ReSharper restore UnusedMember.Local
		}

		#endregion

		#region Smart Panel Target
		private enum SmartPanelTarget
		{
			Main,
			Data,
			Action
		}
		#endregion

		#region Property names to show in property grid ONLY (not for property binding), keep private

		// event style prop names	
		private const string EventCategory = "EventCategory";
		private const string EventStartDatePropertyName = "StartDate";
		private const string EventEndDatePropertyName = "EndDate";
		private const string EventValuePropertyName = "Value";
		// day header style property names
		internal const string DayHeadersPropertyName = "DayHeaderStyle";
		internal const string MonthTitlePropertyName = "MonthTitleStyle";
		internal const string FillerDayPropertyName = "FillerDayStyle";
		internal const string DayPropertyName = "DayStyle";
		internal const string WeekendPropertyName = "WeekendStyle";
		internal const string EventPropertyName = "EventStyle";

		#endregion

		/// <summary>
		/// Retrieves drillthrough's stuff from custom properties
		/// </summary>
		/// <returns></returns>
		private Drillthrough GetDrillthrough()
		{
			Drillthrough drillthrough = new Drillthrough();

			ExpressionInfo reportName = GetCustomProperty(AppointmentCustomProperties, CalendarData.ActionDrillthroughPropertyName, EvaluatorService.EmptyExpression);
			if (EvaluatorService.IsEmptyExpression(reportName))
			{
				return drillthrough;
			}

			drillthrough.ReportName = reportName.Expression;

			GetDrillthroughParameters(drillthrough);

			return drillthrough;
		}

		/// <summary>
		/// Retrieves drillthrough's parameters from custom properties and stores it into 
		/// specified drillthrough object
		/// </summary>
		private void GetDrillthroughParameters(Drillthrough drillthrough)
		{
			ExpressionInfo parameterList = GetCustomProperty(AppointmentCustomProperties, CalendarData.ActionDrillthroughParameterListPropertyName, EvaluatorService.EmptyExpression);
			if (EvaluatorService.IsEmptyExpression(parameterList))
			{
				return;
			}

			string[] parameterIds = parameterList.Expression.Split(CalendarData.ActionDrillthroughParameterDelimeter);
			if (parameterIds.Length == 0)
			{
				return;
			}

			foreach (string parameterId in parameterIds)
			{
				string parameterName = CalendarData.ActionDrillthroughParameterPropertyName + parameterId;
				ExpressionInfo parameterNameExpression = GetCustomProperty(
					AppointmentCustomProperties, parameterName, EvaluatorService.EmptyExpression);

				Parameter parameter = new Parameter();
				parameter.Name = parameterNameExpression;

				parameter.Omit = GetCustomProperty(AppointmentCustomProperties,
					parameterName + CalendarData.ActionDrillthroughParameterOmitSuffix, EvaluatorService.EmptyExpression);

				parameter.Value = GetCustomProperty(AppointmentCustomProperties,
					parameterName + CalendarData.ActionDrillthroughParameterValueSuffix, EvaluatorService.EmptyExpression);

				drillthrough.Parameters.Add(parameter);
			}
		}

		/// <summary>
		/// Stores drillthrough stuff into AppointmentCustomProperties collection
		/// </summary>
		private void SetDrillthrough(Drillthrough drillthrough)
		{
			if (drillthrough == null)
			{
				Debug.Fail("Drillthrough is null");
				return;
			}

			ClearDrillthroughParameters();

			SetCustomProperty(AppointmentCustomProperties, CalendarData.ActionDrillthroughPropertyName,
				ExpressionInfo.FromString(drillthrough.ReportName), EvaluatorService.EmptyExpression);

			if (drillthrough.Parameters.Count == 0)
			{
				return;
			}

			int idCounter = 0;
			StringBuilder parameterList = new StringBuilder();
			foreach (Parameter parameter in drillthrough.Parameters)
			{
				string parameterName = CalendarData.ActionDrillthroughParameterPropertyName + idCounter;
				SetCustomProperty(AppointmentCustomProperties,
					parameterName,
					ExpressionInfo.FromString(parameter.Name), EvaluatorService.EmptyExpression);

				SetCustomProperty(AppointmentCustomProperties,
					parameterName + CalendarData.ActionDrillthroughParameterOmitSuffix,
					parameter.Omit, EvaluatorService.EmptyExpression);

				SetCustomProperty(AppointmentCustomProperties,
					parameterName + CalendarData.ActionDrillthroughParameterValueSuffix,
					parameter.Value, EvaluatorService.EmptyExpression);

				parameterList.Append(idCounter);
				parameterList.Append(CalendarData.ActionDrillthroughParameterDelimeter);

				++idCounter;
			}

			string parameterListValue = parameterList.ToString();
			string separator = new string(CalendarData.ActionDrillthroughParameterDelimeter);
			ExpressionInfo parameterListExpression = ExpressionInfo.FromString(
				parameterListValue.Substring(0, parameterListValue.LastIndexOf(separator)));
			SetCustomProperty(AppointmentCustomProperties,
				CalendarData.ActionDrillthroughParameterListPropertyName,
				parameterListExpression, EvaluatorService.EmptyExpression);
		}

		/// <summary>
		/// Removes drillthrough parameters from custom properties
		/// </summary>
		private void ClearDrillthroughParameters()
		{
			if (AppointmentCustomProperties.Count == 0) return;

			for (int index = AppointmentCustomProperties.Count - 1; index >= 0; index--)
			{
				Debug.WriteLine(AppointmentCustomProperties[index].Name);
				if (AppointmentCustomProperties[index].Name.StartsWith(CalendarData.ActionDrillthroughPropertyName))
				{
					AppointmentCustomProperties.RemoveAt(index);
				}
			}
		}

		#region IValidateable Members

		/// <summary>
		/// Validates the current state of the Calendar.
		/// </summary>
		/// <param name="context">Provides context details during validation.</param>
		/// <returns>Returns zero or more entries providing information about the validation warnings or failures.</returns>
		public ValidationEntry[] Validate(ValidationContext context)
		{
			List<ValidationEntry> entries = new List<ValidationEntry>();
			ReportItem ri = Component as ReportItem;
			if (string.IsNullOrEmpty(DataSetName))
			{
				if (ri != null)
				{
					string message = string.Format(Resources.DataRegionMustSpecifyDataSet, new object[] { ri.Name });
					entries.Add(new ValidationEntry(Severity.Error, message,
						new InvalidOperationException(message), this));
				}
			}
			else
			{
				if (context.ContextualInformation.Contains("DataSetNames"))
				{
					StringCollection dataSetNames = context.ContextualInformation["DataSetNames"] as StringCollection;
					if (dataSetNames != null && !dataSetNames.Contains(DataSetName))
					{
						string message = string.Format(Resources.InvalidDataSetName, new object[] { DataSetName });
						entries.Add(new ValidationEntry(Severity.Error, message, new InvalidOperationException(message), this));
					}
				}
			}

			if (ri != null)
			{
				// RULE: Nested DataRegions are not supported in FPL
				var parent = ri.Parent;
				while (parent != null && !(parent is FixedPage))
				{
					if (parent is DataRegion)
					{
						string message = Resources.FplNestedDataRegions;
						entries.Add(new ValidationEntry(Severity.Error, message, null, this));
						break;
					}
					parent = parent.Parent;
				}
			}

			PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.PropertyType != typeof(ExpressionInfo)) continue;
				ExpressionInfo propertyValue = (ExpressionInfo)propertyInfo.GetValue(this, null);
				Utils.CheckIfExpressionIsBad(propertyValue, entries, context, propertyInfo.Name, ReportItem);
			}

			return entries.ToArray();
		}

		#endregion
	}
}
