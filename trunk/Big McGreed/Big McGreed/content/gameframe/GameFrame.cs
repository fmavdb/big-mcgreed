using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.player;

namespace Big_McGreed.content.gameframe
{
    /// <summary>
    /// Represents the game frame and its contents.
    /// </summary>
    public class GameFrame
    {
        /// <summary>
        /// The Width of the application.
        /// </summary>
        public static int Width;

        /// <summary>
        /// The Height of the application.
        /// </summary>
        public static int Height;

        private int oldLevel = -1;

        private Texture2D mainTexture = null;
        public Texture2D boerderijTexture = null;
        private Texture2D raamAchtergrond = null;
        private Texture2D hpBarTextureGroen = null;
        private Texture2D hpBarTextureRood = null;
        private Texture2D oilBarTextureFull = null;
        private Texture2D oilBarTextureEmpty = null;

        public Vector2 boerderijPositie { get; private set; }
        public Vector2 raamAchtergrondPositie { get; private set; }
        private Vector2 hpBarPositie = Vector2.Zero;
        private Vector2 oilBarPositie = Vector2.Zero;
        public Vector2 goldPositie = Vector2.Zero;
        public Vector2 goldUpgradePositie = Vector2.Zero;
        private Vector2 goldTextUpgradePositie = Vector2.Zero;
        private Vector2 goldTextPositie = Vector2.Zero;
        private Vector2 scorePositie = Vector2.Zero;
        private Vector2 hpBarTextPositie = Vector2.Zero;
        private Vector2 hpBarHpLeftPositie = Vector2.Zero;
        private Vector2 oilBarTextPositie = Vector2.Zero;
        private Vector2 oilBaroilLevelPositie = Vector2.Zero;

        private Rectangle rectangleHP;
        private Rectangle rectangleOil;

        private SpriteFont gameFrameFont;

        private string currency = "$";
        public string hpText = "100/100";
        private string oilText = "100/100";
        public string scoreText = "Score: 0";

        private Texture2D muur;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFrame"/> class.
        /// </summary>
        public GameFrame()
        {
            mainTexture = Program.INSTANCE.loadTexture("Border");

            boerderijTexture = Program.INSTANCE.loadTexture("boerderij0");
            boerderijPositie = new Vector2(Width - boerderijTexture.Width / 2, Height - boerderijTexture.Height * 1.2f);

            raamAchtergrond = Program.INSTANCE.loadTexture("WindowBackground");
            raamAchtergrondPositie = new Vector2(boerderijPositie.X + raamAchtergrond.Width / 2, boerderijPositie.Y + raamAchtergrond.Height / 2);

            hpBarTextureRood = Program.INSTANCE.loadTexture("HPBarRood");
            hpBarTextureGroen = Program.INSTANCE.loadTexture("HPBarGroen");
            hpBarPositie = new Vector2((Width - hpBarTextureGroen.Width) / 1.025f , hpBarTextureGroen.Height);

            rectangleHP = new Rectangle(0, 0, hpBarTextureGroen.Width, hpBarTextureGroen.Height);

            oilBarTextureEmpty = Program.INSTANCE.loadTexture("OilEmpty");
            oilBarTextureFull = Program.INSTANCE.loadTexture("OilFull");
            oilBarPositie = new Vector2((Width - oilBarTextureFull.Width) / 1.025f, oilBarTextureFull.Height *2.5f);

            rectangleOil = new Rectangle(0, 0, oilBarTextureFull.Width, oilBarTextureFull.Height);

            gameFrameFont = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont"); //dit is een font geen image
            goldPositie = new Vector2((Width - hpBarTextureGroen.Width) + 5, hpBarTextureGroen.Height * 4);
            goldTextPositie = new Vector2(goldPositie.X - gameFrameFont.MeasureString("Gold:").X - 20, goldPositie.Y);
            goldTextUpgradePositie = new Vector2(Program.INSTANCE.IManager.upgradeAchtergrond.getX() + Program.INSTANCE.IManager.upgradeAchtergrond.mainTexture.Width, Program.INSTANCE.IManager.upgradeAchtergrond.getY() / 2);
            goldUpgradePositie = new Vector2(goldTextUpgradePositie.X + gameFrameFont.MeasureString("Gold:").X + 10, goldTextUpgradePositie.Y);
            hpBarTextPositie = new Vector2(hpBarPositie.X - gameFrameFont.MeasureString("HP:").X - 30, hpBarPositie.Y);
            oilBarTextPositie = new Vector2(oilBarPositie.X - gameFrameFont.MeasureString("Oil:").X - 20, oilBarPositie.Y);
            hpBarHpLeftPositie = new Vector2(hpBarPositie.X + hpBarTextureGroen.Width /2 - gameFrameFont.MeasureString(hpText).X / 2, hpBarPositie.Y);
            oilBaroilLevelPositie = new Vector2(oilBarPositie.X + oilBarTextureFull.Width / 2 - gameFrameFont.MeasureString(oilText).X / 2, oilBarPositie.Y);

            scorePositie = new Vector2(GameFrame.Width - Program.INSTANCE.IManager.font.MeasureString(scoreText).X * 1.1f, GameFrame.Height - Program.INSTANCE.IManager.font.MeasureString(scoreText).Y);

            //muur = Program.INSTANCE.loadTexture("Muur1");
        }

