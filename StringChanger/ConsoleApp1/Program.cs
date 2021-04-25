using System;
using System.Collections.Generic;
using User.Data;
using User.Extensions;
using User.Workers;

namespace StringChanger
{
    class Program

    {                  
        static void Main(string[] args)
        {
           var list= GetUserData();
           Count = 0;
            list.ForEach(a => Console.WriteLine($"{Count += 1}. {a}")) ;
            
            
            Console.ReadKey();

           
        }

    }
}
