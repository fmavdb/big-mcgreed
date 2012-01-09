using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.highscore
{
    public class HighScore
    {
        public Dictionary<string, int> highScores;
        protected static SpriteFont highscoreFont;
        string titelText = "HIGHSCORE";

        Vector2 locatieHighscore = Vector2.Zero;
        Vector2 locatieHighscorePersonen = Vector2.Zero;
        Vector2 locatieHighscoreScore = Vector2.Zero;

        public HighScore() {
            highscoreFont = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont");
            locatieHighscore = new Vector2(GameFrame.Width / 2 - highscoreFont.MeasureString(titelText).X / 2, (GameFrame.Height / 2 - Program.INSTANCE.menu.highscoreDisplay.Current.Height / 2) + 40);

            highScores = new Dictionary<string, int>();
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
            //Dit is een voorbeeld:
            highScores.Add("Frank", 1000);
            highScores.Add("Geert", 10000);
            highScores.Add("Jelle", -1);
            highScores.Add("Kevin", 100);
            highScores.Add("Rick", 500);
            highScores.Add("Wouter", 0);
            highScores.Add("Martin", 10);
            highScores = (from entry in highScores orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void addToHighScore(string naam, int score)
        {
            //Voeg toe aan database.
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
        }

        public void Draw()
        {
            Program.INSTANCE.spriteBatch.DrawString(highscoreFont, titelText, locatieHighscore, Color.White);

            int nummer = 1;
            Vector2 huidig = locatieHighscorePersonen;
            float huidigY = locatieHighscorePersonen.Y;

            int score = 0;
            foreach (string naam in highScores.Keys)
            {
                highScores.TryGetValue(naam, out score);

                locatieHighscorePersonen = new Vector2(GameFrame.Width / 2 - Program.INSTANCE.menu.highscoreDisplay.Current.Width / 2 + 50, (GameFrame.Height / 2 - Program.INSTANCE.menu.highscoreDisplay.Current.Height / 3) + 50);
                locatieHighscoreScore = new Vector2(GameFrame.Width / 2 + Program.INSTANCE.menu.highscoreDisplay.Current.Width / 4 - 10, huidig.Y);

                Program.INSTANCE.spriteBatch.DrawString(highscoreFont, "" + nummer + ".   " + naam, huidig, Color.White);
                Program.INSTANCE.spriteBatch.DrawString(highscoreFont, "" + score, locatieHighscoreScore, Color.Red);

                huidig.Y += 50;
                nummer++;
            }
        }
    }
}
