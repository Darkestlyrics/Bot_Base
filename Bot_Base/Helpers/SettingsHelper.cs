using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Base.Helpers {
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
            string path = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            string res = null;
            if (Exists(Name)) {
                res = config.AppSettings.Settings[Name].Value;
            }
            return res;
        }
        /// <summary>
        /// Sets the value of a setting in the Settings file
        /// </summary>
        /// <param name="Name">The name of the value</param>
        /// <param name="Value">The new Value of the setting</param>
        /// <returns></returns>
        public static bool SetValue (string Name, string Value, bool autocreate = false) {
            return setValue(Name, Value, autocreate);
        }

        private static bool setValue(string Name, string Value, bool autocreate = false) {
            string path = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            if (Exists(Name)) {
                config.AppSettings.Settings[Name].Value = Value;
                config.Save(ConfigurationSaveMode.Full);
                return true;
            } else {
                if (autocreate) {
                    Create(Name, Value);
                    return true;
                }else
                return false;
            }
        }
        /// <summary>
        /// Check if a Setting exists in the Settings file
        /// </summary>
        /// <param name="Name">The name of the setting to check</param>
        /// <returns>A <see cref="bool"/> based on the existence of the Setting</returns>
        private static bool Exists(string Name) {
            string path = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            return config.AppSettings.Settings.AllKeys.Any(prop => prop == Name);
        }

        private static bool Create(string Name, string value) {
            string path = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(path);
            config.AppSettings.Settings.Add(Name, value);
            return true;       
        }
    }
}
