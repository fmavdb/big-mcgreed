using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.projectile
{
    public class ProjectileDefinition
    {
        public Texture2D mainTexture { get; set; }

        public static ProjectileDefinition forType(int type)
        {
            ProjectileDefinition def = new ProjectileDefinition();
            //def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
            return def;
        }

        public ProjectileDefinition()
        {
            mainTexture = null;
        }
    }
}
