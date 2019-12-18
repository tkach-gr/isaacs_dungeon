using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSIsaac.Components
{
    class DamageComponent
    {
        double lastAttackTime;

        Action attacked;

        public int Damage { get; set; }
        public float AttackSpeed { get; set; }

        public event Action Attacked { add => attacked += value; remove => attacked -= value; }

        public bool CanAttack
        {
            get
            {
                if (lastAttackTime + AttackSpeed * 10 < GameTimer.Elapsed)
                    return true;
                else
                    return false;
            }
        }

        public DamageComponent(int damage, float attackSpeed)
        {
            Damage = damage;
            AttackSpeed = attackSpeed;
            lastAttackTime = double.MinValue;
        }

        public void Attack()
        {
            attacked?.Invoke();
            lastAttackTime = GameTimer.Elapsed;
        }
    }
}
