using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Samples.ReportWizard.MetaData;

namespace GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
{
	public partial class SelectGroupingFields : BaseStep
	{
		private class FieldMetaDataDragDropObject
		{
			public static readonly DataFormats.Format Format;

			private readonly FieldMetaData _data;
			private readonly object _source;
			
			static FieldMetaDataDragDropObject()
			{
				Format = DataFormats.GetFormat( typeof( FieldMetaDataDragDropObject ).FullName );
			}

			public FieldMetaDataDragDropObject(FieldMetaData data, object source)
			{
				_data = data;
				_source = source;
			}

			public FieldMetaData Data
			{
				get { return _data; }
			}
			
			public object Source
			{
				get { return _source; }
			}
		}

		private class FieldMetaDataTreeNode : TreeNode
		{
			private readonly FieldMetaData _field;

			public FieldMetaDataTreeNode(FieldMetaData field) : base(field.Title)
			{
				_field = field;
			}
			public FieldMetaData Field
			{
				get { return _field; }
			}
		}

		private ReportMetaData _selectedReport;
		private readonly List<FieldMetaData> _selectedGroupFields;
		private readonly BindingList<FieldMetaData> _availableGroupFields;

		public SelectGroupingFields()
		{
			_selectedGroupFields = new List<FieldMetaData>();
			_availableGroupFields = new BindingList<FieldMetaData>();
			InitializeComponent();
		}

		protected override void OnDisplay(bool firstDisplay)
		{
			if( firstDisplay )
			{
				SetupDataBinding();
			}
			if( _selectedReport != ReportWizardState.SelectedMasterReport )
			{
				_selectedReport = ReportWizardState.SelectedMasterReport;
				isLastGroupDetail.Checked = false;
				LoadAvailableFields();
			}
		}

		private void SetupDataBinding()
		{
			availableGroups.DataSource = _availableGroupFields;
			availableGroups.DisplayMember = "Title";
		}

		private void LoadAvailableFields()
		{
			_availableGroupFields.Clear();
			selectedGroups.Nodes.Clear();
			_selectedGroupFields.Clear();
			foreach (KeyValuePair<string, FieldMetaData> kvp in _selectedReport.Fields)
			{
				FieldMetaData field = kvp.Value;
				if (!field.AllowTotaling)
				{
					_availableGroupFields.Add( field );
				}
			}
		}

