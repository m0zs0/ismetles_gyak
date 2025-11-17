using ism_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ism_teszt
{
    public class IntegrationTests: IDisposable
    {
        //Előkészítés
        private readonly string tempFilePath;
        private readonly char separator = ';';
        private List<User> users;
        private UserService service;

        public IntegrationTests()
        {
            // Create a temporary file for testing
            tempFilePath = Path.Combine(Path.GetTempPath(),$"users_test_{Guid.NewGuid()}.csv");
            users = new List<User>();
            service = new UserService(users);
        }

        //Tesztek
        [Fact]
        public void CreateUser_IntegrationTest()
        {
            var user = service.CreateUser("Teszt Elek", "pass123", "tesztelek@moriczref.hu", DateTime.Now.ToString(), "3");
            Assert.Single(users);
            Assert.Equal("Teszt Elek", user.Name);
        }

        [Fact]
        public void UpdateUserName_IntegrationTest()
        {
            var user = service.CreateUser("Teszt Elek", "pass123", "tesztelek@moriczref.hu", DateTime.Now.ToString(), "3");
            bool updateResult = service.UpdateUserName(user.Id, "Új Név");
            Assert.True(updateResult);
            Assert.Equal("Új Név", user.Name);
        }

        [Fact]
        public void DeleteUserById_IntegrationTest()
        {
            // új user létrehozása a users listához
            var user = service.CreateUser("Teszt Elek", "pass123", "tesztelek@moriczref.hu", DateTime.Now.ToString(), "3");
            // függvényhívás a user törléséhez
            bool deleteResult = service.DeleteUserById(user.Id);
            // ellenőrzés, hogy a törlés sikeres-e
            Assert.True(deleteResult);
            // ellenőrzés, hogy a users lista üres-e
            Assert.Empty(users);
        }

        [Fact]
        public void ExportUserToCsv_IntegrationTest()
        {
            // új user létrehozása a users listához
            var user = service.CreateUser("Teszt Elek", "pass123", "tesztelek@moriczref.hu", DateTime.Now.ToString(), "3");
            // users lista kiírása CSV fájlba (tempFilePath-re)
            service.SaveUsersToCsvFile(tempFilePath, separator);
            // Fájl létezésének ellenőrzése
            Assert.True(File.Exists(tempFilePath));
            // Fájl tartalmának beolvasása egy strintömbbe
            string[] lines = File.ReadAllLines(tempFilePath);
            // Ellenőrzés, hogy a fájl tartalma egy sor-e
            Assert.Single(lines);
            // Ellenőrzés, hogy a sorban van e "A létrehozott user neve" string
            Assert.Contains("Teszt Elek", lines[0]);
        }

        [Fact]
        public void LoadUsersFromCsv_IntegrationTest()
        {
            //csv sor létrehozása
            string csvLine = $"1{separator}Teszt Elek{separator}pass123{separator}tesztelek@moriczref.hu{separator}" +
                             $"{DateTime.Now:yyyy-MM-dd}{separator}3";
            //csv sor fájlba írása
            File.WriteAllText(tempFilePath, csvLine);
            //users lista beolvasása a fájlból
            service.LoadUsersFromCsvFile(tempFilePath, separator);
            //ellenőrzés, hogy a users listában van e egy elem
            Assert.Single(users);
            //ellenőrzés, hogy az első user neve megegyezik e a csv sorban lévő névvel
            Assert.Equal("Teszt Elek", users[0].Name);
        }

        //Takarítás
        public void Dispose()
        {
            // Cleanup resources if needed
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }

    }
}
