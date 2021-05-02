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
        private static string PathToUsers { get; } = @"C:\GitHubRep\SWFCode\Users";
        private const string End = "end";

        public List<UserData> ListOfData { get; set; } = new List<UserData>();
        public List<string> ListOfFiles { get; set; } = new List<string>();

        public UserWorker()
        {
            GetExistingFiles();
            GetUserData();
            WaitUserAction();
        }

        private void GetExistingFiles()
        {
            try
            {
               ShowDataFromFilesIfExist();           
               
                foreach (var nameOfFile in ListOfFiles)
                {
                    string[] user = File.ReadAllLines($"{nameOfFile}");
                    DateTime.TryParse(user[3], out var dateBirth);
                    ulong.TryParse(user[4], out var salary);
                    ListOfData.Add(new UserData
                    {
                        FirstName = user[0],
                        Name = user[1],
                        LastName = user[2],
                        DateBirth = dateBirth,
                        Salary = salary
                    });
                }
            }
            catch (Exception exGEF)
            {
                Console.WriteLine(exGEF);
            }

        }

        private void ShowDataFromFilesIfExist()
        {
            var exsistUsers = Directory.GetFiles(PathToUsers).ToList();
            if (!exsistUsers.Any())
            {
                Console.WriteLine("В замке пусто, мой Лорд...");
                return;
            }
            foreach(var userFile in exsistUsers)
            {
                ListOfFiles.Add(userFile);
            }
            

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
                    "\nend - что ыб закрыть программу" +
                    "\ndelete - что бы удалить пользователей" +
                    "\naddnew - добавить еще пользователей" +
                    "\nsave - сохранить текущие изменения");
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
                case "save":
                   DoSaveAction();
                    break;
                default:
                    Console.WriteLine("Undefined action! Please, try again or end program");
                    break;
            }
        }

        private void DoDeleteAction()//хуйня не работает
        {
            var listToDel = GetNumsToDelete();
            listToDel.ForEach(x => ListOfData.RemoveAt(x - 1));
        }

        private List<int> GetNumsToDelete()//ага эта тоже
        {
            Console.WriteLine("Введите номерa пользователeй, которыe вы хотите удалить");       
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
            }
            var failedMsg = string.Join(", ",failed);
            Console.WriteLine($"Не удалось удалить ползователей с номером :{failedMsg}");
            return success;
        }

        private void  DoSaveAction()
        {
            try
            {
                foreach (var  user in ListOfData)
                {
                    var fileName = $"User.{user.FirstName}.{user.Name}.{user.LastName}.sav";
                    string[] userData = { user.FirstName, user.Name, user.LastName, user.DateBirth.ToString("dd-MM-yyyy"), user.Salary.ToString()};          
                    var pathToFile = $"{PathToUsers}\\{fileName}";
                    File.WriteAllLines(pathToFile, userData, Encoding.Unicode) ;
                    
                }
                return;
             }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return ;
            }
        }       

    }
}
  