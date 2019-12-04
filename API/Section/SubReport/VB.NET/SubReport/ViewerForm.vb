Imports System.Data.OleDb
Imports System.Xml

Public Class ViewerForm
	Inherits System.Windows.Forms.Form


	Public Sub New()
		MyBase.New()

		'Required for Windows Form Designer support.
		'
		InitializeComponent()
	End Sub

	' Clean up any resources being used.
	'
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub ViewerForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
		cboReport.Items.Add(My.Resources.ItemText1)
		cboReport.Items.Add(My.Resources.ItemText2)
		cboReport.Items.Add(My.Resources.ItemText3)
		cboReport.Items.Add(My.Resources.ItemText4)
		cboReport.Items.Add(My.Resources.ItemText5)
		cboReport.Items.Add(My.Resources.ItemText6)
		cboReport.Items.Add(My.Resources.ItemText7)
		cboReport.Items.Add(My.Resources.ItemText8)
		cboReport.Items.Add(My.Resources.ItemText9)

		cboReport.SelectedIndex = 0
	End Sub

	Private Sub cboReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboReport.SelectedIndexChanged
		Try
			' Change the cursor
			'
			Cursor = Cursors.WaitCursor
			Application.DoEvents()
			Select Case CType(sender, ComboBox).SelectedIndex
				Case 1  ' Simple sub-report 
					Call RunReport_Simple()
				Case 2  ' Nested sub-reports 
					Call RunReport_Nested()
				Case 3  ' Hierarchical structure of sub-reports 
					Call RunReport_Hierarchical()
				Case 4  ' Sub report using the data set that contains the relationship 
					Call RunReport_DSRelations()
				Case 5  ' Master-detail report containing a subreport 
					Call RunReport_MasterSubreport()
				Case 6  ' Bookmark in sub-report 
					Call RunReport_Bookmark()
				Case 7  ' Use a parameter in the subreport 
					Call RunReport_Parameter()
				Case 8  ' To view the DataSet with multiple tables using sub-reports 
					Call RunReport_UnboundDataSet()
			End Select
		Catch ex As Exception
			MessageBox.Show(ex.ToString)
		Finally
			' Cursor back to the original
			'
			Cursor = Cursors.Default
			Application.DoEvents()
		End Try
	End Sub

	Private Sub RunReport_Simple()
		' ***** Simple sub-report *****
		'

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptSimpleMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Nested()
		' ***** Nested sub-reports *****
		'

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptNestedParent))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Hierarchical()
		' *****Hierarchical structure of sub-reports *****
		' 

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptHierarchicalMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_DSRelations()
		' ***** Sub report using the data set that contains the relationship *****
		' 

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptDSRelationParent))
		Dim myJoinedDS As New DataSet()
		Dim cnnString As String
		Dim cnn As New OleDb.OleDbConnection()
		cnnString = My.Resources.ConnectionString
		cnn = New OleDb.OleDbConnection(cnnString)
		cnn.Open()
		Dim catAd As New OleDb.OleDbDataAdapter("Select * from categories order by categoryname", cnn)
		Dim prodAd As New OleDb.OleDbDataAdapter("Select * from products order by productname", cnn)
		Dim ODAd As New OleDb.OleDbDataAdapter("Select * from [order details]", cnn)
		catAd.Fill(myJoinedDS, "Categories")
		prodAd.Fill(myJoinedDS, "Products")
		ODAd.Fill(myJoinedDS, "OrderDetails")
		cnn.Close()
		'Sets the parent-child relationship between DataTable.
		'
		myJoinedDS.Relations.Add("CategoriesProducts", myJoinedDS.Tables("Categories").Columns("CategoryID"), myJoinedDS.Tables("Products").Columns("CategoryID"))
		myJoinedDS.Relations.Add("ProductsOrderDetails", myJoinedDS.Tables("Products").Columns("ProductID"), myJoinedDS.Tables("OrderDetails").Columns("ProductID"))
		rpt.DataSource = (myJoinedDS)
		rpt.DataMember = "Categories"
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_MasterSubreport()
		' ***** Master-detail report contains a subreport *****
		' 

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptMasterMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Bookmark()
		' ***** Bookmark in sub-report *****
		' 

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptBookmarkMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_Parameter()
		' ***** Use a parameter in the subreport *****
		' 

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptParamMain))
		arvMain.LoadDocument(rpt)

	End Sub

	Private Sub RunReport_UnboundDataSet()
		' ***** To view the DataSet with multiple tables using sub-reports *****
		' 

		'To generate dataset using "Customers" and "Orders" tables.
		'
		Dim nwindConn As OleDbConnection = New OleDbConnection(My.Resources.ConnectionString)
		Dim selectCMD As OleDbCommand = New OleDbCommand("SELECT * FROM Customers", nwindConn)
		selectCMD.CommandTimeout = 30
		Dim selectCMD2 As OleDbCommand = New OleDbCommand("SELECT * FROM Orders", nwindConn)
		selectCMD2.CommandTimeout = 30

		Dim custDA As OleDbDataAdapter = New OleDbDataAdapter()
		custDA.SelectCommand = selectCMD
		Dim ordersDA As OleDbDataAdapter = New OleDbDataAdapter()
		ordersDA.SelectCommand = selectCMD2

		Dim DS As DataSet = New DataSet()
		custDA.Fill(DS, "Customers")
		ordersDA.Fill(DS, "Orders")
		nwindConn.Close()

		Dim rpt As New SectionReport()
		rpt.LoadLayout(XmlReader.Create(My.Resources.rptUnboundDSMain))
		rpt.DataSource = DS
		rpt.DataMember = "Customers"
		arvMain.LoadDocument(rpt)
	End Sub
End Class
