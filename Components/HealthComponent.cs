using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSIsaac.Components
{
    class HealthComponent
    {
        int health;

        Action<int, int> changed;
        Action died;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                changed?.Invoke(value, health - value);
                health = value;
                if (health <= 0)
                    died?.Invoke();
            }
        }

        public event Action<int, int> Changed { add => changed += value; remove => changed -= value; }
        public event Action Died { add => died += value; remove => died -= value; }

        public HealthComponent(int health)
        {
            this.health = health;
        }
    }
}
