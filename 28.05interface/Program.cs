using System;

namespace Program 
{
    // Задание 1: 
    public interface IGeometricFigure
    {
        double CalculateArea();
        double CalculatePerimeter();
        string GetName();
    }

    public abstract class GeometricFigure : IGeometricFigure
    {
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
        public abstract string GetName();
    }

    public class Triangle : GeometricFigure
    {
        private double a, b, c;
        public Triangle(double a, double b, double c) { this.a = a; this.b = b; this.c = c; }
        public override double CalculateArea()
        {
            double p = CalculatePerimeter() / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        public override double CalculatePerimeter() => a + b + c;
        public override string GetName() => "Треугольник";
    }

    public class Square : GeometricFigure
    {
        private double side;
        public Square(double side) { this.side = side; }
        public override double CalculateArea() => side * side;
        public override double CalculatePerimeter() => 4 * side;
        public override string GetName() => "Квадрат";
    }

    public class Rectangle : GeometricFigure
    {
        private double w, h;
        public Rectangle(double w, double h) { this.w = w; this.h = h; }
        public override double CalculateArea() => w * h;
        public override double CalculatePerimeter() => 2 * (w + h);
        public override string GetName() => "Прямоугольник";
    }

    public class Rhombus : GeometricFigure
    {
        private double side, d1, d2;
        public Rhombus(double side, double d1, double d2) { this.side = side; this.d1 = d1; this.d2 = d2; }
        public override double CalculateArea() => (d1 * d2) / 2;
        public override double CalculatePerimeter() => 4 * side;
        public override string GetName() => "Ромб";
    }

    public class Parallelogram : GeometricFigure
    {
        private double b, side, h;
        public Parallelogram(double b, double side, double h) { this.b = b; this.side = side; this.h = h; }
        public override double CalculateArea() => b * h;
        public override double CalculatePerimeter() => 2 * (b + side);
        public override string GetName() => "Параллелограмм";
    }

    public class Trapezoid : GeometricFigure
    {
        private double b1, b2, s1, s2, h;
        public Trapezoid(double b1, double b2, double s1, double s2, double h) { this.b1 = b1; this.b2 = b2; this.s1 = s1; this.s2 = s2; this.h = h; }
        public override double CalculateArea() => (b1 + b2) * h / 2;
        public override double CalculatePerimeter() => b1 + b2 + s1 + s2;
        public override string GetName() => "Трапеция";
    }

    public class Circle : GeometricFigure
    {
        private double r;
        public Circle(double r) { this.r = r; }
        public override double CalculateArea() => Math.PI * r * r;
        public override double CalculatePerimeter() => 2 * Math.PI * r;
        public override string GetName() => "Круг";
    }

    public class Ellipse : GeometricFigure
    {
        private double a, b;
        public Ellipse(double a, double b) { this.a = a; this.b = b; }
        public override double CalculateArea() => Math.PI * a * b;
        public override double CalculatePerimeter()
        {
            double h = Math.Pow((a - b) / (a + b), 2);
            return Math.PI * (a + b) * (1 + 3 * h / (10 + Math.Sqrt(4 - 3 * h)));
        }
        public override string GetName() => "Эллипс";
    }

    public class CompositeFigure : IGeometricFigure
    {
        private IGeometricFigure[] figures;
        public CompositeFigure(params IGeometricFigure[] figures) { this.figures = figures; }
        public double CalculateArea()
        {
            double total = 0;
            foreach (var f in figures) total += f.CalculateArea();
            return total;
        }
        public double CalculatePerimeter()
        {
            double total = 0;
            foreach (var f in figures) total += f.CalculatePerimeter();
            return total;
        }
        public string GetName() => "Составная фигура";
    }

    // Задание 2: 
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        double Price { get; }
        int Quantity { get; set; }
        string GetProductType();
        string GetDetails();
    }

    public abstract class Product : IProduct
    {
        private static int nextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, double price, int quantity)
        {
            Id = nextId++;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public abstract string GetProductType();
        public abstract string GetDetails();
    }

    public class HouseholdChemical : Product
    {
        public string Purpose { get; set; }
        public HouseholdChemical(string name, double price, int quantity, string purpose) : base(name, price, quantity)
        {
            Purpose = purpose;
        }
        public override string GetProductType() => "Бытовая химия";
        public override string GetDetails() => $"Назначение: {Purpose}";
    }

