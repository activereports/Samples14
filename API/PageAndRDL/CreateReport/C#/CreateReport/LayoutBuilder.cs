using System;
using System.Text;
using System.IO;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;

namespace GrapeCity.ActiveReports.Samples.CreateReport
{
	internal sealed class LayoutBuilder
	{
		//This section creates the Layout of the PageReport
		//It adds a Table to the report body
		//It adds the TableRows and TableColumns and TableCells
		//It adds Textbox reportitems to the TaleCells
		public static PageReport BuildReportLayout()
		{
			PageReport report = new PageReport();
			report.Report.Body.Height = "5cm";
			report.Report.Width = "20cm";
			//Creating a Table reportitem
			Table table = new Table();
			table.Name = "Table1";
			//Creating the rows,columns as well as TableCells for the table as well as TextBoxes to be placed within the Table Cells
			TextBox[] tableTextBoxes = new TextBox[6];
			TableCell[] tableCells = new TableCell[6];
			TableRow[] tableRows = new TableRow[2];
			TableColumn[] tableColumns = new TableColumn[3];
			String[] textBoxValues = new String[] {Properties.Resources.TitleValue, Properties.Resources.YearReleasedValue, Properties.Resources.MPAAValue, Properties.Resources.TitleField, Properties.Resources.YearReleasedField, Properties.Resources.MPAAField };

			String[] columnsWidth = new String[] { "9cm", "4.6cm", "5.3cm" };
			String[] rowsHeight = new String[] { "1.5cm", "1.5cm" };


			//Setting properties for the Textboxes to be placed in the TableCells
			for (int i = 0; i < tableTextBoxes.Length; i++)
			{
				tableTextBoxes.SetValue(new TextBox(), i);
				tableTextBoxes[i].Name = "textBox" + (i + 1);
				tableTextBoxes[i].Value = ExpressionInfo.FromString(textBoxValues[i]);
				tableTextBoxes[i].Style.PaddingBottom = tableTextBoxes[i].Style.PaddingLeft = tableTextBoxes[i].Style.PaddingRight = tableTextBoxes[i].Style.PaddingTop = ExpressionInfo.FromString("2pt");
				tableTextBoxes[i].Style.TextAlign = ExpressionInfo.FromString("Left");
				tableCells.SetValue(new TableCell(), i);
				tableCells[i].ReportItems.Add(tableTextBoxes[i]);//Adding the TextBoxes to the TableCells
				if (i < rowsHeight.Length)
				{
					tableRows.SetValue(new TableRow(), i);
					tableRows[i].Height = "1.25cm";
					table.Height += "1.25cm";
				}
				if (i < columnsWidth.Length)
				{
					tableColumns.SetValue(new TableColumn(), i);
					tableColumns[i].Width = columnsWidth[i];
					table.Width += columnsWidth[i];
					table.TableColumns.Add(tableColumns[i]);
					tableCells[i].ReportItems[0].Style.BackgroundColor = ExpressionInfo.FromString("LightBlue");
					tableRows[0].TableCells.Add(tableCells[i]);
				}
				else
				{
					tableCells[i].ReportItems[0].Style.BackgroundColor = ExpressionInfo.FromString("=Choose((RowNumber(\"Table1\") +1) mod 2, \"PaleGreen\",)");
					tableRows[1].TableCells.Add(tableCells[i]);
				}
			}
			table.Header.TableRows.Add(tableRows[0]);
			table.Details.TableRows.Add(tableRows[1]);
			table.Top = "1cm";
			table.Left = "0.635cm";
			report.Report.Body.ReportItems.Add(table);
			return report;
		}


		//This section adds the datasource to be used by the reportレポートに使用したデータソースを追加します。
		//It adds the dataset for the report,adds the query,the Fields and their expression
		public static PageReport AddDataSetDataSource(PageReport report)
		{
			// create DataSource for the report
			DataSource dataSource = new DataSource();
			dataSource.Name = "Reels Database";
			dataSource.ConnectionProperties.DataProvider = "OLEDB";
			dataSource.ConnectionProperties.ConnectString = ExpressionInfo.FromString(Properties.Resources.ConnectionString);
			//Create DataSet with specified query and load database fields to the DataSet
			DataSet dataSet = new DataSet();
			Query query = new Query();
			dataSet.Name = "Sample DataSet";
			query.DataSourceName = "Reels Database";
			query.CommandType = QueryCommandType.Text;
			query.CommandText = ExpressionInfo.FromString(Constants.cmdText);
			dataSet.Query = query;

			String[] fieldsList = new String[] { "MoviedID", "Title", "YearReleased", "MPAA" };

			foreach (string fieldName in fieldsList)
			{
				Field field = new Field(fieldName, fieldName, null);
				dataSet.Fields.Add(field);
			}
			//create report definition with specified DataSet and DataSource
			report.Report.DataSources.Add(dataSource);
			report.Report.DataSets.Add(dataSet);
			return report;
		}

		//This section loads the PageReport object created earlier to a Stream
		public static MemoryStream LoadReportToStream(PageReport report)
		{
			string rpt = report.ToRdlString();
			byte[] data = Encoding.UTF8.GetBytes(rpt);
			MemoryStream stream = new MemoryStream(data);
			return stream;
		}
	}
}
