using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Entities
{
    class Walls
    {
        List<DisplayComponent> cells;

        public Walls()
        {
            Vector2f size = new Vector2f();
            size.X = (Config.RoomRect.Width) / 16 / Config.TextureScale.X;
            size.Y = (Config.RoomRect.Height) / 16 / Config.TextureScale.Y + 1;

            cells = new List<DisplayComponent>();

            // Top left corner
            Vector2f position = new Vector2f(Config.RoomRect.Left - 48, Config.RoomRect.Top - 96);
            cells.Add(new DisplayComponent("wall_side_top_left", position));
            // Top roght corner
            position = new Vector2f(Config.RoomRect.Left + Config.RoomRect.Width, Config.RoomRect.Top - 96);
            cells.Add(new DisplayComponent("wall_side_top_right", position));
            // Bottom left corner
            position = new Vector2f(Config.RoomRect.Left - 48, Config.RoomRect.Top + Config.RoomRect.Height);
            cells.Add(new DisplayComponent("wall_side_front_left", position));
            // Bottom right corner
            position = new Vector2f(Config.RoomRect.Left + Config.RoomRect.Width, Config.RoomRect.Top + Config.RoomRect.Height);
            cells.Add(new DisplayComponent("wall_side_front_right", position));

            for (int i = 0; i < size.X; i++)
            {
                position = new Vector2f(Config.RoomRect.Left + i * 48, Config.RoomRect.Top - 48);
                cells.Add(new DisplayComponent("wall_mid", position));

                position = new Vector2f(Config.RoomRect.Left + i * 48, Config.RoomRect.Top - 96);
                cells.Add(new DisplayComponent("wall_top_mid", position));

                position = new Vector2f(Config.RoomRect.Left + i * 48, Config.RoomRect.Top + Config.RoomRect.Height);
                cells.Add(new DisplayComponent("wall_mid", position));

                position = new Vector2f(Config.RoomRect.Left + i * 48, Config.RoomRect.Top + Config.RoomRect.Height - 48);
                cells.Add(new DisplayComponent("wall_top_mid", position));
            }

            for (int i = 0; i < size.Y; i++)
            {
                position = new Vector2f(Config.RoomRect.Left - 48, Config.RoomRect.Top - 48 + i * 48);
                cells.Add(new DisplayComponent("wall_side_mid_left", position));

                position = new Vector2f(Config.RoomRect.Left + Config.RoomRect.Width, Config.RoomRect.Top - 48 + i * 48);
                cells.Add(new DisplayComponent("wall_side_mid_right", position));
            }
        }
    }
}
