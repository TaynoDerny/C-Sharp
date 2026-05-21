using System;

namespace Program
{
    // Задание 5 
    enum ArticleType
    {
        Electronics,
        Clothing,
        Food,
        Books,
        Furniture
    }

    // Задание 6 
    enum ClientType
    {
        Regular,
        VIP,
        Corporate,
        New
    }

    // Задание 7 
    enum PayType
    {
        Cash,
        Card,
        Online,
        Credit
    }

    // Задание 1 
    struct Article
    {
        public int ProductCode;    // Код товара
        public string ProductName; // Название товара
        public double Price;       // Цена товара
        public ArticleType Type;  

        public void Print()
        {
            Console.WriteLine($"Товар #{ProductCode}: {ProductName}, Тип: {Type}, Цена: {Price:C}");
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
        public ClientType Type;    

        public void Print()
        {
            Console.WriteLine($"Клиент #{ClientCode}: {FullName} ({Type})");
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
        public PayType PaymentType; 

        // Вычисление свойство: Сумма заказа
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
            Console.WriteLine($" ЗАКАЗ #{OrderCode} от {OrderDate.ToShortDateString()} ");
            Console.WriteLine($"Клиент: {Client.FullName}, Тип оплаты: {PaymentType}");
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

    // Задание 8:
    class Student
    {
        public string LastName;
        public string FirstName;
        public string Patronymic;
        public string Group;
        public int Age;
        public int[][] Grades; 
        public void SetGrades(int[] programming, int[] admin, int[] design)
        {
            Grades = new int[3][];
            Grades[0] = programming;
            Grades[1] = admin;
            Grades[2] = design;
        }

        public int GetGrade(int subjectIndex, int gradeIndex)
        {
            if (Grades[subjectIndex] == null)
                throw new Exception("Оценки по этому предмету не установлены");
            return Grades[subjectIndex][gradeIndex];
        }

        public double GetAverageBySubject(int subjectIndex)
        {
            if (Grades[subjectIndex] == null || Grades[subjectIndex].Length == 0)
                return 0;
            double sum = 0;
            foreach (int grade in Grades[subjectIndex])
                sum += grade;
            return sum / Grades[subjectIndex].Length;
        }

        public double GetAverageTotal()
        {
            double total = 0;
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Grades[i] != null)
                {
                    foreach (int grade in Grades[i])
                    {
                        total += grade;
                        count++;
                    }
                }
            }
            return count > 0 ? total / count : 0;
        }

        public void Print()
        {
            Console.WriteLine($"Студент: {LastName} {FirstName} {Patronymic}");
            Console.WriteLine($"Группа: {Group}, Возраст: {Age}");
            string[] subjects = { "Программирование", "Администрирование", "Дизайн" };
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"  {subjects[i]}: ");
                if (Grades[i] != null)
                {
                    foreach (int grade in Grades[i])
                        Console.Write(grade + " ");
                    Console.Write($"(ср: {GetAverageBySubject(i):F2})");
                }
                else
                {
                    Console.Write("нет оценок");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Общий средний балл: {GetAverageTotal():F2}");
        }
    }

