using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechnProcessMonitoring.BL.Model;
using TechnProcessMonitoring.BL.Controller;

namespace WpfTechnProcessMonitoring.Viev
{
    /// <summary>
    /// Логика взаимодействия для RegistrationViev.xaml
    /// </summary>
    public partial class RegistrationViev : Window
    {
        private readonly UserController _userController = new UserController();
        public RegistrationViev()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            TextBoxLogin.Clear();
            TextBoxPassword.Clear();
            TextBoxName.Clear();
            TextBoxSecondName.Clear();
        }

        private bool Validation()
        {
            if (!string.IsNullOrEmpty(TextBoxLogin.Text)
                && !string.IsNullOrWhiteSpace(TextBoxLogin.Text) 
                && !string.IsNullOrEmpty(TextBoxPassword.Text)
                && !string.IsNullOrWhiteSpace(TextBoxPassword.Text)
                && !string.IsNullOrEmpty(TextBoxName.Text)
                && !string.IsNullOrWhiteSpace(TextBoxName.Text)
                && !string.IsNullOrEmpty(TextBoxSecondName.Text)
                && !string.IsNullOrWhiteSpace(TextBoxSecondName.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        private void RegistryButton_Click(object sender, RoutedEventArgs e)
        {
            var login = TextBoxLogin.Text;
            var password = TextBoxPassword.Text;
            var name = TextBoxName.Text;
            var secondName = TextBoxSecondName.Text;

            var newUser = new User(name, secondName, login, password);

            if (Validation())
            {
                if (_userController.Load(newUser))
                {
                    MessageBox.Show("Пользователь уже существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Clear();
                }
                else
                {
                    _userController.Save(newUser);
                    AuthorizationViev authorizationViev = new AuthorizationViev();
                    authorizationViev.Show();
                    Close();
                }
            }
            
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationViev authorizationViev = new AuthorizationViev();
            authorizationViev.Show();
            Close();
        }
    }
}
