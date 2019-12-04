using System;
using System.Diagnostics;
using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using IList = System.Collections.IList;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Represens Drillthrough action
	/// </summary>
	public class Drillthrough : IDrillthrough
	{
		private readonly string _reportName;
		private readonly DrillthroughParameter[] _parameters;

		private Drillthrough(string reportName, IList parameters)
		{
			if (reportName == null)
			{
				Trace.TraceWarning("Drillthrough report name is null");
				_reportName = string.Empty;
			}

			if (parameters == null)
			{
				_parameters = new DrillthroughParameter[0];
			}
			else
			{
				int n = parameters.Count;
				_parameters = new DrillthroughParameter[n];
				for (int i = 0; i < n; i++)
				{
					_parameters[i] = (DrillthroughParameter)parameters[i];
				}
			}

			_reportName = reportName;
		}

		/// <summary>
		/// Creates drillthrough object
		/// </summary>
		/// <param name="reportName">report name</param>
		/// <param name="parameters">collection of <see cref="DrillthroughParameter"/></param>
		/// <returns></returns>
		public static IDrillthrough Create(string reportName, IList parameters)
		{
			return new Drillthrough(reportName, parameters);
		}

		#region IDrillthrough Members

		public DrillthroughParameter[] Parameters
		{
			get { return _parameters; }
		}

		public int NumberOfParameters
		{
			get { return _parameters.Length; }
		}

		public string ReportName
		{
			get { return _reportName; }
		}

		/// <summary>
		/// Gets the PageReport associated with the drill through report.
		/// </summary>
		/// <remarks>This is an object type and must be cast to ReportDefintion.</remarks>
		public IDisposable CreatePageDocument() { return null; }

		#endregion
	}


}
