using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;
using TextBox = GrapeCity.ActiveReports.PageReportModel.TextBox;
using System.Globalization;

namespace GrapeCity.ActiveReports.Samples.ReportWizard
{
	internal sealed class LayoutBuilder
	{
		private static class ComponentNames
		{
			public const string Table = "Table{0}";
			public const string TableGroup = "Table_Group{0}";
			public const string TableDetailGroup = "Table_DetailGroup{0}";
			public const string TextBox = "TextBox{0}";
		}

		private static readonly Dictionary<string, int> NameCounts;

		static LayoutBuilder()
		{
			NameCounts = new Dictionary<string, int>();
		}

		internal const string CRIName = "ContentPlaceHolder1";
		private const string DefaultColumnWidth = "2cm";
		private const string DefaultMeasurementUnits = "cm";
		private const string NumericTextAlignment = "Right";
		private const string DefaultTextAlignment = "Left";
		private const string DefaultPaddingLeft = "2pt";

		public static PageReport BuildReport(ReportWizardState state)
		{
			if (state == null)
				throw new ArgumentNullException("state");
			return BuildContentLayout(state);
		}

		public static PageReport BuildContentLayout(ReportWizardState state)
		{
			PageReport def = null;
			//check if Table can be created
			if (state.DisplayFields.Count > 0 || state.GroupingFields.Count > 0)
			{
				FileInfo masterFile = new FileInfo("../../" + state.SelectedMasterReport.MasterReportFile);
				def = PageReport.CreateFromMaster(new Uri(masterFile.FullName));
				CustomReportItem placeHolder = (CustomReportItem)def.Report.Body.ReportItems[CRIName];
				Length contentHeight = placeHolder.Height;
				Table table = BuildTable(state, contentHeight);
				placeHolder.ReportItems.Add(table);
			}
			if (def == null)
			{
				def = new PageReport();
				def.Report.Body.Height = "15cm";
				def.Report.Width = "20cm";
			}
			return def;
		}

		/// <summary>
		///	Builds a table for the given report state, the table will use at most <paramref name="contentHeight"/> height
		/// </summary>
		/// <param name="state">The state of the wizard</param>
		///<param name="contentHeight">The height of the area to work within</param> 
		///<returns>A <see cref="Table"/> data region which is at most <paramref name="contentHeight"/> tall</returns> 
		private static Table BuildTable(ReportWizardState state, Length contentHeight)
		{
			Table table = new Table();
			table.Name = GetNameForComponent(ComponentNames.Table);
			Length rowHeight = CalculateRowHeight(state, contentHeight);
			CreateTableColumns(state, table);
			CreateTableGroups(state, table, rowHeight);
			Grouping detailGroup = CreateDetailGrouping(state);
			if (detailGroup != null)
			{
				table.Details.Grouping = detailGroup;
			}
			//create Table Header, Footer and Detail sections
			TableRow tableHeader = CreateTableHeaderRow(state);
			table.Header.TableRows.Add(tableHeader);
			TableRow tableFooter = CreateTableFooter(state);
			table.Footer.TableRows.Add(tableFooter);
			TableRow tableDetails = CreateTableDetails(state);
			table.Details.TableRows.Add(tableDetails);			
			tableHeader.Height = tableFooter.Height = tableDetails.Height = rowHeight;
			table.Height = CalculateTableHeight(state, contentHeight);
			table.Width = CalculateTableWidth(state);
			return table;
		}

		private static Length CalculateTableHeight(ReportWizardState state, Length contentHeight)
		{
			const double DefaultRowHeight = 0.75;
			// There is a header and footer for each group, plus the table header and footer, and the detail row
			int numberOfRows = 3 + (state.GroupingFields.Count * 2);
			double normalHeight = (DefaultRowHeight * numberOfRows);
			if (normalHeight < contentHeight.ToCentimeters())
			{
				return new Length(normalHeight.ToString(CultureInfo.InvariantCulture) + DefaultMeasurementUnits);
			}
			return contentHeight;
		}

