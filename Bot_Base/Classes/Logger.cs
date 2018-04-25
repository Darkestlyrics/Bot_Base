using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoggyBot.Enums;
using log4net;
using static SoggyBot.Enums.Enums;

namespace SoggyBot.Classes {
    class Logger {
        private static readonly ILog Log = null;


       static Logger() {
          Log =  LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

       /// <summary>
       /// Writes a message to the Log File
       /// </summary>
       /// <param name="level">the Level of the Log</param>
       /// <param name="LogText">The text to write to the log</param>
       /// <param name="ex">the exception encountered</param>
        public void WriteLog(LogLevel level, string LogText, Exception ex = null) {
            writeLog(level, LogText, ex);
        }

        private void writeLog(LogLevel level, string LogText, Exception ex) {
            switch (level) {
                case LogLevel.FATAL:
                    if (ex == null)
                        Log.Fatal(LogText);
                    else
                        Log.Fatal(LogText, ex);
                    break;
                case LogLevel.ERROR:
                    if (ex == null)
                        Log.Error(LogText);
                    else
                        Log.Error(LogText, ex);
                    break;
                case LogLevel.WARN:
                    if (ex == null)
                        Log.Warn(LogText);
                    else
                        Log.Warn(LogText, ex);
                    break;
                case LogLevel.INFO:
                    if (ex == null)
                        Log.Info(LogText);
                    else
                        Log.Info(LogText, ex);
                    break;
                case LogLevel.DEBUG:
                    if (ex == null)
                        Log.Debug(LogText);
                    else
                        Log.Debug(LogText, ex);
                    break;
                default:
                    break;
            }
        }
    }
}

