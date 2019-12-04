using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace GrapeCity.ActiveReports.Samples.CustomDataProvider.CsvDataProvider
{
	/// <summary>
	/// Provides the implementation of <see cref="Extensibility.Data.IDataReader"/> for the .NET Framework CSV Data Provider.
	/// </summary>
	internal class CsvDataReader : DbDataReader
	{
		private Hashtable _typeLookup = new Hashtable(new myCultureComparer(new CultureInfo(Properties.Resources.CultureString)));

		private Hashtable _columnLookup = new Hashtable();
		private object[] _columns;
		private TextReader _textReader;
		private object[] _currentRow;

		//The regular expressions are set to be pre-compiled to make it faster. 

		//Properties are read-only to avoid multi-threading so no one can change any properties on these objects.

		private static readonly Regex _rxDataRow = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))", RegexOptions.Compiled);
		//Used to parse the data rows.

		private static readonly Regex _rxHeaderRow =
			new Regex(@"(?<fieldName>(\w*\s*)*)\((?<fieldType>\w*)\)", RegexOptions.Compiled); //Used to parse the header rows.

		/// <summary>
		/// </summary>
		/// <param name="textReader"><see cref="TextReader"/> to use to read the data.</param>
		public CsvDataReader(TextReader textReader)
		{
			_textReader = textReader;
			ParseCommandText();
		}

		/// <summary>
		/// Parses the passed in command text.
		/// </summary>
		private void ParseCommandText()
		{
			if (_textReader.Peek() == -1)
				return; //Command text is empty or at the end already.

			FillTypeLookup();
			try
			{
				if (!ParseHeader(_textReader.ReadLine()))
					throw new InvalidOperationException(Properties.Resources.ParseTextException);

			}
			catch (Exception)
			{
			}
		}

		//Hashtable is used to return a type for the string value used in the header text.

		private void FillTypeLookup()
		{
			_typeLookup.Add("string", typeof(String));
			_typeLookup.Add("byte", typeof(Byte));
			_typeLookup.Add("boolean", typeof(Boolean));
			_typeLookup.Add("datetime", typeof(DateTime));
			_typeLookup.Add("decimal", typeof(Decimal));
			_typeLookup.Add("double", typeof(Double));
			_typeLookup.Add("int16", typeof(Int16));
			_typeLookup.Add("int32", typeof(Int32));
			_typeLookup.Add("int", typeof(Int32));
			_typeLookup.Add("integer", typeof(Int32));
			_typeLookup.Add("int64", typeof(Int64));
			_typeLookup.Add("sbyte", typeof(SByte));
			_typeLookup.Add("single", typeof(Single));
			_typeLookup.Add("time", typeof(DateTime));
			_typeLookup.Add("date", typeof(DateTime));
			_typeLookup.Add("uint16", typeof(UInt16));
			_typeLookup.Add("uint32", typeof(UInt32));
			_typeLookup.Add("uint64", typeof(UInt64));
		}

		/// <summary>
		/// Returns a type based on the string value passed in from the header text string. If not match is found a string type is returned.
		/// </summary>
		/// <param name="fieldType">String value from the header command text string.</param>
		private Type GetFieldTypeFromString(string fieldType)
		{
			if (_typeLookup.Contains(fieldType))
				return _typeLookup[fieldType] as Type;
			return typeof(String);
		}

		/// <summary>
		/// Parses the first line in the passed in command text string to create the field names and field data types. The field information
		/// is stored in a <see cref="CsvColumn"/> struct, and these column info items are stored in an ArrayList. The column name is also added
		/// to a hashtable for easy lookup later.
		/// </summary>
		/// <param name="header">Header string that contains all the fields.</param>
		/// <returns>True if it can parse the header string, else false.</returns>
		private bool ParseHeader(string header)
		{
			string fieldName;
			int index = 0;
			if (header.IndexOf("(") == -1)
			{
				return false;
			}

			MatchCollection matches = _rxHeaderRow.Matches(header);
			_columns = new object[matches.Count];
			foreach (Match match in matches)
			{
				fieldName = match.Groups["fieldName"].Value;
				Type fieldType = GetFieldTypeFromString(match.Groups["fieldType"].Value);
				_columns.SetValue(new CsvColumn(fieldName, fieldType), index);
				_columnLookup.Add(fieldName, index);
				index++;
			}
			return true;
		}

		/// <summary>
		/// Parses a row of data using a regular expression and stores the information inside an object array that is the current row of data.
		/// If the row does not have the correct number of fields, an exception is raised.
		/// </summary>
		/// <param name="dataRow">String value representing a comma delimited data row.</param>
		/// <returns>True if it can parse the data string, else false.</returns>
		private bool ParseDataRow(string dataRow)
		{
			int index = 0;
			string[] tempData = _rxDataRow.Split(dataRow);

			_currentRow = new object[tempData.Length];
			if (tempData.Length != _columns.Length)
			{
				string error = string.Format(CultureInfo.InvariantCulture, Properties.Resources.ParseDataRowError, dataRow);
				
				throw new InvalidOperationException(error);
			}
			for (int i = 0; i < tempData.Length; i++)
			{
				string value = tempData[i];

				if (value.Length > 1)
				{
					if (value.IndexOf('"', 0) == 0 && value.IndexOf('"', 1) == value.Length - 1)
						value = value.Substring(1, value.Length - 2);
				}
				_currentRow.SetValue(ConvertValue(GetFieldType(index), value), index);
				index++;
			}
			return true;
		}

		/// <summary>
		/// Converts the string value coming from the command text to the appropriate data type, based on the field's type.
		/// This also checks a few string value rules to decide if a String.Empty or System.Data.DBNull needs to be returned.
		/// </summary>
		/// <param name="type">Type of the current column the data belongs to.</param>
		/// <param name="originalValue">String value coming from the command text.</param>
		/// <returns>Resulting object from the converted string, based on the type.</returns>
		private object ConvertValue(Type type, string originalValue)
		{
			Type fieldType = type;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			try
			{
				if (originalValue == "\"\"" || originalValue == " ")
					return string.Empty;
				if (originalValue == "")
					return DBNull.Value;
				if (originalValue == "DBNull")
					return DBNull.Value;
				if (fieldType == typeof(String))
					return originalValue.Trim();
				if (fieldType == typeof(Int32))
					return Convert.ToInt32(originalValue, invariantCulture);
				if (fieldType == typeof(Boolean))
					return Convert.ToBoolean(originalValue, invariantCulture);
				if (fieldType == typeof(DateTime))
					return Convert.ToDateTime(originalValue, invariantCulture);
				if (fieldType == typeof(Decimal))
					return Convert.ToDecimal(originalValue, invariantCulture);
				if (fieldType == typeof(Double))
					return Convert.ToDouble(originalValue, invariantCulture);
				if (fieldType == typeof(Int16))
					return Convert.ToInt16(originalValue, invariantCulture);
				if (fieldType == typeof(Int64))
					return Convert.ToInt64(originalValue, invariantCulture);
				if (fieldType == typeof(Single))
					return Convert.ToSingle(originalValue, invariantCulture);
				if (fieldType == typeof(Byte))
					return Convert.ToByte(originalValue, invariantCulture);
				if (fieldType == typeof(SByte))
					return Convert.ToSByte(originalValue, invariantCulture);
				if (fieldType == typeof(UInt16))
					return Convert.ToUInt16(originalValue, invariantCulture);
				if (fieldType == typeof(UInt32))
					return Convert.ToUInt32(originalValue, invariantCulture);
				if (fieldType == typeof(UInt64))
					return Convert.ToUInt64(originalValue, invariantCulture);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException(string.Format(Properties.Resources.ConvertValueExceprion, originalValue, type), e);
			}
			//If not match is found return DBNull instead.

			return DBNull.Value;
		}


		/// <summary>
		/// Advances the <see cref="CsvDataReader"/> to the next record.
		/// </summary>
		/// <returns>True if there are more rows, otherwise false.</returns>
		public override bool Read()
		{
			if (_textReader.Peek() > -1)
				ParseDataRow(_textReader.ReadLine());
			else
				return false;

			return true;
		}

		/// <summary>
		/// Releases the resources used by the <see cref="CsvDataReader"/>.
		/// </summary>
		public new void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_textReader != null)
					_textReader.Close();
			}

			_typeLookup = null;
			_columnLookup = null;
			_columns = null;
			_currentRow = null;
		}

		/// <summary>
		/// Allows an Object to attempt to free resources and perform other cleanup operations before the Object is reclaimed by garbage collection.
		/// </summary>
		~CsvDataReader()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets the number of columns in the current row.
		/// </summary>
		public override int FieldCount
		{
			get {
				if (_columns == null)
					_columns = new object[0];
				return _columns.Length;
			}
		}

		public override int Depth
		{
			get { return 0; }
		}

		public override bool IsClosed
		{
			get { return false; }
		}

		public override int RecordsAffected
		{
			get { return 0; }
		}

		public override bool HasRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override object this[string name]
		{
			get { return this[GetOrdinal(name).ToString()]; }
		}

		public override object this[int i]
		{
			get { return GetValue(i); }
		}

		/// <summary>
		/// Return the value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find. </param>
		/// <returns>The <see cref="Object"/> which will contain the field value upon return.</returns>
		public override Type GetFieldType(int ordinal)
		{
			if (ordinal > _columns.Length - 1)
				return null;

			return ((CsvColumn) _columns.GetValue(ordinal)).DataType;
		}
		
		/// <summary>
		/// Return the value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find. </param>	   
		/// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
		public override string GetName(int i)
		{
			if (i > _columns.Length - 1)
				return string.Empty;

			return ((CsvColumn) _columns.GetValue(i)).FieldName;
		}

		public override int GetOrdinal(string name)
		{
			object value = _columnLookup[name];
			if (value == null)
				throw new IndexOutOfRangeException("name");
			return (int) value;
		}

		/// <summary>
		/// Return the value of the specified field.
		/// </summary>
		/// <param name="i">The index of the field to find. </param>
		/// <returns>The <see cref="Object"/> which will contain the field value upon return.</returns>
		public override object GetValue(int i)
		{
			if (i > _columns.Length - 1)
				return null;

			return _currentRow.GetValue(i);
		}

		public override void Close()
		{}

		public override System.Data.DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public override bool NextResult()
		{
			throw new NotImplementedException();
		}

		public override string GetDataTypeName(int i)
		{
			return GetFieldType(i).ToString();
		}

		public override int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		public override  bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		public override byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public override char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public override Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		public override short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		public override int GetInt32(int i)
		{
			throw new NotImplementedException();
		}

		public override long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		public override float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		public override double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		public override string GetString(int i)
		{
			throw new NotImplementedException();
		}

		public override decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public override DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		public override bool IsDBNull(int i)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#region EqualityComparer

		class myCultureComparer : IEqualityComparer
		{
			public CaseInsensitiveComparer myComparer;

			public myCultureComparer()
			{
				myComparer = CaseInsensitiveComparer.DefaultInvariant;
			}

			public myCultureComparer(CultureInfo myCulture)
			{
				myComparer = new CaseInsensitiveComparer(myCulture);
			}

			public new bool Equals(object x, object y)
			{
				if (myComparer.Compare(x, y) == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public int GetHashCode(object obj)
			{
				return obj.ToString().ToLower().GetHashCode();
			}
		}

		#endregion 
	}
}
