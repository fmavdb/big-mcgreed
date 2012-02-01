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
    public class UpgradeWeaponShotgun : InterfaceComponent
    {
        public UpgradeWeaponShotgun()
        {
            normal = Program.INSTANCE.loadTexture("ShotgunNormal");
            pressed = Program.INSTANCE.loadTexture("ShotgunClicked");
            hover = Program.INSTANCE.loadTexture("ShotgunHover");
            current = normal;

            errorText = "You need to be in wave 3 to acces this weapon!";

            Location = new Vector2(gameframe.GameFrame.Width / 2 - current.Width / 2, gameframe.GameFrame.Height / 2 + current.Height * 0.8f);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.currentLevel >= 3)
            {
                Program.INSTANCE.player.Weapon.setLevel(2);
            }
            else
            {
                error = true;
            }
        }
    }
}
