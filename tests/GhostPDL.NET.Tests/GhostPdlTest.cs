using Xunit;

namespace GhostPDL.NET.Tests;

public class GhostPdlTest
{
    [Fact]
    public void RevisionGhostPdlIsOk()
    {
        int revisionExpected = 10000;
        int revisionDateExpected = 20220921;

        var gsProcessor = new GhostPdlProcessor();
        var revision = gsProcessor.Revision;

        Assert.Equal(revisionExpected, revision.Revision);
        Assert.Equal(revisionDateExpected, revision.RevisionDate);
    }
}
