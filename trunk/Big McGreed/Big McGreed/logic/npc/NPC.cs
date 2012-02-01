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
using Big_McGreed.content.level;
using XNAGifAnimation;

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
        /// Gets or sets the last hit.
        /// </summary>
        /// <value>
        /// The last hit.
        /// </value>
        public DateTime lastHit { get; set; }

        public bool attacking { get; set; }

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
            lastHit = DateTime.Now;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (type == LevelInformation.forValue(Program.INSTANCE.player.currentLevel).bossType)
            {
                //TODO - Boss is dead, proceed to next level? Player has won the game?
            }
            disposed = true;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected override void run2()
        {
            if (!PrimitivePathFinder.collision(Program.INSTANCE.player, this))
            {
                setLocation(getLocation() + velocity);
            }            
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="batch">The batch.</param>
        public override void Draw(SpriteBatch batch) 
        {
            GifAnimation toDraw = definition.mainTexture;
            if (hitted)
            {
                //toDraw = definition.hittedTexture;
                hitted = false;
            }
            batch.Draw(toDraw.GetTexture(), getLocation(), Color.Tan);
        }

        public void Update(GameTime gameTime)
        {
            definition.mainTexture.Update(gameTime.ElapsedGameTime.Ticks);
        }
    }
}
