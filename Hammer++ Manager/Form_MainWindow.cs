using System;
using System.Collections.Generic;
using System.IO;
using static HammerPP_Manager.Form_DownloadWindow;

namespace HammerPP_Manager
{
    internal partial class Form_MainWindow : HPPM_Form
    {
        internal List<SourceGames.SourceGame> Profiles = new List<SourceGames.SourceGame>();

        internal Form_MainWindow()
        {
            InitializeComponent();

            // Visual Studio doesn't want to update it's starting position, so force it.
            CenterToScreen();

            // Check existing profiles, if theres an installation
            if (CheckForHPPInstallation())
            {
                string[] directories = Directory.GetDirectories(Properties.Settings.Default.SourceSDKBasePath);

                // Convert found folders to just the folder name
                for (int i = 0; i < directories.Length; i++)
                {
                    directories[i] = directories[i].Substring(Properties.Settings.Default.SourceSDKBasePath.Length + 1);
                    foreach(SourceGames.SourceGame game in SourceGames.SUPPORTED_GAMES)
                    {
                        if(game.ProfileNametag == directories[i])
                        {
                            // To do, implement profile window
                            Profiles.Add(game);
                            listboxGames.Items.Add(game.GameName);
                            break;
                        }
                    }
                }
            }
        }

        private void buttonAddProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
        }

        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
        }

        private void buttonEditSources_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
        }

        /// <summary>
        /// Check to make sure an installation exists and that it's valid.
        /// </summary>
        /// <returns></returns>
        private static bool CheckForHPPInstallation(bool showMessage = true)
        {
            if (!Directory.Exists(Properties.Settings.Default.SourceSDKBasePath) || !GameSanityCheck(Properties.Settings.Default.SourceSDKBasePath, SourceGames.SOURCESDKBASE2013MP) || !HPPInstallationCheck())
            {
                if(showMessage)
                    new Form_InfoBox(FormType.SDKPathNotSelected, FormIconType.OBSOLETE, FormSoundType.DENIED).ShowDialog();
                return false;
            }
            if (!File.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg"))
            {
                if (showMessage)
                    new Form_InfoBox(HPPM_Form.FormType.HammerPPInstallationInvalid, HPPM_Form.FormIconType.OBSOLETE, HPPM_Form.FormSoundType.DENIED).ShowDialog();
                return false;
            }
            return true;
        }

        private void buttonInstallHPP_Click(object sender, EventArgs e)
        {
            if (CheckForHPPInstallation(false))
            {
                if (!new Form_InfoBox(HPPM_Form.FormType.InstallHPPAlreadyInstalled, HPPM_Form.FormIconType.LIGHT, HPPM_Form.FormSoundType.CAUTION).ShowDialog())
                    return;
            }
            SwitchToWindow(new Form_SelectSDKPath());
        }

        private void buttonUpdateHPP_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;

            // Check if internet is working
            if (!CheckInternetConnection())
            {
                new Form_InfoBox(HPPM_Form.FormType.NoInternet, HPPM_Form.FormIconType.LIGHT, HPPM_Form.FormSoundType.INFO).ShowDialog();
                return;
            }

            //To do
            //Download tag and check it with current version
            //If the tag is not the same, bring them to the download window but with the isUpdate bool set to true

        }

        
    }
}
