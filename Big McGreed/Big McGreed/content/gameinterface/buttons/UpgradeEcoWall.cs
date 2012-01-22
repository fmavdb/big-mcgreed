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
    public class UpgradeEcoWall : InterfaceComponent
    {
        public UpgradeEcoWall()
        {
            normal = Program.INSTANCE.loadTexture("EcoWall");
            pressed = Program.INSTANCE.loadTexture("EcoWallClicked");
            hover = Program.INSTANCE.loadTexture("EcoWallHighlight");
            current = normal;

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst2).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 5);
        }

        public override void action()
        {
            Program.INSTANCE.player.Wall.LevelUp();
        }
    }
}
