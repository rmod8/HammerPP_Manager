using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal partial class Form_SelectFoundGames : HPPM_Form
    {
        private Tuple<SourceGames.SourceGame, string> dialogResult = null;
        List<Tuple<SourceGames.SourceGame, string>> gameList;
        internal Form_SelectFoundGames(List<Tuple<SourceGames.SourceGame, string>> List)
        {
            InitializeComponent();
            gameList = List;

            // Add game names to ListBox
            for(int i = 0; i < gameList.Count; i++)
            {
                listBox1.Items.Add(gameList[i].Item1.GameName);
            }
        }

        /// <summary>
        /// Override ShowDialog to return the bool.
        /// </summary>
        /// <returns></returns>
        public new Tuple<SourceGames.SourceGame, string> ShowDialog()
        {
            // Show the dialog as usual
            base.ShowDialog();

            // Return the boolean variable indicating the result
            return dialogResult;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            else
            { 
                dialogResult = gameList[listBox1.SelectedIndex];
                Close();
            }
                
        }
    }
}
