using DSharpPlus.CommandsNext;
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
        /// /Global Logger Object
        /// </summary>
        public static Logger Logger { get; set; }
        /// <summary>
        /// Global CommandNextModule
        /// </summary>
        public static CommandsNextModule CommandsNextModule { get; set; }
    }

}
