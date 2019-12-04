using System;
using System.Windows.Forms;

namespace GrapeCity.ActiveReports.Calendar.Design
{
	/// <summary>
	/// A helper to call <see cref="Control.SuspendLayout"/> and <see cref="Control.ResumeLayout(bool)"/> with the using keyword.
	/// </summary>
	internal sealed class SuspendLayoutTransaction : IDisposable
	{
		private readonly Control _controlToSuspend;
		private readonly bool _performLayout;

		public SuspendLayoutTransaction(Control controlToSuspend) : this(controlToSuspend, false)
		{
		}

		public SuspendLayoutTransaction(Control controlToSuspend, bool performLayout)
		{
			_controlToSuspend = controlToSuspend;
			if (_controlToSuspend != null)
				_controlToSuspend.SuspendLayout();
			_performLayout = performLayout;
		}

		public void Dispose()
		{
			if (_controlToSuspend != null)
				_controlToSuspend.ResumeLayout(_performLayout);
		}
	}
}
