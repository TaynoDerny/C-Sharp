using System;

namespace HomeworkLesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выбирете дейтвие :");
                Console.WriteLine("1. Сжать массив (удаление нулей)");
                Console.WriteLine("2. Сумма элементов между Min и Max в массиве 5х5");
                Console.WriteLine("3. Проверка на палиндром");
                Console.WriteLine("4. Подсчет количества слов в предложении");
                Console.WriteLine("5. Сложение и умножение матриц 5х5");
                Console.WriteLine("6. Поиск порядковых номеров символа в строке");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите номер задания: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    case "5": Task5(); break;
                    case "6": Task6(); break;
                    case "0": return;
                    default: 
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу..."); 
                        Console.ReadKey(); 
                        break;
                }
            }
        }

        // Задание 1
        static void Task1()
        {
            Console.WriteLine("\n--- Задание 1: Сжатие массива ---");
            int[] array = { 1, 0, 5, 0, 3, 0, 8, 9, 0, 2 };
            int[] result = new int[array.Length];
            int count = 0;

            Console.WriteLine("Исходный массив: " + ArrayToString(array));

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    result[count] = array[i];
                    count++;
                }
            }

            for (int i = count; i < result.Length; i++)
            {
                result[i] = -1;
            }

            Console.WriteLine("Результат:       " + ArrayToString(result));
            Wait();
        }

        // Задание 2
        static void Task2()
        {
            Console.WriteLine("\n--- Задание 2: Сумма между Min и Max (5x5) ---");
            int[,] matrix = new int[5, 5];
            Random rnd = new Random();
            int min = 100, max = -100;
            int minIdx = 0, maxIdx = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = rnd.Next(-10, 11);
                    Console.Write($"{matrix[i, j], 4}");

                    int currentLinearIdx = i * 5 + j;
                    if (matrix[i, j] < min) { min = matrix[i, j]; minIdx = currentLinearIdx; }
                    if (matrix[i, j] > max) { max = matrix[i, j]; maxIdx = currentLinearIdx; }
                }
                Console.WriteLine();
            }

            int start = (minIdx < maxIdx) ? minIdx : maxIdx;
            int end = (minIdx > maxIdx) ? minIdx : maxIdx;
            int sum = 0;

            for (int k = start + 1; k < end; k++)
            {
                sum += matrix[k / 5, k % 5];
            }

            Console.WriteLine($"\nMin: {min}, Max: {max}");
            Console.WriteLine($"Сумма элементов строго между ними: {sum}");
            Wait();
        }

        // Задание 3
        static void Task3()
        {
            Console.Write("\nВведите строку: ");
            string input = Console.ReadLine().Replace(" ", "").ToLower();
            bool isPalindrome = true;

            for (int i = 0; i < input.Length / 2; i++)
            {
                if (input[i] != input[input.Length - 1 - i])
                {
                    isPalindrome = false;
                    break;
                }
            }

            if (isPalindrome && input.Length > 0)
                Console.WriteLine("Это палиндром!");
            else
                Console.WriteLine("Это не палиндром.");
            Wait();
        }

        // Задание 4
        static void Task4()
        {
            Console.Write("\nВведите предложение: ");
            string input = Console.ReadLine();
            char[] separators = { ' ', '.', '!', '?', ',', ';' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            
            Console.WriteLine($"Количество слов: {words.Length}");
            Wait();
        }

        // Задание 5
        static void Task5()
        {
            int size = 5;
            int[,] A = new int[size, size];
            int[,] B = new int[size, size];
            int[,] sum = new int[size, size];
            int[,] mult = new int[size, size];
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++) {
                    A[i, j] = rnd.Next(1, 6);
                    B[i, j] = rnd.Next(1, 6);
                }

            Console.WriteLine("\nМатрица A:"); PrintMatrix(A);
            Console.WriteLine("\nМатрица B:"); PrintMatrix(B);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++) {
                    sum[i, j] = A[i, j] + B[i, j];
                    for (int k = 0; k < size; k++)
                        mult[i, j] += A[i, k] * B[k, j];
                }

            Console.WriteLine("\nСумма:"); PrintMatrix(sum);
            Console.WriteLine("\nПроизведение:"); PrintMatrix(mult);
            Wait();
        }

        // Задание 6
        static void Task6()
        {
            Console.Write("\nВведите строку: ");
            string str = Console.ReadLine();
            Console.Write("Введите искомый символ: ");
            char target = Console.ReadKey().KeyChar;
            Console.WriteLine("\nРезультат:");

            bool found = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == target)
                {
                    Console.Write(i + " ");
                    found = true;
                }
            }

            if (!found) Console.Write("Символ не найден.");
            Console.WriteLine();
            Wait();
        }

        static void PrintMatrix(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++) {
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write($"{m[i, j], 5}");
                Console.WriteLine();
            }
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
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}