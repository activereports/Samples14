Public Class MainWindow

	Dim CurrentFileLocation As String = System.AppDomain.CurrentDomain.BaseDirectory & "..\..\..\..\Reports\"

	''' <summary>
	''' Preview Button Click- Load selected report on click 
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
		Dim _report As ComboBoxItem = DirectCast(CmbListBox.SelectedItem, ComboBoxItem)

		'If the 'Add Custom Button' checkbox is checked load custom menu of Wpf Viewer 
		If chkCustomButton.IsChecked = True Then
			Dim langDictPath As String = "DefaultWPFViewerTemplates.xaml"
			Dim langDictUri As New Uri(langDictPath, UriKind.Relative)
			ResourceTheme.Source = langDictUri
		End If

		'If the 'Add Custom Button' checkbox is not checked, clear the resource dictionary values.
		If chkCustomButton.IsChecked = False Then
			ResourceTheme.Clear()
		End If

		'Load Selected Report
		Select Case _report.Content.ToString()
			Case "Catalog.rdlx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
			Case "EmployeeSales.rdlx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
			Case "Invoice1.rdlx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
			Case "Invoice2.rpx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
			Case "LabelReport.rpx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
			Case "PaymentSlip.rpx"
				ReportViewer.LoadDocument(CurrentFileLocation + _report.Content.ToString())
				Exit Select
		End Select
	End Sub

	''' <summary>
	'''In the SelectionChanged event of the combo box, disable 'Add Custom Button' checkbox and 'Preview' button, when no report is selected.
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub CmbListBox_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles CmbListBox.SelectionChanged
		Dim _selecteditem As ComboBoxItem = DirectCast(CmbListBox.SelectedItem, ComboBoxItem)

		If CmbListBox.SelectedIndex = 0 Then
			If chkCustomButton IsNot Nothing Then
				chkCustomButton.IsEnabled = False
			End If
			btnPreview.IsEnabled = False
		Else
			chkCustomButton.IsEnabled = True
			btnPreview.IsEnabled = True
		End If
	End Sub
End Class
