using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Big_McGreed.content.menu.buttons
{
    public class Upgrade : Button
    {
        public Upgrade()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("UpgradeNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("UpgradePressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("UpgradeHighlight");
            current = normal;
        }

        public override void action()
        {
            Program.INSTANCE.Exit();
        }
    }
}
