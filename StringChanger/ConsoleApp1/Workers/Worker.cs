using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using User.Data;
using User.Extensions;

namespace User.Workers
{
    class UserWorker
    {
        private static string PathToUser { get; } = @"C:\GitHubRep\SWFCode\User\";
        private const string End = "end";
        
        public List<UserData> ListOfData { get; set; } = new List<UserData>();
        public List<string> ListOfFiles { get; set; } = new List<string>();

        public UserWorker()
        {
            GetExistingFiles();
            GetUserData();
            WaitUserAction();
        }

        private void ShowDataFromFilesIfExist()
        {
            var exsistUsers = Directory.GetFiles(PathToUser).ToList();
            if (!exsistUsers.Any())
            {
                return;
            }
          
        }

        private void GetExistingFiles()
        {
            listoffiles = new list<string>();
            ShowDataFromFilesIfExist();
        }

        private void GetUserData()
        {
            while (true)
            {
                Console.WriteLine("Введите данные нового пользователя (Фамилия Имя Отчества ДатаРождения Доход) или END  для завершения ввода");
                var input = Console.ReadLine();
                if (string.Equals(input, End, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
                var user = input.ConvertToData();
                if (user == null)
                {
                    continue;
                }
                ListOfData.Add(user);
            }
              
        }

        private void WaitUserAction()
        {
            while (true)
            {
                var cnt = 0;
                ListOfData.ForEach(a => Console.WriteLine($"{cnt += 1}. {a}"));
                Console.WriteLine("Выберите следующее действие." +
                    "(end/delete/addnew)");
                var action = Console.ReadLine();
                if (string.Equals(action, End, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
                UserActionFabric(action);
            }
        }

        private void UserActionFabric(string actionName)
        {
            switch (actionName)
            {
                case "delete":
                    DoDeleteAction();
                    break;
                case "addnew":
                    GetUserData();
                    break;
                default:
                    Console.WriteLine("Undefined action! Please, try again or end program");
                    break;
            }
        }

        private void DoDeleteAction()
        {
            var listToDel = GetNumsToDelete();
            listToDel.ForEach(x => ListOfData.RemoveAt(x - 1));
        }

        private List<int> GetNumsToDelete()
        {
            Console.WriteLine("Введите номерa пользователeq, которыe вы хотите удалить");       
            var input = Console.ReadLine();
            var listOfNumber = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var failed = new List<string>();
            var success = new List<int>();

            foreach(var num in listOfNumber)
            {
                if (int.TryParse(num, out var delNum) && delNum > 0 && delNum < ListOfData.Count())
                {
                    success.Add(delNum);
                    continue;
                }
                failed.Add(num);
            }
            var failedMsg = string.Join(", ",failed);
            Console.WriteLine($"Не удалось удалить ползователей с номером :{failedMsg}");
            return success;
        }

    }
}
  