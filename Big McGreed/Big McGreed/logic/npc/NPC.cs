using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.utility;

namespace Big_McGreed.logic.npc
{
    public class NPC : Entity, Destroyable
    {
        //Stelt een door de computer gestuurde entiteit voor.

        public NPCDefinition definition { get; set; }

        public NPC()
        {
        }

        /*
         * 'Verwoest' de NPC.
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

        public override void Draw() {
        }
    }
}
