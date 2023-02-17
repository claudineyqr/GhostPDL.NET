# GhostPDL.NET

[![NuGet](https://img.shields.io/nuget/v/GhostPDL.NET)](https://nuget.org/packages/GhostPDL.NET) [![Nuget](https://img.shields.io/nuget/dt/GhostPDL.NET)](https://nuget.org/packages/GhostPDL.NET)

Convert and create PDFs using [GhostPDL](https://ghostscript.readthedocs.io) with C# and .NET

## Give a Star!

If you liked the project or if GhostPDL.NET helped you, please give a star ;)

## Get Started

GhostPDL.NET can be installed using the Nuget package manager or the dotnet CLI.

```
dotnet add package GhostPDL.NET
```

### Examples

**Convert PDF/X to PDF/A**

```
var pdfContentByte = await File.ReadAllBytesAsync("sample1.pdf");

var ghostProcessor = new GhostPdlProcessorPdfA();
byte[] rawPdfA = await ghostProcessor.ConvertAsync(pdfContentByte, PdfAProfile.A2B);
```

To validate PDF/A result use the [Codeuctivity.PdfAValidator](https://github.com/Codeuctivity/PdfAValidatorApi)

**Run commands Ghostscript**

Documentation Ghostscript:

- https://ghostscript.com
- https://ghostscript.readthedocs.io/en/gs10.0.0/
- https://github.com/ArtifexSoftware/ghostpdl-downloads/releases/download/gs1000/Ghostscript.pdf

```
var switches = new List<string>
{
    "-command1",
    "-command2",
    "-command3"
};

var ghostProcessor = new GhostPdlProcessor();
ghostProcessor.InvokeCommand(switches);
```

### Documentation

- Coming soon.

## Support

- win-x64
- linux-x64
- linux-arm64
- osx-x64

## About

GhostPDL.NET was developed by [Claudiney Queiroz](https://claudineyqueiroz.dev).

## License

Copyright © 2023 Claudiney Queiroz.

The project is licensed under the [GNU AGPLv3](https://github.com/claudineyqr/GhostPDL.NET/blob/master/LICENSE).
