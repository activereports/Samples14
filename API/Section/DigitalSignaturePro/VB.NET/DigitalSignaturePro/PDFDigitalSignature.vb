Imports System.Resources
Imports GrapeCity.ActiveReports.Export.Pdf.Section.Signing
Imports System.Security.Cryptography.X509Certificates
Imports GrapeCity.ActiveReports.Export.Pdf.Section
Imports System.IO
Imports GrapeCity.ActiveReports

Public Class PDFDigitalSignature
	Private _resource As ResourceManager
	Public Sub New()
		_resource = New ResourceManager([GetType]())
		InitializeComponent()
	End Sub

	Private Sub PDFDigitalSignature_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'Set the default for in the 'Signature Format' combo box.
		cmbVisibilityType.SelectedIndex = 3

		arvMain.LoadDocument("..//..//Report//Invoice.rpx")
	End Sub

	Private Sub pdfExportButton_Click(sender As Object, e As EventArgs) Handles pdfExportButton.Click
		Dim oPDFExport As New Export.Pdf.Section.PdfExport()
		Dim sfd As New SaveFileDialog()
		Dim tmpCursor As Cursor = Cursor
		Dim tempPath As String = String.Empty
		' Display the save dialog.

		sfd.Title = _resource.GetString("Title")
		'Title 
		sfd.FileName = "DigitalSignature.pdf"
		' Name of the file for initial display
		sfd.Filter = "PDF|*.pdf"
		' Filter
		If sfd.ShowDialog() <> DialogResult.OK Then
			Return
		End If

		Try
			' Change the cursor.
			Cursor = Cursors.WaitCursor
			Application.DoEvents()

			' Sets the type of signature.
			oPDFExport.Signature.VisibilityType = CType(cmbVisibilityType.SelectedIndex, VisibilityType)

			' Set the signature display area.
			oPDFExport.Signature.Stamp.Bounds = New RectangleF(0.05F, 0.05F, 4.0F, 0.93F)

			' Sets the character position of the signature text
			oPDFExport.Signature.Stamp.TextAlignment = Alignment.Left

			oPDFExport.Signature.Stamp.Font = New Font(_resource.GetString("Font"), 9, FontStyle.Regular, GraphicsUnit.Point, 128)


			' Set the rectangle in which the text is placed in the area that displays the signature.
			'  The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
			oPDFExport.Signature.Stamp.TextRectangle = New RectangleF(1.2F, 0F, 2.8F, 0.93F)

			' Set the signature image.
			Dim fs As New FileStream(Application.StartupPath + "..//..//..//Image//gc.bmp", FileMode.Open, FileAccess.Read)
			oPDFExport.Signature.Stamp.Image = New Bitmap(Image.FromStream(fs))
			fs.Close()

			' Set the display position of the signature image.
			oPDFExport.Signature.Stamp.ImageAlignment = Alignment.Center

			' Set the rectangle image so that it is placed in the area that displays the signature.
			' The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
			oPDFExport.Signature.Stamp.ImageRectangle = New RectangleF(0F, 0F, 1.0F, 0.93F)

			' Sets the password for the certificate and digital signature.
			' For X509Certificate2 class, etc. Please refer to the site of Microsoft.
			' 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
			' 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
			oPDFExport.Signature.Certificate = New X509Certificate2(Application.StartupPath + "..//..//..//GrapeCity.pfx", "password")

			' 
			If chkTimeStamp.Checked Then
				oPDFExport.Signature.TimeStamp = New TimeStamp("https://freetsa.org/tsr", "", "")
			End If

			' Sets the time stamp.
			oPDFExport.Signature.SignDate = New SignatureField(Of DateTime)(DateTime.Now, True)
			' Signing time
			oPDFExport.Signature.DistinguishedName.Visible = False
			' Display whether or not the distinguished name
			oPDFExport.Signature.Contact = New SignatureField(Of String)("activereports.support@grapecity.com", True)
			' Contact
			oPDFExport.Signature.Reason = New SignatureField(Of String)(_resource.GetString("ApprovalText"), True)
			' Reason

			oPDFExport.Signature.Location = New SignatureField(Of String)("Pittsburgh", True)
			' Location
			tempPath = Path.GetTempFileName()
			' Export the file.
			oPDFExport.Export(arvMain.Document, tempPath)
			File.Delete(sfd.FileName)
			File.Move(tempPath, sfd.FileName)

			'Start the output file (Open)
			System.Diagnostics.Process.Start(sfd.FileName)

			' Display the notification message.
			MessageBox.Show(Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch generatedExceptionName As PdfSigningException
			File.Delete(tempPath)
			MessageBox.Show(Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.[Error])
		Finally
			' Replace the cursor
			Cursor = tmpCursor
			Application.DoEvents()

			' End processing
			sfd.Dispose()
			oPDFExport.Dispose()
		End Try
	End Sub
End Class
