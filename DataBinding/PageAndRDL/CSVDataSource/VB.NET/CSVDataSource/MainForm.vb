Imports System.IO
Imports System.Resources

Public Class MainForm

	Dim _resource As New ResourceManager(Me.GetType)
	ReadOnly _settingForNoHeaderFixed = _resource.GetString("NoHeaderFixed")
	ReadOnly _settingForHeaderExistsFixed = _resource.GetString("HeaderExistsFixed")

	' Loads and shows the report.
	Private Sub btnCSV_Click(sender As System.Object, e As System.EventArgs) Handles btnCSV.Click
		Const settingForNoHeaderDelimited As String = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued"

		Dim connectionString = String.Empty
		If rbtnNoHeaderComma.Checked Then
			connectionString = String.Format("Path={0};Encoding={1};TextQualifier="";ColumnsSeparator=,;RowsSeparator=\r\n;Columns={2}",
											 My.Resources.PathToFileNoHeaderComma, My.Resources.CSVEncoding, settingForNoHeaderDelimited)
		ElseIf rbtnHeaderTab.Checked Then
			connectionString = String.Format("Path={0};Encoding={1};TextQualifier="";ColumnsSeparator=" & vbTab & ";RowsSeparator=\r\n;HasHeaders=True",
											 My.Resources.PathToFileHeaderTab, My.Resources.CSVEncoding)
		ElseIf rbtnHeader.Checked Then
			connectionString = String.Format("Path={0};Encoding={1};Columns={2};HasHeaders=True",
											 My.Resources.PathToFileHeader, My.Resources.CSVEncoding, _settingForHeaderExistsFixed)
		ElseIf rbtnNoHeader.Checked Then
			connectionString = String.Format("Path={0};Encoding={1};Columns={2}",
											 My.Resources.PathToFileNoHeader, My.Resources.CSVEncoding, _settingForNoHeaderFixed)
		End If

		Dim report = New PageReport(New FileInfo("..\..\Reports\StockList.rdlx"))
		Dim connectionProps = report.Report.DataSources(0).ConnectionProperties
		connectionProps.DataProvider = "CSV"
		connectionProps.ConnectString = connectionString
		arvMain.ReportViewer.LoadDocument(report.Document)
	End Sub

End Class
