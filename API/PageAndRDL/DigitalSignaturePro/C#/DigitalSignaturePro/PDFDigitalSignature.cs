using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GrapeCity.ActiveReports.Export.Pdf.Section.Signing;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using System.Resources;
using GrapeCity.ActiveReports.Document;

namespace GrapeCity.ActiveReports.Sample.DigitalSignaturePro
{
	public partial class PDFDigitalSignature : Form
	{
		private ResourceManager _resource;
		private PageDocument _pageDocument;
		public PDFDigitalSignature()
		{
			_resource = new ResourceManager(GetType());
			InitializeComponent();
		}

		private void PDFDigitalSignature_Load(object sender, EventArgs e)
		{
			//Set the default for in the 'Signature Format' combo box.
			cmbVisibilityType.SelectedIndex = 3;
			var pageReport = new PageReport();
			_pageDocument = pageReport.Document;
			pageReport.Load(new FileInfo("..//..//..//..//Report//Catalog.rdlx"));
			arvMain.LoadDocument(_pageDocument);
		}

		private void pdfExportButton_Click(object sender, EventArgs e)
		{
			var pdfRE = new Export.Pdf.Page.PdfRenderingExtension();
			var settings = new Export.Pdf.Page.Settings();

			SaveFileDialog sfd = new SaveFileDialog();
			Cursor tmpCursor = Cursor;
			// Display the save dialog.
			
			sfd.Title = _resource.GetString("Title");//Title 
			sfd.FileName = "DigitalSignature.pdf";	  // Name of the file for initial display
			sfd.Filter = "PDF|*.pdf";		  // Filter
			if (sfd.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			try
			{
				// Change the cursor.
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();

				// Sets the type of signature.
				settings.SignatureVisibilityType = (VisibilityType)cmbVisibilityType.SelectedIndex;

				// Set the signature display area.
				settings.SignatureStampBounds = new RectangleF(0.05F, 0.05F, 4.0F, 0.93F);

				// Sets the character position of the signature text
				settings.SignatureStampTextAlignment = Alignment.Left;

				settings.SignatureStampFont = new Font(_resource.GetString("Font"), 9, FontStyle.Regular, GraphicsUnit.Point, 128);


				// Set the rectangle in which the text is placed in the area that displays the signature.
				//  The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
				settings.SignatureStampTextRectangle = new RectangleF(1.2F, 0.0F, 2.8F, 0.93F);

				// Set the signature image.
				//FileStream fs = new FileStream(Application.StartupPath + "..//..//..//Image//gc.bmp", FileMode.Open, FileAccess.Read);
				settings.SignatureStampImageFileName = Application.StartupPath + "..//..//..//Image//gc.bmp";
					//new Bitmap(Image.FromStream(fs));
				//fs.Close();

				// Set the display position of the signature image.
				settings.SignatureStampImageAlignment = Alignment.Center;

				// Set the rectangle image so that it is placed in the area that displays the signature.
				// The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
				settings.SignatureStampImageRectangle = new RectangleF(0.0f, 0.0f, 1.0f, 0.93F);

				// Sets the password for the certificate and digital signature.
				// For X509Certificate2 class, etc. Please refer to the site of Microsoft.
				// 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
				// 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
				settings.SignatureCertificateFileName = Application.StartupPath + "..//..//..//GrapeCity.pfx";
				settings.SignatureCertificatePassword = "password";
				// 
				if (chkTimeStamp.Checked)
				{
					settings.SignatureTimeStamp = new TimeStamp("https://freetsa.org/tsr", "", "");
				}

				// Sets the time stamp.
				settings.SignatureSignDate = DateTime.Now.ToString();       // Signing time
				settings.SignatureDistinguishedNameVisible  = false;                                // Display whether or not the distinguished name
				settings.SignatureContact = new SignatureField<string>("activereports.support@grapecity.com", true);  // Contact

				settings.SignatureReason = _resource.GetString("ApprovalText");
				// Reason

				settings.SignatureLocation = _resource.GetString("PittsburghText");
				// Location
				var outputDirectory = new DirectoryInfo(Path.GetDirectoryName(sfd.FileName));
				var outputProvider = new Rendering.IO.FileStreamProvider(outputDirectory, sfd.FileName);
				outputProvider.OverwriteOutputFile = true;

				// Export the file.
				_pageDocument.Render(pdfRE, outputProvider, settings);

				//Start the output file (Open)
				System.Diagnostics.Process.Start(sfd.FileName);

				// Display the notification message.
				MessageBox.Show(Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (PdfSigningException)
			{
				File.Delete(sfd.FileName);
				MessageBox.Show(Resource.LimitMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				// Replace the cursor
				Cursor = tmpCursor;
				Application.DoEvents();

				// End processing
				sfd.Dispose();
			}
		}
	}
}
