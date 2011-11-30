using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.logic.mask
{
    public class Hit
    {
        /*
         * Stelt een hit voor, kan van een NPC of van de Player zijn.
         */

        public int damage { get; set; }

        public Entity to { get; set; }

        public Entity from { get; set; }

        public Hit(Entity to, Entity from, int damage)
        {
            this.to = to;
            this.from = from;
            this.damage = damage;
        }
    }
}
