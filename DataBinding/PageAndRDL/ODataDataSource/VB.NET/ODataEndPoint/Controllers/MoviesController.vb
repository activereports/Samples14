Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.Web.Http
Imports System.Web.OData

''' <summary>
''' Controller is based on article https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
''' </summary>
Public Class MoviesController
	Inherits ODataController
	Public Function [Get]() As IList(Of Movie)
		Dim movies As New List(Of Movie)()

		Dim connStr = UpdateConnectionString(My.Resources.Reels)
		Dim conn As New OleDbConnection(connStr)
		conn.Open()
		Dim cmd As New OleDbCommand("SELECT Movie.MovieID, Movie.Title, Movie.MPAA, Movie.YearReleased FROM Movie ORDER BY Movie.YearReleased", conn)
		Dim dataReader As OleDbDataReader = cmd.ExecuteReader()
		While dataReader.Read()
			movies.Add(New Movie() With {
				.Id = CType(dataReader.GetValue(0), Integer),
				.Title = dataReader.GetValue(1).ToString(),
				.MPAA = dataReader.GetValue(2).ToString(),
				.YearReleased = CType(dataReader.GetValue(3), Integer)
			})
		End While
		conn.Close()

		Return movies
	End Function

End Class
