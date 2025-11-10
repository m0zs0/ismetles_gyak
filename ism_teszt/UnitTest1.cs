using ism_console;
using ism_core;

namespace ism_teszt
{
    public class UnitTest1
    {
        [Fact]
        public void CreateUser_ShouldAddUserToList()
        {
            //Arrange
            var users = new List<User>();
            UserService service = new UserService(users);

            //Act
            var user = service.CreateUser("Teszt Elek", "jelszo123", "teszt@example.com", DateTime.Now.ToString(), "3");

            //Assert
            Assert.Equal("Teszt Elek", user.Name);
            Assert.Equal("jelszo123", user.Password);
            Assert.Equal("teszt@example.com", user.Email);
            Assert.Equal(3, user.Level);
        }


        [Fact]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            //Arrange
            User user = new User(1, "Teszt Elek", "jelszo123", "teszt@example.com", DateTime.Now.ToString(), 3);
            var users = new List<User> { user };
            UserService service = new UserService(users);


            //Act
            var resultUser = service.GetUserById(1);

            //Assert
            Assert.NotNull(resultUser);
            Assert.Equal("Teszt Elek", resultUser.Name);
        }

        [Fact]
        public void UpdateUserName_ShouldChangeUserName()
        {
            //Arrange
            User user = new User(1, "Teszt Elek", "jelszo123", "teszt@example.com", DateTime.Now.ToString(), 3);
            var users = new List<User> { user };
            UserService service = new UserService(users);
            //Act
            bool result = service.UpdateUserName(1, "Sanyi");
            //Assert
            Assert.True(result);
            Assert.Equal("Sanyi", user.Name);
        }

        [Fact]
        public void UpdateUserName_NonExistentUser_ShouldReturnFalse()
        {
            //Arrange
            var users = new List<User>();
            UserService service = new UserService(users);
            //Act
            bool result = service.UpdateUserName(99, "Sanyi");
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteUserById_WhenUserExists_ShouldRemoveUser()
        {
            //Arrange
            User user = new User(1, "Teszt Elek", "jelszo123", "teszt@example.com", DateTime.Now.ToString(), 3);
            var users = new List<User> { user };
            UserService service = new UserService(users);
            //Act
            bool result = service.DeleteUserById(1);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void DeleteUserById_NonExistentUser_ShouldReturnFalse()
        {
            //Arrange
            var users = new List<User>();
            UserService service = new UserService(users);
            //Act
            bool result = service.DeleteUserById(99);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void ParseFromCsv_ShouldReturnValidUser() {
            //Arrange
            string csv = "1;Teszt Elek;jelszo123;teszt@example.com;2025-11-10 11:24:00;3";

            //Act
            User user = UserService.ParseFromCsv(csv, ';');

            //Assert
            Assert.Equal("Teszt Elek", user.Name);
            Assert.Equal("jelszo123", user.Password);
            Assert.Equal("teszt@example.com", user.Email);
            Assert.Equal(3, user.Level);

        }
    }
}
