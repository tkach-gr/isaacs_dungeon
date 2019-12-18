using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using ECSIsaac.Components;

namespace ECSIsaac.Systems
{
    class DilayedStartSystem : BasicSystem
    {
        public override void Process()
        {
            List<DilayedStartComponent> components = ComponentStorage<DilayedStartComponent>.Components;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].IsTimeOver)
                {
                    components[i].SomeAction();
                    components.Remove(components[i]);
                    i--;
                }
            }
        }
    }
}
