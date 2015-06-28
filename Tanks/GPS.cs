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

        public GPS(int id) : base(id, _weight) { }

        public override string Run(Tank t)
        {
            string result = "id: " + id + ", x: " + t.pos.X + ", y: " + t.pos.Y;
            return result;
        }
    }
}
