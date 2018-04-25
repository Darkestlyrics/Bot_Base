using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoggyBot.Helpers {
  static  class SettingsHelper {
        /// <summary>
        /// Get the value of a setting in the Settings file
        /// </summary>
        /// <param name="Name">The name of the value</param>
        /// <returns>The <see cref="string"/> representation a value in the settings file otherwise returns null</returns>
        public static string GetValue(string Name) {
            return getValue(Name);
        }
        private static string getValue(string Name) {
            string res = null;
            if (Exists(Name)) {
                res = Bot_Base.Properties.Settings.Default[Name].ToString();
            }
            return res;
        }
        /// <summary>
        /// Sets the value of a setting in the Settings file
        /// </summary>
        /// <param name="Name">The name of the value</param>
        /// <param name="Value">The new Value of the setting</param>
        /// <returns></returns>
        public static bool SetValue (string Name, string Value) {
            return setValue(Name, Value);
        }

        private static bool setValue(string Name, string Value) {
            if (Exists(Name)) {
                Bot_Base.Properties.Settings.Default[Name] = Value;
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// Check if a Setting exists in the Settings file
        /// </summary>
        /// <param name="Name">The name of the setting to check</param>
        /// <returns>A <see cref="bool"/> based on the existence of the Setting</returns>
        private static bool Exists(string Name) {
            return Bot_Base.Properties.Settings.Default.Properties.Cast<SettingsProperty>()
                                                                                            .Any(prop => prop.Name == Name);
        }
    }
}
