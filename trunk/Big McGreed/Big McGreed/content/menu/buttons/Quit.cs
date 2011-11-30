using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu.buttons
{
    public class Quit : Button
    {
        public Quit()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("QuitNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("QuitPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("QuitHighlight");
            current = normal;
        }

        public override void action()
        {
            //quit
        }
    }
}
