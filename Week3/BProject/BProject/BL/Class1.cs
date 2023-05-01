using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BProject.BL
{
    class User
    {
        public string UserName;
        public string Password;
        public string Name;
        public string PhoneNumbers;
        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public User(string UserName, string Password, string Name, string PhoneNumbers)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.Name = Name;
            this.PhoneNumbers = PhoneNumbers;
        }public User()
        {
           
        }
    }
    class Service
    {
        public string Name;
        public string Type;
        public int Price;
        public string Discription;
        public Service(string Name,string Type, int Price, string Discription)
        {
            this.Name = Name;
            this.Type = Type;
            this.Price = Price;
            this.Discription = Discription;
        }
        public Service()
        {

        }
        public void ViewServices()
        {
            Console.WriteLine(Name.PadRight(20) + Type.PadRight(20) + Price + Discription.PadLeft(20));
        }
    }
}
