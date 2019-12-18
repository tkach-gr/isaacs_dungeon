using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace ECSIsaac.Components
{
    class MoveComponent
    {
        DisplayComponent displayComponent;
        Returning<Vector2f> getNextPosition;
        List<Predicate<Vector2f>> checkMove;

        public double Speed { get; set; }

        public event Returning<Vector2f> GetNextPosition_Event { add => getNextPosition += value; remove => getNextPosition -= value; }

        public event Predicate<Vector2f> CheckMove_Event { add => checkMove.Add(value); remove => checkMove.Remove(value); }

        public MoveComponent(DisplayComponent displayComponent, double speed)
        {
            ComponentStorage<MoveComponent>.Components.Add(this);

            checkMove = new List<Predicate<Vector2f>>();
            this.displayComponent = displayComponent;
            this.Speed = speed;
        }

        public Vector2f GetNextPosition()
        {
            return getNextPosition();
        }

        public bool CheckMove(Vector2f pos)
        {
            foreach (Predicate<Vector2f> predicate in checkMove)
            {
                if (predicate(pos) != true)
                    return false;
            }

            return true;
        }

        public void Move(Vector2f pos)
        {
            displayComponent.Position = pos;
        }

        public Vector2f GetVelocity(Vector2f direction)
        {
            direction.X *= (float)GameTimer.FrameTime / 10 * (float)Speed;
            direction.Y *= (float)GameTimer.FrameTime / 10 * (float)Speed;
            return direction;
        }
    }
}
