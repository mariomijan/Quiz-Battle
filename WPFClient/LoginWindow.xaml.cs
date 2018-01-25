using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.LoginReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginServiceClient loginClient = new LoginServiceClient();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void Login()
        {
            Home home = new Home();
            Login login = new Login();

            login.username = usernameTextbox.Text;
            login.password = passwordBox.Password;
            if (!string.IsNullOrEmpty(login.username) && !string.IsNullOrEmpty(login.password))
            {
                try
                {
                    if (loginClient.DoesUserExist(login))
                    {
                        MessageBox.Show("Could not find the login in the system", "Failed attempt", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        var currentLogin = loginClient.DecryptPassword(login);
                        if (currentLogin != null)
                        {
                            MessageBox.Show("Successfully logged in", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            home.Show();
                            Close();
                        }
                    }
                }
                catch (FaultException)
                {
                    MessageBox.Show("Could not find the login in the system", "Failed attempt", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill out all fields", "Fill out all fields");
            }
        }

        private void Register()
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Show();
            Close();
        }

    }
}
