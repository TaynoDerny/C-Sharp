using System;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите номер задания (1-4) или 0 для выхода:");
                Console.WriteLine("1. Квадраты в прямоугольнике");
                Console.WriteLine("2. Банковский вклад");
                Console.WriteLine("3. Числовой ряд");
                Console.WriteLine("4. Число справа налево (реверс)");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                Console.ReadKey();
            }
        }

        // Задание 1
        static void Task1()
        {
            Console.Write("Введите сторону A: ");
            int A = int.Parse(Console.ReadLine());
            Console.Write("Введите сторону B: ");
            int B = int.Parse(Console.ReadLine());
            Console.Write("Введите сторону C: ");
            int C = int.Parse(Console.ReadLine());

            if (C > A || C > B)
            {
                Console.WriteLine("Служебное сообщение: Квадрат со стороной C нельзя разместить в прямоугольнике.");
            }
            else
            {
                int countByA = A / C;
                int countByB = B / C;
                int totalSquares = countByA * countByB;
                int leftoverArea = (A * B) - (totalSquares * C * C);

                Console.WriteLine($"Количество квадратов: {totalSquares}");
                Console.WriteLine($"Площадь незанятой части: {leftoverArea}");
            }
        }

        // Задание 2
        static void Task2()
        {
            double S = 10000;
            Console.Write("Введите процент P (0 < P < 25): ");
            double P = double.Parse(Console.ReadLine());

            if (P > 0 && P < 25)
            {
                int K = 0;
                while (S <= 11000)
                {
                    S += S * (P / 100);
                    K++;
                }
                Console.WriteLine($"Количество месяцев K: {K}");
                Console.WriteLine($"Итоговый размер вклада S: {S:F2}");
            }
            else
            {
                Console.WriteLine("Ошибка: P должно быть от 0 до 25.");
            }
        }

        // Задание 3
        static void Task3()
        {
            Console.Write("Введите A: ");
            int A = int.Parse(Console.ReadLine());
            Console.Write("Введите B (A < B): ");
            int B = int.Parse(Console.ReadLine());

            for (int i = A; i <= B; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
        }

        // Задание 4
        static void Task4()
        {
            Console.Write("Введите целое число N > 0: ");
            int N = int.Parse(Console.ReadLine());

            if (N <= 0)
            {
                Console.WriteLine("Число должно быть больше 0.");
                return;
            }

            Console.Write("Число справа налево: ");
            while (N > 0)
            {
                int digit = N % 10; 
                Console.Write(digit);
                N = N / 10; 
            }
            Console.WriteLine();
        }
    }
}
