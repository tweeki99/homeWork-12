using Race.Models;
using Race.Services;
using Race.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            IReporter reporter = new ConsoleReporter();
            Game game = new Game(reporter);
            
            game.Cars.Add(new Bus("Школьный"));
            game.Cars.Add(new Truck("Силач"));
            game.Cars.Add(new SportCar("Скороход"));
            game.Cars.Add(new PassengerCar("Семьянин"));

            game.StartGame();
        }
    }
}
