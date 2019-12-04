' <summary>
' Represents the information about the field of the data source.
' </summary>
Structure CsvColumn
	Private ReadOnly _fieldName As String
	Private ReadOnly _dataType As Type

   ' <summary>
	' Creates new instance of <see cref="CsvColumn"/> class.
	' </summary>
	' <param name="fieldName">The <see cref="CsvColumn"/> name of the field represented by this instance.</param>
	' <param name="dataType">The<see cref="Type"/>of the field represented by this instance.</param>
	Public Sub New(ByVal fieldName As String, ByVal dataType As Type)
		If (fieldName Is Nothing) Then
			Throw New ArgumentNullException("fieldName")
		End If
		If (dataType Is Nothing) Then
			Throw New ArgumentNullException("dataType")
		End If
		_fieldName = fieldName
		_dataType = dataType
	End Sub

   ' <summary>
	' Gets the name of the field represented by this instance of <see cref="CsvColumn"/>.
	' </summary>
	Public ReadOnly Property FieldName() As String
		Get
			Return _fieldName
		End Get
	End Property

	' <summary>
	' Gets the <see cref="Type"/> of the field represented by this instance.
	' </summary>
	Public ReadOnly Property DataType() As Type
		Get
			Return _dataType
		End Get
	End Property


	' <summary>
	' Returns this instance of <see cref="CsvColumn"/> converted to a string.
	' </summary>
	'  <returns>String that represents this instance of <see cref="CsvColumn"/>.</returns>
	Public Overrides Function ToString() As String
		Return String.Concat(New String() {FieldName, "(", DataType.ToString(), ")"})
	End Function

	' <summary>
	' Determines whether two <see cref="CsvColumn"/> instances are equal.
	' </summary>
	'  <param name="obj">The <see cref="CsvColumn"/> to compare with the current <see cref="CsvColumn"/>.</param>
	' <returns>True if the specified <see cref="CsvColumn"/> is equal to the current <see cref="CsvColumn"/>, otherwise, false.</returns>
	Public Overrides Function Equals(ByVal obj As Object) As Boolean
		Dim flag As Boolean

		If (TypeOf (obj) Is CsvColumn) Then
			flag = Equals(CType(obj, CsvColumn))
		Else
			flag = False

		End If
		Return flag
	End Function

	Private Overloads Function Equals(ByVal column As CsvColumn) As Boolean
		Return (column.FieldName = FieldName)
	End Function

	' <summary>
	' Serves as a hash function for a <see cref="CsvColumn"/>, suitable for use in hashing algorithms and data structures like a hash table.
	' </summary>
	' <returns>A hash code for the current <see cref="CsvColumn"/> instance.</returns>
	Public Overrides Function GetHashCode() As Integer
		Return (FieldName.GetHashCode() + DataType.GetHashCode())
	End Function

End Structure
