Friend Class Movie
	Public Property MovieID() As Integer
	Public Property Title() As String
	Public Property MPAA() As String

	Public Sub New(id As Integer, title As String, mpaa As String)
		Me.MovieID = id
		Me.Title = title
		Me.MPAA = mpaa
	End Sub
End Class
