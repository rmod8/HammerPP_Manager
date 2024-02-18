using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HammerPP_Manager
{
    internal static class SourceGames
    {

        internal static readonly SourceGame SOURCESDKBASE2013MP = new SourceGame("243750", "Source SDK Base 2013 Multiplayer", "Source SDK Base 2013 Multiplayer", "hl2");

        internal static readonly SourceGame GARRYSMOD = new SourceGame("4000", "Garry's Mod", "GarrysMod", "garrysmod", "gmod");
        internal static readonly SourceGame FISTFUL_OF_FRAGS = new SourceGame("265630", "Fistful of Frags", "Fistful of Frags", "fof", "fof", "\\sdk\\hl2.exe", "\\sdk\\");
        internal static readonly SourceGame TEAM_FORTRESS_2 = new SourceGame("440", "Team Fortress 2", "Team Fortress 2", "tf", "tf2");
        internal static readonly SourceGame COUNTERSTRIKE_SOURCE = new SourceGame("240", "Counter-Strike: Source", "Counter-Strike Source", "cstrike", "css");
        internal static readonly SourceGame HALF_LIFE_2 = new SourceGame("220", "Half-Life 2", "Half-Life 2", "hl2", "hl2");
        internal static readonly SourceGame HALF_LIFE_2_EP1 = new SourceGame("380", "Half-Life 2: Episode One", "Half-Life 2", "episodic", "hl2ep1");
        internal static readonly SourceGame HALF_LIFE_2_EP2 = new SourceGame("420", "Half-Life 2: Episode Two", "Half-Life 2", "ep2", "hl2ep2");

        // Represents what games to search for when adding a new profile.
        public static readonly SourceGame[] SUPPORTED_GAMES = new SourceGame[] { GARRYSMOD, FISTFUL_OF_FRAGS, TEAM_FORTRESS_2, COUNTERSTRIKE_SOURCE};

        internal class SourceGame
        {
            internal readonly string SteamAppID;
            internal readonly string GameName;
            /// <summary>
            /// The folder the game is in, NOT the game engine folder.
            /// </summary>
            internal readonly string GameDirectory;
            /// <summary>
            /// The folder used for that specific game.
            /// </summary>
            internal readonly string GamePath;
            internal readonly string GameExe;
            internal readonly string ProfileNametag;
            internal readonly string SDKFolder;

            internal SourceGame(string SteamAppID, string GameName, string GameDirectory, string GamePath, string ProfileNametag = null, string GameExe = "\\hl2.exe", string SDKFolder = "")
            {
                this.SteamAppID = SteamAppID;
                this.GameName = GameName;
                this.GameDirectory = GameDirectory;
                this.GamePath = GamePath;
                this.GameExe = GameExe;
                this.ProfileNametag = "hpp-"+ProfileNametag;
                this.SDKFolder = SDKFolder;
            }

            // Override ToString to specify how the item should be displayed in the ListBox
            public override string ToString()
            {
                return GameName; // Display only the 'GameName' property in the ListBox
            }
        }


    }
}
