using System;
using System.Windows;
using System.Windows.Controls;
using TechnProcessMonitoring.BL.Controller;
using TechnProcessMonitoring.BL.Model;

namespace WpfTechnProcessMonitoring.Viev
{

    public partial class AuthorizationViev : Window
    {
        private UserController userController = new UserController();
        public AuthorizationViev()
        {
            InitializeComponent();    
        }


        private void Clear()
        {
            TextBoxLogin.Clear();
            TextBoxPassword.Clear();
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var login = TextBoxLogin.Text;
            var password = TextBoxPassword.Text;
            var curentUser = new User(login, password);

            if (userController.Load(curentUser))
            {
                MainViev mainViev = new MainViev();
                mainViev.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear();
            }
        }

        private void RegistryButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationViev registrationViev = new RegistrationViev();
            registrationViev.Show();
            Close();
        }
    }
}
