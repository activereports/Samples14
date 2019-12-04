Imports System.Collections.Generic
Imports System.IO

Public Class MainForm

	Private _dataList As IList(Of Year)

	' Loads and shows the report.
	Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		_dataList = DataLayer.LoadData()

		Dim rptPath As New FileInfo("..\..\ObjectsReport.rdlx")
		Dim pageReport As New PageReport(rptPath)
		AddHandler pageReport.Document.LocateDataSource, AddressOf OnLocateDataSource
		reportPreview.LoadDocument(pageReport.Document)
	End Sub

	' The handler of <see cref="PageDocument.LocateDataSource"/> that returns appropriate data for a report.
	Private Sub OnLocateDataSource(ByVal sender As Object, ByVal args As LocateDataSourceEventArgs)
		If args.DataSet.Name = "Years" Then ' ObjectsReport :bind collection to report
			args.Data = _dataList
		ElseIf args.DataSet.Name = "Movies" Then ' SubObjectsReport :bind subcollection to subreport
			Dim yearReleased = 1970
			For Each param In args.Parameters
				If param.Name = "YearReleased" Then
					yearReleased = Integer.Parse(param.Value.ToString())
					Exit For
				End If
			Next
            For i As Integer = 0 To _dataList.Count - 1
                If _dataList(i).YearReleased = yearReleased Then
                    args.Data = _dataList(i).Movies
                    Exit For
                End If
            Next
		End If
	End Sub

End Class
