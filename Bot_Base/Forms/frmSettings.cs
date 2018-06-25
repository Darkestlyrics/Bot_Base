using Bot_Base.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Base.Forms {
    public partial class frmSettings : Form {       
        public frmSettings() {
            InitializeComponent();
        }

        public frmSettings(string path) {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e) {
            tbxPrefix.Text = SettingsHelper.GetValue("Prefix");
            tbxToken.Text = SettingsHelper.GetValue("DiscordToken");
            tbxServerName.Text = SettingsHelper.GetValue("ServerName");
            tbxServiceName.Text = SettingsHelper.GetValue("ServiceName");
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e) {
            if (emptyCheck()) {
                SettingsHelper.SetValue("Prefix", tbxPrefix.Text,true);
                SettingsHelper.SetValue("DiscordToken", tbxToken.Text,true);
                SettingsHelper.SetValue("ServerName", tbxServerName.Text,true);
                SettingsHelper.SetValue("ServiceName", tbxServiceName.Text,true);
                this.DialogResult = DialogResult.OK;
                Close();
            } else {
                MessageBox.Show("Please complete all fields");
            }
        }

        private bool emptyCheck() {
            bool res = true;
            if (string.IsNullOrEmpty(tbxPrefix.Text.Trim())) {
                res = false;
            }
            if (string.IsNullOrEmpty(tbxToken.Text.Trim())) {
                res = false;
            }
            if (string.IsNullOrEmpty(tbxServerName.Text.Trim())) {
                res = false;
            }            
            if (string.IsNullOrEmpty(tbxServiceName.Text.Trim())) {
                res = false;
            }
            return res;
        }
    }
}
