Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Namespace UI.WizardSteps
	Partial Public Class BaseStep
		Inherits UserControl
		Private firstDisplay As Boolean
		Private stepTitle As String
		Private stepDescription As String
		Public Sub New()
			firstDisplay = True
			stepTitle = ""
			stepDescription = ""


			InitializeComponent()
		End Sub
		Private wizState As ReportWizardState
		<Browsable(False)> _
		Public Property ReportWizardState() As ReportWizardState
			Get
				Return wizState
			End Get
			Set(ByVal value As ReportWizardState)
				wizState = Value
			End Set
		End Property

		<Browsable(True)> _
		<Description("The title to display in the wizard")> _
		<Category("Appearance")> _
		<DefaultValue("")> _
		Public Property Title() As String
			Get
				Return stepTitle
			End Get
			Set(ByVal value As String)
				stepTitle = value
			End Set
		End Property

		<Browsable(True)> _
		<Description("The text to display describing what to do on this step")> _
		<Category("Appearance")> _
		<DefaultValue("")> _
		Public Property Description() As String

			Get
				Return stepDescription
			End Get
			Set(ByVal value As String)
				stepDescription = value
			End Set
		End Property
		Public Sub OnDisplay()
			OnDisplay(Me.firstDisplay)
			firstDisplay = False
		End Sub
		Protected Overridable Sub OnDisplay(ByVal firstDisplay As Boolean)
		End Sub
		'Called before the wizard moves to the previous or next step
		Public Overridable Sub UpdateState()
		End Sub
	End Class
End Namespace
