using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using ECSIsaac.Components;

namespace ECSIsaac.Systems
{
    class CollisionSystem : BasicSystem
    {
        RenderTarget renderTarget;

        public CollisionSystem(RenderTarget renderTarget)
        {
            this.renderTarget = renderTarget;
        }

        public override void Process()
        {
            List<CollisionComponent> components = ComponentStorage<CollisionComponent>.Components;
            for (int y = 0; y < components.Count; y++)
            {
                if(Config.ShowCollisionRectangle)
                    DrawCollisionRect(components[y]);

                CollisionComponent first = components[y];

                for (int i = 0; i < components.Count; i++)
                {
                    CollisionComponent second = components[i];

                    if (first != second && first.Group != second.Group)
                    {
                        if(CheckCollision(first, second) == true)
                        {
                            int firstDamage = 0;
                            bool canFirstAttack = first.CanAttack;
                            if (canFirstAttack)
                                firstDamage = first.Attack();

                            if(second.CanAttack)
                                first.Collision(second.Attack());

                            if(canFirstAttack)
                                second.Collision(firstDamage);
                        }
                    }
                }
            }
        }

        private void DrawCollisionRect(CollisionComponent component)
        {
            RectangleShape rect = new RectangleShape(new Vector2f(component.Size.X, component.Size.Y));
            rect.Position = new Vector2f(component.Position.X, component.Position.Y);
            rect.FillColor = Color.Transparent;
            rect.OutlineThickness = 1;
            rect.OutlineColor = Color.White;
            renderTarget.Draw(rect);
        }

        private static bool CheckCollision(CollisionComponent first, CollisionComponent second)
        {
            if (first.Size.X < second.Size.X)
                Swap(first, second);

            if (second.Position.X >= first.Position.X && second.Position.X < first.Position.X + first.Size.X)
            {
                if (first.Size.Y < second.Size.Y)
                    Swap(first, second);

                if (second.Position.Y >= first.Position.Y && second.Position.Y < first.Position.Y + first.Size.Y)
                    return true;
                if (second.Position.Y + second.Size.Y >= first.Position.Y && second.Position.Y + second.Size.Y < first.Position.Y + first.Size.Y)
                    return true;
            }
            else if(second.Position.X + second.Size.X >= first.Position.X && second.Position.X + second.Size.X < first.Position.X + first.Size.X)
            {
                if (first.Size.Y < second.Size.Y)
                    Swap(first, second);

                if (second.Position.Y >= first.Position.Y && second.Position.Y < first.Position.Y + first.Size.Y)
                    return true;
                if (second.Position.Y + second.Size.Y >= first.Position.Y && second.Position.Y + second.Size.Y < first.Position.Y + first.Size.Y)
                    return true;
            }

            return false;
        }

        private static void Swap(CollisionComponent first, CollisionComponent second)
        {
            CollisionComponent temp = first;
            first = second;
            second = temp;
        }
    }
}
