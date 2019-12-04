using System;
using System.Collections.Generic;
using System.Globalization;

namespace GrapeCity.ActiveReports.Samples.OracleDataProvider
{
	/// <summary>
	/// Provides various extension methods for data interfaces.
	/// </summary>
	internal static class DataExtensions
	{
		/// <summary>
		/// Reads all records in specified data reader.
		/// </summary>
		/// <param name="reader">The data reader to iterate records for.</param>
		/// <returns></returns>
		public static IEnumerable<System.Data.IDataRecord> ReadAll(this System.Data.IDataReader reader)
		{
			while (reader.Read())
			{
				yield return reader;
			}
		}

		/// <summary>
		/// Gets field value as string in specified record.
		/// </summary>
		/// <param name="record">The record to get value field in.</param>
		/// <param name="name">The field name to get value for.</param>
		/// <returns>The field value.</returns>
		public static string GetString(this System.Data.IDataRecord record, string name)
		{
			var value = record.GetValue(record.GetOrdinal(name));
			return Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Gets field value as integer in specified record.
		/// </summary>
		/// <param name="record">The record to get value field in.</param>
		/// <param name="name">The field name to get value for.</param>
		/// <returns>The field value.</returns>
		public static int GetInt32(this System.Data.IDataRecord record, string name)
		{
			var value = record.GetValue(record.GetOrdinal(name));
			return Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Gets field value as boolean in specified record.
		/// </summary>
		/// <param name="record">The record to get value field in.</param>
		/// <param name="name">The field name to get value for.</param>
		/// <returns>The field value.</returns>
		public static bool GetBoolean(this System.Data.IDataRecord record, string name)
		{
			var value = record.GetValue(record.GetOrdinal(name));
			return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Return whether the specified field is set to null.
		/// </summary>
		/// <param name="record">The record to get value field in</param>
		/// <param name="name">The field name to get value for.</param>
		/// <returns>true if the specified field is set to null; otherwise, false.</returns>
		public static bool IsDBNull(this System.Data.IDataRecord record, string name)
		{
			var value = record.GetValue(record.GetOrdinal(name));
			return value == DBNull.Value;
		}
	}
}
