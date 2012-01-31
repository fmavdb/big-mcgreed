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
        public const string tekst = "Please enter your name";
        public Vector2 tekstLocation;

        public GameOverInterface()
        {
            mainTexture = Program.INSTANCE.loadTexture("TinyInterface");

            title = "Game Over!";

            centerInterface();

            tekstLocation = new Vector2(this.getX() + this.mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(tekst).X / 2, this.getY() + this.mainTexture.Height / 3 - Program.INSTANCE.IManager.font.MeasureString(tekst).Y / 2);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(Program.INSTANCE.IManager.font, tekst, tekstLocation, Color.White);
        }
    }
}