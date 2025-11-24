using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ism_core;

namespace ism_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users;
        private UserService userService;
        public MainWindow()
        {
            InitializeComponent();
            users = new ObservableCollection<User>();
            userService = new UserService(users.ToList());
            dgUsers.ItemsSource = users;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser = userService.CreateUser("Új név", "jelszó", "email@moriczref.hu", DateTime.Now.ToString(), "1");
            users.Add(newUser);
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show($"Biztosan törölni szeretnéd a(z) {selectedUser.Name} felhasználót?", 
                    "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (userService.DeleteUserById(selectedUser.Id))
                    {
                        users.Remove(selectedUser);
                    }
                    else
                    {
                        MessageBox.Show("A felhasználó törlése sikertelen volt.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.CsvFullPath;
            char separator = Config.CsvSeparator;
            userService.SaveUsersToCsvFile(path, separator);
            MessageBox.Show("Felhasználók mentése sikeres!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Config.CsvFullPath;
            char separator = Config.CsvSeparator;
            users.Clear();
            userService.LoadUsersFromCsvFile(path, separator);
            foreach (var user in userService.GetAllUsers())
            {
                users.Add(user);
            }

        }
    }
}