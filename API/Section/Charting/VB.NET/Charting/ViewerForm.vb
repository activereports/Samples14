Imports System.Xml
Imports GrapeCity.ActiveReports.SectionReportModel

Public Class ViewerForm
	Inherits System.Windows.Forms.Form

	Public Sub New()
		MyBase.New()

		'Required designer variable. 
		InitializeComponent()

		' TODO: InitializeComponent After a call, it is a constructor. Please add a code.
		'
	End Sub

	' Required for Windows Form Designer support
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		cboStyle.Items.Add(My.Resources.TwoDBarChart)
		cboStyle.Items.Add(My.Resources.ThreeDPieChart)
		cboStyle.Items.Add(My.Resources.ThreeDBarChart)
		cboStyle.Items.Add(My.Resources.FinanceChart)
		cboStyle.Items.Add(My.Resources.StackedAreaChart)

		' Sets the state of the initial selection of the combo box "chart type".
		cboStyle.SelectedIndex = 0
	End Sub

	Private Sub cboList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboStyle.SelectedIndexChanged
		' Enable the custom property combo only when you select "line graph".
		Call SetCustomProperties(Me.cboStyle.SelectedIndex)
	End Sub

	Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnReport.Click

		Dim rpt As New SectionReport()
		Try
			' Display the preview according to the "chart type" combobox.
			Select Case cboStyle.SelectedIndex
				Case 0  '2D bar chart 
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt2DBar))
				Case 1  '3D pie chart 
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt3DPie))
					If cboCustom.SelectedIndex = 0 Then
						DirectCast(rpt.Sections("Detail").Controls("ChartSalesCategories"), ChartControl).Series(0).Properties("Clockwise") = True
					Else
						DirectCast(rpt.Sections("Detail").Controls("ChartSalesCategories"), ChartControl).Series(0).Properties("Clockwise") = False
					End If
				Case 2  ' 3D bar chart 
					rpt.LoadLayout(XmlReader.Create(My.Resources.rpt3DBar))
				Case 3  ' Finance chart 
					rpt.LoadLayout(XmlReader.Create(My.Resources.rptCandle))
				Case 4  ' Stacked area chart 
					rpt.LoadLayout(XmlReader.Create(My.Resources.rptStackedArea))
			End Select
			arvMain.LoadDocument(DirectCast(rpt, SectionReport))
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		End Try

	End Sub

	Private Sub SetCustomProperties(ByVal iGraphStype As Integer)
		' To clear the selected candidate.
		cboCustom.Items.Clear()

		' Add a selection candidate and set it in the selected state.
		Select Case iGraphStype
			Case 1    ' 2D pie chart 
				' Change the Visible State
				'
				cboCustom.Visible = True
				lblCustom.Visible = True

				cboCustom.Items.Add(My.Resources.Clockwise)
				cboCustom.Items.Add(My.Resources.Counterclockwise)

				cboCustom.SelectedIndex = 1

				'To set a label
				'
				lblCustom.Text = My.Resources.DirectionOfRotation
			Case Else   ' Other 
				'To make invisible
				cboCustom.Visible = False
				lblCustom.Visible = False
		End Select

		Application.DoEvents()
	End Sub

End Class
