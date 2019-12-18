using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace ECSIsaac.Components
{
    class CollisionComponent
    {
        DisplayComponent displayComponent;
        DamageComponent damageComponent;
        IntRect rect;

        public bool CanAttack => damageComponent.CanAttack;

        public Vector2f Position => new Vector2f(displayComponent.Position.X + rect.Left, displayComponent.Position.Y + rect.Top);
        public Vector2f Size => new Vector2f(rect.Width, rect.Height);
        public string Group { get; }
        public int Damage { get => damageComponent.Damage; set => damageComponent.Damage = value; }

        Action<int> onCollision;

        public event Action<int> OnCollisiton { add => onCollision += value; remove => onCollision += value; }

        public CollisionComponent(DisplayComponent displayComponent, DamageComponent damageComponent, IntRect collisionRect, string group)
        {
            ComponentStorage<CollisionComponent>.Components.Add(this);

            this.displayComponent = displayComponent;
            rect = collisionRect;

            this.damageComponent = damageComponent;
            
            Group = group;
        }

        public int Attack()
        {
            damageComponent.Attack();
            return Damage;
        }

        public void Collision(int damageComponent)
        {
            onCollision.Invoke(damageComponent);
        }
    }
}
