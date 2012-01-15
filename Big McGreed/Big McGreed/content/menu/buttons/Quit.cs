using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Big_McGreed.content.menu.buttons
{
    public class Quit : Button
    {
        public Quit()
        {
            normal = Program.INSTANCE.loadTexture("ButtonNormal");
            pressed = Program.INSTANCE.loadTexture("ButtonPressed");
            hover = Program.INSTANCE.loadTexture("ButtonHighlight");
            current = normal;

            text = "QUIT";
        }

        public override void action()
        {
            Program.INSTANCE.Exit();
        }
    }
}
