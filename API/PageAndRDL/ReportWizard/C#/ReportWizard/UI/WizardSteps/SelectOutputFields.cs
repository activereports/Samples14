using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class SelectOutputFields : BaseStep
	{
		private class SourcedFieldMetaData
		{
			public static readonly DataFormats.Format Format;

			static SourcedFieldMetaData()
			{
				Format = DataFormats.GetFormat(typeof( SourcedFieldMetaData ).FullName);
			}

			public FieldMetaData Field;

			public object Source;
		}

		private readonly BindingList<FieldMetaData> _availableFields;
		private readonly BindingList<FieldMetaData> _selectedFields;
		private ReportMetaData _report;

		public SelectOutputFields()
		{
			_report = null;
			_availableFields = new BindingList<FieldMetaData>();
			_selectedFields = new BindingList<FieldMetaData>();
			InitializeComponent();
		}

		protected override void OnDisplay(bool firstDisplay)
		{
			if (_report != ReportWizardState.SelectedMasterReport)
			{
				_report = ReportWizardState.SelectedMasterReport;
				LoadFields();
			}
		}

		public override void UpdateState()
		{
			ReportWizardState.DisplayFields.Clear();
			ReportWizardState.DisplayFields.AddRange(_selectedFields);
		}

		private void LoadFields()
		{
			_availableFields.Clear();
			_selectedFields.Clear();
			foreach (KeyValuePair<string, FieldMetaData> keyValuePair in _report.Fields)
			{
				_availableFields.Add(keyValuePair.Value);
			}
			if (availableOutputFields.DataSource == null)
			{
				availableOutputFields.DisplayMember = "Title";
				availableOutputFields.DataSource = _availableFields;
			}
			if (selectedOutputFields.DataSource == null)
			{
				selectedOutputFields.DisplayMember = "Title";
				selectedOutputFields.DataSource = _selectedFields;
			}
		}

		private void availableOutputFields_GetDataObject(object sender, DragDropListBox.DataObjectEventArgs e)
		{
			int index = availableOutputFields.IndexFromPoint(e.MouseLocation);
			if (index < 0)
			{
				e.DataObject = null;
				return;
			}
			SourcedFieldMetaData data = new SourcedFieldMetaData();
			data.Field = _availableFields[index];
			data.Source = availableOutputFields;
			e.DataObject = data;
		}

		private void availableOutputFields_GetDragEffects(object sender, DragDropListBox.DragDropEffectsEventArgs e)
		{
			e.DragDropEffects = DragDropEffects.Move;
		}

		private void selectedOutputFields_DragDrop(object sender, DragEventArgs e)
		{
			SourcedFieldMetaData ddoField =
					(SourcedFieldMetaData) e.Data.GetData(SourcedFieldMetaData.Format.Name);
			if (ddoField != null)
			{
				AddFieldToSelected(ddoField.Field);
			}
		}

		private void AddFieldToSelected(FieldMetaData field)
		{
			_selectedFields.Add(field);
			_availableFields.Remove(field);
		}

		private void RemoveFieldFromSelected(FieldMetaData field)
		{
			_availableFields.Add(field);
			_selectedFields.Remove(field);
		}

		private void selectedOutputFields_DragEnter(object sender, DragEventArgs e)
		{
			SourcedFieldMetaData ddoField =
					(SourcedFieldMetaData) e.Data.GetData(SourcedFieldMetaData.Format.Name);
			if (ddoField != null)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void addOutputField_Click(object sender, EventArgs e)
		{
			FieldMetaData field = availableOutputFields.SelectedItem as FieldMetaData;
			if (field != null)
			{
				AddFieldToSelected(field);
			}
		}

		private void removeOutputField_Click(object sender, EventArgs e)
		{
			FieldMetaData field = selectedOutputFields.SelectedItem as FieldMetaData;
			if (field != null)
			{
				RemoveFieldFromSelected(field);
			}
			removeOutputField.Enabled = (selectedOutputFields.SelectedIndices.Count > 0);
		}

		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			addOutputField.Enabled = (availableOutputFields.SelectedIndices.Count > 0);
			removeOutputField.Enabled = (selectedOutputFields.SelectedIndices.Count > 0);
		}
	}
}
