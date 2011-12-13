using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu.buttons
{
    public class YesNoSelect : Button
    {
        public YesNoSelect()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");
            hover = normal;
            pressed = normal;
            current = normal;

            location.X = Program.INSTANCE.Width / 2 - current.Width / 2;
            location.Y = Program.INSTANCE.Height / 2 - current.Height / 2;
        }

        public override void action()
        {
        }
    }
}
