using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Big_McGreed.logic.npc;

namespace Big_McGreed.engine.update
{
    public class NPCUpdate
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

        public NPCUpdate()
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
                        lock (Program.INSTANCE.getNPCs())
                        {
                            foreach (NPC npc in Program.INSTANCE.getNPCs())
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

        public void Draw()
        {
            lock (Program.INSTANCE.getNPCs())
            {
                foreach (NPC npc in Program.INSTANCE.getNPCs())
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
