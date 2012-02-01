using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.content.gameframe;
using System.Data.OleDb;
using Big_McGreed.utility;

namespace Big_McGreed.content.highscore
{
    public class HighScore : IDraw
    {
        public Dictionary<string, int> highScores;
        protected static SpriteFont highscoreFont;
        string titelText = "HIGHSCORE";

        Vector2 locatieHighscore = Vector2.Zero; //TODO - dit kan via Program.INSTANCE.IManager.highscoredisplay.getLocation()
        Vector2 locatieHighscorePersonen = Vector2.Zero;
        Vector2 locatieHighscoreScore = Vector2.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScore"/> class.
        /// </summary>
        public HighScore() 
        {
            UpdateHighscore();
        }

        public void addToHighScore(string naam, int score)
        {
            foreach (string bestaandeNaam in highScores.Keys)
            {
                if (bestaandeNaam == Program.INSTANCE.player.naam)
                {
                    Program.INSTANCE.highscoreNameInUse = true;
                    return;
                }
            }
            Program.INSTANCE.highscoreNameInUse = false;
            Program.INSTANCE.dataBase.ExecuteQuery("INSERT INTO Highscore (Naam, Score) VALUES ('" + naam + "', '" + score + "')");
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Highscore;

            //Voeg toe aan database.
            //Laad gegevens... SELECT naam, score FROM highscores ORDER BY score LIMIT 10
        }

        public void LoadContent()
        {
            highscoreFont = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont");
            locatieHighscore = new Vector2(GameFrame.Width / 2 - highscoreFont.MeasureString(titelText).X / 2, (GameFrame.Height / 2 - Program.INSTANCE.IManager.highscoreDisplay.mainTexture.Height / 2) + 40);
        }

        /// <summary>
        /// Draws the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public void Draw(SpriteBatch batch)
        {
            UpdateHighscore();
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

        void UpdateHighscore()
        {
            highScores = new Dictionary<string, int>();

            OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT Naam, Score FROM Highscore ORDER BY Score DESC");
            string naam;
            int punten;
            for (int i = 1; reader.Read() && i < 10; i++)
            {
                naam = reader.GetString(reader.GetOrdinal("Naam"));
                punten = reader.GetInt32(reader.GetOrdinal("Score"));
                highScores.Add(naam, punten);
            }
            reader.Close(); //ERG BELANGRIJK! DIT MAAKT een TWEEDE query mogelijk!
        }

        public void NameInUse(SpriteBatch batch)
        {
            batch.DrawString(Program.INSTANCE.IManager.font, "This name is already being used!", new Vector2(Program.INSTANCE.IManager.gameOverInterface.getX() + Program.INSTANCE.IManager.gameOverInterface.mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString("This name is already being used!").X / 2,
                                                    Program.INSTANCE.IManager.gameOverInterface.getY() + Program.INSTANCE.IManager.gameOverInterface.mainTexture.Height / 2 - Program.INSTANCE.IManager.font.MeasureString("This name is already being used!").Y / 2), Color.Red);
        }
    }
}
