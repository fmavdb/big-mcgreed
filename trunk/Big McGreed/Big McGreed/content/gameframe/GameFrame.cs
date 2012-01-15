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

        public Vector2 boerderijPositie { get; private set; }
        private Vector2 hpBarPositie = Vector2.Zero;

        private Rectangle rectangleHP;

        private SpriteFont gameFrameFont;
        private Vector2 gameFrameFontPositie;

        private string currency = "$";

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
            hpBarPositie = new Vector2((Width - hpBarTextureGroen.Width) / 1.025f , 0 + hpBarTextureGroen.Height);

            rectangleHP = new Rectangle(0, 0, hpBarTextureGroen.Width, hpBarTextureGroen.Height);

            gameFrameFont = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont"); //dit is een font geen image
            gameFrameFontPositie = new Vector2((Width - hpBarTextureGroen.Width) / 1.025f, hpBarTextureGroen.Height * 2.5f);

            //muur = Program.INSTANCE.loadTexture("Muur1");
        }

        /// <summary>
        /// Draws this instance.
        /// Be sure to watch the order when you draw something! Placing something first, will draw it first, antyhing that comes after it, will be drawn over the first texture.
        /// </summary>
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
            batch.DrawString(gameFrameFont, currency + Program.INSTANCE.player.gold, gameFrameFontPositie, Color.White);
        }

        public void UpdateHP(int hp)
        {
            double factor = ((double)hp / (double)Player.maxHP);
            rectangleHP = new Rectangle(0, 0, (int)(hpBarTextureGroen.Width * factor), hpBarTextureGroen.Height);
        }
    }
}
