Imports System.IO
Imports System.Windows.Forms.VisualStyles
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports GrapeCity.ActiveReports.Document

Public Class MainForm

	Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		RichTextBox.Rtf = My.Resources.Description
		Dim myPicturesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
		LoadImages(myPicturesPath, myPicturesPath + "\")
	End Sub

	Private Sub LoadImages(ByVal directory As String, ByVal basePath As String)
		For Each dir As String In IO.Directory.GetDirectories(directory)
			LoadImages(dir, basePath)
		Next dir

		Dim FileTypes As String() = New String() {"*.jpg", "*.gif", "*.bmp", "*.png"}
		Dim img As Image = Nothing
		Dim thumbnail As Image = Nothing

		For Each fileType As String In FileTypes
			For Each file As String In IO.Directory.GetFiles(directory, fileType)
				Try
					Dim title As String = file.Replace(basePath, "")
					img = Image.FromFile(file)
					thumbnail = GetImageThumbnail(img, ImageList.ImageSize.Width, ImageList.ImageSize.Height, Color.Transparent, VerticalAlignment.Center)
					ImageList.Images.Add(title, thumbnail)
					Dim imgInfo As FileInfo = New FileInfo(file)
					Dim ext As String = imgInfo.Extension
					ext = ext.Remove(0, 1) 'Remove leading '.'  
					Dim item As ListViewItem = New ListViewItem(title, title)
					item.Tag = ext
					listView.Items.Add(item)
				Catch ex As Exception
					'Swallow the exception and move on

				Finally
					If (Not img Is Nothing) Then
						img.Dispose()
					End If
					If (Not thumbnail Is Nothing) Then
						thumbnail.Dispose()
					End If
				End Try
			Next file
		Next fileType
	End Sub

	Function GetImageThumbnail(ByVal originalImage As Image, ByVal imgWidth As Integer, ByVal imgHeight As Integer, ByVal penColor As Color, ByVal alignment As VerticalAlignment) As Image
		imgWidth = Math.Min(imgWidth, originalImage.Width)
		imgHeight = Math.Min(imgHeight, originalImage.Height)
		Dim retoriginalImage As New Bitmap(imgWidth, imgHeight, PixelFormat.Format64bppPArgb)
		Dim grp As Graphics = Graphics.FromImage(retoriginalImage)
		Dim tnWidth As Integer = imgWidth
		Dim tnHeight As Integer = imgHeight

		' take the original image proportions into account. 
		If (originalImage.Width > originalImage.Height) Then
			tnHeight = CInt((CSng(originalImage.Height) / CSng(originalImage.Width)) * tnWidth)
		ElseIf (originalImage.Width < originalImage.Height) Then
			tnWidth = CInt((CSng(originalImage.Width) / CSng(originalImage.Height)) * tnHeight)
		End If

		Dim iLeft As Integer = 0
		Dim iTop As Integer = 0
		Select Case (alignment)
			Case VerticalAlignment.Center
				iLeft = Convert.ToInt16((imgWidth / 2) - (tnWidth / 2))
				iTop = Convert.ToInt16((imgHeight / 2) - (tnHeight / 2))

			Case VerticalAlignment.Bottom
				iLeft = Convert.ToInt16((imgWidth / 2) - (tnWidth / 2))
				iTop = Convert.ToInt16(imgHeight - tnHeight)
		End Select

		grp.PixelOffsetMode = PixelOffsetMode.None
		grp.InterpolationMode = InterpolationMode.HighQualityBicubic

		grp.DrawImage(originalImage, iLeft, iTop, tnWidth, tnHeight)

		Dim pn As Pen = New Pen(penColor, 1) 'Color.Wheat
		grp.DrawRectangle(pn, 0, 0, retoriginalImage.Width - 1, retoriginalImage.Height - 1)
		grp.Dispose()
		Return retoriginalImage
	End Function

	Private Sub showReport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles showReport.Click
		Dim reportData As Stream = Me.GetType.Assembly.GetManifestResourceStream("GrapeCity.ActiveReports.Samples.CustomResourceLocator.DemoReport.rdlx")
		Dim reader As New StreamReader(reportData)
		Dim def As New PageReport(reader)
		def.ResourceLocator = New MyPicturesLocator()
		Dim runtime As New PageDocument(def)
		runtime.Parameters("PictureName").CurrentValue = listView.SelectedItems(0).Text
		runtime.Parameters("MimeType").CurrentValue = String.Format("image/{0}", listView.SelectedItems(0).Tag)
		Dim preview As PreviewForm = Nothing

		Try
			preview = New PreviewForm(runtime)
			preview.ShowDialog(Me)
		Finally
			If (Not preview Is Nothing) Then
				preview.Dispose()
			End If
		End Try
	End Sub

	Private Sub listView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listView.SelectedIndexChanged

		showReport.Enabled = (listView.SelectedItems.Count > 0)
	End Sub

End Class
