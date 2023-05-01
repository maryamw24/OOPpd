using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSCrud
{
    class User
    {
        public string UserName;
        public string Password;
        public string Name;
        public string PhoneNumbers;
        public User(string UserName,string Password)
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
        }
            
    }
    class Service
    {
        public string Name;
        public string Type;
        public int Price;
        public string Discription;
        public Service(int Price)
            {
              
            }
    }
}
