using System;

namespace GrapeCity.ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// Represents the information about the field of the data source.
	/// </summary>
	internal struct CsvColumn
	{
		private readonly string _fieldName;
		private readonly Type _dataType;

		/// <summary>
		/// Creates new instance of <see cref="CsvColumn"/> class.
		/// </summary>
		/// <param name="fieldName">The <see cref="CsvColumn"/> name of the field represented by this instance.</param>
		/// <param name="dataType">The<see cref="Type"/>of the field represented by this instance.</param>
		public CsvColumn(string fieldName, Type dataType)
		{
			if (fieldName == null)
				throw new ArgumentNullException("fieldName");
			if (dataType == null)
				throw new ArgumentNullException("dataType");
			_fieldName = fieldName;
			_dataType = dataType;
		}

		/// <summary>
		/// Gets the name of the field represented by this instance.
		/// </summary>
		public string FieldName
		{
			get { return _fieldName; }
		}

		/// <summary>
		/// Gets the <see cref="Type"/> of the field represented by this instance.
		/// </summary>
		public Type DataType
		{
			get { return _dataType; }
		}

		/// <summary>
		/// Returns this instance of <see cref="CsvColumn"/> converted to a string.
		/// </summary>
		///  <returns>String that represents this instance of <see cref="CsvColumn"/>.</returns>
		public override string ToString()
		{
			return String.Concat(new string[] {FieldName, "(", DataType.ToString(), ")"});
		}

		/// <summary>
		/// Determines whether two <see cref="CsvColumn"/> instances are equal.
		/// </summary>
		///  <param name="obj">The <see cref="CsvColumn"/> to compare with the current <see cref="CsvColumn"/>.</param>
		/// <returns>True if the specified <see cref="CsvColumn"/> is equal to the current <see cref="CsvColumn"/>, otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			bool flag;

			if (obj is CsvColumn)
			{
				flag = Equals((CsvColumn) obj);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private bool Equals(CsvColumn column)
		{
			return column.FieldName == FieldName;
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="CsvColumn"/>, suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>A hash code for the current <see cref="CsvColumn"/> instance.</returns>
		public override int GetHashCode()
		{
			return (FieldName.GetHashCode() + DataType.GetHashCode());
		}
	}
}
