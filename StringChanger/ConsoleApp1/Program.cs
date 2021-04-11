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

        private static Data A { get; set; } = new Data();
       


        private static void TryInputData()
        {
            Console.WriteLine("введите через запятую свои - Фамилия Имя Отчество (дата рождения)ДД-ММ-ГГГГ Доход.\n Пример записи: \n Иванов Иван Иванович 01-01-2011 5000");
            var input = Console.ReadLine();
            var subs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            try
            {
                if (!DateTime.TryParse(subs[3], out var tryDateBirth))
                {
                    Console.WriteLine("Вы ввели не дату подходящего формата ДД-ММ-ГГГГ. попробуйте еще раз ");
                    TryInputData();
                }
                else
                {
                    A.DateBirth = tryDateBirth;
                }

                if (subs[4].StartsWith("$"))
                {
                    subs[4] = subs[4].Substring(1);
                    if (!ulong.TryParse(subs[4], out var trySalary))
                    {
                        Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                        TryInputData();
                    }
                   A.Salary = trySalary * 65;
                }
                else
                {
                    if (subs[4].EndsWith("$"))
                    {
                        int EndIndex = subs[4].LastIndexOf("$");
                        subs[4] = subs[4].Substring(0, EndIndex);
                        if (!ulong.TryParse(subs[4], out var trySalary))
                        {
                            Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                            TryInputData();
                        }

                        A.Salary = trySalary * 65;
                    }
                    else
                    {
                        if (!ulong.TryParse(subs[4], out var trySalary))
                        {
                            Console.WriteLine("Вы ввели не доход в числовом формате, попробуйте еще раз.");
                            TryInputData();
                        }
                        A.Salary = trySalary;
                    }
                }
                A.FirstName = subs[0];
                A.Name = subs[1];
                A.LastName = subs[2];
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Простите, что то пошло не так( {ex.Message} ). Попробуйте еще раз");
                TryInputData();
            }
        }
        static void Main(string[] args)
        {
            TryInputData();
            Console.WriteLine($"Фамилия: {A.FirstName}.\nИмя: {A.Name}.\nОтчество: {A.LastName}.\nДата рождения: {A.DateBirth.ToString("dd,MM,yyyy")}.\nДоход: {A.Salary} (Рублей).");
            Console.ReadKey();
        }

    }
}
