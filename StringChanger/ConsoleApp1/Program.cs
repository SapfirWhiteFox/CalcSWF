using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data;

namespace StringChanger
{
    class Program

    {
        private const string End = "end";
        public static List<Data> GetUserData()
        {
            var userList = new List<Data>();
            while (true)
            {
                Console.WriteLine("Введите данные нового пользователя (Фамилия Имя Отчества ДатаРождения Доход) или END  для завершения ввода");
                var input = Console.ReadLine();
                if (string.Equals(input, End, StringComparison.CurrentCultureIgnoreCase))
                {
                    return userList;
                }
                var user = ConvertToData(input);
                if (user == null)
                {
                    continue;
                }
                userList.Add(user);

            }
            
        }

        public static Data ConvertToData(string lol)
        {
            ulong dollars = 0;
            try
            {
                var subs = lol.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
         
                if (subs.Count > 5)
                {
                    Console.WriteLine("простите, но вы ввели слишком много значений");
                    return null;
                }

                if (subs[4].StartsWith('$'))
                {
                    subs[4] = subs[4].Substring(1, subs[4].Length - 1);
                    if (ulong.TryParse(subs[4], out dollars))
                    {
                        dollars *= 65;
                    }
                }

                if (subs[4].EndsWith('$'))
                {
                    subs[4] = subs[4].Substring(0, subs[4].Length - 1);
                    if (ulong.TryParse(subs[4], out dollars))
                    {
                        dollars *= 65;
                    }
                }

                if (!DateTime.TryParse(subs[3], out var tryDateBirth)
                    || (dollars <= 0 && !ulong.TryParse(subs[4], out dollars)))
                {
                    Console.WriteLine("Ошибка ввода доходов и/или даты рождения");
                    return null;
                }
                return new Data
                {
                    FirstName = subs[0],
                    Name = subs[1],
                    LastName = subs[2],
                    DateBirth = tryDateBirth,
                    Salary = dollars
                };
            }
            catch (Exception ex) 
            {
                
                Console.WriteLine($"произошла ошибка - {ex}");
                return null;
            }
        }            

        static void Main(string[] args)
        {
           var list= GetUserData();
            int count = 0;
            list.ForEach(a => Console.WriteLine($"{count += 1}. {a.FirstName} {a.Name} {a.LastName} родившийся {a.DateBirth.ToString("dd-MM-yyyy")} получает ЗП - {a.Salary} (RUB).")) ;
            Console.ReadKey();

           
        }

    }
}
