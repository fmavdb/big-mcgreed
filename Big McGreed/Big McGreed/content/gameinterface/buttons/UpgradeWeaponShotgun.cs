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
            normal = Program.INSTANCE.loadTexture("EcoHelpNormal");
            pressed = Program.INSTANCE.loadTexture("EcoHelpClicked");
            hover = Program.INSTANCE.loadTexture("EcoHelpHighlight");
            current = normal;

            Location = new Vector2(gameframe.GameFrame.Width / 2 - current.Width / 2, gameframe.GameFrame.Height / 2 + current.Height * 0.8f);
        }

        public override void action()
        {
            Program.INSTANCE.player.Weapon.setLevel(2);
        }
    }
}
