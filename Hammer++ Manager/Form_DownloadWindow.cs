using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal partial class Form_DownloadWindow : HPPM_Form
    {
        /// <summary>
        /// Indicates if we should simply override files from the downloaded zip, or delete everything.
        /// </summary>
        private bool isUpdate;
        internal Form_DownloadWindow(bool isUpdate = false)
        {
            InitializeComponent();
            this.isUpdate = isUpdate;


            // We don't need to prompt the user if an installation already exists.
            if (isUpdate)
                buttonDownload_Click(this, null);
        }

        internal enum DisclaimerType
        {
            Info,
            Warning,
            Error
        }
        internal void ConsoleWrite(string input, DisclaimerType type = DisclaimerType.Info)
        {
            string disclaimer;
            switch (type)
            {
                case DisclaimerType.Info:
                    disclaimer = "[Info] ";
                    break;
                case DisclaimerType.Warning:
                    disclaimer = "[Warning] ";
                    sfxCaution.Play();
                    break;
                case DisclaimerType.Error:
                    disclaimer = "[Error] ";
                    sfxDenied.Play();
                    break;
                default:
                    disclaimer = "";
                    break;
            }
            textboxLog.AppendText(disclaimer + input + "\r\n");
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            DateTime currentDateTime = DateTime.Now;
            saveFileDialog.FileName = "hppmngr_downloadlog_" + currentDateTime.ToString("ddMMyyyy_HHmmss") + ".txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, textboxLog.Text);
                MessageBox.Show("Text saved to file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Main download script
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            textboxLog.Clear();
            // Inform the user explicity that Hammer++ will be formatted to reinstall.
            if (!isUpdate)
            {
                if (HPPInstallationCheck())
                {
                    ConsoleWrite("Existing Hammer++ Installation Detected! Prompting user...", DisclaimerType.Warning);

                    // Stop the download if the user doesn't want us to delete the existing Hammer++ installation
                    if (!new Form_InfoBox(FormType.HPPAlreadyInstalled, FormIconType.LIGHT, FormSoundType.INFO).ShowDialog())
                    {
                        this.Enabled = true;
                        return;
                    }

                    // Now we delete the installation
                    ConsoleWrite("User chose to continue the installation, deleting previous Hammer++ installation...");

                    // Wrapped in try catch incase the user left Hammer++ open
                    try
                    {
                        // Delete Hammer++ Folder
                        if (Directory.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus"))
                            Directory.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus", true);

                        // Delete Hammer++ Exectuable
                        if (File.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus.exe"))
                            File.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus.exe");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ConsoleWrite("Failed to delete existing files! Check if Hammer++ is currently open!", DisclaimerType.Error);
                        this.Enabled = true;
                        return;
                    }
                }
                else
                    ConsoleWrite("No previous Hammer++ installation detected, continuing...");

                // Check to see if we can even install Hammer++
                if (GetTotalFreeSpace(Properties.Settings.Default.SourceSDKBasePath.Substring(0, 3)) < 50000000)
                {
                    ConsoleWrite("Not enough free space to install Hammer++ on " + Properties.Settings.Default.SourceSDKBasePath.Substring(0, 3) + "\r\n" +
                        "Please free up space on the drive before continuing!", DisclaimerType.Error);
                    this.Enabled = true;
                    return;
                }
                else
                    ConsoleWrite("Drive has enough space to install on, continuing...");


                // Check if internet is working
                if (!CheckInternetConnection())
                {
                    ConsoleWrite("Cannot connect to GitHub! Check your internet connection or see if GitHub is down!", DisclaimerType.Error);
                    this.Enabled = true;
                    return;
                }
                else
                    ConsoleWrite("Successfully pinged GitHub, continuing...");


                // Finally, let's get the file and extract it!
                using (WebClient WebReq = new WebClient())
                {
                    // Get Zip URL
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    WebReq.Headers.Add("User-Agent: rmod8-hammerpp_manager");
                    string jsonString;
                    using (Stream stream = WebReq.OpenRead(@"https://api.github.com/repos/ficool2/HammerPlusPlus-Website/releases/latest"))
                    {
                        StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                        jsonString = reader.ReadToEnd();
                    }
                    ConsoleWrite("API Connection Successful!");
                    API_Request rqst = JsonConvert.DeserializeObject<API_Request>(jsonString);
                    ConsoleWrite("Got current version! Version Tag: " + rqst.tag_name + ", downloading...");

                    // If this is true then it we shouldn't already be here, but better safe than sorry.
                    // If the tag got from GitHub is already saved, it means it's already installed
                    if(Properties.Settings.Default.CurrentTag == rqst.tag_name && isUpdate)
                    {
                        ConsoleWrite("Latest Version Already Installed!", DisclaimerType.Error);
                        this.Enabled = true;
                        return;
                    }

                    // Save tag to use to check if we need to update.
                    Properties.Settings.Default.CurrentTag = rqst.tag_name;
                    Properties.Settings.Default.Save();

                    // Download ZIP and put into memory stream
                    using (MemoryStream zipMS = new MemoryStream(WebReq.DownloadData(rqst.assets[0].browser_download_url)))
                    {
                        ConsoleWrite("Downloaded successfully, extracting...");
                        zipMS.Position = 0;
                        using (ZipArchive archive = new ZipArchive(zipMS, ZipArchiveMode.Read))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.Name == "README.txt")
                                    continue;
                                string extractedFilePath = Properties.Settings.Default.SourceSDKBasePath + entry.FullName.Substring(entry.FullName.IndexOf('/'));

                                // If the entry is a directory listing, we make the directory then continue...
                                if (extractedFilePath[extractedFilePath.Length - 1] == '/')
                                {
                                    if (!Directory.Exists(extractedFilePath))
                                    {
                                        Directory.CreateDirectory(extractedFilePath);
                                        continue;
                                    }
                                }
                                else
                                    entry.ExtractToFile(extractedFilePath);
                            }
                        }
                    }

                }

                // Installation finished, now to configure it.
                ConsoleWrite("Hammer++ files installed!");
                this.Enabled = true;
                buttonDownload.Enabled = false;
                buttonNext.Enabled = true;
                new Form_InfoBox(FormType.OpenHammerPPToConfigure, FormIconType.SOUND, FormSoundType.SUCCESS).ShowDialog();
                Process.Start(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus.exe");

            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg"))
            {
                //User didn't listen!
                new Form_InfoBox(FormType.OpenHammerPPToConfigure, FormIconType.SOUND, FormSoundType.SUCCESS).ShowDialog();
                Process.Start(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus.exe");
            }
            else
            {
                //Continue

                // Add compile profiles to the Hammer++ Installation
                List<string> sequencesLines = File.ReadLines(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg").ToList();
                // Remove closing bracket
                sequencesLines.RemoveAt(sequencesLines.Count-1);

                foreach(MasterSequence masterSequence in MasterSequence.HPPSequences)
                {
                    sequencesLines.Add(masterSequence.sequenceName);
                    sequencesLines.Add("\t{");
                    for (int i = 0; i < masterSequence.sequences.Length; i++)
                    {
                        sequencesLines.Add("\t\t\""+(i+1)+"\"");
                        sequencesLines.Add("\t\t{");
                        sequencesLines.Add(masterSequence.sequences[i].enable);
                        sequencesLines.Add(masterSequence.sequences[i].specialcmd);
                        if(masterSequence.sequences[i].run != null)
                            sequencesLines.Add(masterSequence.sequences[i].run);
                        sequencesLines.Add(masterSequence.sequences[i].parms);
                        sequencesLines.Add("\t\t}");
                    }
                    sequencesLines.Add("\t}");
                }
                sequencesLines.Add("}");
                using (TextWriter tw = new StreamWriter(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_sequences.cfg"))
                {
                    foreach (String s in sequencesLines)
                        tw.WriteLine(s);
                }

                Properties.Settings.Default.HPPInstalled = true;
                Properties.Settings.Default.Save();
                SwitchToWindow(new Form_MainWindow());
            }
        }
    }

    
}
