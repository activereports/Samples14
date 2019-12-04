using GrapeCity.ActiveReports.Calendar.Design;

namespace GrapeCity.ActiveReports.Calendar.Validation
{
	/// <summary>
	/// Represents the interface for validatable properties.
	/// </summary>
	internal interface IValidatable
	{
		/// <summary>
		/// Performs validation using the specified evaluator.
		/// </summary>
		void Validate(EvaluatorService evaluator);
	}
}
