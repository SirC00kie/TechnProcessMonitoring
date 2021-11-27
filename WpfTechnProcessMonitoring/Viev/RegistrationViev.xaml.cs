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
        private UserController userController = new UserController();
        public RegistrationViev()
        {
            InitializeComponent();
        }

        private void RegistryButton_Click(object sender, RoutedEventArgs e)
        {
            var login = TextBoxLogin.Text;
            var password = TextBoxPassword.Text;
            var name = TextBoxName.Text;
            var secondName = TextBoxSecondName.Text;

            var newUser = new UserController(name, secondName, login, password);
            AuthorizationViev authorizationViev = new AuthorizationViev();
            authorizationViev.Show();
            Close();
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationViev authorizationViev = new AuthorizationViev();
            authorizationViev.Show();
            Close();
        }
    }
}
