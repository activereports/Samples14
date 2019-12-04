using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Samples.CustomResourceLocator.Properties;

namespace GrapeCity.ActiveReports.Samples.CustomResourceLocator
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm frm = new MainForm();
			Application.Run(frm);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			richTextBox1.Rtf = Resources.Description;
			string myPicturesPath = Environment.GetFolderPath( Environment.SpecialFolder.MyPictures);

			
			LoadImages( myPicturesPath, myPicturesPath + @"\" );
		}

		private void LoadImages( string directory, string basePath )
		{
			foreach( string dir in Directory.GetDirectories( directory ) )
			{
				LoadImages( dir, basePath );
			}

			string [] fileTypes = new string[] { "*.jpg", "*.gif", "*.bmp", "*.png" };

			foreach(string fileType in fileTypes)
			{
				foreach( string file in Directory.GetFiles( directory, fileType ) )
				{
					try
					{
						string title = file.Replace(basePath, "");
						using( Image img = Image.FromFile( file ) )
						using (Image thumbnail = GetImageThumbnail(img, imageList1.ImageSize.Width, imageList1.ImageSize.Height, Color.Transparent, VerticalAlignment.Center))
						{
							imageList1.Images.Add(title, thumbnail);
						}

						FileInfo imgInfo = new FileInfo( file );
						string ext = imgInfo.Extension;
						ext = ext.Remove(0, 1); // Remove leading'.' 

						ListViewItem item = new ListViewItem( title, title );
						item.Tag = ext;
						listView.Items.Add( item );
					}
					catch( FileNotFoundException )
					{
						//  Swallow the exception and move on
					}
					catch( OutOfMemoryException )
					{
						//  Swallow the exception and move on 
					}
				}
			}
		}

		private static Image GetImageThumbnail(Image originalImage, int imgWidth, int imgHeight, Color penColor,
											  VerticalAlignment alignment)
		{
			imgWidth = Math.Min(imgWidth, originalImage.Width);
			imgHeight = Math.Min(imgHeight, originalImage.Height);
			Bitmap retoriginalImage = new Bitmap(imgWidth, imgHeight, PixelFormat.Format64bppPArgb);
			Graphics grp = Graphics.FromImage(retoriginalImage);
			int tnWidth = imgWidth, tnHeight = imgHeight;

			//  take the original image proportions into account. 
			if (originalImage.Width > originalImage.Height)
				tnHeight = (int)(((float)originalImage.Height / (float)originalImage.Width) * tnWidth);
			else if (originalImage.Width < originalImage.Height)
				tnWidth = (int)(((float)originalImage.Width / (float)originalImage.Height) * tnHeight);


			int iLeft = 0;
			int iTop = 0;
			switch (alignment)
			{
				case VerticalAlignment.Center:
					iLeft = (imgWidth / 2) - (tnWidth / 2);
					iTop = (imgHeight / 2) - (tnHeight / 2);
					break;
				case VerticalAlignment.Bottom:
					iLeft = (imgWidth / 2) - (tnWidth / 2);
					iTop = imgHeight - tnHeight;
					break;
			}
			grp.PixelOffsetMode = PixelOffsetMode.None;
			grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grp.DrawImage(originalImage, iLeft, iTop, tnWidth, tnHeight);

			Pen pn = new Pen(penColor, 1);  
			grp.DrawRectangle(pn, 0, 0, retoriginalImage.Width - 1, retoriginalImage.Height - 1);
			grp.Dispose();
			return retoriginalImage;
		}

		private void showReport_Click(object sender, EventArgs e)
		{
			Stream reportData =
				GetType().Assembly.GetManifestResourceStream(
					"GrapeCity.ActiveReports.Samples.CustomResourceLocator.DemoReport.rdlx");
			reportData.Position = 0;
			StreamReader reader = new StreamReader(reportData);
			PageReport def = new PageReport(reader);
			def.ResourceLocator = new MyPicturesLocator();
			PageDocument runtime = new PageDocument(def);
			runtime.Parameters["PictureName"].CurrentValue = listView.SelectedItems[0].Text;
			runtime.Parameters["MimeType"].CurrentValue = string.Format( "image/{0}", listView.SelectedItems[0].Tag );

			using (PreviewForm preview = new PreviewForm(runtime))
			{
				preview.ShowDialog(this);
			}
		}

		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			showReport.Enabled = listView.SelectedItems.Count > 0;
		}
	}
}
