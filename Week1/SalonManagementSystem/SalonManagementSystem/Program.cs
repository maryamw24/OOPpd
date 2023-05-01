using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SalonManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] UserNames = new string[100];
            string[] Names = new string[100];
            string[] Roles = new string[100];
            string[] PhoneNumbers = new string[100];
            string[] Passwords = new string[100];
            string[] ServiceNames = new string[100];
            int[] ServiceCharges = new int[100];
            string[] ServiceType = new string[100];
            int userCount = 0;
            int serviceCount = 0;
            string UserPath = "D:\\Bussiness Application\\SalonManagementSystem\\users.txt";
            string ServicePath = "D:\\Bussiness Application\\SalonManagementSystem\\services.txt";
            loadUserData(UserPath, ref userCount, UserNames, Passwords, Names, Roles, PhoneNumbers);
            LoadServiceData(ServicePath, ref serviceCount, ServiceNames, ServiceType, ServiceCharges);
            string username;
            string password;
            string role;
            string name;
            string phoneNumber;
            string choice = "0";
            while (choice != "3")
            {
                Console.Clear();
                choice = MainMenu();
                if (choice == "1")
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
                    while(true)
                    {
                        Console.WriteLine("Enter your role:");
                        role = Console.ReadLine();
                        if(role == "customer" || role == "Customer" || role == "owner" || role == "Owner")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter Correct role:");
                        }
                    }
                    valid = IsValid(username, userCount, UserNames, Passwords);
                    if(valid == true)
                    {
                        AddUser(username, password, role, name, phoneNumber, ref userCount, UserNames, Passwords, Roles, Names, PhoneNumbers);
                        Console.WriteLine("");
                        Console.WriteLine("You can signin now");
                        StoreUserData(UserPath, userCount, UserNames, Passwords,Roles, Names, PhoneNumbers);
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
                if(choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("________________________________________LOGIN SCREEN");
                    Console.WriteLine("");
                    string type;
                    Console.WriteLine("Enter your UserName:");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    password = Console.ReadLine();
                    type = SignIn(username, password, userCount, UserNames, Passwords, Roles, PhoneNumbers);
                    if(type == "-1")
                    {
                        Console.WriteLine("Incorrect userName or Password!");
                        Console.ReadKey();
                    }
                    if (type == "owner" || type == "Owner")
                    {
                        OwnerMenu(ServiceNames, ServiceType, ServiceCharges, serviceCount, ServicePath);
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Incorresct username or Password");
                        Console.ReadKey();
                    }
                }
            }

        }
        static string MainMenu()
        {
            Console.Clear();
            string choice;
            Console.WriteLine("1.SignUp");
            Console.WriteLine("2.SignIn");
            Console.WriteLine("3.Exit");
            Console.Write("Enter your choice:");
            choice = Console.ReadLine();
            return choice;
        }
        static void AddUser(string Username, string Password, string role, string name, string PhoneNumber,ref int userCount,
            string[] UserNames, string[] Passwords,string[] Roles, string[] Names, string[] PhoneNumbers)
        {
            UserNames[userCount] = Username;
            Passwords[userCount] = Password;
            Roles[userCount] = role;
            Names[userCount] = name;
            PhoneNumbers[userCount] = PhoneNumber;
            userCount++;
        }
        static void StoreUserData(string UserPath, int userCount, string[] UserNames, string[] Roles, string[] Passwords, string[] Names,string[] PhoneNumbers)
        {
            StreamWriter file = new StreamWriter(UserPath);
            for(int i = 0; i < userCount;i++)
            {
                file.WriteLine(UserNames[i] + "," + Passwords[i] + "," + Names[i] + "," + Roles[i] + "," + PhoneNumbers[i]);
            }
            file.Flush();
            file.Close();
        }
        static bool IsValid(string username, int userCount, string[] UserNames, string[] passwords)
        {
            bool flag = true;
            for(int i = 0; i < userCount;i++)
            {
                if(username == UserNames[i])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
       
        static string SignIn(string Username, string Password, int userCount,string[] UserNames, string[] Passwords, string[] Roles, string[] PhoneNumbers)
        {
            for(int i = 0; i < userCount; i++)
            {
                if(Username == UserNames[i] && Password == Passwords[i])
                {
                    return Roles[i];
                }
            }
            return "-1";
        }
       static void loadUserData(string UserPath,ref int userCount, string[] UserNames, string[] Passwords, string[] Names, string[] Roles, string[] PhoneNumbers)
       {
            if(File.Exists(UserPath))
            {
                StreamReader file = new StreamReader(UserPath);
                string record;
                while((record = file.ReadLine()) != null)
                {
                    UserNames[userCount] = parseData(record, 1);
                    Passwords[userCount] = parseData(record, 2);
                    Names[userCount] = parseData(record, 3);
                    Roles[userCount] = parseData(record, 5);
                    PhoneNumbers[userCount] = parseData(record, 4);
                    userCount++;
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("Unable to load data!");
            }
        }
        static string parseData(string record, int field)
        {
            int coma = 1;
            string item = "";
            for(int i = 0; i < record.Length;i++)
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
        static string PrintOwnerMenu()
        {
            string choice;
            Console.WriteLine("___________________________________OWNER MENU");
            Console.WriteLine("1.Add Service");
            Console.WriteLine("2.Update Service");
            Console.WriteLine("3.Delete Service");
            Console.WriteLine("4.All Service");
            Console.WriteLine("Enter your choice:");
            choice = Console.ReadLine();
            return choice;
        }
        static void OwnerMenu(string[] ServiceNames, string[] ServiceType, int[] ServiceCharges,int serviceCount, string ServicePath )
        {
            
            /*int index;*/
            string choice;
            bool admin = true;
            while(admin)
            {
                Console.Clear();
                choice = PrintOwnerMenu();
                if(choice == "1")
                {
                    AddService(ServiceNames, ServiceType, ServiceCharges, ref serviceCount, ServicePath);
                }
                else if(choice == "2")
                {
                    UpdateService(ServiceNames, ServiceType, ServiceCharges, serviceCount, ServicePath);
                }
                else if(choice == "3")
                {
                    DeleteService(ServiceNames, ServiceType, ServiceCharges,ref serviceCount, ServicePath);
                }
                else if (choice == "4")
                {
                    ViewServices(ServiceNames, ServiceType, ServiceCharges, serviceCount);
                }
             
            }
             
        }
        static void AddService(string[] ServiceNames, string[] ServiceType, int[] ServiceCharges, ref int serviceCount, string ServicePath)
        {
            Console.WriteLine("Enter the service name:");
            ServiceNames[serviceCount] = Console.ReadLine();
            Console.WriteLine("Enter the service type(hair/ makeup/ skin:");
            ServiceType[serviceCount] = Console.ReadLine();
            Console.WriteLine("Enter the service charges:");
            ServiceCharges[serviceCount] = int.Parse(Console.ReadLine());
            serviceCount++;
            storeServiceData(ServiceNames, ServiceType, ServiceCharges, serviceCount, ServicePath);
            Console.ReadKey();
        }
        static void storeServiceData(string[] ServiceNames, string[] ServiceType, int[] serviceCharges, int serviceCount, string ServicePath)
        {
            StreamWriter file = new StreamWriter(ServicePath);
            for(int i = 0; i < serviceCount; i++)
            {
                file.WriteLine(ServiceNames[i] + "," + ServiceType[i] + "," + serviceCharges[i]);
            }
            file.Flush();
            file.Close();
        }
        static void LoadServiceData(string ServicePath,ref int serviceCount, string[] ServiceNames, string[] ServiceType, int[] ServiceCharges)
        {
            if (File.Exists(ServicePath))
            {
                StreamReader file = new StreamReader(ServicePath);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    ServiceNames[serviceCount] = parseData(record, 1);
                    ServiceType[serviceCount] = parseData(record, 2);
                    ServiceCharges[serviceCount] = int.Parse(parseData(record, 3));
                    serviceCount++;
                }
                file.Close();
            }
            else
            {
                Console.WriteLine("Unable to load Service Data!");
            }
            Console.ReadKey();
        }
        static void UpdateService(string[] ServiceNames,string[] ServiceType, int[] ServiceCharges, int serviceCount, string ServicePath)
        {
            string ServiceName;
            Console.WriteLine("Enter the service name you want to update:");
            ServiceName = Console.ReadLine();
            int index = CheckIndex(ServiceName, ServiceNames, serviceCount);
            if (index != -1)
            { 
                Console.WriteLine("Enter the updated Price:");
                ServiceCharges[index] = int.Parse(Console.ReadLine());
                Console.WriteLine("Your price is being updated!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No such service found!");
                Console.ReadKey();
            }
            storeServiceData(ServiceNames, ServiceType, ServiceCharges, serviceCount, ServicePath);
        }
        static void DeleteService(string[] ServiceNames, string[] ServiceType, int[] ServiceCharges, ref int serviceCount, string ServicePath)
        {
            string serviceName;
            Console.WriteLine("Enter name of the service you wan to delete:");
            serviceName = Console.ReadLine();
            int index = CheckIndex(serviceName, ServiceNames, serviceCount);
            if(index != -1)
            {
                RemoveFromArray(index,ref serviceCount, ServicePath, ServiceNames, ServiceCharges, ServiceType);
                Console.WriteLine("Service deleted successfully!");
                Console.ReadKey();
            }
        }
        static void RemoveFromArray(int index, ref int serviceCount,string ServicePath, string[] ServiceNames, int[] ServiceCharges, string[] ServiceType)
        {
            for(int i = index ; i < serviceCount -1 ;i++)
            {
                ServiceNames[i] = ServiceNames[i + 1];
                ServiceCharges[i] = ServiceCharges[i + 1];
                ServiceType[i] = ServiceType[i + 1];
            }
            serviceCount--;
            storeServiceData(ServiceNames, ServiceType, ServiceCharges, serviceCount, ServicePath);
        }
        static void ViewServices(string[] ServiceNames, string[] ServiceType, int[] ServiceCharges, int serviceCount)
        {
            Console.WriteLine("Service Name".PadRight(20) + "Service Type".PadRight(20) + "Service charges".PadRight(20));
            for(int i=0;i < serviceCount; i++)
            {
                Console.WriteLine(ServiceNames[i].PadRight(20) + ServiceType[i].PadRight(20) + ServiceCharges[i]);
            }
            Console.ReadKey();
        }
   
        
        static int CheckIndex(string serviceName,string[] ServiceNames, int serviceCount)
        {
            for(int i = 0; i <serviceCount ; i++)
            {
                if(ServiceNames[i] == serviceName)
                {
                    return i;
                }    -
            }
            return -1;

        }


    }
}
