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

        private Random soundRandom = new Random();

        private Vector2 velocity = Vector2.Zero;

        /// <summary>
        /// Gets or sets the last hit.
        /// </summary>
        /// <value>
        /// The last hit.
        /// </value>
        public DateTime lastHit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NPC"/> is attacking.
        /// </summary>
        /// <value>
        ///   <c>true</c> if attacking; otherwise, <c>false</c>.
        /// </value>
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
            velocity = new Vector2(definition.speed, 0f);
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
            velocity = new Vector2(definition.speed, 0f);
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
            int soundIndex = soundRandom.Next(1, 2);
            Console.WriteLine(soundIndex);

            if (type == LevelInformation.forValue(Program.INSTANCE.player.currentLevel).bossType)
            {
                if (Program.INSTANCE.player.currentLevel == 1)
                {
                    Program.INSTANCE.player.currentLevel = 2;
                    if (soundIndex == 1)
                    {
                        Program.INSTANCE.defeatBoss.PlaySound();
                    }
                    else if (soundIndex == 2)
                    {
                        Program.INSTANCE.defeatBoss2.PlaySound();
                    }
                    Program.INSTANCE.CurrentGameState = GameWorld.GameState.Upgrade;
                }
                else if (Program.INSTANCE.player.currentLevel == 2)
                {
                    Program.INSTANCE.player.currentLevel = 3;
                    Program.INSTANCE.CurrentGameState = GameWorld.GameState.Upgrade;
                }
                else if (Program.INSTANCE.player.currentLevel == 3)
                {
                    Program.INSTANCE.IManager.gameOverInterface.gameWon = true;
                    Program.INSTANCE.CurrentGameState = GameWorld.GameState.GameOver;
                }
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
            Color toColor = Color.White;
            if (hitted > 0)
            {
                toColor = Color.Red;
                hitted--;
            }
            else if (hitted == 0)
            {
                hitted = -1;
            }
            batch.Draw(toDraw.GetTexture(), getLocation(), toColor);
        }

        public void Update(GameTime gameTime)
        {
            definition.mainTexture.Update(gameTime.ElapsedGameTime.Ticks);
        }
    }
}
