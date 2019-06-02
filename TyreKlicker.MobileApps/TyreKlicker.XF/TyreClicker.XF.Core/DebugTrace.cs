using System;
using System.Diagnostics;
using MvvmCross.Logging;

namespace TyreKlicker.XF.Core
{
    public class DebugTrace : IMvxLog
    {
        public bool IsLogLevelEnabled(MvxLogLevel logLevel)
        {
            return true;
        }

        public bool Log(MvxLogLevel logLevel, Func<string> messageFunc, Exception exception = null,
            params object[] formatParameters)
        {
            Debug.WriteLine(logLevel + ":" + messageFunc());

            return true;
        }
    }
}