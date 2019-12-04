Imports System
Imports System.Drawing

Imports GrapeCity.ActiveReports.Extensibility.Layout
Imports GrapeCity.ActiveReports.Extensibility.Layout.Internal
Imports GrapeCity.ActiveReports.Extensibility.Rendering.Components

Namespace Rendering.Layout
    Public Class RtfControlContentRange
        Inherits ContentRange
        Implements IStaticContentRange, ICustomContentRange

        Private ReadOnly _owner As ICustomReportItem
        Public ReadOnly Info As RtfContentInfo

        Public Sub New(owner As ICustomReportItem, info As RtfContentInfo)
            _owner = owner
            Me.Info = info
        End Sub

        Public Function GetNextRange(deltaHeight As Single, rtf As String) As RtfControlContentRange
            Dim newInfo = New RtfContentInfo(rtf, Info.Area, Info.TotalSize)
            Dim newContent = New RtfControlContentRange(_owner, newInfo)
            
            newContent.StartVertRange += deltaHeight
            newContent.EndVertRange += deltaHeight
            
            Return newContent
        End Function

        Public ReadOnly Property isLastPage As Boolean
            Get
                Return EndVertRange >= Info.TotalSize.Height
            End Get
        End Property

        Public Overrides ReadOnly Property Owner As IReportItem
            Get
                Return _owner
            End Get
        End Property

        Public Function Fork(reportItem As ICustomReportItem) As IContentRange Implements ICustomContentRange.Fork
            Return New RtfControlContentRange(_owner, Info)
        End Function

        Public Property StartVertRange As Single Implements IStaticContentRange.StartVertRange
            Get
                Return Info.Area.Top
            End Get
            Set
                Info.Area.Top = Math.Min(value, Info.TotalSize.Height)
            End Set
        End Property

        Public Property EndVertRange As Single Implements IStaticContentRange.EndVertRange
            Get
                Return Info.Area.Bottom
            End Get
            Set
                Info.Area.Bottom = Math.Min(value, Info.TotalSize.Height)
            End Set
        End Property

        Public Property StartHorzRange As Single Implements IStaticContentRange.StartHorzRange
            Get
                Return Info.Area.Left
            End Get
            Set
                Info.Area.Left = Math.Min(value, Info.TotalSize.Width)
            End Set
        End Property

        Public Property EndHorzRange As Single Implements IStaticContentRange.EndHorzRange
            Get
                Return Info.Area.Right
            End Get
            Set
                Info.Area.Right = Math.Min(value, Info.TotalSize.Width)
            End Set
        End Property

        Public ReadOnly Property CompleteItemHorizontally As Boolean Implements IStaticContentRange.CompleteItemHorizontally
            Get
                Return StartHorzRange >= EndHorzRange
            End Get
        End Property

        Public ReadOnly Property CompleteItemVertically As Boolean Implements IStaticContentRange.CompleteItemVertically
            Get
                Return StartVertRange >= EndVertRange
            End Get
        End Property

        Public ReadOnly Property ItemWidth As Integer Implements IStaticContentRange.ItemWidth
        Public ReadOnly Property ItemHeight As Integer Implements IStaticContentRange.ItemHeight
    End Class

    Public Class RtfContentInfo
        Public ReadOnly Rtf As String
        Public ReadOnly Property Area As Range
        Public ReadOnly Property TotalSize As SizeF

        Public Sub New(rtf As String, range As Range, totalSize As SizeF)
            Me.Rtf = rtf
            Me.Area = range
            Me.TotalSize = totalSize
        End Sub
    End Class

    Public Class Range
        Public Property Left As Single
        Public Property Right As Single
        Public Property Top As Single
        Public Property Bottom As Single

        Public Sub New(top As Single, bottom As Single, left As Single, right As Single)
            Me.Top = top
            Me.Bottom = bottom
            Me.Left = left
            Me.Right = right
        End Sub
    End Class
End NameSpace