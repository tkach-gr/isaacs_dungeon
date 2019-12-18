using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Entities
{
    class HealthBar
    {
        enum HeartState
        {
            Empty,
            Half,
            Full
        }

        List<DisplayComponent> hearts;
        int health;

        public Vector2f Position { get; set; }

        public HealthBar(int health, Vector2f position)
        {
            Position = position;
            hearts = new List<DisplayComponent>();
            UpdateHealth(health);
            this.health = health;
        }

        private void AddHearts(HeartState heartState, uint count = 1)
        {
            string imageName = string.Empty;
            if (heartState == HeartState.Empty)
                imageName = "ui_heart_empty";
            else if (heartState == HeartState.Half)
                imageName = "ui_heart_half";
            else if (heartState == HeartState.Full)
                imageName = "ui_heart_full";

            Vector2u imageSize = ImageManager.Get(imageName).Size;
            
            for (int i = 0; i < count; i++)
            {
                Vector2f heartPosition = new Vector2f(Position.X + (imageSize.X * Config.TextureScale.X * hearts.Count), Position.Y);
                hearts.Add(new DisplayComponent(imageName, heartPosition));
            }
        }

        public void UpdateHealth(int hp)
        {
            if (hp > health)
                AddHearts(HeartState.Full, (uint)(hp - health) / 2);
            
            for (int i = 0; i < hearts.Count; i++)
            {
                if (hp >= 2)
                    hearts[i].UpdateTexture("ui_heart_full");
                else if (hp == 1)
                    hearts[i].UpdateTexture("ui_heart_half");
                else
                    hearts[i].UpdateTexture("ui_heart_empty");

                hp -= 2;
            }
        }
    }
}
