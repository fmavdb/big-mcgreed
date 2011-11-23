using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Big_McGreed.logic.utility;
using Big_McGreed.engine.events;
using Big_McGreed.utility;

namespace Big_McGreed.engine
{
    public class Engine : Destroyable
    {
        private static Engine INSTANCE = new Engine();
        protected bool running = false;
        private LinkedList<Event> events = new LinkedList<Event>();

        private Engine()
        {
        }

        public void start()
        {
            running = true;
        }

        public void stop()
        {
            running = false;
        }

        public void destroy()
        {
            running = false;
            lock (events)
            {
                events.Clear();
            }
        }

        public void submitEvent(Event e)
        {
            lock (events)
            {
                events.AddLast(e);
            }
            if (running)
            {
                executeEvent(e);
            }
        }

        private void executeEvent(Event e) {
            if (running)
            {
                Thread subThread = new Thread(e.run);
                e.updateLastRun();
                try
                {
                    subThread.Start();
                }
                catch (Exception ef)
                {
                }
                if (e.isRunning())
                {
                    executeEvent(e);
                }
                else
                {
                    lock (events)
                    {
                        events.Remove(e);
                    }
                }
            }
        }

        public static Engine getInstance()
        {
            return INSTANCE;
        }
    }
}
