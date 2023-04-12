// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GhostPDL.NET.Utils
{
    internal static class GhostPdlNativeLibrary
    {
        private const string dllName = "libgpdl";

        static GhostPdlNativeLibrary()
        {
            NativeLibrary.SetDllImportResolver(typeof(GhostPdlNativeLibrary).Assembly, ImportResolver);
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (string.Equals(libraryName, dllName))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return NativeLibrary.Load("gpdldll64.dll", assembly, searchPath);

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    NativeLibrary.TryLoad("libgpdl.so.10.00", assembly, searchPath, out IntPtr handle);

                    if (handle != IntPtr.Zero)
                        return handle;

                    return NativeLibrary.Load("libgpdl.rhel.so.10.00", assembly, searchPath);
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return NativeLibrary.Load("libgpdl.dylib.10.00", assembly, searchPath);
            }
            return IntPtr.Zero;
        }

        [DllImport(dllName, CharSet = CharSet.Ansi, EntryPoint = "gsapi_revision")]
        public static extern int GetRevision(ref GhostPdlRevision vers, int length);

        [DllImport(dllName, EntryPoint = "gsapi_new_instance")]
        public static extern int NewInstance(out IntPtr instance, IntPtr handle);

        [DllImport(dllName, EntryPoint = "gsapi_init_with_args")]
        public static extern int InitWithArgs(IntPtr instance, int argc, string[] argv);

        [DllImport(dllName, EntryPoint = "gsapi_exit")]
        public static extern int Exit(IntPtr instance);

        [DllImport(dllName, EntryPoint = "gsapi_delete_instance")]
        public static extern void DeleteInstance(IntPtr instance);
    }
}
