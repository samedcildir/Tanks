using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class HomingWeapon : Weapon
    {
        private class HomingBullet : Bullet
        {
            private const double followingCoeff = 1.0 / 5;
            private Tank t;

            public HomingBullet(double speed, double orientation, PointF pos, Tank t)
                : base(speed, orientation, pos)
            {
                this.t = t;
            }

            public override void Step()
            {
                double ang = Math.Atan2(t.pos.Y - pos.Y, t.pos.X - pos.X);

                orientation += (ang - orientation) * followingCoeff;

                base.Step();
            }
        }

        public const double _weight = 8;
        public const int _reloadTime = 10000;

        public DateTime lastFireTime { get; private set; }

        public const double speed = 30; // will not be added to tank's speed
        public const double viewAngle = 30; // if targetID is not in this area than it won't be fired.
        public const double maxDistance = 100; // if targetID is not in this area than it won't be fired.
        public const double homing_bullet_damage = 60;

        public HomingWeapon(int id, List<Tank> tanks)
            : base(id, _weight, _reloadTime, tanks)
        {
            damage = homing_bullet_damage;
            lastFireTime = DateTime.MinValue;
        }

        // Since its a smart weapon it won't ignore target ID
        public override void Fire(Tank t, int targetID)
        {
            if ((DateTime.Now - lastFireTime).Milliseconds > reloadTime)
            {
                Tank tnk = tanks.First(_t => _t.id == targetID);

                PointF pos = t.pos;
                double ang1 = t.orientation - viewAngle / 2;
                double ang2 = t.orientation + viewAngle / 2;
                double ang = Math.Atan2(tnk.pos.Y - pos.Y, tnk.pos.X - pos.X);
                double dist = Math.Sqrt((tnk.pos.X - pos.X) * (tnk.pos.X - pos.X) + (tnk.pos.Y - pos.Y) * (tnk.pos.Y - pos.Y));
                if (ang1 < ang && ang2 > ang && dist <= maxDistance)
                {
                    bullets.Add(new HomingBullet(speed, t.orientation, t.pos, t));
                    lastFireTime = DateTime.Now;
                }
            }
        }

        public override void DrawBullets()
        {
        }
    }
}
