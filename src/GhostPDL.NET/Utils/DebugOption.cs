// 
// This file is part of GhostPDL.NET, a wrapper around the GhostPDL library for the .NET
// Author: Claudiney Queiroz (claudiney.queiroz@gmail.com, https://www.linkedin.com/in/claudineyqr/)
// Copyright © 2023 Claudiney Queiroz.
// License: GNU Affero General Public License v3.0 (GNU AGPLv3)

namespace GhostPDL.NET.Utils
{
    public enum DebugOption
    {
        /// <summary>
        /// Disable debug
        /// </summary>
        None,

        /// <summary>
        /// Set all of the subset switches
        /// </summary>
        DEBUG,

        /// <summary>
        /// Compiled Fonts
        /// </summary>
        CCFONTDEBUG,

        /// <summary>
        /// CFF Fonts
        /// </summary>
        CFFDEBUG,

        /// <summary>
        /// CMAP
        /// </summary>
        CMAPDEBUG,

        /// <summary>
        /// CIE color
        /// </summary>
        DOCIEDEBUG,

        /// <summary>
        /// EPS handling
        /// </summary>
        EPSDEBUG,

        /// <summary>
        /// Font API
        /// </summary>
        FAPIDEBUG,

        /// <summary>
        /// Initialization
        /// </summary>
        INITDEBUG,

        /// <summary>
        /// PDF Interpreter
        /// </summary>
        PDFDEBUG,

        /// <summary>
        /// PDF Writer
        /// </summary>
        PDFWRDEBUG,

        /// <summary>
        /// setpagedevice
        /// </summary>
        SETPDDEBUG,

        /// <summary>
        /// Static Resources
        /// </summary>
        STRESDEBUG,

        /// <summary>
        /// TTF Fonts
        /// </summary>
        TTFDEBUG,

        /// <summary>
        /// ViewGIF
        /// </summary>
        VGIFDEBUG,

        /// <summary>
        /// ViewJPEG
        /// </summary>
        VJPGDEBUG
    }
}