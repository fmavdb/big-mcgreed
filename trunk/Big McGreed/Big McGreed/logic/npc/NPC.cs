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

namespace Big_McGreed.logic.npc
{
    public class NPC : Entity, Destroyable
    {
        public int type { get; private set; }

        public NPCDefinition definition { get { return NPCDefinition.forType(type); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPC"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public NPC(int type)
        {
            this.type = type;
            updateLifes(definition.hp);
            visible = true;
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void destroy()
        {
            destroyed = true;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected override void run2()
        {
            setLocation(PrimitivePathFinder.getPosition(translate(1, 0), definition.mainTexture.Width, definition.mainTexture.Height, 0));
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw() 
        {
            Texture2D toDraw = definition.mainTexture;
            if (hitted)
            {
                toDraw = definition.hittedTexture;
                hitted = false;
            }
            Program.INSTANCE.spriteBatch.Draw(toDraw, getLocation(), Color.White);
        }
    }
}
