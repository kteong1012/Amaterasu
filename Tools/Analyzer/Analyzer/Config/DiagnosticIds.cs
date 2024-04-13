using System;
using System.Collections.Generic;
using System.Text;

namespace Analyzer.Config
{
    public static class DiagnosticIds
    {
        #region Standard
        #region Error
        public const string ForceBrace = "STD001";
        #endregion
        #endregion

        #region Logic
        #region Error
        public const string GameServiceAttribute = "LOG001";
        public const string GameServiceAttributeNone = "LOG002";
        #endregion
        #endregion
    }
}
