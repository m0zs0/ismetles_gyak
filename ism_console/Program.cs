using System;
using System.Collections.Generic;
using ism_core;

namespace ism_console
{
    class Program
    {
        public static List<User> users = new List<User>();
        public static UserService userService = new UserService(users);

        public static char separator = Config.CsvSeparator;

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

            CreateUser(userService);

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
