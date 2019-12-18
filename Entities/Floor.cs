using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Entities
{
    class Floor
    {
        List<DisplayComponent> cells;

        public Floor()
        {
            Vector2f floorSize = new Vector2f();
            floorSize.X = Config.RoomRect.Width / 16 / Config.TextureScale.X;
            floorSize.Y = Config.RoomRect.Height / 16 / Config.TextureScale.Y;

            cells = new List<DisplayComponent>();

            for (int y = 0; y < floorSize.Y; y++)
            {
                for (int i = 0; i < floorSize.X; i++)
                {
                    cells.Add(new DisplayComponent("floor_1", new Vector2f(Config.RoomRect.Left + i * 48, Config.RoomRect.Top + y * 48)));
                }
            }
        }
    }
}
