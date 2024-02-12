// Template form for all forms used in this program.
// Applies dark theme, adds sounds, common methods, etc.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gsemac.Forms.Styles.Applicators;
using Gsemac.Forms.Styles.StyleSheets;

namespace HammerPP_Manager
{
    internal class HPPM_Form : Form
    {
        internal HPPM_Form()
        {
            applicator.ApplyStyles(this);
        }

        // Setup static SoundPlayers for the sounds used in this program.
        internal static SoundPlayer sfxCaution = new SoundPlayer(Properties.Resources.caution);
        internal static SoundPlayer sfxDenied = new SoundPlayer(Properties.Resources.denied);
        internal static SoundPlayer sfxInfo = new SoundPlayer(Properties.Resources.info);
        internal static SoundPlayer sfxSuccess = new SoundPlayer(Properties.Resources.success);

        // InfoType Enum used for Info Boxes
        internal enum HPPM_FormType
        {
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


    }
}
