using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.Drawing.Gdi;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using FontStyle = GrapeCity.ActiveReports.Drawing.FontStyle;

namespace GrapeCity.ActiveReports.Samples.Export.Rendering.PdfSharp
{
	internal sealed class PdfFontsFactory
	{
		private const string DefaultFontName = "MS UI Gothic";
		
		private readonly XPdfFontOptions _options;

		private readonly IDictionary<FontInfo, XFont> _fonts = new Dictionary<FontInfo, XFont>();

		static PdfFontsFactory()
		{
			GlobalFontSettings.FontResolver = new GdiFontResolver();
			GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.Unicode;
		}
		
		public PdfFontsFactory(bool embedFonts)
		{
			_options = new XPdfFontOptions(PdfFontEncoding.Unicode);
		}

		public XFont GetPdfFont(FontInfo font)
		{
			XFont resultFont;
			if (_fonts.TryGetValue(font, out resultFont))
				return resultFont;
			try
			{
				resultFont = new XFont(font.Family, font.Size, GetFontStyle(font), _options);
			}
			catch (Exception)
			{
				try
				{
					resultFont = new XFont(DefaultFontName, font.Size, GetFontStyle(font), _options);
				}
				catch (Exception)
				{
					resultFont = new XFont(GlobalFontSettings.DefaultFontName, font.Size, GetFontStyle(font), _options);
				}
			}
			_fonts[font] = resultFont;
			return resultFont;
		}

		private static XFontStyle GetFontStyle(FontInfo fontInfo)
		{
			XFontStyle resultStyle = XFontStyle.Regular;
			if ((fontInfo.Weight & FontWeight.Bold) != 0)
				resultStyle |= XFontStyle.Bold;
			if ((fontInfo.Style & FontStyle.Italic) != 0)
				resultStyle |= XFontStyle.Italic;
			if ((fontInfo.Decoration & FontDecoration.Linethrough) != 0)
				resultStyle |= XFontStyle.Strikeout;
			if ((fontInfo.Decoration & FontDecoration.Underline) != 0)
				resultStyle |= XFontStyle.Underline;
			return resultStyle;
		}
		
		private class GdiFontResolver : IFontResolver
		{
			public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
			{
				var style = System.Drawing.FontStyle.Regular;
				if (isBold) style |= System.Drawing.FontStyle.Bold;
				if (isItalic) style |= System.Drawing.FontStyle.Italic;
				try
				{
					using (var font = new Font(familyName, 80, style))
					{
						if (font.Name == "Microsoft Sans Serif")
							using (var font2 = new Font(DefaultFontName, 80, style))
								return GetFaceName(font2.FontFamily, isBold, isItalic);
						return GetFaceName(font.FontFamily, isBold, isItalic);
					}
				}
				catch
				{
					using (var font = new Font(DefaultFontName, 80, style))
						return GetFaceName(font.FontFamily, isBold, isItalic);
				}
			}
			
			public byte[] GetFont(string faceName)
			{
				var fontName = GetFontName(faceName);
				var fontData = GetFontBytes(fontName, GetFontStyle(faceName), 0x66637474);
				if (fontData == null) return new byte[0];
				
				// https://docs.microsoft.com/en-us/typography/opentype/spec/otff#collections
				var sTTCTag = GetULong(fontData, 0);
				if (sTTCTag != GetULong(new []{ (byte)'t', (byte)'t', (byte)'c', (byte)'f' }, 0))
					return fontData;
				
				var uiFontCount = GetULong(fontData, 8);
				for (int uiFontIndex = 0; uiFontIndex < uiFontCount; uiFontIndex++)
				{
					// https://docs.microsoft.com/en-us/typography/opentype/spec/otff
					var pos = (int)GetULong(fontData, 12 + uiFontIndex * 4);
					var tablesCount = GetUShort(fontData, pos + 4);
					for (int tableIndex = 0; tableIndex < tablesCount; tableIndex++)
					{
						var tag = GetULong(fontData, pos + 12 + tableIndex * 16);
						// https://docs.microsoft.com/en-us/typography/opentype/spec/name
						if (tag == GetULong(new []{ (byte)'n', (byte)'a', (byte)'m', (byte)'e' }, 0))
						{
							var offset = (int)GetULong(fontData, pos + 20 + tableIndex * 16);
							var count = GetUShort(fontData, offset + 2);
							var stringOffset = GetUShort(fontData, offset + 4);
							for (int nameIndex = 0; nameIndex < count; nameIndex++)
							{
								var platformID = GetUShort(fontData, offset + 6 + nameIndex * 12);
								var encodingID = GetUShort(fontData, offset + 8 + nameIndex * 12);
								var dataLength = GetUShort(fontData, offset + 14 + nameIndex * 12);
								var dataOffset = GetUShort(fontData, offset + 16 + nameIndex * 12);

								var nameOffset = offset + stringOffset + dataOffset;
								var unicodeName = platformID == 0 || platformID == 3 || (platformID == 2 && encodingID == 1);
								var name = unicodeName ? ReadUnicodeString(fontData, nameOffset, dataLength) : ReadStandardString(fontData, nameOffset, dataLength);
								if (name != fontName) continue;
								
								// rebuild font and fix offsets
								// https://docs.microsoft.com/en-us/typography/opentype/spec/otff
								var headerSize = 12 + tablesCount * 16;
								var tablesSize = 0;
								for (int i = 0; i < tablesCount; i++)
								{
									var tableSize = (int)GetULong(fontData, pos + 24 + i * 16);
									tablesSize += i < tablesCount - 1 ? (tableSize + 3) & ~3 : tableSize;
								}
								var newFontData = new byte[headerSize + tablesSize];
								Array.Copy(fontData, pos, newFontData, 0, headerSize);
								var runningOffset = headerSize;
								for (int i = 0; i < tablesCount; i++)
								{
									var tableOffset = (int)GetULong(fontData, pos + 20 + i * 16);
									var tableSize = (int)GetULong(fontData, pos + 24 + i * 16);
									SetULong(newFontData, 20 + i * 16, (uint)runningOffset);
									Array.Copy(fontData, tableOffset, newFontData, runningOffset, tableSize);
									runningOffset += tableSize;
									if (i >= tablesCount - 1) continue;
									while (runningOffset % 4 != 0)
									{
										newFontData[runningOffset] = 0;
										runningOffset++;
									}
								}
								return newFontData;
							}
							break;
						}
					}
				}
				return new byte[0];
			}

