using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameinterface.interfaces;
using Big_McGreed.content.upgrades;
using Big_McGreed.logic.player;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class UpgradeOilHelp : InterfaceComponent
    {
        public UpgradeOilHelp()
        {
            normal = Program.INSTANCE.loadTexture("OilHelpNormal");
            pressed = Program.INSTANCE.loadTexture("OilHelpClicked");
            hover = Program.INSTANCE.loadTexture("OilHelpHighlight");
            current = normal;

            hoverText = "Hitt more and get the help of 2 gunmen." + "\n" + "COST: " + UpgradeDefinition.forName("boerderij1").cost;

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst1).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst1Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 2.5f);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.boerderij.LevelUp())
            {
                Program.INSTANCE.player.gold -= Program.INSTANCE.player.boerderij.definition.cost;
                Program.INSTANCE.player.evil++;
                Program.INSTANCE.player.damageFactor = 1.25;
                Program.INSTANCE.player.UpdateCrosshair();
            }
        }
    }
}
