using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.player;
using Big_McGreed.utility;
using Big_McGreed.logic.npc;

namespace Big_McGreed.logic
{
    public abstract class Entity : Locatable, Runnable
    {
        //NPC of Speler gebruiken deze gemeenschappelijke class.

        public int lifes { get; private set; }

        public bool visible { get; set; }

        public int radius { get; set; }

        public Entity()
        {
            visible = false;
        }

        /*
         * Algemene update.
         */
        public void run()
        {

        }

        /*
         * Specifieke update.
         */
        protected abstract void run2();

        public abstract void Draw();

    }
}
