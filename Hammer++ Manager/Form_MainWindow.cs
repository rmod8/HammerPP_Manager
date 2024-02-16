using System;
using System.IO;

namespace HammerPP_Manager
{
    internal partial class Form_MainWindow : HPPM_Form
    {
        internal Form_MainWindow()
        {
            InitializeComponent();
        }

        private void buttonAddProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForSDKInstallation()) return;
        }

        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForSDKInstallation()) return;
        }

        private void buttonEditSources_Click(object sender, EventArgs e)
        {
            if (!CheckForSDKInstallation()) return;
        }

        private static bool CheckForSDKInstallation()
        {
            if (!Directory.Exists(Properties.Settings.Default.SourceSDKBasePath) || !GameSanityCheck(Properties.Settings.Default.SourceSDKBasePath, SourceGames.SOURCESDKBASE2013MP))
            {
                new Form_InfoBox(FormType.SDKPathNotSelected, FormIconType.OBSOLETE, FormSoundType.DENIED).ShowDialog();
                return false;
            }
            return true;
        }

        private void buttonInstallHPP_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdateHPP_Click(object sender, EventArgs e)
        {

        }

        
    }
}
