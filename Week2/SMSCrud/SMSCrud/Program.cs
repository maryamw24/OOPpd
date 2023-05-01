using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSCrud;
using System.IO;

namespace SMSCrud
{
    class Program
    {
        static void Main(string[] args)
        {
            string username;
            string password;
            string name;
            string phoneNumber;
            List<User> data = new List< User > ();
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
                    Console.WriteLine("Enter full name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter your phone number(11digits):");
                    phoneNumber = Console.ReadLine();
                    Console.WriteLine("Enter your UserName:");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    password = Console.ReadLine();
                    valid = IsValid(username, data);
                    if (valid == true)
                    {
                        AddUser(data, username, password, name, phoneNumber);
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
                        Console.WriteLine("Enter your UserName:");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter your password:");
                        password = Console.ReadLine();
                        type = SignIn(username, password, data);
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
        static bool IsValid(string username, List<User> data)
        {
            bool flag = true;
            for (int i = 0; i < data.Count; i++)
            {
                if (username == data[i].UserName)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        static void AddUser(List<User> data, string username, string password, string name, string phonenumber)
        {
            User info = new User();
            info.UserName = username;
            info.Password = password;
            info.Name = name;
            info.PhoneNumbers = phonenumber;
            data.Add(info);
        }
        static string SignIn(string Username, string Password, List<User> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (Username == Data[i].UserName && Password == Data[i] .Password)
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
        static void OwnerMenu( List<Service> Service)
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
            Service s = new Service();
            Console.WriteLine("Enter the service name:");
            s.Name = Console.ReadLine();
            Console.WriteLine("Enter the service type(hair/ makeup/ skin:");
            s.Type = Console.ReadLine();
            Console.WriteLine("Enter the service charges:");
            s.Price = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter a short discription of the Service:");
            s.Discription = Console.ReadLine();
            Service.Add(s);
            storeServiceData(Service);
            Console.ReadKey();
        }
        static void storeServiceData(List<Service> Service)
        {
            string path = "D:\\Bussiness Application\\SMSCrud\\service.txt";
            StreamWriter file = new StreamWriter(path);
            for (int i = 0; i < Service.Count; i++)
            {
                file.WriteLine(Service[i].Name + "," + Service[i].Type + "," + Service[i].Price + "," + Service[i].Discription) ;
            }
            file.Flush();
            file.Close();
        }
        static void LoadServiceData(List<Service> Service)
        {
            string path = "D:\\Bussiness Application\\SMSCrud\\service.txt";
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
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) +  "Service charges".PadRight(20)+ "Service Discription".PadRight(20) );
            for (int i = 0; i < Service.Count; i++)
            {
                if (Service[i].Type == "skin" || Service[i].Type == "Skin")
                {
                    Console.WriteLine(Service[i].Name.PadRight(20) + Service[i].Type.PadRight(20) + Service[i].Price  + Service[i].Discription.PadLeft(20) );
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("_____________________________Hair Services");
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) +  "Service charges".PadRight(20) + "Service Discription".PadLeft(20));
            for (int i = 0; i < Service.Count; i++)
            {
                if (Service[i].Type == "hair" || Service[i].Type == "Hair")
                {
                    Console.WriteLine(Service[i].Name.PadRight(20) + Service[i].Type.PadRight(20) + Service[i].Price + Service[i].Discription.PadLeft(20));
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("_____________________________Makeup Services");
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) + "Service charges".PadRight(20) + "Service Discription".PadRight(20));
            for (int i = 0; i < Service.Count; i++)
            {
                if (Service[i].Type == "makeup" || Service[i].Type == "Makeup")
                {
                    Console.WriteLine(Service[i].Name.PadRight(20) + Service[i].Type.PadRight(20) + Service[i].Price + Service[i].Discription.PadRight(20));
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
            if(choice == 1)
            {

            }
        }
        static void AsendingOrder(List<Service> Service)
        { 


        }
        static int CheckIndex(List<Service> Service,string serviceName)
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
            string path = "D:\\Bussiness Application\\SMSCrud\\users.txt"; 
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
            string path = "D:\\Bussiness Application\\SMSCrud\\users.txt";
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
