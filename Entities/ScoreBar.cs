using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ECSIsaac.Entities
{
    class ScoreBar : Drawable
    {
        Text text;
        long score;

        public long Score
        {
            get => score;
            set
            {
                score = value;
                text.DisplayedString = "Score: " + score;
            }
        }

        public ScoreBar()
        {
            ComponentStorage<Drawable>.Components.Add(this);

            Font font = new Font("font.ttf");
            text = new Text("Score: 0", font);
            text.Position = Config.ScoreBarPosition;
            text.CharacterSize = Config.ScoreBarCharacterSize;

            score = 0;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(text);
        }

        public void RemoveComponents()
        {
            ComponentStorage<Drawable>.Components.Remove(this);
        }
    }
}
