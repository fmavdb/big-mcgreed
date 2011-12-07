using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.logic.projectile
{
    public class Projectile : Locatable
    {
        public int type { get; private set; }

        public ProjectileDefinition definition { get; set; }

        public Projectile(int type)
        {
            this.type = type;
            definition = ProjectileDefinition.forType(type);
        }
    }
}
