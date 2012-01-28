using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Big_McGreed.logic.npc;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

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
                            LinkedList<NPC> npcs = new LinkedList<NPC>(Program.INSTANCE.npcs);
                            foreach (NPC npc in npcs)
                            {
                                if (!npc.disposed)
                                {
                                    npc.run();
                                }
                                else
                                {
                                    RemoveNPC(npc);
                                }
                            }
                        }
                    break;
                }
                System.Threading.Thread.Sleep(25);
            }
        }

        private void RemoveNPC(NPC npc)
        {
            lock (Program.INSTANCE.npcs)
            {
                Program.INSTANCE.npcs.Remove(npc);
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            lock (Program.INSTANCE.npcs)
            {
                foreach (NPC npc in Program.INSTANCE.npcs)
                {
                    if (npc.visible && npc.definition.mainTexture != null && !npc.disposed)
                    {
                        npc.Draw(batch);
                    }
                }
            }
        }
    }
}
