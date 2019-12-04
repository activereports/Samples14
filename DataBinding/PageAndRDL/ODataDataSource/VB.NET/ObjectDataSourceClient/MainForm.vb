Imports System.IO
Imports GrapeCity.ActiveReports
Imports ODataDataSource.ODataDataSource.Default

''' <summary>
''' ObjectDataSource client, which get data via OData
''' </summary>
Public Class MainForm
	Private ReadOnly _dataList As IList(Of Year)
	Private Const serviceUri As String = "http://localhost:55856/"

	Public Sub New()
		InitializeComponent()
		Dim container = New Container(New Uri(serviceUri))
		_dataList = GetYears(container)
	End Sub

	Private Function GetYears(container As Container) As IList(Of Year)
		Dim years = New List(Of Year)()
		For Each movie In container.Movies
			Dim year = movie.YearReleased
			If years.Count = 0 OrElse years(years.Count - 1).YearReleased <> year Then
				years.Add(New Year(year))
			End If
			Dim newMovie = New Movie(movie.Id, movie.Title, movie.MPAA)
			years(years.Count - 1).Movies.Add(newMovie)
		Next
		Return years
	End Function

	Private Sub OnLocateDataSource(sender As Object, args As LocateDataSourceEventArgs)
		If args.DataSet.Name = "Years" Then
			' ObjectsReport :bind collection to report
			args.Data = _dataList
		ElseIf args.DataSet.Name = "Movies" Then
			' SubObjectsReport :bind subcollection to subreport
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
				End If
			Next
		End If
	End Sub

	Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim rptPath = New FileInfo("..\..\..\..\Reports\ObjectsReport.rdlx")
		Dim pageReport = New PageReport(rptPath)
		AddHandler pageReport.Document.LocateDataSource, AddressOf OnLocateDataSource
		reportPreview.LoadDocument(pageReport.Document)
	End Sub
End Class
