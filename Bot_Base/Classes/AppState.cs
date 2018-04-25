using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SoggyBot.Classes {
    static class AppState {
        /// <summary>
        /// Global Client Object
        /// </summary>
        public static Bot_Base Client { get; set; }
        /// <summary>
        /// /Global Logger Object
        /// </summary>
        public static Logger Logger { get; set; }

    }

}
