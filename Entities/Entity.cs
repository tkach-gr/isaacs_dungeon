using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ECSIsaac.Components;

namespace ECSIsaac.Entities
{
    class Entity
    {
        DisplayComponent displayComponent;
        HealthComponent healthComponent;
        DamageComponent damageComponent;
        CollisionComponent collisionComponent;
        MoveComponent moveComponent;
        DilayedStartComponent dilayedStartComponent;
        DisplayComponent heroDisplayComponent;

        Action died;

        public event Action Died { add => died += value; remove => died -= value; }

        public Entity(DisplayComponent heroDisplayComponent, Vector2f position)
        {
            this.heroDisplayComponent = heroDisplayComponent;

            Random rand = new Random((int)(GameTimer.Elapsed * 1000));
            switch (rand.Next(3))
            {
                case 0:
                    displayComponent = new DisplayComponent(Config.EntityZombieImageName, position);
                    break;
                case 1:
                    displayComponent = new DisplayComponent(Config.EntityDemonImageName, position);
                    break;
                case 2:
                    displayComponent = new DisplayComponent(Config.EntityOgreImageName, position);
                    break;
            }

            dilayedStartComponent = new DilayedStartComponent(1000);
            dilayedStartComponent.SomeAction_Event += DilayedStartComponent_SomeAction_Event;
        }

        private void DilayedStartComponent_SomeAction_Event()
        {
            healthComponent = new HealthComponent(Config.EntityHealth);
            healthComponent.Died += HealthComponent_Died;

            damageComponent = new DamageComponent(Config.EntityDamage, Config.EntityAttackSpeed);

            collisionComponent = new CollisionComponent(displayComponent, damageComponent, Config.EntityCollisionRect, "entity");
            collisionComponent.OnCollisiton += CollisionComponent_OnCollisiton;

            moveComponent = new MoveComponent(displayComponent, Config.EntityMoveSpeed);
            moveComponent.GetNextPosition_Event += MoveComponent_GetNextPosition_Event;
        }

        private Vector2f MoveComponent_GetNextPosition_Event()
        {
            Vector2f nextPosition = heroDisplayComponent.Position - displayComponent.Position;
            float divider;

            if (Math.Abs(nextPosition.X) > Math.Abs(nextPosition.Y))
                divider = Math.Abs(nextPosition.X);
            else
                divider = Math.Abs(nextPosition.Y);

            if (nextPosition.X != 0)
                nextPosition.X /= divider;

            if (nextPosition.Y != 0)
                nextPosition.Y /= divider;

            nextPosition = moveComponent.GetVelocity(nextPosition);

            return displayComponent.Position + nextPosition;
        }

        private void HealthComponent_Died()
        {
            RemoveComponents();
            died?.Invoke();
        }

        public void RemoveComponents()
        {
            ComponentStorage<Drawable>.Components.Remove(displayComponent);
            ComponentStorage<HealthComponent>.Components.Remove(healthComponent);
            ComponentStorage<DamageComponent>.Components.Remove(damageComponent);
            ComponentStorage<CollisionComponent>.Components.Remove(collisionComponent);
            ComponentStorage<MoveComponent>.Components.Remove(moveComponent);
            ComponentStorage<DilayedStartComponent>.Components.Remove(dilayedStartComponent);
        }

        private void CollisionComponent_OnCollisiton(int enemyDamage)
        {
            healthComponent.Health -= enemyDamage;
        }
    }
}
