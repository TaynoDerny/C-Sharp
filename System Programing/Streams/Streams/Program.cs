using System;
using System.Diagnostics;
using System.Threading;

namespace AllTasksMenu
{
    class Program
    {
        static int counter = 0;
        static object locker = new object();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" МЕНЮ ЗАДАНИЙ ");
                Console.WriteLine("1. Задача 1.");
                Console.WriteLine("2. Задача 2.");
                Console.WriteLine("3. Задача 3.");
                Console.WriteLine("4. Задача 4.");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите задание: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Task1()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАЧА 1 \n");

            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadMethod1));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadMethod1));

            t1.Start("Поток А");
            t2.Start("Поток Б");

            t1.Join();
            t2.Join();

            Console.WriteLine("\nВсе потоки завершили работу!");
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void ThreadMethod1(object obj)
        {
            string name = (string)obj;
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"[{name}] Шаг: {i}");
                Thread.Sleep(500);
            }
        }

        static void Task2()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАЧА 2 \n");

            Timer timer = new Timer(TimerCallback2, null, 1000, 2000);

            Thread bgThread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Фоновый поток работает...");
                    Thread.Sleep(300);
                }
            });

            bgThread.IsBackground = true;
            bgThread.Start();

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();

            timer.Dispose();
            Console.WriteLine("\nВозврат в меню...");
        }

        static void TimerCallback2(object state)
        {
            Console.WriteLine($"Таймер: {DateTime.Now}");
        }

        static void Task3()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАЧА 3 \n");

            counter = 0;
            Console.WriteLine("Без блокировки (гонка данных):");
            Thread[] threadsWithoutLock = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                threadsWithoutLock[i] = new Thread(IncrementCounter);
                threadsWithoutLock[i].Start();
            }
            foreach (var t in threadsWithoutLock) t.Join();
            Console.WriteLine($"Итоговое значение: {counter}");

            counter = 0;
            Console.WriteLine("\nС блокировкой (lock):");
            Thread[] threadsWithLock = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                threadsWithLock[i] = new Thread(IncrementCounterLock);
                threadsWithLock[i].Start();
            }
            foreach (var t in threadsWithLock) t.Join();
            Console.WriteLine($"Итоговое значение: {counter}");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void IncrementCounter()
        {
            for (int i = 0; i < 100000; i++)
            {
                counter++;
            }
        }

        static void IncrementCounterLock()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (locker)
                {
                    counter++;
                }
            }
        }

        static void Task4()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАЧА 4 \n");
            Console.WriteLine("Приготовьтесь...");

            Random rnd = new Random();
            int delay = rnd.Next(2000, 5000);

            Thread.Sleep(delay);

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                Console.WriteLine("\nФальстарт! Вы нажали слишком рано.");
                Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ЖМИ!");
            Console.ResetColor();

            Stopwatch sw = Stopwatch.StartNew();

            Thread timeoutThread = new Thread(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("\nВы не успели. Слишком долго.");
                Environment.Exit(0);
            });

            timeoutThread.IsBackground = true;
            timeoutThread.Start();

            Console.ReadKey(true);

            sw.Stop();
            Console.WriteLine($"\nВаше время реакции: {sw.ElapsedMilliseconds} мс");

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}