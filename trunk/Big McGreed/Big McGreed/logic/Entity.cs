using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.player;
using Big_McGreed.utility;

namespace Big_McGreed.logic
{
    public abstract class Entity : Runnable
    {
        //NPC of Speler gebruiken deze gemeenschappelijke class.

        public int lifes { get; private set; }

        public Entity()
        {
            if (this is Player) //instancof Player
            {
            }
        }

        /*
         * Algemene update.
         */
        public void run()
        {
        }

        protected abstract void run2();
    }
}
