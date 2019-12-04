Imports GrapeCity.ActiveReports.Document.Section.Annotations
Imports System.IO
Imports System.Resources

Public Class AnnotationForm
	Private resource As New ResourceManager(Me.GetType)
	Private WithEvents _tsbAnnotation As New ToolStripButton(resource.GetString("CustomAnnotation"))

	Private Sub AnnotationForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		' Add custom button for annotation.
		Dim ts As ToolStrip = arvMain.Toolbar.ToolStrip
		ts.Items.Add(_tsbAnnotation)
		'Load report layout and run report
		arvMain.LoadDocument("..//..//Report//Annotation.rpx")
	End Sub

	Private Sub tsbExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _tsbAnnotation.Click
		'Depending on the presence or absence of annotation, to display the confirmation message. 
		If arvMain.Document.Pages(arvMain.ReportViewer.CurrentPage - 1).Annotations.Count > 0 Then
			MessageBox.Show(My.Resources.StampMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If
		' Gets the image from a resource seal.
		Dim thisExe As Reflection.Assembly
		thisExe = Reflection.Assembly.GetExecutingAssembly()
		Dim file As FileStream = New FileStream("..//..//Resources//stamp.png", FileMode.Open, FileAccess.Read, FileShare.Read)
		Dim imgStamp As New Bitmap(file)

		' Create an annotation, you can assign the value of the property.
		Dim annoImg As New AnnotationImage()
		annoImg.BackgroundImage = imgStamp ' Image
		annoImg.Color = Color.Transparent  'Background color
		annoImg.BackgroundLayout = Document.Section.Annotations.ImageLayout.Zoom  ' Display format
		annoImg.ShowBorder = False  'Display border (hidden)  
		' Add a comment.
		' (Specify the additional position)
		annoImg.Attach(6.09F, 1.19F)
		arvMain.Document.Pages(arvMain.ReportViewer.CurrentPage - 1).Annotations.Add(annoImg)
		' (Set the size)
		annoImg.Height = 0.7F
		annoImg.Width = 0.7F

		'To update the Viewer. 
		arvMain.Refresh()
	End Sub

End Class
