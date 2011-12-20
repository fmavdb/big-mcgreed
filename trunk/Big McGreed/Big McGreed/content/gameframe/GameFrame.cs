using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.player;

namespace Big_McGreed.content.gameframe
{
    public class GameFrame
    {
        public static int Width;

        public static int Height;

        private Texture2D mainTexture = null;
        private Texture2D boerderijTexture = null;
        private Texture2D hpBarTextureGroen = null;
        private Texture2D hpBarTextureRood = null;

        public Vector2 boerderijPositie { get; private set; }
        private Vector2 hpBarPositie = Vector2.Zero;

        private Rectangle rectangleHP;

        public GameFrame()
        {
            mainTexture = Program.INSTANCE.Content.Load<Texture2D>("Border");

            boerderijTexture = Program.INSTANCE.Content.Load<Texture2D>("Boerderij");
            boerderijPositie = new Vector2(Width - boerderijTexture.Width / 2, Height - boerderijTexture.Height * 1.2f);

            hpBarTextureRood = Program.INSTANCE.Content.Load<Texture2D>("HPBarRood");
            hpBarTextureGroen = Program.INSTANCE.Content.Load<Texture2D>("HPBarGroen");
            hpBarPositie = new Vector2((Width - hpBarTextureGroen.Width) / 1.025f , 0 + hpBarTextureGroen.Height);

            rectangleHP = new Rectangle(0, 0, hpBarTextureGroen.Width, hpBarTextureGroen.Height);
        }

        public void Draw()
        {
            if (mainTexture != null)
            {
                Program.INSTANCE.spriteBatch.Draw(mainTexture, Vector2.Zero, Color.White);
                Program.INSTANCE.spriteBatch.Draw(Program.INSTANCE.player.definition.personTexture, Program.INSTANCE.player.BoerLocatie, Color.Black);
                Program.INSTANCE.spriteBatch.Draw(boerderijTexture, boerderijPositie, Color.White);
                Program.INSTANCE.spriteBatch.Draw(hpBarTextureRood, hpBarPositie, Color.White);
                Program.INSTANCE.spriteBatch.Draw(hpBarTextureGroen, hpBarPositie, rectangleHP, Color.White);
            }
        }

        public void UpdateHP(int hp)
        {
            double factor = ((double)hp / (double)Player.maxhp);
            rectangleHP = new Rectangle(0, 0, (int)(hpBarTextureGroen.Width * factor), hpBarTextureGroen.Height);
        }
    }
}
