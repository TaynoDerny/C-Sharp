using System;

namespace MyLibrary
{
    //  ЗАДАНИЕ 1 
    public static class MessageHelper
    {
        public static void ShowInfo(string message)
        {
            Console.WriteLine($"[INFO]: {message}");
        }
    }

    //  ЗАДАНИЕ 2 
    public static class YearHelper
    {
        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
    }

    //  ЗАДАНИЕ 3 
    public static class MathHelper
    {
        public static int FindMax(int a, int b, int c)
        {
            return Math.Max(a, Math.Max(b, c));
        }

        public static int FindMin(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        public static int FindSum(int a, int b, int c)
        {
            return a + b + c;
        }
    }

    //  ЗАДАНИЕ 4 
    public static class NumberHelper
    {
        public static long CalculateFactorial(int n)
        {
            if (n < 0) return 0;
            long result = 1;
            for (int i = 1; i <= n; i++) result *= i;
            return result;
        }

        public static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }

        public static bool IsEven(int n) => n % 2 == 0;

        public static bool IsOdd(int n) => n % 2 != 0;
    }

    //  ЗАДАНИЕ 5 
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0) throw new ArgumentException("Знаменатель не может быть равен нулю.");
            Numerator = numerator;
            Denominator = denominator;
        }

        // Метод для сложения дробей
        public Fraction Add(Fraction other)
        {
            int newNum = this.Numerator * other.Denominator + other.Numerator * this.Denominator;
            int newDen = this.Denominator * other.Denominator;
            return new Fraction(newNum, newDen);
        }

        // Метод для вывода дроби
        public string GetString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}