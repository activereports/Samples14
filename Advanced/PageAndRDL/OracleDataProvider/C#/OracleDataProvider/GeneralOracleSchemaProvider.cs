using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using GrapeCity.ActiveReports.Extensibility.Data;
using GrapeCity.ActiveReports.Extensibility.Data.SchemaModel;

namespace GrapeCity.ActiveReports.Samples.OracleDataProvider
{
	public class GeneralOracleSchemaProvider : IDbSchemaProvider
	{
		public DataSchema GetSchema(DbConnection connection)
		{
			DataSchema result = null;

			var command = connection.CreateCommand();
			command.CommandText = TableInfo.Query;

			var keycommand = connection.CreateCommand();
			keycommand.CommandText = FKInfo.Query;

			try
			{
				using (var reader = command.ExecuteReader())
				using (var keyreader = keycommand.ExecuteReader())
				{
					var rows = reader.ReadAll().Select(record => new
					{
						TableType = (TableType)Enum.Parse(typeof(TableType), record.GetString(TableInfo.Type), true),
						Schema = record.GetString(TableInfo.Owner),
						Name = record.GetString(TableInfo.Name),
						Column = record.GetString(TableInfo.ColumnName),
						IsPrimary = (record.GetString(TableInfo.ConstraintType) == "P"),
						DataType = MapDataType(record.GetString(TableInfo.DataType))
					}
					).ToList();

					var tables =
						rows.GroupBy(i => i.Schema + i.Name,
								(key, g) => new
									Table(new DbName(g.First().Name, g.First().Schema),
										g.First().TableType,
										g.Select(gg => new Extensibility.Data.SchemaModel.Column(gg.Column, gg.DataType, gg.IsPrimary)).ToList()))
							.ToList();

					var keyrows = keyreader.ReadAll().Select(record => new
					{
						FirstTableName = new DbName(record.GetString(FKInfo.FirstTable), record.GetString(FKInfo.FirstOwner)),
						FirstColumn = record.GetString(FKInfo.FirstColumn),
						SecondTableName = new DbName(record.GetString(FKInfo.SecondTable), record.GetString(FKInfo.SecondOwner)),
						SecondColumn = record.GetString(FKInfo.SecondColumn),
					}
					);

					var foreignKeys = keyrows.Select(i => new ForeignKey(
						tables.First(t => t.Name.Equals(i.FirstTableName)),
						tables.First(t => t.Name.Equals(i.SecondTableName)),
						new Extensibility.Data.SchemaModel.Column[] { tables.First(t => t.Name.Equals(i.FirstTableName)).Columns.First(c => c.Name == i.FirstColumn) },
						new Extensibility.Data.SchemaModel.Column[] { tables.First(t => t.Name.Equals(i.SecondTableName)).Columns.First(c => c.Name == i.SecondColumn) }
					));

					result = new DataSchema(tables, foreignKeys);
				}
			}
			finally
			{
				connection.Close();
			}

			return result;
		}

		private static Type MapDataType(string dataType)
		{
			if (string.IsNullOrEmpty(dataType))
			{
				return typeof(object);
			}

			//we need to remove numbers inside brackets to avoid cases like timestamp(N) or something else
			dataType = Regex.Replace(dataType, @"\((\d+)\)", string.Empty);

			Type value;
			return DataTypeMap.TryGetValue(dataType, out value) ? value : typeof(object);
		}

		//http://msdn.microsoft.com/en-us/library/yk72thhd.aspx - data type mappings
		private static readonly Dictionary<string, Type> DataTypeMap =
			new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
			{
				{"bfile", typeof(Byte[])},
				{"blob", typeof(Byte[])},
				{"char", typeof(String)},
				{"clob", typeof(String)},
				{"date", typeof(DateTime)},
				{"float", typeof(Decimal)},
				{"integer", typeof(Decimal)},
				{"interval year to month", typeof(Int32)},
				{"interval day to second", typeof(TimeSpan)},
				{"long", typeof(String)},
				{"long raw", typeof(Byte[])},
				{"nchar", typeof(String)},
				{"nclob", typeof(String)},
				//http://stackoverflow.com/questions/5502016/which-net-data-type-is-best-for-mapping-the-number-oracle-data-type-in-nhiberna
				{"number", typeof(Decimal)},
				{"nvarchar2", typeof(String)},
				{"raw", typeof(Byte[])},
				{"rowid", typeof(String)},
				{"urowid", typeof(String)},
				{"timestamp", typeof(DateTime)},
				{"timestamp with local time zone", typeof(DateTime)},
				{"timestamp with time zone", typeof(DateTime)},
				{"unsigned integer", typeof(Decimal)},
				{"varchar2", typeof(String)},
				{"varchar", typeof(String)},
				{"binary_double", typeof(Double)},
				{"binary_float", typeof(Single)},
				{"nvarchar", typeof(String)}
			};

		private static class TableInfo
		{
			/*
			user_objects - http://docs.oracle.com/cd/B14117_01/server.101/b10755/statviews_2578.htm
			all_objects - http://docs.oracle.com/cd/B14117_01/server.101/b10755/statviews_1102.htm
			ALL_TAB_COLUMNS - http://docs.oracle.com/cd/B19306_01/server.102/b14237/statviews_2094.htm
			all_constraints - http://docs.oracle.com/cd/B19306_01/server.102/b14237/statviews_1037.htm
			*/
			public const string Query =
				@"SELECT ao.owner, uo.object_type, uo.object_name, atc.column_name,atc.data_type,cols.constraint_type
				FROM user_objects uo
				join all_objects ao ON uo.object_id = ao.object_id
				left join ALL_TAB_COLUMNS atc on atc.owner = ao.owner and atc.table_name = uo.object_name
				left join
				(select acc.table_name,acc.column_name,acc.constraint_name,cons.constraint_type from all_cons_columns acc
				join all_constraints cons on cons.constraint_name = acc.constraint_name and cons.constraint_type = 'P')
				cols on cols.COLUMN_NAME = atc.column_name and cols.TABLE_NAME = atc.table_name
				WHERE uo.subobject_name is NULL
				AND uo.temporary = 'N' AND uo.generated = 'N'
				AND uo.secondary = 'N' AND (uo.OBJECT_TYPE = 'TABLE' OR uo.OBJECT_TYPE = 'VIEW')";

			public const string Owner = "OWNER";
			public const string Name = "OBJECT_NAME";
			public const string Type = "OBJECT_TYPE";
			public const string ColumnName = "COLUMN_NAME";
			public const string DataType = "DATA_TYPE";
			public const string ConstraintType = "CONSTRAINT_TYPE";
		}

		private static class FKInfo
		{
			/*
			 USER_CONSTRAINTS - http://docs.oracle.com/cd/B13789_01/server.101/b10755/statviews_2510.htm
			 USER_CONS_COLUMNS - http://docs.oracle.com/cd/B14117_01/server.101/b10755/statviews_2508.htm
			*/
			public const string Query =
				@"SELECT  UC.OWNER as FIRST_OWNER,
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
				AND uc.constraint_type = 'R'";

			public const string FirstOwner = "FIRST_OWNER";
			public const string FirstTable = "FIRST_TABLE";
			public const string FirstColumn = "FIRST_COLUMN";
			public const string SecondOwner = "SECOND_OWNER";
			public const string SecondTable = "SECOND_TABLE";
			public const string SecondColumn = "SECOND_COLUMN";
		}
	}
}
