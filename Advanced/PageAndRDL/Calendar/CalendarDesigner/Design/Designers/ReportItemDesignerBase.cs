using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.Calendar.Rendering;
using GrapeCity.ActiveReports.Design.DdrDesigner;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.Designers;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Rectangle = System.Drawing.Rectangle;

namespace GrapeCity.ActiveReports.Calendar.Design.Designers
{
	/// <summary>
	/// Represents the base class for dashboard report item designers.
	/// </summary>
	public  class ReportItemDesignerBase : CustomReportItemDesigner
	{
		private readonly Collection<PropertyDescriptor> _propertyDescriptors = new Collection<PropertyDescriptor>();

		internal new CustomReportItem ReportItem
		{
			get { return base.ReportItem; }
		}

		#region Properties Initialization

		/// <summary>
		/// Gets the collection of custom property descriptors.
		/// </summary>
		internal Collection<PropertyDescriptor> PropertyDescriptors
		{
			get { return _propertyDescriptors; }
		}

		/// <summary>
		/// Adds custom properties to the property collection.
		/// </summary>
		protected virtual void AddCustomProperties() { }

		/// <summary>
		/// Adds the custom property collection to the property editor.
		/// </summary>
		public sealed override void PostFilterProperties(IDictionary properties)
		{
			base.PostFilterProperties(properties);

			PropertyFilterImpl.Apply(properties, PropertyDescriptors, null, GetDesignerHost());
		}

		#region Custom properties

		internal IComponentChangeService ComponentChangeService
		{
			get
			{
				if (_componentChangeService == null)
				{
					// Retrieve the site
					ISite site = ReportItem.Site;
					// Retrieve the service
					if (site != null)
					{
						_componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
				}
				return _componentChangeService;
			}
		}
		private IComponentChangeService _componentChangeService;

		internal ExpressionInfo GetCustomProperty(CustomPropertyDefinitionCollection properties, string name, ExpressionInfo defaultValue)
		{
			// get property
			CustomPropertyDefinition property = properties[name];

			// if property is not set then return default value
			return property == null ? defaultValue : property.Value;
		}

		internal void SetCustomProperty(CustomPropertyDefinitionCollection properties, string name, ExpressionInfo value, ExpressionInfo defaultValue)
		{
			// Fire component changing
			if (ComponentChangeService != null)
			{
				ComponentChangeService.OnComponentChanging(ReportItem, null);
			}

			// get property
			CustomPropertyDefinition property = properties[name];

			bool isDefaultValue = defaultValue == value;
			if (isDefaultValue)
			{
				// reset the property value
				if (property != null)
					properties.Remove(property);
			}
			else
			{
				// set property value
				if (property == null)
				{
					properties.Add(new CustomPropertyDefinition(name, value));
				}
				else
				{
					property.Value = value;
				}
			}

			// Fire component changed
			if (ComponentChangeService != null)
			{
				ComponentChangeService.OnComponentChanged(ReportItem, null, null, null);
			}
		}

		#endregion

		#endregion

		#region Glyph Drawing

		/// <summary>
		/// Does the glyph painting.
		/// </summary>
		/// <param name="pe"></param>
		/// <returns></returns>
		protected sealed override bool OnItemPaint(PaintEventArgs pe)
		{
			if (pe.ClipRectangle.Width <= 0 || pe.ClipRectangle.Height <= 0) return false;

			GraphicsState state = pe.Graphics.Save();
			try
			{
				// clear own bound rectangle
				pe.Graphics.SetClip(pe.ClipRectangle, CombineMode.Intersect);
				// add gaps to avoid artifacts on left and bottom borders due to clipping
				const int SideGap = 1;
				Rectangle bounds = pe.ClipRectangle;
				bounds.Width -= SideGap;
				bounds.Height -= SideGap;
				bounds = ConversionService.ReverseScale(bounds);
				if (bounds.Width <= 0 || bounds.Height <= 0) return false;
				//Case 36389: In some cases, the appointment text was not displaying underlined when it should have been.  Work around is to
				// use the graphics of a bitmap and then draw the bitmap to the original designers graphics.
				using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						//Offset the graphics to compensate for the clip bounds if they are not (0,0).
						graphics.TranslateTransform(
							-RenderUtils.ConvertPixelsToTwips(bounds.Left, pe.Graphics.DpiX),
							-RenderUtils.ConvertPixelsToTwips(bounds.Top, pe.Graphics.DpiY));
						DrawGlyph(graphics, bounds);
					}

					float scaleFactor = ConversionService.ScalingFactor;
					pe.Graphics.ScaleTransform(scaleFactor, scaleFactor);

					pe.Graphics.DrawImageUnscaled(bitmap, bounds);
				}
			}
			finally
			{
				pe.Graphics.Restore(state);
			}
			return true;
		}

		/// <summary>
		/// Draws the appearance glyph (border and backdrop), overidden by the control designers.
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="bounds"></param>
		protected virtual void DrawGlyph(Graphics graphics, Rectangle bounds) { }

		#endregion

		#region Evaluation Utils

		/// <summary>
		/// Gets the internal wrapper for evaluation service.
		/// </summary>
		internal EvaluatorService EvaluatorService
		{
			get { return _evaluator ?? (_evaluator = EvaluatorService.Create(Component)); }
		}
		private EvaluatorService _evaluator;

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a <see cref="String" />.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		/// <param name="defaultValue">The default value to return when the conversion fails or nothing to convert</param>
		protected string EvaluateStringExpression(ExpressionInfo expression, string defaultValue)
		{
			if (expression != null && expression.IsConstant)
			{
				return Convert.ToString(EvaluatorService.EvaluateExpression(expression), CultureInfo.InvariantCulture);
			}
			return defaultValue;
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a type.
		/// </summary>
		/// <typeparam name="T">The specified type.</typeparam>
		/// <param name="expression">The expression to evaluate.</param>
		/// <param name="defaultValue">The default value to use when the conversion fails or nothing to convert</param>
		protected T EvaluateExpression<T>(ExpressionInfo expression, T defaultValue) where T : struct
		{
			// convert an expression to the string
			string stringValue = EvaluatorService.EvaluateStringExpression(expression);
			// set the default value
			T result = defaultValue;
			// convert the string to enum value
			if (Enum.IsDefined(typeof(T), stringValue))
			{
				result = (T)Enum.Parse(typeof(T), stringValue);
			}
			return result;
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a color.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		/// <param name="defaultColor">The default color to use when the conversion fails or nothing to convert</param>
		internal Color EvaluateColorpression(ExpressionInfo expression, Color defaultColor)
		{
			Color color = defaultColor;
			if (expression != null && expression.IsConstant)
			{
				color = Utils.ParseColor(EvaluatorService.EvaluateStringExpression(expression), color);
			}
			return color;
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a length.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		/// <param name="defaultLength">The default length to use when the conversion fails or nothing to convert</param>
		internal Length EvaluateLengthExpression(ExpressionInfo expression, Length defaultLength)
		{
			Length length = defaultLength;
			if (expression != null && expression.IsConstant)
			{
				Length lineWidthValue = EvaluatorService.EvaluateStringExpression(expression);
				if (lineWidthValue.IsValid)
					length = lineWidthValue;
			}
			return length;
		}

		#endregion

		internal const string DocumentMapLabelPropertyName = "Label";
		internal const string BookmarkIDPropertyName = "Bookmark";
		internal const string NamePropertyName = "Name";
		internal const string TooltipPropertyName = "ToolTip";
		internal const string DataElementNamePropertyName = "DataElementName";
		internal const string DataElementOutputPropertyName = "DataElementOutput";
	}
}
