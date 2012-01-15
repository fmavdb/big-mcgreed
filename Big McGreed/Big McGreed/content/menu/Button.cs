﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu
{
    /// <summary>
    /// Represents a button or an interface. 
    /// Als je een betere naam weet zeg ut dan :P
    /// </summary>
    public abstract class Button
    {
        /// <summary>
        /// Wether the button is tiny or not.
        /// </summary>
        protected bool tinyButton = false;

        /// <summary>
        /// Wether this instance is an interface or not.
        /// </summary>
        protected bool isInterface = false;

        /// <summary>
        /// Executes the action.
        /// </summary>
        public abstract void action();

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
            font = Program.INSTANCE.Content.Load<SpriteFont>("ButtonFont");
            tinyFont = Program.INSTANCE.Content.Load<SpriteFont>("TinyButtonFont");
        }

        private Vector2 location;

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
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
                    if (!isInterface)
                    {
                        textLocation = new Vector2(value.X - font.MeasureString(text).X / 2 + current.Width / 2, value.Y - font.LineSpacing / 2 + current.Height / 2);
                    }
                }
                else
                {
                    textLocation = new Vector2(value.X - tinyFont.MeasureString(text).X / 2 + current.Width / 2, value.Y - tinyFont.LineSpacing / 2 + current.Height / 2);
                }
            }
        }

        protected string text = "";

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
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

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
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

        /// <summary>
        /// Gets or sets the tiny font.
        /// </summary>
        /// <value>
        /// The tiny font.
        /// </value>
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

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
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

        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        /// <value>
        /// The normal.
        /// </value>
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

        /// <summary>
        /// Gets or sets the pressed.
        /// </summary>
        /// <value>
        /// The pressed.
        /// </value>
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

        /// <summary>
        /// Gets or sets the hover.
        /// </summary>
        /// <value>
        /// The hover.
        /// </value>
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

        protected Vector2 textLocation = Vector2.Zero;

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(current, location, Color.White);
            batch.DrawString(tinyButton ? tinyFont : font, text, textLocation, Color.White);
        }
    }
}
