using System;
using System.Drawing;
using System.Windows.Forms;

using GrapeCity.ActiveReports.Samples.Rtf.Control;
using GrapeCity.ActiveReports.Samples.Rtf.Native;

namespace GrapeCity.ActiveReports.Samples.Rtf
{
	public class RtfEditor : RichTextBoxFixed
	{
		private RtfDesigner _designer;
		private bool _isDeactivating;

		public bool IsActive { get; private set; }

		public RtfEditor(RtfDesigner designer) : base()
		{
			_designer = designer;
		}

		public void Activate()
		{
			if (!_designer.Controls.Contains(this))
			{
				_designer.Controls.Add(this);
				LostFocus += EditorLostFocus;
			}

			SyncWithSource();
			Focus();

			IsActive = true;
		}

		public void Deactivate(bool save)
		{
			if (_isDeactivating)
				return;

			_isDeactivating = true;

			if (save)
			{
				_designer.SetRtf(Rtf);
			}
			else
			{
				_designer.SetRtf(_designer.GetRtf()); // force redraw without saving

				while (CanUndo)
					Undo();
			}

			_designer.Controls.Clear();
			_designer.RePaint();
			_designer.Select();

			IsActive = false;
			_isDeactivating = false;
		}

		#region KeyDown Handlers

		public override bool PreProcessMessage(ref Message msg)
		{
			var e = CreateKeyEventArgs(msg);

			if (msg.Msg == 0x100 || msg.Msg == 0x104)
				OnKeyDown(e);

			return e.Handled || base.PreProcessMessage(ref msg);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			var command = GetEditorCommand(e.KeyData);

			if (command != null)
			{
				command();
				e.Handled = true;
			}
		}

		#endregion

		#region Helpers

		private KeyEventArgs CreateKeyEventArgs(Message msg)
		{
			var keyData = msg.WParam.ToInt32();
			var keyCode = keyData & (int)Keys.KeyCode;
			var keyModifier = 0;

			if (NativeMethods.GetKeyState(Keys.ShiftKey) < 0)
			{
				keyModifier |= (int)Keys.Shift;
			}
			if (NativeMethods.GetKeyState(Keys.ControlKey) < 0)
			{
				keyModifier |= (int)Keys.Control;
			}
			if (NativeMethods.GetKeyState(Keys.Menu) < 0)
			{
				keyModifier |= (int)Keys.Alt;
			}

			return new KeyEventArgs((Keys)(keyCode | keyModifier));
		}

		private void EditorLostFocus(object sender, EventArgs e)
		{
			LostFocus -= EditorLostFocus;
			Deactivate(true);
		}

		private void SyncWithSource()
		{
			Rtf = _designer.GetRtf();
			Size = _designer.Size;
			Location = Point.Empty;
			Margin = Padding.Empty;
			BorderStyle = BorderStyle.None;
		}

		private Action GetEditorCommand(Keys keys)
		{
			switch (keys)
			{
				case Keys.Control | Keys.A:
					return SelectAll;

				case Keys.Control | Keys.C:
					return Copy;

				case Keys.Control | Keys.X:
					return Cut;

				case Keys.Control | Keys.V:
					return Paste;

				case Keys.Control | Keys.B:
					return () => SelectionFont = new Font(SelectionFont, SelectionFont.Style ^ FontStyle.Bold);

				case Keys.Control | Keys.I:
					return () => SelectionFont = new Font(SelectionFont, SelectionFont.Style ^ FontStyle.Italic);

				case Keys.Control | Keys.U:
					return () => SelectionFont = new Font(SelectionFont, SelectionFont.Style ^ FontStyle.Underline);

				case Keys.Control | Keys.T:
					return () => SelectionFont = new Font(SelectionFont, SelectionFont.Style ^ FontStyle.Strikeout);

				case Keys.Control | Keys.Z:
					return () =>
					{
						if (CanUndo)
							Undo();
					};

				case Keys.Control | Keys.Shift | Keys.Z:
					return () =>
					{
						if (CanRedo)
							Redo();
					};

				case Keys.Delete:
					return () =>
					{
						SelectionLength = SelectionLength > 0 ? SelectionLength : 1;
						SelectedText = string.Empty;
					};

				case Keys.Escape:
					return () =>
					{
						Deactivate(save: false);
					};

				case Keys.Alt | Keys.Enter:
					return () =>
					{
						Deactivate(save: true);
					};

				default:
					return null;
			}
		}

		#endregion
	}
}
