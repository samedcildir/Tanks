using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public abstract class Weapon
    {
        protected List<Tank> tanks;
        protected List<Bullet> bullets = new List<Bullet>();
        public Color color { get; set; }

        public double damage { get; protected set; }
        public int id { get; private set; }
        public double weight { get; private set; }
        public int reloadTime { get; private set; }

        abstract public void Fire(Tank t, int targetID);
        abstract public void Draw(Graphics g, Tank t);

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

        protected Weapon(int id, double weight, int reloadTime, List<Tank> tanks, Color color)
        {
            this.id = id;
            this.weight = weight;
            this.reloadTime = reloadTime;
            this.tanks = tanks;
            this.color = color;
        }
    }
    public abstract class Bullet
    {
        private const int width = 2;
        private const int height = 10;

        public Color color;
        public PointF pos;
        protected double speed;
        protected double orientation; // in degrees

        public virtual void Draw(Graphics g, Tank t)
        {
            // TODO: Fill here

            Rectangle rect = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);

            Matrix m = new Matrix();
            m.RotateAt((float)orientation, pos);

            g.FillRectangle(new SolidBrush(color), rect);
            g.ResetTransform();
        }

        protected Bullet(double speed, double orientation, PointF pos, Color color)
        {
            this.speed = speed;
            this.orientation = orientation;
            this.pos = pos;
            this.color = color;
        }

        public virtual void Step()
        {
            pos.X += (float)(this.speed * Math.Cos(orientation));
            pos.Y += (float)(this.speed * Math.Cos(orientation));
        }
    }
}
