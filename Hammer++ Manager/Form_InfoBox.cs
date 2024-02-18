namespace HammerPP_Manager
{
    internal partial class Form_InfoBox : HPPM_Form
    {
        private bool dialogResult = false;
        internal Form_InfoBox(FormType type, FormIconType icon, FormSoundType sound, SourceGames.SourceGame sourceGame = null, string[] parameters = null)
        {
            InitializeComponent();

            // Change the icon to what we want
            switch (icon)
            {
                case FormIconType.GLOBAL:
                    this.pictureboxIcon.Image = Properties.Resources.global;
                    break;
                case FormIconType.LIGHT:
                    this.pictureboxIcon.Image = Properties.Resources.light;
                    break;
                case FormIconType.OBSOLETE:
                    this.pictureboxIcon.Image = Properties.Resources.obsolete;
                    break;
                case FormIconType.SOUND:
                    this.pictureboxIcon.Image = Properties.Resources.sound;
                    break;
            }

            // Setup buttons
            switch (type)
            {
                case FormType.GamePathValid:
                case FormType.GamePathNotFoundManual:
                case FormType.GamePathNotFoundAuto:
                case FormType.OpenHammerPPToConfigure:
                case FormType.NoGamesFoundAuto:
                case FormType.NoGamesFoundManual:
                case FormType.SDKPathNotSelected:
                case FormType.HammerPPInstallationInvalid:
                case FormType.NoInternet:
                case FormType.ProfileAlreadyInstalled:
                    buttonYes.Enabled = false;
                    buttonYes.Hide();
                    buttonNo.Text = "Ok";
                    break;
            }

            // Setup text
            switch (type)
            {
                case FormType.GamePathNotFoundAuto:
                    this.Text += "Game Path Not Found!";
                    this.labelTitle.Text = "Auto Scan Failed to find the game directory!";
                    this.labelDescription.Text = "Failed to find " + sourceGame.GameName + "'s directory!" +
                        "\nCheck to make sure it's installed on Steam and Validate the game files.\n" +
                        "\nIf you are sure it's installed and validated, try manually selecting the directory.";
                    this.Size = new System.Drawing.Size(585, 180);
                    break;

                case FormType.GamePathNotFoundManual:
                    this.Text += "Game Path Not Valid!";
                    this.labelTitle.Text = "Failed to verify selected game directory!";
                    this.labelDescription.Text = "Failed to verify '" + parameters[0] +
                        "'\nas " + sourceGame.GameName + "'s directory!" +
                        "\nEnsure you select the '" + sourceGame.GameDirectory + "' folder and not any sub-folders." +
                        "\n\nWindows is buggy when reopening selection dialogs, so go to the parent folder of the\n" +
                        "'" + sourceGame.GameName + "' folder,\n" +
                        "then open the '" + sourceGame.GameName + "' folder, then click the 'Open' button.";
                    this.Size = new System.Drawing.Size(585, 210);
                    break;

                case FormType.GamePathValid:
                    this.Text += "Game Path Verified!";
                    this.labelTitle.Text = "Valid Game Path Found!";
                    this.labelDescription.Text = "Press Ok to continue to the next stage!";
                    this.Size = new System.Drawing.Size(375, 125);
                    break;

                case FormType.OpenHammerPPToConfigure:
                    this.Text += "Please Open Hammer++";
                    this.labelTitle.Text = "Information!";
                    this.labelDescription.Text = "To properly setup Hammer++, the program needs to be opened atleast once.\n" +
                        "When you click 'Ok', Hammer++ will open.\n" +
                        "Select any configuration, wait for it to load successfully, then close it and click 'Next'.";
                    this.Size = new System.Drawing.Size(530, 160);
                    break;

                case FormType.NoGamesFoundAuto:
                    this.Text += "No Games Found!";
                    this.labelTitle.Text = "Auto Scan Failed to find any Source Games!";
                    this.labelDescription.Text = "No valid Source Games were found!\n" +
                        "Ensure the Source Game you want to add has had it's files validated.\n" +
                        "If all else fails, manually select the game's directory.";
                    this.Size = new System.Drawing.Size(585, 160);
                    break;

                case FormType.NoGamesFoundManual:
                    this.Text += "No Games Found!";
                    this.labelTitle.Text = "Failed to verify any Source Games!";
                    this.labelDescription.Text = "No valid Source Games were found!\n" +
                        "Ensure the Source Game you want to add has had it's files validated.";
                    this.Size = new System.Drawing.Size(520, 150);
                    break;

                case FormType.SDKPathNotSelected:
                    this.Text += "Configuration Issue!";
                    this.labelTitle.Text = "SDK Path not configured!";
                    this.labelDescription.Text = "No Installation has been setup.\n" +
                        "Please go to the \"Tools\" drop down box and " +
                        "install Hammer++.";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                case FormType.HPPAlreadyInstalled:
                    this.Text += "Hammer++ Already Installed!";
                    this.labelTitle.Text = "Hammer++ Installation Detected!";
                    this.labelDescription.Text = "Either a full installation or remnant files of a Hammer++ installation\r\nexist in Source SDK Base 2013 Multiplayer's Folder.\n\n" +
                        "To ensure best compatability, we need to delete both the \n'hammerplusplus.exe' file and 'hammerplusplus' folder.\n\n" +
                        "Press Yes to delete and continue.\n" +
                        "Press No to stop the installation.";
                    this.Size = new System.Drawing.Size(460, 210);
                    break;

                case FormType.HammerPPInstallationInvalid:
                    this.Text += "Hammer++ Installation Invalid!";
                    this.labelTitle.Text = "Hammer++ Installation Invalid!";
                    this.labelDescription.Text = "The Hammer++ Installation is either damaged or invalid.\n" +
                        "Please go to the \"Tools\" drop down box and install Hammer++.";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                case FormType.InstallHPPAlreadyInstalled:
                    this.Text += "Hammer++ Already Installed!";
                    this.labelTitle.Text = "Hammer++ Already Installed!";
                    this.labelDescription.Text = "Hammer++ is already installed, do you want to continue?";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                case FormType.NoInternet:
                    this.Text += "Internet Connection Issue!";
                    this.labelTitle.Text = "Can't Connect to GitHub!";
                    this.labelDescription.Text = "Hammer++ Manager can't connect to GitHub.com!\n" +
                        "Check your internet connection or check if GitHub is down.";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                case FormType.ProfileAlreadyInstalled:
                    this.Text += "Profile Already Added!";
                    this.labelTitle.Text = "Profile Already Added!";
                    this.labelDescription.Text = "This game has already been added.";
                    this.Size = new System.Drawing.Size(375, 125);
                    break;

                case FormType.ProfileResidueData:
                    this.labelTitle.Text = "Residual Files Found!";
                    this.labelTitle.Text = "Dirty Profile Detected!";
                    this.labelDescription.Text = "A broken profile relating to this game has been found.\nDo you wish to delete this broken profile and install a fresh profile?";
                    buttonYes.Text = "Clean Up";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                case FormType.DeleteProfile:
                    this.labelTitle.Text = "Delete Profile.";
                    this.labelTitle.Text = "Delete profile.";
                    this.labelDescription.Text = "Are you sure you want to delete " + sourceGame.GameName +"'s profile?";
                    this.Size = new System.Drawing.Size(450, 140);
                    break;

                default:
                    break;
            }


            // Play sound
            switch (sound)
            {
                case FormSoundType.CAUTION:
                    sfxCaution.Play();
                    break;
                case FormSoundType.DENIED:
                    sfxDenied.Play();
                    break;
                case FormSoundType.INFO:
                    sfxInfo.Play();
                    break;
                case FormSoundType.SUCCESS:
                    sfxSuccess.Play();
                    break;
            }

        }

        /// <summary>
        /// Override ShowDialog to return the bool.
        /// </summary>
        /// <returns></returns>
        public new bool ShowDialog()
        {
            // Show the dialog as usual
            base.ShowDialog();

            // Return the boolean variable indicating the result
            return dialogResult;
        }

        private void buttonYes_Click(object sender, System.EventArgs e)
        {
            dialogResult = true;
            Close();
        }

        private void buttonNo_Click(object sender, System.EventArgs e)
        {
            dialogResult = false;
            Close();
        }
    }
}
