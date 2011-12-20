using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class UpgradeAchtergrond : Button
    {
        public UpgradeAchtergrond()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("UpgradeAchtergrond");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("UpgradeAchtergrond");
            hover = Program.INSTANCE.Content.Load<Texture2D>("UpgradeAchtergrond");
            current = normal;

            Location = new Vector2(GameFrame.Width / 2 - current.Width / 2, GameFrame.Height / 2 - current.Height / 2);
        }

        public override void action()
        {
        }
    }
}