		private void selectedGroups_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent( FieldMetaDataDragDropObject.Format.Name ))
			{
				FieldMetaDataDragDropObject ddoField =
					(FieldMetaDataDragDropObject) e.Data.GetData( FieldMetaDataDragDropObject.Format.Name );
				Point localPoint = selectedGroups.PointToClient( new Point( e.X, e.Y ) );
				// Find position of the drop
				FieldMetaDataTreeNode parentNode = (FieldMetaDataTreeNode) selectedGroups.GetNodeAt( localPoint );
				if (parentNode != null)
				{
					int parentIndex = _selectedGroupFields.IndexOf( parentNode.Field );
					if(ddoField.Source == selectedGroups)
					{
						//need to remove existing node
						_selectedGroupFields.Remove(ddoField.Data);
					}
					_selectedGroupFields.Insert( parentIndex, ddoField.Data );
				}
				else // First node or not over a node
				{
					if (ddoField.Source == selectedGroups)
					{
						//need to remove existing node
						_selectedGroupFields.Remove(ddoField.Data);
					}
					_selectedGroupFields.Add( ddoField.Data );
				}
				RebuildSelectedGroupsTree();
				// Remove the old field from the list
				if (ddoField.Source == availableGroups)
					_availableGroupFields.Remove(ddoField.Data);
			}
		}

		private void RebuildSelectedGroupsTree()
		{
			selectedGroups.BeginUpdate();
			selectedGroups.Nodes.Clear();
			FieldMetaDataTreeNode parentNode = null;
			foreach (FieldMetaData fieldMetaData in _selectedGroupFields)
			{
				if (parentNode == null)
				{
					parentNode = new FieldMetaDataTreeNode(fieldMetaData);
					selectedGroups.Nodes.Add(parentNode);
				}
				else
				{
					FieldMetaDataTreeNode child = new FieldMetaDataTreeNode(fieldMetaData);
					parentNode.Nodes.Add(child);
					parentNode = child;
				}
			}
			selectedGroups.EndUpdate();
			selectedGroups.ExpandAll();
		}

		private void availableGroups_GetDataObject(object sender, DragDropListBox.DataObjectEventArgs e)
		{
			int index = availableGroups.IndexFromPoint(e.MouseLocation);
			if (index < 0)
			{
				e.DataObject = null;
				return;
			}
			FieldMetaData data = (FieldMetaData) availableGroups.Items[index];
			e.DataObject = new FieldMetaDataDragDropObject(data, availableGroups);
		}

		private void availableGroups_GetDragEffects(object sender, DragDropListBox.DragDropEffectsEventArgs e)
		{
			e.DragDropEffects = DragDropEffects.Move;
		}

		private void selectedGroups_DragEnter(object sender, DragEventArgs e)
		{
			FieldMetaDataDragDropObject ddoField =
					(FieldMetaDataDragDropObject) e.Data.GetData(FieldMetaDataDragDropObject.Format.Name);
			if (ddoField != null && ( ddoField.Source == selectedGroups || ddoField.Source == availableGroups ))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void selectedGroups_ItemDrag(object sender, ItemDragEventArgs e)
		{
			FieldMetaDataTreeNode node = e.Item as FieldMetaDataTreeNode;
			if (node != null)
			{
				selectedGroups.DoDragDrop(new FieldMetaDataDragDropObject(node.Field, selectedGroups), DragDropEffects.Move);
				Invalidate();
			}
		}

		private void addGroup_Click(object sender, EventArgs e)
		{
			FieldMetaData field = availableGroups.SelectedItem as FieldMetaData;
			if (field != null)
			{
				_selectedGroupFields.Add(field);
				_availableGroupFields.Remove(field);
				RebuildSelectedGroupsTree();
			}
		}

		private void removeGroup_Click(object sender, EventArgs e)
		{
			FieldMetaDataTreeNode selectedNode = selectedGroups.SelectedNode as FieldMetaDataTreeNode;
			if (selectedNode != null)
			{
				FieldMetaData field = selectedNode.Field;
				_selectedGroupFields.Remove(field);
				_availableGroupFields.Add(field);
				RebuildSelectedGroupsTree();
				selectedGroups.Focus();
			}
			removeGroup.Enabled = selectedGroups.SelectedNode != null;
		}

		private void selectedGroups_DragOver(object sender, DragEventArgs e)
		{
			FieldMetaDataDragDropObject ddoField =
					(FieldMetaDataDragDropObject)e.Data.GetData(FieldMetaDataDragDropObject.Format.Name);
			if (ddoField != null && e.Effect == DragDropEffects.Move)
			{
				Point currentLocation = MousePosition;
				currentLocation = selectedGroups.PointToClient(currentLocation);
				TreeNode node = selectedGroups.GetNodeAt(currentLocation);
				selectedGroups.Refresh();
				if (node != null)
				{
					Rectangle nodeBounds = node.Bounds;
					Point start = new Point(nodeBounds.Left, nodeBounds.Top+1);
					Point end = new Point(nodeBounds.Right, nodeBounds.Top + 1);
					using (Pen pen = new Pen(Color.Black, 2.0f))
					using (Graphics g = selectedGroups.CreateGraphics())
					{
						g.DrawLine(pen, start, end);
					}
				}
				else if(selectedGroups.Nodes.Count > 0)
				{
					TreeNode lastNode = selectedGroups.Nodes[selectedGroups.Nodes.Count - 1];
					while (lastNode.Nodes.Count > 0)
						lastNode = lastNode.Nodes[selectedGroups.Nodes.Count - 1];
					Rectangle nodeBounds = lastNode.Bounds;
					Point start = new Point(nodeBounds.Left, nodeBounds.Bottom);
					Point end = new Point(nodeBounds.Right, nodeBounds.Bottom);
					using (Pen pen = new Pen(Color.Black, 2.0f))
					using (Graphics g = selectedGroups.CreateGraphics())
					{
						g.DrawLine(pen, start, end);
					}
				}
			}
		}

		public override void UpdateState()
		{
			ReportWizardState.GroupingFields.Clear();
			if( _selectedGroupFields.Count > 0 )
			{
				int lastIndex = _selectedGroupFields.Count;
				if( isLastGroupDetail.Checked )
					lastIndex--;
				for( int i = 0; i < lastIndex; i++ )
				{
					ReportWizardState.GroupingFields.Add( _selectedGroupFields[i] );
				}
			}
			if( isLastGroupDetail.Checked && _selectedGroupFields.Count > 0)
			{
				ReportWizardState.DetailGroupingField = _selectedGroupFields[_selectedGroupFields.Count - 1];
			}
		}

		private void selectedGroups_DragLeave(object sender, EventArgs e)
		{
			selectedGroups.Refresh();
		}

		private void selectedGroups_AfterSelect(object sender, TreeViewEventArgs e)
		{
			removeGroup.Enabled = selectedGroups.SelectedNode != null;
		}
	}
}
