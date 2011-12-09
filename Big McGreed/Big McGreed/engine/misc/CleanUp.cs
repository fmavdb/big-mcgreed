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

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanUp"/> class.
        /// </summary>
        public CleanUp()
        {
            thread = new Thread(new ThreadStart(run));
            thread.Priority = ThreadPriority.Lowest;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            running = true;
            thread.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void stop()
        {
            running = false;
            thread.Abort();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
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
