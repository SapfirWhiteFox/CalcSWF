using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data;
using User.Extensions;

namespace User.Workers
{
    class UserWorker
    {
        private const string End = "end";
        
        public List<UserData> ListOfData { get; set; } = new List<UserData>();
        //public List<string> ListOfFiles { get; set; } = new List<string>();

        public UserWorker()
        {
            //GetExistingFiles();
            GetUserData();
            WaitUserAction();
        }

        //private void showdatafromfilesifexist()
        //{
        //    if (!listoffiles.any())
        //    {
        //        return;
        //    }
        //    listoffiles.foreach(x =>
        //    {
        //        var lines = file.readalllines(x).tolist();
        //        lines.foreach(console.writeline);
        //    });
        //}

        //private void GetExistingFiles()
        //{
        //    ListOfFiles = new List<string>();
        //    ShowDataFromFilesIfExist();
        //}

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
            var input = Console.ReadLine();

            return new List<int>();
        }

    }
}
  