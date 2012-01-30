using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.player
{
    public struct PlayerDefinition 
    {
        /// <summary>
        /// Loads the definition.
        /// </summary>
        /// <returns></returns>
        public static PlayerDefinition getDefinition() {

            PlayerDefinition def = GameWorld.playerDefinition;
            if (!def.loaded) 
            {
                def = new PlayerDefinition();
                def.mainTexture = Program.INSTANCE.loadTexture("crosshair");
                def.personTexture = Program.INSTANCE.loadTexture("boer");
                def.loaded = true;
                GameWorld.playerDefinition = def;
            }
            return def;
        }

        public Texture2D mainTexture { get; private set; }

        public Texture2D personTexture { get; private set; }

        public bool loaded { get; private set; }
    }
}
