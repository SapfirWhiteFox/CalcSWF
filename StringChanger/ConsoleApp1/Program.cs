using System;
using System.IO;
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
           var worker = new UserWorker();         
                     
            Console.ReadKey();           
        }
    }
}
