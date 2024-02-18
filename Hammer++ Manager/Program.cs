using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form_MainWindow());

            //This is used to test how dialog boxes are displayed. Uncomment this and comment the MainWindow to debug InfoBoxs.
            //Application.Run(new Form_InfoBox(HPPM_Form.FormType.NoGamesFoundManual, HPPM_Form.FormIconType.LIGHT, HPPM_Form.FormSoundType.INFO));
        }
    }
}
