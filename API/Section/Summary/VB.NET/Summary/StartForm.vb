Imports System.Xml

'CalculatedFields sample shows how to create a new Calculated Field in an ActiveReport and use it with the built in summary features.
Public Class StartForm
	Inherits System.Windows.Forms.Form

	Dim rpt As New SectionReport
	Public Sub New()
		MyBase.New()

		'This call is required by the Windows Form Designer.
		InitializeComponent()
		Me.comboBox1.Items.AddRange(New String() {My.Resources.OrdersReport, My.Resources.DataFieldExpressionsReport})

	End Sub

	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	'The main entry point for the application.
	<STAThread()>
	Shared Sub Main()
		Application.Run(New StartForm())
	End Sub 'Main



	Private Sub Button_Click(sender As Object, e As EventArgs) Handles button1.Click
		Dim rpt As New SectionReport
		Dim reportPath As String = "..\\..\\" + CType(comboBox1.SelectedItem, String)
		rpt.LoadLayout(XmlReader.Create(reportPath))
		rpt.PrintWidth = 6.5!
		arvMain.LoadDocument(rpt)
	End Sub

	Private Sub comboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBox1.SelectedIndexChanged
		If (Not button1.Enabled AndAlso Not IsNothing(comboBox1.SelectedItem)) Then
			button1.Enabled = True
		End If
	End Sub

	Private Sub comboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboBox1.KeyPress
		e.Handled = True
	End Sub
End Class
