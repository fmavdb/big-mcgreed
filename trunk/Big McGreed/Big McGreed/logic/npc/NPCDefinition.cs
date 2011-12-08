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

        public Color[] pixels { get; set; }

        public static NPCDefinition forType(int type)
        {
            NPCDefinition def = new NPCDefinition();
            def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
            //def.pixels = new Color[def.mainTexture.Width * def.mainTexture.Height];
            //def.mainTexture.GetData<Color>(def.pixels);
            return def;
        }

        public NPCDefinition()
        {
            mainTexture = null;
        }
    }
}
