using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class UpgradeAchtergrond : Button
    {
        public UpgradeAchtergrond()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("UpgradeAchtergrond");
            pressed = normal;
            hover = normal;
            current = normal;

            text = "UPGRADES";
            Location = new Vector2(GameFrame.Width / 2 - Button.font.MeasureString(text).X / 2, 0); //(GameFrame.Height / 2 - current.Height / 2));

            //Location = new Vector2(GameFrame.Width / 2 - current.Width / 2, GameFrame.Height / 2 - current.Height / 2);
        }

        public override void action()
        {
        }
    }
}
