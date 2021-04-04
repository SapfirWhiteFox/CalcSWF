using System;
using System.Globalization;

namespace CalcSWF
{
    class Program
    {    
        private static double Result { get; set; } = 0;
        private static double Number { get; set; } = 0;        
        private const string End = "end";
        private const string Reset = "reset";

        private static void InputInt()
        {            
           Console.WriteLine("Введите число");
           string input = Console.ReadLine();
            if (!double.TryParse(input, out double numberTry))
            {
                Console.WriteLine("Вы ввели не число,пожалуйста попробуйте еще раз");
                InputInt();
            }
            Console.WriteLine(numberTry);
            Number = numberTry;
            return;
        }
        private static void InputString()
        {
            Console.WriteLine("Введите знаки действия \"+\", \"-\", \"*\", \"/\".Или \"end\" что бы закончить.Или \"reset\"  что бы начать сначала.");
            var input = Console.ReadLine();
            if (string.Equals(input, End, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            if (string.Equals(input, Reset, StringComparison.CurrentCultureIgnoreCase))
            {
                Result=0;
                InputInt();
                Result += Number;
                InputString();
            }

            switch (input)
                {
                    case "+":
                        InputInt();
                        Result += Number;
                        Console.WriteLine($"Result = {Result.ToString()}");
                    break;

                    case "-":
                        InputInt();
                        Result -= Number;
                        Console.WriteLine($"Result = {Result.ToString()}");
                    break;

                    case "*":
                        InputInt();
                        Result *= Number;
                        Console.WriteLine($"Result = {Result.ToString()}");
                    break;

                    case "/":
                        InputInt();                      
                        Result /= Number;
                        Console.WriteLine($"Result = {Result.ToString()}");
                    break;
                    default:
                        Console.WriteLine("простите, вы ввели не знак действия, введите знаки действия \"+\", \"-\", \"*\", \"/\".Или \"end\" что бы закончить.Или \"reset\"  что бы начать сначала." +
                        "\n(пожалуйста вводите без кавычек (\"\"))"); 
                    break;
            }           
            InputString();
        }           

        static void Main(string[] args)
        {
            InputInt();
            Result += Number;
            InputString();           
            Console.ReadKey();
        }
    }
}
