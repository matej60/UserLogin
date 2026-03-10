using System.Windows;
using UserLogin.Controllers;
using UserLogin.Models;

namespace UserLogin.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataHandler handler = new DataHandler();
        
        public MainWindow()
        {
            InitializeComponent();

            handler.Import();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            handler.Import();
            string username = Name.Text;
            string password = Password.Password;

            password = Controllers.PasswordHelper.Hash(password);

            User user = handler.Login(username, password);

            if (user == null)
            {
                MessageBox.Show("špatné jméno nebo heslo");
                return;
            }
            
            UserList userList = new UserList(user, handler);
            userList.Show();
            this.Close();

        }
    }
}