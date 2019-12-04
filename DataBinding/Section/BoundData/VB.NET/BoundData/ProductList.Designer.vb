<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class ProductList 
	Inherits GrapeCity.ActiveReports.SectionReport

	'Form overrides dispose to clean up the component list.
	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	'NOTE: The following procedure is required by the ActiveReports Designer
	'It can be modified using the ActiveReports Designer.
	'Do not modify it using the code editor.
	Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
	Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
	Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductList))
		Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
		Me.Shape1 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
		Me.LabelProductList = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.LabelProductName = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.LabelUnitPrice = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.LabelUnitsOnOrder = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.LabelQuantityPerUnit = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.LabelProductsList = New GrapeCity.ActiveReports.SectionReportModel.Label()
		Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
		Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
		Me.textBoxProductName = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.textBoxQuantityPerUnit = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.textBoxUnitPrice = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.textBoxUnitsOnOrder = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
		Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
		CType(Me.LabelProductList, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.LabelProductName, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.LabelUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.LabelUnitsOnOrder, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.LabelQuantityPerUnit, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.LabelProductsList, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.textBoxProductName, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.textBoxQuantityPerUnit, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.textBoxUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.textBoxUnitsOnOrder, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'PageHeader
		'
		Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Shape1, Me.LabelProductList, Me.LabelProductName, Me.LabelUnitPrice, Me.LabelUnitsOnOrder, Me.LabelQuantityPerUnit, Me.LabelProductsList, Me.Line1})
		Me.PageHeader.Height = 1.385!
		Me.PageHeader.Name = "PageHeader"
		'
		'Shape1
		'
		Me.Shape1.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(96, Byte), Integer))
		Me.Shape1.Height = 0.823!
		Me.Shape1.Left = 0.083!
		Me.Shape1.LineColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
		Me.Shape1.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Transparent
		Me.Shape1.Name = "Shape1"
		Me.Shape1.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(50.0!, Nothing, Nothing, Nothing, Nothing)
		Me.Shape1.Style = GrapeCity.ActiveReports.SectionReportModel.ShapeType.RoundRect
		Me.Shape1.Top = 0.073!
		Me.Shape1.Width = 6.115!
		' 
		' labelProductList
		' 
		resources.ApplyResources(Me.LabelProductList, "LabelProductList")
		Me.LabelProductList.Height = 0.387!
		Me.LabelProductList.HyperLink = Nothing
		Me.LabelProductList.Left = 0.428!
		Me.LabelProductList.Name = "LabelProductList"
		Me.LabelProductList.Style = "font-family: Arial Narrow; font-size: 24pt"
		Me.LabelProductList.Top = 0.281!
		Me.LabelProductList.Width = 2.042!
		'
		'LabelProductName
		'
		resources.ApplyResources(Me.LabelProductName, "LabelProductName")
		Me.LabelProductName.Height = 0.2!
		Me.LabelProductName.HyperLink = Nothing
		Me.LabelProductName.Left = 0.177!
		Me.LabelProductName.Name = "LabelProductName"
		Me.LabelProductName.Style = ""
		Me.LabelProductName.Top = 1.031!
		Me.LabelProductName.Width = 1.771!
		'
		'LabelUnitPrice
		'
		resources.ApplyResources(Me.LabelUnitPrice, "LabelUnitPrice")
		Me.LabelUnitPrice.Height = 0.2!
		Me.LabelUnitPrice.HyperLink = Nothing
		Me.LabelUnitPrice.Left = 3.51!
		Me.LabelUnitPrice.Name = "LabelUnitPrice"
		Me.LabelUnitPrice.Style = ""
		Me.LabelUnitPrice.Top = 1.031!
		Me.LabelUnitPrice.Width = 1.344!
		'
		'LabelUnitsOnOrder
		'
		resources.ApplyResources(Me.LabelUnitsOnOrder, "LabelUnitsOnOrder")
		Me.LabelUnitsOnOrder.Height = 0.2!
		Me.LabelUnitsOnOrder.HyperLink = Nothing
		Me.LabelUnitsOnOrder.Left = 4.854!
		Me.LabelUnitsOnOrder.Name = "LabelUnitsOnOrder"
		Me.LabelUnitsOnOrder.Style = ""
		Me.LabelUnitsOnOrder.Top = 1.031!
		Me.LabelUnitsOnOrder.Width = 1.344!
		'
		'LabelQuantityPerUnit
		'
		resources.ApplyResources(Me.LabelQuantityPerUnit, "LabelQuantityPerUnit")
		Me.LabelQuantityPerUnit.Height = 0.2!
		Me.LabelQuantityPerUnit.HyperLink = Nothing
		Me.LabelQuantityPerUnit.Left = 1.948!
		Me.LabelQuantityPerUnit.Name = "LabelQuantityPerUnit"
		Me.LabelQuantityPerUnit.Style = ""
		Me.LabelQuantityPerUnit.Top = 1.031!
		Me.LabelQuantityPerUnit.Width = 1.562!
		'
		'LabelProductsList
		'
		resources.ApplyResources(Me.LabelProductsList, "LabelProductsList")
		Me.LabelProductsList.Height = 0.398!
		Me.LabelProductsList.HyperLink = Nothing
		Me.LabelProductsList.Left = 0.428!
		Me.LabelProductsList.Name = "LabelProductsList"
		Me.LabelProductsList.Style = "font-family: Arial Narrow; font-size: 24pt; ddo-char-set: 1"
		Me.LabelProductsList.Top = 0.281!
		Me.LabelProductsList.Width = 2.042!
		'
		'Line1
		'
		Me.Line1.Height = 0.0!
		Me.Line1.Left = 0.198!
		Me.Line1.LineWeight = 1.0!
		Me.Line1.Name = "Line1"
		Me.Line1.Top = 1.302083!
		Me.Line1.Width = 6.0!
		Me.Line1.X1 = 0.198!
		Me.Line1.X2 = 6.198!
		Me.Line1.Y1 = 1.302083!
		Me.Line1.Y2 = 1.302083!
		'
		'Detail
		'
		Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.textBoxProductName, Me.textBoxQuantityPerUnit, Me.textBoxUnitPrice, Me.textBoxUnitsOnOrder})
		Me.Detail.Height = 0.1979166!
		Me.Detail.Name = "Detail"
		'
		'textBoxProductName
		'
		Me.textBoxProductName.DataField = "ProductName"
		Me.textBoxProductName.Height = 0.198!
		Me.textBoxProductName.Left = 0.177!
		Me.textBoxProductName.Name = "textBoxProductName"
		Me.textBoxProductName.Text = "ProductName"
		Me.textBoxProductName.Top = 0.0!
		Me.textBoxProductName.Width = 1.771!
		'
		'textBoxQuantityPerUnit
		'
		Me.textBoxQuantityPerUnit.DataField = "QuantityPerUnit"
		Me.textBoxQuantityPerUnit.Height = 0.198!
		Me.textBoxQuantityPerUnit.Left = 1.948!
		Me.textBoxQuantityPerUnit.Name = "textBoxQuantityPerUnit"
		Me.textBoxQuantityPerUnit.Text = "QuantityPerUnit"
		Me.textBoxQuantityPerUnit.Top = 0.0!
		Me.textBoxQuantityPerUnit.Width = 1.562!
		'
		'textBoxUnitPrice
		'
		Me.textBoxUnitPrice.DataField = "UnitPrice"
		Me.textBoxUnitPrice.Height = 0.198!
		Me.textBoxUnitPrice.Left = 3.51!
		Me.textBoxUnitPrice.Name = "textBoxUnitPrice"
		Me.textBoxUnitPrice.Text = "UnitPrice"
		Me.textBoxUnitPrice.Top = 0.0!
		Me.textBoxUnitPrice.Width = 1.344!
		'
		'textBoxUnitsOnOrder
		'
		Me.textBoxUnitsOnOrder.DataField = "UnitsOnOrder"
		Me.textBoxUnitsOnOrder.Height = 0.198!
		Me.textBoxUnitsOnOrder.Left = 4.854!
		Me.textBoxUnitsOnOrder.Name = "textBoxUnitsOnOrder"
		Me.textBoxUnitsOnOrder.Style = "ddo-shrink-to-fit: none"
		Me.textBoxUnitsOnOrder.Text = "UnitsOnOrder"
		Me.textBoxUnitsOnOrder.Top = 0.0!
		Me.textBoxUnitsOnOrder.Width = 1.344!
		'
		'PageFooter
		'
		Me.PageFooter.Height = 0.0!
		Me.PageFooter.Name = "PageFooter"
		'
		'ProductList
		'
		Me.MasterReport = False
		Me.PageSettings.PaperHeight = 11.0!
		Me.PageSettings.PaperWidth = 8.5!
		Me.PrintWidth = 6.271584!
		Me.Sections.Add(Me.PageHeader)
		Me.Sections.Add(Me.Detail)
		Me.Sections.Add(Me.PageFooter)
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
			"l; font-size: 10pt; color: Black; ddo-char-set: 204", "Normal"))
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"))
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
			"lic", "Heading2", "Normal"))
		Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"))
		CType(Me.LabelProductList, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LabelProductName, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LabelUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LabelUnitsOnOrder, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LabelQuantityPerUnit, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LabelProductsList, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.textBoxProductName, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.textBoxQuantityPerUnit, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.textBoxUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.textBoxUnitsOnOrder, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
	Private WithEvents Shape1 As GrapeCity.ActiveReports.SectionReportModel.Shape
	Private WithEvents LabelProductList As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents LabelProductName As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents LabelUnitPrice As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents LabelUnitsOnOrder As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents LabelQuantityPerUnit As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents LabelProductsList As GrapeCity.ActiveReports.SectionReportModel.Label
	Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
	Private WithEvents textBoxProductName As GrapeCity.ActiveReports.SectionReportModel.TextBox
	Private WithEvents textBoxQuantityPerUnit As GrapeCity.ActiveReports.SectionReportModel.TextBox
	Private WithEvents textBoxUnitPrice As GrapeCity.ActiveReports.SectionReportModel.TextBox
	Private WithEvents textBoxUnitsOnOrder As GrapeCity.ActiveReports.SectionReportModel.TextBox
End Class
