using System;
using System.Collections.Generic;
using System.Threading;

namespace AutoRace
{
    // Делегаты
    public delegate void MessageHandler(string message);
    public delegate void FinishHandler(string winnerName);

    // Абстрактный класс автомобиль
    public abstract class Car
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Speed { get; set; }
        
        public event MessageHandler OnMessage;
        public event FinishHandler OnFinish;

        public Car(string name)
        {
            Name = name;
            Position = 0;
            Random rnd = new Random();
            Speed = rnd.Next(10, 30); 
        }

        public abstract void Move(); // Абстрактный метод

        protected void SendMessage(string msg)
        {
            if (OnMessage != null) // Проверка есть ли подписчики
                OnMessage(msg);
        }

        protected void SendFinish()
        {
            if (OnFinish != null)
                OnFinish(Name);
        }
    }

    // Спортивная машина
    public class SportsCar : Car
    {
        public SportsCar() : base("Спортивная машина") { }

        public override void Move()
        {
            Random rnd = new Random();
            Speed += rnd.Next(-5, 10); // Большой разброс скорости
            if (Speed < 1) Speed = 1;
            if (Speed > 50) Speed = 50;

            Position += Speed;
            SendMessage($"{Name} проехала {Position} км. Скорость: {Speed} км/ч");
            
            if (Position >= 100) SendFinish(); // Финиш
        }
    }

    // Легковой автомобиль
    public class PassengerCar : Car
    {
        public PassengerCar() : base("Легковой автомобиль") { }

        public override void Move()
        {
            Random rnd = new Random();
            Speed += rnd.Next(-3, 5);
            if (Speed < 1) Speed = 1;

            Position += Speed;
            SendMessage($"{Name} проехал {Position} км. Скорость: {Speed} км/ч");

            if (Position >= 100) SendFinish();
        }
    }

    // Грузовик
    public class Truck : Car
    {
        public Truck() : base("Грузовик") { }

        public override void Move()
        {
            Random rnd = new Random();
            Speed += rnd.Next(-2, 3);
            if (Speed < 1) Speed = 1;

            Position += Speed;

            if (Position > 100)
            {
                Position = 100; // Фиксируем на финише
            }

            SendMessage($"{Name} проехал {Position} км. Скорость: {Speed} км/ч");
            
            if (Position >= 100) SendFinish();
        }
    }

    // Автобус
    public class Bus : Car
    {
        public Bus() : base("Автобус") { }

        public override void Move()
        {
            Random rnd = new Random();
            Speed += rnd.Next(-4, 6);

            Position += Speed;
            SendMessage($"{Name} проехал {Position} км. Скорость: {Speed} км/ч");

            if (Position >= 100) SendFinish();
        }
    }

    // Класс для управления гонкой
    public class Race
    {
        private List<Car> cars = new List<Car>();
        private bool isRaceOver = false;

        public void AddCar(Car car)
        {
            car.OnMessage += Car_OnMessage; // Подписка на событие
            car.OnFinish += Car_OnFinish;
            cars.Add(car);
        }

        private void Car_OnMessage(string message)
        {
            if (!isRaceOver)
                Console.WriteLine(message);
        }

        private void Car_OnFinish(string winnerName)
        {
            if (!isRaceOver)
            {
                isRaceOver = true;
                Console.WriteLine($"!!! ПОБЕДИТЕЛЬ !!!");
                Console.WriteLine($"Машина '{winnerName}' пришла к финишу первой!");
            }
        }

        public void StartRace()
        {
            Console.WriteLine("СТАРТ ГОНКИ!");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();

            while (!isRaceOver)
            {
                Console.WriteLine("\n--- Новый ход ---");
                
                foreach (var car in cars)
                {
                    if (!isRaceOver)
                    {
                        car.Move();
                    }
                }
                
                Thread.Sleep(500); // Задержка
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Race race = new Race();
            race.AddCar(new SportsCar());
            race.AddCar(new PassengerCar());
            race.AddCar(new Truck());
            race.AddCar(new Bus());
            race.StartRace();

            Console.ReadLine();
        }
    }
}