using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal partial class Form_TermsAndConditions : HPPM_Form
    {
        private int page = 0;
        private static string[] licenses = { Properties.Resources.licenseHPPManager, Properties.Resources.licenseGsemacCommons, Properties.Resources.licenseCostura };
        private static string[] licensesNames = { "Hammer++ Manager", "Gsemac.Common", "Costura.Fody" };
        internal Form_TermsAndConditions()
        {
            InitializeComponent();
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            UpdateLicenseTextBox();
        }

        #region Assembly Attribute Accessors

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (page == 0)
                return;
            page--;
            UpdateLicenseTextBox();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (page == licenses.Length - 1)
                return;
            page++;
            UpdateLicenseTextBox();
        }

        private void UpdateLicenseTextBox()
        {
            labelLicenseName.Text = "License for " + licensesNames[page] +":";
            labelLicenseCount.Text = (page + 1) + " / " + licenses.Length;
            textboxLicense.Text = licenses[page];
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/rmod8/HammerPP_Manager/tree/master");
        }
    }
}
