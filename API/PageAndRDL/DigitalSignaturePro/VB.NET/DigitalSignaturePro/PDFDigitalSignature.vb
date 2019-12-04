Imports GrapeCity.ActiveReports.Export.Pdf.Section
Imports GrapeCity.ActiveReports.Export.Pdf.Section.Signing
Imports System.IO
Imports System.Resources
Imports GrapeCity.ActiveReports.Document

Public Class PDFDigitalSignature
	Private _resource As ResourceManager
	Private _pageDocument As PageDocument
	Private Sub PDFDigitalSignature_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		' Set the default state of the combo "format of the signature"
		cmbVisibilityType.SelectedIndex = 3
		_resource = New ResourceManager(Me.GetType)
		Dim PageReport = New PageReport()
		_pageDocument = PageReport.Document
		PageReport.Load(New FileInfo("..//..//..//..//Report//Catalog.rdlx"))
		arvMain.LoadDocument(_pageDocument)
	End Sub

	Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExport.Click

		Dim pdfRE = New Export.Pdf.Page.PdfRenderingExtension()
		Dim settings = New Export.Pdf.Page.Settings()

		Dim sfd As New SaveFileDialog()
		Dim tmpCursor As Cursor = Nothing
		Dim tempPath As String = String.Empty

		sfd.Title = _resource.GetString("Title")
		' Title
		' Name of the file for initial display
		sfd.FileName = "DigitalSignature.pdf"
		sfd.Filter = "PDF|*.pdf"           ' Filter
		If sfd.ShowDialog() <> DialogResult.OK Then
			Exit Sub
		End If

		Try
			' Change the cursor.
			Cursor = Cursors.WaitCursor
			Application.DoEvents()

			' Sets the type of signature.
			settings.SignatureVisibilityType = CType(cmbVisibilityType.SelectedIndex, VisibilityType)

			' Set the signature display area.
			settings.SignatureStampBounds = New RectangleF(0.05F, 0.05F, 4.0F, 0.93F)

			' Sets the character position of the signature text
			settings.SignatureStampTextAlignment = Alignment.Left

			settings.SignatureStampFont = New Font(_resource.GetString("Font"), 9, FontStyle.Regular, GraphicsUnit.Point, 128)


			' Set the rectangle in which the text is placed in the area that displays the signature.
			'  The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
			settings.SignatureStampTextRectangle = New RectangleF(1.2F, 0F, 2.8F, 0.93F)

			' Set the signature image.
			'FileStream fs = new FileStream(Application.StartupPath + "..//..//..//Image//gc.bmp", FileMode.Open, FileAccess.Read);
			settings.SignatureStampImageFileName = Application.StartupPath + "..//..//..//Image//gc.bmp"
			'new Bitmap(Image.FromStream(fs));
			'fs.Close();

			' Set the display position of the signature image.
			settings.SignatureStampImageAlignment = Alignment.Center

			' Set the rectangle image so that it is placed in the area that displays the signature.
			' The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
			settings.SignatureStampImageRectangle = New RectangleF(0F, 0F, 1.0F, 0.93F)

			' Sets the password for the certificate and digital signature.
			' For X509Certificate2 class, etc. Please refer to the site of Microsoft.
			' 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
			' 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
			settings.SignatureCertificateFileName = Application.StartupPath + "..//..//..//GrapeCity.pfx"
			settings.SignatureCertificatePassword = "password"
			' 
			If chkTimeStamp.Checked Then
				settings.SignatureTimeStamp = New TimeStamp("https://freetsa.org/tsr", "", "")
			End If

			' Sets the time stamp.
			settings.SignatureSignDate = DateTime.Now.ToString()
			' Signing time
			settings.SignatureDistinguishedNameVisible = False
			' Display whether or not the distinguished name
			settings.SignatureContact = New SignatureField(Of String)("activereports.support@grapecity.com", True)
			' Contact
			settings.SignatureReason = _resource.GetString("ApprovalText")
			' Reason

			settings.SignatureLocation = _resource.GetString("PittsburghText")
			' Location
			Dim outputDirectory = New DirectoryInfo(Path.GetDirectoryName(sfd.FileName))
			Dim outputProvider = New Rendering.IO.FileStreamProvider(outputDirectory, sfd.FileName)
			outputProvider.OverwriteOutputFile = True

			' Export the file.
			_pageDocument.Render(pdfRE, outputProvider, settings)

			'Start the output file (Open)
			Process.Start(sfd.FileName)

			' Display the notification message.
			MessageBox.Show(My.Resources.Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As PdfSigningException
			File.Delete(tempPath)
			MessageBox.Show(My.Resources.Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		Finally
			' Replace the cursor
			Cursor = tmpCursor
			Application.DoEvents()

			' End processing
			sfd.Dispose()
		End Try
	End Sub

End Class
