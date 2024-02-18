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

            Form_MainWindow form_MainWindow = new Form_MainWindow();
            try
            {
                Application.Run(form_MainWindow);
            }
            catch(Exception ex)
            {
                form_MainWindow.Enabled = false;
                form_MainWindow.Dispose();
                Application.Run(new Form_CrashWindow(ex));
            }
            

            //This is used to test how dialog boxes are displayed. Uncomment this and comment the MainWindow to debug InfoBoxs.
            //Application.Run(new Form_InfoBox(HPPM_Form.FormType.NoGamesFoundManual, HPPM_Form.FormIconType.LIGHT, HPPM_Form.FormSoundType.INFO));
        }
    }
}
