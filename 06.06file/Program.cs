using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class PaymentBill : IXmlSerializable
{
    //  Статическое свойство
    public static bool SerializeCalculatedFields { get; set; } = false;

    //  Основные поля
    public decimal RatePerDay { get; set; }          // оплата за день
    public int Days { get; set; }                    // количество дней
    public decimal PenaltyRatePerDay { get; set; }   // штраф за один день задержки оплаты
    public int OverdueDays { get; set; }             // количество дней задержки оплаты

    [XmlIgnore]
    public decimal PaymentWithoutPenalty => RatePerDay * Days;

    [XmlIgnore]
    public decimal PenaltyAmount => PenaltyRatePerDay * OverdueDays;

    [XmlIgnore]
    public decimal TotalAmount => PaymentWithoutPenalty + PenaltyAmount;

    public override string ToString()
    {
        return $"Оплата/день: {RatePerDay}, Кол-во дней: {Days}\n" +
               $"Штраф/день задержки: {PenaltyRatePerDay}, Дней задержки: {OverdueDays}\n" +
               $"Сумма без штрафа: {PaymentWithoutPenalty}, Штраф: {PenaltyAmount}\n" +
               $"ИТОГО к оплате: {TotalAmount}\n";
    }

    public System.Xml.Schema.XmlSchema GetSchema() { return null; }

    public void WriteXml(XmlWriter writer)
    {
        NumberFormatInfo pointCulture = CultureInfo.InvariantCulture.NumberFormat;

        writer.WriteElementString(nameof(RatePerDay), RatePerDay.ToString(pointCulture));
        writer.WriteElementString(nameof(Days), Days.ToString());
        writer.WriteElementString(nameof(PenaltyRatePerDay), PenaltyRatePerDay.ToString(pointCulture));
        writer.WriteElementString(nameof(OverdueDays), OverdueDays.ToString());

        if (SerializeCalculatedFields)
        {
            writer.WriteElementString(nameof(PaymentWithoutPenalty), PaymentWithoutPenalty.ToString(pointCulture));
            writer.WriteElementString(nameof(PenaltyAmount), PenaltyAmount.ToString(pointCulture));
            writer.WriteElementString(nameof(TotalAmount), TotalAmount.ToString(pointCulture));
        }
    }

    public void ReadXml(XmlReader reader)
    {
        if (reader.IsEmptyElement)
        {
            reader.Read();
            return;
        }

        reader.ReadStartElement();

        RatePerDay = decimal.Parse(reader.ReadElementString(nameof(RatePerDay)), CultureInfo.InvariantCulture);
        Days = int.Parse(reader.ReadElementString(nameof(Days)));
        PenaltyRatePerDay = decimal.Parse(reader.ReadElementString(nameof(PenaltyRatePerDay)), CultureInfo.InvariantCulture);
        OverdueDays = int.Parse(reader.ReadElementString(nameof(OverdueDays)));

        reader.ReadEndElement();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание тестового объекта
        var bill = new PaymentBill
        {
            RatePerDay = 100.50m,
            Days = 5,
            PenaltyRatePerDay = 15.20m,
            OverdueDays = 2
        };

        PaymentBill.SerializeCalculatedFields = false;
        SaveToFile("bill_without_calc.xml", bill);

        PaymentBill.SerializeCalculatedFields = true;
        SaveToFile("bill_with_calc.xml", bill);

        Console.WriteLine("--- Восстанавливаем объект из файла 'bill_without_calc.xml' ---");
        var restoredBill = LoadFromFile("bill_without_calc.xml");
        Console.WriteLine(restoredBill);

        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void SaveToFile(string fileName, PaymentBill obj)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PaymentBill));
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(fs, obj);
        }
        Console.WriteLine($"Сохранён файл: {fileName}");
    }

    static PaymentBill LoadFromFile(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PaymentBill));
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            return (PaymentBill)serializer.Deserialize(fs);
        }
    }
}