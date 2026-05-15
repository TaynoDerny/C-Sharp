using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберитите действие:");
                Console.WriteLine("1. Сжать массив (удаление нулей)");
                Console.WriteLine("2. Сначала отрицательные, потом положительные");
                Console.WriteLine("3. Подсчет вхождений числа");
                Console.WriteLine("4. Поменять местами столбцы в матрице M x N");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите номер задания: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    case "0": return;
                    default: Console.WriteLine("Ошибка! Нажмите любую клавишу..."); Console.ReadKey(); break;
                }
            }
        }

        // Задание 1
        static void Task1()
        {
            int[] arr = { 1, 0, 4, 0, 0, 5, 8, 0, 2 };
            Console.WriteLine("\nИсходный массив: " + ArrayToString(arr));
            
            int[] result = new int[arr.Length];
            int index = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                    result[index++] = arr[i];
            }

            for (int i = index; i < result.Length; i++)
            {
                result[i] = -1;
            }

            Console.WriteLine("Сжатый массив:   " + ArrayToString(result));
            Wait();
        }

        // Задание 2
        static void Task2()
        {
            int[] arr = { 5, -2, 0, -8, 3, -1, 0, 7 };
            Console.WriteLine("\nИсходный массив: " + ArrayToString(arr));
            
            int[] result = new int[arr.Length];
            int pos = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0) result[pos++] = arr[i];
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 0) result[pos++] = arr[i];
            }

            Console.WriteLine("Результат:       " + ArrayToString(result));
            Wait();
        }

        // Задание 5
        static void Task3()
        {
            int[] arr = { 1, 5, 3, 5, 2, 5, 8, 1 };
            Console.WriteLine("\nМассив: " + ArrayToString(arr));
            Console.Write("Введите число для поиска: ");
            
            if (int.TryParse(Console.ReadLine(), out int target))
            {
                int count = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == target) count++;
                }
                Console.WriteLine($"Число {target} встречается {count} раз(а).");
            }
            else Console.WriteLine("Ошибка ввода числа.");
            
            Wait();
        }

        // Задание 4
        static void Task4()
        {
            int M = 4, N = 5; // Порядок M на N
            int[,] matrix = new int[M, N];
            Random rnd = new Random();

            Console.WriteLine($"\nИсходная матрица {M}x{N}:");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rnd.Next(10, 99);
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.Write("\nВведите номер первого столбца (0-4): ");
            int col1 = int.Parse(Console.ReadLine());
            Console.Write("Введите номер второго столбца (0-4): ");
            int col2 = int.Parse(Console.ReadLine());

            if (col1 >= 0 && col1 < N && col2 >= 0 && col2 < N)
            {
                for (int i = 0; i < M; i++)
                {
                    int temp = matrix[i, col1];
                    matrix[i, col1] = matrix[i, col2];
                    matrix[i, col2] = temp;
                }

                Console.WriteLine("\nМатрица после перестановки:");
                for (int i = 0; i < M; i++)
                {
                    for (int j = 0; j < N; j++)
                        Console.Write(matrix[i, j] + " ");
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("Неверные индексы столбцов!");

            Wait();
        }

        static string ArrayToString(int[] arr)
        {
            string s = "";
            for (int i = 0; i < arr.Length; i++)
                s += arr[i] + (i == arr.Length - 1 ? "" : ", ");
            return s;
        }

        static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}