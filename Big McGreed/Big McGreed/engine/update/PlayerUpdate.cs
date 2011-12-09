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

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            running = true;
            main.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void stop()
        {
            running = false;
            main.Abort();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUpdate"/> class.
        /// </summary>
        public PlayerUpdate()
        {
            main = new Thread(new ThreadStart(run));
            main.Priority = ThreadPriority.AboveNormal;        
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        protected void run()
        {
            while (running)
            {
                switch(Program.INSTANCE.CurrentGameState) {
                    case GameWorld.GameState.InGame:
                        if (Program.INSTANCE.player != null && Program.INSTANCE.player.definition.mainTexture != null)
                        {
                            Program.INSTANCE.player.run();
                        }
                    break;
                }
                System.Threading.Thread.Sleep(25);
            }
        }
    }
}
