using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.utility;

namespace Big_McGreed.engine.events
{
	public abstract class Event : Runnable
	{
        private int delay { get; set; }
        private long lastRun { get; set; }
        protected bool running = false;

        public Event(int delay)
        {
            this.delay = delay;
            //lastRun = 
            running = true;
        }

        public abstract void run();

        public int getTimeRemaining()
        {
            return delay;
        }

        public void updateLastRun()
        {
            //lastRun  =
        }

        public bool isRunning()
        {
            return running;
        }
	}
}
