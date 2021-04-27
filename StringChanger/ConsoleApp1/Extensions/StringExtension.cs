using System;
using System.Collections.Generic;
using System.Linq;
using User.Data;


namespace User.Extensions
{
    public static class StringExtension
    {
        public static UserData ConvertToData(this string input)
        {
            
            ulong dollars = 0;
            try
            {
                var subs = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

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
                                    || (dollars <= 0 && !ulong.TryParse(subs[4], out dollars)) 
                                    || tryDateBirth.Year > DateTime.Now.Year-14 
                                    || tryDateBirth.Year < DateTime.Now.Year -120)
                {
                    Console.WriteLine("Ошибка ввода доходов и/или даты рождения");
                    return null;
                }
                return new UserData
                {
                    FirstName = subs[0],
                    Name = subs[1],
                    LastName = subs[2],
                    DateBirth = tryDateBirth,
                    Salary = dollars
                };
            }
            catch (Exception ex1)
            {

                Console.WriteLine($"произошла ошибка - {ex1}");
                return null;
            }
        }
    }
}
