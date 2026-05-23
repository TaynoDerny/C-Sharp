using System;

namespace Program
{
    // Задание 1: 
    class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Human(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
        }
    }

    class Builder : Human
    {
        public string Specialization { get; set; }

        public Builder(string name, int age, string specialization) : base(name, age)
        {
            Specialization = specialization;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Специализация: {Specialization}");
        }

        public void Build() => Console.WriteLine($"{Name} строит.");
    }

    class Sailor : Human
    {
        public string Rank { get; set; }

        public Sailor(string name, int age, string rank) : base(name, age)
        {
            Rank = rank;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Звание: {Rank}");
        }

        public void Sail() => Console.WriteLine($"{Name} плавает.");
    }

    class Pilot : Human
    {
        public string AircraftType { get; set; }

        public Pilot(string name, int age, string aircraftType) : base(name, age)
        {
            AircraftType = aircraftType;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Тип самолета: {AircraftType}");
        }

        public void Fly() => Console.WriteLine($"{Name} летает.");
    }

    // Задание 2
    class Passport
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNumber { get; set; }
        public string Country { get; set; }

        public Passport(string firstName, string lastName, string passportNumber, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            PassportNumber = passportNumber;
            Country = country;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Паспорт: {LastName} {FirstName}, Номер: {PassportNumber}, Страна: {Country}");
        }
    }

    class ForeignPassport : Passport
    {
        public string ForeignPassportNumber { get; set; }
        private string[] visas;
        private int visaCount;

        public ForeignPassport(string firstName, string lastName, string passportNumber, string country, string foreignPassportNumber)
            : base(firstName, lastName, passportNumber, country)
        {
            ForeignPassportNumber = foreignPassportNumber;
            visas = new string[10];
            visaCount = 0;
        }

        public void AddVisa(string visaInfo)
        {
            if (visaCount < visas.Length)
            {
                visas[visaCount] = visaInfo;
                visaCount++;
            }
            else
            {
                Console.WriteLine("Невозможно добавить больше виз (достигнут лимит).");
            }
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Номер загранпаспорта: {ForeignPassportNumber}");
            Console.WriteLine("Визы:");
            if (visaCount == 0)
            {
                Console.WriteLine("  Нет виз");
            }
            else
            {
                for (int i = 0; i < visaCount; i++)
                {
                    Console.WriteLine($"  - {visas[i]}");
                }
            }
        }
    }

    // Задание 3:
    class Animal
    {
        public string Name { get; set; }
        public string Characteristic { get; set; }

        public Animal(string name, string characteristic)
        {
            Name = name;
            Characteristic = characteristic;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Животное: {Name}, Характеристика: {Characteristic}");
        }
    }

    class Tiger : Animal
    {
        public Tiger(string name) : base(name, "Полосатый, хищник, сильный") { }
        public void Roar() => Console.WriteLine($"{Name} рычит!");
    }

    class Crocodile : Animal
    {
        public Crocodile(string name) : base(name, "Рептилия, водный, мощные челюсти") { }
        public void Snap() => Console.WriteLine($"{Name} щелкает челюстями!");
    }

    class Kangaroo : Animal
    {
        public Kangaroo(string name) : base(name, "Сумчатый, прыгает, имеет сумку") { }
        public void Jump() => Console.WriteLine($"{Name} высоко прыгает!");
    }

    // Задание 4: 
    abstract class Shape
    {
        public abstract double GetArea();
    }

    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea() => Width * Height;
    }

    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius) => Radius = radius;

        public override double GetArea() => Math.PI * Radius * Radius;
    }

    class RightTriangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public RightTriangle(double @base, double height)
        {
            Base = @base;
            Height = height;
        }

        public override double GetArea() => 0.5 * Base * Height;
    }

    class Trapezoid : Shape
    {
        public double Base1 { get; set; }
        public double Base2 { get; set; }
        public double Height { get; set; }

        public Trapezoid(double base1, double base2, double height)
        {
            Base1 = base1;
            Base2 = base2;
            Height = height;
        }

        public override double GetArea() => 0.5 * (Base1 + Base2) * Height;
    }

    class Program
    {
        static void Main()
        {
            // Задание 1
            Console.WriteLine("Введите данные для строителя:");
            Console.Write("Имя: ");
            string bName = Console.ReadLine();
            Console.Write("Возраст: ");
            int bAge = int.Parse(Console.ReadLine());
            Console.Write("Специализация: ");
            string bSpec = Console.ReadLine();

            Console.WriteLine("\nВведите данные для моряка:");
            Console.Write("Имя: ");
            string sName = Console.ReadLine();
            Console.Write("Возраст: ");
            int sAge = int.Parse(Console.ReadLine());
            Console.Write("Звание: ");
            string sRank = Console.ReadLine();

            Console.WriteLine("\nВведите данные для летчика:");
            Console.Write("Имя: ");
            string pName = Console.ReadLine();
            Console.Write("Возраст: ");
            int pAge = int.Parse(Console.ReadLine());
            Console.Write("Тип самолета: ");
            string pAircraft = Console.ReadLine();

            Builder builder = new Builder(bName, bAge, bSpec);
            Sailor sailor = new Sailor(sName, sAge, sRank);
            Pilot pilot = new Pilot(pName, pAge, pAircraft);

            Console.WriteLine();
            builder.ShowInfo(); builder.Build();
            Console.WriteLine();
            sailor.ShowInfo(); sailor.Sail();
            Console.WriteLine();
            pilot.ShowInfo(); pilot.Fly();
            Console.WriteLine();

            // Задание 2
            Console.WriteLine("\nВведите данные для паспорта:");
            Console.Write("Имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            Console.Write("Номер паспорта: ");
            string passNum = Console.ReadLine();
            Console.Write("Страна: ");
            string country = Console.ReadLine();
            Console.Write("Номер загранпаспорта: ");
            string foreignNum = Console.ReadLine();

            ForeignPassport fp = new ForeignPassport(firstName, lastName, passNum, country, foreignNum);

            Console.Write("Сколько виз добавить? ");
            int visaCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < visaCount; i++)
            {
                Console.Write($"Введите информацию о визе {i + 1}: ");
                string visa = Console.ReadLine();
                fp.AddVisa(visa);
            }

            Console.WriteLine();
            fp.ShowInfo();
            Console.WriteLine();

            // Задание 3
            Console.WriteLine("\nВведите имена животных:");
            Console.Write("Имя тигра: ");
            string tigerName = Console.ReadLine();
            Console.Write("Имя крокодила: ");
            string crocName = Console.ReadLine();
            Console.Write("Имя кенгуру: ");
            string kangName = Console.ReadLine();

            Tiger tiger = new Tiger(tigerName);
            Crocodile croc = new Crocodile(crocName);
            Kangaroo kang = new Kangaroo(kangName);

            Console.WriteLine();
            tiger.ShowInfo(); tiger.Roar();
            Console.WriteLine();
            croc.ShowInfo(); croc.Snap();
            Console.WriteLine();
            kang.ShowInfo(); kang.Jump();
            Console.WriteLine();

            // Задание 4
            Console.WriteLine("\nВведите данные для прямоугольника:");
            Console.Write("Ширина: ");
            double w = double.Parse(Console.ReadLine());
            Console.Write("Высота: ");
            double h = double.Parse(Console.ReadLine());

            Console.WriteLine("\nВведите радиус круга:");
            double r = double.Parse(Console.ReadLine());

            Console.WriteLine("\nВведите данные для прямоугольного треугольника:");
            Console.Write("Основание: ");
            double triBase = double.Parse(Console.ReadLine());
            Console.Write("Высота: ");
            double triHeight = double.Parse(Console.ReadLine());

            Console.WriteLine("\nВведите данные для трапеции:");
            Console.Write("Основание 1: ");
            double trapBase1 = double.Parse(Console.ReadLine());
            Console.Write("Основание 2: ");
            double trapBase2 = double.Parse(Console.ReadLine());
            Console.Write("Высота: ");
            double trapHeight = double.Parse(Console.ReadLine());

            Shape[] shapes = new Shape[]
            {
                new Rectangle(w, h),
                new Circle(r),
                new RightTriangle(triBase, triHeight),
                new Trapezoid(trapBase1, trapBase2, trapHeight)
            };

            Console.WriteLine();
            for (int i = 0; i < shapes.Length; i++)
            {
                Console.WriteLine($"Фигура {i + 1}: Площадь = {shapes[i].GetArea():F2}");
            }
        }
    }
}