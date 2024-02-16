using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammerPP_Manager
{
    internal static class SourceGames
    {

        internal static readonly SourceGame SOURCESDKBASE2013MP = new SourceGame("243750", "Source SDK Base 2013 Multiplayer", "Source SDK Base 2013 Multiplayer", "hl2", "sdk2013mp");

        internal static readonly SourceGame GARRYSMOD = new SourceGame("4000", "Garry's Mod", "GarrysMod", "garrysmod", "gmod");
        internal static readonly SourceGame FISTFUL_OF_FRAGS = new SourceGame("265630", "Fistful of Frags", "Fistful of Frags", "fof", "fof", "\\sdk\\hl2.exe");
        internal static readonly SourceGame TEAM_FORTRESS_2 = new SourceGame("440", "Team Fortress 2", "Team Fortress 2", "tf", "tf2");
        internal static readonly SourceGame COUNTERSTRIKE_SOURCE = new SourceGame("240", "Counter-Strike: Source", "Counter-Strike Source", "cstrike", "css");

        internal class SourceGame
        {
            internal readonly string SteamAppID;
            internal readonly string GameName;
            internal readonly string GameDirectory;
            internal readonly string GamePath;
            internal readonly string GameExe;
            internal readonly string ProfileNametag;

            internal SourceGame(string SteamAppID, string GameName, string GameDirectory, string GamePath, string ProfileNametag, string GameExe = "\\hl2.exe")
            {
                this.SteamAppID = SteamAppID;
                this.GameName = GameName;
                this.GameDirectory = GameDirectory;
                this.GamePath = GamePath;
                this.GameExe = GameExe;
                this.ProfileNametag = "hpp-"+ProfileNametag;
            }
        }


    }
}
