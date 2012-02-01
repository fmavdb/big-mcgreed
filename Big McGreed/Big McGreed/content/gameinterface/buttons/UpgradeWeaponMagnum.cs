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
    public class UpgradeWeaponMagnum : InterfaceComponent
    {
        public UpgradeWeaponMagnum()
        {
            normal = Program.INSTANCE.loadTexture("MagnumNormal");
            pressed = Program.INSTANCE.loadTexture("MagnumClicked");
            hover = Program.INSTANCE.loadTexture("MagnumHover");
            current = normal;

            hoverText = "The standard, all favourite Magnum!";

            Location = new Vector2(gameframe.GameFrame.Width / 2 - current.Width / 2, gameframe.GameFrame.Height / 2 - current.Height * 1.5f);
        }

        public override void action()
        {
            if (Program.INSTANCE.player.currentLevel >= 1)
            {
                Program.INSTANCE.player.Weapon.setLevel(0);
            }
        }
    }
}
