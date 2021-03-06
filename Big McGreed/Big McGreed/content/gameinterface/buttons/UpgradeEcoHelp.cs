﻿using System;
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
    public class UpgradeEcoHelp : InterfaceComponent
    {
        public UpgradeEcoHelp()
        {
            normal = Program.INSTANCE.loadTexture("EcoHelpNormal");
            pressed = Program.INSTANCE.loadTexture("EcoHelpClicked");
            hover = Program.INSTANCE.loadTexture("EcoHelpHighlight");
            current = normal;

            hoverText = "Get more HP and get the help of 2 gunmen." + "\n" + "COST: " + UpgradeDefinition.forName("boerderij1").cost;

            Location = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.X + Program.INSTANCE.IManager.font.MeasureString(UpgradeAchtergrond.tekst2).X / 2 - current.Width / 2, Program.INSTANCE.IManager.upgradeAchtergrond.tekst2Location.Y + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Height / 2.5f);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.boerderij.LevelUp())
            {
                Program.INSTANCE.player.good++;
                Program.INSTANCE.player.gold -= Program.INSTANCE.player.boerderij.definition.cost;
                Program.INSTANCE.player.UpdateCrosshair();
                Program.INSTANCE.player.Lifes += 50;
                Player.maxHP = 150;
                Program.INSTANCE.gameFrame.UpdateHP(Program.INSTANCE.player.Lifes);
            }
        }
    }
}
