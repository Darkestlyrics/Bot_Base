using Bot_Base.Classes;
using Bot_Base.Helpers;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bot_Base {
    public partial class BotBaseService : ServiceBase {
        private bool _isRunning = false;
        private ManualResetEvent stopFlag = new ManualResetEvent(false);

        public BotBaseService() {
            try {
                InitializeComponent();
                Config.Init();
                FolderHelper.CreateDirStructure();
                AppState.Client = new Classes.Bot_Base(Properties.Settings.Default.ServerName,
                    new DiscordConfiguration {
                        AutoReconnect = true,
                        LargeThreshold = 250,
                        LogLevel = LogLevel.Warning,
                        Token = Config.conf.DiscordToken,
                        TokenType = TokenType.Bot,
                        UseInternalLogHandler = true
                    });
            } catch(Exception ex) {
                AppState.Logger.WriteLog(Enums.Enums.LogLevel.FATAL, "Could not start service", ex);
            }
        }

        

        protected override void OnStart(string[] args) {
            try {
                AppState.Logger.WriteLog(Enums.Enums.LogLevel.INFO, "Starting Service");
                _isRunning = true;
#if (DEBUG)
                Debugger.Launch();
#endif
                if (AppState.Client.Start(3).Status) {
                    //initialise the client if we can connect
                    AppState.Client.Init();
                    FolderHelper.CreateDirStructure();
                } else {
                    AppState.Logger.WriteLog(Enums.Enums.LogLevel.ERROR, "Could not connect to server");
                }
            } catch (Exception ex) {
                AppState.Logger.WriteLog(Enums.Enums.LogLevel.ERROR,"Exception occured during start", ex);
            }

        }

        protected override void OnStop() {
            stopFlag.Set();
            _isRunning = false;
            AppState.Client.Stop();
        }
    }
}