        /// <summary>
        /// Draws this instance.
        /// Be sure to watch the order when you draw something! Placing something first, will draw it first, antyhing that comes after it, will be drawn over the first texture.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (mainTexture != null)
            {
                batch.Draw(mainTexture, Vector2.Zero, Color.White);
            }
            DrawRaamAchterkant(batch);
            batch.Draw(Program.INSTANCE.player.definition.personTexture, Program.INSTANCE.player.BoerLocatie, Color.White);
            DrawBoerderij(batch);
            batch.Draw(hpBarTextureRood, hpBarPositie, Color.White);
            batch.Draw(hpBarTextureGroen, hpBarPositie, rectangleHP, Color.White);
            batch.Draw(oilBarTextureEmpty, oilBarPositie, Color.White);
            batch.Draw(oilBarTextureFull, oilBarPositie, rectangleOil, Color.White);

            batch.DrawString(gameFrameFont, "Gold:", goldTextPositie, Color.White);
            batch.DrawString(gameFrameFont, "HP:", hpBarTextPositie, Color.White);
            batch.DrawString(gameFrameFont, "Oil:", oilBarTextPositie, Color.White);
            batch.DrawString(gameFrameFont, scoreText, scorePositie, Color.White);
            batch.DrawString(gameFrameFont, currency + Program.INSTANCE.player.gold, goldPositie, Color.White);
            batch.DrawString(gameFrameFont, hpText, hpBarHpLeftPositie, Color.White);
            batch.DrawString(gameFrameFont, oilText, oilBaroilLevelPositie, Color.White);
        }

        /// <summary>
        /// Updates the HP.
        /// </summary>
        /// <param name="hp">The hp.</param>
        public void UpdateHP(int hp)
        {
            double factor = ((double)hp / (double)Player.maxHP);
            rectangleHP = new Rectangle(0, 0, (int)(hpBarTextureGroen.Width * factor), hpBarTextureGroen.Height);
            hpText = hp + "/" + Player.maxHP;
            Program.INSTANCE.sendHP = true;
        }

        public void UpdateOil(int oil)
        {
            double factor = ((double)oil / (double)Player.maxOil);
            rectangleOil = new Rectangle(0, 0, (int)(oilBarTextureFull.Width * factor), oilBarTextureFull.Height);
            oilText = oil + "/" + Player.maxOil;
            Program.INSTANCE.sendOil = true;
        }

        public void UpdateScore()
        {
            scoreText = "Score: " + Program.INSTANCE.player.Score;
            scorePositie = new Vector2(GameFrame.Width - Program.INSTANCE.IManager.font.MeasureString(scoreText).X * 1.1f, GameFrame.Height - Program.INSTANCE.IManager.font.MeasureString(scoreText).Y);
            Program.INSTANCE.sendScore = true;
        }

        public void DrawGold(SpriteBatch batch)
        {
            batch.DrawString(gameFrameFont, "Gold:", goldTextUpgradePositie, Color.White);
            batch.DrawString(gameFrameFont, currency + Program.INSTANCE.player.gold, goldUpgradePositie, Color.White);
            Program.INSTANCE.sendGold = true;
        }

        public void DrawBoerderij(SpriteBatch batch)
        {
            if (oldLevel != Program.INSTANCE.player.boerderij.getLevel())
            {
                if (Program.INSTANCE.player.boerderij.getLevel() == 0)
                {
                    boerderijTexture = Program.INSTANCE.loadTexture("boerderij0");
                    oldLevel = Program.INSTANCE.player.boerderij.getLevel();
                }
                else
                {
                    boerderijTexture = Program.INSTANCE.loadTexture("boerderij1");
                    oldLevel = Program.INSTANCE.player.boerderij.getLevel();
                }
            }
            batch.Draw(boerderijTexture, boerderijPositie, Color.White);
        }

        public void DrawRaamAchterkant(SpriteBatch batch)
        {
            batch.Draw(raamAchtergrond, raamAchtergrondPositie, Color.White);
        }
    }
}
