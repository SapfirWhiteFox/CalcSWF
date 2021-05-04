using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data;

namespace Files.Workers
{
    public class FileWorker
    {
        public string PathToUsers { get; } = @"C:\GitHubRep\SWFCode\Users";       
        public List<string> ListOfFiles { get; set; } = new List<string>();
        public FileWorker(string pathToFiles)
        {
            if (string.IsNullOrWhiteSpace(pathToFiles))
            {
                throw new ArgumentNullException(nameof(pathToFiles));
            }
            PathToUsers = pathToFiles;
        }

        public List<UserData> ShowDataFromFilesIfExist()
        {
            var data = new List<UserData>();  
            try
            {                              
                var files =GetExistingFiles();

                foreach (var nameOfFile in files)
                {
                    string[] user = File.ReadAllLines($"{nameOfFile}");
                    DateTime.TryParse(user[3], out var dateBirth);
                    ulong.TryParse(user[4], out var salary);
                    data.Add(new UserData
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
            return data;
        }

        public List<string> GetExistingFiles()
        {
            var files = new List<string>();
            var exsistUsers = Directory.GetFiles(PathToUsers).ToList();
            if (!exsistUsers.Any())
            {
                Console.WriteLine("В замке пусто, мой Лорд...");
                return files;
            }
            foreach (var userFile in exsistUsers)
            {
                files.Add(userFile);
            }
            return files;

        }
       
        public void DoSaveAction(List<UserData> data)
        {
            try
            {
                foreach (var user in data)
                {
                    var fileName = $"User.{user.FirstName}.{user.Name}.{user.LastName}.sav";
                    string[] userData = { user.FirstName, user.Name, user.LastName, user.DateBirth.ToString("dd-MM-yyyy"), user.Salary.ToString() };
                    var pathToFile = $"{PathToUsers}\\{fileName}";
                    File.WriteAllLines(pathToFile, userData, Encoding.Unicode);

                }
                return;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }

    }
}
