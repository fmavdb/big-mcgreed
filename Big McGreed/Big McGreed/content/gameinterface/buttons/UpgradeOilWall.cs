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

            hoverText = "Get an electric fence which you can use LIMITED \n times for 30 seconds, before your oil runs out" + "\n" + "COST: " + UpgradeDefinition.forName("muur1").cost;

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst1).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 5);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.Wall.LevelUp() == true)
            {
                Program.INSTANCE.player.evilWall = true;
                Program.INSTANCE.IManager.upgradeAchtergrond.tekst3 = "Your industrial thinking brought you an industrial fence!";
                Program.INSTANCE.IManager.upgradeAchtergrond.timer = 5000;
                Program.INSTANCE.player.evil++;
                Program.INSTANCE.player.UpdateCrosshair();
            }
        }
    }
}
