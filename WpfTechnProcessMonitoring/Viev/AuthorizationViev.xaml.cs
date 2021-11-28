using System;
using System.Windows;
using System.Windows.Controls;
using TechnProcessMonitoring.BL.Controller;
using TechnProcessMonitoring.BL.Model;

namespace WpfTechnProcessMonitoring.Viev
{

    public partial class AuthorizationViev : Window
    {
        private readonly UserController _userController = new UserController();
        public AuthorizationViev()
        {
            InitializeComponent();    
        }


        private void Clear()
        {
            TextBoxLogin.Clear();
            TextBoxPassword.Clear();
        }

        private bool Validation()
        {
            if (!string.IsNullOrEmpty(TextBoxLogin.Text)
                && !string.IsNullOrWhiteSpace(TextBoxLogin.Text)
                && !string.IsNullOrEmpty(TextBoxPassword.Text)
                && !string.IsNullOrWhiteSpace(TextBoxPassword.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var login = TextBoxLogin.Text;
            var password = TextBoxPassword.Text;
            var curentUser = new User(login, password);

            if (Validation())
            {
                if (_userController.Load(curentUser))
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
            
        }

        private void RegistryButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationViev registrationViev = new RegistrationViev();
            registrationViev.Show();
            Close();
        }
    }
}
