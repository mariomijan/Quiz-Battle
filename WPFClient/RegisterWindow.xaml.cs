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
using WPF.LoginReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for RegisterWIndow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        LoginServiceClient loginClient = new LoginServiceClient();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logWin = new LoginWindow();
            Login login = new Login();
            login.username = usernameTextbox.Text;
            login.password = passwordBox.Password;

            try
            {
                if (!string.IsNullOrEmpty(login.username) && !string.IsNullOrEmpty(login.password))
                {
                    if (loginClient.DoesUserExist(login))
                    {
                        loginClient.EncryptAdminPassword(login);
                        MessageBox.Show("Successfully Registered");
                    }
                    else
                    {
                        MessageBox.Show("A user with that username already exists");
                    }
                }
                else
                {
                    MessageBox.Show("Fill out all fields", "Fill out all fields");
                }
            }
            catch (Exception)
            {
                throw;
            }
            Close();
            logWin.Show();
        }
    }
}
