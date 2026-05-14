using System;
using System.Globalization;

namespace Project1{

    class Program
    {
        static void Main()
        {
            //Задание 1
            byte number;
            Console.WriteLine("Введите число от 1 до 100: ");
            number = Convert.ToByte(Console.ReadLine());
            if(number > 0 &&  number < 100)
            {
                if (number % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if(number % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else if(number % 5 == 0 && number % 3 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
            }
            else
            {
                Console.WriteLine("Число введено неверно!");
            }
            /// задание 2
            int number_2_1;
            int number_2_2;
            System.Console.WriteLine("Введите число:");
            number_2_1 = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Введите процент:");
            number_2_2 = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine($"Процент от числа {number_2_1} = {number_2_1 * number_2_2 / 100.0f}");
            /// Задание 3
            Console.WriteLine("Введите четыре цифры по очереди:");

            int d1 = Convert.ToInt32(Console.ReadLine()); // Тысячи
            int d2 = Convert.ToInt32(Console.ReadLine()); // Сотни
            int d3 = Convert.ToInt32(Console.ReadLine()); // Десятки
            int d4 = Convert.ToInt32(Console.ReadLine()); // Единицы

            // Собираем число:
            int result = d1 * 1000 + d2 * 100 + d3 * 10 + d4;

            Console.WriteLine($"Сформированное число: {result}");
            //Задание 4
            Console.Write("Введите шестизначное число: ");
            string input = Console.ReadLine();

            if (input.Length != 6)
            {
                Console.WriteLine("Ошибка: Вы ввели не шестизначное число!");
            }
            else
            {
                Console.Write("Введите номер первого разряда (1-6): ");
                int pos1 = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Введите номер второго разряда (1-6): ");
                int pos2 = Convert.ToInt32(Console.ReadLine());

                char[] digits = input.ToCharArray();

                char temp = digits[pos1 - 1];
                digits[pos1 - 1] = digits[pos2 - 1];
                digits[pos2 - 1] = temp;

                string results = new string(digits);
                Console.WriteLine($"Результат: {results}");
            }
            //Задание 5
            Console.Write("Введите дату (ДД.ММ.ГГГГ): ");
            string userDateInput = Console.ReadLine();

            if (DateTime.TryParseExact(userDateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                string periodName = "";
                int monthNumber = parsedDate.Month;

                if (monthNumber == 12 || monthNumber == 1 || monthNumber == 2)
                    periodName = "Winter";
                else if (monthNumber >= 3 && monthNumber <= 5)
                    periodName = "Spring";
                else if (monthNumber >= 6 && monthNumber <= 8)
                    periodName = "Summer";
                else
                    periodName = "Autumn";

                string weeklyDay = parsedDate.DayOfWeek.ToString();

                Console.WriteLine($"{periodName} {weeklyDay}");
            }
            else
            {
                Console.WriteLine("Ошибка: некорректный формат даты.");
            }


            //Задание 6
            Console.WriteLine("Выберите направление перевода:");
            Console.WriteLine("1 - Из Цельсия в Фаренгейт");
            Console.WriteLine("2 - Из Фаренгейта в Цельсий");

            string userChoice = Console.ReadLine();

            Console.Write("Введите значение температуры: ");
            double rawValue = Convert.ToDouble(Console.ReadLine());

            if (userChoice == "1")
            {
                double fahrenheitResult = rawValue * 1.8 + 32;
                Console.WriteLine($"{rawValue}°C равно {fahrenheitResult}°F");
            }
            else if (userChoice == "2")
            {
                double celsiusResult = (rawValue - 32) / 1.8;
                Console.WriteLine($"{rawValue}°F равно {celsiusResult}°C");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный выбор операции.");
            }

            //Задача 7
            Console.Write("Введите начало диапазона: ");
            int firstEdge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите конец диапазона: ");
            int secondEdge = Convert.ToInt32(Console.ReadLine());

            if (firstEdge > secondEdge)
            {
                int tempValue = firstEdge;
                firstEdge = secondEdge;
                secondEdge = tempValue;
            }

            Console.WriteLine($"Четные числа в диапазоне от {firstEdge} до {secondEdge}:");

            for (int currentNum = firstEdge; currentNum <= secondEdge; currentNum++)
            {
                if (currentNum % 2 == 0)
                {
                    Console.Write($"{currentNum} ");
                }
            }
        }
    }
}