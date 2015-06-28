using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public class Tank
    {
        public const double width = 10;
        public const double height = 15;

        public const double max_weight = 25; // after that speed will be smaller than 1 so actually its not max :)
        //also acc will be smaller than 0.1

        public const double ultimate_max_speed = 10;
        private double max_speed = ultimate_max_speed;
        public double currentSpeed;
        public double targetSpeed;

        public const double ultimate_max_acc = 2;
        private double max_acc = ultimate_max_acc;
        public double currentAcc;
        public double targetAcc;

        public double health { get; private set; }
        public int id { get; private set; }
        public Color color { get; set; }
        public PointF pos { get; private set; }
        public double orientation { get; private set; }
        private double ___weight;
        public double weight
        {
            get { return ___weight; }
            set
            {
                ___weight = value;
                // if value = 0 -> ultimate_max_speed
                // if value = max_weight -> 1
                // otherwise something like 1/x
                max_speed = ultimate_max_speed / (value / max_weight * (ultimate_max_speed - 1) + 1);
                max_acc = ultimate_max_acc / (value / max_weight * (10 * ultimate_max_acc - 1) + 1);
            }
        }

        public Tank(PointF pos, Color color, int id, double orientation = 0.0)
        {
            weight = 0;
            health = 100;
            currentSpeed = 0;
            targetSpeed = 0;
            currentAcc = 0;

            this.pos = pos;
            this.color = color;
            this.id = id;
            this.orientation = orientation;
        }

        #region Sensor
        private List<Sensor> sensors = new List<Sensor>();

        public void AddSensor(Sensor s)
        {
            sensors.Add(s);
            weight += s.weight;
        }

        // Returns the results that came from sensors
        private List<string> RunSensors()
        {
            List<string> results = new List<string>();
            sensors.ForEach(s => results.Add(s.Run(this)));
            return results;
        }
        #endregion

        #region Weapon
        private List<Weapon> weapons = new List<Weapon>();

        public void AddWeapon(Weapon w)
        {
            weapons.Add(w);
            weight += w.weight;
        }

        private void FireWeapon(int weaponID, int targetID)
        {
            weapons.First(w => w.id == weaponID).Fire(this, targetID);
        }
        #endregion

        public void Hit(double damage)
        {
            health -= damage;
        }

        private void Move()
        {
            // TODO: fix this its not looking good.
            pos = new PointF(pos.X + (float)(currentSpeed * Math.Cos(orientation)), pos.Y + (float)(currentSpeed * Math.Sin(orientation)));
        }

        private void changeSpeed()
        {
            if (targetSpeed > max_speed) targetSpeed = max_speed;
            if (targetAcc > max_acc) targetAcc = max_acc;

            // TODO: Not complete yet
        }

        public class WeaponCommand
        {
            public int weaponID;
            public int targetID;
        }
        public class MoveCommand
        {
            public double targetSpeed = double.NegativeInfinity;
            public double targetAcc = double.NegativeInfinity;

            // TODO: we also should have angulars
        }
        // if the tank's ai cannot send data in time then we call this function without parameters.
        public List<String> StepTank(List<WeaponCommand> weaponCommands = null, MoveCommand moveCommand = null)
        {
            foreach (var w in weapons)
                w.StepBullets();

            if (weaponCommands != null)
                foreach (var v in weaponCommands)
                    FireWeapon(v.weaponID, v.targetID);

            if (moveCommand != null)
            {
                targetSpeed = moveCommand.targetSpeed;
                targetAcc = moveCommand.targetAcc;
            }

            return RunSensors();
        }
    }
}
