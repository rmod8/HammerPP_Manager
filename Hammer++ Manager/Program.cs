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

            //Application.Run(new Form_InfoBox(HPPM_Form.FormType.NoInternet, HPPM_Form.FormIconType.LIGHT, HPPM_Form.FormSoundType.INFO));
        }
    }
}
