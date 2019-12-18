using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ECSIsaac.Entities;
using ECSIsaac.Systems;

namespace ECSIsaac
{
    public delegate T Returning<T>();

    class Game
    {
        List<BasicSystem> systems;
        List<Entity> entities;
        RenderWindow window;
        HealthBar heroHealthBar;
        ScoreBar scoreBar;
        Floor floor;
        Walls walls;
        Hero hero;
        int wave;

        public Game()
        {
            systems = new List<BasicSystem>();
            ImageManager.Load("frames");

            window = new RenderWindow(new VideoMode(Config.WindowSize.X, Config.WindowSize.Y), "Isaac", Styles.Close);
            InitSystems(window);

            floor = new Floor();
            walls = new Walls();

            entities = new List<Entity>();
        }

        public void Run()
        {
            GameTimer.Reset();

            scoreBar = new ScoreBar();

            hero = new Hero();
            hero.Died += Hero_Died;
            hero.HealthChanged += Hero_HealthChanged;
            heroHealthBar = new HealthBar(Config.HeroHealth, Config.HeroHealthBarPosition);
            
            wave = 1;

            GameTimer.Start();

            window.Closed += Window_Closed;

            while (window.IsOpen)
            {
                window.Clear(new Color(36, 36, 36));

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    window.Close();

                window.DispatchEvents();

                if (GameTimer.Elapsed / 4000 > wave)
                {
                    GenerateEntity();
                    wave++;
                }

                ProcessSystems();
                window.Display();
                GameTimer.NextFrame();
            }
        }

        private void Hero_Died()
        {
            while(entities.Count != 0)
            {
                entities[0].RemoveComponents();
                entities.Remove(entities[0]);
            }

            scoreBar.RemoveComponents();
            Run();
        }

        private void Entity_Died()
        {
            scoreBar.Score += 100;
        }

        private void Hero_HealthChanged(int currentHealth, int difference)
        {
            heroHealthBar.UpdateHealth(currentHealth);
        }

        private void GenerateEntity()
        {
            Random rand = new Random((int)(GameTimer.Elapsed * 1000));
            int randNum = rand.Next(20);
            int entityCount = 1;
            if (randNum > 18)
                entityCount = 3;
            else if (randNum > 10)
                entityCount = 2;

            for(int i = 0; i < entityCount; i++)
            {
                Vector2f position = new Vector2f()
                {
                    X = rand.Next(Config.RoomRect.Left, Config.RoomRect.Left + Config.RoomRect.Width - 100),
                    Y = rand.Next(Config.RoomRect.Top, Config.RoomRect.Top + Config.RoomRect.Height - 100),
                };
                entities.Add(new Entity(hero.HeroDisplayComponent, position));
                entities.Last().Died += Entity_Died;
            }
        }

        private void InitSystems(RenderTarget target)
        {
            DisplaySystem displaySystem = new DisplaySystem(target);
            systems.Add(displaySystem);

            MoveSystem moveSystem = new MoveSystem();
            systems.Add(moveSystem);

            CollisionSystem collisionSystem = new CollisionSystem(target);
            systems.Add(collisionSystem);

            ThrowingSystem throwingSystem = new ThrowingSystem();
            systems.Add(throwingSystem);

            DilayedStartSystem dilayedStartSystem = new DilayedStartSystem();
            systems.Add(dilayedStartSystem);
        }

        private void ProcessSystems()
        {
            foreach (BasicSystem item in systems)
            {
                item.Process();
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            RenderWindow window = sender as RenderWindow;
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
