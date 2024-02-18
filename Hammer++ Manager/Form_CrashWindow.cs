using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal partial class Form_CrashWindow : HPPM_Form
    {
        internal Form_CrashWindow(Exception error)
        {
            InitializeComponent();
            textboxCrashLog.AppendText("Hammer++ Manager v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            textboxCrashLog.AppendText(Environment.NewLine + Environment.NewLine);
            textboxCrashLog.AppendText("Exception Type: " + error.GetType().FullName);
            textboxCrashLog.AppendText(Environment.NewLine + Environment.NewLine);
            textboxCrashLog.AppendText("Exception Description: " + error.Message);
            textboxCrashLog.AppendText(Environment.NewLine + Environment.NewLine);
            textboxCrashLog.AppendText("Stack Trace:" + Environment.NewLine);
            textboxCrashLog.AppendText(error.StackTrace);
            textboxCrashLog.AppendText(Environment.NewLine + Environment.NewLine + "Program Settings:" + Environment.NewLine);
            textboxCrashLog.AppendText("CurrentTag: " + Properties.Settings.Default.CurrentTag + Environment.NewLine);
            textboxCrashLog.AppendText("SourceSDKBasePath: " + Properties.Settings.Default.SourceSDKBasePath + Environment.NewLine);
            textboxCrashLog.AppendText(Environment.NewLine + Environment.NewLine + "Running Environment Specifications:" + Environment.NewLine);
            textboxCrashLog.AppendText("OS: " + System.Runtime.InteropServices.RuntimeInformation.OSDescription.ToString() + Environment.NewLine);
            textboxCrashLog.AppendText("OS Architecture: " + System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString() + Environment.NewLine);

            //Get DriveInfo for all the drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    textboxCrashLog.AppendText("Drive " + drive.Name.Substring(0, 1) + ": " + drive.TotalFreeSpace + " bytes free (" + drive.TotalFreeSpace / 1000000 + " MBs)" + Environment.NewLine);
                }
            }
            textboxCrashLog.AppendText("Used Memory: " + Process.GetCurrentProcess().PrivateMemorySize64 + " (" + Process.GetCurrentProcess().PrivateMemorySize64 / 1000000 + " MBs)");
        }

        private void buttonSaveDump_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            DateTime currentDateTime = DateTime.Now;
            saveFileDialog.FileName = "hppmngr_" + currentDateTime.ToString("ddMMyyyy_HHmmss") + ".txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, textboxCrashLog.Text);
                    MessageBox.Show("Text saved to file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
