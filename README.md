# GhostPDL.NET

Convert and create PDFs using [GhostPDL](https://ghostscript.readthedocs.io) with C# and .NET

## Give a Star!

If you liked the project or if GhostPDL.NET helped you, please give a star ;)

## Get Started

GhostPDL.NET can be installed using the Nuget package manager or the dotnet CLI.

```
dotnet add package GhostPDL.NET
```

```
Install-Package GhostPDL.NET
```

---

| Package               | Version                                                                                                        | Popularity                                                                                                      |
| --------------------- | -------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| `GhostPDL.NET`        | [![NuGet](https://img.shields.io/nuget/v/GhostPDL.NET)](https://nuget.org/packages/GhostPDL.NET)               | [![Nuget](https://img.shields.io/nuget/dt/GhostPDL.NET)](https://nuget.org/packages/GhostPDL.NET)               |
| `GhostPDL.NET.Native` | [![NuGet](https://img.shields.io/nuget/v/GhostPDL.NET.Native)](https://nuget.org/packages/GhostPDL.NET.Native) | [![Nuget](https://img.shields.io/nuget/dt/GhostPDL.NET.Native)](https://nuget.org/packages/GhostPDL.NET.Native) |

### Examples

Convert PDF/X to PDF/A

```
var pdfContentByte = await File.ReadAllBytesAsync("sample1.pdf");

var ghostProcessor = new GhostPdlProcessorPdfA();
byte[] rawPdfA = await ghostProcessor.ConvertAsync(pdfContentByte, PdfAProfile.A2B);
```

Run commands [Ghostscript](https://ghostscript.readthedocs.io/en/latest/Use.html)

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
