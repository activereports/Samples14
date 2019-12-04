 _
Partial Class BindIListToDataGridSample
	Inherits System.Windows.Forms.Form



Private components As System.ComponentModel.IContainer

Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BindIListToDataGridSample))
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.btnGenerateReport = New System.Windows.Forms.Button()
		Me.customDataLbl = New System.Windows.Forms.Label()
		Me.dataGridView = New System.Windows.Forms.DataGridView()
		Me.SupplierIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DiscontinuedDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
		Me.CategoryIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ReorderLevelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ProductNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.ProductIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.QuantityPerUnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.UnitsInStockDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.UnitsOnOrderDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.UnitPriceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.productCollection = New GrapeCity.ActiveReports.Samples.IListBinding.DataLayer.ProductCollection()
		CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btnGenerateReport
		'
		resources.ApplyResources(Me.btnGenerateReport, "btnGenerateReport")
		Me.btnGenerateReport.Name = "btnGenerateReport"
		'
		'customDataLbl
		'
		resources.ApplyResources(Me.customDataLbl, "customDataLbl")
		Me.customDataLbl.Name = "customDataLbl"
		Me.customDataLbl.AutoSize = True
		'
		'dataGridView
		'
		DataGridViewCellStyle6.Font = New System.Drawing.Font("MS PGothic", 10.0!)
		Me.dataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
		resources.ApplyResources(Me.dataGridView, "dataGridView")
		Me.dataGridView.AutoGenerateColumns = False
		DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
		Me.dataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SupplierIDDataGridViewTextBoxColumn, Me.DiscontinuedDataGridViewCheckBoxColumn, Me.CategoryIDDataGridViewTextBoxColumn, Me.ReorderLevelDataGridViewTextBoxColumn, Me.ProductNameDataGridViewTextBoxColumn, Me.ProductIDDataGridViewTextBoxColumn, Me.QuantityPerUnitDataGridViewTextBoxColumn, Me.UnitsInStockDataGridViewTextBoxColumn, Me.UnitsOnOrderDataGridViewTextBoxColumn, Me.UnitPriceDataGridViewTextBoxColumn})
		Me.dataGridView.DataSource = Me.productCollection
		Me.dataGridView.Name = "dataGridView"
		DataGridViewCellStyle10.Font = New System.Drawing.Font("MS PGothic", 10.0!)
		Me.dataGridView.RowsDefaultCellStyle = DataGridViewCellStyle10
		'
		'SupplierIDDataGridViewTextBoxColumn
		'
		Me.SupplierIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.SupplierIDDataGridViewTextBoxColumn.DataPropertyName = "SupplierID"
		DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SupplierIDDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle8
		Me.SupplierIDDataGridViewTextBoxColumn.FillWeight = 200.0!
		resources.ApplyResources(Me.SupplierIDDataGridViewTextBoxColumn, "SupplierIDDataGridViewTextBoxColumn")
		Me.SupplierIDDataGridViewTextBoxColumn.Name = "SupplierIDDataGridViewTextBoxColumn"
		'
		'DiscontinuedDataGridViewCheckBoxColumn
		'
		Me.DiscontinuedDataGridViewCheckBoxColumn.DataPropertyName = "Discontinued"
		resources.ApplyResources(Me.DiscontinuedDataGridViewCheckBoxColumn, "DiscontinuedDataGridViewCheckBoxColumn")
		Me.DiscontinuedDataGridViewCheckBoxColumn.Name = "DiscontinuedDataGridViewCheckBoxColumn"
		'
		'CategoryIDDataGridViewTextBoxColumn
		'
		Me.CategoryIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.CategoryIDDataGridViewTextBoxColumn.DataPropertyName = "CategoryID"
		resources.ApplyResources(Me.CategoryIDDataGridViewTextBoxColumn, "CategoryIDDataGridViewTextBoxColumn")
		Me.CategoryIDDataGridViewTextBoxColumn.Name = "CategoryIDDataGridViewTextBoxColumn"
		'
		'ReorderLevelDataGridViewTextBoxColumn
		'
		Me.ReorderLevelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.ReorderLevelDataGridViewTextBoxColumn.DataPropertyName = "ReorderLevel"
		DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ReorderLevelDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle9
		resources.ApplyResources(Me.ReorderLevelDataGridViewTextBoxColumn, "ReorderLevelDataGridViewTextBoxColumn")
		Me.ReorderLevelDataGridViewTextBoxColumn.Name = "ReorderLevelDataGridViewTextBoxColumn"
		'
		'ProductNameDataGridViewTextBoxColumn
		'
		Me.ProductNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.ProductNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName"
		resources.ApplyResources(Me.ProductNameDataGridViewTextBoxColumn, "ProductNameDataGridViewTextBoxColumn")
		Me.ProductNameDataGridViewTextBoxColumn.Name = "ProductNameDataGridViewTextBoxColumn"
		'
		'ProductIDDataGridViewTextBoxColumn
		'
		Me.ProductIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.ProductIDDataGridViewTextBoxColumn.DataPropertyName = "ProductID"
		resources.ApplyResources(Me.ProductIDDataGridViewTextBoxColumn, "ProductIDDataGridViewTextBoxColumn")
		Me.ProductIDDataGridViewTextBoxColumn.Name = "ProductIDDataGridViewTextBoxColumn"
		'
		'QuantityPerUnitDataGridViewTextBoxColumn
		'
		Me.QuantityPerUnitDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.QuantityPerUnitDataGridViewTextBoxColumn.DataPropertyName = "QuantityPerUnit"
		resources.ApplyResources(Me.QuantityPerUnitDataGridViewTextBoxColumn, "QuantityPerUnitDataGridViewTextBoxColumn")
		Me.QuantityPerUnitDataGridViewTextBoxColumn.Name = "QuantityPerUnitDataGridViewTextBoxColumn"
		'
		'UnitsInStockDataGridViewTextBoxColumn
		'
		Me.UnitsInStockDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.UnitsInStockDataGridViewTextBoxColumn.DataPropertyName = "UnitsInStock"
		resources.ApplyResources(Me.UnitsInStockDataGridViewTextBoxColumn, "UnitsInStockDataGridViewTextBoxColumn")
		Me.UnitsInStockDataGridViewTextBoxColumn.Name = "UnitsInStockDataGridViewTextBoxColumn"
		'
		'UnitsOnOrderDataGridViewTextBoxColumn
		'
		Me.UnitsOnOrderDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.UnitsOnOrderDataGridViewTextBoxColumn.DataPropertyName = "UnitsOnOrder"
		resources.ApplyResources(Me.UnitsOnOrderDataGridViewTextBoxColumn, "UnitsOnOrderDataGridViewTextBoxColumn")
		Me.UnitsOnOrderDataGridViewTextBoxColumn.Name = "UnitsOnOrderDataGridViewTextBoxColumn"
		'
		'UnitPriceDataGridViewTextBoxColumn
		'
		Me.UnitPriceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		Me.UnitPriceDataGridViewTextBoxColumn.DataPropertyName = "UnitPrice"
		resources.ApplyResources(Me.UnitPriceDataGridViewTextBoxColumn, "UnitPriceDataGridViewTextBoxColumn")
		Me.UnitPriceDataGridViewTextBoxColumn.Name = "UnitPriceDataGridViewTextBoxColumn"
		'
		'productCollection
		'
		Me.productCollection.Capacity = 128
		'
		'BindIListToDataGridSample
		'
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.dataGridView)
		Me.Controls.Add(Me.customDataLbl)
		Me.Controls.Add(Me.btnGenerateReport)
		Me.Name = "BindIListToDataGridSample"
		CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Private productCollection As IListBinding.DataLayer.ProductCollection
	Private WithEvents btnGenerateReport As System.Windows.Forms.Button
	Private customDataLbl As System.Windows.Forms.Label
	Friend WithEvents dataGridView As System.Windows.Forms.DataGridView
Friend WithEvents SupplierIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents DiscontinuedDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
Friend WithEvents CategoryIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents ReorderLevelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents ProductNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents ProductIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents QuantityPerUnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents UnitsInStockDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents UnitsOnOrderDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
Friend WithEvents UnitPriceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
