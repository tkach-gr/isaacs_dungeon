using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace ECSIsaac.Components
{
    class ThrowingComponent
    {
        double lastShotTime;
        double elapsedTime;
        Returning<Vector2f> canThrow;
        Action<Vector2f> onThrow;
        
        public float RateOfFire { get; set; }

        public event Returning<Vector2f> CanThrow_Event { add => canThrow += value; remove => canThrow -= value; }
        public event Action<Vector2f> OnThrow { add => onThrow += value; remove => onThrow -= value; }

        public ThrowingComponent()
        {
            ComponentStorage<ThrowingComponent>.Components.Add(this);

            lastShotTime = double.MinValue;
            RateOfFire = Config.HeroRateOfFire;
        }

        public Vector2f? CanThrow()
        {
            elapsedTime += GameTimer.FrameTime;
            if (lastShotTime + RateOfFire * 100 < elapsedTime)
            {
                Vector2f dir = canThrow.Invoke();
                if (dir.X != 0 || dir.Y != 0)
                {
                    lastShotTime = elapsedTime;
                    return dir;
                }
                
                return null;
            }

            return null;
        }

        public void Throw(Vector2f direction)
        {
            onThrow.Invoke(direction);
        }
    }
}
