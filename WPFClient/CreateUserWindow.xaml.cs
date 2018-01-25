using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF.UserReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        private LoginServiceClient loginClient = new LoginServiceClient();

        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        public void Create()
        {
            try
            {
                // Valider input
                LoginReference.Login login = new LoginReference.Login();
                LoginReference.User user = new LoginReference.User();
                login.username = usernameTextbox.Text;
                login.password = passwordBox.Password;
                user.name = firstNameTextbox.Text;
                user.lastName = lastnameTextbox.Text;
                user.country = countryTextbox.Text;
                user.phone = phoneTextbox.Text;
                user.point = Convert.ToInt32(pointTextbox.Text);
                loginClient.AddGuestAndLoginEncryptPw(login, user);
                MessageBoxResult result = MessageBox.Show("Do you want to create more users?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if(result == MessageBoxResult.Yes)
                {
                    ClearTextboxes(this);
                    passwordBox.Clear();
                }
                else if(result == MessageBoxResult.No)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        public static void ClearTextboxes(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox)
                {
                    TextBox tb = (TextBox)visualChild;
                    tb.Clear();
                }
                ClearTextboxes(visualChild);
            }
        }

    }

}

