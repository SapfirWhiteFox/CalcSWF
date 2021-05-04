using System;
using System.IO;
using System.Collections.Generic;
using User.Data;
using User.Extensions;
using User.Workers;
using Files.Workers;


namespace StringChanger
{
    class Program

    {        
        
        static void Main(string[] args)
        {
           var fileWorker = new FileWorker(@"C:\GitHubRep\SWFCode\Users");
           var worker = new UserWorker(fileWorker);

            Console.WriteLine("Program is over.Press any key.");
            Console.ReadKey();           
        }
    }
}
