using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace GrapeCity.ActiveReports.Samples.IListBinding
{
	public partial class BindIListToDataGridSample
	{

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BindIListToDataGridSample));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.SupplierIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DiscontinuedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.CategoryIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ReorderLevelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProductNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ProductIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.QuantityPerUnitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UnitsInStockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UnitsOnOrderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UnitPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._productCollection = new GrapeCity.ActiveReports.Samples.IListBinding.DataLayer.ProductCollection();
			this.customDataLbl = new System.Windows.Forms.Label();
			this.btnGenerateReport = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS PGothic", 10F);
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			resources.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.AutoGenerateColumns = false;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SupplierIDDataGridViewTextBoxColumn,
            this.DiscontinuedDataGridViewCheckBoxColumn,
            this.CategoryIDDataGridViewTextBoxColumn,
            this.ReorderLevelDataGridViewTextBoxColumn,
            this.ProductNameDataGridViewTextBoxColumn,
            this.ProductIDDataGridViewTextBoxColumn,
            this.QuantityPerUnitDataGridViewTextBoxColumn,
            this.UnitsInStockDataGridViewTextBoxColumn,
            this.UnitsOnOrderDataGridViewTextBoxColumn,
            this.UnitPriceDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this._productCollection;
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle5.Font = new System.Drawing.Font("MS PGothic", 10F);
			this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
			// 
			// SupplierIDDataGridViewTextBoxColumn
			// 
			this.SupplierIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.SupplierIDDataGridViewTextBoxColumn.DataPropertyName = "SupplierID";
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SupplierIDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.SupplierIDDataGridViewTextBoxColumn.FillWeight = 200F;
			resources.ApplyResources(this.SupplierIDDataGridViewTextBoxColumn, "SupplierIDDataGridViewTextBoxColumn");
			this.SupplierIDDataGridViewTextBoxColumn.Name = "SupplierIDDataGridViewTextBoxColumn";
			// 
			// DiscontinuedDataGridViewCheckBoxColumn
			// 
			this.DiscontinuedDataGridViewCheckBoxColumn.DataPropertyName = "Discontinued";
			resources.ApplyResources(this.DiscontinuedDataGridViewCheckBoxColumn, "DiscontinuedDataGridViewCheckBoxColumn");
			this.DiscontinuedDataGridViewCheckBoxColumn.Name = "DiscontinuedDataGridViewCheckBoxColumn";
			// 
			// CategoryIDDataGridViewTextBoxColumn
			// 
			this.CategoryIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.CategoryIDDataGridViewTextBoxColumn.DataPropertyName = "CategoryID";
			resources.ApplyResources(this.CategoryIDDataGridViewTextBoxColumn, "CategoryIDDataGridViewTextBoxColumn");
			this.CategoryIDDataGridViewTextBoxColumn.Name = "CategoryIDDataGridViewTextBoxColumn";
			// 
			// ReorderLevelDataGridViewTextBoxColumn
			// 
			this.ReorderLevelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ReorderLevelDataGridViewTextBoxColumn.DataPropertyName = "ReorderLevel";
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ReorderLevelDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
			resources.ApplyResources(this.ReorderLevelDataGridViewTextBoxColumn, "ReorderLevelDataGridViewTextBoxColumn");
			this.ReorderLevelDataGridViewTextBoxColumn.Name = "ReorderLevelDataGridViewTextBoxColumn";
			// 
			// ProductNameDataGridViewTextBoxColumn
			// 
			this.ProductNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ProductNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName";
			resources.ApplyResources(this.ProductNameDataGridViewTextBoxColumn, "ProductNameDataGridViewTextBoxColumn");
			this.ProductNameDataGridViewTextBoxColumn.Name = "ProductNameDataGridViewTextBoxColumn";
			// 
			// ProductIDDataGridViewTextBoxColumn
			// 
			this.ProductIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ProductIDDataGridViewTextBoxColumn.DataPropertyName = "ProductID";
			resources.ApplyResources(this.ProductIDDataGridViewTextBoxColumn, "ProductIDDataGridViewTextBoxColumn");
			this.ProductIDDataGridViewTextBoxColumn.Name = "ProductIDDataGridViewTextBoxColumn";
			// 
			// QuantityPerUnitDataGridViewTextBoxColumn
			// 
			this.QuantityPerUnitDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.QuantityPerUnitDataGridViewTextBoxColumn.DataPropertyName = "QuantityPerUnit";
			resources.ApplyResources(this.QuantityPerUnitDataGridViewTextBoxColumn, "QuantityPerUnitDataGridViewTextBoxColumn");
			this.QuantityPerUnitDataGridViewTextBoxColumn.Name = "QuantityPerUnitDataGridViewTextBoxColumn";
			// 
			// UnitsInStockDataGridViewTextBoxColumn
			// 
			this.UnitsInStockDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.UnitsInStockDataGridViewTextBoxColumn.DataPropertyName = "UnitsInStock";
			resources.ApplyResources(this.UnitsInStockDataGridViewTextBoxColumn, "UnitsInStockDataGridViewTextBoxColumn");
			this.UnitsInStockDataGridViewTextBoxColumn.Name = "UnitsInStockDataGridViewTextBoxColumn";
			// 
			// UnitsOnOrderDataGridViewTextBoxColumn
			// 
			this.UnitsOnOrderDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.UnitsOnOrderDataGridViewTextBoxColumn.DataPropertyName = "UnitsOnOrder";
			resources.ApplyResources(this.UnitsOnOrderDataGridViewTextBoxColumn, "UnitsOnOrderDataGridViewTextBoxColumn");
			this.UnitsOnOrderDataGridViewTextBoxColumn.Name = "UnitsOnOrderDataGridViewTextBoxColumn";
			// 
			// UnitPriceDataGridViewTextBoxColumn
			// 
			this.UnitPriceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.UnitPriceDataGridViewTextBoxColumn.DataPropertyName = "UnitPrice";
			resources.ApplyResources(this.UnitPriceDataGridViewTextBoxColumn, "UnitPriceDataGridViewTextBoxColumn");
			this.UnitPriceDataGridViewTextBoxColumn.Name = "UnitPriceDataGridViewTextBoxColumn";
			// 
			// _productCollection
			// 
			this._productCollection.Capacity = 128;
			// 
			// customDataLbl
			// 
			resources.ApplyResources(this.customDataLbl, "customDataLbl");
			this.customDataLbl.Name = "customDataLbl";
			this.customDataLbl.AutoSize = true;
			// 
			// btnGenerateReport
			// 
			resources.ApplyResources(this.btnGenerateReport, "btnGenerateReport");
			this.btnGenerateReport.Name = "btnGenerateReport";
			this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
			// 
			// BindIListToDataGridSample
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.customDataLbl);
			this.Controls.Add(this.btnGenerateReport);
			this.Name = "BindIListToDataGridSample";
			this.Load += new System.EventHandler(this.BindIListToDataGridSample_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		internal DataGridView dataGridView1;
		internal DataGridViewTextBoxColumn SupplierIDDataGridViewTextBoxColumn;
		internal DataGridViewCheckBoxColumn DiscontinuedDataGridViewCheckBoxColumn;
		internal DataGridViewTextBoxColumn CategoryIDDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn ReorderLevelDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn ProductNameDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn ProductIDDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn QuantityPerUnitDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn UnitsInStockDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn UnitsOnOrderDataGridViewTextBoxColumn;
		internal DataGridViewTextBoxColumn UnitPriceDataGridViewTextBoxColumn;
		private Label customDataLbl;
		private Button btnGenerateReport;
	}
}
