 _
Partial Class StartForm
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.Container = Nothing

Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartForm))
		Me.btnCustomers = New System.Windows.Forms.Button()
		Me.lblDescription = New System.Windows.Forms.Label()
		Me.customersOrdersGroup = New System.Windows.Forms.GroupBox()
		Me.radioEmail = New System.Windows.Forms.RadioButton()
		Me.radioID = New System.Windows.Forms.RadioButton()
		Me.radioAll = New System.Windows.Forms.RadioButton()
		Me.btnCustomersLeveled = New System.Windows.Forms.Button()
		Me.lblDescriptionLeveled = New System.Windows.Forms.Label()
		Me.customersOrdersGroup.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnCustomers
		'
		resources.ApplyResources(Me.btnCustomers, "btnCustomers")
		Me.btnCustomers.Name = "btnCustomers"
		'
		'lblDescription
		'
		resources.ApplyResources(Me.lblDescription, "lblDescription")
		Me.lblDescription.Name = "lblDescription"
		'
		'customersOrdersGroup
		'
		Me.customersOrdersGroup.Controls.Add(Me.radioEmail)
		Me.customersOrdersGroup.Controls.Add(Me.radioID)
		Me.customersOrdersGroup.Controls.Add(Me.radioAll)
		Me.customersOrdersGroup.Controls.Add(Me.lblDescription)
		Me.customersOrdersGroup.Controls.Add(Me.btnCustomers)
		resources.ApplyResources(Me.customersOrdersGroup, "customersOrdersGroup")
		Me.customersOrdersGroup.Name = "customersOrdersGroup"
		Me.customersOrdersGroup.TabStop = False
		'
		'radioEmail
		'
		resources.ApplyResources(Me.radioEmail, "radioEmail")
		Me.radioEmail.Name = "radioEmail"
		'
		'radioID
		'
		resources.ApplyResources(Me.radioID, "radioID")
		Me.radioID.Name = "radioID"
		'
		'radioAll
		'
		Me.radioAll.Checked = True
		resources.ApplyResources(Me.radioAll, "radioAll")
		Me.radioAll.Name = "radioAll"
		Me.radioAll.TabStop = True
		'
		'btnCustomersLeveled
		'
		resources.ApplyResources(Me.btnCustomersLeveled, "btnCustomersLeveled")
		Me.btnCustomersLeveled.Name = "btnCustomersLeveled"
		'
		'lblDescriptionLeveled
		'
		resources.ApplyResources(Me.lblDescriptionLeveled, "lblDescriptionLeveled")
		Me.lblDescriptionLeveled.Name = "lblDescriptionLeveled"
		'
		'StartForm
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.lblDescriptionLeveled)
		Me.Controls.Add(Me.btnCustomersLeveled)
		Me.Controls.Add(Me.customersOrdersGroup)
		Me.Name = "StartForm"
		Me.customersOrdersGroup.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents btnCustomers As System.Windows.Forms.Button
	Private radioAll As System.Windows.Forms.RadioButton
	Private radioID As System.Windows.Forms.RadioButton
	Private radioEmail As System.Windows.Forms.RadioButton
	Private lblDescription As System.Windows.Forms.Label
	Private customersOrdersGroup As System.Windows.Forms.GroupBox
Private lblDescriptionLeveled As System.Windows.Forms.Label
Private WithEvents btnCustomersLeveled As System.Windows.Forms.Button

End Class
