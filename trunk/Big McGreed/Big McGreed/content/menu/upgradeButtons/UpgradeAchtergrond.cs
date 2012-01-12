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

            isInterface = true;
            text = "UPGRADES";

            textLocation = new Vector2(GameFrame.Width / 2 - Button.font.MeasureString(text).X / 2, GameFrame.Height / 3.6f - Button.font.MeasureString(text).Y / 2);
        }

        public override void action()
        {
        }
    }
}
