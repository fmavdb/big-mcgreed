using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Big_McGreed.content.gameinterface.interfaces;

namespace Big_McGreed.content.gameinterface
{
    public abstract class GameInterface : Locatable
    {
        /// <summary>
        /// Gets or sets the main texture.
        /// </summary>
        /// <value>
        /// The main texture.
        /// </value>
        public Texture2D mainTexture { get; protected set; }

        protected string title = null;

        protected Vector2 titleLocation;

        /// <summary>
        /// Centers the interface.
        /// </summary>
        public void centerInterface()
        {
            setLocation(new Vector2(GameFrame.Width / 2 - mainTexture.Width / 2, GameFrame.Height / 2 - mainTexture.Height / 2));
            if (title != null)
            {
                centerTitle();
            }
        }

        /// <summary>
        /// Centers the title.
        /// </summary>
        public void centerTitle()
        {
            titleLocation = new Vector2(getX() + mainTexture.Width / 2 - Program.INSTANCE.IManager.font.MeasureString(title).X / 2, getY() + Program.INSTANCE.IManager.font.MeasureString(title).Y);
        }

        /// <summary>
        /// Draws the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(mainTexture, getLocation(), Color.White);
            if (title != null)
            {
                batch.DrawString(Program.INSTANCE.IManager.font, title, titleLocation, Color.White);
            }
            if (this is UpgradeAchtergrond)
            {
                ((UpgradeAchtergrond)this).Draw(batch);
            }
        }
    }
}
