using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using GrapeCity.ActiveReports.Export.Pdf.Section.Signing;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using System.Resources;

namespace GrapeCity.ActiveReports.Sample.DigitalSignaturePro
{
	public partial class PDFDigitalSignature : Form
	{
		private ResourceManager _resource;
		public PDFDigitalSignature()
		{
			_resource = new ResourceManager(GetType());
			InitializeComponent();
		}

		private void PDFDigitalSignature_Load(object sender, EventArgs e)
		{
			//Set the default for in the 'Signature Format' combo box.
			cmbVisibilityType.SelectedIndex = 3;

			arvMain.LoadDocument("..//..//Report//Invoice.rpx");
		}

		private void pdfExportButton_Click(object sender, EventArgs e)
		{
			Export.Pdf.Section.PdfExport oPDFExport = new Export.Pdf.Section.PdfExport();
			SaveFileDialog sfd = new SaveFileDialog();
			Cursor tmpCursor = Cursor;
			string tempPath = string.Empty;
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
				oPDFExport.Signature.VisibilityType = (VisibilityType)cmbVisibilityType.SelectedIndex;

				// Set the signature display area.
				oPDFExport.Signature.Stamp.Bounds = new RectangleF(0.05F, 0.05F, 4.0F, 0.93F);

				// Sets the character position of the signature text
				oPDFExport.Signature.Stamp.TextAlignment = Alignment.Left;

				oPDFExport.Signature.Stamp.Font = new Font(_resource.GetString("Font"), 9, FontStyle.Regular, GraphicsUnit.Point, 128);


				// Set the rectangle in which the text is placed in the area that displays the signature.
				//  The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
				oPDFExport.Signature.Stamp.TextRectangle = new RectangleF(1.2F, 0.0F, 2.8F, 0.93F);

				// Set the signature image.
				FileStream fs = new FileStream(Application.StartupPath + "..//..//..//Image//gc.bmp", FileMode.Open, FileAccess.Read);
				oPDFExport.Signature.Stamp.Image = new Bitmap(Image.FromStream(fs));
				fs.Close();

				// Set the display position of the signature image.
				oPDFExport.Signature.Stamp.ImageAlignment = Alignment.Center;

				// Set the rectangle image so that it is placed in the area that displays the signature.
				// The coordinate specified in this property starts with the top left point, relative to the rectangular signature.
				oPDFExport.Signature.Stamp.ImageRectangle = new RectangleF(0.0f, 0.0f, 1.0f, 0.93f);

				// Sets the password for the certificate and digital signature.
				// For X509Certificate2 class, etc. Please refer to the site of Microsoft.
				// 　[X509Certificate2 クラス(System.Security.Cryptography.X509Certificates)]
				// 　http://msdn.microsoft.com/ja-jp/library/system.security.cryptography.x509certificates.x509certificate2.aspx
				oPDFExport.Signature.Certificate = new X509Certificate2(Application.StartupPath + "..//..//..//GrapeCity.pfx", "password");

				// 
				if (chkTimeStamp.Checked)
				{
					oPDFExport.Signature.TimeStamp = new TimeStamp("https://freetsa.org/tsr", "", "");
				}

				// Sets the time stamp.
				oPDFExport.Signature.SignDate = new SignatureField<DateTime>(DateTime.Now, true);		 // Signing time
				oPDFExport.Signature.DistinguishedName.Visible = false;								 // Display whether or not the distinguished name
				oPDFExport.Signature.Contact = new SignatureField<string>("activereports.support@grapecity.com", true);  // Contact
				
				oPDFExport.Signature.Reason = new SignatureField<string>(_resource.GetString("ApprovalText"), true);
				// Reason
				
				oPDFExport.Signature.Location = new SignatureField<string>("Pittsburgh", true);
				// Location
				tempPath = Path.GetTempFileName();
				// Export the file.
				oPDFExport.Export(arvMain.Document, tempPath);
				File.Delete(sfd.FileName);
				File.Move(tempPath, sfd.FileName);

				//Start the output file (Open)
				System.Diagnostics.Process.Start(sfd.FileName);

				// Display the notification message.
				MessageBox.Show(Resource.FinishExportMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (PdfSigningException)
			{
				File.Delete(tempPath);
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
				oPDFExport.Dispose();
			}
		}
	}
}
