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
using static System.Windows.Forms.LinkLabel;

namespace HammerPP_Manager
{
    internal partial class Form_EditSources : HPPM_Form
    {
        private SourceGames.SourceGame game;
        private List<string> sources = new List<string>();
        internal Form_EditSources(SourceGames.SourceGame inputGame)
        {
            InitializeComponent();
            game = inputGame;
            UpdateSourcesListings();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateGameConfig()
        {
            string gameBin = null;

            string[] gameInfoLines = File.ReadAllLines(Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt");
            foreach (string line in gameInfoLines)
            {
                if(line.Length > 9 && line.Substring(0, 10) == "\t\t\tgamebin")
                {
                    gameBin = line;
                    break;
                }
            }

            // Throw exception if gamebin doesn't exist, meaning the user did a big nono and modified the file beyond function.
            if (gameBin == null) throw new DataException("Game Info for " + game.GameName+ " does not contain a 'gamebin' listing!");

            using (FileStream fs = File.Create(Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt.hppmngr"))
            {
                List<string> Buffer = new List<string>
                {
                    "\"GameInfo\"",
                    "{",
                    "\tFileSystem",
                    "\t{",
                    "\t\tSearchPaths",
                    "\t\t{",
                    "\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_textures.vpk",
                    "\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_sound_misc.vpk",
                    "\t\t\tgame\t\t\t\t|all_source_engine_paths|hl2/hl2_misc.vpk",
                    "\t\t\tplatform\t\t\t|all_source_engine_paths|platform/platform_misc.vpk",
                    gameBin
                };

                foreach (string source in sources)
                    Buffer.Add("\t\t\tgame\t\t\t\t\"" + source + "\"");

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

            File.Copy(Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt.hppmngr", Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt", true);
            File.Delete(Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt.hppmngr");
        }

        private void UpdateSourcesListings()
        {
            this.Enabled = false;

            // Clear list
            sources = new List<string>();

            string[] gameInfoLines = File.ReadAllLines(Properties.Settings.Default.SourceSDKBasePath + "\\" + game.ProfileNametag + "\\gameinfo.txt");
            for(int i = 0; i < gameInfoLines.Length; i++)
            {
                string buffer = "";
                for(int x = 0; x < gameInfoLines[i].Length; x++)
                {
                    char currentLetter = gameInfoLines[i][x];
                    switch (currentLetter)
                    {
                        case '\t':
                            break;

                        default:
                            buffer += currentLetter;
                            break;
                    }
                }

                if (buffer.Length > 3 && buffer.Substring(0, 4) == "game" && buffer[4] != '|' && buffer[4] != 'b')
                {
                    sources.Add(buffer.Substring(5, buffer.Length - 6));
                }
            }

            // Add sources list to the visual listbox
            listboxSources.DataSource = sources;
            this.Enabled = true;
        }

        private void buttonAddFolder_Click(object sender, EventArgs e)
        {
            // We're gonna use OpenFileDialog because OpenFolderDialog sucks

            // Set up OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Folder...";
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = "[Folder Selection]";

            // If the user didn't click the OK button, we return...
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            sources.Add(Path.GetDirectoryName(openFileDialog.FileName));
            UpdateGameConfig();
            UpdateSourcesListings();
            openFileDialog.Dispose();
        }

        private void buttonAddVPKFolder_Click(object sender, EventArgs e)
        {
            // We're gonna use OpenFileDialog because OpenFolderDialog sucks

            // Set up OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Folder...";
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = "[Folder Selection]";

            // If the user didn't click the OK button, we return...
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            sources.Add(Path.GetDirectoryName(openFileDialog.FileName)+"\\*_dir.vpk");
            UpdateGameConfig();
            UpdateSourcesListings();
            openFileDialog.Dispose();
        }

        private void buttonAddVPK_Click(object sender, EventArgs e)
        {
            // Set up OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Folder...";
            openFileDialog.CheckFileExists = false;
            openFileDialog.Filter = "VPK Directory File (*_dir.vpk)|*_dir.vpk";

            // If the user didn't click the OK button, we return...
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            sources.Add(openFileDialog.FileName);
            UpdateGameConfig();
            UpdateSourcesListings();
            openFileDialog.Dispose();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listboxSources.SelectedIndex == -1) return;
            sources.RemoveAt(listboxSources.SelectedIndex);
            UpdateGameConfig();
            UpdateSourcesListings();
        }
    }
}
