using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using GrapeCity.ActiveReports.Expressions;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Calendar
{
	/// <summary>
	/// Represents a wrapper for <see cref="IExpressionEvaluatorService"/>.
	/// </summary>
	public sealed class EvaluatorService
	{
		private readonly IExpressionEvaluatorService _evaluator;

		/// <summary>
		/// Private ctor.
		/// </summary>
		private EvaluatorService(IExpressionEvaluatorService evaluator)
		{
			if (evaluator == null)
				throw new ArgumentNullException("evaluator");
			_evaluator = evaluator;
		}

		/// <summary>
		/// Returns a reference to <see cref="IExpressionEvaluatorService"/> or null if it is not available (e.g. designer/component not yet initialized / sited?).
		/// </summary>
		private static IExpressionEvaluatorService GetEvaluatorService(IComponent component)
		{
			IExpressionEvaluatorService evaluatorService = null;
			if (component != null && component.Site != null)
			{
				evaluatorService = component.Site.GetService(typeof(IExpressionEvaluatorService)) as IExpressionEvaluatorService;
			}
			Debug.Assert(evaluatorService != null, typeof(IExpressionEvaluatorService).Name + " is unavailable.");
			return evaluatorService;
		}

		/// <summary>
		/// Factory method to create an instance of <see cref="EvaluatorService"/>.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public static EvaluatorService Create(IComponent component)
		{
			if (component == null)
				throw new ArgumentNullException("component");
			return new EvaluatorService(GetEvaluatorService(component));
		}

		/// <summary>
		/// Represents empty expression resolved from ExpressionInfo.EmptyString.
		/// </summary>
		public static readonly ExpressionInfo EmptyExpression = ExpressionInfo.FromString(null);

		/// <summary>
		/// Returns true if passed expression is empty.
		/// </summary>
		/// <param name="expr">the expression to compare</param>
		public static bool IsEmptyExpression(ExpressionInfo expr)
		{
			return EmptyExpression.Equals(expr);
		}

		/// <summary>
		/// Gets the <see cref="IExpressionEvaluatorService"/> to use in evaluations.
		/// </summary>
		private IExpressionEvaluatorService Evaluator
		{
			get { return _evaluator; }
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		/// <remarks>
		/// Keep in mind that many dynamic expressions such as those accessing Fields collection or other global collections will not be evaluated at design time in which case null may be returned.
		/// </remarks>
		public object EvaluateExpression(ExpressionInfo expression)
		{
			if (Evaluator == null) return null;
			return Evaluator.Evaluate(expression);
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a <see cref="String"/>.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		public string EvaluateStringExpression(ExpressionInfo expression)
		{
			return Convert.ToString(EvaluateExpression(expression), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a <see cref="Int32"/>.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		public int EvaluateInt32Expression(ExpressionInfo expression)
		{
			return Convert.ToInt32(EvaluateStringExpression(expression), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a <see cref="Double"/>.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		public double EvaluateDoubleExpression(ExpressionInfo expression)
		{
			string stringValue = EvaluateStringExpression(expression);
			if (stringValue.Length > 0)
				return Convert.ToDouble(EvaluateStringExpression(expression), CultureInfo.InvariantCulture);
			else
				return 0; // returns in the case of empty string
		}

		/// <summary>
		/// Evaluates the specified expression if possible and returns its value as a <see cref="Boolean"/>.
		/// </summary>
		/// <param name="expression">The expression to evaluate.</param>
		public bool EvaluateBooleanExpression(ExpressionInfo expression)
		{
			return Convert.ToBoolean(EvaluateStringExpression(expression), CultureInfo.InvariantCulture);
		}
	}
}
