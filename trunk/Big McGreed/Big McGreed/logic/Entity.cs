using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.player;
using Big_McGreed.utility;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.mask;
using Big_McGreed.engine.ticks;

namespace Big_McGreed.logic
{
    public abstract class Entity : Locatable, Runnable
    {
        //NPC of Speler gebruiken deze gemeenschappelijke class.

        protected Queue<Hit> receivedHits { get; private set; }

        protected Hashtable ticks { get; private set; }

        protected int lifes { get; private set; }

        public bool visible { get; set; }

        public Entity()
        {
            visible = false;
            receivedHits = new Queue<Hit>();
            ticks = new Hashtable();
        }

        /*
         * Algemene update.
         */
        public void run()
        {
            lock (ticks.SyncRoot)
            {
                foreach (Tick tick in ticks)
                {
                    if (tick.getTimeRemaining() <= 0)
                    {
                        tick.run();
                        tick.updateLastRun();
                    }
                }
            }
            run2();

            processHits();
        }

        protected void processHits()
        {
            if (receivedHits.Count > 0)
            {
                Hit hit = receivedHits.Dequeue();
                lifes -= hit.damage;
                if (lifes <= 0)
                {
                    visible = false; //TODO
                }
            }
        }

        public void hit(Hit hit)
        {
            lock (receivedHits)
            {
                receivedHits.Enqueue(hit);
            }
        }

        protected void updateLifes(int lifes)
        {
            this.lifes = lifes;
        }

        /*
         * Specifieke update.
         */
        protected abstract void run2();

        /*
         * Specifieke draw.
         */
        public abstract void Draw();

        public void registerTick(Tick tick, string identifier)
        {
            if (identifier == null)
                tick.identifier = "event";
            lock (ticks.SyncRoot)
            {
                ticks.Add(identifier, tick);
            }
        }
    
        public void removeTick(string identifier)
        {
            lock (ticks.SyncRoot)
            {
                ticks.Remove(identifier);
            }
        }
    }
}
