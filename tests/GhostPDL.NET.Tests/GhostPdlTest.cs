// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

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
