Imports System.Xml

Public Class ViewerForm

	Dim _manager As System.Resources.ResourceManager
	'  LINQ to Object
	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		_manager = New System.Resources.ResourceManager(Me.GetType)

		' To generate a report.
		Dim rpt As New SectionReport
		rpt = New SectionReport
		rpt.LoadLayout(XmlReader.Create("..\\..\\rptLINQtoObject.rpx"))

		arvMain.LoadDocument(rpt)
	End Sub

End Class
