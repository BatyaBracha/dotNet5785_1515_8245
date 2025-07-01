using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Login : Window, INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(Login));

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password)); // אם את משתמשת ב־INotifyPropertyChanged
                }
            }
        }

        private void TextBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonLoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Volunteer.Login(Id, Password) == BO.Role.ADMINISTRATOR)
                {
                    // Navigate to Admin window
                    MainWindow adminWindow = new MainWindow();
                    adminWindow.Show();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed. Please check your credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonLoginAsVolunteer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Role role = s_bl.Volunteer.Login(Id, Password);
                if (role == BO.Role.STANDARD || role == BO.Role.ADMINISTRATOR)
                {
                    // Navigate to Volunteer window
                    Volunteer.VolunteerMainWindow volunteerWindow = new Volunteer.VolunteerMainWindow(Id,Password);
                    volunteerWindow.Show();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed. Please check your password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