			private static byte[] GetFontBytes(string fontName, System.Drawing.FontStyle fontStyle, int table, int size = 0)
			{
				using (Font font = new Font(fontName, 80, fontStyle))
				using (Graphics gTemp = SafeGraphics.CreateReferenceGraphics())
				{
					IntPtr hFont = font.ToHfont();
					IntPtr hdc = gTemp.GetHdc();
					IntPtr oldHFont = SelectObject(hdc, hFont);

					try
					{
						var fontDataSize = size == 0 ? GetFontDataSize(hdc, table, 0, IntPtr.Zero, 0) : (uint)size;
						if (fontDataSize == 0 || fontDataSize == 0xFFFFFFFF) return null;
						var fontData = new byte[fontDataSize];
						var result = GetFontData(hdc, table, 0, fontData, (int) fontDataSize);
						if (result == 0xFFFFFFFF) return null;
						return fontData;
					}
					catch
					{
						return null;
					}
					finally
					{
						SelectObject(hdc, oldHFont);
						DeleteObject(hFont);
						gTemp.ReleaseHdc(hdc);
					}
				}
			}
			
			private static FontResolverInfo GetFaceName(FontFamily fontFamily, bool isBold, bool isItalic)
			{
				bool simulateBold = false;
				bool simulateItalic = false;
				if (isBold && isItalic && !fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))
				{
					if (fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold))
						simulateItalic = true;
					else if (fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic))
						simulateBold = true;
					else
						simulateBold = simulateItalic = true;
				}
				else if (isBold && !fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold))
					simulateBold = true;
				else if (isItalic && !fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic))
					simulateItalic = true;

				var faceName = fontFamily.GetName(1033) + GetFontStyle(isBold && !simulateBold, isItalic && !simulateItalic);
				return new FontResolverInfo(faceName, simulateBold, simulateItalic);
			}
			
			private static string ReadStandardString(byte[] data, int offset, int length)
			{
				return Encoding.ASCII.GetString(data, offset, length);
			}

			private static string ReadUnicodeString(byte[] data, int offset, int length)
			{
				length /= 2;
				var buf = new StringBuilder(length);
				for (int k = 0; k < length; ++k)
				{
					int ch = (data[offset] << 8) + data[offset + 1];
					offset += 2;
					buf.Append((char)ch);
				}
				return buf.ToString();
			}
			
			private static void SetULong(byte[] data, int offset, uint value)
			{
				data[offset] = (byte)((value >> 24) & 0xFF);
				data[offset + 1] = (byte)((value >> 16) & 0xFF);
				data[offset + 2] = (byte)((value >> 8) & 0xFF);
				data[offset + 3] = (byte)((value) & 0xFF);
			}
			
			private static uint GetULong(byte[] data, int offset)
			{
				return (uint) ((data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | (data[offset + 3]));
			}

			public static ushort GetUShort(byte[] data, int offset)
			{
				return (ushort)((data[offset] << 8) | (data[offset + 1]));
			}
			
			private static string GetFontStyle(bool isBold, bool isItalic)
			{
				if (isBold && isItalic) return " Bold Italic";
				if (isBold) return " Bold";
				if (isItalic) return " Italic";
				return " Regular";
			}
			
			private static string GetFontName(string faceName)
			{
				if (faceName.EndsWith(" Bold Italic") || faceName.EndsWith(" Italic Bold")) return faceName.Substring(0, faceName.Length - 12);
				if (faceName.EndsWith(" Bold")) return faceName.Substring(0, faceName.Length - 5);
				if (faceName.EndsWith(" Italic")) return faceName.Substring(0, faceName.Length - 7);
				if (faceName.EndsWith(" Regular")) return faceName.Substring(0, faceName.Length - 8);
				return faceName;
			}
			
			private static System.Drawing.FontStyle GetFontStyle(string faceName)
			{
				if (faceName.EndsWith(" Bold Italic") || faceName.EndsWith(" Italic Bold")) return System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic;
				if (faceName.EndsWith(" Bold")) return System.Drawing.FontStyle.Bold;
				if (faceName.EndsWith(" Italic")) return System.Drawing.FontStyle.Italic;
				return System.Drawing.FontStyle.Regular;
			}
			
			[DllImport("gdi32.dll")]
			private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
		
			[DllImport("gdi32.dll")]
			private static extern IntPtr DeleteObject(IntPtr hObject);

			[DllImport("gdi32.dll", EntryPoint="GetFontData")]
			private static extern uint GetFontDataSize(IntPtr hdc, Int32 dwTable, Int32 dwOffset, IntPtr data, Int32 cbData);
			
			[DllImport("gdi32.dll")]
			private static extern uint GetFontData(IntPtr hdc, Int32 dwTable, Int32 dwOffset, [In, MarshalAs(UnmanagedType.LPArray)] byte[] data, Int32 cbData);
		}
	}
}
