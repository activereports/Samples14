Friend Class Year

	Public Property YearReleased() As Integer
		Get
			Return m_YearReleased
		End Get
		Private Set(value As Integer)
			m_YearReleased = value
		End Set
	End Property
	Private m_YearReleased As Integer

	Public Property Movies() As IList(Of Movie)
		Get
			Return m_Movies
		End Get
		Private Set(value As IList(Of Movie))
			m_Movies = value
		End Set
	End Property
	Private m_Movies As IList(Of Movie)

	Public Sub New(yearReleased As Integer)
		Me.YearReleased = yearReleased
		Me.Movies = New List(Of Movie)()
	End Sub
End Class
