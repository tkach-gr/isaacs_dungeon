using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using ECSIsaac.Components;

namespace ECSIsaac.Systems
{
    class DisplaySystem : BasicSystem
    {
        RenderTarget target;

        public DisplaySystem(RenderTarget target)
        {
            this.target = target;
        }

        public override void Process()
        {
            foreach(Drawable item in ComponentStorage<Drawable>.Components)
            {
                target.Draw(item);
            }
        }
    }
}
