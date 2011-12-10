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

        /// <summary>
        /// Initializes a new definition for the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ObjectDefinition forType(int type)
        {
            ObjectDefinition def = (ObjectDefinition)GameWorld.objectDefinitions[type];
            if (def == null)
            {
                //def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
                //def.pixels = new Color[def.mainTexture.Width * def.mainTexture.Height];
                //def.mainTexture.GetData<Color>(def.pixels);
                GameWorld.objectDefinitions.Add(type, def);
            }
            return def;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDefinition"/> class.
        /// </summary>
        public ObjectDefinition()
        {
            mainTexture = null;
        }
	}
}
