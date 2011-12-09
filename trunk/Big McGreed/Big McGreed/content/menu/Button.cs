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

        protected Texture2D current;

        public Texture2D Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
            }
        }

        protected Texture2D normal;

        public Texture2D Normal
        {
            get
            {
                return normal;
            }
            set
            {
                normal = value;
            }
        }

        public Texture2D pressed;

        public Texture2D Pressed
        {
            get
            {
                return pressed;
            }
            set
            {
                pressed = value;
            }
        }

        protected Texture2D hover;

        public Texture2D Hover
        {
            get
            {
                return hover;
            }
            set
            {
                hover = value;
            }
        }

        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(current, location, Color.White);
        }
    }
}
