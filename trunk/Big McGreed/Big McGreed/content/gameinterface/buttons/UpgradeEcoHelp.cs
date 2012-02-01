using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameinterface.interfaces;
using Big_McGreed.content.upgrades;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class UpgradeEcoHelp : InterfaceComponent
    {
        public UpgradeEcoHelp()
        {
            normal = Program.INSTANCE.loadTexture("EcoHelpNormal");
            pressed = Program.INSTANCE.loadTexture("EcoHelpClicked");
            hover = Program.INSTANCE.loadTexture("EcoHelpHighlight");
            current = normal;

            hoverText = "Get more HP and get the help of 2 gunmen.";

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst2).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 2.5f);
        }

        public override void action()
        {
            //Program.INSTANCE.player.Wall.LevelUp();
            Program.INSTANCE.player.good++;
            Program.INSTANCE.player.UpdateCrosshair();
        }
    }
}
