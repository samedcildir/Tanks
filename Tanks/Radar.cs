using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: decide wether to add angle of seen tank in result
// don't add because: if we add that than everyone will use maxViewAngle but we can make viewAngle constant anyway
// add because: thats what radars do :)

namespace Tanks
{
    public class Radar : Sensor
    {
        private List<Tank> tanks;

        public const double _weight = 5;
        public const double max_viewAngle = 45;
        public const double max_angularVelocity = Math.PI / 10;
        public const int maxViewDistance = 100;

        private double angleWithBase = 0;
        private double viewAngle;
        private double angularVelocity;

        public Radar(int id, double viewAngle, double angularVelocity, List<Tank> tanks, Color color)
            : base(id, _weight, color)
        {
            this.viewAngle = viewAngle > max_viewAngle ? max_viewAngle : viewAngle;
            this.angularVelocity = angularVelocity > max_angularVelocity ? max_angularVelocity : angularVelocity;
            this.tanks = tanks;
        }

        public override string Run(Tank t)
        {
            angleWithBase += angularVelocity;

            string result = "id: " + id;
            result += ", angleWithBase: " + angleWithBase;

            PointF pos = t.pos;
            double ang1 = t.orientation + angleWithBase - viewAngle / 2;
            double ang2 = t.orientation + angleWithBase + viewAngle / 2;

            List<KeyValuePair<Tank, double>> tanksInRegion = new List<KeyValuePair<Tank, double>>();
            foreach (var tnk in tanks)
            {
                double ang = Math.Atan2(tnk.pos.Y - pos.Y, tnk.pos.X - pos.X);
                if (ang1 < ang && ang2 > ang)
                {
                    double dist = Math.Sqrt((tnk.pos.X - pos.X) * (tnk.pos.X - pos.X) + (tnk.pos.Y - pos.Y) * (tnk.pos.Y - pos.Y));
                    if (dist > maxViewDistance) continue;
                    tanksInRegion.Add(new KeyValuePair<Tank, double>(tnk, dist));
                }
            }

            result += ", count: " + tanksInRegion.Count;
            tanksInRegion.ForEach(pair => result += ", id: " + pair.Key.id + ", distance: " + pair.Value);

            return result;
        }

        private const int drawAlpha = 80;
        public override void Draw(Graphics g, Tank t)
        {
            double ang1 = t.orientation + angleWithBase - viewAngle / 2;
            double ang2 = t.orientation + angleWithBase + viewAngle / 2;
            g.FillPie(new SolidBrush(Color.FromArgb(drawAlpha, color))
                , (int)t.pos.X - maxViewDistance, (int)t.pos.Y - maxViewDistance
                , 2 * maxViewDistance, 2 * maxViewDistance, (int)ang1, (int)ang2);
        }
    }
}
