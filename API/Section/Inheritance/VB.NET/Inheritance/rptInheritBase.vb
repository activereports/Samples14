Imports System.IO
Imports System.Resources

Public Class rptInheritBase
	Inherits GrapeCity.ActiveReports.SectionReport

	'Path to the csv file.
	Private _csvPath As String
	'Stream to load the csv file.
	Private _invoiceFileStream As StreamReader
	'String array to store the data.
	Private _fieldNameArray As String()

	Private _resource As New ResourceManager(GetType(rptInheritBase))

	Public Sub New()
		' This call is required by the ActiveReports designer.
	End Sub

	'ActiveReport overrides dispose to clean up for the list of components.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub

	Protected WriteOnly Property CsvPath() As String
		Set(ByVal Value As String)
			'CsvPath property
			_csvPath = Value
		End Set
	End Property

	Protected Sub BaseReport_DataInitialize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DataInitialize

		'  Load CSV to stream.
		Dim invoiceFileStream As New StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))))


		'Read a line from the stream, and create an array string.
		Dim _currentLine As String = invoiceFileStream.ReadLine()
		_fieldNameArray = _currentLine.Split(New Char() {","c})

		'Field only to create a number of the array.
		Dim i As Integer
		For i = 0 To _fieldNameArray.Length - 1
			Fields.Add(_fieldNameArray(i).ToString())
		Next i
	End Sub

	Protected Sub BaseReport_FetchData(ByVal sender As Object, ByVal eArgs As GrapeCity.ActiveReports.SectionReport.FetchEventArgs) Handles MyBase.FetchData
		Try
			'Read a line from the stream, and creats an array string.
			Dim _currentLine As String = _invoiceFileStream.ReadLine()
			Dim _currentArray As String() = _currentLine.Split(New Char() {","c})

			'Store the Value property of Field object number of the array.
			Dim i As Integer
			For i = 0 To _currentArray.Length - 1
				Fields(_fieldNameArray(i).ToString()).Value = _currentArray(i).ToString()
			Next i

			'Set EOF to false and continue to read the data.
			eArgs.EOF = False
		Catch
			'Close the stream when the it has exceeded the time to read the last line.
			_invoiceFileStream.Close()

			'Set EOF to true and quit reading the data.
			eArgs.EOF = True
		End Try
	End Sub

	Protected Sub rptInheritBase_ReportStart(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ReportStart
		'  Load CSV to stream.
		_invoiceFileStream = New StreamReader(_csvPath, System.Text.Encoding.GetEncoding(Convert.ToInt32(_resource.GetString("CodePage"))))
		_invoiceFileStream.ReadLine()
	End Sub
End Class
