using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.player;
using Big_McGreed.utility;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.mask;
using Big_McGreed.engine.ticks;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Big_McGreed.logic
{
    /// <summary>
    /// Represents an entity; Player or NPC
    /// </summary>
    public abstract class Entity : Locatable, IRunnable
    {
        /// <summary>
        /// Gets the received hits.
        /// </summary>
        protected Queue<Hit> receivedHits { get; private set; }

        /// <summary>
        /// Gets the ticks.
        /// </summary>
        protected Hashtable ticks { get; private set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        public int damage { get; protected set; }

        private int lifes;

        /// <summary>
        /// Gets or sets the lifes.
        /// </summary>
        /// <value>
        /// The lifes.
        /// </value>
        public int Lifes {
            get
            {
                return lifes;
            }
            
            set 
            { 
                this.lifes = value;
                if (this is Player)
                {
                    Program.INSTANCE.gameFrame.UpdateHP(this.lifes);
                }
            } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Entity"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool visible { get; protected set; }

        /// <summary>
        /// Wether the entity is hitted or not.
        /// </summary>
        protected int hitted = -1;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Entity"/> is destroyed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if destroyed; otherwise, <c>false</c>.
        /// </value>
        public bool disposed { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            visible = false;
            receivedHits = new Queue<Hit>();
            ticks = new Hashtable();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void run()
        {
            lock (ticks.SyncRoot)
            {
                foreach (Tick tick in ticks)
                {
                    if (tick.getTimeRemaining() <= 0)
                    {
                        tick.run();
                        tick.updateLastRun();
                    }
                }
            }
            run2();

            processHits();
        }

        /// <summary>
        /// Processes the hits.
        /// </summary>
        protected void processHits()
        {
            if (receivedHits.Count > 0)
            {
                Hit hit = receivedHits.Dequeue();
                if (hit != null)
                {
                    Lifes -= hit.Damage;
                    if (Lifes <= 0)
                    {
                        if (this is NPC)
                        {
                            ((NPC)this).Dispose();
                            if (hit.From is Player)
                                ((Player)hit.From).gold += ((NPC)this).definition.gold;
                        }
                        else if (this is Player)
                        {
                            Program.INSTANCE.CurrentGameState = GameWorld.GameState.GameOver;
                        }
                    } 
                }
            }
        }

        /// <summary>
        /// Hits the specified hit.
        /// </summary>
        /// <param name="hit">The hit.</param>
        public void hit(Hit hit)
        {
            lock (receivedHits)
            {
                receivedHits.Enqueue(hit);
                hitted = 25;
                if (this is NPC)
                {
                    ((NPC)this).attacking = false;
                    //setX(getX() * 0.95f);
                }
            }
        }

        /// <summary>
        /// Updates the lifes.
        /// </summary>
        /// <param name="lifes">The lifes.</param>
        protected void updateLifes(int lifes)
        {
            this.lifes = lifes;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected abstract void run2();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public abstract void Draw(SpriteBatch batch);

        /// <summary>
        /// Registers the tick.
        /// </summary>
        /// <param name="tick">The tick.</param>
        /// <param name="identifier">The identifier.</param>
        public void registerTick(Tick tick, string identifier)
        {
            if (identifier == null)
                tick.identifier = "event";
            lock (ticks.SyncRoot)
            {
                ticks.Add(identifier, tick);
            }
        }

        /// <summary>
        /// Removes the tick.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        public void removeTick(string identifier)
        {
            lock (ticks.SyncRoot)
            {
                ticks.Remove(identifier);
            }
        }
    }
}
