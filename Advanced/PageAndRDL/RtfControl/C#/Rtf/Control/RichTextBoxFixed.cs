using System.Text;
using System.Windows.Forms;

using GrapeCity.ActiveReports.Samples.Rtf.Native;

namespace GrapeCity.ActiveReports.Samples.Rtf.Control
{
	public class RichTextBoxFixed : RichTextBox
	{
		public override string Text
		{
			get
			{
				int textLength = GetTextLength(NativeMethods.GTL_USECRLF);

				var getText = new NativeMethods.GETTEXTEX();
				getText.cb = textLength + 2;

				getText.flags = NativeMethods.GT_USECRLF;
				getText.codepage = 1200;

				var sb = new StringBuilder(getText.cb / 2);

				UnsafeNativeMethods.SendMessage(Handle, NativeMethods.EM_GETTEXTEX, ref getText, sb);

				return sb.ToString();
			}
			set { base.Text = value; }
		}

		public override int TextLength
		{
			get { return GetTextLength(NativeMethods.GTL_USECRLF) / 2; }
		}

		public override int SelectionLength
		{
			get
			{
				var charRange = new NativeMethods.CHARRANGE();
				UnsafeNativeMethods.SendMessage(Handle, NativeMethods.EM_EXGETSEL, 0, ref charRange);
				return charRange.cpMax - charRange.cpMin;
			}
			set { base.SelectionLength = value; }
		}

		private int GetTextLength(int gtl)
		{
			var getLength = new NativeMethods.GETTEXTLENGTHEX();
			getLength.flags = gtl;
			getLength.codepage = 1200;
			return UnsafeNativeMethods.SendMessage(Handle, NativeMethods.EM_GETTEXTLENGTHEX, ref getLength, 0);
		}
	}
}
