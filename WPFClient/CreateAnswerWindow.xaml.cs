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

namespace WPF
{
    /// <summary>
    /// Interaction logic for CreateAnswerWindow.xaml
    /// </summary>
    public partial class CreateAnswerWindow : Window
    {
        public CreateAnswerWindow()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void Create()
        {
            AnswerReference.Answer answer = new AnswerReference.Answer();
            answer.description = descriptionTextBox.Text;
            answer.isCorrect = Convert.ToBoolean(isCorrectTextBox.Text);
            answer.pointAmount = Convert.ToInt32(pointAmountTextBox.Text);
            MessageBoxResult result = MessageBox.Show("Do you want to create more users?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                ClearTextboxes(this);
            }
            else if (result == MessageBoxResult.No)
            {
                Close();
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
