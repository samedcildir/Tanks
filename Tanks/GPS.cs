using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class GPS : Sensor
    {
        public const double _weight = 2;

        public GPS(int id, Color color) : base(id, _weight, color) { }

        public override string Run(Tank t)
        {
            string result = "id: " + id + ", x: " + t.pos.X + ", y: " + t.pos.Y;
            return result;
        }

        private int drawTime = 0;
        private const int drawTimeMax = 10;
        private const int drawRMax = 10;
        private const int drawAlpha = 80;
        public override void Draw(Graphics g, Tank t)
        {
            int r = (int)((double)drawRMax * drawTime / drawTimeMax);

            g.FillEllipse(new SolidBrush(Color.FromArgb(drawAlpha, color)), t.pos.X - r, t.pos.Y - r, 2 * r, 2 * r);
        }
    }
}
