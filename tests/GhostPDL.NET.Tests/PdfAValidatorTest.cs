// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Codeuctivity;
using GhostPDL.NET.Enums;
using UglyToad.PdfPig.Writer;
using Xunit;

namespace GhostPDL.NET.Tests
{
    public class PdfAValidatorTest
    {
        private static string BasePath => Regex.Replace(AppContext.BaseDirectory, @"bin.*", "", RegexOptions.IgnoreCase);
        private static string BasePathPdfs => Path.Combine(BasePath, "PDFs");

        [Theory]
        [InlineData(PdfName.Sample1)]
        [InlineData(PdfName.Sample2)]
        [InlineData(PdfName.Sample3)]
        [InlineData(PdfName.Sample4)]
        [InlineData(PdfName.Sample5)]
        [InlineData(PdfName.Sample6)]
        [InlineData(PdfName.Sample7)]
        public async Task PdfGeneratedIsPdfA1B(string pdfName)
        {
            var fileBytes = await GetBytesPdf(pdfName);
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A1B, pdfName);
        }

        [Theory]
        [InlineData(PdfName.Sample1)]
        [InlineData(PdfName.Sample2)]
        [InlineData(PdfName.Sample3)]
        [InlineData(PdfName.Sample4)]
        [InlineData(PdfName.Sample5)]
        [InlineData(PdfName.Sample6)]
        [InlineData(PdfName.Sample7)]
        public async Task PdfGeneratedIsPdfA2B(string pdfName)
        {
            var fileBytes = await GetBytesPdf(PdfName.Sample1);
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A2B, pdfName);
        }

        [Theory]
        [InlineData(PdfName.Sample1)]
        [InlineData(PdfName.Sample2)]
        [InlineData(PdfName.Sample3)]
        [InlineData(PdfName.Sample4)]
        [InlineData(PdfName.Sample5)]
        [InlineData(PdfName.Sample6)]
        [InlineData(PdfName.Sample7)]
        public async Task PdfGeneratedIsPdfA3B(string pdfName)
        {
            var fileBytes = await GetBytesPdf(PdfName.Sample1);
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A3B, pdfName);
        }

        [Fact]
        public async Task PdfMegedGeneratedIsPdfA1B()
        {
            var fileBytes = GetBytesAllPdfsMerged();
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A1B, "merged");
        }

        [Fact]
        public async Task PdfMegedGeneratedIsPdfA2B()
        {
            var fileBytes = GetBytesAllPdfsMerged();
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A2B, "merged");
        }

        [Fact]
        public async Task PdfMegedGeneratedIsPdfA3B()
        {
            var fileBytes = GetBytesAllPdfsMerged();
            await PdfGeneratedIsPdfA(fileBytes, PdfAProfile.A3B, "merged");
        }

        private static async Task PdfGeneratedIsPdfA(
            byte[] fileBytes,
            PdfAProfile pdfAProfile,
            string pdfName,
            bool deleteOutputFile = true)
        {
            var gsProcessor = new GhostPdlProcessorPdfA();
            var rawPdfA = await gsProcessor.ConvertAsync(fileBytes, pdfAProfile);

            string outputFile = Path.Combine(CreateOutputFolder(), $"output-{pdfName}-{pdfAProfile}-{Guid.NewGuid()}.pdf");
            await File.WriteAllBytesAsync(outputFile, rawPdfA);

            string profileName = "";
            switch (pdfAProfile)
            {
                case PdfAProfile.A1B:
                    profileName = "PDF/A-1B validation profile";
                    break;
                case PdfAProfile.A2B:
                    profileName = "PDF/A-2B validation profile";
                    break;
                case PdfAProfile.A3B:
                    profileName = "PDF/A-3B validation profile";
                    break;
            }

            using var pdfAValidator = new PdfAValidator();
            var result = await pdfAValidator.ValidateWithDetailedReportAsync(outputFile);

            if (deleteOutputFile && File.Exists(outputFile))
                File.Delete(outputFile);

            Assert.True(result.Jobs.Job.ValidationReport.IsCompliant);
            Assert.True(result.Jobs.Job.ValidationReport.ProfileName == profileName);
        }

        private static string CreateOutputFolder()
        {
            var pathOutput = Path.Combine(BasePath, "output");
            if (!Directory.Exists(pathOutput))
                Directory.CreateDirectory(pathOutput);

            return pathOutput;
        }

        private static async Task<byte[]> GetBytesPdf(string pdfName)
        {
            return await File.ReadAllBytesAsync(Path.Combine(BasePathPdfs, $"{pdfName}.pdf"));
        }

        private static byte[] GetBytesAllPdfsMerged()
        {
            string[] filesPdf = new[]
            {
                Path.Combine(BasePathPdfs, $"{PdfName.Sample1}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample2}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample3}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample4}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample5}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample6}.pdf"),
                Path.Combine(BasePathPdfs, $"{PdfName.Sample7}.pdf")
            };

            var fileBytes = filesPdf
                .Select(x => File.ReadAllBytes(x))
                .ToList();

            var mergedFileBytes = PdfMerger.Merge(fileBytes);
            return mergedFileBytes;
        }
    }

    internal static class PdfName
    {
        public const string Sample1 = "sample1";
        public const string Sample2 = "sample2";
        public const string Sample3 = "sample3";
        public const string Sample4 = "sample4";
        public const string Sample5 = "sample5";
        public const string Sample6 = "sample6";
        public const string Sample7 = "sample7";
    }
}