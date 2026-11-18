using MyLibrary;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ЗАДАНИЕ 1 ");
            MessageHelper.ShowInfo("Привет! Это сообщение из подключенной DLL-библиотеки.");

            Console.WriteLine("\n ЗАДАНИЕ 2 ");
            int year1 = 2024;
            int year2 = 2023;
            Console.WriteLine($"{year1} год високосный? {YearHelper.IsLeapYear(year1)}");
            Console.WriteLine($"{year2} год високосный? {YearHelper.IsLeapYear(year2)}");

            Console.WriteLine("\n ЗАДАНИЕ 3 ");
            int a = 10, b = 45, c = 3;
            Console.WriteLine($"Числа: {a}, {b}, {c}");
            Console.WriteLine($"Минимум: {MathHelper.FindMin(a, b, c)}");
            Console.WriteLine($"Максимум: {MathHelper.FindMax(a, b, c)}");
            Console.WriteLine($"Сумма: {MathHelper.FindSum(a, b, c)}");

            Console.WriteLine("\n ЗАДАНИЕ 4 ");
            int num = 5;
            Console.WriteLine($"Факториал {num}: {NumberHelper.CalculateFactorial(num)}");
            Console.WriteLine($"Число 13 простое? {NumberHelper.IsPrime(13)}");
            Console.WriteLine($"Число 12 четное? {NumberHelper.IsEven(12)}");
            Console.WriteLine($"Число 12 нечетное? {NumberHelper.IsOdd(12)}");

            Console.WriteLine("\n ЗАДАНИЕ 5 ");
            Fraction f1 = new Fraction(1, 2); // 1/2
            Fraction f2 = new Fraction(1, 3); // 1/3

            Console.WriteLine($"Дробь 1: {f1.GetString()}");
            Console.WriteLine($"Дробь 2: {f2.GetString()}");

            Fraction sum = f1.Add(f2);
            Console.WriteLine($"Результат сложения: {sum.GetString()}");

            Console.WriteLine("\nВсе задания выполнены.");
            Console.ReadLine();
        }
    }
}