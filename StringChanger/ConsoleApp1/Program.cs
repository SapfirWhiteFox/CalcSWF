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
        public static List<Data> GetUserData()
        {
            var userList = new List<Data>();
            while (true)
            { 
                var input = Console.ReadLine();
                if (input == "end")
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
            var subs = lol.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (subs[4].StartsWith("$"))
            {
                subs[4] = subs[4].Substring(1, subs[4].Length - 1);
                if (ulong.TryParse(subs[4], out dollars))
                {
                    dollars *= 65;
                }
            }

            if (subs[4].EndsWith("$"))
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

        static void Main(string[] args)
        {
           var list= GetUserData();
            list.ForEach(a=>Console.WriteLine(a.Salary));
            Console.ReadKey();

           
        }

    }
}
