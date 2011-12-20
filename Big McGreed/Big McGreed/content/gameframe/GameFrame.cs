using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.gameframe
{
    public class GameFrame
    {
        public static int Width;

        public static int Height;

        private Texture2D mainTexture = null;

        public GameFrame()
        {
            mainTexture = Program.INSTANCE.Content.Load<Texture2D>("Border");
        }

        public void Draw()
        {
            if (mainTexture != null)
            {
                Program.INSTANCE.spriteBatch.Draw(mainTexture, Vector2.Zero, Color.White);
            }
        }
    }
}
