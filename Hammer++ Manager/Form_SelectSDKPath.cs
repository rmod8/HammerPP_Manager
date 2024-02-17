using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal partial class Form_SelectSDKPath : HPPM_Form
    {
        internal Form_SelectSDKPath()
        {
            InitializeComponent();
            sfxInfo.Play();
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            // We're gonna use OpenFileDialog because OpenFolderDialog sucks

            // Set up OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Source SDK Base 2013 Multiplayer Directory...";
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = "[Folder Selection]";

            // If the user didn't click the OK button, we return...
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            if (!GameSanityCheck(Path.GetDirectoryName(openFileDialog.FileName), SourceGames.SOURCESDKBASE2013MP))
            {
                new Form_InfoBox(FormType.GamePathNotFoundManual, FormIconType.OBSOLETE, FormSoundType.DENIED, SourceGames.SOURCESDKBASE2013MP, new string[] { Path.GetDirectoryName(openFileDialog.FileName) }).ShowDialog();
                return;
            }
            else
            {
                Properties.Settings.Default.SourceSDKBasePath = Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.Save();
                new Form_InfoBox(FormType.GamePathValid, FormIconType.SOUND, FormSoundType.SUCCESS).ShowDialog();
                SwitchToWindow(new Form_DownloadWindow());
            }
        }

        private void buttonAutomatic_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            string path = ScanForGameDirectory(SourceGames.SOURCESDKBASE2013MP);
            if (path == null)
            {
                new Form_InfoBox(FormType.GamePathNotFoundAuto, FormIconType.OBSOLETE, FormSoundType.DENIED, SourceGames.SOURCESDKBASE2013MP).ShowDialog();
                this.Enabled = true;
                return;
            }
            else
            {
                Properties.Settings.Default.SourceSDKBasePath = path;
                Properties.Settings.Default.Save();
                new Form_InfoBox(FormType.GamePathValid, FormIconType.SOUND, FormSoundType.SUCCESS).ShowDialog();
                SwitchToWindow(new Form_DownloadWindow());
            }
        }
    }
}
