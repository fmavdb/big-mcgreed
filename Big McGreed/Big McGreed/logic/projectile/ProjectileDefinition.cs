using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.projectile
{
    public class ProjectileDefinition
    {
        public Texture2D mainTexture { get; set; }

        public Color[,] pixels { get; set; }

        /// <summary>
        /// Returns the definition from the cache.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ProjectileDefinition forType(int type)
        {
            ProjectileDefinition def = (ProjectileDefinition)GameWorld.projectileDefinitions[type];
            if (def == null) 
            {
                def = new ProjectileDefinition();
                def.mainTexture = Program.INSTANCE.loadTexture("bullet_bill");
                Color[] colors1D = new Color[def.mainTexture.Width * def.mainTexture.Height];
                def.mainTexture.GetData<Color>(colors1D);
                def.pixels = new Color[def.mainTexture.Width, def.mainTexture.Height];
                for (int x = 0; x < def.mainTexture.Width; x++)
                    for (int y = 0; y < def.mainTexture.Height; y++)
                        def.pixels[x, y] = colors1D[x + y * def.mainTexture.Width];
                GameWorld.projectileDefinitions.Add(type, def);
            }
            return def;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileDefinition"/> class.
        /// </summary>
        public ProjectileDefinition()
        {
            mainTexture = null;
        }
    }
}
