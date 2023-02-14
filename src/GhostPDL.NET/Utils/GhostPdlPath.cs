// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GhostPDL.NET.Utils
{
    public class GhostPdlPath
    {
        public string BasePath { get; private set; }
        public string LibPath { get; private set; }
        public string IccProfilesPath { get; private set; }
        public string ResourcePath { get; private set; }
        public string DirectoriesSeparator { get; private set; }

        public GhostPdlPath()
        {
            string dirOS = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                dirOS = "win-x64";
                DirectoriesSeparator = ";";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                RuntimeInformation.OSArchitecture == Architecture.X64)
            {
                dirOS = "linux-x64";
                DirectoriesSeparator = ":";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                RuntimeInformation.OSArchitecture == Architecture.Arm64)
            {
                dirOS = "linux-arm64";
                DirectoriesSeparator = ":";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                dirOS = "osx-x64";
                DirectoriesSeparator = ":";
            }

            BasePath = Path.Combine(GetAssemblyDirectory(), "runtimes", dirOS, "native", "ghostpdl-contents");
            LibPath = Path.Combine(BasePath, "lib");
            IccProfilesPath = Path.Combine(BasePath, "iccprofiles");
            ResourcePath = Path.Combine(BasePath, "Resource");
        }

        private static string GetAssemblyDirectory()
        {
            string location = Assembly.GetAssembly(typeof(GhostPdlNativeLibrary)).CodeBase;
            var uri = new UriBuilder(location);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
