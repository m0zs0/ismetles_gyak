using System;
using System.Collections.Generic;
using ism_core;

namespace ism_console
{
    //taskkill /F /IM ism_console.exe
    class Program
    {
        public static List<User> users = new List<User>();
        public static UserService userService = new UserService(users);

        public static char separator = Config.CsvSeparator;

        static void ShowMenu() {
            Console.WriteLine("\n--- USER MENÜ ---");
            Console.WriteLine("1: Új felhasználó létrehozása");
            Console.WriteLine("2: Felhasználó keresése index alapján");
            Console.WriteLine("3: Felhasználó nevének frissítése index alapján");
            Console.WriteLine("4: Felahsználó törlése index alapján");
            Console.WriteLine("5: Felhasználók listázása");
            Console.WriteLine("6: Felhasználók fájlba írása");
            Console.WriteLine("0: Kilépés");
        }

        public static void CreateUser(UserService service) {
            Console.Write("Kérek egy nevet: ");
            string name = Console.ReadLine();
            Console.Write("Kérek egy jelszót: ");
            string password = Console.ReadLine();
            Console.Write("Kérek egy email címet: ");
            string email = Console.ReadLine();
            Console.Write("Kérek egy regisztrációs dátumot (yyyy-MM-dd): ");
            string regDate = Console.ReadLine();
            Console.Write("Kérek egy szintet (1-5): ");
            string levelStr = Console.ReadLine();
            try
            {
                User user = service.CreateUser(name, password, email, regDate, levelStr);
                Console.WriteLine(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Hiba: "+ex.Message);
                
            }
            
        }

        public static void ReadUser(UserService service)
        {
            Console.WriteLine("Kérem a user id-jét: ");
            int id = int.Parse(Console.ReadLine());

            User user = service.GetUserById(id);
            Console.WriteLine(user!= null?user:"Nincs ilyen user!");
        }

        static void UpdateUser(UserService service)
        {
            Console.Write("Módosítandó user Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Új név: ");
            string newName = Console.ReadLine();

            bool updated = service.UpdateUserName(id, newName);
            Console.WriteLine(updated?"Név frissítve.":"Nincs ilyen id-jű felhasználó.");
        }

        static void DeleteUser(UserService service) {
            Console.Write("Törlendő felhasználó Id: ");
            int id = int.Parse(Console.ReadLine());

            bool deleted = service.DeleteUserById(id);
            Console.WriteLine(deleted?"Felhasználó törölve.":"Nincs ilyen id-jű felhasználó.");
        }

        static void ListAllUsers(UserService service) {
            List<User> allUsers = service.GetAllUsers();
            if (allUsers.Count == 0)
            {
                Console.WriteLine("Nincs felhasználó.");
                return;
            }
            foreach (User user in allUsers)
            {
                Console.WriteLine(user);
                Console.WriteLine("--------------------");
            }
        }

        static void Main(string[] args)
        {
            // UI metódusok

            /*
            // új user hozzáadása "csv" stringből
            string csv = "2;mozso;jelszo123;mozso@mzsrk.hu;2025-09-15;1";
            try
            {
                User user = UserService.ParseFromCsv(csv, separator);
                users.Add(user);
            } catch (ArgumentException ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
                Environment.Exit(1);
            }
            */

            while(true) {
                ShowMenu();
                Console.Write("Kérem a menüpont számát: ");
                string choice = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine("\n");
                switch (choice)
                {
                    case "1": CreateUser(userService); break;
                    case "2": ReadUser(userService); break;
                    case "3": UpdateUser(userService); break;
                    case "4": DeleteUser(userService); break;
                    case "5": ListAllUsers(userService); break;
                    case "0": Console.WriteLine("Viszlát!"); Environment.Exit(0); break;
                    default: Console.WriteLine("Nincs ilyen menüpont!"); break;
                }
            }
            
            Console.ReadKey();

            /*User user = new User();
            try
            {
                user.Name = "Tibi";
                user.Password = "pass";
                user.Email = "tibi@moriczref.hu";
                user.RegistrationDate = DateTime.Parse("2025-09-08");
                user.Level = 3;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
                Environment.Exit(1);
            }
            Console.WriteLine(user);

            try
            {
                User user1 = new User(2, "mozso", "jelszo123", "mozso@mzsrk.hu", "2025-09-15", 1);
                Console.WriteLine(user1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
                Environment.Exit(1);
            }*/




        }
    }
}
