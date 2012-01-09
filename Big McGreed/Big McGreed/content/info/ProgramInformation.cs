using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.info
{
    public class ProgramInformation
    {
        private SpriteFont spriteFont;
        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramInformation"/> class.
        /// </summary>
        public ProgramInformation()
        {
            spriteFont = Program.INSTANCE.Content.Load<SpriteFont>("FPS");
            memory = new Vector2(0, spriteFont.LineSpacing);
            npcs = new Vector2(0, spriteFont.LineSpacing * 2);
            projectiles = new Vector2(0, spriteFont.LineSpacing * 3);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
           {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

        }

        Vector2 memory;
        Vector2 npcs;
        Vector2 projectiles;

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            frameCounter++;
            Program.INSTANCE.spriteBatch.DrawString(spriteFont, "FPS: " + frameRate.ToString(), Vector2.One, Color.White);
            Program.INSTANCE.spriteBatch.DrawString(spriteFont, "Memory: " + System.GC.GetTotalMemory(false), memory, Color.White);
            Program.INSTANCE.spriteBatch.DrawString(spriteFont, "NPC's: " + Program.INSTANCE.npcs.Count, npcs, Color.White);
            Program.INSTANCE.spriteBatch.DrawString(spriteFont, "Projectiles: " + Program.INSTANCE.GameMap.Projectiles.Count, projectiles, Color.White);
        }
    }
}
