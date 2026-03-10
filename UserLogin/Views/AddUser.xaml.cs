using System;
using System.Windows;
using UserLogin.Controllers;
using UserLogin.Views;

namespace UserLogin.Views
{
    /// <summary>
    /// Interakční logika pro Window1.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        DataHandler handler;

        public AddUser(DataHandler handler)
        {
            InitializeComponent();

            this.handler = handler; 
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            string username = Name.Text;
            string password = Password.Password;


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) 
            {
                MessageBox.Show("vyplň všechna pole");
                return;
            }
            password = Controllers.PasswordHelper.Hash(password);
            handler.AddNewUser(username, password);

            Close();
            
        }
    }
}
