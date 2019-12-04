Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text.RegularExpressions
Imports GrapeCity.ActiveReports.PageReportModel
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
Imports TextBox = GrapeCity.ActiveReports.PageReportModel.TextBox
Imports System.Globalization
Friend NotInheritable Class LayoutBuilder
	Private Class ComponentNames
		Public Const Table As String = "Table{0}"
		Public Const TableGroup As String = "Table_Group{0}"
		Public Const TableDetailGroup As String = "Table_DetailGroup{0}"
		Public Const TextBox As String = "TextBox{0}"

	End Class
	Private Shared ReadOnly nameCounts As Dictionary(Of String, Integer)
	Shared Sub New()
		nameCounts = New Dictionary(Of String, Integer)()
	End Sub
	Friend Const CRIName As String = "ContentPlaceHolder1"

	Private Const DefaultColumnWidth As String = "2cm"

	Private Const DefaultMeasurementUnits As String = "cm"

	Private Const NumericTextAlignment As String = "Right"

	Private Const DefaultTextAlignment As String = "Left"


	Public Shared Function BuildReport(ByVal state As ReportWizardState) As PageReport
		If (state) Is Nothing Then
			Throw New ArgumentNullException("state")

		End If
		Return BuildContentLayout(state)
	End Function
	Public Shared Function BuildContentLayout(ByVal state As ReportWizardState) As PageReport
		Dim def As PageReport = Nothing
		'check if Table can be created
		If state.DisplayFields.Count > 0 OrElse state.GroupingFields.Count > 0 Then
			Dim masterFile As New FileInfo("../../" + state.SelectedMasterReport.MasterReportFile)
			def = PageReport.CreateFromMaster(New Uri(masterFile.FullName))
			Dim placeHolder As CustomReportItem = CType(def.Report.Body.ReportItems(CRIName), CustomReportItem)
			Dim contentHeight As Length = placeHolder.Height
			Dim table As Table = BuildTable(state, contentHeight)
			placeHolder.ReportItems.Add(table)
		End If
		If def Is Nothing Then
			def = New PageReport()
			def.Report.Body.Height = "15cm"
			def.Report.Width = "20cm"
		End If
		Return def
	End Function
	'<summary>
	'Builds a table for the given report state, the table will use at most <paramref name="contentHeight"/> height
	'</summary>
	' <param name="state">The state of the wizard</param>
	'<param name="contentHeight">The height of the area to work within</param>
	' <returns>A <see cref="Table"/> data region which is at most <paramref name="contentHeight"/> tall</returns>
	Private Shared Function BuildTable(ByVal state As ReportWizardState, ByVal contentHeight As Length) As Table
		Dim table As New Table()
		table.Name = GetNameForComponent(ComponentNames.Table)
		Dim rowHeight As Length = CalculateRowHeight(state, contentHeight)
		CreateTableColumns(state, table)
		CreateTableGroups(state, table, rowHeight)
		Dim detailGroup As Grouping = CreateDetailGrouping(state)
		If Not (detailGroup) Is Nothing Then
			table.Details.Grouping = detailGroup
		End If
		'create Table Header, Footer and Detail sections
		Dim tableHeader As TableRow = CreateTableHeaderRow(state)
		table.Header.TableRows.Add(tableHeader)
		Dim tableFooter As TableRow = CreateTableFooter(state)
		table.Footer.TableRows.Add(tableFooter)
		Dim tableDetails As TableRow = CreateTableDetails(state)
		table.Details.TableRows.Add(tableDetails)
		tableHeader.Height = rowHeight
		tableFooter.Height = rowHeight
		tableDetails.Height = rowHeight
		table.Height = CalculateTableHeight(state, contentHeight)
		table.Width = CalculateTableWidth(state)
		Return table
	End Function
	Private Shared Function CalculateTableHeight(ByVal state As ReportWizardState, ByVal contentHeight As Length) As Length
		Const DefaultRowHeight As Double = 0.75
		'There is a header and footer for each group, plus the table header and footer, and the detail row
		Dim numberOfRows As Integer = 3 + (state.GroupingFields.Count * 2)
		Dim normalHeight As Double = (DefaultRowHeight * numberOfRows)
		If normalHeight < contentHeight.ToCentimeters() Then
			Return New Length(normalHeight.ToString(CultureInfo.InvariantCulture) + DefaultMeasurementUnits)
		End If
		Return contentHeight
	End Function
	Private Shared Function CreateTableDetails(ByVal state As ReportWizardState) As TableRow
		Dim columns As New List(Of FieldMetaData)()
		columns.AddRange(state.GroupingFields)
		columns.AddRange(state.DisplayFields)
		Dim tableDetails As New TableRow()
		For Each field As FieldMetaData In columns
			Dim tableDetailsCell As New TableCell()
			Dim tableDetailsTextBox As TextBox = CreateTextBox()
			tableDetailsTextBox.Name = LayoutBuilder.GetNameForComponent(ComponentNames.TextBox)
			If state.DisplayFields.Contains(field) Then
				'if detail grouping set, summarize summarizeable
				If Not ((state.DetailGroupingField) Is Nothing) AndAlso (field.AllowTotaling) AndAlso Not ((field).Equals(state.DetailGroupingField)) Then
					tableDetailsTextBox.Value = GetFieldSummaryExpression(field)
				Else
					tableDetailsTextBox.Value = GetFieldValueExpression(field)
				End If
				If field.IsNumeric Then
					tableDetailsTextBox.Style.TextAlign = NumericTextAlignment
				Else
					tableDetailsTextBox.Style.TextAlign = DefaultTextAlignment
				End If
				If Not String.IsNullOrEmpty(field.FormatString) Then
					tableDetailsTextBox.Style.Format = field.FormatString
				End If
			End If
			tableDetailsCell.ReportItems.Add(tableDetailsTextBox)
			tableDetails.TableCells.Add(tableDetailsCell)
		Next
		Return tableDetails
	End Function
	Private Shared Function CreateTableFooter(ByVal state As ReportWizardState) As TableRow
		Dim columns As New List(Of FieldMetaData)()
		columns.AddRange(state.GroupingFields)
		columns.AddRange(state.DisplayFields)
		Dim tableFooter As New TableRow()
		For Each field As FieldMetaData In columns
			Dim tableFooterCell As New TableCell()
			Dim tableFooterTextBox As TextBox = CreateTextBox()
			tableFooterTextBox.Name = LayoutBuilder.GetNameForComponent(ComponentNames.TextBox)
			If field.IsNumeric Then
				If state.DisplayGrandTotals AndAlso field.AllowTotaling Then
					tableFooterTextBox.Value = GetFieldSummaryExpression(field)
					If field.IsNumeric Then
						tableFooterTextBox.Style.TextAlign = NumericTextAlignment
						tableFooterTextBox.Style.ShrinkToFit = "True"
					End If
					If Not String.IsNullOrEmpty(field.FormatString) Then
						tableFooterTextBox.Style.Format = field.FormatString
					End If
					tableFooterTextBox.Style.BorderColor.Top = "LightGray"
					tableFooterTextBox.Style.BorderStyle.Top = "Double"
					tableFooterTextBox.Style.BorderWidth.Top = "3pt"
				End If
			End If
			tableFooterCell.ReportItems.Add(tableFooterTextBox)
			tableFooter.TableCells.Add(tableFooterCell)
		Next
		Return tableFooter
	End Function
	Private Shared Function CreateDetailGrouping(ByVal state As ReportWizardState) As Grouping
		Dim detailGroup As Grouping = Nothing
		If Not (state.DetailGroupingField) Is Nothing Then
			detailGroup = New Grouping()
			detailGroup.Name = GetNameForComponent(ComponentNames.TableDetailGroup)
			detailGroup.GroupExpressions.Add(GetFieldValueExpression(state.DetailGroupingField))
		End If
		Return detailGroup
	End Function
	Private Shared Function CreateTableHeaderRow(ByVal state As ReportWizardState) As TableRow
		Dim columns As New List(Of FieldMetaData)()
		columns.AddRange(state.GroupingFields)
		columns.AddRange(state.DisplayFields)
		Dim tableHeader As New TableRow()
		For Each field As FieldMetaData In columns
			Dim tableHeaderCell As New TableCell()
			Dim tableHeaderTextBox As TextBox = CreateTextBox()
			tableHeaderTextBox.Name = LayoutBuilder.GetNameForComponent(ComponentNames.TextBox)
			tableHeaderTextBox.Value = field.Title
			'set table header values according to grouping expressions
			If state.GroupingFields.Contains(field) Then
				If field.IsNumeric Then
					tableHeaderTextBox.Style.TextAlign = NumericTextAlignment
				Else
					tableHeaderTextBox.Style.TextAlign = DefaultTextAlignment
				End If
			End If
			tableHeaderCell.ReportItems.Add(tableHeaderTextBox)
			tableHeader.TableCells.Add(tableHeaderCell)
		Next
		Return tableHeader
	End Function
	Private Shared Sub CreateTableGroups(ByVal state As ReportWizardState, ByVal table As Table, ByVal rowHeight As Length)
		For Each groupField As FieldMetaData In state.GroupingFields
			Dim group As TableGroup = CreateTableGroup(state, groupField, rowHeight)
			If Not (group) Is Nothing Then
				table.TableGroups.Add(group)
				For Each row As TableRow In group.Header.TableRows
					table.Height += row.Height
				Next
				For Each row As TableRow In group.Footer.TableRows
					table.Height += row.Height
				Next
			End If
		Next
	End Sub
	Private Shared Function CalculateRowHeight(ByVal state As ReportWizardState, ByVal contentHeight As Length) As Length
		'Calculate the height of each row
		' Restriced by the height of the content placeholder
		Const DefaultRowHeight As Double = 0.75
		'There is a header and footer for each group, plus the table header and footer, and the detail row
		Dim numberOfRows As Integer = 3 + (state.GroupingFields.Count * 2)
		Dim rowHeight As Double = Math.Min(DefaultRowHeight, (contentHeight.ToCentimeters() / numberOfRows))
		Return rowHeight.ToString(CultureInfo.InvariantCulture) + DefaultMeasurementUnits
	End Function
	Private Shared Function CreateTableGroup(ByVal state As ReportWizardState, ByVal groupField As FieldMetaData, ByVal rowHeight As Length) As TableGroup
		Dim columns As New List(Of FieldMetaData)()
		columns.AddRange(state.GroupingFields)
		columns.AddRange(state.DisplayFields)
		Dim tableGroup As New TableGroup()
		Dim groupHeaderRow As New TableRow()
		Dim groupFooterRow As New TableRow()
		tableGroup.Grouping.Name = GetNameForComponent(ComponentNames.TableGroup)
		tableGroup.Grouping.GroupExpressions.Add(GetFieldValueExpression(groupField))
		groupFooterRow.Height = rowHeight
		groupHeaderRow.Height = rowHeight
		For Each columnField As FieldMetaData In columns
			Dim groupHeaderCell As New TableCell()
			Dim groupFooterCell As New TableCell()
			'set TextBoxes names avoiding duplicate names
			Dim groupHeaderTextBox As TextBox = CreateTextBox()
			groupHeaderTextBox.Name = GetNameForComponent(ComponentNames.TextBox)
			Dim groupFooterTextBox As TextBox = CreateTextBox()
			groupFooterTextBox.Name = GetNameForComponent(ComponentNames.TextBox)
			'show grouping expression value in the appropriate table cell
			If (groupField).Equals(columnField) Then
				groupHeaderTextBox.Value = GetFieldValueExpression(groupField)
				If Not String.IsNullOrEmpty(groupField.FormatString) Then
					groupHeaderTextBox.Style.Format = groupField.FormatString
				End If
				If groupField.IsNumeric Then
					groupHeaderTextBox.Style.TextAlign = NumericTextAlignment
				Else
					groupHeaderTextBox.Style.TextAlign = DefaultTextAlignment
				End If
			End If
			'add group subtotals to the table cells if needed
			If state.DisplayGroupSubtotals Then
				'We only do subtotals on the fields selected for output, not the groups
				If state.DisplayFields.Contains(columnField) Then
					If columnField.AllowTotaling Then
						groupFooterTextBox.Value = GetFieldSummaryExpression(columnField)
						groupFooterTextBox.Style.TextAlign = NumericTextAlignment
						If Not String.IsNullOrEmpty(columnField.FormatString) Then
							groupFooterTextBox.Style.Format = columnField.FormatString
						End If

						groupFooterTextBox.Style.BorderColor.Top = "LightGray"
						groupFooterTextBox.Style.BorderStyle.Top = "Solid"
						groupFooterTextBox.Style.BorderWidth.Top = "1pt"
						groupFooterTextBox.Style.ShrinkToFit = "True"

					End If
				End If
			End If
			groupHeaderCell.ReportItems.Add(groupHeaderTextBox)
			groupFooterCell.ReportItems.Add(groupFooterTextBox)
			groupHeaderRow.TableCells.Add(groupHeaderCell)
			groupFooterRow.TableCells.Add(groupFooterCell)
		Next
		tableGroup.Header.TableRows.Add(groupHeaderRow)
		tableGroup.Footer.TableRows.Add(groupFooterRow)
		Return tableGroup
	End Function
	Private Shared Sub CreateTableColumns(ByVal state As ReportWizardState, ByVal table As Table)
		If table.TableColumns.Count > 0 Then
			Throw New ArgumentException(Resources.ArgumentExceptionMessage, "table")

		End If
		Dim columns As New List(Of FieldMetaData)()
		columns.AddRange(state.GroupingFields)
		columns.AddRange(state.DisplayFields)
		For Each columnField As FieldMetaData In columns
			'create Table Columns with predefined widths
			Dim tableColumn As New TableColumn()
			If Not String.IsNullOrEmpty(columnField.PreferredWidth) Then
				tableColumn.Width = columnField.PreferredWidth
			Else
				tableColumn.Width = DefaultColumnWidth
			End If
			table.TableColumns.Add(tableColumn)
		Next
	End Sub
	Private Shared Function CalculateTableWidth(ByVal state As ReportWizardState) As Length
		Dim length As New Length("0.0cm")
		For Each field As FieldMetaData In state.GroupingFields
			length += New Length(field.PreferredWidth)
		Next
		For Each field As FieldMetaData In state.DisplayFields
			length += New Length(field.PreferredWidth)
		Next
		Return length
	End Function
	'<summary>
	'create Table Columns with predefined widths
	'</summary>
	'<returns></returns>
	Private Shared Function CreateTextBox() As TextBox
		Dim textBox As New TextBox()
		textBox.CanGrow = True
		textBox.Style.FontSize = "8pt"
		Return textBox
	End Function

	Private Shared Function GetFieldSummaryExpression(ByVal field As FieldMetaData) As String
		Dim format As String = "={0}({1})"
		Dim fieldValue As String = GetFieldValue(field)
		Return String.Format(format, field.SummaryFunction, fieldValue)
	End Function

	Private Shared Function GetFieldValue(ByVal field As FieldMetaData) As String
		Const validName As String = "^[a-zA-Z_]([a-zA-Z0-9_]*)$"
		Const itemSyntax As String = "Fields!{0}.Value"
		Const indexerSyntax As String = "Fields(""{0}"").Value"
		Dim formatString As String
		If Regex.IsMatch(field.Name, validName) Then
			formatString = itemSyntax
		Else
			formatString = indexerSyntax
		End If
		Return String.Format(formatString, field.Name)
	End Function

	Private Shared Function GetFieldValueExpression(ByVal field As FieldMetaData) As String
		Dim format As String = "={0}"
		Return String.Format(format, GetFieldValue(field))
	End Function
	Private Shared Function GetNameForComponent(ByVal baseName As String) As String
		Dim name As String = baseName
		Dim count As Integer
		If Not nameCounts.TryGetValue(baseName, count) Then
			count = 0
		End If
		count = count + 1
		name = String.Format(name, count)
		nameCounts(baseName) = count
		Return name
	End Function
End Class
