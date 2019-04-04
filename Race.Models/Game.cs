using System;
using System.Collections.Generic;
using Race.Models.Abstract;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Race.Services;
using Race.Services.Abstract;

namespace Race.Models
{
    public class Game
    {
        public List<Car> Cars { get; set; }
        public bool _gameStatus;
        static object _locker = new object();
        public IReporter _reporter;

        public Game(IReporter reporter)
        {
            Cars = new List<Car>();
            _gameStatus = false;
            _reporter = reporter;
        }

        public void StartGame()
        {
            int idCar = 1;
            foreach (var car in Cars)
            {
                car.Id = idCar++;
                car.FinishEvent += _reporter.SendMessage;
            }

            ShowAllWalls();
            ShowAllCars();

            TimerCallback tm = new TimerCallback(ChangeSpeed);
            Timer timer = new Timer(tm, null, 0, 2000);

            List<Task> tasks = new List<Task>();

            for (int j = 0; j < Cars.Count; j++)
            {
                var car = Cars[j];
                var task = new Task(() => GoCar(ref car, Cars.IndexOf(car)));
                tasks.Add(task);
                task.Start();
            }
            
            _gameStatus = true;

            Task.WaitAny(tasks.ToArray());

            Console.ReadLine();
        }

        private void GoCar(ref Car car, int index)
        {
            while (true)
            {
                if (_gameStatus)
                {
                    car.Drive(ref _gameStatus);

                    if (car.PosOld < car.PosNow)
                    {
                        lock (_locker)
                        {
                            ShowCar(car, index);
                            ShowCarRating();
                            ShowCarsSpeed();
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }
        
        public void ChangeSpeed(object obj)
        {
            Random rnd = new Random();
            foreach (var car in Cars)
            {
                car.SpeedNow = rnd.Next(car.MinSpeed, car.MaxSpeed);
            }
        }

        public void ShowCar(Car car, int indent)
        {
            Console.SetCursorPosition(car.PosOld, indent*2+1);
            Console.WriteLine(" ");
            Console.SetCursorPosition(car.PosNow, indent * 2 + 1);
            Console.WriteLine(car.Id);
        }
        
        private void ShowCarRating()
        {
            Console.SetCursorPosition(0, Cars.Count * 2 + 1);
            Console.Write("Car Position");

            var sortedUsers = from u in Cars
                              orderby u.DriveNow descending
                              select u;
            int index = 1;
            foreach (var car in sortedUsers)
            {
                Console.SetCursorPosition(0, Cars.Count * 2 + 1 + index);
                Console.Write($"                   ");
                Console.SetCursorPosition(0, Cars.Count * 2 + 1 + index);
                Console.Write($"{car.Id}: {car.Name}");
                Console.SetCursorPosition(19, Cars.Count * 2 + 1 + index);
                Console.Write("|");
                index++;
            }
        }

        private void ShowCarsSpeed()
        {
            Console.SetCursorPosition(19, Cars.Count * 2 + 1);
            Console.Write("|Speed");
            int index = 0;
            foreach (var car in Cars)
            {
                Console.SetCursorPosition(20, Cars.Count * 2 + 2 + index);
                index++;
                Console.WriteLine($"{index}: {car.Name} - {car.SpeedNow}    ");
            }
        }

        public void ShowAllCars()
        {
            int indent = 1;

            foreach (var car in Cars)
            {
                Console.SetCursorPosition(Convert.ToInt32(car.DriveNow), indent);
                Console.WriteLine(car.Id);
                indent += 2;
            }
        }

        private void ShowAllWalls()
        {
            int indent = 1;
            ShowWall(0);
            foreach (var car in Cars)
            {
                ShowWall(indent + 1);
                indent += 2;
            }
        }
        
        private static void ShowWall(int top)
        {
            Console.SetCursorPosition(0, top);

            for (int i = 0; i < 100; i++)
            {
                Console.Write("-");
            }
            Console.Write("|");
        }
    }
}
