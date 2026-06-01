using System;

namespace Program
{
    // Задание 2: Пользовательское исключение
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(string message) : base(message) { }
        public InvalidAmountException(string message, Exception inner) : base(message, inner) { }
    }

    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
        public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }
    }

    public class BankAccount
    {
        private static int _accountCounter = 1000;

        public string AccountNumber { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(double initialBalance = 0)
        {
            if (initialBalance < 0)
                throw new InvalidAmountException("Начальный баланс не может быть отрицательным");

            AccountNumber = $"ACCT-{_accountCounter++}";
            Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Сумма пополнения должна быть положительной");

            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Сумма снятия должна быть положительной");

            if (amount > Balance)
                throw new InsufficientFundsException("Недостаточно средств на счете");

            Balance -= amount;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: 
            Console.WriteLine("ЗАДАНИЕ 1: Безопасное деление и работа с массивами \n");
            Task1_SafeDivision();

            Console.WriteLine("\n ЗАДАНИЕ 1 ЗАВЕРШЕНО \n");
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти ко второму заданию...");
            Console.ReadKey();
            Console.Clear();

            //  Задание 2: 
            Console.WriteLine("ЗАДАНИЕ 2: Банковский счёт с пользовательскими исключениями \n");
            Task2_BankAccount();

            Console.WriteLine("\n Программа завершена ");
            Console.ReadKey();
        }

        // Заадание 1 
        static void Task1_SafeDivision()
        {
            int[] results = new int[3];
            int index = 0;
            bool continueProgram = true;

            while (continueProgram && index < results.Length)
            {
                try
                {
                    Console.Write("Введите первое число: ");
                    int num1 = int.Parse(Console.ReadLine());

                    Console.Write("Введите второе число: ");
                    int num2 = int.Parse(Console.ReadLine());

                    int result = num1 / num2;

                    results[index] = result;
                    index++;

                    Console.WriteLine($"Результат: {result}");

                    if (index < results.Length)
                    {
                        Console.Write("Хотите продолжить? (да/нет): ");
                        string answer = Console.ReadLine().Trim().ToLower();
                        if (answer != "да")
                            continueProgram = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Ошибка: деление на ноль невозможно");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Массив результатов заполнен, дальнейшие вычисления невозможны");
                    break;
                }
                finally
                {
                    Console.WriteLine("Попытка выполнения операции завершена");
                }
            }

            Console.WriteLine("\nВсе успешно полученные результаты:");
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine($"Результат {i + 1}: {results[i]}");
            }
        }

        // Задание 2 
        static void Task2_BankAccount()
        {
            BankAccount account = null;

            while (account == null)
            {
                try
                {
                    Console.Write("Введите начальный баланс (или 0 для счёта по умолчанию): ");
                    double initial = double.Parse(Console.ReadLine());
                    account = new BankAccount(initial);
                    Console.WriteLine($"Счёт создан. Номер счета: {account.AccountNumber}, баланс: {account.Balance:F2}");
                }
                catch (InvalidAmountException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите корректное число");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }
            }

            bool exit = false;
            while (!exit)
            {
                try
                {
                    Console.WriteLine("\nДоступные операции:");
                    Console.WriteLine("1 - Пополнить");
                    Console.WriteLine("2 - Снять");
                    Console.WriteLine("3 - Показать баланс");
                    Console.WriteLine("4 - Выйти");
                    Console.Write("Выберите действие: ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Write("Введите сумму пополнения: ");
                            double depositAmount = double.Parse(Console.ReadLine());
                            account.Deposit(depositAmount);
                            Console.WriteLine("Счёт успешно пополнен");
                            break;

                        case "2":
                            Console.Write("Введите сумму снятия: ");
                            double withdrawAmount = double.Parse(Console.ReadLine());
                            account.Withdraw(withdrawAmount);
                            Console.WriteLine("Средства успешно сняты");
                            break;

                        case "3":
                            break; // Баланс выведется после switch

                        case "4":
                            exit = true;
                            Console.WriteLine("Программа завершена.");
                            continue;

                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            continue;
                    }

                    Console.WriteLine($"Текущий баланс: {account.Balance:F2}");
                }
                catch (InvalidAmountException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine($"Текущий баланс: {account.Balance:F2}");
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine($"Текущий баланс: {account.Balance:F2}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите корректное число");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }
            }
        }
    }
}