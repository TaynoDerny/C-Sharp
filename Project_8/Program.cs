using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Задание 1 
            Console.WriteLine("=== Задание 1 ===");
            Console.Write("Введите число (набор цифр 0-9): ");
            string input1 = Console.ReadLine();
            try
            {
                int number1 = int.Parse(input1);
                Console.WriteLine($"Результат преобразования: {number1}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Введенное число выходит за границы диапазона типа int.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: В строке присутствуют недопустимые символы (ожидаются только цифры).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
            Console.WriteLine();

            //  Задание 2 
            Console.WriteLine("=== Задание 2 ===");
            Console.Write("Введите число в двоичной системе (набор 0 и 1): ");
            string input2 = Console.ReadLine();
            try
            {
                int number2 = Convert.ToInt32(input2, 2);
                Console.WriteLine($"Результат в десятичном представлении: {number2}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Введенное двоичное число выходит за границы диапазона типа int.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Неправильный ввод. Строка должна содержать только 0 и 1.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
            Console.WriteLine();

            //  Задание 3 
            Console.WriteLine("=== Задание 3 ===");
            Console.WriteLine("Создание кредитной карты.");
            try
            {
                Console.Write("Введите номер карты (16 цифр): ");
                string cardNumber = Console.ReadLine();

                Console.Write("Введите ФИО владельца: ");
                string ownerName = Console.ReadLine();

                Console.Write("Введите CVC код (3 цифры): ");
                string cvcCode = Console.ReadLine();

                Console.Write("Введите год окончания (например, 2028): ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("Введите месяц окончания (1-12): ");
                int month = int.Parse(Console.ReadLine());

                DateTime expirationDate = new DateTime(year, month, 1);

                CreditCard card = new CreditCard(cardNumber, ownerName, cvcCode, expirationDate);
                Console.WriteLine("Карта успешно создана!");
                card.PrintInfo();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка инициализации карты: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введены неверные данные (ожидаются числа).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
            Console.WriteLine();

            //  Задание 4 
            Console.WriteLine("=== Задание 4 ===");
            Console.Write("Введите выражение на умножение (например, 3*2*1*4): ");
            string input4 = Console.ReadLine();
            try
            {
                if (string.IsNullOrWhiteSpace(input4))
                    throw new FormatException("Строка пуста.");

                string[] parts = input4.Split('*');
                int result = 1;

                checked
                {
                    foreach (string part in parts)
                    {
                        result *= int.Parse(part.Trim());
                    }
                }

                Console.WriteLine($"Результат выражения: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: В строке должны быть только целые числа и оператор *.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Результат вычислений или одно из чисел слишком велико (выход за границы int).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }

            Console.ReadLine(); // Пауза
        }
    }

    //  Класс для задания 3 
    public class CreditCard
    {
        public string CardNumber { get; private set; }
        public string OwnerName { get; private set; }
        public string CvcCode { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public CreditCard(string cardNumber, string ownerName, string cvcCode, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Номер карты не может быть пустым.");

            string cleaned = cardNumber.Replace(" ", "").Replace("-", "");
            if (cleaned.Length != 16 || !IsDigitsOnly(cleaned))
                throw new ArgumentException("Номер карты должен состоять из 16 цифр.");

            if (string.IsNullOrWhiteSpace(ownerName))
                throw new ArgumentException("ФИО владельца не может быть пустым.");

            if (string.IsNullOrWhiteSpace(cvcCode) || cvcCode.Length != 3 || !IsDigitsOnly(cvcCode))
                throw new ArgumentException("CVC код должен состоять из 3 цифр.");

            if (expirationDate < DateTime.Now)
                throw new ArgumentException("Срок действия карты истек.");

            CardNumber = cleaned;
            OwnerName = ownerName;
            CvcCode = cvcCode;
            ExpirationDate = expirationDate;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Номер карты: {CardNumber}");
            Console.WriteLine($"Владелец: {OwnerName}");
            Console.WriteLine($"CVC: ***");
            Console.WriteLine($"Годен до: {ExpirationDate:MM/yyyy}");
        }
    }
}