using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ДОМАШНЕЕ ЗАДАНИЕ Модуль 3 Часть 1 ===");
            Console.WriteLine("1 - Квадрат из символов");
            Console.WriteLine("2 - Проверка числа на палиндром");
            Console.WriteLine("3 - Фильтрация массива");
            Console.WriteLine("4 - Работа с классом Веб-сайт");
            Console.WriteLine("5 - Работа с классом Журнал");
            Console.WriteLine("6 - Работа с классом Магазин");
            Console.WriteLine("0 - Выход");
            Console.Write("Выберите задание: ");
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
                default: Console.WriteLine("Неверный выбор. Нажмите любую клавишу..."); Console.ReadKey(); break;
            }
        }
    }

    static void Task1()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 1: Квадрат из символов ===");
        Console.Write("Введите длину стороны квадрата: ");
        int side = int.Parse(Console.ReadLine());
        Console.Write("Введите символ: ");
        char symbol = Console.ReadLine()[0];
        DrawSquare(side, symbol);
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static void DrawSquare(int side, char symbol)
    {
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
                Console.Write(symbol);
            Console.WriteLine();
        }
    }

    static void Task2()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 2: Палиндром ===");
        Console.Write("Введите число: ");
        int num = int.Parse(Console.ReadLine());
        bool result = IsPalindrome(num);
        Console.WriteLine($"Число {num} {(result ? "является" : "НЕ является")} палиндромом");
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static bool IsPalindrome(int number)
    {
        string s = number.ToString();
        for (int i = 0; i < s.Length / 2; i++)
            if (s[i] != s[s.Length - 1 - i])
                return false;
        return true;
    }

    static void Task3()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 3: Фильтрация массива ===");
        Console.Write("Введите элементы оригинального массива через пробел: ");
        string[] origStr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int[] original = Array.ConvertAll(origStr, int.Parse);

        Console.Write("Введите элементы массива для фильтрации через пробел: ");
        string[] filtStr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int[] filter = Array.ConvertAll(filtStr, int.Parse);

        int[] result = FilterArray(original, filter);
        Console.Write("Результат: ");
        foreach (int num in result)
            Console.Write(num + " ");
        Console.WriteLine("\n\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static int[] FilterArray(int[] original, int[] filter)
    {
        int count = 0;
        for (int i = 0; i < original.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < filter.Length; j++)
            {
                if (original[i] == filter[j])
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                count++;
        }

        int[] result = new int[count];
        int index = 0;
        for (int i = 0; i < original.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < filter.Length; j++)
            {
                if (original[i] == filter[j])
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                result[index] = original[i];
                index++;
            }
        }
        return result;
    }

    static void Task4()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 4: Веб-сайт ===");
        Website site = new Website();
        site.InputData();
        Console.WriteLine("\n--- Данные о сайте ---");
        site.DisplayData();
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static void Task5()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 5: Журнал ===");
        Magazine mag = new Magazine();
        mag.InputData();
        Console.WriteLine("\n--- Данные о журнале ---");
        mag.DisplayData();
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static void Task6()
    {
        Console.Clear();
        Console.WriteLine("=== Задание 6: Магазин ===");
        Store store = new Store();
        store.InputData();
        Console.WriteLine("\n--- Данные о магазине ---");
        store.DisplayData();
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
}

class Website
{
    private string name;
    private string url;
    private string description;
    private string ip;

    public void InputData()
    {
        Console.Write("Название сайта: ");
        name = Console.ReadLine();
        Console.Write("Путь к сайту: ");
        url = Console.ReadLine();
        Console.Write("Описание сайта: ");
        description = Console.ReadLine();
        Console.Write("IP адрес сайта: ");
        ip = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Название: {name}");
        Console.WriteLine($"Путь: {url}");
        Console.WriteLine($"Описание: {description}");
        Console.WriteLine($"IP: {ip}");
    }

    public string GetName() => name;
    public string GetUrl() => url;
    public string GetDescription() => description;
    public string GetIp() => ip;
}

class Magazine
{
    private string name;
    private int yearFounded;
    private string description;
    private string contactPhone;
    private string contactEmail;

    public void InputData()
    {
        Console.Write("Название журнала: ");
        name = Console.ReadLine();
        Console.Write("Год основания: ");
        yearFounded = int.Parse(Console.ReadLine());
        Console.Write("Описание журнала: ");
        description = Console.ReadLine();
        Console.Write("Контактный телефон: ");
        contactPhone = Console.ReadLine();
        Console.Write("Контактный e-mail: ");
        contactEmail = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Название: {name}");
        Console.WriteLine($"Год основания: {yearFounded}");
        Console.WriteLine($"Описание: {description}");
        Console.WriteLine($"Телефон: {contactPhone}");
        Console.WriteLine($"E-mail: {contactEmail}");
    }

    public string GetName() => name;
    public int GetYearFounded() => yearFounded;
    public string GetDescription() => description;
    public string GetContactPhone() => contactPhone;
    public string GetContactEmail() => contactEmail;
}

class Store
{
    private string name;
    private string address;
    private string profile;
    private string contactPhone;
    private string contactEmail;

    public void InputData()
    {
        Console.Write("Название магазина: ");
        name = Console.ReadLine();
        Console.Write("Адрес магазина: ");
        address = Console.ReadLine();
        Console.Write("Описание профиля магазина: ");
        profile = Console.ReadLine();
        Console.Write("Контактный телефон: ");
        contactPhone = Console.ReadLine();
        Console.Write("Контактный e-mail: ");
        contactEmail = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Название: {name}");
        Console.WriteLine($"Адрес: {address}");
        Console.WriteLine($"Профиль: {profile}");
        Console.WriteLine($"Телефон: {contactPhone}");
        Console.WriteLine($"E-mail: {contactEmail}");
    }

    public string GetName() => name;
    public string GetAddress() => address;
    public string GetProfile() => profile;
    public string GetContactPhone() => contactPhone;
    public string GetContactEmail() => contactEmail;
}