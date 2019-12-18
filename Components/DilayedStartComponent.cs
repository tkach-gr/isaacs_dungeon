using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSIsaac.Components
{
    class DilayedStartComponent
    {
        double delay;
        double startTime;

        public bool IsTimeOver => startTime + delay < GameTimer.Elapsed;

        Action someAction;

        public event Action SomeAction_Event { add => someAction += value; remove => someAction += value; }

        public DilayedStartComponent(double delay)
        {
            ComponentStorage<DilayedStartComponent>.Components.Add(this);

            this.delay = delay;
            startTime = GameTimer.Elapsed;
        }

        public void SomeAction()
        {
            someAction();
        }
    }
}
