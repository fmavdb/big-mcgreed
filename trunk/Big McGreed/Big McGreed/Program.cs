using System;

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
            INSTANCE = new GameWorld();
            using (INSTANCE)
            {
                INSTANCE.Run();
            }
        }
    }
#endif
}

