using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public abstract class Sensor
    {
        public int id { get; private set; }
        public double weight { get; private set; }
        abstract public string Run(Tank t);

        protected Sensor(int id, double weight)
        {
            this.id = id;
            this.weight = weight;
        }
    }
}
