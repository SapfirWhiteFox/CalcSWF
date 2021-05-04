using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using User.Data;
using User.Extensions;
using Files.Workers;

namespace User.Workers
{
    class UserWorker
    {       
        private const string End = "end";

        public List<UserData> ListOfData { get; set; } = new List<UserData>();       
        
        public FileWorker FileWorker { get; }

        public UserWorker(FileWorker worker)

        {
            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }
            FileWorker = worker;
            ListOfData = FileWorker.ShowDataFromFilesIfExist();
            var cnt = 0;
            ListOfData.ForEach(a => Console.WriteLine($"{cnt += 1}. {a}"));
            GetUserData();
            WaitUserAction();
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
                    "\nend - что бы закрыть программу" +
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
                    FileWorker.DoSaveAction(ListOfData);
                    break;
                default:
                    Console.WriteLine("Undefined action! Please, try again or end program");
                    break;
            }
        }
            public void DoDeleteAction()//хуйня не работает
        {           
            var listToDel = GetNumsToDelete();
           
            foreach (var userNum in listToDel)
            {
                var fileName = $"User.{ListOfData[userNum - 1].FirstName}.{ListOfData[userNum - 1].Name}.{ListOfData[userNum - 1].LastName}.sav";
                Console.WriteLine(fileName);
                var filePath = $"{FileWorker.PathToUsers}\\{fileName}";
                Console.WriteLine(filePath);
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch { }

                }
                ListOfData.RemoveAt(userNum - 1);
            }          
        }

        public List<int> GetNumsToDelete()//ага эта тоже
        {
            Console.WriteLine("Введите номерa пользователeй, которыe вы хотите удалить");
            var input = Console.ReadLine();
            var listOfNumber = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var failed = new List<string>();
            var success = new List<int>();            

            foreach (var num in listOfNumber)
            {
                if (int.TryParse(num, out var delNum) && delNum > 0 && delNum <= ListOfData.Count())
                {
                    success.Add(delNum);
                    continue;
                }
                failed.Add(num);
            }
            var failedMsg = string.Join(", ", failed);
            Console.WriteLine($"Не удалось удалить ползователей с номером :{failedMsg}");
            return success.Distinct().OrderByDescending(x => x).ToList();
        }                 

    }
}
  