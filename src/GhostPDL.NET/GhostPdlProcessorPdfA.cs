// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GhostPDL.NET.Enums;
using GhostPDL.NET.Utils;

namespace GhostPDL.NET
{
    public class GhostPdlProcessorPdfA : GhostPdlProcessor
    {
        private readonly string _filePdfADef;

        public GhostPdlProcessorPdfA()
        {
            _filePdfADef = Path.Combine(GhostPdlPaths.LibPath, "PDFA_custom_def.ps");
        }

        /// <summary>
        /// Convert document PDF/X to PDF/A
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="pdfAProfile"></param>
        /// <param name="debugOptions"></param>
        /// <returns></returns>
        public async Task<byte[]> ConvertAsync(
            byte[] fileBytes,
            PdfAProfile pdfAProfile = PdfAProfile.A2B,
            IEnumerable<DebugOption> debugOptions = null)
        {
            string fileTempInput = await CreateTempFile(fileBytes);
            string fileTempOutput = await CreateTempFile(Array.Empty<byte>());

            var switches = new List<string>
            {
                "-sColorConversionStrategy=RGB",
                "-sDEVICE=pdfwrite",
                $"-sOutputFile={fileTempOutput}",
                $"-dPDFA={(int)pdfAProfile}",
                "-dPDFACompatibilityPolicy=1",
                "-dPDFSETTINGS=/default",
                "-dAutoRotatePages=/All",
                "-dEmbedAllFonts=true",
                $"{_filePdfADef}",
                "-f",
                fileTempInput
            };

            try
            {
                await UpdatePdfADef();
                InvokeCommand(switches, debugOptions);
                return await File.ReadAllBytesAsync(fileTempOutput);
            }
            finally
            {
                RemoveTempFile(fileTempInput);
                RemoveTempFile(fileTempOutput);
            }
        }

        /// <summary>
        /// Update file PDFA_def.ps with path server
        /// </summary>
        private async Task UpdatePdfADef()
        {
            string pattern = @"\/ICCProfile \((.*)+\) % Customise";
            string textFileDef = await File.ReadAllTextAsync(_filePdfADef);

            var match = Regex.Match(textFileDef, pattern);
            if (!match.Success)
                throw new FileNotFoundException("ICCProfile not found");

            string pathIccProfileCurrent = match.Value
                .Replace("/ICCProfile (", "")
                .Replace(") % Customise", "");

            string fileIccProfile = Path.Combine(GhostPdlPaths.IccProfilesPath, "srgb.icc");
            string pathIccProfile = fileIccProfile.Replace(@"\", "/");

            if (!string.Equals(pathIccProfileCurrent, pathIccProfile))
            {
                string pathIccProfileNew = @$"/ICCProfile ({pathIccProfile}) % Customise";
                textFileDef = Regex.Replace(textFileDef, pattern, pathIccProfileNew);
                await File.WriteAllTextAsync(_filePdfADef, textFileDef);
            }
        }
    }
}
