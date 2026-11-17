using System;
using System.Collections.Generic;
using System.Threading;

namespace SyncHWMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            bool createdNew;
            using (Mutex mutex = new Mutex(true, "MY_MUTEX", out createdNew))
            {
                if (!createdNew)
                {
                    Console.WriteLine("Приложение уже запущено!");
                    Console.ReadLine();
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("2. Защита счетчика (Mutex)");
                    Console.WriteLine("3. Ограничение потоков (Semaphore)");
                    Console.WriteLine("4. Производитель-Потребитель (Semaphore + lock)");
                    Console.WriteLine("5. Старт группы потоков (ManualResetEvent)");
                    Console.WriteLine("6. Поочередный вывод (AutoResetEvent)");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выберите задание: ");

                    string choice = Console.ReadLine();
                    Console.Clear();

                    switch (choice)
                    {
                        case "2": Task2(); break;
                        case "3": Task3(); break;
                        case "4": Task4(); break;
                        case "5": Task5(); break;
                        case "6": Task6(); break;
                        case "0": return;
                    }

                    Console.WriteLine("\nНажмите Enter для возврата в меню...");
                    Console.ReadLine();
                }
            }
        }

        class MutexCounter { public int count = 0; }

        static void Task2()
        {
            Console.WriteLine(" Задание 2 ");
            MutexCounter counter = new MutexCounter();
            Mutex mutex = new Mutex();
            Thread[] threads = new Thread[5];

            for (int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 1000000; j++)
                    {
                        mutex.WaitOne();
                        counter.count++;
                        mutex.ReleaseMutex();
                    }
                });
                threads[i].Start();
            }

            foreach (var t in threads) t.Join();
            Console.WriteLine($"Итоговое значение: {counter.count} (Должно быть 5000000)");
        }

        static void Task3()
        {
            Console.WriteLine(" Задание 3 ");
            Semaphore sem = new Semaphore(3, 3);
            int completedTasks = 0;
            int totalTasks = 6;

            for (int i = 1; i <= totalTasks; i++)
            {
                int taskId = i;
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    while (true)
                    {
                        if (sem.WaitOne(500))
                        {
                            try
                            {
                                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Задача {taskId} начала работу.");
                                Thread.Sleep(2000);
                            }
                            finally
                            {
                                sem.Release();
                                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Задача {taskId} освободила семафор.");
                                Interlocked.Increment(ref completedTasks);
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Задача {taskId}: таймаут 500мс, ждем дальше...");
                        }
                    }
                });
            }

            while (completedTasks < totalTasks) Thread.Sleep(100);
            Console.WriteLine("Все задачи выполнены.");
        }

        static void Task4()
        {
            Console.WriteLine(" Задание 4 ");
            Queue<int> queue = new Queue<int>();
            Semaphore empty = new Semaphore(5, 5);
            Semaphore full = new Semaphore(0, 5);
            object lockObj = new object();

            Thread producer = new Thread(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    empty.WaitOne();
                    lock (lockObj) { queue.Enqueue(i); }
                    Console.WriteLine($"Производитель добавил: {i}");
                    full.Release();
                    Thread.Sleep(200);
                }
            });

            Thread[] consumers = new Thread[2];
            for (int i = 0; i < 2; i++)
            {
                int cid = i + 1;
                consumers[i] = new Thread(() =>
                {
                    while (true)
                    {
                        full.WaitOne();
                        int item;
                        lock (lockObj) { item = queue.Dequeue(); }
                        Console.WriteLine($"Потребитель {cid} забрал: {item}");
                        empty.Release();
                        Thread.Sleep(400);
                    }
                });
                consumers[i].IsBackground = true;
                consumers[i].Start();
            }

            producer.Start();
            producer.Join();
            Thread.Sleep(2000);
        }

        static void Task5()
        {
            Console.WriteLine(" Задание 5 ");
            ManualResetEvent startEvent = new ManualResetEvent(false);

            for (int i = 1; i <= 10; i++)
            {
                int threadId = i;
                new Thread(() =>
                {
                    startEvent.WaitOne();
                    Console.WriteLine($"Поток {threadId} стартовал!");
                }).Start();
            }

            Console.WriteLine("Главный поток спит 2 секунды...");
            Thread.Sleep(2000);

            Console.WriteLine("Главный поток дает сигнал!");
            startEvent.Set();

            Thread.Sleep(500);
            Console.WriteLine("\nДемонстрация Reset(): новые потоки снова будут ждать.");
            startEvent.Reset();

            new Thread(() =>
            {
                Console.WriteLine("Новый поток ждет сигнала...");
                startEvent.WaitOne();
                Console.WriteLine("Новый поток дождался!");
            }).Start();

            Thread.Sleep(1500);
        }

        static void Task6()
        {
            Console.WriteLine(" Задание 6 ");
            AutoResetEvent event1 = new AutoResetEvent(true);
            AutoResetEvent event2 = new AutoResetEvent(false);

            Thread tOdd = new Thread(() =>
            {
                for (int i = 1; i <= 10; i += 2)
                {
                    event1.WaitOne();
                    Console.WriteLine(i);
                    event2.Set();
                }
            });

            Thread tEven = new Thread(() =>
            {
                for (int i = 2; i <= 10; i += 2)
                {
                    event2.WaitOne();
                    Console.WriteLine(i);
                    event1.Set();
                }
            });

            tOdd.Start();
            tEven.Start();
            tOdd.Join(); tEven.Join();
        }
    }
}