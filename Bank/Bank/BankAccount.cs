using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

public class Account
{
    public int Number;
    public string name;
    public float Sum;
}
public class BankAccount
{
    static Dictionary<int, Account> account = new Dictionary<int, Account>();// для создание аккаунта
    static Account currentUser;//нынешний пользователь
    static Account recipient;//кому переводим

    public void Menu() // меню выбора взаимодействия
    {
        while (true)
        {
            Console.WriteLine("|Сделайте выбор|");
            Console.WriteLine("1 - Создать пользователя");
            Console.WriteLine("2 - Переключиться на пользователя по номеру счета");
            Console.WriteLine("3 - Завершить операции");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    Action();
                    break;
                case 3:
                    Console.WriteLine("|Информация по последнему выбранному вами счету|");        
                    Out();
                    return;
                default:
                    Console.WriteLine("\n|!ОШИБКА!|\n");
                    break;
            }
        }
    }
    public static void CreateUser()// Создаем аккаунт
    {
        Console.WriteLine("Введите номер счета:");
        int number = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите ФИО пользователя:");
        string name = Console.ReadLine();
        Console.WriteLine("Введите сумму на счету:");
        float sum = float.Parse(Console.ReadLine());

        Account user = new Account()
        {
            Number = number,
            name = name,
            Sum = sum
        };
        account[number] = user;
        Console.WriteLine("\nНовый аккаунт создан.\n");
    }
    public static void Action() // Выбираем действие для аккаунта
    {
        Console.WriteLine("Введите номер счета аккаунта:");
        int number = Convert.ToInt32(Console.ReadLine()); // Номер счета

        if (account.ContainsKey(number))
        {
            currentUser = account[number];
            Console.WriteLine($"Успешно переключено на аккаунт с номером счета: {number}\n");
            while (true)
            {
                // Меню выбора
                Console.WriteLine("\nМеню аккаунта:" +
                                  "\nВведите - 1 для вывода информации о текущем счете" +
                                  "\nВведите - 2 чтобы пополнить счёт" +
                                  "\nВведите - 3 чтобы снять деньги со счёта" +
                                  "\nВведите - 4 чтобы снять все деньги со счёта" +
                                  "\nВведите - 5 чтобы сделать перевод на другой счёт" +
                                  "\nВведите - 6 чтобы попасть в главное меню");
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        Out();
                        break;
                    case 2:
                        Dob();
                        break;
                    case 3:
                        Umen();
                        break;
                    case 4:
                        Obnul();
                        break;
                    case 5:
                        Perevod();
                        break;
                    case 6:
                        return;
                }
            }
        }
        else
        {
            Console.WriteLine($"|Аккаунта с номером счета {number} не существует|");
        }
    }
    public static void Out() // Ввод информации
    {
        if (currentUser != null) // currentUser - текущий пользователь
        {
            Console.WriteLine("\n ---------------------------------------");
            Console.WriteLine("|Информация по счету");
            Console.WriteLine($"|Номер счета: {currentUser.Number}");
            Console.WriteLine($"|ФИО владельца счета: {currentUser.name}");
            Console.WriteLine($"|Сумму на счету: {currentUser.Sum} рублей");
            Console.WriteLine(" ---------------------------------------");
        }
        else
        {
            Console.WriteLine("Аккаунт не выбран.");
        }
    }
    public static void Dob()// вносим деньги
    {
        Console.WriteLine($"Сколько хотите положить на счёт? На счету: {currentUser.Sum} рублей");
        float sumdob = float.Parse(Console.ReadLine());// вносимые деньги

        currentUser.Sum += sumdob;// прибавка к существующим
        Console.WriteLine($"Вы внесли: {sumdob} рублей\n");
        Console.WriteLine($"\nНа счету: {currentUser.Sum} рублей\n");
    }
    public static void Umen()// Снимаем деньги
    {
        Console.WriteLine($"Сколько хотите снять со счёта? На счету: {currentUser.Sum} рублей\n");
        float sumUmen = float.Parse(Console.ReadLine());//  снимаемые со счета деньги

        if (currentUser.Sum > sumUmen)// проверяем хватает ли на нашем счету денег для снятия | currentUser.Sum - сумма на счету
        {
            currentUser.Sum -= sumUmen;
            Console.WriteLine($"Вы сняли: {sumUmen} рублей\n");
            Console.WriteLine($"На счету: {currentUser.Sum} рублей\n");
        }
        else if (currentUser.Sum < sumUmen)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("\nНА ВАШЕМ СЧЕТУ НЕ ДОСТАТОЧНО СРЕДСТВ ДЛЯ ЭТОЙ ОПЕРАЦИИ\n");
        }
        else if (currentUser.Sum == sumUmen)
        {
            Obnul();
        }
    }
    public static void Obnul() // забираем со счета все имеющиеся деньги
    {
        Console.WriteLine($"Вы сняли {currentUser.Sum} рублей");
        currentUser.Sum -= currentUser.Sum;
        Console.WriteLine($"|ВНИМАНИЕ! Вы сняли со счета все деньги, на счету осталось: {currentUser.Sum} рублей|");
    }
    public static void Perevod()// Совершаем перевод денег между двумя счетами
    {
        Console.WriteLine("Введите номер счёта для перевода:");
        int accountNumber = Convert.ToInt32(Console.ReadLine());

        if (account.ContainsKey(accountNumber))
        {
            Console.WriteLine("Введите сумму для перевода:");
            float sumPerevod = float.Parse(Console.ReadLine()); // sumPerevod - деньги для перевода
            if (sumPerevod <= currentUser.Sum)
            {
                recipient = account[accountNumber];
                currentUser.Sum -= sumPerevod; // посе перевода у нынешнего аккаунта остается
                recipient.Sum += sumPerevod; // прибавление денег к счету другого пользователя

                Console.WriteLine("Перевод выполнен успешно.");
                Console.WriteLine($"Сумма {sumPerevod} рублей переведена на счет аккаунта с номером {recipient.Number}");
                Console.WriteLine($"На счету осталось: {currentUser.Sum} рублей");
            }
            else
            {
                Console.WriteLine("\nНА ВЫБРАННОМ СЧЕТУ НЕ ДОСТАТОЧНО СРЕДСТВ ДЛЯ ЭТОЙ ОПЕРАЦИИ\n");
            }
        }
        else
        {
            Console.WriteLine($"Аккаунта с номером счета {accountNumber} не существует");
        }
    }
}
