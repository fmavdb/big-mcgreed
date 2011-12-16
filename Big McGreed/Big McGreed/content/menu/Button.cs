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
        protected bool tinyButton;

        public abstract void action();

        public Button()
        {
            font = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont");
            tinyFont = Program.INSTANCE.Content.Load<SpriteFont>("TinyButtonFont");
        }

        private Vector2 location;

        public Vector2 Location
        {
            get 
            {
                return location;  
            }
            set 
            { 
                location = value;
                if (!tinyButton)
                {
                    middleOfButton = new Vector2(value.X - font.MeasureString(text).X / 2 + current.Width / 2, value.Y - font.LineSpacing / 2 + current.Height / 2);
                }
                else
                {
                    middleOfButton = new Vector2(value.X - tinyFont.MeasureString(text).X / 2 + current.Width / 2, value.Y - tinyFont.LineSpacing / 2 + current.Height / 2);
                }
            }
        }

        protected string text = "";

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        protected static SpriteFont font;

        public SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }

        protected static SpriteFont tinyFont;

        public SpriteFont TinyFont
        {
            get
            {
                return tinyFont;
            }
            set
            {
                tinyFont = value;
            }
        }

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

        protected Texture2D pressed;

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

        private Vector2 middleOfButton = Vector2.Zero;

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(current, location, Color.White);
            Program.INSTANCE.spriteBatch.DrawString(tinyButton ? tinyFont : font, text, middleOfButton, Color.White);
        }
    }
}