    // Задание 9: 
    namespace SevenWonders
    {
        public class GreatPyramidOfGiza
        {
            public string Name => "Пирамида Хеопса";
            public string Location => "Египет, Гиза";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class HangingGardensOfBabylon
        {
            public string Name => "Висячие сады Семирамиды";
            public string Location => "Вавилон (совр. Ирак)";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class StatueOfZeus
        {
            public string Name => "Статуя Зевса в Олимпии";
            public string Location => "Греция, Олимпия";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class TempleOfArtemis
        {
            public string Name => "Храм Артемиды в Эфесе";
            public string Location => "Турция, Эфес";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class MausoleumAtHalicarnassus
        {
            public string Name => "Мавзолей в Галикарнасе";
            public string Location => "Турция, Бодрум";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class ColossusOfRhodes
        {
            public string Name => "Колосс Родосский";
            public string Location => "Греция, о. Родос";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class LighthouseOfAlexandria
        {
            public string Name => "Александрийский маяк";
            public string Location => "Египет, Александрия";
            public void Print() => Console.WriteLine($"Чудо: {Name}, Место: {Location}");
        }

        public class WondersOfTheWorld
        {
            public static void ShowAll()
            {
                Console.WriteLine(" 7 ЧУДЕС СВЕТА ");
                new GreatPyramidOfGiza().Print();
                new HangingGardensOfBabylon().Print();
                new StatueOfZeus().Print();
                new TempleOfArtemis().Print();
                new MausoleumAtHalicarnassus().Print();
                new ColossusOfRhodes().Print();
                new LighthouseOfAlexandria().Print();
            }
        }
    }

    // Задание 10: Столицы 
    namespace Russia
    {
        public class Moscow
        {
            public string Name => "Москва";
            public int Population => 11920000;
            public void Print() => Console.WriteLine($"Столица России: {Name}, Население: {Population:N0} чел.");
        }
    }

    namespace France
    {
        public class Paris
        {
            public string Name => "Париж";
            public int Population => 2141000;
            public void Print() => Console.WriteLine($"Столица Франции: {Name}, Население: {Population:N0} чел.");
        }
    }

    namespace Japan
    {
        public class Tokyo
        {
            public string Name => "Токио";
            public int Population => 13960000;
            public void Print() => Console.WriteLine($"Столица Японии: {Name}, Население: {Population:N0} чел.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Article laptop = new Article { ProductCode = 101, ProductName = "Ноутбук", Price = 55000, Type = ArticleType.Electronics };
            Article mouse = new Article { ProductCode = 102, ProductName = "Мышь", Price = 1500, Type = ArticleType.Electronics };

            Client customer = new Client
            {
                ClientCode = 1,
                FullName = "Иванов Иван Иванович",
                Address = "ул. Пушкина, д. 10",
                Phone = "8-800-555-35-35",
                OrderCount = 5,
                TotalSpent = 120000,
                Type = ClientType.Regular
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать структуру Article (Товар)");
                Console.WriteLine("2. Показать структуру Client (Клиент)");
                Console.WriteLine("3. Показать структуру RequestItem (Позиция заказа)");
                Console.WriteLine("4. Показать структуру Request (Заказ с расчетом суммы)");
                Console.WriteLine("5. Задание 5: Article + ArticleType (enum)");
                Console.WriteLine("6. Задание 6: Client + ClientType (enum)");
                Console.WriteLine("7. Задание 7: Request + PayType (enum)");
                Console.WriteLine("8. Задание 8: Класс Student");
                Console.WriteLine("9. Задание 9: 7 чудес света");
                Console.WriteLine("10. Задание 10: Столицы (пространства имён)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": laptop.Print(); Wait(); break;
                    case "2": customer.Print(); Wait(); break;
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
                            PaymentType = PayType.Online,
                            Items = new RequestItem[]
                            {
                                new RequestItem { Product = laptop, Quantity = 1 },
                                new RequestItem { Product = mouse, Quantity = 2 }
                            }
                        };
                        order.Print();
                        Wait();
                        break;
                    case "5": DemoTask5(); Wait(); break;
                    case "6": DemoTask6(); Wait(); break;
                    case "7": DemoTask7(); Wait(); break;
                    case "8": DemoTask8(); Wait(); break;
                    case "9": DemoTask9(); Wait(); break;
                    case "10": DemoTask10(); Wait(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный ввод!"); Wait(); break;
                }
            }
        }

        static void DemoTask5()
        {
            Console.WriteLine("\n Задание 5: Article + ArticleType ");
            Article phone = new Article
            {
                ProductCode = 201,
                ProductName = "Смартфон",
                Price = 29999,
                Type = ArticleType.Electronics
            };
            phone.Print();
        }

        static void DemoTask6()
        {
            Console.WriteLine("\n Задание 6: Client + ClientType ");
            Client vipClient = new Client
            {
                ClientCode = 777,
                FullName = "Артёмов Артём Артёмович",
                Address = "ул. VIP, д. 1",
                Phone = "8-999-999-99-99",
                OrderCount = 20,
                TotalSpent = 500000,
                Type = ClientType.VIP
            };
            vipClient.Print();
        }

        static void DemoTask7()
        {
            Console.WriteLine("\n Задание 7: Request + PayType ");
            Article laptop = new Article { ProductCode = 101, ProductName = "Ноутбук", Price = 55000, Type = ArticleType.Electronics };
            Client client = new Client { ClientCode = 1, FullName = "Иванов И.И.", Type = ClientType.Regular };

            Request order = new Request
            {
                OrderCode = 5002,
                Client = client,
                OrderDate = DateTime.Now,
                PaymentType = PayType.Online,
                Items = new RequestItem[]
                {
                    new RequestItem { Product = laptop, Quantity = 1 }
                }
            };
            order.Print();
        }

        static void DemoTask8()
        {
            Console.WriteLine("\n Задание 8: Класс Student ");
            Student student = new Student
            {
                LastName = "Петров",
                FirstName = "Алексей",
                Patronymic = "Сергеевич",
                Group = "21ПР12",
                Age = 20
            };

            student.SetGrades(
                new int[] { 5, 4, 5, 3 },
                new int[] { 5, 5, 4 },
                new int[] { 4, 4, 3, 5 }
            );

            student.Print();
            Console.WriteLine($"\nОценка по программированию (индекс 2): {student.GetGrade(0, 2)}");
            Console.WriteLine($"Средний балл по администрированию: {student.GetAverageBySubject(1):F2}");
        }

        static void DemoTask9()
        {
            Console.WriteLine("\n Задание 9: 7 чудес света ");
            SevenWonders.WondersOfTheWorld.ShowAll();
        }

        static void DemoTask10()
        {
            Console.WriteLine("\n Задание 10: Столицы ");
            Russia.Moscow moscow = new Russia.Moscow();
            France.Paris paris = new France.Paris();
            Japan.Tokyo tokyo = new Japan.Tokyo();

            moscow.Print();
            paris.Print();
            tokyo.Print();
        }

        static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
