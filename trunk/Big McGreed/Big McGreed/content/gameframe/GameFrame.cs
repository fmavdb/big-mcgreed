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

        private Texture2D mainTexture = null;
        public Texture2D boerderijTexture = null;
        private Texture2D hpBarTextureGroen = null;
        private Texture2D hpBarTextureRood = null;
        private Texture2D oilBarTextureFull = null;
        private Texture2D oilBarTextureEmpty = null;

        public Vector2 boerderijPositie { get; private set; }
        private Vector2 hpBarPositie = Vector2.Zero;
        private Vector2 oilBarPositie = Vector2.Zero;
        private Vector2 goldPositie = Vector2.Zero;
        private Vector2 goldTextPositie = Vector2.Zero;
        private Vector2 hpBarTextPositie = Vector2.Zero;
        private Vector2 hpBarHpLeftPositie = Vector2.Zero;
        private Vector2 oilBarTextPositie = Vector2.Zero;
        private Vector2 oilBaroilLevelPositie = Vector2.Zero;

        private Rectangle rectangleHP;
        private Rectangle rectangleOil;

        private SpriteFont gameFrameFont;

        private string currency = "$";
        private string hpText = "100/100";
        private string oilText = "100/100";

        private Texture2D muur;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFrame"/> class.
        /// </summary>
        public GameFrame()
        {
            mainTexture = Program.INSTANCE.loadTexture("Border");

            boerderijTexture = Program.INSTANCE.loadTexture("Boerderij");
            boerderijPositie = new Vector2(Width - boerderijTexture.Width / 2, Height - boerderijTexture.Height * 1.2f);

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
            hpBarTextPositie = new Vector2(hpBarPositie.X - gameFrameFont.MeasureString("HP:").X - 30, hpBarPositie.Y);
            oilBarTextPositie = new Vector2(oilBarPositie.X - gameFrameFont.MeasureString("Oil:").X - 20, oilBarPositie.Y);
            hpBarHpLeftPositie = new Vector2(hpBarPositie.X + hpBarTextureGroen.Width /2 - gameFrameFont.MeasureString(hpText).X / 2, hpBarPositie.Y);
            oilBaroilLevelPositie = new Vector2(oilBarPositie.X + oilBarTextureFull.Width / 2 - gameFrameFont.MeasureString(oilText).X / 2, oilBarPositie.Y);

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
            batch.Draw(Program.INSTANCE.player.definition.personTexture, Program.INSTANCE.player.BoerLocatie, Color.Black);
            batch.Draw(boerderijTexture, boerderijPositie, Color.White);
            batch.Draw(hpBarTextureRood, hpBarPositie, Color.White);
            batch.Draw(hpBarTextureGroen, hpBarPositie, rectangleHP, Color.White);
            batch.Draw(oilBarTextureEmpty, oilBarPositie, Color.White);
            batch.Draw(oilBarTextureFull, oilBarPositie, rectangleOil, Color.White);

            batch.DrawString(gameFrameFont, "Gold:", goldTextPositie, Color.White);
            batch.DrawString(gameFrameFont, "HP:", hpBarTextPositie, Color.White);
            batch.DrawString(gameFrameFont, "Oil:", oilBarTextPositie, Color.White);
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
        }
    }
}
