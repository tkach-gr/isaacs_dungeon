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
    class Hero
    {
        DisplayComponent displayComponent;
        MoveComponent moveComponent;
        HealthComponent healthComponent;
        DamageComponent damageComponent;
        DamageComponent weaponDamageComponent;
        CollisionComponent collisionComponent;
        ThrowingComponent throwingComponent;

        public DisplayComponent HeroDisplayComponent => displayComponent;

        public event Action<int, int> HealthChanged
        {
            add => healthComponent.Changed += value;
            remove => healthComponent.Changed -= value;
        }

        public event Action Died { add => healthComponent.Died += value; remove => healthComponent.Died -= value; }

        public Hero()
        {
            displayComponent = new DisplayComponent(Config.HeroImageName, Config.HeroBasicPosition);

            moveComponent = new MoveComponent(displayComponent, Config.HeroMoveSpeed);
            moveComponent.GetNextPosition_Event += MoveComponent_GetNextPosition_Event;
            moveComponent.CheckMove_Event += MoveComponent_CheckMove_Event;

            healthComponent = new HealthComponent(Config.HeroHealth);
            healthComponent.Died += HealthComponent_Died;

            damageComponent = new DamageComponent(Config.HeroBodyDamage, Config.HeroAttackSpeed);

            weaponDamageComponent = new DamageComponent(Config.HeroWeaponDamage, Config.WeaponAttackSpeed);

            collisionComponent = new CollisionComponent(displayComponent, damageComponent, Config.HeroCollisionRect, "player");
            collisionComponent.OnCollisiton += CollisionComponent_OnCollisiton;
        
            throwingComponent = new ThrowingComponent();
            throwingComponent.CanThrow_Event += ThrowingComponent_CanThrow_Event;
            throwingComponent.OnThrow += ThrowingComponent_OnThrow;
        }

        private void HealthComponent_Died()
        {
            ComponentStorage<Drawable>.Components.Remove(displayComponent);
            ComponentStorage<MoveComponent>.Components.Remove(moveComponent);
            ComponentStorage<HealthComponent>.Components.Remove(healthComponent);
            ComponentStorage<DamageComponent>.Components.Remove(damageComponent);
            ComponentStorage<CollisionComponent>.Components.Remove(collisionComponent);
            ComponentStorage<DamageComponent>.Components.Remove(weaponDamageComponent);
            ComponentStorage<ThrowingComponent>.Components.Remove(throwingComponent);
        }

        private void ThrowingComponent_OnThrow(Vector2f direction)
        {
            Vector2f weaponPosition = new Vector2f();
            weaponPosition.X = collisionComponent.Position.X + collisionComponent.Size.X / 2;
            weaponPosition.Y = collisionComponent.Position.Y + collisionComponent.Size.Y / 2;
            ThrowingWeapon weapon = new ThrowingWeapon(weaponPosition, direction, weaponDamageComponent.Damage, weaponDamageComponent.AttackSpeed);
        }

        private Vector2f ThrowingComponent_CanThrow_Event()
        {
            Vector2f dir = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                dir.Y--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                dir.X--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                dir.X++;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                dir.Y++;

            return dir;
        }

        private void CollisionComponent_OnCollisiton(int enemyDamage)
        {
            healthComponent.Health -= enemyDamage;
        }

        private bool MoveComponent_CheckMove_Event(Vector2f pos)
        {
            IntRect rect = Config.HeroCollisionRect;

            if (pos.X + rect.Left >= Config.RoomRect.Left &&
                pos.Y + rect.Top >= Config.RoomRect.Top &&
                pos.X + rect.Left + rect.Width < Config.RoomRect.Left + Config.RoomRect.Width &&
                pos.Y + rect.Top + rect.Height < Config.RoomRect.Top + Config.RoomRect.Height - 10)
            { return true; }
            else
            { return false; }
        }

        private Vector2f MoveComponent_GetNextPosition_Event()
        {
            Vector2f velocity = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                velocity.Y--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                velocity.X--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                velocity.X++;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                velocity.Y++;

            velocity = moveComponent.GetVelocity(velocity);

            return displayComponent.Position + velocity;
        }
    }
}
