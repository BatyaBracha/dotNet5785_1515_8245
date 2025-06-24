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

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Hide the password characters
                textBox.Text = new string('*', textBox.Text.Length);
                textBox.CaretIndex = textBox.Text.Length; // Move caret to the end
            }
        }

        private void TextBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonLoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            // Handle login logic here
            MessageBox.Show("Login button clicked!");
        }

        private void ButtonLoginAsVolunteer_Click(object sender, RoutedEventArgs e)
        {
            // Handle login logic here
            MessageBox.Show("Login button clicked!");
        }
    }
}
