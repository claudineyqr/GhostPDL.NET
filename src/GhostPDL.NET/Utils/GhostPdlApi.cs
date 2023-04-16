// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GhostPDL.NET.Utils
{
    internal static class GhostPdlApi
    {
        /// <summary>
        /// Get informations of the currently loaded GhostPdl library.
        /// </summary>
        public static GhostPdlRevision GetRevision()
        {
            GhostPdlRevision _revision = new();
            var code = GhostPdlNativeLibrary.GetRevision(ref _revision, Marshal.SizeOf(_revision));
            if (code != 0)
                throw new GhostPdlException("gsapi_revision");

            return _revision;
        }

        public static void RunCommand(IEnumerable<string> switches, IEnumerable<DebugOption> debugOptions = null)
        {
            GhostPdlNativeLibrary.NewInstance(out var instance, IntPtr.Zero);
            if (instance == IntPtr.Zero)
                throw new GhostPdlException("gsapi_new_instance");

            try
            {
                var switchesBase = GetCommandsBase();

                switchesBase.AddRange(GetSwitchesDebug(debugOptions));
                switchesBase.AddRange(switches);

                string[] argsArray = switchesBase.Distinct().ToArray();

                int code = GhostPdlNativeLibrary.InitWithArgs(instance, argsArray.Length, argsArray);
                if (IsError(code))
                    throw new GhostPdlException($"gsapi_init_with_args {code}");
            }
            catch (DllNotFoundException ex)
            {
                throw new GhostPdlException($"Exception: {ex.Message}");
            }
            catch (BadImageFormatException ex)
            {
                throw new GhostPdlException($"Exception: {ex.Message}");
            }
            catch (GhostPdlException ex)
            {
                throw new GhostPdlException($"Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new GhostPdlException($"Exception: {ex.Message}");
            }
            finally
            {
                GhostPdlNativeLibrary.Exit(instance);
                GhostPdlNativeLibrary.DeleteInstance(instance);
            }
        }

        private static bool IsError(int code)
        {
            if (code < 0 && code != -101)
                return code != -110;

            return false;
        }

        private static List<string> GetCommandsBase()
        {
            var paths = new GhostPdlPath();
            string[] directories = new[] {
                paths.BasePath,
                paths.IccProfilesPath,
                paths.LibPath,
                paths.ResourcePath
            };

            var args = new List<string>
            {
                "gs",
                $"-I{string.Join(paths.DirectoriesSeparator, directories)}",
                "-dNOPAUSE",
                "-dBATCH",
                "-dSAFER",
                "-dQUIET",
                "-dCompatibilityLevel=1.7"
            };
            return args;
        }

        private static IEnumerable<string> GetSwitchesDebug(IEnumerable<DebugOption> debugOptions)
        {
            var switchesDebug = new List<string>();

            if (debugOptions != null &&
                debugOptions.Any())
            {
                if (debugOptions.Any(x => x == DebugOption.All))
                {
                    switchesDebug.Add("-dDEBUG");
                }
                else
                {
                    switchesDebug = debugOptions
                        .Select(x => $"-d{x}")
                        .Distinct()
                        .ToList();
                }
            }
            return switchesDebug;
        }
    }
}
