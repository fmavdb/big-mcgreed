using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Big_McGreed.logic.npc;
using System.Threading.Tasks;

namespace Big_McGreed.engine.update
{
    public class NPCUpdate
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
        /// Initializes a new instance of the <see cref="NPCUpdate"/> class.
        /// </summary>
        public NPCUpdate()
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
                        lock (Program.INSTANCE.npcs)
                        {
                            foreach (NPC npc in Program.INSTANCE.npcs)
                            {
                                if (npc.definition.mainTexture != null)
                                {
                                    npc.run();
                                }
                            }
                        }
                    break;
                }
                System.Threading.Thread.Sleep(25);
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            lock (Program.INSTANCE.npcs)
            {
                foreach (NPC npc in Program.INSTANCE.npcs)
                {
                    if (npc.visible && npc.definition.mainTexture != null)
                    {
                        npc.Draw();
                    }
                }
            }
        }
    }
}
