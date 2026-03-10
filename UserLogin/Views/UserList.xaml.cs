using System;
using System.Collections.ObjectModel;
using System.Windows;
using UserLogin.Controllers;
using UserLogin.Models;

namespace UserLogin.Views
{
    /// <summary>
    /// Interakční logika pro UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        private User currentUser;

        private DataHandler handler;

        public ObservableCollection<User> UsersList {  get;}

        public Visibility AdminVisibility =>
            currentUser is Admin ? Visibility.Visible : Visibility.Collapsed;

        public UserList(User user, DataHandler handler)
        {
            InitializeComponent();

            this.currentUser = user;
            this.handler = handler;

            UsersList = handler.Users;

            DataContext = this;

            WelcomeMsg.Text = $"Vítej {user.Name}";
        }
        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser(handler);
            addUser.ShowDialog();


        }
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is User user)
            {
                handler.RemoveUser(user);
            }
        }

        private void ModUser(object sender, RoutedEventArgs e)  // change user's passwd
        {
            if (UsersListBox.SelectedItem is User user) 
            {;
                string newPasswd = Microsoft.VisualBasic.Interaction.InputBox("nové heslo: ");

                if (!string.IsNullOrEmpty(newPasswd)) 
                {
                    handler.ResetPass(user, Controllers.PasswordHelper.Hash(newPasswd));
                }
            }
        }
        private void ChangeYourPasswd(object sender, RoutedEventArgs e) // change current user passwd
        {
            string newPasswd = Microsoft.VisualBasic.Interaction.InputBox("nové heslo: ");

            if (!string.IsNullOrEmpty(newPasswd))
            {
                handler.ResetPass(currentUser, Controllers.PasswordHelper.Hash(newPasswd));
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            handler.Export();
        }
    }
}
