using System;
using System.Linq;
using System.Globalization;

namespace PortfolioApp
{
    class Program
    {
        private const string End = "end";
        private const string SexM = "муж";
        private const string SexF = "жен";
        public static String FirstName { get; set; } = null;
        public static String Name { get; set; } = null;
        public static String LastName { get; set; } = null;
        public static int? Age { get; set; } = null;
        public static String Sex { get; set; } = null;
        public static String Book { get; set; } = null;



        public static void GetUserData()
        {
            Console.WriteLine("Введите данные нового пользователя (Фамилия Имя Отчество Пол(муж/ж) Возраст(от 14 до 120) ЛюбимаяКнига(если есть)))");
            while (true)
            {
                try 
                {
                    CultureInfo ruRu = new CultureInfo("ru-RU");                                     
                    var inputUser = Console.ReadLine();
                    if (string.Equals(inputUser, End, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }
                    if (inputUser == null)
                    {
                        Console.WriteLine("простите, но вы ничего не ввели.Введите пользователя заново.");
                        continue;
                    }

                    var subs = inputUser?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (subs.Count > 6)
                    {
                        Console.WriteLine("Простите, но вы ввели слишком много значений.Введите пользователя заново.");
                        continue;
                    }                  
                    if (subs.Count < 4)
                    {
                        Console.WriteLine("Простите, но вы ввели слишком мало значений.Введите пользователя заново.");
                        continue;
                    }
                    if (!int.TryParse(subs[4],out var tryAge)
                                         &&( tryAge >  120
                                         || tryAge < 14))
                    {
                        Console.WriteLine("Ошибка ввода возраста.Введите пользователя заново.");
                        continue;
                    }
                    if(!string.Equals(subs[3], SexM, StringComparison.CurrentCultureIgnoreCase)
                    && !string.Equals(subs[3], SexF, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("Ошибка ввода пола.Введите пользователя заново.");
                       continue;
                    }                  
                    FirstName = subs[0];
                    Name = subs[1];
                    LastName = subs[2];
                    Sex = subs[3];
                    Age = tryAge;
                    if (subs.Count > 5)
                    {
                        Book = subs[5]; 
                    }
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Простите,произошла ошибка.Введите пользователя заново.");
                    continue;
                }
             }
        }

        public static void UseAndShowData()
        {
            var sexMsg = "Пол - Мужской.";
            if (string.Equals(Sex, SexF, StringComparison.CurrentCultureIgnoreCase))

            {
                sexMsg = "Пол - Женский.";
            }
            var pensionAge = 70;
            if (string.Equals(Sex, SexF, StringComparison.CurrentCultureIgnoreCase))
                   
            {
                pensionAge = 65;
            }
            var beforePensionAge = -(Age - pensionAge) ;           
            var pensionAgeMsg = $"До пенсии осталось {beforePensionAge} лет.";
            if (beforePensionAge <= 0)
            {
                pensionAgeMsg = "Уже пенсионер.";
            }
            var bookMsg =$"Любимая книга пользователя -{Book}" ;
            if(Book==null)
            {
                bookMsg = "Любимая книга пользователя не указана";
            }

            double yearsLived = (double)Age / 1.20  ;
            var yearsLivedMsg = $"Прожито {yearsLived.ToString()} % жизни.";
            Console.WriteLine($"Пользователь:{FirstName} {Name} {LastName}.\n{sexMsg}\nВозраст: {Age}.\n{yearsLivedMsg}\n{pensionAgeMsg}\n{bookMsg}");

        }
        public static void GetUserAdvert()
        {

        }



        public static void Main(string[] args)
        {
            GetUserData();
            UseAndShowData();
            GetUserAdvert();           // пирожок делай через сабстринг. ВСПОМИНАЙ.

            Console.WriteLine("Program is over.Press any key.");
            Console.ReadKey();
        }
      
    }
}
