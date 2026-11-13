using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncMylti
{
    class Program
    {
        static int _globalCounter = 0;
        static int ticketsLeft = 100;
        static int fuelLevel = 0;

        static bool isTask1Running = false;
        static bool isTask4Running = false;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Задание 1: Диспетчер задач");
                Console.WriteLine("2. Задание 2: Асинхронный калькулятор");
                Console.WriteLine("3. Задание 3: Касса кинотеатра");
                Console.WriteLine("4. Задание 4: Космический запуск");
                Console.WriteLine("0. Выход");    
                Console.Write("Выберите задание: ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "4":
                        RunTask4();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }

        static void RunTask1()
        {
            Console.WriteLine(" ЗАДАНИЕ 1: ДИСПЕТЧЕР ЗАДАЧ ");

            isTask1Running = true;
            Thread timeThread = new Thread(ShowTime);
            timeThread.IsBackground = true;
            timeThread.Start();

            ThreadPool.QueueUserWorkItem(DoTask, 1);
            ThreadPool.QueueUserWorkItem(DoTask, 2);
            ThreadPool.QueueUserWorkItem(DoTask, 3);

            Thread.Sleep(5000);
            isTask1Running = false;
        }

        static void ShowTime()
        {
            while (isTask1Running)
            {
                Console.WriteLine($"[Фоновый поток] Текущее время: {DateTime.Now:HH:mm:ss}");
                Thread.Sleep(1000);
            }
        }

        static void DoTask(object state)
        {
            int taskId = (int)state;
            Console.WriteLine($"Задача №{taskId} выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Задача №{taskId} ЗАВЕРШЕНА.");
        }

        static void RunTask2()
        {
            Console.WriteLine(" ЗАДАНИЕ 2: АСИНХРОННЫЙ КАЛЬКУЛЯТОР ");

            Func<int, double> mathDelegate = CalculateHeavyMath;

            Console.WriteLine("Главный поток: Запускаем расчет...");

            Task.Run(() => mathDelegate(25))
                .ContinueWith(t => CalculationFinished(t));

            Console.WriteLine("Расчет запущен. Главный поток свободен.");

            Thread.Sleep(3500);
        }

        static double CalculateHeavyMath(int n)
        {
            Thread.Sleep(3000);
            return Math.Sqrt(n);
        }

        static void CalculationFinished(Task<double> task)
        {
            double result = task.Result;
            Console.WriteLine($"\n[Callback] Расчет завершен! Корень из 25 = {result}");
        }

        static void RunTask3()
        {
            Console.WriteLine(" ЗАДАНИЕ 3: КАССА КИНОТЕАТРА ");

            ticketsLeft = 100;
            Console.WriteLine("Касса открыта. Билетов: 100\n");

            Thread[] cashiers = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                cashiers[i] = new Thread(SellTickets);
                cashiers[i].Start(i + 1);
            }

            foreach (var cashier in cashiers)
            {
                cashier.Join();
            }

            Console.WriteLine($"\n Касса закрыта. Итоговый остаток: {ticketsLeft} ");
        }

        static void SellTickets(object cashierId)
        {
            while (true)
            {
                int remaining = Interlocked.Decrement(ref ticketsLeft);

                if (remaining < 0)
                {
                    Interlocked.Increment(ref ticketsLeft);
                    break;
                }

                Console.WriteLine($"Кассир {cashierId} продал билет. Осталось: {remaining}");
                Thread.Sleep(10);
            }
        }

        static void RunTask4()
        {
            Console.WriteLine(" ЗАДАНИЕ 4: КОСМИЧЕСКИЙ ЗАПУСК ");

            fuelLevel = 0;
            isTask4Running = true;

            Thread telemetryThread = new Thread(MonitorTelemetry);
            telemetryThread.IsBackground = true;
            telemetryThread.Start();

            Console.WriteLine("[Система] Запущен расчет траектории...");
            Task.Run(() => CalculateTrajectory(4))
                .ContinueWith(t => Console.WriteLine("\n[Система] Траектория рассчитана! К запуску готов"));

            Console.WriteLine("[Система] Запущены топливные насосы...");
            ThreadPool.QueueUserWorkItem(PumpFuel);

            Console.WriteLine("[Система] Ожидание 5 секунд до старта...\n");
            Thread.Sleep(5500);

            isTask4Running = false;
            int finalFuel = Interlocked.CompareExchange(ref fuelLevel, 0, 0);

            Console.WriteLine($"\n=== ИТОГ: Уровень топлива перед запуском: {finalFuel} ===");
        }

        static void MonitorTelemetry()
        {
            while (isTask4Running)
            {
                Thread.Sleep(1000);
                Console.WriteLine("[Телеметрия] Все системы в норме...");
            }
        }

        static void CalculateTrajectory(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        static void PumpFuel(object state)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Interlocked.Add(ref fuelLevel, 10);
            }
        }
    }
}