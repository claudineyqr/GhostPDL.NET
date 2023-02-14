// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

using System;

namespace GhostPDL.NET.Utils
{
    public struct GhostPdlRevision
    {
        public IntPtr Product;
        public IntPtr Copyright;
        public int Revision;
        public int RevisionDate;
    }
}
