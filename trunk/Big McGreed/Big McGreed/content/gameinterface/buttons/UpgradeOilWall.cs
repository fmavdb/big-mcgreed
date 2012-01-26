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
    public class UpgradeOilWall : InterfaceComponent
    {
        public UpgradeOilWall()
        {
            normal = Program.INSTANCE.loadTexture("OilWall");
            pressed = Program.INSTANCE.loadTexture("OilWallClicked");
            hover = Program.INSTANCE.loadTexture("OilWallHighlight");
            current = normal;

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst1).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 5);
        }

        public override void action()
        {
            Program.INSTANCE.player.Wall.LevelUp();
        }
    }
}
