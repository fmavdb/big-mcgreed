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
        public const string tekst1 = "Industrie Upgrades";

        public const string tekst2 = "Ecologie Upgrades";

        public string tekst3 = null;

        public Vector2 tekst1Location;

        public Vector2 tekst2Location;

        public int timer = -1;


        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeAchtergrond"/> class.
        /// </summary>
        public UpgradeAchtergrond()
        {
            mainTexture = Program.INSTANCE.loadTexture("NormalInterface");

            title = "UPGRADES";

            centerInterface();

            tekst1Location = new Vector2(getX() + Program.INSTANCE.IManager.font.MeasureString(tekst1).X * 0.25f, titleLocation.Y * 1.15f);
            tekst2Location = new Vector2(getX() + mainTexture.Width - Program.INSTANCE.IManager.font.MeasureString(tekst2).X * 1.25f, titleLocation.Y * 1.15f);
        }

        /// <summary>
        /// Draws the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(Program.INSTANCE.IManager.font, tekst1, tekst1Location, Color.White);
            batch.DrawString(Program.INSTANCE.IManager.font, tekst2, tekst2Location, Color.White);
            if (tekst3 != null && timer > 0)
            {
                batch.DrawString(Program.INSTANCE.IManager.font, tekst3, new Vector2(getX() + mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(tekst3).X / 2, getY() + mainTexture.Height - Program.INSTANCE.IManager.font.MeasureString(tekst3).Y * 2), Color.Red);
                timer--;
            }
            else if (timer == 0)
            {
                tekst3 = null;
                timer = -1;
            }
        }
    }
}
