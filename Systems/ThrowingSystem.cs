using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Systems
{
    class ThrowingSystem : BasicSystem
    {
        public override void Process()
        {
            foreach (ThrowingComponent item in ComponentStorage<ThrowingComponent>.Components)
            {
                Vector2f? dir = item.CanThrow();
                if (dir != null)
                {
                    item.Throw((Vector2f)dir);
                }
            }
        }
    }
}