		private static TableRow CreateTableDetails(ReportWizardState state)
		{
			List<FieldMetaData> columns = new List<FieldMetaData>();
			columns.AddRange( state.GroupingFields );
			columns.AddRange( state.DisplayFields );
			TableRow tableDetails = new TableRow();
			foreach (FieldMetaData field in columns)
			{
				TableCell tableDetailsCell = new TableCell();
				TextBox tableDetailsTextBox = CreateTextBox();
				
				tableDetailsTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
				if (state.DisplayFields.Contains(field)) 
				{
					//if detail grouping set, summarize summarizeable
					if ((state.DetailGroupingField != null) &&
						(field.AllowTotaling) &&
						(field != state.DetailGroupingField))
					{
						tableDetailsTextBox.Value = GetFieldSummaryExpression(field);
					}
					else
						tableDetailsTextBox.Value = GetFieldValueExpression(field);
					if (field.IsNumeric)
					{
						tableDetailsTextBox.Style.TextAlign = NumericTextAlignment;
					}
					else
					{
						tableDetailsTextBox.Style.TextAlign = DefaultTextAlignment;
						tableDetailsTextBox.Style.PaddingLeft = DefaultPaddingLeft;
					}
					if( !string.IsNullOrEmpty( field.FormatString ))
						tableDetailsTextBox.Style.Format = field.FormatString;	
				}
				tableDetailsCell.ReportItems.Add(tableDetailsTextBox);
				tableDetails.TableCells.Add(tableDetailsCell);
			}
			return tableDetails;
		}

		private static TableRow CreateTableFooter(ReportWizardState state)
		{
			List<FieldMetaData> columns = new List<FieldMetaData>();
			columns.AddRange(state.GroupingFields);
			columns.AddRange(state.DisplayFields);
			TableRow tableFooter = new TableRow();
			foreach (FieldMetaData field in columns)
			{
				TableCell tableFooterCell = new TableCell();
				TextBox tableFooterTextBox = CreateTextBox();
				tableFooterTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
				if (field.IsNumeric)
					if (state.DisplayGrandTotals && field.AllowTotaling)
					{
						tableFooterTextBox.Value = GetFieldSummaryExpression(field);
						if (field.IsNumeric)
						{
							tableFooterTextBox.Style.TextAlign = NumericTextAlignment;
							tableFooterTextBox.Style.ShrinkToFit = "True";
						}
						if (!string.IsNullOrEmpty(field.FormatString))
						{
							tableFooterTextBox.Style.Format = field.FormatString;
						}
						tableFooterTextBox.Style.BorderColor.Top = "LightGray";
						tableFooterTextBox.Style.BorderStyle.Top = "Double";
						tableFooterTextBox.Style.BorderWidth.Top = "3pt";
					}
				tableFooterCell.ReportItems.Add(tableFooterTextBox);
				tableFooter.TableCells.Add(tableFooterCell);
			}
			return tableFooter;
		}

		private static Grouping CreateDetailGrouping(ReportWizardState state)
		{
			Grouping detailGroup = null;
			if (state.DetailGroupingField != null)
			{
				detailGroup = new Grouping();
				detailGroup.Name = GetNameForComponent( ComponentNames.TableDetailGroup);
				detailGroup.GroupExpressions.Add(GetFieldValueExpression(state.DetailGroupingField));
			}
			return detailGroup;
		}

