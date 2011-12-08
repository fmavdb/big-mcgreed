using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map.objects
{
	public class ObjectDefinition
	{
        public Texture2D mainTexture { get; set; }

        public Color[] pixels { get; set; }

        public static ObjectDefinition forType(int type)
        {
            ObjectDefinition def = new ObjectDefinition();
            //def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
            //def.pixels = new Color[def.mainTexture.Width * def.mainTexture.Height];
            //def.mainTexture.GetData<Color>(def.pixels);
            return def;
        }

        public ObjectDefinition()
        {
            mainTexture = null;
        }
	}
}
