using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.map.objects
{
    public class GameObject : Locatable
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        public int type { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameObject"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool visible { get; private set; }

        private Vector2 velocity = Vector2.Zero;

        /// <summary>
        /// Gets the definition.
        /// </summary>
        public ObjectDefinition definition { get { return ObjectDefinition.forType(type); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="location">The location.</param>
        public GameObject(int type, Vector2 location)
        {
            this.type = type;
            setLocation(location);
            visible = true;
            if (type == 1)
            {
                velocity = new Vector2(2f, 0f);
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            if (velocity != Vector2.Zero)
            {
                if (getLocation().X < GameFrame.Width)
                {
                    setLocation(getLocation() + velocity);
                }
                else
                {
                    setX(GameWorld.random.Next(-ObjectDefinition.forType(1).mainTexture.Width - GameWorld.random.Next(0, 800), -ObjectDefinition.forType(1).mainTexture.Width));
                }
            }
            batch.Draw(definition.mainTexture, getLocation(), Color.White);
        }
    }
}
