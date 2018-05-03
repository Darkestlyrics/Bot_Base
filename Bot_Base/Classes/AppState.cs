using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bot_Base.Classes {
    static class AppState {
        /// <summary>
        /// Global Client Object
        /// </summary>
        public static Bot_Base Client { get; set; }
        /// <summary>
        /// Global base path
        /// </summary>
        public static string Path = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// /Global Logger Object
        /// </summary>
        public static Logger Logger = new Logger();
        /// <summary>
        /// Global CommandNextModule
        /// </summary>
        public static CommandsNextModule CommandsNextModule { get; set; }
        /// <summary>
        /// Global VoiceNextClinet
        /// </summary>
        public static VoiceNextClient VoiceNextClient { get; set; }

    }

}


