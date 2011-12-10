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
            ProjectileDefinition def = (ProjectileDefinition)GameWorld.projectileDefinitions[type];
            if (def == null) {
                def = new ProjectileDefinition();
                def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("bullet_bill");
                GameWorld.projectileDefinitions.Add(type, def);
            }
            return def;
        }

        public ProjectileDefinition()
        {
            mainTexture = null;
        }
    }
}
