namespace MyTinkoff.CMD
{
    using MyTinkoff.BL;
    using MyTinkoff.BL.Controller;
    using MyTinkoff.BL.Model;
    using System;
    using System.Threading;

    /// <summary>
    /// Интерфейс приложения.
    /// </summary>
    public class Program
    {
        static Random random = new Random();
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Добро пожаловать в приложение Тинькофф");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nПожалуйста введите ваше имя,фамилию,отчество если\n" +
                                  "вы еще не зарегистрированы, то программа автоматические\n" +
                                  "зарегестрирует вас,если вы не достигли совершеннолетия\n" +
                                  "программа автоматические завершит свою реботу\n");
                
                Console.Write("Введите ваше имя: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string name = Console.ReadLine();
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("введите вашу фамилию: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string lastName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Введите ваше отчество: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string firstName = Console.ReadLine();

                var age = DateTimeCheck();

                Console.ForegroundColor = ConsoleColor.Cyan;
                var user = new User(name, lastName, firstName, age);
                var userController = new UserController(user);

                Console.ForegroundColor = ConsoleColor.Green;
                if (userController.Flag)
                {
                    Console.WriteLine("Вы успешно зарегистрировались!\n");
                }
                else
                {
                    Console.WriteLine("Вы успешно вошли в свой аккаунт\n");
                }

                var flag = true;
                while (flag)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Выберите операцию.\n" +
                                      "    Q-создать карточный аккаунт\n" +
                                      "    W-войти в карточный аккаунт\n" +
                                      "    E-выйти с аккаунта пользователя\n" +
                                      "    Z-завершить работу программы");
                    Console.Write("<");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    var key = Console.ReadKey();
                    Console.WriteLine("\n");
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    switch (key.Key)
                    {
                        case ConsoleKey.Q://Создать.
                            var Newitem = CreateOrDefault(1);
                            var Newcard = new Card(Newitem.N, Newitem.P, user);
                            var NewcardController = new CardController(Newcard);
                            NewcardController.Add();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Успешная регистрация");

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"   Запомните, ваш номер:{ Newcard.NumberCards}, пароль: { Newcard.Password}");

                            flag = NextOperation(NewcardController);
                            break;
                        //Следующая----------------------------------------
                        case ConsoleKey.W://Войти.
                            var item = CreateOrDefault(2);
                            var card2 = new Card(item.N, item.P, user);
                            var cardController = new CardController(card2);
                            bool yn = cardController.Enter();
                            if (yn)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Вы вошли в свой карточный аккаунт!");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                flag = NextOperation(cardController);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Такого карточного аккаунта не существует хах\n");
                            }
                            break;
                        //Следующая----------------------------------------
                        case ConsoleKey.E://Выйти с аккаунта.
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Вы успешно вышли со своего аккаунта пользователя!");
                            flag = false;
                            break;
                        //Следующая----------------------------------------
                        case ConsoleKey.Z://Завершить работу программу.
                            Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\nДосвиданья! йо-хо-хо-хо");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Дальнейшие действия после создания аккаунта карты.
        /// </summary>
        /// <param name="cardController"></param>
        /// <returns></returns>
        private static bool NextOperation(CardController cardController)
        {
            var flag = true;
            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nВыберите операцию");
                Console.WriteLine("    A-сделать перевод на другую карту\n" +
                                  "    E-выйти с аккаунта пользователя\n" +
                                  "    P-пополнить свой счет\n" +
                                  "    R-посмотреть данные\n" +
                                  "    Z-завершить работу программы");
                Console.Write("<");
                Console.ForegroundColor = ConsoleColor.Gray;
                var key = Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine();
                        var numberCardTranslation = NumberCheck();//НОМЕР КАРТЫ КУДА МЫ ХОТИМ СДЕЛАТЬ ПЕРЕВОД
                        var yn = cardController.Enter(numberCardTranslation);
                        if(yn)// есть карта куда надо сделать перевод
                        {
                            Console.Write($"\nВведите сумму которую хотите перевести на карту {cardController.CardTransition.User.Name}: ") ;
                            var theAmount = IntCheck();//та самая сумма

                            Console.ForegroundColor = ConsoleColor.Green;
                            if (cardController.MoneyTransition(theAmount)) Console.WriteLine($"Был осуществлен перевод на пользователя {cardController.CardTransition.User.Name}, на вашем счету осталось {cardController.Card.MoneyOTC}");
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Не достаточно средств на вашем счете");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Такой карты не существует");
                        }
                        break;
                    //Следующая----------------------------------------
                    case ConsoleKey.E:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nВы успешно вышли со своего аккаунта пользователя!");
                        flag = false;
                        break;
                    //Следующая----------------------------------------
                    case ConsoleKey.P:
                        Console.Write("\nНа сколько денег вы хотите пополнить свой счет: ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                        var moneyOTC = IntCheck();// число на которое мы хотим пополнить наш счет.
                        cardController.AddMoney(moneyOTC);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{cardController.Card.User.Name} ваш счет: {cardController.Card.MoneyOTC}");
                        break;
                    //Следующая----------------------------------------
                    case ConsoleKey.R:
                        Console.WriteLine(cardController);
                        break;
                    //Следующая----------------------------------------
                    case ConsoleKey.Z:
                        Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("\nДосвиданья! йо-хо-хо-хо");
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                        break;
                }
            }
            return flag;
        }

        /// <summary>
        /// Создать или войти в аккаунт карты.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static (string N, string P) CreateOrDefault(int a)
        {
            switch (a)
            {
                case 1:// Создать
                    string n = random.Next(1000,10000).ToString()+" "+random.Next(1000, 10000).ToString()+" "+random.Next(1000, 10000).ToString()+" "+ random.Next(1000, 10000).ToString();
                    string p = random.Next(10000000,100000000).ToString();
                    return (n, p);
                case 2:// Войти
                    var newn = NumberCheck();
                    var newp = PasswordCheck();
                    return(newn, newp);
            }
            return ("1", "1");
        }

        /// <summary>
        /// проверка на ########.
        /// </summary>
        /// <returns></returns>
        private static string PasswordCheck()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Введите пароль карты: ");
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                var r = Console.ReadLine();
                if(int.TryParse(r , out int result) && r.Length == 8) return r;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Не верный формат пароля,введите восьмезначное число(########): ");
            }
        }

        /// <summary>
        /// проверка #### #### #### ####.
        /// </summary>
        /// <returns></returns>
        private static string NumberCheck()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Введите номер карты: ");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                var r = Console.ReadLine();
                int count = 0;
                int count2 = 0;
                var count3 = 0;
                foreach(var item in r)
                {
                    if ((count != 4 && count != 14 && count != 9) || count == 0)
                        if (int.TryParse(item.ToString(), out int result)) count++;
                        else count3 = -1;
                    else
                        if(item.ToString() == " ")
                        {
                            count2++;
                            count++;
                        }
                        else count3 = -1;
                }
                if (count2 == 3 && count == 19 && count3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return r.ToString();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Не верный формат,введите строку вида - #### #### #### ####: ");
                }
            }
        }

        /// <summary>
        /// проверка на ##.##.####.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static DateTime DateTimeCheck()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Введите год вашего рождения: ");

            DateTime result;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                var age = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Red;
                if(DateTime.TryParse(age, out result))
                {
                    if (DateTime.Now.Year - result.Year < 18)
                        throw new Exception("вы не достигли совершеннолетие");
                    else if (DateTime.Now.Year - result.Year == 18)
                        if (result.Month > DateTime.Now.Month)
                            throw new Exception("вы не достигли совершеннолетие");
                        else if (result.Month == DateTime.Now.Month)
                            if (DateTime.Now.Day < result.Day)
                                throw new Exception("вы не достигли совершеннолетие");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return result;
                }
                Console.Write("Неверный формат данных,надо ЧЧ.ММ.ГГГГ,введите заново: ");
            }
        }

        /// <summary>
        /// проверка на число Int32.
        /// </summary>
        /// <returns></returns>
        private static int IntCheck()
        {
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if(result > 0)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Не верный формат данных, введите просто число: ");
            }
        }
    }
}