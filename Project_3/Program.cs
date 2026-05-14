using System;

namespace HomeworkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            Console.WriteLine("=== Задание 1: Рисование квадрата ===");
            Console.Write("Введите длину стороны: ");
            int side = int.Parse(Console.ReadLine());
            
            Console.Write("Введите символ для рисования: ");
            char symbol = Console.ReadKey().KeyChar;
            Console.WriteLine(); // Переход на новую строку после ReadKey
            
            PrintSquare(side, symbol);
            Console.WriteLine();

            // Задание 2
            Console.WriteLine("=== Задание 2: Проверка на палиндром ===");
            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());
            
            bool isPal = IsPalindrome(number);
            Console.WriteLine(isPal ? "Это палиндром" : "Это не палиндром");
            Console.WriteLine();

            //  Задание 3
            Console.WriteLine("=== Задание 3: Фильтрация массива ===");
            
            // Ввод оригинального массива
            Console.Write("Введите элементы массива через пробел: ");
            string input1 = Console.ReadLine();
            string[] parts1 = input1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] originalArray = new int[parts1.Length];
            for (int i = 0; i < parts1.Length; i++) originalArray[i] = int.Parse(parts1[i]);

            // Ввод фильтра
            Console.Write("Введите числа для удаления через пробел: ");
            string input2 = Console.ReadLine();
            string[] parts2 = input2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] filterArray = new int[parts2.Length];
            for (int i = 0; i < parts2.Length; i++) filterArray[i] = int.Parse(parts2[i]);

            // Фильтрация
            int[] result = FilterArray(originalArray, filterArray);

            Console.Write("Результат: ");
            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }
            Console.WriteLine("\n\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void PrintSquare(int side, char symbol)
        {
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
        }

        static bool IsPalindrome(int number)
        {
            string s = number.ToString();
            int left = 0;
            int right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right]) return false;
                left++;
                right--;
            }
            return true;
        }

        static int[] FilterArray(int[] original, int[] filter)
        {
            int count = 0;
            for (int i = 0; i < original.Length; i++)
            {
                bool toDelete = false;
                for (int j = 0; j < filter.Length; j++)
                {
                    if (original[i] == filter[j]) { toDelete = true; break; }
                }
                if (!toDelete) count++;
            }

            int[] result = new int[count];
            int index = 0;
            for (int i = 0; i < original.Length; i++)
            {
                bool toDelete = false;
                for (int j = 0; j < filter.Length; j++)
                {
                    if (original[i] == filter[j]) { toDelete = true; break; }
                }
                if (!toDelete) result[index++] = original[i];
            }
            return result;
        }
    }
}
