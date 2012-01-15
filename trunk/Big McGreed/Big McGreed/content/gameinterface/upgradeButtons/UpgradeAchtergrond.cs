using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class UpgradeAchtergrond : InterfaceComponent
    {
        public UpgradeAchtergrond()
        {
            normal = Program.INSTANCE.loadTexture("UpgradeAchtergrond");
            pressed = normal;
            hover = normal;
            current = normal;

            isInterface = true;
            text = "UPGRADES";

            textLocation = new Vector2(GameFrame.Width / 2 - InterfaceComponent.font.MeasureString(text).X / 2, GameFrame.Height / 3.6f - InterfaceComponent.font.MeasureString(text).Y / 2);
        }

        public override void action()
        {
        }
    }
}
