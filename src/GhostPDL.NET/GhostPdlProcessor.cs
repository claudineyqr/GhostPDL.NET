// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GhostPDL.NET.Utils;

namespace GhostPDL.NET
{
    public class GhostPdlProcessor
    {
        protected GhostPdlPath GhostPdlPaths { get; private set; }
        public GhostPdlRevision Revision => GhostPdlApi.GetRevision();

        public GhostPdlProcessor()
        {
            GhostPdlPaths = new GhostPdlPath();
        }

        public void InvokeCommand(IEnumerable<string> switches, IEnumerable<DebugOption> debugOptions = null)
        {
            GhostPdlApi.RunCommand(switches, debugOptions);
        }

        /// <summary>
        /// Create file temporary on disc
        /// </summary>
        /// <remarks>Return path temp file</remarks>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        public static async Task<string> CreateTempFile(byte[] fileBytes)
        {
            string fileTempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}-{Guid.NewGuid()}.pdf");
            await File.WriteAllBytesAsync(fileTempPath, fileBytes);
            return fileTempPath;
        }

        /// <summary>
        /// Remove file temporary on disc
        /// </summary>
        /// <param name="fileTempPath"></param>
        public static void RemoveTempFile(string fileTempPath)
        {
            if (File.Exists(fileTempPath))
                File.Delete(fileTempPath);
        }
    }
}