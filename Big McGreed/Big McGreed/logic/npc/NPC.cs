using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Big_McGreed.utility;
using Big_McGreed.logic.map;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.logic.npc
{
    /// <summary>
    /// Represents a NPC.
    /// </summary>
    public class NPC : Entity, IDisposable
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        public int type { get; private set; }

        private Vector2 velocity = Vector2.Zero;

        /// <summary>
        /// Gets the definition.
        /// </summary>
        public NPCDefinition definition { get { return NPCDefinition.forType(type); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPC"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="location">The location.</param>
        public NPC(int type, Vector2 location)
        {
            this.type = type;
            setLocation(location);
            velocity = new Vector2(5f, 0f);
            visible = true;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPC"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public NPC(int type)
        {
            this.type = type;
            velocity = new Vector2(5f, 0f);
            visible = true;
            Initialize();
        }

        private void Initialize()
        {
            updateLifes(definition.hp);
            damage = definition.damage;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            disposed = true;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected override void run2()
        {
            setLocation(getLocation() + velocity);
            
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public override void Draw(SpriteBatch batch) 
        {
            Texture2D toDraw = definition.mainTexture;
            if (hitted)
            {
                toDraw = definition.hittedTexture;
                hitted = false;
            }
            batch.Draw(toDraw, getLocation(), Color.White);
        }
    }
}
