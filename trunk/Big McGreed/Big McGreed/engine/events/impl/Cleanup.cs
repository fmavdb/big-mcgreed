using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.engine.events.impl
{
    class Cleanup : Event
    {
        public Cleanup()
        {
            this.delay = 30000; //Every 30 seconds, run memory cleanup.
        }

        public override void run()
        {
            System.GC.Collect();
        }
    }
}
