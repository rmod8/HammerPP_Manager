// Template form for all forms used in this program.
// Applies dark theme, adds sounds, common methods, etc.

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Gsemac.Forms.Styles.Applicators;
using Gsemac.Forms.Styles.StyleSheets;

namespace HammerPP_Manager
{
    internal class HPPM_Form : Form
    {
        internal HPPM_Form()
        {
            // Applies style to the form.
            applicator.ApplyStyles(this);
        }

        // Setup static SoundPlayers for the sounds used in this program.
        internal static SoundPlayer sfxCaution = new SoundPlayer(Properties.Resources.caution);
        internal static SoundPlayer sfxDenied = new SoundPlayer(Properties.Resources.denied);
        internal static SoundPlayer sfxInfo = new SoundPlayer(Properties.Resources.info);
        internal static SoundPlayer sfxSuccess = new SoundPlayer(Properties.Resources.success);

        /// <summary>
        /// Used for InfoBoxes, to specify what info to show.
        /// </summary>
        internal enum FormType
        {
            /// <summary>
            /// User hasn't set their SDK path.
            /// </summary>
            SDKPathNotSelected,
            /// <summary>
            /// Hammer++ already installed.
            /// </summary>
            HPPAlreadyInstalled,
            /// <summary>
            /// Profile for the game already exists.
            /// </summary>
            ProfileAlreadyInstalled,
            /// <summary>
            /// Game Path not found using Automatic Search.
            /// </summary>
            GamePathNotFoundAuto,
            /// <summary>
            /// Game Path not found using Manual Selection.
            /// </summary>
            GamePathNotFoundManual,
            /// <summary>
            /// Game Path is valid.
            /// </summary>
            GamePathValid,
            /// <summary>
            /// User needs to open Hammer++ to configure profiles.
            /// </summary>
            OpenHammerPPToConfigure,
            /// <summary>
            /// No games found from autoscan.
            /// </summary>
            NoGamesFoundAuto
        }

        /// <summary>
        /// Used for InfoBoxes, to specify their icon.
        /// </summary>
        internal enum FormIconType
        {
            /// <summary>
            /// Show GLOBAL icon.
            /// </summary>
            GLOBAL,
            /// <summary>
            /// Show LIGHT icon.
            /// </summary>
            LIGHT,
            /// <summary>
            /// Show OBSOLETE icon.
            /// </summary>
            OBSOLETE,
            /// <summary>
            /// Show SOUND icon.
            /// </summary>
            SOUND
        }

        /// <summary>
        /// Used for InfoBoxes, to specify their sound.
        /// </summary>
        internal enum FormSoundType
        {
            CAUTION,
            DENIED,
            INFO,
            SUCCESS
        }

        // Used to setup DarkUI theme for the program
        static IStyleSheet styleSheet = StyleSheet.FromStream(GenerateStreamFromString(Properties.Resources.DarkUI));
        static IStyleApplicator applicator = new UserPaintStyleApplicator(styleSheet);
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Returns wether input path is valid for the SourceGame inputted
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        internal static bool GameSanityCheck(string basePath, SourceGames.SourceGame sourcegame)
        {
            string[] DirCheckList;
            string[] FileCheckList;

            switch (sourcegame.SteamAppID)
            {
                // GARRYSMOD
                case "4000":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\garrysmod",
                        "\\platform",
                        "\\sourceengine"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\bin\\vbsp.exe",
                        "\\bin\\vvis.exe",
                        "\\bin\\vrad.exe",
                        "\\garrysmod\\garrysmod_000.vpk",
                        "\\garrysmod\\garrysmod_001.vpk",
                        "\\garrysmod\\garrysmod_002.vpk",
                        "\\garrysmod\\garrysmod_dir.vpk",
                        "\\garrysmod\\gameinfo.txt"
                        };
                    break;

                // FOF
                case "265630":
                    DirCheckList = new string[]{
                        "\\ds",
                        "\\fof",
                        "\\linux",
                        "\\mac",
                        "\\sdk"
                        };

                    FileCheckList = new string[]{
                        "\\sdk\\hl2.exe",
                        "\\fof\\fof_000.vpk",
                        "\\fof\\fof_001.vpk",
                        "\\fof\\fof_002.vpk",
                        "\\fof\\fof_003.vpk",
                        "\\fof\\fof_004.vpk",
                        "\\fof\\fof_005.vpk",
                        "\\fof\\fof_dir.vpk",
                        "\\fof\\gameinfo.txt"
                        };
                    break;

                // SDK BASE 2013 MP
                case "243750":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\hl2",
                        "\\hl2mp",
                        "\\platform",
                        "\\sdktools",
                        "\\sourcetest"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\bin\\vbsp.exe",
                        "\\bin\\vvis.exe",
                        "\\bin\\vrad.exe"
                        };
                    break;

                default:
                    return false;
            }

            for (int i = 0; i < DirCheckList.Length; i++)
            {
                if (!Directory.Exists(basePath + DirCheckList[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < FileCheckList.Length; i++)
            {
                if (!File.Exists(basePath + FileCheckList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Closes this form and opens the form passed in.
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="windowToOpen"></param>
        internal void SwitchToWindow(Form windowToOpen)
        {
            this.Hide();
            windowToOpen.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Check if Hammer++ is open.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static bool IsHPPOpen()
        {
            // Get all running processes with the specified name
            Process[] processes = Process.GetProcessesByName("hammerplusplus");

            // If any process with the given name is found, return true; otherwise, return false
            return processes.Length > 0;
        }


        internal static List<string> GetGameConfigLines()
        {
            List<string> Lines = new List<string>();
            using (MemoryStream MemoryStream = new MemoryStream())
            {
                using (FileStream FileStream = File.OpenRead(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt"))
                {
                    FileStream.CopyTo(MemoryStream);
                    MemoryStream.Position = 0;
                }

                using (BinaryReader BinaryReader = new BinaryReader(MemoryStream))
                {
                    string CurrentLine = "";
                    while (MemoryStream.Position != MemoryStream.Length)
                    {
                        char CurrentChar = BinaryReader.ReadChar();
                        switch (CurrentChar)
                        {
                            case '\n':
                                if (CurrentLine.Length > 0)
                                {
                                    Lines.Add(CurrentLine);
                                    CurrentLine = "";
                                }
                                break;

                            case '\r':
                                break;

                            default:
                                CurrentLine += CurrentChar;
                                break;
                        }
                    }
                }
            }
            return Lines;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HPPM_Form));
            this.SuspendLayout();
            // 
            // HPPM_Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HPPM_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }
    }
}