    public class FoodProduct : Product
    {
        public DateTime ExpirationDate { get; set; }
        public FoodProduct(string name, double price, int quantity, DateTime expirationDate) : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
        }
        public override string GetProductType() => "Продукты питания";
        public override string GetDetails() => $"Срок годности: {ExpirationDate.ToShortDateString()}";
    }

    public interface IProductFlow
    {
        void Arrival(int quantity);
        void Sold(int quantity);
        void WrittenOff(int quantity);
        void Transferred(int quantity, string destination);
        void ShowStatus();
    }

    public class ProductManager : IProductFlow
    {
        private IProduct product;
        public ProductManager(IProduct product) { this.product = product; }

        public void Arrival(int quantity)
        {
            product.Quantity += quantity;
            Console.WriteLine($"Поступило {quantity} ед. товара '{product.Name}'. Текущее кол-во: {product.Quantity}");
        }

        public void Sold(int quantity)
        {
            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Ошибка: недостаточно товара '{product.Name}' для продажи. Доступно: {product.Quantity}");
                return;
            }
            product.Quantity -= quantity;
            Console.WriteLine($"Продано {quantity} ед. товара '{product.Name}'. Остаток: {product.Quantity}");
        }

        public void WrittenOff(int quantity)
        {
            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Ошибка: недостаточно товара '{product.Name}' для списания. Доступно: {product.Quantity}");
                return;
            }
            product.Quantity -= quantity;
            Console.WriteLine($"Списано {quantity} ед. товара '{product.Name}'. Остаток: {product.Quantity}");
        }

        public void Transferred(int quantity, string destination)
        {
            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Ошибка: недостаточно товара '{product.Name}' для передачи. Доступно: {product.Quantity}");
                return;
            }
            product.Quantity -= quantity;
            Console.WriteLine($"Передано {quantity} ед. товара '{product.Name}' в {destination}. Остаток: {product.Quantity}");
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Статус товара:");
            Console.WriteLine($"ID: {product.Id}, {product.GetProductType()}: {product.Name}, Цена: {product.Price:C}, Кол-во: {product.Quantity}");
            Console.WriteLine(product.GetDetails());
            Console.WriteLine($"Общая стоимость на складе: {product.Quantity * product.Price:C}");
        }
    }

    class Program
    {
        static void Main()
        {
            // Задание 1:
            Console.Write("Введите сторону квадрата: ");
            double side = double.Parse(Console.ReadLine());
            Console.Write("Введите радиус круга: ");
            double radius = double.Parse(Console.ReadLine());
            Console.Write("Введите основание треугольника: ");
            double triBase = double.Parse(Console.ReadLine());
            Console.Write("Введите высоту треугольника: ");
            double triHeight = double.Parse(Console.ReadLine());

            IGeometricFigure[] shapes = new IGeometricFigure[]
            {
                new Square(side),
                new Circle(radius),
                new Triangle(triBase, triBase, triBase)
            };

            Console.WriteLine("\nРезультаты:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape.GetName()}: Площадь = {shape.CalculateArea():F2}, Периметр = {shape.CalculatePerimeter():F2}");
            }

            CompositeFigure composite = new CompositeFigure(new Square(2), new Circle(1), new Rectangle(3, 4));
            Console.WriteLine($"\nСоставная фигура: Площадь = {composite.CalculateArea():F2}, Периметр = {composite.CalculatePerimeter():F2}");

            // Задание 2: 
            Console.Write("Введите название товара (бытовая химия): ");
            string chemName = Console.ReadLine();
            Console.Write("Введите цену: ");
            double chemPrice = double.Parse(Console.ReadLine());
            Console.Write("Введите количество: ");
            int chemQty = int.Parse(Console.ReadLine());
            Console.Write("Введите назначение: ");
            string purpose = Console.ReadLine();

            HouseholdChemical chem = new HouseholdChemical(chemName, chemPrice, chemQty, purpose);
            ProductManager manager = new ProductManager(chem);

            manager.ShowStatus();

            Console.Write("Сколько единиц поступило? ");
            int arrival = int.Parse(Console.ReadLine());
            manager.Arrival(arrival);

            Console.Write("Сколько единиц продано? ");
            int sold = int.Parse(Console.ReadLine());
            manager.Sold(sold);

            Console.Write("Сколько единиц списано? ");
            int writeOff = int.Parse(Console.ReadLine());
            manager.WrittenOff(writeOff);

            Console.Write("Сколько единиц передано? ");
            int transfer = int.Parse(Console.ReadLine());
            Console.Write("Куда передано? ");
            string dest = Console.ReadLine();
            manager.Transferred(transfer, dest);

            manager.ShowStatus();

            // Задание 2:
            Console.WriteLine("\nПродукты питания:");
            Console.Write("Введите название продукта: ");
            string foodName = Console.ReadLine();
            Console.Write("Введите цену: ");
            double foodPrice = double.Parse(Console.ReadLine());
            Console.Write("Введите количество: ");
            int foodQty = int.Parse(Console.ReadLine());
            Console.Write("Введите срок годности (дд.мм.гггг): ");
            DateTime exp = DateTime.Parse(Console.ReadLine());

            FoodProduct food = new FoodProduct(foodName, foodPrice, foodQty, exp);
            ProductManager foodManager = new ProductManager(food);
            foodManager.ShowStatus();
        }
    }
}