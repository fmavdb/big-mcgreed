using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.map.objects
{
    public class GameObject : Locatable, IDisposable
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="GameObject"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool disposed { get; private set; }

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
                velocity = new Vector2(1f, 0f);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            disposed = true;
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            if (velocity != Vector2.Zero)
            {
                if (getLocation().X - definition.mainTexture.Width <= GameFrame.Width)
                {
                    setLocation(getLocation() + velocity);
                }
                else
                {
                    try
                    {
                        setX(-GameWorld.random.Next(definition.mainTexture.Width, GameWorld.random.Next(1, GameFrame.Width)));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR:" + ex);
                    }
                }
            }
            if ((getX() + definition.mainTexture.Width) > 0 && getX() <= GameFrame.Width)
            {
                batch.Draw(definition.mainTexture, getLocation(), Color.White);
            }
        }
    }
}
