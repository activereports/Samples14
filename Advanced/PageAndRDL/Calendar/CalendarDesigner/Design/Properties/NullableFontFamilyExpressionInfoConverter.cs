using System.ComponentModel;
using System.Globalization;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;


namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// NullableFontFamilyExpressionInfoConverter
	/// </summary>
	////[DoNotObfuscateType]
	internal sealed class NullableFontFamilyExpressionInfoConverter : FontFamilyExpressionInfoConverter
	{
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				ExpressionInfo expression = ExpressionInfo.FromString((string)value);
				if (EvaluatorService.IsEmptyExpression(expression))
					return expression;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
