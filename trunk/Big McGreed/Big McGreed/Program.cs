using System;
using System.Windows.Forms;
using Big_McGreed.engine;
using Big_McGreed.engine.events.impl;

namespace Big_McGreed
{
#if WINDOWS || XBOX
    static class Program
    {

        public static GameWorld INSTANCE = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            startXNA();
        }

        public static void startXNA()
        {
            INSTANCE = new GameWorld();
            using (INSTANCE)
            {
                INSTANCE.Run();
            }
            Engine.getInstance().submitEvent(new Cleanup());
        }
    }
#endif
}

