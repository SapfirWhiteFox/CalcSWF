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
        private static string TryFirstName { get; set; } = "";
        private static string TryName { get; set; } = "";
        private static string TryLastName { get; set; } = "";
        private static DateTime TryDateBirth { get; set; }
        private static ulong TrySalary { get; set; } = 0;


        private static void TryInputData()
        {
            Console.WriteLine("введите через запятую свои - Фамилия Имя Отчество (дата рождения)ДД-ММ-ГГГГ Доход.\n Пример записи: \n Иванов Иван Иванович 01-01-2011 5000");
            var input = Console.ReadLine();
            var subs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            try
            {
                if (!DateTime.TryParse(subs[3], out DateTime tryDateBirth))
                {
                    Console.WriteLine("Вы ввели не дату подходящего формата ДД-ММ-ГГГГ. попробуйте еще раз ");
                    TryInputData();
                }
                else
                {
                    TryDateBirth = tryDateBirth;
                }

                if (subs[4].StartsWith("$"))
                {
                    subs[4] = subs[4].Substring(1);
                    if (!ulong.TryParse(subs[4], out ulong trySalary))
                    {
                        Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                        TryInputData();
                    }
                    TrySalary = trySalary * 65;
                }
                else
                {
                    if (subs[4].EndsWith("$"))
                    {
                        int EndIndex = subs[4].LastIndexOf("$");
                        subs[4] = subs[4].Substring(0, EndIndex);
                        if (!ulong.TryParse(subs[4], out ulong trySalary))
                        {
                            Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                            TryInputData();
                        }

                        TrySalary = trySalary * 65;
                    }
                    else
                    {
                        if (!ulong.TryParse(subs[4], out ulong trySalary))
                        {
                            Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                            TryInputData();
                        }
                        TrySalary = trySalary;
                    }
                }
                TryFirstName = subs[0];
                TryName = subs[1];
                TryLastName = subs[2];
                return;
            }
            catch 
            {
                Console.WriteLine("что то пошло не так, попробуйте еще раз");
                TryInputData();
            }
        }
        static void Main(string[] args)
        {
            TryInputData();
            Console.WriteLine("Фамилия: "+ TryFirstName
                +".\nИмя: " + TryName
                + ".\nОтчество: " + TryLastName
                + ".\nДата рождения: " + TryDateBirth
                + "\nДоход: " + TrySalary +"(Рублей)."
                );
            Console.ReadKey();
        }

    }
}
