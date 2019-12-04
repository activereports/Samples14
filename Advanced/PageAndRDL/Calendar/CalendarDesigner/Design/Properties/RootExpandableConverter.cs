using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace GrapeCity.ActiveReports.Calendar.Design.Properties
{
	/// <summary>
	/// Represents the root converter for design time compound types.
	/// </summary>
	/// <remarks>The converter is based on BaseExpandableConverter from design assembly, but it has differences.</remarks>
	////[DoNotObfuscateType]
	internal class RootExpandableConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Gets the individual values from the value list string provided to the converter.
		/// </summary>
		/// <param name="valuesList">The string of values to split apart.</param>
		/// <param name="culture">The <see cref="CultureInfo"/>used to determine what the list seperator is. If the value is null the Current UI Culture will be used</param>
		/// <returns>a string array containing the values split from the original string.</returns>
		internal static string[] GetValuesFromList(string valuesList, CultureInfo culture)
		{
			#region regexSplitMembers definition
			/// <summary>
			///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
			///  
			///  A description of the regular expression:
			///  
			///  Match a prefix but exclude it from the capture. [(,|^)]
			///      [1]: A numbered capture group. [,|^]
			///          Select from 2 alternatives
			///              ,
			///              Beginning of line or string
			///  Greedy subexpression. [[^(),]+ | \( (?> [^()]+ | \( (?<pnths>) | \) (?<-pnths>) )* (?(pnths)(?!)) \)], any number of repetitions
			///      Select from 2 alternatives
			///          Any character that is not in this class: [(),], one or more repetitions
			///           \( (?> [^()]+ | \( (?<pnths>) | \) (?<-pnths>) )* (?(pnths)(?!)) \)
			///              (
			///              Greedy subexpression. [ [^()]+ | \( (?<pnths>) | \) (?<-pnths>) ], any number of repetitions
			///                  Select from 3 alternatives
			///                      Any character that is not in this class: [()], one or more repetitions
			///                       \( (?<pnths>) 
			///                          (
			///                          [pnths]: A named capture group. []
			///                              Empty
			///                       \) (?<-pnths>) 
			///                          )
			///                          Balancing group. Remove the most recent [pnths] capture from the stack. []
			///                              Empty
			///              Conditional expression
			///                  Did the capture named [pnths] match?
			///                  If yes, search for [(?!)]
			///                      Match if prefix is absent. []
			///                          Empty
			///              )
			///
			/// </summary>
			#endregion

			if (culture == null)
				culture = CultureInfo.CurrentUICulture;

			string regExPattern = string.Format(@"(?<=({0}|^))(?>[^(){0}]+ |\((?> [^()]+ | \( (?<pnths>) | \) (?<-pnths>) )* (?(pnths)(?!))\))*", culture.TextInfo.ListSeparator[0]);

			Regex regexSplitMembers = new Regex(regExPattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
			string[] values;

			if (valuesList != null && valuesList != string.Empty)
			{
				MatchCollection matches = regexSplitMembers.Matches(valuesList);

				values = new string[matches.Count];
				for (int i = 0; i < matches.Count; i++)
				{
					values[i] = matches[i].Value.Trim();
				}
			}
			else
			{
				values = new string[] { };
			}
			return values;
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