		private static TableRow CreateTableHeaderRow(ReportWizardState state)
		{
			List<FieldMetaData> columns = new List<FieldMetaData>();
			columns.AddRange(state.GroupingFields);
			columns.AddRange(state.DisplayFields);
			TableRow tableHeader = new TableRow();
			foreach (FieldMetaData field in columns)
			{
				TableCell tableHeaderCell = new TableCell();
				TextBox tableHeaderTextBox = CreateTextBox();
				tableHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox);
				tableHeaderTextBox.Value = field.Title;
				//set table header values according to grouping expressions
				if (state.GroupingFields.Contains(field))
				{
					if (field.IsNumeric)
						tableHeaderTextBox.Style.TextAlign = NumericTextAlignment;
					else
					{
						tableHeaderTextBox.Style.TextAlign = DefaultTextAlignment;
						tableHeaderTextBox.Style.PaddingLeft = DefaultPaddingLeft;
					}
				}
				tableHeaderCell.ReportItems.Add(tableHeaderTextBox);
				tableHeader.TableCells.Add(tableHeaderCell);
			}
			return tableHeader;
		}

		private static void CreateTableGroups( ReportWizardState state, Table table, Length rowHeight )
		{
			foreach( FieldMetaData groupField in state.GroupingFields )
			{
				TableGroup group = CreateTableGroup( state, groupField, rowHeight );
				if( group != null )
				{
					table.TableGroups.Add( group );
					foreach( TableRow row in group.Header.TableRows )
					{
						table.Height += row.Height;
					}
					foreach( TableRow row in group.Footer.TableRows )
					{
						table.Height += row.Height;
					}
				}
			}
		}

		private static Length CalculateRowHeight( ReportWizardState state, Length contentHeight )
		{
			//Calculate the height of each row 
			// Restriced by the height of the content placeholder
			const double DefaultRowHeight = 0.75;
			// There is a header and footer for each group, plus the table header and footer, and the detail row
			int numberOfRows = 3 + (state.GroupingFields.Count * 2);
			double rowHeight = Math.Min( DefaultRowHeight, (contentHeight.ToCentimeters() / numberOfRows) );
			return rowHeight.ToString( CultureInfo.InvariantCulture ) + DefaultMeasurementUnits;
		}

		private static TableGroup CreateTableGroup( ReportWizardState state, FieldMetaData groupField, Length rowHeight )
		{
			List<FieldMetaData> columns = new List<FieldMetaData>();
			columns.AddRange( state.GroupingFields );
			columns.AddRange( state.DisplayFields );
			TableGroup tableGroup = new TableGroup();
			TableRow groupHeaderRow = new TableRow();
			TableRow groupFooterRow = new TableRow();
			tableGroup.Grouping.Name = GetNameForComponent( ComponentNames.TableGroup );
			tableGroup.Grouping.GroupExpressions.Add( GetFieldValueExpression( groupField ) );
			groupFooterRow.Height = groupHeaderRow.Height = rowHeight;
			foreach( FieldMetaData columnField in columns )
			{
				TableCell groupHeaderCell = new TableCell();
				TableCell groupFooterCell = new TableCell();
				//set TextBoxes names avoiding duplicate names
				TextBox groupHeaderTextBox = CreateTextBox();
				groupHeaderTextBox.Name = GetNameForComponent( ComponentNames.TextBox );
				TextBox groupFooterTextBox = CreateTextBox();
				groupFooterTextBox.Name = GetNameForComponent( ComponentNames.TextBox );
				// show grouping expression value in the appropriate table cell
				if (groupField == columnField)
				{
					groupHeaderTextBox.Value = GetFieldValueExpression( groupField );
					if (!string.IsNullOrEmpty( groupField.FormatString) )
					{
						groupHeaderTextBox.Style.Format = groupField.FormatString;
					}
					if ( groupField.IsNumeric )
					{
						groupHeaderTextBox.Style.TextAlign = NumericTextAlignment;
					}
					else
					{
						groupHeaderTextBox.Style.TextAlign = DefaultTextAlignment;
						groupHeaderTextBox.Style.PaddingLeft = DefaultPaddingLeft;
					}
				}
				//add group subtotals to the table cells if needed
				if (state.DisplayGroupSubtotals)
				{
					//We only do subtotals on the fields selected for output, not the groups 
					if ( state.DisplayFields.Contains( columnField ) )
					{
						if (columnField.AllowTotaling)
						{
							groupFooterTextBox.Value = GetFieldSummaryExpression( columnField );
							groupFooterTextBox.Style.TextAlign = NumericTextAlignment;
							if ( !string.IsNullOrEmpty( columnField.FormatString) )
							{
								groupFooterTextBox.Style.Format = columnField.FormatString;
							}
							groupFooterTextBox.Style.BorderColor.Top = "LightGray";
							groupFooterTextBox.Style.BorderStyle.Top = "Solid";
							groupFooterTextBox.Style.BorderWidth.Top = "1pt";
							groupFooterTextBox.Style.ShrinkToFit = "True";
						}
					}
				}
				groupHeaderCell.ReportItems.Add(groupHeaderTextBox);
				groupFooterCell.ReportItems.Add(groupFooterTextBox);
				groupHeaderRow.TableCells.Add(groupHeaderCell);
				groupFooterRow.TableCells.Add(groupFooterCell);
			}
			tableGroup.Header.TableRows.Add(groupHeaderRow);
			tableGroup.Footer.TableRows.Add(groupFooterRow);
			return tableGroup;
		}

