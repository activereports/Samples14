Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.Enterprise.Data.DataEngine.Expressions
Imports System.IO
Imports System.Text

Friend NotInheritable Class LayoutBuilder
	' This section creates the Layout of the PageReport
	' It adds a Table to the report body
	' It adds the TableRows and TableColumns and TableCells
	' It adds Textbox reportitems to the TaleCells

	Public Shared Function BuildReportLayout() As PageReport
		Dim report As New PageReport()
		report.Report.Body.Height = "5cm"
		report.Report.Width = "20cm"
		'Creating a Table reportitem
		Dim table As New Table()
		table.Name = "Table1"
		'Creating the rows,columns as well as TableCells for the table as well as TextBoxes to be placed within the Table Cells
		Dim tableTextBoxes As TextBox() = New TextBox(5) {}
		Dim tableCells As TableCell() = New TableCell(5) {}
		Dim tableRows As TableRow() = New TableRow(1) {}
		Dim tableColumns As TableColumn() = New TableColumn(2) {}

		Dim textBoxValues As [String]() = New [String](5) {My.Resources.TitleValue, My.Resources.YearReleasedValue, My.Resources.MPAAValue, My.Resources.TitleField, My.Resources.YearReleasedField, My.Resources.MPAAField}

		Dim columnsWidth As [String]() = New [String]() {"9cm", "4.6cm", "5.3cm"}
		Dim rowsHeight As [String]() = New [String]() {"1.5cm", "1.5cm"}
		'Setting properties for the Textboxes to be placed in the TableCells
		For i As Integer = 0 To tableTextBoxes.Length - 1
			tableTextBoxes.SetValue(New TextBox(), i)
			tableTextBoxes(i).Name = "textBox" & (i + 1)
			tableTextBoxes(i).Value = ExpressionInfo.FromString(textBoxValues(i))
			With tableTextBoxes(i).Style
				.PaddingBottom = "2pt"
				.PaddingLeft = "2pt"
				.PaddingRight = "2pt"
				.PaddingTop = "2pt"
			End With
			tableTextBoxes(i).Style.TextAlign = ExpressionInfo.FromString("Left")
			tableCells.SetValue(New TableCell(), i)
			tableCells(i).ReportItems.Add(tableTextBoxes(i))
			'Adding the TextBoxes to the TableCells
			If i < rowsHeight.Length Then
				tableRows.SetValue(New TableRow(), i)
				tableRows(i).Height = "1.25cm"
				table.Height += "1.25cm"
			End If
			If i < columnsWidth.Length Then
				tableColumns.SetValue(New TableColumn(), i)
				tableColumns(i).Width = columnsWidth(i)
				table.Width += columnsWidth(i)
				table.TableColumns.Add(tableColumns(i))
				tableCells(i).ReportItems(0).Style.BackgroundColor = ExpressionInfo.FromString("LightBlue")
				tableRows(0).TableCells.Add(tableCells(i))
			Else
				tableCells(i).ReportItems(0).Style.BackgroundColor = ExpressionInfo.FromString("=Choose((RowNumber(""Table1"") +1) mod 2, ""PaleGreen"",)")
				tableRows(1).TableCells.Add(tableCells(i))
			End If
		Next
		table.Header.TableRows.Add(tableRows(0))
		table.Details.TableRows.Add(tableRows(1))
		table.Top = "1cm"
		table.Left = "0.635cm"
		report.Report.Body.ReportItems.Add(table)
		Return report
	End Function

	'This section adds the datasource to be used by the report
	'It adds the dataset for the report,adds the query,the Fields and their expression
	Public Shared Function AddDataSetDataSource(report As PageReport) As PageReport
		'create DataSource for the report
		Dim dataSource As New DataSource()
		dataSource.Name = "Reels Database"
		dataSource.ConnectionProperties.DataProvider = "OLEDB"
		'Fetching the connectionstring from the GetDBPath class
		dataSource.ConnectionProperties.ConnectString = ExpressionInfo.FromString(My.Resources.ConnectionString)
		'Create DataSet with specified query and load database fields to the DataSet
		Dim dataSet As New DataSet()
		Dim query As New Query()
		dataSet.Name = "Sample DataSet"
		query.DataSourceName = "Reels Database"
		query.CommandType = QueryCommandType.Text
		query.CommandText = ExpressionInfo.FromString(Constants.cmdText)
		dataSet.Query = query
		Dim fieldsList As [String]() = New [String]() {"MoviedID", "Title", "YearReleased", "MPAA"}

		For Each fieldName As String In fieldsList
			Dim field As New Field(fieldName, fieldName, Nothing)
			dataSet.Fields.Add(field)
		Next
		'create report definition with specified DataSet and DataSource
		report.Report.DataSources.Add(dataSource)
		report.Report.DataSets.Add(dataSet)
		Return report
	End Function

	'This section loads the PageReport object created earlier to a Stream
	Public Shared Function LoadReportToStream(report As PageReport) As MemoryStream
		Dim rpt As String = report.ToRdlString()

		Dim data As Byte() = Encoding.UTF8.GetBytes(rpt)
		Dim stream As New MemoryStream(data)
		Return stream
	End Function

End Class
