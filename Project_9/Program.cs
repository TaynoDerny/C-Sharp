using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
           // Задание 1: 
            Console.Write("Введите длину стороны квадрата: ");
            int sideLength = int.Parse(Console.ReadLine());
            Console.Write("Введите символ: ");
            char symbol = Console.ReadLine()[0];
            DrawSquare(sideLength, symbol);
            Console.WriteLine();

            // Задание 2:         
            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());
            bool isPalindrome = IsPalindrome(number);
            Console.WriteLine($"Число {number} {(isPalindrome ? "является" : "НЕ является")} палиндромом");
            Console.WriteLine();

            // Задание 3:   
            int[] originalArray = { 1, 2, 6, -1, 88, 7, 6 };
            int[] filterArray = { 6, 88, 7 };
            int[] filteredResult = FilterArray(originalArray, filterArray);
            
            Console.Write("Оригинальный массив: ");
            PrintArray(originalArray);
            Console.Write("Массив для фильтрации: ");
            PrintArray(filterArray);
            Console.Write("Результат фильтрации: ");
            PrintArray(filteredResult);
            Console.WriteLine();

            // Задание 4:  
            Website site = new Website();
            site.InputData();
            site.DisplayData();
            Console.WriteLine($"Название сайта (через метод): {site.GetName()}");
            Console.WriteLine();

            // Задание 5:  
            Magazine journal = new Magazine();
            journal.InputData();
            journal.DisplayData();
            Console.WriteLine($"Год основания (через метод): {journal.GetYear()}");
            Console.WriteLine();

            // Задание 6:  
            Store store = new Store();
            store.InputData();
            store.DisplayData();
            Console.WriteLine($"Адрес магазина (через метод): {store.GetAddress()}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        //  ЗАДАНИЕ 1 
        static void DrawSquare(int sideLength, char symbol)
        {
            if (sideLength <= 0)
            {
                Console.WriteLine("Длина стороны должна быть больше 0");
                return;
            }
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        //  ЗАДАНИЕ 2 
        static bool IsPalindrome(int number)
        {
            string numStr = number.ToString();
            int left = 0;
            int right = numStr.Length - 1;
            while (left < right)
            {
                if (numStr[left] != numStr[right])
                    return false;
                left++;
                right--;
            }
            return true;
        }

        //  ЗАДАНИЕ 3 
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

        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + (i == arr.Length - 1 ? "" : " "));
            }
            Console.WriteLine();
        }

        //  ЗАДАНИЕ 4 
        class Website
        {
            private string name;
            private string url;
            private string description;
            private string ipAddress;

            public void InputData()
            {
                Console.Write("Введите название сайта: ");
                name = Console.ReadLine();
                Console.Write("Введите путь к сайту (URL): ");
                url = Console.ReadLine();
                Console.Write("Введите описание сайта: ");
                description = Console.ReadLine();
                Console.Write("Введите IP адрес сайта: ");
                ipAddress = Console.ReadLine();
            }

            public void DisplayData()
            {
                Console.WriteLine("Данные о сайте:");
                Console.WriteLine($"Название: {name}");
                Console.WriteLine($"URL: {url}");
                Console.WriteLine($"Описание: {description}");
                Console.WriteLine($"IP адрес: {ipAddress}");
            }

            public string GetName() { return name; }
            public string GetUrl() { return url; }
            public string GetDescription() { return description; }
            public string GetIpAddress() { return ipAddress; }
        }

        //  ЗАДАНИЕ 5 
        class Magazine
        {
            private string name;
            private int foundationYear;
            private string description;
            private string contactPhone;
            private string contactEmail;

            public void InputData()
            {
                Console.Write("Введите название журнала: ");
                name = Console.ReadLine();
                Console.Write("Введите год основания: ");
                foundationYear = int.Parse(Console.ReadLine());
                Console.Write("Введите описание журнала: ");
                description = Console.ReadLine();
                Console.Write("Введите контактный телефон: ");
                contactPhone = Console.ReadLine();
                Console.Write("Введите контактный e-mail: ");
                contactEmail = Console.ReadLine();
            }

            public void DisplayData()
            {
                Console.WriteLine("Данные о журнале:");
                Console.WriteLine($"Название: {name}");
                Console.WriteLine($"Год основания: {foundationYear}");
                Console.WriteLine($"Описание: {description}");
                Console.WriteLine($"Телефон: {contactPhone}");
                Console.WriteLine($"E-mail: {contactEmail}");
            }

            public string GetName() { return name; }
            public int GetYear() { return foundationYear; }
            public string GetDescription() { return description; }
            public string GetPhone() { return contactPhone; }
            public string GetEmail() { return contactEmail; }
        }

        //  ЗАДАНИЕ 6 
        class Store
        {
            private string name;
            private string address;
            private string profileDescription;
            private string contactPhone;
            private string contactEmail;

            public void InputData()
            {
                Console.Write("Введите название магазина: ");
                name = Console.ReadLine();
                Console.Write("Введите адрес магазина: ");
                address = Console.ReadLine();
                Console.Write("Введите описание профиля магазина: ");
                profileDescription = Console.ReadLine();
                Console.Write("Введите контактный телефон: ");
                contactPhone = Console.ReadLine();
                Console.Write("Введите контактный e-mail: ");
                contactEmail = Console.ReadLine();
            }

            public void DisplayData()
            {
                Console.WriteLine("Данные о магазине:");
                Console.WriteLine($"Название: {name}");
                Console.WriteLine($"Адрес: {address}");
                Console.WriteLine($"Профиль: {profileDescription}");
                Console.WriteLine($"Телефон: {contactPhone}");
                Console.WriteLine($"E-mail: {contactEmail}");
            }

            public string GetName() { return name; }
            public string GetAddress() { return address; }
            public string GetProfile() { return profileDescription; }
            public string GetPhone() { return contactPhone; }
            public string GetEmail() { return contactEmail; }
        }
    }
}