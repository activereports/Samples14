using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using GrapeCity.ActiveReports.Calendar.Design.Tools;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Design.DdrDesigner.ReportViewerWinForms.UI.Controls;
using GrapeCity.ActiveReports.Design.DdrDesigner.Tools;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Controls
{
	/// <summary>
	/// Represents visibility editor to use in smart panels.
	/// </summary>
	internal class VisibilityEditor : VerticalPanel
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ReportItem _reportItem;
		private readonly VisibilityWrapper _visibilityWrapper;

		private InitialVisibilityEditor _initialVisibilityEditor;
		private CheckBoxEx toggleVisibilityBox;
		private TypedValueEditor toggleItemEditor;
		private bool _isInitialized;

		public VisibilityEditor(IServiceProvider serviceProvider, ReportItem reportItem)
		{
			_serviceProvider = serviceProvider;
			_reportItem = reportItem;
			_visibilityWrapper = new VisibilityWrapper(_reportItem.Visibility, _reportItem);

			InitializeComponent();
			UpdateUI();
		}

		private void InitializeComponent()
		{
			_isInitialized = false;

			using (new SuspendLayoutTransaction(this))
			{
				AutoSize = true;

				//
				// _initialVisibilityEditor
				//
				_initialVisibilityEditor = new InitialVisibilityEditor(_serviceProvider, _visibilityWrapper);
				_initialVisibilityEditor.Margin = System.Windows.Forms.Padding.Empty;
				Controls.Add(_initialVisibilityEditor);
				//
				// toggleVisibilityBox
				//
				toggleVisibilityBox = new CheckBoxEx(_serviceProvider);
				toggleVisibilityBox.Dock = DockStyle.Top;
				toggleVisibilityBox.Text = Resources.ToggleVisibilityCaption;
				toggleVisibilityBox.CheckedChanged += toggleVisibilityBox_CheckedChanged;
				Controls.Add(toggleVisibilityBox);
				//
				// toggleItemEditor
				//
				toggleItemEditor = new TypedValueEditor(_serviceProvider,
					Resources.ToggleItemEditorCaption, _visibilityWrapper, VisibilityWrapper.ToggleItemPropertyName);
				toggleItemEditor.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, toggleItemEditor.ControlInfo, 1);
				Controls.Add(toggleItemEditor);
			}

			_isInitialized = true;
		}

		private void UpdateUI()
		{
			bool isToggleItemNotEmpty = !string.IsNullOrEmpty(_visibilityWrapper.ToggleItem);
			toggleVisibilityBox.Checked = toggleItemEditor.Enabled = isToggleItemNotEmpty;
		}

		private void toggleVisibilityBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!_isInitialized) return;

			toggleItemEditor.Enabled = toggleVisibilityBox.Checked;
			if (!toggleVisibilityBox.Checked)
				toggleItemEditor.Value = string.Empty;
		}

		#region InitialVisibilityEditor class

		/// <summary>
		/// Provides an editor for initial visibility. 
		/// </summary>
		private sealed class InitialVisibilityEditor : VerticalPanel
		{
			private readonly IServiceProvider _serviceProvider;
			private readonly VisibilityWrapper _visibilityWrapper;

			private LabelEx visibilityLabel;
			private RadioButtonEx btnVisible;
			private RadioButtonEx btnHidden;
			private RadioButtonEx btnExpression;
			private TypedValueEditor visibilityExpressionEditor;
			private bool _isInitialized;

			public InitialVisibilityEditor(IServiceProvider serviceProvider, VisibilityWrapper visibilityWrapper)
			{
				if (serviceProvider == null)
					throw new ArgumentNullException("serviceProvider");
				if (visibilityWrapper == null)
					throw new ArgumentNullException("visibilityWrapper");
				_serviceProvider = serviceProvider;
				_visibilityWrapper = visibilityWrapper;

				InitializeComponent();
				UpdateUI();
			}

			private void InitializeComponent()
			{
				_isInitialized = false;

				using (new SuspendLayoutTransaction(this))
				{
					AutoSize = true;
					//
					// visibilityLabel
					//
					visibilityLabel = new LabelEx(_serviceProvider);
					visibilityLabel.Dock = DockStyle.Top;
					visibilityLabel.Text = Resources.InitialVisibilityCaption;
					Controls.Add(visibilityLabel);
					//
					// btnVisible
					//
					btnVisible = new RadioButtonEx(_serviceProvider);
					btnVisible.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, btnVisible.ControlInfo, 1); // 1in
					btnVisible.Dock = DockStyle.Top;
					btnVisible.Text = Resources.InitialVisibilityVisibleLabel;
					btnVisible.CheckedChanged += btnVisible_CheckedChanged;
					Controls.Add(btnVisible);
					//
					// btnHidden
					//
					btnHidden = new RadioButtonEx(_serviceProvider);
					btnHidden.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, btnHidden.ControlInfo, 1); // 1in
					btnHidden.Dock = DockStyle.Top;
					btnHidden.Text = Resources.InitialVisibilityHiddenLabel;
					btnHidden.CheckedChanged += btnHidden_CheckedChanged;
					Controls.Add(btnHidden);
					//
					// btnExpression
					//
					btnExpression = new RadioButtonEx(_serviceProvider);
					btnExpression.Margin = Utils.UpdateMarginByIndent(System.Windows.Forms.Padding.Empty, btnExpression.ControlInfo, 1); // 1in
					btnExpression.Dock = DockStyle.Top;
					btnExpression.Text = Resources.InitialVisibilityExpressionLabel;
					btnExpression.CheckedChanged += btnExpression_CheckedChanged;
					Controls.Add(btnExpression);
					//
					// visibilityExpressionEditor
					//
					visibilityExpressionEditor = new TypedValueEditor(_serviceProvider,
						null, _visibilityWrapper, VisibilityWrapper.HiddenPropertyName);
					visibilityExpressionEditor.Margin = System.Windows.Forms.Padding.Empty;
					Controls.Add(visibilityExpressionEditor);
				}

				_isInitialized = true;
			}

			private void UpdateUI()
			{
				btnHidden.Checked = String.Compare(_visibilityWrapper.Hidden.Expression, Boolean.TrueString, StringComparison.OrdinalIgnoreCase) == 0;
				btnVisible.Checked = String.Compare(_visibilityWrapper.Hidden.Expression, Boolean.FalseString, StringComparison.OrdinalIgnoreCase) == 0;
				visibilityExpressionEditor.Enabled = btnExpression.Checked = !btnVisible.Checked && !btnHidden.Checked;
				if (btnExpression.Checked)
				{
					visibilityExpressionEditor.Value = _visibilityWrapper.Hidden;
				}
			}

			private void btnVisible_CheckedChanged(object sender, EventArgs e)
			{
				if (!_isInitialized) return;

				if (btnVisible.Checked)
				{
					btnHidden.Checked = btnExpression.Checked = false;
					visibilityExpressionEditor.Enabled = false;
					visibilityExpressionEditor.Value = _visibilityWrapper.Hidden = ExpressionInfo.FromString(Boolean.FalseString);
				}
			}

			private void btnHidden_CheckedChanged(object sender, EventArgs e)
			{
				if (!_isInitialized) return;

				if (btnHidden.Checked)
				{
					btnVisible.Checked = btnExpression.Checked = false;
					visibilityExpressionEditor.Enabled = false;
					visibilityExpressionEditor.Value = _visibilityWrapper.Hidden = ExpressionInfo.FromString(Boolean.TrueString);
				}
			}

			private void btnExpression_CheckedChanged(object sender, EventArgs e)
			{
				if (!_isInitialized) return;

				visibilityExpressionEditor.Enabled = btnExpression.Checked;
				if (btnExpression.Checked)
				{
					btnVisible.Checked = btnHidden.Checked = false;
					visibilityExpressionEditor.Value = _visibilityWrapper.Hidden;
				}
			}
		}

		#endregion

		#region VisibilityWrapper class

		/// <summary>
		/// Represents a wrapper for <see cref="Visibility"/> to wrap its properties by converters and editors.
		/// </summary>
		private sealed class VisibilityWrapper
		{
			public const string HiddenPropertyName = "Hidden";
			public const string ToggleItemPropertyName = "ToggleItem";

			private readonly Visibility _realVisibility;
			private readonly ReportItem _reportItem;

			public VisibilityWrapper(Visibility visibility, ReportItem reportItem)
			{
				Debug.Assert(visibility != null, "Visibility is null!");
				Debug.Assert(reportItem != null, "Report item is null!");
				_realVisibility = visibility;
				_reportItem = reportItem;
			}

			private IComponentChangeService ComponentChangeService
			{
				get
				{
					if (_componentChangeService == null)
					{
						if (_reportItem != null && _reportItem.Site != null)
						{
							_componentChangeService = (IComponentChangeService)_reportItem.Site.GetService(typeof(IComponentChangeService));
						}
						if (_componentChangeService == null)
						{
							Debug.Fail(typeof(IComponentChangeService) + " is not available.");
						}
					}
					return _componentChangeService;
				}
			}
			private IComponentChangeService _componentChangeService;

			/// <summary>
			/// Gets or sets <see cref="Visibility.Hidden"/> property of wrapped <see cref="Visibility"/> instance.
			/// </summary>
			[TypeConverter(typeof(BooleanExpressionInfoConverter))]
			[Editor(typeof(ExpressionInfoUITypeEditor), typeof(UITypeEditor))]
			public ExpressionInfo Hidden
			{
				get
				{
					return _realVisibility.Hidden;
				}
				set
				{
					if (_realVisibility.Hidden != value)
					{
						_realVisibility.Hidden = value;
						ComponentChangeService.OnComponentChanged(_reportItem, null, null, null);
					}
				}
			}

			/// <summary>
			/// Gets or sets <see cref="Visibility.ToggleItem"/> property of wrapped <see cref="Visibility"/> instance.
			/// </summary>
			[TypeConverter(typeof(ToggleItemConverter))]
			public string ToggleItem
			{
				get
				{
					return _realVisibility.ToggleItem;
				}
				// ReSharper disable UnusedMember.Local
				set
				{
					if (_realVisibility.ToggleItem != value)
					{
						_realVisibility.ToggleItem = value;
						ComponentChangeService.OnComponentChanged(_reportItem, null, null, null);
					}
				}
				// ReSharper restore UnusedMember.Local
			}
		}

		#endregion
	}
}
