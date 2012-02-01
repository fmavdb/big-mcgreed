using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Big_McGreed.content.input
{
    /// <summary>
    /// Contains key events.
    /// </summary>
    public class InputEvents
    {
        /// <summary>
        /// Handles the pressed key.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        public void KeyPress(object sender, KeyPressEventArgs e)
        {
            //Character that was typed = e.KeyChar
            foreach (char Char in utility.StringUtilities.validChars)
            {
                if (Char == e.KeyChar)
                {
                    Program.INSTANCE.player.naam += e.KeyChar;
                }
            }
        }

        /// <summary>
        /// Handles the pressed backspace.
        /// </summary>
        public void OnBackSpacePress()
        {
            Program.INSTANCE.player.naam = Program.INSTANCE.player.naam.Substring(0, Program.INSTANCE.player.naam.Length - 1);
        }
    }
}
