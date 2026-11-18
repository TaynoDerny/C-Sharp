using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelProgrammingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ЗАДАНИЕ 1: Три способа запуска Task ");
            RunTask1();

            Console.WriteLine("\nНажмите любую клавишу для перехода к Заданию 2...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine(" ЗАДАНИЕ 2: Простые числа от 0 до 1000 ");
            RunTask2();

            Console.WriteLine("\nНажмите любую клавишу для перехода к Заданию 3...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine(" ЗАДАНИЕ 3: Простые числа с границами и подсчетом ");
            RunTask3(0, 1000);

            Console.WriteLine("\nНажмите любую клавишу для перехода к Заданию 4...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine(" ЗАДАНИЕ 4: Массив Task (Min, Max, Avg, Sum) ");
            RunTask4();

            Console.WriteLine("\nНажмите любую клавишу для перехода к Заданию 5...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine(" ЗАДАНИЕ 5: Continuation Tasks ");
            RunTask5();

            Console.WriteLine("\nВсе задания выполнены. Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        //  ЗАДАНИЕ 1 
        static void RunTask1()
        {
            Action printDateTime = () =>
            {
                Console.WriteLine($"[Поток {Task.CurrentId}] Текущая дата и время: {DateTime.Now}");
            };

            Console.WriteLine("1. Через метод Start()");
            Task task1 = new Task(printDateTime);
            task1.Start();
            task1.Wait();

            Console.WriteLine("2. Через метод Task.Factory.StartNew()");
            Task task2 = Task.Factory.StartNew(printDateTime);
            task2.Wait();

            Console.WriteLine("3. Через метод Task.Run()");
            Task task3 = Task.Run(printDateTime);
            task3.Wait();
        }

        //  ЗАДАНИЕ 2 
        static void RunTask2()
        {
            Task task = Task.Run(() =>
            {
                Console.WriteLine("Простые числа:");
                for (int i = 0; i <= 1000; i++)
                {
                    if (IsPrime(i))
                    {
                        Console.Write(i + " ");
                    }
                }
                Console.WriteLine();
            });

            Console.WriteLine("Основной поток ожидает завершения задачи...");
            task.Wait();
            Console.WriteLine("Задача завершена.");
        }

        //  ЗАДАНИЕ 3 
        static void RunTask3(int start, int end)
        {
            Task<int> task = Task.Run(() =>
            {
                int count = 0;
                Console.WriteLine($"Поиск в диапазоне от {start} до {end}:");
                for (int i = start; i <= end; i++)
                {
                    if (IsPrime(i))
                    {
                        Console.Write(i + " ");
                        count++;
                    }
                }
                Console.WriteLine();
                return count;
            });

            Console.WriteLine("Основной поток ожидает...");
            int totalCount = task.Result;

            Console.WriteLine($"Задача завершена. Количество простых чисел: {totalCount}");
        }

        //  ЗАДАНИЕ 4 
        static void RunTask4()
        {
            int[] array = { 5, 2, 9, 1, 7, 4, 8, 3, 6, 10, 15, -5, 0 };
            Console.WriteLine("Исходный массив: " + string.Join(", ", array));

            Task[] tasks = new Task[4];

            tasks[0] = Task.Run(() => Console.WriteLine($"Минимум: {array.Min()}"));
            tasks[1] = Task.Run(() => Console.WriteLine($"Максимум: {array.Max()}"));
            tasks[2] = Task.Run(() => Console.WriteLine($"Среднее: {array.Average()}"));
            tasks[3] = Task.Run(() => Console.WriteLine($"Сумма: {array.Sum()}"));

            Task.WaitAll(tasks);

            Console.WriteLine("Все вычисления завершены.");
        }

        //  ЗАДАНИЕ 5 
        static void RunTask5()
        {
            int[] initialArray = { 5, 1, 9, 1, 5, 7, 3, 9, 2, 8, 4 };
            int searchValue = 7;
            Console.WriteLine("Исходный массив: " + string.Join(", ", initialArray));

            Task<int[]> task1 = Task.Run(() =>
            {
                Console.WriteLine("[Task 1] Удаление дублей...");
                return initialArray.Distinct().ToArray();
            });

            Task<int[]> task2 = task1.ContinueWith(prevTask =>
            {
                int[] distinctArray = prevTask.Result;
                Console.WriteLine("[Task 2] Сортировка массива...");
                Array.Sort(distinctArray);
                return distinctArray;
            });

            Task task3 = task2.ContinueWith(prevTask =>
            {
                int[] sortedArray = prevTask.Result;
                Console.WriteLine("[Task 3] Отсортированный массив: " + string.Join(", ", sortedArray));

                int index = Array.BinarySearch(sortedArray, searchValue);

                if (index >= 0)
                    Console.WriteLine($"[Task 3] Значение {searchValue} найдено на индексе {index}.");
                else
                    Console.WriteLine($"[Task 3] Значение {searchValue} не найдено.");
            });

            task3.Wait();
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}