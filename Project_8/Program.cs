using System;


// Задание 1

Console.WriteLine("=== Задание 1 ===");
Console.Write("Введите число (набор цифр 0-9): ");
string input1 = Console.ReadLine();

try
{
    // Попытка парсинга строки в целое число
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


// Задание 2: 

Console.WriteLine("=== Задание 2 ===");
Console.Write("Введите число в двоичной системе (набор 0 и 1): ");
string input2 = Console.ReadLine();

try
{
    // Преобразование строки из двоичной системы счисления (основание 2) в десятичное int
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


// Задание 3

Console.WriteLine("=== Задание 3 ===");
try
{
    // Пробуем создать валидную карту
    CreditCard validCard = new CreditCard("4111222233334444", "IVAN IVANOV", "123", new DateTime(2028, 12, 1));
    Console.WriteLine("Карта 1 успешно создана:");
    validCard.PrintInfo();

    // Пробуем создать карту с ошибкой (неверный CVC) для проверки исключения
    Console.WriteLine("\nПопытка создать карту с некорректными данными...");
    CreditCard invalidCard = new CreditCard("4111222233334444", "IVAN IVANOV", "12", new DateTime(2028, 12, 1));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Ошибка инициализации карты: {ex.Message}");
}

Console.WriteLine();

// Задание 4

Console.WriteLine("=== Задание 4 ===");
Console.Write("Введите выражение на умножение (например, 3*2*1*4): ");
string input4 = Console.ReadLine();

try
{
    if (string.IsNullOrWhiteSpace(input4))
        throw new FormatException("Строка пуста.");

    // Разделяем строку по символу '*'
    string[] parts = input4.Split('*');
    int result = 1;

    // Блок checked для отлова переполнения при умножении больших чисел
    checked 
    {
        foreach (string part in parts)
        {
            // Убираем возможные пробелы и парсим каждое число
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

Console.ReadLine(); // Пауза в конце программы

// Задание 3

public class CreditCard
{
    public string CardNumber { get; private set; }
    public string OwnerName { get; private set; }
    public string CvcCode { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    public CreditCard(string cardNumber, string ownerName, string cvcCode, DateTime expirationDate)
    {
        // Проверка номера карты (упрощенная: не пусто и длина 16)
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
            throw new ArgumentException("Номер карты должен состоять из 16 символов.");

        // Проверка имени
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("ФИО владельца не может быть пустым.");

        // Проверка CVC
        if (string.IsNullOrWhiteSpace(cvcCode) || cvcCode.Length != 3)
            throw new ArgumentException("CVC код должен состоять из 3 цифр.");

        // Проверка даты завершения (карта не должна быть просрочена)
        if (expirationDate < DateTime.Now)
            throw new ArgumentException("Срок действия карты истек.");

        CardNumber = cardNumber;
        OwnerName = ownerName;
        CvcCode = cvcCode;
        ExpirationDate = expirationDate;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Номер карты: {CardNumber}");
        Console.WriteLine($"Владелец: {OwnerName}");
        Console.WriteLine($"CVC: *** (Скрыто в целях безопасности)");
        Console.WriteLine($"Годен до: {ExpirationDate:MM/yyyy}");
    }
}