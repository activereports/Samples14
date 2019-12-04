Imports System.Data.OleDb

Friend NotInheritable Class DataLayer
	' Returns IEnumerable data object.
	Public Shared Function LoadData() As IList(Of Year)
		Dim years = New List(Of Year)()

		Dim connStr = My.Resources.ConnectionString
		Dim conn = New OleDbConnection(connStr)
		conn.Open()
		Dim cmd = New OleDbCommand("SELECT Movie.YearReleased, Movie.MovieID, Movie.Title, Movie.MPAA FROM Movie ORDER BY Movie.YearReleased", conn)
		Dim dataReader = cmd.ExecuteReader()
		While dataReader.Read()
			Dim year As Integer = Integer.Parse(dataReader.GetValue(0).ToString())
			If years.Count = 0 OrElse years(years.Count - 1).YearReleased <> year Then
				years.Add(New Year(year))
			End If
			Dim movie = New Movie(Integer.Parse(dataReader.GetValue(1).ToString()), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString())
			years(years.Count - 1).Movies.Add(movie)
		End While
		conn.Close()

		Return years
	End Function
End Class

Friend NotInheritable Class Year
	Public Property YearReleased() As Integer
		Get
			Return _YearReleased
		End Get
		Private Set(value As Integer)
			_YearReleased = value
		End Set
	End Property
	Private _YearReleased As Integer
	Public Property Movies() As IList(Of Movie)
		Get
			Return _Movies
		End Get
		Private Set(value As IList(Of Movie))
			_Movies = value
		End Set
	End Property
	Private _Movies As IList(Of Movie)
	Public Sub New(year As Integer)
		YearReleased = year
		Movies = New List(Of Movie)()
	End Sub
End Class

Friend NotInheritable Class Movie
	Public Property MovieID() As Integer
		Get
			Return _MovieID
		End Get
		Private Set(value As Integer)
			_MovieID = value
		End Set
	End Property
	Private _MovieID As Integer
	Public Property Title() As String
		Get
			Return _Title
		End Get
		Private Set(value As String)
			_Title = value
		End Set
	End Property
	Private _Title As String
	Public Property MPAA() As String
		Get
			Return _MPAA
		End Get
		Private Set(value As String)
			_MPAA = value
		End Set
	End Property
	Private _MPAA As String
	Public Sub New(id As Integer, movieTitle As String, movieMPAA As String)
		MovieID = id
		Title = movieTitle
		MPAA = movieMPAA
	End Sub
End Class
