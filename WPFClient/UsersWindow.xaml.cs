using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using WPF.CategoryReference;
using WPF.LoginReference;
using WPF.UserReference;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Default.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private UserServiceClient userClient = new UserServiceClient();
        private IList<UserReference.User> userList;

        public UsersWindow()
        {
            InitializeComponent();
            // DEN HER METODE SKAL KALDES TIL SIDST, OG I TILFÆLDE AF WCF ER NEDE SKAL DER LAVES ET TJEK
            LoadUserData();
        }

        private void findUserButton_Click(object sender, RoutedEventArgs e)
        {
            FindUser();
        }

        private void editUserButton_Click(object sender, RoutedEventArgs e)
        {
            EditUser();
        }

        private void removeUserButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveUser();
        }

        private void createUser_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
        }

        // HVIS WCF ER NEDE CRASHER PROGRAMMET PGA DEN IK KAN LOADE DB TING
        private void LoadUserData()
        {
            userList = new ObservableCollection<UserReference.User>(userClient.GetAllUsers());
            try
            {
                userDatagrid.ItemsSource = userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void CreateUser()
        {
            CreateUserWindow createUserWindow = new CreateUserWindow();
            createUserWindow.ShowDialog();
            RefreshDataGrid();
        }

        /// <summary>
        /// Refreshes the datagrid with data from DB
        /// </summary>
        public void RefreshDataGrid()
        {
            userList.Clear();
            userList = new ObservableCollection<UserReference.User>(userClient.GetAllUsers());
            userDatagrid.ItemsSource = null;
            userDatagrid.ItemsSource = userList;
        }

        /// <summary>
        /// Deletes a user on delete keypress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userDatagrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                try
                {
                    UserReference.User user = (UserReference.User)userDatagrid.SelectedItem;
                    var selectedRow = userDatagrid.SelectedIndex;
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete: {user.name}?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        userList.RemoveAt(selectedRow);
                        userClient.RemoveUser(user.id);
                    }
                    else
                    {
                        RefreshDataGrid();
                    }
                }
                catch (InvalidCastException invalidCast)
                {
                    MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes a user when clicking the remove button
        /// </summary>
        private void RemoveUser()
        {
            // Hvis man sletter tom row får man cast exception
            try
            {
                UserReference.User user = (UserReference.User)userDatagrid.SelectedItem;
                var selectedRow = userDatagrid.SelectedIndex;
                if (user != null)
                {
                    MessageBoxResult messageboxResult = MessageBox.Show($"Are you sure you want to delete {user.name} ?", "Are you sure?", MessageBoxButton.YesNo);
                    if (messageboxResult == MessageBoxResult.Yes)
                    {
                        userList.RemoveAt(selectedRow);
                        userClient.RemoveUser(user.id);
                    }
                }
            }
            catch (InvalidCastException invalidCast)
            {
                MessageBox.Show(invalidCast.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void FindUser()
        {
           // UserReference.User user = new UserReference.User();
            ObservableCollection<UserReference.User> userlist = (ObservableCollection<UserReference.User>)userDatagrid.ItemsSource;
            ObservableCollection<UserReference.User> foundUsers = new ObservableCollection<UserReference.User>();
            foreach (var user in userlist)
            {
                if (user.name.ToLower().Contains(searchTextBox.Text))
                {
                    foundUsers.Add(user);
                }
            }
            if(foundUsers.Count > 0)
            {
                userDatagrid.ItemsSource = foundUsers;
            }
            
        }

        private void EditUser()
        {
            UserReference.User user = (UserReference.User)userDatagrid.SelectedItem;
            var selectedRow = userDatagrid.SelectedIndex;
            if (user != null)
            {
                try
                { 
                    userList.ElementAt(selectedRow);
                    try
                    {
                        userClient.UpdateUser(user);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Data was changed by someone else, please refresh and try again !");
                    }
                    userDatagrid.Items.Refresh();
                    MessageBox.Show("Information updated", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            Close();
            home.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logwin = new LoginWindow();
            Close();
            logwin.Show();
        }
    }
}
