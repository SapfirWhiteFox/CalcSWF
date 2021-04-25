using System;

namespace User.Data
{
   public class UserData
    {
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public ulong Salary { get; set; }

        public override string ToString()
        {
            return $" {FirstName} {Name} {LastName} родившийся {DateBirth.ToString("dd-MM-yyyy")} получает ЗП - {Salary} (RUB).";
        }

    }
}
