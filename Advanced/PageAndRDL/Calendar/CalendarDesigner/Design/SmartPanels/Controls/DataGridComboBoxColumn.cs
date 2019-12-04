using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// Helper class to style data grid columns.
	/// </summary>
	internal sealed class DataGridComboBoxColumn : DataGridColumnStyle
	{
		private readonly IDesignerHost _designerHost;

		/// <summary>
		/// Instance reqursion guard
		/// </summary>
		private bool reqursionGuard;
		private CurrencyManager cm;
		private int iCurrentRow;
		private readonly DataGridComboBox comboBox;
		private bool editing;

		// Constructor - create combobox, register selection change event handler,
		// register lose focus event handler
		public DataGridComboBoxColumn(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException("serviceProvider");

			_designerHost = serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			Debug.Assert(_designerHost != null, "Expected to be able to get a hold of a designer host.");

			this.cm = null;
			this.iCurrentRow = -1;
			// Create ComboBox and force DropDownList style
			this.comboBox = new DataGridComboBox(this, serviceProvider);
			this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // default style
		}

		/// <summary>
		/// Specifies the combobox control associated with this column
		/// </summary>
		public ComboBox ComboBox { get { return comboBox; } }

		static internal void AdjustColumnWidths(DataGridEx dataGrid, String tableStyleMappingName)
		{
			// just re-set column widths
			DataGridTableStyle tableStyle = dataGrid.TableStyles[tableStyleMappingName];
			if (tableStyle == null || tableStyle.GridColumnStyles.Count == 0) { return; }
			int totalWidth = 0;
			foreach (DataGridColumnStyle columnStyle in tableStyle.GridColumnStyles)
			{
				totalWidth += columnStyle.Width;
			}
			int scrollBarWidth = dataGrid.VerticalScrollBarVisible ? dataGrid.VerticalScrollBarWidth : 0;
			int delta = dataGrid.ClientSize.Width - scrollBarWidth - totalWidth - (tableStyle.RowHeadersVisible ? tableStyle.RowHeaderWidth : 0) - (tableStyle.GridColumnStyles.Count + 2) * 1;
			int columnDelta = delta / tableStyle.GridColumnStyles.Count;
			// just distribute it equally for now
			int i = 0;
			for (; i < tableStyle.GridColumnStyles.Count - 1; i++)
			{
				tableStyle.GridColumnStyles[i].Width += columnDelta;
			}
			tableStyle.GridColumnStyles[i].Width += delta - (tableStyle.GridColumnStyles.Count - 1) * columnDelta;

			int comboboxLeft = tableStyle.RowHeadersVisible ? tableStyle.RowHeaderWidth + 2 : 0;
			// move data grid combobox into correct position when vertical scroll bar visible
			if (!dataGrid.VerticalScrollBarVisible)
				return;
			foreach (DataGridColumnStyle columnStyle in tableStyle.GridColumnStyles)
			{
				if (columnStyle is DataGridComboBoxColumn)
				{
					DataGridComboBoxColumn comboboxColumn = (DataGridComboBoxColumn)columnStyle;
					if (comboboxColumn.ComboBox != null)
					{
						comboboxColumn.ComboBox.Left = comboboxLeft;
						comboboxColumn.ComboBox.Width = columnStyle.Width;
					}
				}
				comboboxLeft += columnStyle.Width;
			}
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			Flush();
			return true;
		}

		protected override void Abort(int rowNum)
		{
			this.HideCombo();
		}

		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (value != base.ReadOnly)
				{
					base.ReadOnly = value;
					Invalidate();
				}

			}
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		protected override Size GetPreferredSize(Graphics g, object value)
		{
			return new Size(100, comboBox.PreferredHeight);
		}

		protected override int GetMinimumHeight()
		{
			return comboBox.PreferredHeight;
		}

		protected override int GetPreferredHeight(Graphics g, object value)
		{
			return comboBox.PreferredHeight;
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			using (Brush bgBrush = new SolidBrush(this.DataGridTableStyle.BackColor))
			using (Brush fgBrush = new SolidBrush(this.DataGridTableStyle.ForeColor))
			{
				Paint(g, bounds, source, rowNum, bgBrush, fgBrush, alignToRight);
			}
		}

		protected override void Paint(
			Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			object value = this.GetColumnValueAtRow(source, rowNum);
			string text;
			if (value == null || value == DBNull.Value)
			{
				text = this.NullText;
			}
			else
			{
				text = value.ToString();
			}
			g.FillRectangle(backBrush, bounds);
			using (StringFormat drawFormat = new StringFormat())
			{
				drawFormat.Alignment = alignToRight ? StringAlignment.Far : StringAlignment.Near;
				drawFormat.LineAlignment = StringAlignment.Near;
				drawFormat.FormatFlags = StringFormatFlags.LineLimit;
				if (IsReadOnly(source, rowNum))
				{
					using (Brush disabledBrush = new SolidBrush(SystemColors.GrayText))
					{
						g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, disabledBrush, bounds, drawFormat);
					}
				}
				else
				{
					g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, bounds, drawFormat);
				}
			}
		}

		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			if (comboBox.Parent != null)
			{
				comboBox.Parent.Controls.Remove(comboBox);
			}
			if (value != null)
			{
				value.Controls.Add(comboBox);
			}
		}

		// ReSharper disable RedundantAssignment
		/// <summary>
		/// Called by the grid upon column cell edit
		/// </summary>
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			if (reqursionGuard) { return; }
			reqursionGuard = true;
			try
			{
				String value = Convert.ToString(GetColumnValueAtRow(source, rowNum));
				readOnly = IsReadOnly(source, rowNum);
				if (cellIsVisible && !readOnly)
				{
					// Save current row in the datagrid and currency manager associated with the data source for the datagrid
					this.iCurrentRow = rowNum;
					this.cm = source;
					if (!this.ComboBox.Visible)
					{
						this.comboBox.Bounds = bounds;
						this.comboBox.Text = value;
						// Set combo box selection to given text
						this.comboBox.SelectedIndex = this.comboBox.FindStringExact(value);
						if (this.comboBox.SelectedIndex < 0)
						{
							this.comboBox.Text = value;
						}
						ShowCombo();
					}
				}
			}
			finally
			{
				reqursionGuard = false;
			}
		}
		// ReSharper restore RedundantAssignment

		protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			return base.GetColumnValueAtRow(source, rowNum) ?? string.Empty;
		}

		protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			if (source.Position != rowNum)
			{
				Trace.TraceInformation("Invalid position in the currency manager, ignoring set value");
				return;
			}
			try
			{
				//throwing an error here will cause an unhandeled exception and kill the smart panels. Use custom events so the smart panel can responed to the invalid value by showing a user message 
				// ReSharper disable ConstantNullCoalescingCondition
				base.SetColumnValueAtRow(source, rowNum, value ?? string.Empty);
				// ReSharper restore ConstantNullCoalescingCondition
			}
			catch (Exception ex)
			{
				OnSetValueFailed(ex);
			}
		}

		// On datagrid scroll, hide the combobox
		private void DataGrid_Scroll(object sender, EventArgs e)
		{
			Flush();
		}

		/// <summary>
		/// Check to see if the selected value is the ValueExpression combobox text. '&lt;expression...&gt;'
		/// </summary>
		/// <returns></returns>
		private bool IsValueExpressionSelected()
		{
			return string.Compare(comboBox.Text, Resources.ValueExpression, true) == 0;
		}

		/// <summary>
		/// Checks to see if the selected value is the BlankValue combobox text. '&lt;blank...&gt;'
		/// </summary>
		private bool IsValueBlankSelected()
		{
			return string.Compare(comboBox.Text, Resources.ValueBlank, true) == 0;
		}

		/// <summary>
		/// Commits current value and updates the underlying textbox
		/// </summary>
		private void Flush()
		{
			if (reqursionGuard) { return; }
			reqursionGuard = true;
			try
			{
				if (iCurrentRow < 0)
				{
					Debug.Assert(!comboBox.Visible, "Invalid combo state");
					return;
				}
				SetColumnValueAtRow(cm, iCurrentRow, comboBox.Text);
				if (!editing)
				{
					iCurrentRow = -1;
				}
				HideCombo();
			}
			finally
			{
				reqursionGuard = false;
			}
		}

		/// <summary>
		/// Makes the ComboBox visible and place on top text box control
		/// </summary>
		private void ShowCombo()
		{
			this.comboBox.Show();
			// Add event handler for datagrid scroll notification
			this.DataGridTableStyle.DataGrid.Scroll += DataGrid_Scroll;
			this.comboBox.Focus();
		}

		private void HideCombo()
		{
			if (this.comboBox.Visible)
			{
				this.DataGridTableStyle.DataGrid.Scroll -= DataGrid_Scroll;
				this.comboBox.Hide();
				EndUpdate();
			}
		}

		#region Disposable pattern support

		private bool isDisposed;

		protected override void Dispose(bool disposing)
		{
			if (isDisposed) { return; }
			try
			{
				if (disposing)
				{
					if (comboBox != null)
					{
						comboBox.Dispose();
					}
				}
				isDisposed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		#endregion

		#region ReadOnly Support

		private bool IsReadOnly(CurrencyManager source, int rowNumber)
		{
			return ReadOnly || DataGridTableStyle.DataGrid.ReadOnly || OnIsReadOnlyCallback(source, rowNumber);
		}

		/// <summary>
		/// Raises the <see cref="IsReadOnlyCallback"/> event and returns a value from the handler, or false.
		/// </summary>
		private bool OnIsReadOnlyCallback(CurrencyManager source, int rowNumber)
		{
			return IsReadOnlyCallback != null && IsReadOnlyCallback(this, source, rowNumber);
		}

		/// <summary>
		/// Allows a handler to determine if the specified row is read-only.
		/// </summary>
		/// <remarks>This method is called when the column needs to determnine if the value is read-only.</remarks>
		internal event IsReadOnlyHandler IsReadOnlyCallback;

		#endregion

		#region SetValueFailed event members

		/// <summary>
		/// Event occuring whenever a value is not able to be set for the current cell.
		/// </summary>
		/// <remarks>Fix for CR# 23450: An unhandled exception was getting thrown so we handle it and execute an event that can be listend to so a message can be displayed.</remarks>
		public event SetValueFailedEventHandler SetValueFailed;

		private void OnSetValueFailed(Exception ex)
		{
			if (SetValueFailed != null)
				SetValueFailed(this, new SetValueFailedEventArgs(ex));
		}

		internal delegate void SetValueFailedEventHandler(object sender, SetValueFailedEventArgs args);

		/// <summary>
		/// Encapsulates set value failed data.
		/// </summary>
		internal sealed class SetValueFailedEventArgs : EventArgs
		{
			private readonly Exception _exception;

			/// <summary>
			/// Initializes new instance of <see cref="SetValueFailedEventArgs"/> class.
			/// </summary>
			/// <param name="exception"></param>
			public SetValueFailedEventArgs(Exception exception)
			{
				_exception = exception;
			}

			/// <summary>
			/// Gets exception.
			/// </summary>
			public Exception Exception
			{
				get { return _exception; }
			}
		}

		#endregion

		#region DataGridComboBox class

		/// <summary>
		/// Implements customized combox hosted in <see cref="DataGridComboBoxColumn"/>.
		/// </summary>
		private sealed class DataGridComboBox : ComboBoxEx
		{
			private readonly DataGridComboBoxColumn column;
			private ScheduledUpdate scheduledUpdate;

			public DataGridComboBox(DataGridComboBoxColumn column, IServiceProvider serviceProvider)
				: base(serviceProvider)
			{
				if (column == null)
				{
					throw new ArgumentNullException("Invalid data grid column");
				}
				this.Visible = false;
				this.column = column;
			}

			/// <summary>
			/// Gets/sets the current expression text for the combo control. Used to ensure the expression dialog shows the currently set expression text.
			/// </summary>
			/// <remarks>Since GetColumnValueAtRow doesn't return the current expression text if the value hasn't been applied yet so this member is used instead.</remarks>
			private string CurrentText
			{
				get
				{
					if (_currentText.Length > 0)
						return _currentText;

					return (column.cm != null && column.iCurrentRow >= 0) ? column.GetColumnValueAtRow(column.cm, column.iCurrentRow) as string : string.Empty;
				}
				set { _currentText = value; }
			}
			private string _currentText = string.Empty;

			/// <summary>
			///	On combo box losing focus, set the column value, hide the combo box, and unregister scroll event handler
			/// </summary>
			protected override void OnLeave(EventArgs e)
			{
				column.Flush();
				base.OnLeave(e);
			}

			/// <summary>
			/// Called when the text is changed in the combobox
			/// </summary>
			protected override void OnSelectedIndexChanged(EventArgs e)
			{
				if (column.reqursionGuard) { return; }
				if (!Visible) { return; }
				column.reqursionGuard = true;
				try
				{
					column.ColumnStartedEditing(this);
					string value = column.comboBox.Text;
					if (column.IsValueExpressionSelected())
					{
						value = CurrentText;
						ExpressionForm expressionForm = new ExpressionForm(column._designerHost);
						expressionForm.Expression = value;

						IUIService uiService = column._designerHost.GetService(typeof(IUIService)) as IUIService;
						if (uiService.ShowDialog(expressionForm) == DialogResult.OK)
						{
							value = expressionForm.Expression;
							CurrentText = value;
						}
						else
						{
							value = (column.cm != null && column.iCurrentRow >= 0) ?
								column.GetColumnValueAtRow(column.cm, column.iCurrentRow) as string : string.Empty;
						}
						this.Focus();
					}
					else if (column.IsValueBlankSelected())
					{
						value = string.Empty;
					}
					if (value != null)
					{
						CurrentText = value;
						ScheduledUpdate.ScheduleUpdate(this, value);
					}
				}
				finally
				{
					column.reqursionGuard = false;

				}
				base.OnSelectedIndexChanged(e);
			}

			protected override void OnTextChanged(EventArgs e)
			{
				if (column.reqursionGuard) { return; }
				if (!Visible) { return; }
				column.reqursionGuard = true;
				try
				{
					// prepare for edit being called...
					int selectionStart = this.SelectionStart;
					int selectionLength = this.SelectionLength;
					column.editing = true;
					column.ColumnStartedEditing(this);
					if (!this.Focused)
					{
						this.Focus();
						this.Select(selectionStart, selectionLength);
					}
				}
				finally
				{
					column.editing = false;
					column.reqursionGuard = false;
				}
			}

			protected override bool ProcessKeyMessage(ref Message m)
			{
				// Keep all the keys for the combo
				return ProcessKeyEventArgs(ref m);
			}

			#region Disposable pattern

			private bool isDisposed;

			protected override void Dispose(bool disposing)
			{
				if (isDisposed) { return; }
				try
				{
					if (disposing)
					{
						if (scheduledUpdate != null)
						{
							scheduledUpdate.Dispose();
							scheduledUpdate = null;
						}
					}
					isDisposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			#endregion

			#region ScheduledUpdate class

			/// <summary>
			/// Helper class to support asynch combo update.
			/// </summary>
			private sealed class ScheduledUpdate : IDisposable
			{
				private readonly DataGridComboBox combo;
				private readonly string value;
				private readonly Timer timer;

				private ScheduledUpdate(DataGridComboBox combo, string value)
				{
					this.combo = combo;
					this.value = value;
					timer = new Timer { Interval = 1 };
					timer.Tick += TickHandler;
					timer.Start();
				}

				/// <summary>
				/// Allows scheduling updates for the specified outline
				/// </summary>
				public static void ScheduleUpdate(DataGridComboBox combo, string value)
				{
					if (combo == null)
					{
						Debug.Fail("Invalid combo");
						return;
					}
					if (combo.scheduledUpdate != null)
					{
						Trace.TraceInformation("There is already scheduled update, ignoring");
						return; // just swallow it
					}
					combo.scheduledUpdate = new ScheduledUpdate(combo, value);
				}

				private void TickHandler(object sender, EventArgs e)
				{
					timer.Stop();
					if (combo == null) return;
					combo.column.reqursionGuard = true;
					try
					{
						// Reset the selection and set the specified text
						combo.SelectedIndex = -1;
						combo.Text = value;
						combo.Select(0, combo.Text.Length);
						combo.scheduledUpdate.Dispose();
						combo.scheduledUpdate = null;
					}
					finally
					{
						combo.column.reqursionGuard = false;
					}
				}

				#region IDisposable Members

				private bool isDisposed;

				public void Dispose()
				{
					if (isDisposed) { return; }
					timer.Stop();
					timer.Tick -= TickHandler;
					isDisposed = true;
				}

				#endregion
			}

			#endregion
		}

		#endregion
	}
}
