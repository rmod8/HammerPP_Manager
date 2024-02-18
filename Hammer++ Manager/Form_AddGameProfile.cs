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
    internal partial class Form_AddGameProfile : HPPM_Form
    {
        List<Tuple<SourceGames.SourceGame, string>> FoundSourceGames = new List<Tuple<SourceGames.SourceGame, string>>();
        List<SourceGames.SourceGame> ExistingProfiles = new List<SourceGames.SourceGame>();

        internal Form_AddGameProfile(List<SourceGames.SourceGame> Profiles)
        {
            InitializeComponent();
            ExistingProfiles = Profiles;
            sfxCaution.Play();
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            FoundSourceGames.Clear();

            // We're gonna use OpenFileDialog because OpenFolderDialog sucks

            // Set up OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Source SDK Base 2013 Multiplayer Directory...";
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = "[Folder Selection]";

            // If the user didn't click the OK button, we return...
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            //Find all possible source games from the selected directory.
            //Remember, multiple Source Games can exist within one directory
            foreach(SourceGames.SourceGame game in SourceGames.SUPPORTED_GAMES)
            {
                if (GameSanityCheck(Path.GetDirectoryName(openFileDialog.FileName), game))
                    FoundSourceGames.Add(new Tuple<SourceGames.SourceGame, string>(game, Path.GetDirectoryName(openFileDialog.FileName)));
            }

            //If no games found, show a info box and return.
            if (FoundSourceGames.Count < 1)
            {
                new Form_InfoBox(FormType.NoGamesFoundAuto, FormIconType.OBSOLETE, FormSoundType.DENIED).ShowDialog();
                return;
            }
            //If only found one game, add it.
            else if(FoundSourceGames.Count == 1)
            {
                CheckIfGameProfileAlreadyExists(FoundSourceGames[0]);
            }
            //If found more than one game, let the user choose what they want to add.
            else
            {
                //Prompt the user to select from the list of all games found.
                Tuple<SourceGames.SourceGame, string> selectedGame = new Form_SelectFoundGames(FoundSourceGames).ShowDialog();
                //If they didn't select anything, return
                if (selectedGame == null) return;

                CheckIfGameProfileAlreadyExists(selectedGame);
            }
            openFileDialog.Dispose();
        }

        private void buttonAuto_Click(object sender, EventArgs e)
        {
            FoundSourceGames.Clear();

            // For each supported game...
            for (int i = 0; i < SourceGames.SUPPORTED_GAMES.Length; i++)
            {
                // Try to find the game's path from the system
                string path = ScanForGameDirectory(SourceGames.SUPPORTED_GAMES[i]);

                // If string returned is valid, that means a valid path got found, add it to the list alongside the game it represents.
                if (path != null)
                    FoundSourceGames.Add(new Tuple<SourceGames.SourceGame, string>(SourceGames.SUPPORTED_GAMES[i], path));
            }

            //If no games found, show a info box and return.
            if (FoundSourceGames.Count < 1)
            {
                new Form_InfoBox(FormType.NoGamesFoundAuto, FormIconType.OBSOLETE, FormSoundType.DENIED).ShowDialog();
                return;
            }

            //Prompt the user to select from the list of all games found.
            Tuple<SourceGames.SourceGame, string> selectedGame = new Form_SelectFoundGames(FoundSourceGames).ShowDialog();

            //If they didn't select anything, return
            if (selectedGame == null) return;
            CheckIfGameProfileAlreadyExists(selectedGame);
        }


        // Checks if the inputted profile already exists in ExistingProfiles
        private void CheckIfGameProfileAlreadyExists(Tuple<SourceGames.SourceGame, string> Profile)
        {
            for(int i = 0; i < ExistingProfiles.Count; i++)
            {
                if (ExistingProfiles[i].ProfileNametag == Profile.Item1.ProfileNametag)
                {
                    new Form_InfoBox(FormType.ProfileAlreadyInstalled, FormIconType.LIGHT, FormSoundType.INFO).ShowDialog();
                    return;
                }
            }

            this.Enabled = false;
            bool DirectoryAlreadyExists = false;
            bool ConfigEntryAlreadyExists = false;

            // Check if the game already exists...
            if (Directory.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag))
                DirectoryAlreadyExists = true;

            // Check if config already exists...
            List<string> GameConfigLines = GetGameConfigLines();
            for (int i = 0; i < GameConfigLines.Count; i++)
            {
                if (GameConfigLines[i] == "\t\t\"" + Profile.Item1.GameName + "\"")
                {
                    ConfigEntryAlreadyExists = true;
                    break;
                }
            }

            // If theres any residue data, inform the user. If they say yes, the files will be deleted, if not, it returns.
            if (DirectoryAlreadyExists || ConfigEntryAlreadyExists)
            {
                if (!new Form_InfoBox(FormType.ProfileResidueData, FormIconType.LIGHT, FormSoundType.CAUTION).ShowDialog())
                {
                    this.Enabled = true;
                    return;
                }

                if (ConfigEntryAlreadyExists)
                {
                    for (int i = 0; i < GameConfigLines.Count; i++)
                    {
                        if (GameConfigLines[i] == "\t\t\"" + Profile.Item1.GameName + "\"")
                        {
                            while (GameConfigLines[i] != "\t\t}")
                                GameConfigLines.RemoveAt(i);
                            GameConfigLines.RemoveAt(i);
                            break;
                        }
                    }

                    using (FileStream fs = File.Create(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr"))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            for (int i = 0; i < GameConfigLines.Count; i++)
                            {
                                for (int x = 0; x < GameConfigLines[i].Length; x++)
                                {
                                    bw.Write(GameConfigLines[i][x]);
                                }
                                bw.Write('\r');
                                bw.Write('\n');
                            }
                        }
                    }

                    // If the original txt file is open, this prevents misery
                    File.Copy(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr", Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", true);
                    File.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr");

                }

                if (DirectoryAlreadyExists)
                    if (Directory.Exists(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag))
                        Directory.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag, true);
            }

            // Time to finally set the game profile up

            // First, make the directory...
            Directory.CreateDirectory(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag);

            // Then right the game info file
            using (FileStream fs = File.Create(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag + "\\gameinfo.txt.hppmngr"))
            {
                List<string> Buffer = new List<string>();

                // Boilerplate shite!
                Buffer.Add("\"GameInfo\"");
                Buffer.Add("{");
                Buffer.Add("\tFileSystem");
                Buffer.Add("\t{");
                Buffer.Add("\t\tSearchPaths");
                Buffer.Add("\t\t{");
                Buffer.Add("\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_textures.vpk");
                Buffer.Add("\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_sound_misc.vpk");
                Buffer.Add("\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_misc.vpk");
                Buffer.Add("\t\t\tplatform\t\t\t|all_source_engine_paths|platform/platform_misc.vpk");
                Buffer.Add("\t\t\tgamebin\t\t\t\"" + Profile.Item2 + "\\bin\"");

                switch (Profile.Item1.SteamAppID)
                {
                    // GarrysMod
                    case "4000":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\platform\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\platform\\*_dir.vpk\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\sourceengine\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\sourceengine\\*_dir.vpk\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\garrysmod\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\garrysmod\\*_dir.vpk\"");
                        break;

                    // FORTRESS_FOREVER
                    case "253530":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\FortressForever\"");
                        break;

                    // HALF_LIFE_2
                    case "220":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\hl2\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\hl2\\*_dir.vpk\"");
                        break;

                    // HALF_LIFE_2_EP1
                    case "380":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\episodic\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\episodic\\*_dir.vpk\"");
                        break;

                    // HALF_LIFE_2_EP2
                    case "420":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\ep2\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\ep2\\*_dir.vpk\"");
                        break;

                    // FOF
                    case "265630":
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\" + Profile.Item1.GamePath + "\"");
                        Buffer.Add("\t\t\tgame\t\t\t\"" + Profile.Item2 + "\\" + Profile.Item1.GamePath + "\\fof_dir.vpk" + "\"");
                        break;
                }
                Buffer.Add("\t\t}");
                Buffer.Add("\t}");
                Buffer.Add("}");

                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    for (int i = 0; i < Buffer.Count; i++)
                    {
                        for (int x = 0; x < Buffer[i].Length; x++)
                        {
                            bw.Write(Buffer[i][x]);
                        }
                        bw.Write('\r');
                        bw.Write('\n');
                    }
                }
            }

            File.Copy(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag + "\\gameinfo.txt.hppmngr", Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag + "\\gameinfo.txt");
            File.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag + "\\gameinfo.txt.hppmngr");

            // GameInfo Written!
            // Time to write to gameconfig

            List<string> GameConfigBuffer = new List<string>();
            GameConfigBuffer.Add("\t\t\"" + Profile.Item1.GameName + "\"");
            GameConfigBuffer.Add("\t\t{");
            GameConfigBuffer.Add("\t\t\t\"GameDir\"\t\t\"" + Properties.Settings.Default.SourceSDKBasePath + "\\" + Profile.Item1.ProfileNametag + "\"");
            GameConfigBuffer.Add("\t\t\t\"Hammer\"");
            GameConfigBuffer.Add("\t\t\t{");

            string[] FGDS = Directory.GetFiles(Profile.Item2 + Profile.Item1.SDKFolder, "*.fgd", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < FGDS.Length; i++)
            {
                GameConfigBuffer.Add("\t\t\t\t\"GameData" + i + "\"\t\t\"" + FGDS[i] + "\"");
            }

            GameConfigBuffer.Add("\t\t\t\t\"TextureFormat\"\t\t\"5\"");
            GameConfigBuffer.Add("\t\t\t\t\"MapFormat\"\t\t\"4\"");
            GameConfigBuffer.Add("\t\t\t\t\"DefaultTextureScale\"\t\t\"0.250000\"");
            GameConfigBuffer.Add("\t\t\t\t\"DefaultLightmapScale\"\t\t\"16\"");
            GameConfigBuffer.Add("\t\t\t\t\"GameExe\"\t\t\"" + Profile.Item2 + Profile.Item1.GameExe + "\"");
            GameConfigBuffer.Add("\t\t\t\t\"DefaultSolidEntity\"\t\t\"func_detail\"");
            GameConfigBuffer.Add("\t\t\t\t\"DefaultPointEntity\"\t\t\"info_player_start\"");
            GameConfigBuffer.Add("\t\t\t\t\"BSP\"\t\t\"" + Properties.Settings.Default.SourceSDKBasePath + "\\bin\\vbsp.exe\"");
            GameConfigBuffer.Add("\t\t\t\t\"Vis\"\t\t\"" + Properties.Settings.Default.SourceSDKBasePath + "\\bin\\vvis.exe\"");
            GameConfigBuffer.Add("\t\t\t\t\"Light\"\t\t\"" + Properties.Settings.Default.SourceSDKBasePath + "\\bin\\vrad.exe\"");
            GameConfigBuffer.Add("\t\t\t\t\"GameExeDir\"\t\t\"" + Profile.Item2 + "\\" + Profile.Item1.GamePath + "\"");
            GameConfigBuffer.Add("\t\t\t\t\"MapDir\"\t\t\"" + Profile.Item2 + "\\" + Profile.Item1.GamePath + "\\mapsrc\"");
            GameConfigBuffer.Add("\t\t\t\t\"BSPDir\"\t\t\"" + Profile.Item2+"\\"+Profile.Item1.GamePath + "\\maps\"");
            GameConfigBuffer.Add("\t\t\t\t\"PrefabDir\"\t\t\"" + Profile.Item2 + "\\bin\\prefabs\"");
            GameConfigBuffer.Add("\t\t\t\t\"CordonTexture\"\t\t\"tools/toolsskybox2d\"");
            GameConfigBuffer.Add("\t\t\t\t\"MaterialExcludeCount\"\t\t\"0\"");
            GameConfigBuffer.Add("\t\t\t}");
            GameConfigBuffer.Add("\t\t}");

            GameConfigLines = GetGameConfigLines();
            for (int i = 0; i < GameConfigLines.Count; i++)
            {
                if (GameConfigLines[i] == "\t{")
                {
                    i++;
                    GameConfigLines.InsertRange(i, GameConfigBuffer);
                    break;
                }
            }

            using (FileStream fs = File.Create(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr"))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    for (int i = 0; i < GameConfigLines.Count; i++)
                    {
                        for (int x = 0; x < GameConfigLines[i].Length; x++)
                        {
                            bw.Write(GameConfigLines[i][x]);
                        }
                        bw.Write('\r');
                        bw.Write('\n');
                    }
                }
            }

            // If the original txt file is open, this prevents misery
            File.Copy(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr", Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", true);
            File.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt.hppmngr");

            // Close this form if the selected profile is unique
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
