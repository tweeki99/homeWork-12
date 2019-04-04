using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race.Models.Abstract
{
    public delegate void CarEventHandler(string message);

    public abstract class Car
    {
        public abstract event CarEventHandler FinishEvent;
        public abstract int MaxSpeed { get; set; }
        public abstract int MinSpeed { get; set; }
        public abstract int SpeedNow { get; set; }
        public abstract double DriveNow { get; set; }
        public abstract int PosOld { get; set; }
        public abstract int PosNow { get; set; }
        public abstract string Name { get; set; }
        public abstract int Id { get; set; }

        public abstract void Drive(ref bool _gameStatus);
    }
}
