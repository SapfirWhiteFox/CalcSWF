using System;
using System.Linq;
using System.Globalization;
using System.Text;

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

 public static void Main(string[] args)
        {
            //GetUserData();
            //UseAndShowData();
            Console.WriteLine($"{FirstName} {Name} {LastName}, введите приветствие.");
            var input = Console.ReadLine();
            UserAdvert(input);                     
            Console.WriteLine("Program is over.Press any key.");
            Console.ReadKey();
        }

        public static void GetUserData()
        {
            Console.WriteLine("Введите данные нового пользователя (Фамилия Имя Отчество Пол(муж/жен) Возраст(от 14 до 120) ЛюбимаяКнига(если есть)))");
            while (true)
            {
                try 
                {                                                      
                    var inputUser = Console.ReadLine();                           
                    var subs = inputUser?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
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
            var pensionAgeMsg = $"До пенсии лет осталось - {beforePensionAge}.";
            if (beforePensionAge <= 0)
            {
                pensionAgeMsg = "Уже пенсионер.";
            }
            var bookMsg =$"Любимая книга пользователя - {Book}." ;
            if(Book==null)
            {
                bookMsg = "Любимая книга пользователя не указана";
            }
            double yearsLived = (double)Age / 1.20  ;
            var yearsLivedMsg = $"Прожито {yearsLived.ToString("N1")} % жизни.";
            Console.WriteLine($"Пользователь:{FirstName} {Name} {LastName}.\n{sexMsg}\nВозраст: {Age}.\n{yearsLivedMsg}\n{pensionAgeMsg}\n{bookMsg}");
        }

        public static void UserAdvert(string advert)
        {
            StringBuilder sb = new StringBuilder();     
            var index = advert.IndexOf("Сосиска", StringComparison.CurrentCultureIgnoreCase);
            if (index != -1)
            {
               
                        var leftSub = $"{advert.Substring(0, index)}ПОНЧИК";
                        var rightSub= advert.Substring(index + 7, advert.Length - (index + 7));
                        sb.Append($"{leftSub}{rightSub}");
                          
                UserAdvert(sb.ToString());
            } 
            else
            {
                sb.Append(advert);
            }
            Console.WriteLine(sb.ToString()); 
            return;         
        }        

       
      
    }
}
