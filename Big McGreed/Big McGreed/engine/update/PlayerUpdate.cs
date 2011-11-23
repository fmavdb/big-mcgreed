using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Big_McGreed.engine.update
{
    class PlayerUpdate
    {
        private Thread main;

        protected bool running = false;

        public void start()
        {
            running = true;
            main.Start();
        }

        public void stop()
        {
            running = false;
            main.Abort();
        }

        public PlayerUpdate()
        {
            main = new Thread(new ThreadStart(run));
            main.Priority = ThreadPriority.AboveNormal;          
        }

        protected void run()
        {
            while (running)
            {
                if (Program.INSTANCE.getPlayer() != null)
                {
                    Program.INSTANCE.getPlayer().run();
                }
            }
        }
    }
}
