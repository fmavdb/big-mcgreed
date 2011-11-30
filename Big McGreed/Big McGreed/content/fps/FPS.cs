using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.fps
{
    public class FPS
    {
        private SpriteFont spriteFont;
        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        public FPS()
        {
            spriteFont = Program.INSTANCE.Content.Load<SpriteFont>("FPS");
        }

        public void update(GameTime gameTime)
        {
            ///////////////FPS/////////////////
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

        }

        public void draw()
        {
            frameCounter++;
            Program.INSTANCE.spriteBatch.DrawString(spriteFont, frameRate.ToString(), Vector2.One, Color.White);
        }
    }
}
