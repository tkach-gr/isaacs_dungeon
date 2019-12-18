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
    class ThrowingWeapon
    {
        DisplayComponent displayComponent;
        MoveComponent moveComponent;
        HealthComponent healthComponent;
        DamageComponent damageComponent;
        CollisionComponent collisionComponent;
        Vector2f direction;

        public ThrowingWeapon(Vector2f position, Vector2f direction, int damage, float attackSpeed)
        {
            displayComponent = new DisplayComponent(Config.HeroWeaponName);
            displayComponent.Position = position;

            moveComponent = new MoveComponent(displayComponent, Config.WeaponSpeed);
            moveComponent.GetNextPosition_Event += MoveComponent_GetNextPosition;
            moveComponent.CheckMove_Event += MoveComponent_CheckMove_Event;

            healthComponent = new HealthComponent(Config.WeaponHealth);
            healthComponent.Died += HealthComponent_Died;

            damageComponent = new DamageComponent(damage, attackSpeed);
            damageComponent.Attacked += DamageComponent_Attacked;

            collisionComponent = new CollisionComponent(displayComponent, damageComponent, Config.WeaponCollisionRect, "player");
            collisionComponent.OnCollisiton += CollisionComponent_OnCollisiton;

            this.direction = direction;
        
            Vector2f origin = displayComponent.Origin;
            origin.X = displayComponent.Size.X / 2;
            origin.Y = displayComponent.Size.Y / 2;
            displayComponent.Origin = origin;
        }

        private void DamageComponent_Attacked()
        {
            RemoveComponents();
        }

        private void HealthComponent_Died()
        {
            RemoveComponents();
        }

        private void RemoveComponents()
        {
            ComponentStorage<Drawable>.Components.Remove(displayComponent);
            ComponentStorage<MoveComponent>.Components.Remove(moveComponent);
            ComponentStorage<HealthComponent>.Components.Remove(healthComponent);
            ComponentStorage<DamageComponent>.Components.Remove(damageComponent);
            ComponentStorage<CollisionComponent>.Components.Remove(collisionComponent);
        }

        private bool MoveComponent_CheckMove_Event(Vector2f pos)
        {
            IntRect rect = Config.WeaponCollisionRect;

            if (pos.X + rect.Left >= Config.RoomRect.Left &&
                pos.Y + rect.Top >= Config.RoomRect.Top &&
                pos.X + rect.Left + rect.Width < Config.RoomRect.Left + Config.RoomRect.Width &&
                pos.Y + rect.Top + rect.Height < Config.RoomRect.Top + Config.RoomRect.Height)
            {
                return true;
            }
            else
            {
                RemoveComponents();
                return false;
            }
        }

        private Vector2f MoveComponent_GetNextPosition()
        {
            displayComponent.Rotation += Config.WeaponRotationSpeed;
            return displayComponent.Position + moveComponent.GetVelocity(direction);
        }

        private void CollisionComponent_OnCollisiton(int enemyDamage)
        {
            healthComponent.Health -= enemyDamage;
        }
    }
}
