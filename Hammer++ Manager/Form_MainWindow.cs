using System;
using System.Collections.Generic;
using System.IO;
using static HammerPP_Manager.Form_DownloadWindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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
            UpdateProfileListings();


        }

        private void buttonAddProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
            new Form_AddGameProfile(Profiles).ShowDialog();
            UpdateProfileListings();
        }

        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
            UpdateProfileListings();
        }

        private void buttonEditSources_Click(object sender, EventArgs e)
        {
            if (!CheckForHPPInstallation()) return;
        }

        /// <summary>
        /// Check to make sure a Hammer++ Installation and SourceSDK Config exists and that it's valid.
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

        /* Update button, might implement later
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
        */

        /// <summary>
        /// Finds folders in the Source SDK Base 2013 folder, and profiles them if their folder name
        /// matches the folder name of a source game.
        /// Examples: hpp-gmod == Garry's Mod.
        ///           hpp-fof == Fistful of Frags.
        /// </summary>
        private void UpdateProfileListings()
        {
            this.Enabled = false;

            //If SDKBase is configured and Hammer++ installed
            if (CheckForHPPInstallation(false))
            {
                // Clear lists
                Profiles = new List<SourceGames.SourceGame>();

                // Get directories in SDK Base Folder
                string[] directories = Directory.GetDirectories(Properties.Settings.Default.SourceSDKBasePath);

                // For each folder...
                for (int i = 0; i < directories.Length; i++)
                {
                    // Convert found folder paths to just the folder name
                    directories[i] = directories[i].Substring(Properties.Settings.Default.SourceSDKBasePath.Length + 1);

                    // For Each supported game...
                    foreach (SourceGames.SourceGame game in SourceGames.SUPPORTED_GAMES)
                    {
                        //Check if it's ProfileNameTag matches the folder name
                        if (game.ProfileNametag == directories[i])
                        {
                            //If so, add it to both the profile list and the visual list
                            Profiles.Add(game);
                            break;
                        }
                    }
                }

                // Add profiles list to the visual listbox
                listboxGames.DataSource = Profiles;
            }
            this.Enabled = true;
        }

        
    }
}
