using Race.Models.Abstract;
using Race.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race.Models
{
    public class Bus : Car
    {
        public override event CarEventHandler FinishEvent;
        public override int Id { get; set; }
        public override int MaxSpeed { get; set; }
        public override int MinSpeed { get; set; }
        public override int SpeedNow { get; set; }
        public override double DriveNow { get; set; }
        public override int PosOld { get; set; }
        public override int PosNow { get; set; }
        public override string Name { get; set; }

        public Bus(string name)
        {
            MaxSpeed = 80;
            MinSpeed = 20;
            
            DriveNow = 0;

            PosOld = 0;
            PosNow = 0;
            
            if (name.Length > 16)
            {
                Name = name.Remove(13);
                Name += "...";
            }
            else Name = name;
        }

        public override void Drive(ref bool _gameStatus)
        {
            PosOld = PosNow;
            DriveNow += Convert.ToDouble(SpeedNow) / 80;

            PosNow = Convert.ToInt32(DriveNow);

            if (PosNow >= 100)
            {
                _gameStatus = false;
                Console.Clear();
                FinishEvent($"Автобус \"{Name}\" под номером {Id} одержал победу");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}