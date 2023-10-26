using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

public class BankAccount
{
    private int nom;
    private string name;
    private float sum;
    public void Out(int nom, string name, float sum) // Ввод информации
    {
        this.nom = nom;
        this.name = name;
        this.sum = sum;
    }
    public void Info() // Вывод информации
    {
        Console.WriteLine("\n ---------------------------------------");
        Console.WriteLine("Информация по счету");
        Console.WriteLine($"|Номер счета: {nom}");
        Console.WriteLine($"|ФИО владельца счета: {name}");
        Console.WriteLine($"|Сумму на счету: {sum} рублей");
        Console.WriteLine(" ---------------------------------------");
    }
    public void Dob(float sumdob) // вносим деньги
    {
        sum += sumdob;
        Console.WriteLine($"\n|На счет {nom} было внесено: {sumdob} рублей|");
        Console.WriteLine($"|На счету: {nom} теперь {sum} рублей|");
    }
    public void Umen(float sumUmen) // Снимаем деньги
    {
        if (sumUmen < sum)// Снятие денег
        {
            sum -= sumUmen;
            Console.WriteLine($"\n|Со счета:{nom} было снято {sumUmen} рублей|");
            Console.WriteLine($"|На счету: {nom} теперь {sum} рублей|");
        }
        else if (sumUmen == sum) // Снятие вообще всех имеющихся денег
        {
            this.Obnul();
        }
        else
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("\nНА ВАШЕМ СЧЕТУ НЕ ДОСТАТОЧНО СРЕДСТВ ДЛЯ ЭТОЙ ОПЕРАЦИИ\n");
        }
    }
    public void Obnul() // забираем со счета все имеющиеся деньги
    {
        Console.WriteLine($"Вы сняли {sum} руб");
        sum = sum - sum;
        Console.WriteLine($"|ВНИМАНИЕ! Вы сняли со своего счета все деньги, на вашем счету осталось: {sum} рублей|");
    }
    public void Perevod(float sumPerevod, BankAccount recipient) // Совершаем перевод денег между двумя счетами
    {
        if (sumPerevod <= sum) // sumPerevod - деньги для перевода
        {
            sum -= sumPerevod; // отнимаем от общ.
            recipient.sum += sumPerevod;
            Console.WriteLine($"На счет: {recipient.nom} было переведено: {sumPerevod} рублей");
        }
        else
        {
            Console.WriteLine("Недостаточно средств на счете.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Объявляем два счета
        BankAccount account1 = new BankAccount();
        BankAccount account2 = new BankAccount();

        Console.WriteLine("Здравствуйте. Пожалуйста введите информацию по первому счёту \n(Номер счёта, Фамилия, Сумма на текщем счёте):");
        account1.Out(Convert.ToInt32(Console.ReadLine()), Convert.ToString(Console.ReadLine()), Convert.ToSingle(Console.ReadLine()));
        Console.WriteLine("\nЗдравствуйте. Пожалуйста введите информацию по второму счёту \n(Номер счёта, Фамилия, Сумма на текщем счёте):");
        account2.Out(Convert.ToInt32(Console.ReadLine()), Convert.ToString(Console.ReadLine()), Convert.ToSingle(Console.ReadLine()));

    beginning: // якорь (возваращает для выбора счета)
        Console.WriteLine("\nВыберите счет \nЧтобы выбрать первый счет нажмите - 1\n" +
                            "Чтобы выбрать второй счет нажмите - 2\n" +
                            "Чтобы завершить все операции введите - 3");
        int schet = Convert.ToInt32(Console.ReadLine());

        if (schet == 1)// - Первый счет
        {
            Console.WriteLine("Вы выбрали первый счет");
        start: // якорь (возваращает для выбора действия со счетом)
            account1.Info();
            Console.WriteLine("Чтобы положить деньги на счёт введите - 1\n" +
                              "Чтобы снять деньги со счёта введите - 2\n" +
                              "Чтобы перенести деньги на другой счёт введите - 3\n" +
                              "Чтобы вернуться в меню выбора счета введите - 4\n" +
                              "Чтобы завершить все операции введите - 5");

            int selec = Convert.ToInt32(Console.ReadLine());
            if (selec == 1)
            {
                Console.WriteLine($"Введите сумму которую желаете положить на счёт?");
                account1.Dob(Convert.ToSingle(Console.ReadLine()));
                goto start; // якорь (возваращает для выбора действия со счетом)
            }
            else if (selec == 2)
            {
                Console.WriteLine($"\nВведите сумму которую желаете снять со счёта?");
                account1.Umen(Convert.ToSingle(Console.ReadLine()));
                goto start; // якорь (возваращает для выбора действия со счетом)
            }
            else if (selec == 3)
            {
                Console.WriteLine($"\nВведите сумму которую желаете перевести на другой счет?");
                account1.Perevod(Convert.ToSingle(Console.ReadLine()), account2);
                goto start;// якорь (возваращает для выбора действия со счетом)
            }
            else if (selec == 4)
            {
                goto beginning;// якорь (возваращает для выбора счета)
            }
            else if (selec == 5)
            {
                goto complete; // якорь (Для завершения всех действий)
            }
        }
        else if (schet == 2)// - Второй счет
        {
            Console.WriteLine("Вы выбрали второй счет");
        start1:  // якорь (возваращает для выбора действия со счетом)
            account2.Info();
            Console.WriteLine("Чтобы положить деньги на счёт введите - 1\n" +
                              "Чтобы снять деньги со счёта введите - 2\n" +
                              "Чтобы перенести деньги на другой счёт введите - 3\n" +
                              "Чтобы вернуться в меню выбора счета введите - 4\n" +
                              "Чтобы завершить все операции введите - 5");

            int selec = Convert.ToInt32(Console.ReadLine());
            if (selec == 1)
            {
                Console.WriteLine($"Введите сумму которую желаете положить на счёт?");
                account2.Dob(Convert.ToSingle(Console.ReadLine()));
                goto start1;// якорь (возваращает для выбора действия со счетом)
            }
            else if (selec == 2)
            {
                Console.WriteLine($"\nВведите сумму которую желаете снять со счёта?");
                account2.Umen(Convert.ToSingle(Console.ReadLine()));
                goto start1;
            }
            else if (selec == 3)
            {
                Console.WriteLine($"\nВведите сумму которую желаете перевести на другой счет?");
                account2.Perevod(Convert.ToSingle(Console.ReadLine()), account1);
                goto start1;
            }
            else if (selec == 4)
            {
                goto beginning;// якорь (возваращает для выбора счета)
            }
            else if (selec == 5)
            {
                goto complete;// якорь (Для завершения всех действий)
            }
        }
        else if (schet == 3)
        {
            goto complete;// якорь (Для завершения всех действий)
        }

    complete:// якорь (Для завершения всех действий)

        Console.WriteLine($"\n|----------------------------------------|");
        Console.WriteLine($"|Информация по счетам после всех операций|");
        account1.Info();
        account2.Info();
        Console.WriteLine($"|----------------------------------------|");
    }
}