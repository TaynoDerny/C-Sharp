using System;

namespace Program 
{
    // Задание 1
    struct Article
    {
        public int ProductCode;    // Код товара
        public string ProductName; // Название товара
        public double Price;       // Цена товара

        public void Print()
        {
            Console.WriteLine($"Товар #{ProductCode}: {ProductName}, Цена: {Price:C}");
        }
    }

    // Задание 2
    struct Client
    {
        public int ClientCode;      // Код клиента
        public string FullName;     // ФИО
        public string Address;      // Адрес
        public string Phone;        // Телефон
        public int OrderCount;      // Количество заказов
        public double TotalSpent;   // Общая сумма заказов

        public void Print()
        {
            Console.WriteLine($"Клиент #{ClientCode}: {FullName}");
            Console.WriteLine($"Адрес: {Address}, Тел: {Phone}");
            Console.WriteLine($"Заказов: {OrderCount}, Всего потрачено: {TotalSpent:C}");
        }
    }

    // Задание 3
    struct RequestItem
    {
        public Article Product; // Товар
        public int Quantity;    // Количество

        public void Print()
        {
            Console.WriteLine($"- {Product.ProductName} x {Quantity} (ед. цена: {Product.Price:C})");
        }
    }

    // Задание 4
    struct Request
    {
        public int OrderCode;       // Код заказа
        public Client Client;       // Клиент
        public DateTime OrderDate;  // Дата заказа
        public RequestItem[] Items; // Перечень товаров

        // Вычисляемое свойство: Сумма заказа
        public double TotalPrice
        {
            get
            {
                double sum = 0;
                if (Items != null)
                {
                    for (int i = 0; i < Items.Length; i++)
                    {
                        sum += Items[i].Product.Price * Items[i].Quantity;
                    }
                }
                return sum;
            }
        }

        public void Print()
        {
            Console.WriteLine($" ЗАКАЗ #{OrderCode} от {OrderDate.ToShortDateString()} ===");
            Console.WriteLine($"Клиент: {Client.FullName}");
            Console.WriteLine("Состав заказа:");
            if (Items != null)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    Items[i].Print();
                }
            }
            Console.WriteLine($"ИТОГО К ОПЛАТЕ: {TotalPrice:C}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Article laptop = new Article { ProductCode = 101, ProductName = "Ноутбук", Price = 55000 };
            Article mouse = new Article { ProductCode = 102, ProductName = "Мышь", Price = 1500 };
            
            Client customer = new Client 
            { 
                ClientCode = 1, 
                FullName = "Иванов Иван Иванович", 
                Address = "ул. Пушкина, д. 10", 
                Phone = "8-800-555-35-35",
                OrderCount = 5,
                TotalSpent = 120000
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберитите действие: ");
                Console.WriteLine("1. Показать структуру Article (Товар)");
                Console.WriteLine("2. Показать структуру Client (Клиент)");
                Console.WriteLine("3. Показать структуру RequestItem (Позиция заказа)");
                Console.WriteLine("4. Показать структуру Request (Заказ с расчетом суммы)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        laptop.Print();
                        Wait();
                        break;
                    case "2":
                        customer.Print();
                        Wait();
                        break;
                    case "3":
                        RequestItem item = new RequestItem { Product = laptop, Quantity = 2 };
                        item.Print();
                        Wait();
                        break;
                    case "4":
                        Request order = new Request
                        {
                            OrderCode = 5001,
                            Client = customer,
                            OrderDate = DateTime.Now,
                            Items = new RequestItem[] 
                            { 
                                new RequestItem { Product = laptop, Quantity = 1 },
                                new RequestItem { Product = mouse, Quantity = 2 }
                            }
                        };
                        order.Print();
                        Wait();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        Wait();
                        break;
                }
            }
        }

        static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}