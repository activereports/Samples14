Imports System.Text
Imports System.Security
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace Native
	Public Module NativeMethods
		<StructLayout(LayoutKind.Sequential)>
		Public Structure RECT
			Public left As Int32
			Public top As Int32
			Public right As Int32
			Public bottom As Int32
		End Structure

		<StructLayout(LayoutKind.Sequential)>
		Public Structure FORMATRANGE
			Public hdc As IntPtr
			Public hdcTarget As IntPtr
			Public rc As RECT
			Public rcPage As RECT
			Public chrg As CHARRANGE
		End Structure

		<StructLayout(LayoutKind.Sequential, Pack:=4)>
		Public Class CHARFORMATA
			Public cbSize As Integer = Marshal.SizeOf(GetType(CHARFORMATA))
			Public dwMask As Integer
			Public dwEffects As Integer
			Public yHeight As Integer
			Public yOffset As Integer
			Public crTextColor As Integer
			Public bCharSet As Byte
			Public bPitchAndFamily As Byte
			<MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H20)>
			Public szFaceName As Byte() = New Byte(31) {}
		End Class

		<StructLayout(LayoutKind.Sequential, Pack:=4)>
		Public Class CHARFORMATW
			Public cbSize As Integer = Marshal.SizeOf(GetType(CHARFORMATW))
			Public dwMask As Integer
			Public dwEffects As Integer
			Public yHeight As Integer
			Public yOffset As Integer
			Public crTextColor As Integer
			Public bCharSet As Byte
			Public bPitchAndFamily As Byte
			<MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H40)>
			Public szFaceName As Byte() = New Byte(63) {}
		End Class

		<StructLayout(LayoutKind.Sequential)>
		Public Structure CHARRANGE
			Public cpMin As Integer
			Public cpMax As Integer
		End Structure

		<StructLayout(LayoutKind.Sequential)>
		Public Structure GETTEXTLENGTHEX
			Public flags As Integer
			Public codepage As Integer
		End Structure

		<StructLayout(LayoutKind.Sequential)>
		Public Structure GETTEXTEX
			Public cb As Integer
			Public flags As Integer
			Public codepage As Integer
			Public lpDefaultChar As IntPtr
			Public lpUsedDefChar As IntPtr
		End Structure

		Private Const WM_USER As Integer = &H400
		Public Const EM_GETTEXTEX As Integer = WM_USER + 94
		Public Const EM_EXGETSEL As Integer = WM_USER + 52
		Public Const EM_GETTEXTLENGTHEX As Integer = WM_USER + 95
		Public Const EM_GETCHARFORMAT As Integer = &H43A
		Public Const GT_DEFAULT As Integer = 0
		Public Const GT_USECRLF As Integer = 1
		Public Const GTL_DEFAULT As Integer = 0
		Public Const GTL_USECRLF As Integer = 1
		Public Const GTL_CLOSE As Integer = 4
		Public Const CFM_BOLD As Integer = 1
		Public Const CFM_ITALIC As Integer = 2
		Public Const CFM_UNDERLINE As Integer = 4
		Public Const SCF_DEFAULT As Integer = 0
		Public Const SCF_SELECTION As Integer = 1
		Public Const CFE_BOLD As Integer = 1
		Public Const CFE_ITALIC As Integer = 2
		Public Const CFE_UNDERLINE As Integer = 4
		Public Const EM_FORMATRANGE As Integer = &H400 + 57

		<DllImport("user32")>
		Public Function GetKeyState(ByVal VirtKey As Keys) As Short
		End Function

		<DllImport("user32.dll")>
		Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As Int32, ByVal lParam As IntPtr) As Int32
		End Function
	End Module

	Public Module UnsafeNativeMethods
		<DllImport("user32.dll", CharSet:=CharSet.Auto)>
		Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer,
		                     <[In], Out, MarshalAs(UnmanagedType.LPStruct)> ByVal lParam As CHARFORMATA) As IntPtr
		End Function

		<DllImport("user32.dll", CharSet:=CharSet.Auto)>
		Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer,
		                     <[In], Out, MarshalAs(UnmanagedType.LPStruct)> ByVal lParam As CHARFORMATW) As IntPtr
		End Function

		<DllImport("user32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
		Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByRef lParam As CHARRANGE) As Integer
		End Function

		<DllImport("user32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
		Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByRef wParam As GETTEXTLENGTHEX, ByVal lParam As Integer) As Integer
		End Function

		<DllImport("user32.dll", EntryPoint:="SendMessage", CharSet:=CharSet.Auto)>
		Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByRef wParam As GETTEXTEX, ByVal lParam As StringBuilder) As Integer
		End Function

		<DllImport("ole32.dll", CharSet:=CharSet.Unicode, PreserveSig:=False)>
		Private Function CoCreateInstance(
		                                  <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal rclsid As Guid, ByVal pUnkOuter As IntPtr, ByVal dwClsContext As Integer,
		                                  <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal riid As Guid) As IntPtr
		End Function

		<ComImport, SuppressUnmanagedCodeSecurity, Guid("2206CCB0-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
		Friend Interface IDBPromptInitialize
			<PreserveSig>
			Function PromptDataSource(
			                          <[In]> ByVal pUnkOuter As IntPtr,
			                          <[In]> ByVal hWndParent As IntPtr, ByVal dwPromptOptions As Integer, ByVal cSourceTypeFilter As Integer, ByVal sqSourceTypeFilter As IntPtr,
			                          <[In], MarshalAs(UnmanagedType.LPWStr)> ByVal szProviderFilter As String,
			                          <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal riid As Guid, <Out> ByRef pDataSource As IntPtr) As Integer
		End Interface

		<ComImport, SuppressUnmanagedCodeSecurity, Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
		Friend Interface IDataInitialize
			<PreserveSig>
			Function GetDataSource(
			                       <[In]> ByVal pUnkOuter As IntPtr,
			                       <[In]> ByVal dwClsCtx As Integer,
			                       <[In], MarshalAs(UnmanagedType.LPWStr)> ByVal pwszInitializationString As String,
			                       <[In], MarshalAs(UnmanagedType.LPStruct)> ByVal riid As Guid, <Out>
			                       <MarshalAs(UnmanagedType.[Interface])> ByRef ppDataSource As IDataInitialize) As Integer
			<PreserveSig>
			Sub GetInitializationString(
			                            <[In]> ByVal pDataSource As IntPtr,
			                            <[In]> ByVal fincludePassword As SByte, <Out>
			                            <MarshalAs(UnmanagedType.LPWStr)> ByRef ppwszInitString As String)
		End Interface

		Function BuildConnectionString(ByVal handle As IntPtr, ByRef connectionString As String) As Boolean
			Dim dataInitializePointer As IntPtr
			Dim dataLinksPointer As IntPtr = CoCreateInstance(DBO.CLSID_DataLinks, IntPtr.Zero, &H17, DBO.IID_IDataInitialize)
			Dim dbPromptInitializeObject As Object = Marshal.GetTypedObjectForIUnknown(dataLinksPointer, GetType(IDBPromptInitialize))
			Dim dbPromptInitialize As IDBPromptInitialize = CType(dbPromptInitializeObject, IDBPromptInitialize)
			dbPromptInitialize.PromptDataSource(IntPtr.Zero, handle, 2, 0, IntPtr.Zero, Nothing, DBO.IID_IUnknown, dataInitializePointer)

			If dataInitializePointer <> IntPtr.Zero Then
				Dim dataInitialize As IDataInitialize = CType(Marshal.GetTypedObjectForIUnknown(dataLinksPointer, GetType(IDataInitialize)), IDataInitialize)
				dataInitialize.GetInitializationString(dataInitializePointer, 1, connectionString)
				Return True
			End If

			Return False
		End Function

		Friend Class DBO
			Friend Shared ReadOnly IID_IUnknown As Guid = New Guid("00000000-0000-0000-C000-000000000046")
			Friend Shared ReadOnly IID_IDataInitialize As Guid = New Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")
			Friend Shared ReadOnly CLSID_DataLinks As Guid = New Guid("2206CDB2-19C1-11D1-89E0-00C04FD7A829")
		End Class
	End Module
End NameSpace