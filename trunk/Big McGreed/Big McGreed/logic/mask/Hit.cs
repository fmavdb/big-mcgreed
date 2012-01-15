using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.logic.mask
{
    public class Hit
    {
        public int Damage { get; private set; }

        public Entity To { get; set; }

        public Entity From { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hit"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="damage">The damage.</param>
        public Hit(Entity to, Entity from, int damage)
        {
            this.To = to;
            this.From = from;
            this.Damage = damage;
        }
    }
}
