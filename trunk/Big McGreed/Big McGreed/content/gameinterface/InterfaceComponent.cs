using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Big_McGreed.logic.player;

namespace Big_McGreed.content.gameinterface
{
    /// <summary>
    /// Represents a component on an interface.
    /// </summary>
    public abstract class InterfaceComponent
    {
        /// <summary>
        /// Wether the button is tiny or not.
        /// </summary>
        protected bool tinyButton = false;

        /// <summary>
        /// Executes the action.
        /// </summary>
        public abstract void action();

        protected string hoverText = null;

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
                    textLocation = new Vector2(value.X - Program.INSTANCE.IManager.font.MeasureString(text).X / 2 + current.Width / 2, value.Y - Program.INSTANCE.IManager.font.LineSpacing / 2 + current.Height / 2);
                }
                else
                {
                    textLocation = new Vector2(value.X - Program.INSTANCE.IManager.tinyFont.MeasureString(text).X / 2 + current.Width / 2, value.Y - Program.INSTANCE.IManager.tinyFont.LineSpacing / 2 + current.Height / 2);
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

        private bool thisButtonDisabledCrosshair = false;

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(current, location, Color.White);
            batch.DrawString(tinyButton ? Program.INSTANCE.IManager.tinyFont : Program.INSTANCE.IManager.font, text, textLocation, Color.White);
            if (current == hover)
            {
                if (hoverText != null)
                {
                    Player.drawCrosshair = false;
                    thisButtonDisabledCrosshair = true;
                    batch.DrawString(Program.INSTANCE.IManager.tinyFont, hoverText, new Vector2(Mouse.GetState().X, Mouse.GetState().Y - Program.INSTANCE.IManager.tinyFont.MeasureString(hoverText).Y), Color.White);
                }
            }
            else
            {
                if (hoverText != null && thisButtonDisabledCrosshair)
                {
                    Player.drawCrosshair = true;
                    thisButtonDisabledCrosshair = false;
                }
            }
        }
    }
}
