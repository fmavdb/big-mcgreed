using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.npc
{
    public class NPCDefinition
    {
        public Texture2D mainTexture { get; set; }

        public Color[,] pixels { get; private set; }

        public Texture2D hittedTexture { get; private set; }

        public int hp = 0;

        /// <summary>
        /// Initializes a new instance for the npc type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static NPCDefinition forType(int type)
        {
            NPCDefinition def = (NPCDefinition)GameWorld.npcDefinitions[type];
            if (def == null)
            {
                def = new NPCDefinition();
                def.mainTexture = Program.INSTANCE.loadTexture("poppetje");
                def.hittedTexture = Program.INSTANCE.loadTexture("poppetje_rood");
                Color[] colors1D = new Color[def.mainTexture.Width * def.mainTexture.Height];
                def.mainTexture.GetData<Color>(colors1D);
                def.pixels = new Color[def.mainTexture.Width, def.mainTexture.Height];
                for (int x = 0; x < def.mainTexture.Width; x++)
                    for (int y = 0; y < def.mainTexture.Height; y++)
                        def.pixels[x, y] = colors1D[x + y * def.mainTexture.Width];
                def.hp = 20;
                GameWorld.npcDefinitions.Add(type, def);
            }
            return def;
        }

        public NPCDefinition()
        {
            mainTexture = null;
        }
    }
}
