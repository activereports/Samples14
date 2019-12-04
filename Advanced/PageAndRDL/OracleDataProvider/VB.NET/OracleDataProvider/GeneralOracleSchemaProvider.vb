Imports System.Data.Common
Imports System.Text.RegularExpressions
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Extensibility.Data
Imports GrapeCity.ActiveReports.Extensibility.Data.SchemaModel

Namespace GrapeCity.ActiveReports.Samples.OracleDataProvider
	Public Class GeneralOracleSchemaProvider
		Implements IDbSchemaProvider
		Public Function GetSchema(ByVal connection As DbConnection) As DataSchema Implements IDbSchemaProvider.GetSchema
			Dim result As DataSchema = Nothing
			Dim command = connection.CreateCommand()
			command.CommandText = TableInfo.Query
			Dim keycommand = connection.CreateCommand()
			keycommand.CommandText = FKInfo.Query

			Try

				Using reader = command.ExecuteReader()

					Using keyreader = keycommand.ExecuteReader()
						Dim rows = reader.ReadAll().Select(Function(record) New With
									{Key .TableType = CType([Enum].Parse(GetType(TableType), record.GetString(TableInfo.Type), True), TableType),
									 .Schema = record.GetString(TableInfo.Owner),
									 .Name = record.GetString(TableInfo.Name),
									 .Column = record.GetString(TableInfo.ColumnName),
									 .IsPrimary = (record.GetString(TableInfo.ConstraintType) = "P"),
									 .DataType = MapDataType(record.GetString(TableInfo.DataType))}).ToList()

						Dim tables = rows.GroupBy(Function(i) i.Schema + i.Name, Function(key, g) New Table(New DbName(g.First().Name, g.First().Schema), g.First().TableType, g.Select(Function(gg) New Column(gg.Column, gg.DataType, gg.IsPrimary)).ToList)).ToList()

						Dim keyrows = keyreader.ReadAll().Select(Function(record) New With
										{Key .FirstTableName = New DbName(record.GetString(FKInfo.FirstTable), record.GetString(FKInfo.FirstOwner)),
										.FirstColumn = record.GetString(FKInfo.FirstColumn),
										.SecondTableName = New DbName(record.GetString(FKInfo.SecondTable), record.GetString(FKInfo.SecondOwner)),
										.SecondColumn = record.GetString(FKInfo.SecondColumn)
										})
						Dim foreignKeys = keyrows.Select(Function(i)
										Return New ForeignKey(tables.First(Function(t) t.Name.Equals(i.FirstTableName)),
										tables.First(Function(t) t.Name.Equals(i.SecondTableName)),
										 New Extensibility.Data.SchemaModel.Column() {tables.First(Function(t) t.Name.Equals(i.FirstTableName)).Columns.First(Function(c) c.Name = i.FirstColumn)}, New Extensibility.Data.SchemaModel.Column() {tables.First(Function(t) t.Name.Equals(i.SecondTableName)).Columns.First(Function(c) c.Name = i.SecondColumn)})

														 End Function)
						result = New DataSchema(tables, foreignKeys)
					End Using
				End Using

			Finally
				connection.Close()
			End Try

			Return result
		End Function

		Private Shared Function MapDataType(ByVal dataType As String) As Type
			If String.IsNullOrEmpty(dataType) Then
				Return GetType(Object)
			End If

			dataType = Regex.Replace(dataType, "\((\d+)\)", String.Empty)
			Dim value As Type = Nothing
			Return If(DataTypeMap.TryGetValue(dataType, value), value, GetType(Object))
		End Function

		Private Shared ReadOnly DataTypeMap As Dictionary(Of String, Type) = New Dictionary(Of String, Type)(StringComparer.OrdinalIgnoreCase) From {
			{"bfile", GetType(Byte())},
			{"blob", GetType(Byte())},
			{"char", GetType(String)},
			{"clob", GetType(String)},
			{"date", GetType(DateTime)},
			{"float", GetType(Decimal)},
			{"integer", GetType(Decimal)},
			{"interval year to month", GetType(Int32)},
			{"interval day to second", GetType(TimeSpan)},
			{"long", GetType(String)},
			{"long raw", GetType(Byte())},
			{"nchar", GetType(String)},
			{"nclob", GetType(String)},
			{"number", GetType(Decimal)},
			{"nvarchar2", GetType(String)},
			{"raw", GetType(Byte())},
			{"rowid", GetType(String)},
			{"urowid", GetType(String)},
			{"timestamp", GetType(DateTime)},
			{"timestamp with local time zone", GetType(DateTime)},
			{"timestamp with time zone", GetType(DateTime)},
			{"unsigned integer", GetType(Decimal)},
			{"varchar2", GetType(String)},
			{"varchar", GetType(String)},
			{"binary_double", GetType(Double)},
			{"binary_float", GetType(Single)},
			{"nvarchar", GetType(String)}
		}

		Private Class TableInfo
			Public Const Query As String = "SELECT ao.owner, uo.object_type, uo.object_name, atc.column_name,atc.data_type,cols.constraint_type
				FROM user_objects uo
				join all_objects ao ON uo.object_id = ao.object_id
				left join ALL_TAB_COLUMNS atc on atc.owner = ao.owner and atc.table_name = uo.object_name
				left join
				(select acc.table_name,acc.column_name,acc.constraint_name,cons.constraint_type from all_cons_columns acc
				join all_constraints cons on cons.constraint_name = acc.constraint_name and cons.constraint_type = 'P')
				cols on cols.COLUMN_NAME = atc.column_name and cols.TABLE_NAME = atc.table_name
				WHERE uo.subobject_name is NULL
				AND uo.temporary = 'N' AND uo.generated = 'N'
				AND uo.secondary = 'N' AND (uo.OBJECT_TYPE = 'TABLE' OR uo.OBJECT_TYPE = 'VIEW')"
			Public Const Owner As String = "OWNER"
			Public Const Name As String = "OBJECT_NAME"
			Public Const Type As String = "OBJECT_TYPE"
			Public Const ColumnName As String = "COLUMN_NAME"
			Public Const DataType As String = "DATA_TYPE"
			Public Const ConstraintType As String = "CONSTRAINT_TYPE"
		End Class

		Private Class FKInfo
			Public Const Query As String = "SELECT  UC.OWNER as FIRST_OWNER,
				UC.TABLE_NAME as FIRST_TABLE,
				UCC2.COLUMN_NAME as FIRST_COLUMN,
				UCC.OWNER as SECOND_OWNER,
				UCC.TABLE_NAME as SECOND_TABLE,
				UCC.COLUMN_NAME as SECOND_COLUMN
				FROM (SELECT OWNER, TABLE_NAME, CONSTRAINT_NAME, R_CONSTRAINT_NAME, CONSTRAINT_TYPE FROM USER_CONSTRAINTS) UC,
				(SELECT OWNER, TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME FROM USER_CONS_COLUMNS) UCC,
				(SELECT TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME FROM USER_CONS_COLUMNS) UCC2
				WHERE UC.R_CONSTRAINT_NAME = UCC.CONSTRAINT_NAME
				AND UC.CONSTRAINT_NAME = UCC2.CONSTRAINT_NAME
				AND uc.constraint_type = 'R'"
			Public Const FirstOwner As String = "FIRST_OWNER"
			Public Const FirstTable As String = "FIRST_TABLE"
			Public Const FirstColumn As String = "FIRST_COLUMN"
			Public Const SecondOwner As String = "SECOND_OWNER"
			Public Const SecondTable As String = "SECOND_TABLE"
			Public Const SecondColumn As String = "SECOND_COLUMN"
		End Class
	End Class
End Namespace
