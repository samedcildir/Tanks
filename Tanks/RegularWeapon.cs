using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class RegularWeapon : Weapon
    {
        private class RegularBullet : Bullet
        {
            public RegularBullet(double speed, double orientation, PointF pos) : base(speed, orientation, pos) { }

            public override void Step()
            {
                base.Step();
            }
        }

        public const double _weight = 5;
        public const int _reloadTime = 1500;

        public DateTime lastFireTime { get; private set; }

        public const double speed = 30; // will be added to tank's speed
        public const double regular_bullet_damage = 35;

        public RegularWeapon(int id, List<Tank> tanks)
            : base(id, _weight, _reloadTime, tanks)
        {
            damage = regular_bullet_damage;
            lastFireTime = DateTime.MinValue;
        }

        // Since its only a regular weapon it ignores target ID
        public override void Fire(Tank t, int targetID)
        {
            if ((DateTime.Now - lastFireTime).Milliseconds > reloadTime)
            {
                bullets.Add(new RegularBullet(speed + t.currentSpeed, t.orientation, t.pos));
                lastFireTime = DateTime.Now;
            }
        }

        private bool isInside(PointF center, double width, double height, PointF p)
        {
            return p.X > center.X - width / 2 && p.X < center.X + width / 2 && p.Y > center.Y - height / 2 && p.Y < center.Y + height / 2;
        }
        
        public override void DrawBullets()
        {
        }
    }
}
