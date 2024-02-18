// Template form for all forms used in this program.
// Applies dark theme, adds sounds, common methods, etc.

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
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
            NoGamesFoundAuto,
            /// <summary>
            /// No games found from manual selection.
            /// </summary>
            NoGamesFoundManual,
            /// <summary>
            /// Hammer++ Installation is not valid.
            /// </summary>
            HammerPPInstallationInvalid,
            /// <summary>
            /// If the user tries to click on the install button, inform them if they want to continue.
            /// </summary>
            InstallHPPAlreadyInstalled,
            /// <summary>
            /// No Internet Connection
            /// </summary>
            NoInternet,
            /// <summary>
            /// Profile Residue Data
            /// </summary>
            ProfileResidueData,
            /// <summary>
            /// Delete Profile
            /// </summary>
            DeleteProfile
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

                // FORTRESS_FOREVER
                case "253530":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\FortressForever",
                        "\\platform",
                        "\\hl2"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\FortressForever\\gameinfo.txt"
                        };
                    break;

                // HALF_LIFE_2
                case "220":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\hl2"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\bin\\vbsp.exe",
                        "\\bin\\vvis.exe",
                        "\\bin\\vrad.exe",
                        "\\hl2\\hl2_misc_000.vpk",
                        "\\hl2\\hl2_misc_001.vpk",
                        "\\hl2\\hl2_misc_002.vpk",
                        "\\hl2\\hl2_misc_003.vpk",
                        "\\hl2\\hl2_misc_dir.vpk",
                        "\\hl2\\hl2_pak_000.vpk",
                        "\\hl2\\hl2_pak_dir.vpk",
                        "\\hl2\\hl2_sound_misc_000.vpk",
                        "\\hl2\\hl2_sound_misc_001.vpk",
                        "\\hl2\\hl2_sound_misc_002.vpk",
                        "\\hl2\\hl2_sound_misc_dir.vpk",
                        "\\hl2\\gameinfo.txt"
                        };
                    break;

                // HALF_LIFE_2_EP1
                case "380":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\episodic"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\bin\\vbsp.exe",
                        "\\bin\\vvis.exe",
                        "\\bin\\vrad.exe",
                        "\\episodic\\ep1_pak_000.vpk",
                        "\\episodic\\ep1_pak_001.vpk",
                        "\\episodic\\ep1_pak_002.vpk",
                        "\\episodic\\ep1_pak_003.vpk",
                        "\\episodic\\ep1_pak_004.vpk",
                        "\\episodic\\ep1_pak_005.vpk",
                        "\\episodic\\ep1_pak_006.vpk",
                        "\\episodic\\ep1_pak_007.vpk",
                        "\\episodic\\ep1_pak_008.vpk",
                        "\\episodic\\ep1_pak_009.vpk",
                        "\\episodic\\ep1_pak_dir.vpk",
                        "\\episodic\\gameinfo.txt"
                        };
                    break;

                // HALF_LIFE_2_EP2
                case "420":
                    DirCheckList = new string[]{
                        "\\bin",
                        "\\ep2"
                        };

                    FileCheckList = new string[]{
                        "\\hl2.exe",
                        "\\bin\\vbsp.exe",
                        "\\bin\\vvis.exe",
                        "\\bin\\vrad.exe",
                        "\\ep2\\ep2_pak_000.vpk",
                        "\\ep2\\ep2_pak_001.vpk",
                        "\\ep2\\ep2_pak_002.vpk",
                        "\\ep2\\ep2_pak_003.vpk",
                        "\\ep2\\ep2_pak_004.vpk",
                        "\\ep2\\ep2_pak_005.vpk",
                        "\\ep2\\ep2_pak_006.vpk",
                        "\\ep2\\ep2_pak_007.vpk",
                        "\\ep2\\ep2_pak_008.vpk",
                        "\\ep2\\ep2_pak_009.vpk",
                        "\\ep2\\ep2_pak_010.vpk",
                        "\\ep2\\ep2_pak_011.vpk",
                        "\\ep2\\ep2_pak_012.vpk",
                        "\\ep2\\ep2_pak_013.vpk",
                        "\\ep2\\ep2_pak_014.vpk",
                        "\\ep2\\ep2_pak_015.vpk",
                        "\\ep2\\ep2_pak_016.vpk",
                        "\\ep2\\ep2_pak_017.vpk",
                        "\\ep2\\ep2_pak_018.vpk",
                        "\\ep2\\ep2_pak_019.vpk",
                        "\\ep2\\ep2_pak_dir.vpk",
                        "\\ep2\\gameinfo.txt"
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Returns the directory of the installation of the game put in the parameters. Returns null if it can't be found.
        /// </summary>
        /// <param name="sourceGame"></param>
        /// <returns></returns>
        public static string ScanForGameDirectory(SourceGames.SourceGame sourceGame)
        {
            // Check the default places first...

            if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\" + sourceGame.GameDirectory) && GameSanityCheck(@"C:\Program Files (x86)\Steam\steamapps\common\" + sourceGame.GameDirectory, sourceGame))
                return @"C:\Program Files (x86)\Steam\steamapps\common\" + sourceGame.GameDirectory;

            if (Directory.Exists(@"C:\Program Files\Steam\steamapps\common\" + sourceGame.GameDirectory) && GameSanityCheck(@"C:\Program Files\Steam\steamapps\common\" + sourceGame.GameDirectory, sourceGame))
                return @"C:\Program Files\Steam\steamapps\common\" + sourceGame.GameDirectory;

            // Ok, we couldn't find it on the default C: drive, time to check the other drives...

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                // Don't try to access drives which we can't access, like optical drives or RAM.
                if (!drive.IsReady || (drive.DriveType == DriveType.Ram || drive.DriveType == DriveType.Unknown || drive.DriveType == DriveType.CDRom))
                    continue;

                // Try to find on root of drive...
                if (Directory.Exists(drive.Name + @"SteamLibrary\steamapps\common\" + sourceGame.GameDirectory) && GameSanityCheck(drive.Name + @"SteamLibrary\steamapps\common\" + sourceGame.GameDirectory, sourceGame))
                    return drive.Name + @"SteamLibrary\steamapps\common\" + sourceGame.GameDirectory;

                //Sometimes people put their SteamLibrary folder in another folder, so let's check each folder...
                string[] folders = Directory.GetDirectories(drive.Name);
                foreach (string folder in folders)
                {
                    if (Directory.Exists(folder + @"\SteamLibrary\steamapps\common\" + sourceGame.GameDirectory) && GameSanityCheck(folder + @"\SteamLibrary\steamapps\common\" + sourceGame.GameDirectory, sourceGame))
                        return folder + @"\SteamLibrary\steamapps\common\" + sourceGame.GameDirectory;
                }
            }

            // If all else fails, return null and hope the caller checks the condition and realises the game doesn't exist.
            return null;
        }

        /// <summary>
        /// Checks if Properties.Settings.Default.SourceSDKBasePath has a Hammer++ installation or an residual files.
        /// </summary>
        /// <returns></returns>
        internal static bool HPPInstallationCheck()
        {
            return Directory.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus") || File.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus.exe");
        }

        /// <summary>
        /// Returns true if the device is connected to the internet, false if not.
        /// </summary>
        /// <returns></returns>
        internal static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://github.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns amount of free space in bytes on the specified drive.
        /// </summary>
        /// <param name="driveName"></param>
        /// <returns></returns>
        internal long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.AvailableFreeSpace;
                }
            }
            return -1;
        }

    }

    /// <summary>
    /// Class used for the GitHub API request
    /// </summary>
    public class API_Request
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public string id { get; set; }
        public API_author author { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public string draft { get; set; }
        public string prerelease { get; set; }
        public string created_at { get; set; }
        public string published_at { get; set; }
        public IList<API_release> assets { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }

    public class API_author
    {
        public string login { get; set; }
        public string id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public string site_admin { get; set; }
    }

    public class API_release
    {
        public string url { get; set; }
        public string id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public API_author uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public string size { get; set; }
        public string download_count { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string browser_download_url { get; set; }
    }

}