		private static void CreateTableColumns( ReportWizardState state, Table table )
		{
			if( table.TableColumns.Count > 0 )
				throw new ArgumentException( Properties.Resources.ArgumentExceptionMessage, "table" );
			List<FieldMetaData> columns = new List<FieldMetaData>();
			columns.AddRange( state.GroupingFields );
			columns.AddRange( state.DisplayFields );
			foreach( FieldMetaData columnField in columns )
			{
				//create Table Columns with predefined widths
				TableColumn tableColumn = new TableColumn();
				if( !string.IsNullOrEmpty( columnField.PreferredWidth ) )
				{
					tableColumn.Width = columnField.PreferredWidth;
				}
				else
				{
					tableColumn.Width = DefaultColumnWidth;
				}
				table.TableColumns.Add( tableColumn );
			}
		}

		private static Length CalculateTableWidth( ReportWizardState state )
		{
			Length length = new Length("0.0cm");
			foreach( FieldMetaData field in state.GroupingFields )
			{
				length += new Length( field.PreferredWidth );
			}
			foreach( FieldMetaData field in state.DisplayFields )
			{
				length += new Length( field.PreferredWidth );
			}
			return length;
		}

		/// <summary>
		///creates new TextBox with the default style 
		/// </summary>
		/// <returns></returns>
		private static TextBox CreateTextBox()
		{
			TextBox textBox = new TextBox();
			textBox.CanGrow = true;
			textBox.Style.FontSize = "8pt";
			return textBox;
		}

		private static string GetFieldSummaryExpression( FieldMetaData field )
		{
			string format = "={0}({1})";

			string fieldValue = GetFieldValue( field );
			return string.Format( format, field.SummaryFunction, fieldValue );
		}

		private static string GetFieldValue( FieldMetaData field )
		{
			const string validName = @"^[a-zA-Z_]([a-zA-Z0-9_]*)$";
			const string itemSyntax = "Fields!{0}.Value";
			const string indexerSyntax = "Fields(\"{0}\").Value";

			string formatString;
			if( Regex.IsMatch( field.Name, validName ) )
			{
				formatString = itemSyntax;
			}
			else
			{
				formatString = indexerSyntax;
			}
			return string.Format( formatString, field.Name );
		}

		private static string GetFieldValueExpression( FieldMetaData field )
		{
			string format = "={0}";
			return string.Format( format, GetFieldValue( field ) );
		}

		private static string GetNameForComponent( string baseName )
		{
			string name = baseName;
			int count;
			if( !NameCounts.TryGetValue( baseName, out count ) )
			{
				count = 0;
			}
			count++;
			name = string.Format( name, count );
			NameCounts[baseName] = count;
			return name;
		}
	}
}
