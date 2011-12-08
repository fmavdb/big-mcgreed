using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Big_McGreed.engine.misc
{
    public class CleanUp
    {
        public static CleanUp INSTANCE = new CleanUp();

        private Thread thread;
        private bool running = false;

        public CleanUp()
        {
            thread = new Thread(new ThreadStart(run));
            thread.Priority = ThreadPriority.Lowest;
        }

        public void start()
        {
            running = true;
            thread.Start();
        }

        public void stop()
        {
            running = false;
            thread.Abort();
        }

        protected void run()
        {
            while (running)
            {
                System.GC.Collect();
                System.Threading.Thread.Sleep(30000); //30 seconds
            }
        }
    }
}
