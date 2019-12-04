using System;
using System.Text;
using System.Security;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GrapeCity.ActiveReports.Samples.Rtf.Native
{
	public static class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FORMATRANGE
		{
			public IntPtr hdc;
			public IntPtr hdcTarget;
			public RECT rc;
			public RECT rcPage;
			public CHARRANGE chrg;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CHARFORMATA
		{
			public int cbSize = Marshal.SizeOf(typeof(CHARFORMATA));
			public int dwMask;
			public int dwEffects;
			public int yHeight;
			public int yOffset;
			public int crTextColor;
			public byte bCharSet;
			public byte bPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
			public byte[] szFaceName = new byte[0x20];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CHARFORMATW
		{
			public int cbSize = Marshal.SizeOf(typeof(CHARFORMATW));
			public int dwMask;
			public int dwEffects;
			public int yHeight;
			public int yOffset;
			public int crTextColor;
			public byte bCharSet;
			public byte bPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x40)]
			public byte[] szFaceName = new byte[0x40];
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CHARRANGE
		{
			public int cpMin;
			public int cpMax;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct GETTEXTLENGTHEX
		{
			public int flags;
			public int codepage;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct GETTEXTEX
		{
			public int cb;
			public int flags;
			public int codepage;
			public IntPtr lpDefaultChar;
			public IntPtr lpUsedDefChar;
		}

		private const int WM_USER = 0x0400;

		public const int EM_GETTEXTEX = WM_USER + 94;

		public const int EM_EXGETSEL = WM_USER + 52;

		public const int EM_GETTEXTLENGTHEX = WM_USER + 95;

		public const int EM_GETCHARFORMAT = 0x43a;

		public const int GT_DEFAULT = 0;

		public const int GT_USECRLF = 1;

		public const int GTL_DEFAULT = 0;

		public const int GTL_USECRLF = 1;

		public const int GTL_CLOSE = 4;

		public const int CFM_BOLD = 1;

		public const int CFM_ITALIC = 2;

		public const int CFM_UNDERLINE = 4;

		public const int SCF_DEFAULT = 0;

		public const int SCF_SELECTION = 1;

		public const int CFE_BOLD = 1;

		public const int CFE_ITALIC = 2;

		public const int CFE_UNDERLINE = 4;

		public const int EM_FORMATRANGE = 0x400 + 57;

		[DllImport("user32")]
		public static extern short GetKeyState(Keys VirtKey);

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
	}

	public static class UnsafeNativeMethods
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out, MarshalAs(UnmanagedType.LPStruct)] NativeMethods.CHARFORMATA lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In, Out, MarshalAs(UnmanagedType.LPStruct)] NativeMethods.CHARFORMATW lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.CHARRANGE lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, ref NativeMethods.GETTEXTLENGTHEX wParam, int lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, ref NativeMethods.GETTEXTEX wParam, StringBuilder lParam);

		[DllImport("ole32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern IntPtr CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter, int dwClsContext, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

		[ComImport, SuppressUnmanagedCodeSecurity, Guid("2206CCB0-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IDBPromptInitialize
		{
			[PreserveSig]
			int PromptDataSource([In]IntPtr pUnkOuter, [In]IntPtr hWndParent, int dwPromptOptions, int cSourceTypeFilter, IntPtr sqSourceTypeFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string szProviderFilter, [In, MarshalAs(UnmanagedType.LPStruct)]Guid riid, out IntPtr pDataSource);
		}

		[ComImport, SuppressUnmanagedCodeSecurity, Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IDataInitialize
		{
			[PreserveSig]
			int GetDataSource([In] IntPtr pUnkOuter, [In] int dwClsCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.Interface)] out IDataInitialize ppDataSource);
			[PreserveSig]
			void GetInitializationString([In]IntPtr pDataSource, [In]sbyte fincludePassword, [MarshalAs(UnmanagedType.LPWStr)]out string ppwszInitString);
		}

		public static bool BuildConnectionString(IntPtr handle, ref string connectionString)
		{
			IntPtr dataInitializePointer;
			IntPtr dataLinksPointer = CoCreateInstance(DBO.CLSID_DataLinks, IntPtr.Zero, 0x17, DBO.IID_IDataInitialize);
			object dbPromptInitializeObject = Marshal.GetTypedObjectForIUnknown(dataLinksPointer, typeof(IDBPromptInitialize));
			IDBPromptInitialize dbPromptInitialize = (IDBPromptInitialize)dbPromptInitializeObject;
			dbPromptInitialize.PromptDataSource(IntPtr.Zero, handle, 2, 0, IntPtr.Zero, null,
				DBO.IID_IUnknown, out dataInitializePointer);
			if (dataInitializePointer != IntPtr.Zero)
			{
				IDataInitialize dataInitialize = (IDataInitialize)Marshal.GetTypedObjectForIUnknown(dataLinksPointer, typeof(IDataInitialize));
				dataInitialize.GetInitializationString(dataInitializePointer, 1, out connectionString);
				return true;
			}
			return false;
		}

		internal class DBO
		{
			internal static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
			internal static readonly Guid IID_IDataInitialize = new Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829");
			internal static readonly Guid CLSID_DataLinks = new Guid("2206CDB2-19C1-11D1-89E0-00C04FD7A829");
		}
	}
}
