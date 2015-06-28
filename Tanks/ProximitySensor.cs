using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class ProximitySensor : Sensor
    {
        private List<Tank> tanks;

        public const double _weight = 3;
        public const double max_viewAngle = 45;
        public const double max_angularVelocity = Math.PI / 10;
        public const double maxViewDistance = 60;

        private double angleWithBase = 0;
        private double viewAngle;
        private double angularVelocity;

        public ProximitySensor(int id, double viewAngle, double angularVelocity, List<Tank> tanks)
            : base(id, _weight)
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

            Tank closestTank = null;
            double minDist = maxViewDistance;
            foreach (var tnk in tanks)
            {
                double ang = Math.Atan2(tnk.pos.Y - pos.Y, tnk.pos.X - pos.X);
                if (ang1 < ang && ang2 > ang)
                {
                    double dist = Math.Sqrt((tnk.pos.X - pos.X) * (tnk.pos.X - pos.X) + (tnk.pos.Y - pos.Y) * (tnk.pos.Y - pos.Y));
                    if (dist <= minDist)
                    {
                        closestTank = tnk;
                        minDist = dist;
                    }
                }
            }

            if(closestTank != null)
                result += ", closestTank: " + closestTank.id + ", distance: " + minDist;
            else
                result += ", closestTank: null, distance: -1";

            return result;
        }
    }
}
