//https://stackoverflow.com/questions/3730968/how-to-disable-cursor-in-textbox


using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HammerPP_Manager
{
    internal class ReadOnlyTextBox : TextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public ReadOnlyTextBox()
        {
            this.ReadOnly = true;
            this.BackColor = Color.White;
            this.GotFocus += TextBoxGotFocus;
            this.Cursor = Cursors.Arrow; // mouse cursor like in other controls
        }

        private void TextBoxGotFocus(object sender, EventArgs args)
        {
            HideCaret(this.Handle);
        }
    }
}
