using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot_Base.Enums;
using log4net;
using log4net.Appender;
using log4net.Repository;
using static Bot_Base.Enums.Enums;

namespace Bot_Base.Classes {
    class Logger {
        private readonly ILog Log = null;

        public Logger() {
            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //get the current logging repository for this application
            ILoggerRepository repository = LogManager.GetRepository();
            //get all of the appenders for the repository
            IAppender[] appenders = repository.GetAppenders();
            //only change the file path on the 'FileAppenders'
            foreach (IAppender appender in (from iAppender in appenders
                                            where iAppender is FileAppender
                                            select iAppender)) {
                FileAppender fileAppender = appender as FileAppender;
                //set the path to your logDirectory using the original file name defined
                //in configuration
                fileAppender.File = Path.Combine(AppState.Path,"Logs","Log.txt" );
                //make sure to call fileAppender.ActivateOptions() to notify the logging
                //sub system that the configuration for this appender has changed.
                fileAppender.ActivateOptions();
            }
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

