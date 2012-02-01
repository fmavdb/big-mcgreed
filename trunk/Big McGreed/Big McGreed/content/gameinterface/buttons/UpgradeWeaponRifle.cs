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
    public class UpgradeWeaponRifle : InterfaceComponent
    {
        public UpgradeWeaponRifle()
        {
            normal = Program.INSTANCE.loadTexture("RifleNormal");
            pressed = Program.INSTANCE.loadTexture("RifleClicked");
            hover = Program.INSTANCE.loadTexture("RifleHover");
            current = normal;

            Location = new Vector2(gameframe.GameFrame.Width / 2 - current.Width / 2, gameframe.GameFrame.Height / 2 - current.Height / 3);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.currentLevel >= 2)
            {
                Program.INSTANCE.player.Weapon.setLevel(1);
            }
            else
            {
                Program.INSTANCE.IManager.upgradeAchtergrond.tekst3 = "You need to be in level 2 or higher to use this weapon!";
                Program.INSTANCE.IManager.upgradeAchtergrond.timer = 1000;
            }
        }
    }
}
