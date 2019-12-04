Imports System.IO
Imports GrapeCity.ActiveReports

''' <summary>
''' Json Provider clinet, which get data via OData.
''' </summary>
Public Class MainForm

	Private _pageReport As PageReport
	Private Const _path As String = "..\..\..\..\Reports\testReport.rdlx"
	Private _service As Service = Service.None

	Public Sub New()
		InitializeComponent()
		Dim rptPath As FileInfo = New FileInfo(_path)
		_pageReport = New PageReport(rptPath)
		AddHandler _pageReport.Document.LocateDataSource, AddressOf OnLocateDataSource
		comboBox1.SelectedIndex = 0
	End Sub


	' The handler of <see cref="PageDocument.LocateDataSource"/> that returns appropriate data for a report.
	Private Sub OnLocateDataSource(sender As Object, args As LocateDataSourceEventArgs)
		Dim data As String = Nothing
		Dim dataSourceName As String = args.DataSet.Query.DataSourceName
		If dataSourceName = "DataSource1" Then
			Try
				data = DataLayer.CreateData(_service)
			Catch ex As Exception
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
				Return
			End Try
		End If
		args.Data = data
	End Sub


	Private Sub comboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBox1.SelectedIndexChanged
		If _service = comboBox1.SelectedIndex Then
			Return
		End If

		_service = CType([Enum].Parse(GetType(Service), comboBox1.SelectedIndex.ToString()), Service)
		reportPreview.LoadDocument(_pageReport.Document)
	End Sub
End Class
