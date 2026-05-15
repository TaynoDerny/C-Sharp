using System;

namespace PracticeModule2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выбирите действие:");
                Console.WriteLine("1. Чётные, нечётные, уникальные элементы");
                Console.WriteLine("2. Количество элементов меньше заданного");
                Console.WriteLine("3. Поиск последовательности из 3-х чисел");
                Console.WriteLine("4. Общие элементы двух массивов (без повторов)");
                Console.WriteLine("5. Min и Max в двумерном массиве");
                Console.WriteLine("6. Подсчет слов в предложении");
                Console.WriteLine("7. Переворот слов в предложении");
                Console.WriteLine("8. Подсчет гласных букв");
                Console.WriteLine("9. Поиск подстроки в строке");
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
                    case "7": Task7(); break;
                    case "8": Task8(); break;
                    case "9": Task9(); break;
                    case "0": return;
                    default: Console.WriteLine("Ошибка ввода!"); Wait(); break;
                }
            }
        }

        // Задание 1
        static void Task1()
        {
            int[] arr = { 1, 2, 2, 3, 4, 5, 5, 6, 7, 8 };
            int even = 0, odd = 0, unique = 0;

            Console.WriteLine("Массив: " + ArrayToString(arr));

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0) even++; else odd++;

                bool isUnique = true;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (i != j && arr[i] == arr[j])
                    {
                        isUnique = false;
                        break;
                    }
                }
                if (isUnique) unique++;
            }

            Console.WriteLine($"Чётных: {even}\nНечётных: {odd}\nУникальных: {unique}");
            Wait();
        }

        // Задание 2
        static void Task2()
        {
            int[] arr = { 1, 5, 8, 12, 4, 7, 3, 9 };
            Console.WriteLine("Массив: " + ArrayToString(arr));
            Console.Write("Введите число для сравнения: ");
            int limit = int.Parse(Console.ReadLine());
            int count = 0;

            foreach (int x in arr) if (x < limit) count++;

            Console.WriteLine($"Количество элементов меньше {limit}: {count}");
            Wait();
        }

        // Задание 3
    static void Task3()
    {
        int[] arr = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };
        Console.WriteLine("Массив: " + ArrayToString(arr));
        Console.Write("Введите три числа через пробел: ");
        
        string inputStr = Console.ReadLine();
        string[] parts = inputStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 3)
        {
            Console.WriteLine("Ошибка: нужно ввести минимум ТРИ числа!");
            Wait();
            return;
        }

        try 
        {
            int n1 = int.Parse(parts[0]);
            int n2 = int.Parse(parts[1]);
            int n3 = int.Parse(parts[2]);
            
            int count = 0;
            for (int i = 0; i <= arr.Length - 3; i++)
            {
                if (arr[i] == n1 && arr[i + 1] == n2 && arr[i + 2] == n3) 
                    count++;
            }

            Console.WriteLine($"Последовательность {n1} {n2} {n3} встречается {count} раз(а).");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: вводите только цифры!");
        }

        Wait();
    }

        // Задание 4
        static void Task4()
        {
            int[] A = { 1, 2, 3, 4, 5, 2 };
            int[] B = { 2, 4, 6, 8, 2 };
            int[] temp = new int[Math.Min(A.Length, B.Length)];
            int count = 0;

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    if (A[i] == B[j])
                    {
                        bool alreadyExists = false;
                        for (int k = 0; k < count; k++)
                            if (temp[k] == A[i]) alreadyExists = true;

                        if (!alreadyExists) temp[count++] = A[i];
                        break;
                    }
                }
            }

            Console.WriteLine("Массив A: " + ArrayToString(A));
            Console.WriteLine("Массив B: " + ArrayToString(B));
            Console.Write("Общие элементы: ");
            for (int i = 0; i < count; i++) Console.Write(temp[i] + " ");
            Wait();
        }

        // Задание 5
        static void Task5()
        {
            int[,] matrix = { { 5, 2, 9 }, { 1, 8, 4 }, { 7, 3, 6 } };
            int min = matrix[0, 0], max = matrix[0, 0];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                    if (matrix[i, j] < min) min = matrix[i, j];
                    if (matrix[i, j] > max) max = matrix[i, j];
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Min: {min}, Max: {max}");
            Wait();
        }

        // Задание 6
        static void Task6()
        {
            Console.Write("Введите предложение: ");
            string input = Console.ReadLine();
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"Количество слов: {words.Length}");
            Wait();
        }

        // Задание 7
        static void Task7()
        {
            Console.Write("Введите предложение: ");
            string input = Console.ReadLine();
            string[] words = input.Split(' ');
            
            Console.Write("Результат: ");
            foreach (string word in words)
            {
                char[] charArray = word.ToCharArray();
                for (int i = 0; i < charArray.Length / 2; i++)
                {
                    char temp = charArray[i];
                    charArray[i] = charArray[charArray.Length - 1 - i];
                    charArray[charArray.Length - 1 - i] = temp;
                }
                Console.Write(new string(charArray) + " ");
            }
            Wait();
        }

        // Задание 8
        static void Task8()
        {
            Console.Write("Введите предложение: ");
            string input = Console.ReadLine().ToLower();
            string vowels = "aeiouyаеёиоуыэюя";
            int count = 0;

            foreach (char c in input)
            {
                if (vowels.IndexOf(c) != -1) count++;
            }
            Console.WriteLine($"Количество гласных: {count}");
            Wait();
        }

        // Задание 9
        static void Task9()
        {
            Console.Write("Введите строку: ");
            string text = Console.ReadLine();
            Console.Write("Введите подстроку для поиска: ");
            string sub = Console.ReadLine();

            int count = 0;
            int index = text.IndexOf(sub);
            while (index != -1)
            {
                count++;
                index = text.IndexOf(sub, index + sub.Length);
            }
            Console.WriteLine($"Количество вхождений: {count}");
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