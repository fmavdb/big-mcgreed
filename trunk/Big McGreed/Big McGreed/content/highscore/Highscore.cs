using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.content.gameframe;
using System.Data.OleDb;

namespace Big_McGreed.content.highscore
{
    public class HighScore
    {
        public Dictionary<string, int> highScores;
        protected static SpriteFont highscoreFont;
        string titelText = "HIGHSCORE";

        Vector2 locatieHighscore = Vector2.Zero; //TODO - dit kan via Program.INSTANCE.IManager.highscoredisplay.getLocation()
        Vector2 locatieHighscorePersonen = Vector2.Zero;
        Vector2 locatieHighscoreScore = Vector2.Zero;

        public HighScore() {
            highscoreFont = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont");
            locatieHighscore = new Vector2(GameFrame.Width / 2 - highscoreFont.MeasureString(titelText).X / 2, (GameFrame.Height / 2 - Program.INSTANCE.IManager.highscoreDisplay.mainTexture.Height / 2) + 40);

            highScores = new Dictionary<string, int>();

            OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT Naam, Score FROM Highscore ORDER BY Score DESC");
            string naam;
            int punten;
            for (int i = 0; reader.Read() && i < 10; i++)
            {
                naam = reader.GetString(reader.GetOrdinal("Naam"));
                punten = reader.GetInt32(reader.GetOrdinal("Score"));
                highScores.Add(naam, punten);
            }
        }

        public void addToHighScore(string naam, int score)
        {
            //Voeg toe aan database.
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
        }

        public void Draw(SpriteBatch batch)
        {
            //batch.DrawString(highscoreFont, titelText, locatieHighscore, Color.White);

            int nummer = 1;
            Vector2 huidig = locatieHighscorePersonen;
            float huidigY = locatieHighscorePersonen.Y;

            int score = 0;
            foreach (string naam in highScores.Keys)
            {
                highScores.TryGetValue(naam, out score);

                locatieHighscorePersonen = new Vector2(GameFrame.Width / 2 - Program.INSTANCE.IManager.highscoreDisplay.mainTexture.Width / 2 + 50, (GameFrame.Height / 2 - Program.INSTANCE.IManager.highscoreDisplay.mainTexture.Height / 3) + 40);
                locatieHighscoreScore = new Vector2(GameFrame.Width / 2 + Program.INSTANCE.IManager.highscoreDisplay.mainTexture.Width / 4 - 10, huidig.Y);

                if (nummer != 10)
                {
                    batch.DrawString(highscoreFont, "  " + nummer + ".   " + naam, huidig, Color.White);
                }
                else
                {
                    batch.DrawString(highscoreFont, "" + nummer + ".   " + naam, huidig, Color.White);
                }
                batch.DrawString(highscoreFont, "" + score, locatieHighscoreScore, Color.Red);

                huidig.Y += 50;
                nummer++;
            }
        }
    }
}
