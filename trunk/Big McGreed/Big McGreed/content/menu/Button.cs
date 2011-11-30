using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu
{
    public abstract class Button
    {
        public abstract void action();

        public Vector2 location;

        public Texture2D current;

        public Texture2D normal;

        public Texture2D pressed;

        public Texture2D hover;

        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(current, location, Color.White);
        }
    }
}
