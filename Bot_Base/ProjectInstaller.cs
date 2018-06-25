using Bot_Base.Forms;
using Bot_Base.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Base {
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer {
        public ProjectInstaller() {
#if (DEBUG)
                Debugger.Launch();
#endif
                InitializeComponent();
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e) {
            frmSettings frmSettings = new frmSettings();
            DialogResult res = frmSettings.ShowDialog();
            if (res != DialogResult.OK) {
                throw new ApplicationException("Settings window canceled, please run the setup again and fill in all fields");
            }
        }
    }
}
