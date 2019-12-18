using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Systems
{
    class MoveSystem : BasicSystem
    {
        public override void Process()
        {
            List<MoveComponent> components = ComponentStorage<MoveComponent>.Components;
            for (int i = 0; i < components.Count; i++)
            {
                Vector2f nextPos = components[i].GetNextPosition();
                if (nextPos.X == 0 && nextPos.Y == 0)
                    return;
                else if(components[i].CheckMove(nextPos))
                    components[i].Move(nextPos);
            }
        }
    }
}
