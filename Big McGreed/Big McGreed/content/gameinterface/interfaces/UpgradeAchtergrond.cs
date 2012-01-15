using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class UpgradeAchtergrond : GameInterface
    {
        private string tekst1 = "Industrie Upgrades";

        private string tekst2 = "Ecologie Upgrades";


        public UpgradeAchtergrond()
        {
            mainTexture = Program.INSTANCE.loadTexture("UpgradeAchtergrond");

            title = "UPGRADES";

            centerInterface();
        }

        public void Draw(SpriteBatch batch)
        {

        }
    }
}
