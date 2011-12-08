using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Big_McGreed.logic;

namespace Big_McGreed.engine.update
{
    public class PlayerUpdate
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
                switch(Program.INSTANCE.getGameState()) {
                    case GameWorld.GameState.InGame:
                        if (Program.INSTANCE.getPlayer() != null)
                        {
                            EntityManipulation.PlayerRun(Program.INSTANCE.getPlayer());
                        }
                    break;
                }
                System.Threading.Thread.Sleep(25);
            }
        }
    }
}
