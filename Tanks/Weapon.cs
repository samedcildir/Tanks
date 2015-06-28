using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public abstract class Weapon
    {
        protected List<Tank> tanks;
        protected List<Bullet> bullets = new List<Bullet>();
        protected List<Laser> lasers = new List<Laser>();

        public double damage { get; protected set; }
        public int id { get; private set; }
        public double weight { get; private set; }
        public int reloadTime { get; private set; }
        abstract public void Fire(Tank t, int targetID);
        abstract public void DrawBullets();

        private bool isInside(PointF center, double width, double height, PointF p)
        {
            return p.X > center.X - width / 2 && p.X < center.X + width / 2 && p.Y > center.Y - height / 2 && p.Y < center.Y + height / 2;
        }

        public void StepBullets()
        {
            bullets.ForEach(b => b.Step());
            foreach (var b in bullets.ToList())
            {
                foreach (var t in tanks)
                {
                    if (isInside(t.pos, Tank.width, Tank.height, b.pos))
                    {
                        t.Hit(damage);
                        bullets.Remove(b);
                        break;
                    }
                }
            }
        }

        public void StepLaser()
        {
            foreach (var l in lasers.ToList())
            {
                if (l.fireTime + l.fireDelay > DateTime.Now) continue;
                foreach (var t in tanks)
                {
                    // TODO: by using startPoint and orinetation, find endPoint
                    l.startPoint = t.pos;
                }
            }
        }

        protected Weapon(int id, double weight, int reloadTime, List<Tank> tanks)
        {
            this.id = id;
            this.weight = weight;
            this.reloadTime = reloadTime;
            this.tanks = tanks;
        }
    }
    public abstract class Bullet
    {
        public PointF pos;
        protected double speed;
        protected double orientation;

        protected Bullet(double speed, double orientation, PointF pos)
        {
            this.speed = speed;
            this.orientation = orientation;
            this.pos = pos;
        }

        public virtual void Step()
        {
            pos.X += (float)(this.speed * Math.Cos(orientation));
            pos.Y += (float)(this.speed * Math.Cos(orientation));
        }
    }
    public abstract class Laser
    {
        public PointF startPoint;
        public PointF endPoint;
        public double orientation;
        public DateTime fireTime;
        public TimeSpan fireDelay = TimeSpan.FromSeconds(1);

        protected Laser(double orientation)
        {
            this.orientation = orientation;
            this.fireTime = DateTime.Now;
        }
    }
}
