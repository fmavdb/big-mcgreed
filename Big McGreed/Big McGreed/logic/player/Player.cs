using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.utility;

namespace Big_McGreed.logic.player
{
    public class Player : Entity, Destroyable
    {

        public PlayerDefinition definition { get; set; }

        //Stelt een speler voor.

        public Player()
        {

        }

        /*
         * 'Verwoest' de speler.
         */
        public void destroy()
        {
        }

        /*
         * Specifieke update.
         */
        protected override void run2()
        {
        }

        public override void Draw()
        {
            
        }
    }
}
