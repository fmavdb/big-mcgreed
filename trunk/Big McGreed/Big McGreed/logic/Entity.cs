using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.player;
using Big_McGreed.utility;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.mask;

namespace Big_McGreed.logic
{
    public abstract class Entity : Locatable, Runnable
    {
        //NPC of Speler gebruiken deze gemeenschappelijke class.

        public Queue<Hit> receivedHits { get; set; }

        public int lifes { get; private set; }

        public bool visible { get; set; }

        public int radius { get; set; }

        public Entity()
        {
            visible = false;
            receivedHits = new Queue<Hit>();
        }

        /*
         * Algemene update.
         */
        public void run()
        {
            run2();

            processHits();
        }

        public void processHits()
        {
            if (receivedHits.Count > 0)
            {
                Hit hit = receivedHits.Dequeue();
                lifes -= hit.damage;
                if (lifes <= 0)
                {
                    //entity is dead.
                }
            }
        }

        /*
         * Specifieke update.
         */
        protected abstract void run2();

        public abstract void Draw();

    }
}
