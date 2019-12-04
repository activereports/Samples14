Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Imports GrapeCity.ActiveReports.Samples.Rtf.Native

Namespace Rendering
	Public Module RtfRenderer
		Function RenderToStream(rtfContent As String, sizeInInches As SizeF) As Stream
			Dim stream = New MemoryStream()

			Using RenderImage(rtfContent, sizeInInches, Function(hdc, size) New Metafile(stream, hdc, New RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual))
			End Using

			stream.Position = 0
			Return stream
		End Function

		Function RenderMetafile(rtfContent As String, sizeInInches As SizeF) As Image
			Return RenderImage(rtfContent, sizeInInches, Function(hdc, size) New Metafile(hdc, New RectangleF(Point.Empty, size), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual))
		End Function

		Private Function RenderImage(rtfContent As String, sizeInInches As SizeF, createImage As Func(Of IntPtr, Size, Image)) As Image
			Using richText = New RichTextBox()
				richText.Rtf = rtfContent
				richText.CreateControl()

				Using g = richText.CreateGraphics()
					Dim dpiX = g.DpiX
					Dim dpiY = g.DpiY
					richText.Width = CInt((sizeInInches.Width * dpiX))
					richText.Height = CInt((sizeInInches.Height * dpiY))
					Dim hdc = g.GetHdc()
					Dim image = createImage(hdc, New Size(richText.Width, richText.Height))

					Using imageG = Graphics.FromImage(image)
						Dim formatRange = New FORMATRANGE()
						formatRange.hdcTarget = imageG.GetHdc()
						formatRange.hdc = formatRange.hdcTarget
						formatRange.rcPage.right = CInt(Math.Ceiling(sizeInInches.Width * 1440))
						formatRange.rc.right = formatRange.rcPage.right
						formatRange.rcPage.bottom = CInt(Math.Ceiling(sizeInInches.Height * 1440))
						formatRange.rc.bottom = formatRange.rcPage.bottom
						formatRange.chrg.cpMin = 0
						formatRange.chrg.cpMax = -1
						Dim lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(formatRange))
						Marshal.StructureToPtr(formatRange, lParam, False)
						NativeMethods.SendMessage(richText.Handle, EM_FORMATRANGE, 1, lParam)
						Marshal.FreeCoTaskMem(lParam)
						imageG.ReleaseHdc(formatRange.hdc)
					End Using

					g.ReleaseHdc(hdc)
					Return image
				End Using
			End Using
		End Function
	End Module
End NameSpace