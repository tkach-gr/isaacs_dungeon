using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ECSIsaac
{
    static class Config
    {
        // Global properties
        public static Vector2u WindowSize { get; set; } = new Vector2u(960, 680);
        public static Vector2f TextureScale { get; set; } = new Vector2f(3, 3);
        public static bool ShowCollisionRectangle { get; set; } = false;

        // Room
        public static IntRect RoomRect { get; set; } = new IntRect(new Vector2i(120, 125), new Vector2i(720, 480));

        // Scorebar
        public static Vector2f ScoreBarPosition { get; set; } = new Vector2f(350, 0);
        public static uint ScoreBarCharacterSize { get; set; } = 52;

        // Hero properties
        public static IntRect HeroCollisionRect { get; set; } = new IntRect(new Vector2i(0, 24), new Vector2i(48, 60));
        public static Vector2f HeroBasicPosition { get; set; } = new Vector2f((RoomRect.Left + RoomRect.Width) / 2, (RoomRect.Top + RoomRect.Height) / 2 - 20);
        public static string HeroImageName { get; set; } = "knight_m_idle_anim_f0";
        public static int HeroMoveSpeed { get; set; } = 4;
        public static float HeroAttackSpeed { get; set; } = float.MaxValue;
        public static int HeroHealth { get; set; } = 6;
        public static int HeroBodyDamage { get; set; } = 0;
        public static int HeroWeaponDamage { get; set; } = 5;
        public static float HeroRateOfFire { get; set; } = 4f;
        public static string HeroWeaponName { get; set; } = "weapon_knife";
        public static Vector2f HeroHealthBarPosition { get; set; } = new Vector2f(10, 10);

        // Weapon
        public static int WeaponHealth { get; set; } = 0;
        public static double WeaponSpeed { get; set; } = 6;
        public static float WeaponAttackSpeed { get; set; } = 5;
        public static IntRect WeaponCollisionRect { get; set; } = new IntRect(new Vector2i(-12, -12), new Vector2i(24, 24));
        public static float WeaponRotationSpeed { get; set; } = 0.2f;

        // Entity
        public static IntRect EntityCollisionRect { get; set; } = new IntRect(new Vector2i(24, 27), new Vector2i(48, 72));
        public static string EntityZombieImageName { get; set; } = "big_zombie_idle_anim_f0";
        public static string EntityDemonImageName { get; set; } = "big_demon_idle_anim_f0";
        public static string EntityOgreImageName { get; set; } = "ogre_idle_anim_f0";
        public static float EntityAttackSpeed { get; set; } = 60;
        public static int EntityMoveSpeed { get; set; } = 3;
        public static int EntityHealth { get; set; } = 15;
        public static int EntityDamage { get; set; } = 1;
    }
}
