using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Big_McGreed.content.gameinterface;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class GameOverInterface : GameInterface
    {
        public Vector2 naamLocation;

        public bool gameWon;

        public GameOverInterface()
        {
            mainTexture = Program.INSTANCE.loadTexture("HighscoreInterface");

            title = "GAME OVER!";

            centerInterface();

            titleLocation = new Vector2(getX() + mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(title).X / 2, getY() + Program.INSTANCE.IManager.font.MeasureString(title).Y * 0.50f);
            //naamLocation = new Vector2(this.getX() + this.mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(tekst).X / 2, this.getY() + this.mainTexture.Height / 3 - Program.INSTANCE.IManager.font.MeasureString(tekst).Y / 2);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(Program.INSTANCE.IManager.font, Program.INSTANCE.player.naam, new Vector2(this.getX() + this.mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(Program.INSTANCE.player.naam).X / 2, this.getY() + this.mainTexture.Height / 2 - Program.INSTANCE.IManager.font.MeasureString(Program.INSTANCE.player.naam).Y / 2), Color.Black);
        }
    }
}