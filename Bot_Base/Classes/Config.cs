using SoggyBot.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoggyBot.Classes {
    public class GlobalConfig {
        public string DiscordToken { get; set; }

        public GlobalConfig() {
            DiscordToken = SettingsHelper.GetValue("DiscordToken");
        }

    }

public static class Config {
        public static GlobalConfig conf { get; internal set; }
        public static void Init() {
            conf = new GlobalConfig();

        }
    }
}


