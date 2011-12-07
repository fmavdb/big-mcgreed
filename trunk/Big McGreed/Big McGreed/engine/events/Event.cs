﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.utility;

namespace Big_McGreed.engine.events
{
	public abstract class Event : Runnable
	{
        public int delay { get; set; }
        private DateTime lastRun { get; set; }
        protected bool running = false;

        public Event()
        {
            lastRun = DateTime.Now;
            running = true;
        }

        public abstract void run();

        public int getTimeRemaining()
        {
            return delay - ((int) (DateTime.Now.Millisecond - lastRun.Millisecond));
        }

        public void updateLastRun()
        {
            lastRun = DateTime.Now;
        }

        public bool isRunning()
        {
            return running;
        }
	}
}
