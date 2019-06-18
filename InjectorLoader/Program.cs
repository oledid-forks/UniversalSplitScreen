using System;
using System.Runtime.InteropServices;

namespace InjectorLoader
{
	static class Program
	{
		class Injector32
		{
			[DllImport("InjectorCPP32.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
			public static extern uint Inject(
					int pid,
					[MarshalAsAttribute(UnmanagedType.LPWStr)] string injectionDllPath32,
					[MarshalAsAttribute(UnmanagedType.LPWStr)] string injectionDllPath64,
					IntPtr hWnd,
					string ipcChannelNameRead,
					string ipcChannelNameWrite,
					int controllerIndex,
					int allowedMouseHandle,
					bool useLegacyInput,
					bool HookGetCursorPos,
					bool HookGetForegroundWindow,
					bool HookGetAsyncKeyState,
					bool HookGetKeyState,
					bool HookCallWindowProcW,
					bool HookRegisterRawInputDevices,
					bool HookSetCursorPos,
					bool HookXInput);
		}

		class Injector64
		{
			[DllImport("InjectorCPP64.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
			public static extern uint Inject(
					int pid,
					[MarshalAsAttribute(UnmanagedType.LPWStr)] string injectionDllPath32,
					[MarshalAsAttribute(UnmanagedType.LPWStr)] string injectionDllPath64,
					IntPtr hWnd,
					string ipcChannelNameRead,
					string ipcChannelNameWrite,
					int controllerIndex,
					int allowedMouseHandle,
					bool useLegacyInput,
					bool HookGetCursorPos,
					bool HookGetForegroundWindow,
					bool HookGetAsyncKeyState,
					bool HookGetKeyState,
					bool HookCallWindowProcW,
					bool HookRegisterRawInputDevices,
					bool HookSetCursorPos,
					bool HookXInput);
		}

		public static void Main(string[] args)
		{
			const int argsL = 16;

			if (args.Length != argsL)
			{
				throw new ArgumentException($"Need exactly {argsL} arguments");
			}

			//Arguments
			int.TryParse(args[0], out int pid);

			string injectionDllPath = args[1];

			int.TryParse(args[2], out int _hWnd);
			IntPtr hWnd = (IntPtr)_hWnd;

			string ipcChannelNameRead = args[3];
			string ipcChannelNameWrite = args[4];

			int.TryParse(args[5], out int controllerIndex);

			int.TryParse(args[6], out int allowedMouseHandle);

			bool useLegacyInput = args[7].ToLower().Equals("true");

			int i = 7;
			bool nextBool() => args[i++].ToLower().Equals("true");

			bool HookGetCursorPos = nextBool();
			bool HookGetForegroundWindow = nextBool();
			bool HookGetAsyncKeyState = nextBool();
			bool HookGetKeyState = nextBool();
			bool HookCallWindowProcW = nextBool();
			bool HookRegisterRawInputDevices = nextBool();
			bool HookSetCursorPos = nextBool();
			bool HookXInput = nextBool();

			//InjectorCPP function
			uint nt;
			if (Environment.Is64BitProcess)
			{
				nt = Injector64.Inject(pid,
					"",
					injectionDllPath,
					hWnd,
					ipcChannelNameRead,
					ipcChannelNameWrite,
					controllerIndex,
					allowedMouseHandle,
					useLegacyInput,
					HookGetCursorPos,
					HookGetForegroundWindow,
					HookGetAsyncKeyState,
					HookGetKeyState,
					HookCallWindowProcW,
					HookRegisterRawInputDevices,
					HookSetCursorPos,
					HookXInput);
			}
			else
			{
				nt = Injector32.Inject(pid,
					injectionDllPath,
					"",
					hWnd,
					ipcChannelNameRead,
					ipcChannelNameWrite,
					controllerIndex,
					allowedMouseHandle,
					useLegacyInput,
					HookGetCursorPos,
					HookGetForegroundWindow,
					HookGetAsyncKeyState,
					HookGetKeyState,
					HookCallWindowProcW,
					HookRegisterRawInputDevices,
					HookSetCursorPos,
					HookXInput);
			}

			//Set exit code
			Environment.Exit((int)nt);
		}
	}
}
