Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Text
Imports PdfSharp.Pdf
Imports PdfSharp.Fonts
Imports PdfSharp.Drawing
Imports GrapeCity.ActiveReports.Drawing
Imports GrapeCity.ActiveReports.Drawing.Gdi

Friend NotInheritable Class PdfFontsFactory
	Private ReadOnly _options As XPdfFontOptions
	Private Shared ReadOnly DefaultFontName As String = "MS UI Gothic"

	Private ReadOnly _fonts As IDictionary(Of FontInfo, XFont) = New Dictionary(Of FontInfo, XFont)()

	Shared Sub New()
		GlobalFontSettings.FontResolver = New GdiFontResolver()
		GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.Unicode
	End Sub

	Public Sub New(embedFonts As Boolean)
		_options = New XPdfFontOptions(PdfFontEncoding.Unicode)
	End Sub

	Public Function GetPdfFont(font As FontInfo) As XFont
		Dim resultFont As XFont = Nothing
		If _fonts.TryGetValue(font, resultFont) Then
			Return resultFont
		End If
		Try
			resultFont = New XFont(font.Family, font.Size, GetFontStyle(font), _options)
		Catch generatedExceptionName As Exception
			Try
				resultFont = New XFont(DefaultFontName, font.Size, GetFontStyle(font), _options)
			Catch generatedExceptionName2 As Exception
				resultFont = New XFont(GlobalFontSettings.DefaultFontName, font.Size, GetFontStyle(font), _options)
			End Try
		End Try
		_fonts(font) = resultFont
		Return resultFont
	End Function

	Private Shared Function GetFontStyle(fontInfo As FontInfo) As XFontStyle
		Dim resultStyle As XFontStyle = XFontStyle.Regular
		If (fontInfo.Weight And FontWeight.Bold) <> 0 Then
			resultStyle = resultStyle Or XFontStyle.Bold
		End If
		If (fontInfo.Style And Drawing.FontStyle.Italic) <> 0 Then
			resultStyle = resultStyle Or XFontStyle.Italic
		End If
		If (fontInfo.Decoration And FontDecoration.Linethrough) <> 0 Then
			resultStyle = resultStyle Or XFontStyle.Strikeout
		End If
		If (fontInfo.Decoration And FontDecoration.Underline) <> 0 Then
			resultStyle = resultStyle Or XFontStyle.Underline
		End If
		Return resultStyle
	End Function

	Private Class GdiFontResolver
		Implements IFontResolver

		Function ResolveTypeface(familyName As String, isBold As Boolean, isItalic As Boolean) As FontResolverInfo Implements IFontResolver.ResolveTypeface
			Dim style = System.Drawing.FontStyle.Regular
			If isBold Then style = style Or System.Drawing.FontStyle.Bold
			If isItalic Then style = style Or System.Drawing.FontStyle.Italic

			Try
				Using font = New Font(familyName, 80, style)
					If font.Name = "Microsoft Sans Serif" Then
						Using font2 = New Font(DefaultFontName, 80, style)
							Return GetFaceName(font2.FontFamily, isBold, isItalic)
						End Using
					End If
					Return GetFaceName(font.FontFamily, isBold, isItalic)
				End Using
			Catch
				Using font = New Font(DefaultFontName, 80, style)
					Return GetFaceName(font.FontFamily, isBold, isItalic)
				End Using
			End Try
		End Function

		Function GetFont(faceName As String) As Byte() Implements IFontResolver.GetFont
			Dim fontName = GetFontName(faceName)
			Dim fontData = GetFontBytes(fontName, GetFontStyle(faceName), &H66637474)
			If fontData Is Nothing Then Return New Byte(-1) {}
			Dim sTTCTag = GetULong(fontData, 0)
			If sTTCTag <> 1953784678 Then Return fontData
			Dim uiFontCount = CInt(GetULong(fontData, 8))

			For uiFontIndex As Integer = 0 To uiFontCount - 1
				Dim pos = CInt(GetULong(fontData, 12 + uiFontIndex * 4))
				Dim tablesCount = GetUShort(fontData, pos + 4)

				For tableIndex As Integer = 0 To tablesCount - 1
					Dim tag = GetULong(fontData, pos + 12 + tableIndex * 16)

					If tag = 1851878757 Then
						Dim offset = CInt(GetULong(fontData, pos + 20 + tableIndex * 16))
						Dim count = GetUShort(fontData, offset + 2)
						Dim stringOffset = GetUShort(fontData, offset + 4)

						For nameIndex As Integer = 0 To count - 1
							Dim platformID = GetUShort(fontData, offset + 6 + nameIndex * 12)
							Dim encodingID = GetUShort(fontData, offset + 8 + nameIndex * 12)
							Dim dataLength = GetUShort(fontData, offset + 14 + nameIndex * 12)
							Dim dataOffset = GetUShort(fontData, offset + 16 + nameIndex * 12)
							Dim nameOffset = offset + stringOffset + dataOffset
							Dim unicodeName = platformID = 0 OrElse platformID = 3 OrElse (platformID = 2 AndAlso encodingID = 1)
							Dim name = If(unicodeName, ReadUnicodeString(fontData, nameOffset, dataLength), ReadStandardString(fontData, nameOffset, dataLength))
							If name <> fontName Then Continue For
							Dim headerSize = 12 + tablesCount * 16
							Dim tablesSize = 0

							For i As Integer = 0 To tablesCount - 1
								Dim tableSize = CInt(GetULong(fontData, pos + 24 + i * 16))
								tablesSize += If(i < tablesCount - 1, (tableSize + 3) And Not 3, tableSize)
							Next

							Dim newFontData = New Byte(headerSize + tablesSize - 1) {}
							Array.Copy(fontData, pos, newFontData, 0, headerSize)
							Dim runningOffset = headerSize

							For i As Integer = 0 To tablesCount - 1
								Dim tableOffset = CInt(GetULong(fontData, pos + 20 + i * 16))
								Dim tableSize = CInt(GetULong(fontData, pos + 24 + i * 16))
								SetULong(newFontData, 20 + i * 16, CUInt(runningOffset))
								Array.Copy(fontData, tableOffset, newFontData, runningOffset, tableSize)
								runningOffset += tableSize
								If i >= tablesCount - 1 Then Continue For

								While runningOffset Mod 4 <> 0
									newFontData(runningOffset) = 0
									runningOffset += 1
								End While
							Next

							Return newFontData
						Next

						Exit For
					End If
				Next
			Next

			Return New Byte(-1) {}
		End Function

		Private Shared Function GetFontBytes(fontName As String, fontStyle As System.Drawing.FontStyle, table As Integer, Optional size As Integer = 0) As Byte()
			Using font As Font = New Font(fontName, 80, fontStyle)

				Using gTemp As Graphics = SafeGraphics.CreateReferenceGraphics()
					Dim hFont As IntPtr = font.ToHfont()
					Dim hdc As IntPtr = gTemp.GetHdc()
					Dim oldHFont As IntPtr = SelectObject(hdc, hFont)

					Try
						Dim fontDataSize = If(size = 0, GetFontDataSize(hdc, table, 0, IntPtr.Zero, 0), CUInt(size))
						If fontDataSize = 0 OrElse fontDataSize = &HFFFFFFFF Then Return Nothing
						Dim fontData = New Byte(CInt(fontDataSize - 1)) {}
						Dim result = GetFontData(hdc, table, 0, fontData, CInt(fontDataSize))
						If result = &HFFFFFFFF Then Return Nothing
						Return fontData
					Catch
						Return Nothing
					Finally
						SelectObject(hdc, oldHFont)
						DeleteObject(hFont)
						gTemp.ReleaseHdc(hdc)
					End Try
				End Using
			End Using
		End Function

		Private Shared Function GetFaceName(fontFamily As FontFamily, isBold As Boolean, isItalic As Boolean) As FontResolverInfo
			Dim simulateBold As Boolean = False
			Dim simulateItalic As Boolean = False

			If isBold AndAlso isItalic AndAlso Not fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) Then

				If fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold) Then
					simulateItalic = True
				ElseIf fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic) Then
					simulateBold = True
				Else
					simulateBold = True
					simulateItalic = True
				End If
			ElseIf isBold AndAlso Not fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold) Then
				simulateBold = True
			ElseIf isItalic AndAlso Not fontFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic) Then
				simulateItalic = True
			End If

			Dim faceName = fontFamily.GetName(1033) & GetFontStyle(isBold AndAlso Not simulateBold, isItalic AndAlso Not simulateItalic)
			Return New FontResolverInfo(faceName, simulateBold, simulateItalic)
		End Function

		Private Shared Function ReadStandardString(data As Byte(), offset As Integer, length As Integer) As String
			Return Encoding.ASCII.GetString(data, offset, length)
		End Function

		Private Shared Function ReadUnicodeString(data As Byte(), offset As Integer, length As Integer) As String
			length \= 2
			Dim buf = New StringBuilder(length)

			For k As Integer = 0 To length - 1
				Dim ch As Integer = GetUShort(data, offset)
				offset += 2
				buf.Append(ChrW(ch))
			Next

			Return buf.ToString()
		End Function

		Private Shared Sub SetULong(data As Byte(), offset As Integer, ByVal value As UInteger)
			data(offset) = CType(((value >> 24) And 255), Byte)
			data((offset + 1)) = CType(((value >> 16) And 255), Byte)
			data((offset + 2)) = CType(((value >> 8) And 255), Byte)
			data((offset + 3)) = CType((value And 255), Byte)
		End Sub

		Private Shared Function GetULong(data As Byte(), offset As Integer) As UInteger
			Return (CType(data(offset), UInteger) << 24) Or ((CType(data(offset + 1), UInteger) << 16) Or ((CType(data(offset + 2), UInteger) << 8) Or CType(data(offset + 3), UInteger)))
		End Function

		Public Shared Function GetUShort(data As Byte(), offset As Integer) As UShort
			Return (CType(data(offset), UShort) << 8) Or CType(data(offset + 1), UShort)
		End Function

		Private Shared Function GetFontStyle(isBold As Boolean, isItalic As Boolean) As String
			If isBold AndAlso isItalic Then Return " Bold Italic"
			If isBold Then Return " Bold"
			If isItalic Then Return " Italic"
			Return " Regular"
		End Function

		Private Shared Function GetFontName(faceName As String) As String
			If faceName.EndsWith(" Bold Italic") OrElse faceName.EndsWith(" Italic Bold") Then Return faceName.Substring(0, faceName.Length - 12)
			If faceName.EndsWith(" Bold") Then Return faceName.Substring(0, faceName.Length - 5)
			If faceName.EndsWith(" Italic") Then Return faceName.Substring(0, faceName.Length - 7)
			If faceName.EndsWith(" Regular") Then Return faceName.Substring(0, faceName.Length - 8)
			Return faceName
		End Function

		Private Shared Function GetFontStyle(faceName As String) As System.Drawing.FontStyle
			If faceName.EndsWith(" Bold Italic") OrElse faceName.EndsWith(" Italic Bold") Then Return System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic
			If faceName.EndsWith(" Bold") Then Return System.Drawing.FontStyle.Bold
			If faceName.EndsWith(" Italic") Then Return System.Drawing.FontStyle.Italic
			Return System.Drawing.FontStyle.Regular
		End Function


		<DllImport("gdi32.dll")>
		Private Shared Function SelectObject(ByVal hdc As IntPtr, ByVal obj As IntPtr) As IntPtr
		End Function

		<DllImport("gdi32.dll")>
		Private Shared Function DeleteObject(ByVal hObject As IntPtr) As IntPtr
		End Function

		<DllImport("gdi32.dll", EntryPoint:="GetFontData")>
		Private Shared Function GetFontDataSize(ByVal hdc As IntPtr, ByVal dwTable As Int32, ByVal dwOffset As Int32, ByVal data As IntPtr, ByVal cbData As Int32) As UInteger
		End Function

		<DllImport("gdi32.dll")>
		Private Shared Function GetFontData(ByVal hdc As IntPtr, ByVal dwTable As Int32, ByVal dwOffset As Int32, <[In], MarshalAs(UnmanagedType.LPArray)> ByVal data As Byte(), ByVal cbData As Int32) As UInteger
		End Function

	End Class
End Class
