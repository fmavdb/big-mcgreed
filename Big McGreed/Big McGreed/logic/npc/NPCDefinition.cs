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

        public Color[] pixels { get; private set; }

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
                def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
                def.hittedTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje_rood");
                def.pixels = new Color[def.mainTexture.Width * def.mainTexture.Height];
                def.mainTexture.GetData<Color>(def.pixels);
                def.hp = 100;
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
