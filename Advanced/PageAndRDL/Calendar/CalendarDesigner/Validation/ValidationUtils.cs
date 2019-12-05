using System;
using System.Globalization;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Calendar.Validation
{
	/// <summary>
	/// Represents the validation utils.
	/// </summary>
	public class ValidationUtils
	{
		/// <summary>
		/// Checks if a length expression preperty is literal. If so, it validates it against the specified min/max range limit.
		/// </summary>
		/// <param name="evaluator">the evaluator to use for evaluation</param>
		/// <param name="expression"><see cref="ExpressionInfo"/> value to validate.</param>
		/// <param name="min"><see cref="Length"/> value representing the minium size allowed</param>
		/// <param name="max"><see cref="Length"/> value representing the maximum size allowed</param>
		public static void ValidateLength(EvaluatorService evaluator, ExpressionInfo expression, Length min, Length max)
		{
			if (expression != null && expression.IsConstant && !EvaluatorService.IsEmptyExpression(expression))
			{
				object result = evaluator.EvaluateExpression(expression);
				if (result == null)
					return;
				string temp = result.ToString();
				if (temp.Length > 0)
				{
					Length size = temp.Trim();
					ValidateLength(size, min, max);
				}
			}
		}

		/// <summary>
		/// Validates a length to make sure it is valid and is within the specified min/max range limit.
		/// </summary>
		/// <param name="value"><see cref="Length"/> value to validate</param>
		/// <param name="min"><see cref="Length"/>value representing the minimun size allowed</param>
		/// <param name="max"><see cref="Length"/>value representing the maximum size allowed</param>
		public static void ValidateLength(Length value, Length min, Length max)
		{
			if (value == Length.Empty)
				return;
			if (!value.IsValid)
			{
				string message = Resources.InvalidLengthUnit;
				throw new ArgumentException(message);
			}
			if (value.ToTwips() < min.ToTwips() || value.ToTwips() > max.ToTwips())
			{
				string message = string.Format(Resources.InvalidLengthUnitRange,
					new object[] { min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture) });
				throw new ArgumentOutOfRangeException(null, message);
			}
		}
	}
}
