using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.highscore
{
    public class HighScore
    {
        public Dictionary<string, int> highScores;

        public HighScore() {
            highScores = new Dictionary<string, int>();
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
            //Dit is een voorbeeld:
            highScores.Add("Frank", 1000);
            highScores.Add("Geert", 5);
            highScores.Add("Jelle", -1);
            highScores.Add("Kevin", 100);
            highScores.Add("Rick", 500);
            highScores.Add("Wouter", 0);
        }

        public void addToHighScore(string naam, int score)
        {
            //Voeg toe aan database.
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
        }

        public void Draw()
        {
            int nummer = 1;
            foreach (string naam in highScores.Keys)
            {
                Program.INSTANCE.spriteBatch.DrawString(Program.INSTANCE.menu.menuButtonKlein.TinyFont, "" + nummer, Vector2.Zero, Color.White);
                Program.INSTANCE.spriteBatch.DrawString(Program.INSTANCE.menu.menuButtonKlein.TinyFont, naam, Vector2.Zero, Color.White);
                nummer++;
            }
        }
    }
}
