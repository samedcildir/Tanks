using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Color color { get; set; }

        abstract public void Draw(Graphics g, Tank t);

        protected Sensor(int id, double weight, Color color)
        {
            this.id = id;
            this.weight = weight;
            this.color = color;
        }
    }
}
