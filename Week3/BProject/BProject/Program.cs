using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BProject.BL;

namespace BProject
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<User> data = new List<User>();
            List<Service> service = new List<Service>();
            LoadUserData(data);
            LoadServiceData(service);
            while (true)
            {
                Console.Clear();
                int choice = MainMenu();
                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("___________________________________________SIGN UP SCREEN");
                    bool valid;
                    User info = SignUpInput();
                    valid = IsValid(info, data);
                    if (valid == true)
                    {
                        AddUser(data, info);
                        StoreUserData(data);
                        Console.WriteLine("You can Signin now!!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("UserName already exists!");
                        Console.WriteLine("Signup with another username");
                        Console.ReadKey();
                    }
                }
                if (choice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("________________________________________LOGIN SCREEN");
                    Console.WriteLine("");
                    string type;
                    User info = SignInInput();
                    type = SignIn(info, data);
                    if (type == "-1")
                    {
                        Console.WriteLine("Incorrect userName or Password!");
                        Console.ReadKey();
                    }
                    else
                    {
                        OwnerMenu(service);
                    }

                }
            }
        }
        static int MainMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("1.SignUp");
            Console.WriteLine("2.SignIn");
            Console.WriteLine("3.Exit");
            Console.Write("Enter your choice:");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static User SignUpInput()
        {
            Console.WriteLine("Enter full name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your phone number(11digits):");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter your UserName:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            if (name != null && phoneNumber != null && username != null && password != null)
            {
                User info = new User(username, password, name, phoneNumber);
                return info;
            }
            return null;

        }
        static User SignInInput()
        {
            Console.WriteLine("Enter your UserName:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            if(username != null && password != null)
            {
                User info = new User(username, password);
                return info;
            }
            return null;
        }
        static bool IsValid( User info, List<User> data)
        {
            bool flag = true;
            for (int i = 0; i < data.Count; i++)
            {
                if (info.UserName == data[i].UserName)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        static void AddUser(List<User> data, User info)
        {
            data.Add(info);
        }
        static string SignIn(User info, List<User> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (info.UserName == Data[i].UserName && info.Password == Data[i].Password)
                {
                    return "1";
                }
            }
            return "-1";
        }
        static string PrintOwnerMenu()
        {
            string choice;
            Console.WriteLine("___________________________________OWNER MENU");
            Console.WriteLine("1.Add Service");
            Console.WriteLine("2.Update Service");
            Console.WriteLine("3.Delete Service");
            Console.WriteLine("4.All Service");
            Console.WriteLine("5.Sort Services");
            Console.WriteLine("Enter your choice:");
            choice = Console.ReadLine();
            return choice;
        }
        static void OwnerMenu(List<Service> Service)
        {
            string choice;
            bool admin = true;
            while (admin)
            {
                Console.Clear();
                choice = PrintOwnerMenu();
                if (choice == "1")
                {
                    AddService(Service);
                }
                else if (choice == "2")
                {
                    UpdateService(Service);
                }
                else if (choice == "3")
                {
                    DeleteService(Service);
                }
                else if (choice == "4")
                {
                    ViewServices(Service);
                }

            }

        }
        static void AddService(List<Service> Service)
        {
            Console.WriteLine("Enter the service name:");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter the service type(hair/ makeup/ skin:");
            string Type = Console.ReadLine();
            Console.WriteLine("Enter the service charges:");
            int Price = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter a short discription of the Service:");
            string Discription = Console.ReadLine();
            Service s = new Service(Name, Type, Price, Discription);
            Service.Add(s);
            storeServiceData(Service);
            Console.ReadKey();
        }
        static void storeServiceData(List<Service> Service)
        {
            string path = "D:\\OOPpd\\Week3\\BProject\\services.txt";
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < Service.Count; i++)
            {
                file.WriteLine(Service[i].Name + "," + Service[i].Type + "," + Service[i].Price + "," + Service[i].Discription);
            }
            file.Flush();
            file.Close();
        }
        static void LoadServiceData(List<Service> Service)
        {
            string path = "D:\\OOPpd\\Week3\\BProject\\services.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    Service s = new Service();
                    s.Name = parseData(record, 1);
                    s.Type = parseData(record, 2);
                    s.Price = int.Parse(parseData(record, 3));
                    s.Discription = parseData(record, 4);
                    Service.Add(s);
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("Unable to load Service Data!");
            }

        }
        static void UpdateService(List<Service> service)
        {
            string ServiceName;
            Console.WriteLine("Enter the service name you want to update:");
            ServiceName = Console.ReadLine();
            int index = CheckIndex(service, ServiceName);
            if (index != -1)
            {
                Console.WriteLine("Enter the updated Price:");
                service[index].Price = int.Parse(Console.ReadLine());
                Console.WriteLine("Your price is being updated!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No such service found!");
                Console.ReadKey();
            }
            storeServiceData(service);
        }
        static void DeleteService(List<Service> service)
        {
            string serviceName;
            Console.WriteLine("Enter name of the service you wan to delete:");
            serviceName = Console.ReadLine();
            int index = CheckIndex(service, serviceName);
            if (index != -1)
            {
                service.RemoveAt(index);
                Console.WriteLine("Service deleted successfully!");
                Console.ReadKey();
                storeServiceData(service);
            }
            else
            {
                Console.WriteLine("no such Service available!");
                Console.ReadKey();
            }
        }
        static void ViewServices(List<Service> Service)
        {
            Console.WriteLine("_____________________________Skin Services");
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) + "Service charges".PadRight(20) + "Service Discription".PadRight(20));
            foreach (Service i in Service)
            {
                if (i.Type == "skin" || i.Type == "Skin")
                {
                    i.ViewServices();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("_____________________________Hair Services");
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) + "Service charges".PadRight(20) + "Service Discription".PadLeft(20));
            foreach (Service i in Service)
            {
                if (i.Type == "hair" || i.Type == "Hair")
                {
                    i.ViewServices();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("_____________________________Makeup Services");
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) + "Service charges".PadRight(20) + "Service Discription".PadRight(20));
            foreach (Service i in Service)
            {
                if (i.Type == "makeup" || i.Type == "Makeup")
                {
                    i.ViewServices();
                }
            }
            Console.ReadKey();
        }
        static void SortMenu(List<Service> Service)
        {
            int choice;
            Console.WriteLine("1.Price: High to low");
            Console.WriteLine("2.Price: Low to high");
            Console.WriteLine("Enter your choice:");
            choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {

            }
        }
       
        static int CheckIndex(List<Service> Service, string serviceName)
        {
            for (int i = 0; i < Service.Count; i++)
            {
                if (Service[i].Name == serviceName)
                {
                    return i;
                }
            }
            return -1;

        }
        static void StoreUserData(List<User> Data)
        {
            string path = "D:\\OOPpd\\Week3\\BProject\\Users.txt";
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < Data.Count; i++)
            {
                file.WriteLine(Data[i].UserName + "," + Data[i].Password + "," + Data[i].Name + "," + Data[i].PhoneNumbers);
            }
            file.Flush();
            file.Close();
        }
        static void LoadUserData(List<User> Data)
        {
            string path = "D:\\OOPpd\\Week3\\BProject\\Users.txt";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    User info = new User();
                    info.UserName = parseData(record, 1);
                    info.Password = parseData(record, 2);
                    info.Name = parseData(record, 3);
                    info.PhoneNumbers = parseData(record, 4);
                    Data.Add(info);
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("Unable to Load the File");
                Console.ReadKey();
            }
        }
        static string parseData(string record, int field)
        {
            int coma = 1;
            string item = "";
            for (int i = 0; i < record.Length; i++)
            {
                if (record[i] == ',')
                {
                    coma++;
                }
                else if (coma == field)
                {
                    item = item + record[i];
                }

            }
            return item;
        }

    }
}
    
