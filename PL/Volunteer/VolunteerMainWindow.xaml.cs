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

namespace PL.Volunteer
{
    /// <summary>
    /// Interaction logic for VolunteerMainWindow.xaml
    /// </summary>
    public partial class VolunteerMainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public VolunteerMainWindow(int Id)
        {
            CurrentVolunteer = s_bl.Volunteer.Read(Id);
            InitializeComponent();
        }

        public BO.Volunteer? CurrentVolunteer
        {
            get { return (BO.Volunteer)GetValue(CurrentVolunteerProperty); }
            set { SetValue(CurrentVolunteerProperty, value); }
        }

        public static readonly DependencyProperty CurrentVolunteerProperty =
        DependencyProperty.Register("CurrentVolunteer", typeof(BO.Volunteer), typeof(VolunteerMainWindow), new PropertyMetadata(null));

        public bool IsCallInProgress => CurrentVolunteer?.CallInProgress?.ToString() != null;
        public bool IsNotCallInProgress => CurrentVolunteer?.CallInProgress?.ToString() == null;

        public BO.TypeOfDistance TypeOfDistance { get; set; } = BO.TypeOfDistance.WALK;
        public void comboBoxTypeOfDistance_SelectionChanged(object sender, SelectionChangedEventArgs e)
              => CurrentVolunteer.TypeOfDistance = sender is ComboBox comboBox ? (BO.TypeOfDistance)comboBox.SelectedItem : BO.TypeOfDistance.WALK;
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                CurrentVolunteer.Password = passwordBox.Password; // Update the CurrentVolunteer.Password property
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Volunteer.Update(CurrentVolunteer.Id, CurrentVolunteer!);
                MessageBox.Show("Volunteer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CurrentVolunteer = s_bl.Volunteer.Read(CurrentVolunteer!.Id);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void queryVolunteerDetails()
        {
            int id = CurrentVolunteer!.Id;
            CurrentVolunteer = s_bl.Volunteer.Read(id);
        }

        private void VolunteerObserver()
                      => queryVolunteerDetails();

        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Volunteer.AddObserver(CurrentVolunteer!.Id, VolunteerObserver);
        

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(CurrentVolunteer!.Id, VolunteerObserver);

        private void btnUpdateCallCompletion_Click(object sender, RoutedEventArgs e)
        {
            try { 
            s_bl.Call.TreatmentCompletionUpdate(CurrentVolunteer!.Id, CurrentVolunteer.CallInProgress!.Id);
                MessageBox.Show("Call completion updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CurrentVolunteer.CallInProgress = null; // Clear the call in progress after completion
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnChooseCallForTreatment_Click(object sender, RoutedEventArgs e)
        {
            PL.Call.OpenCallsListWindow openCallsListWindow = new PL.Call.OpenCallsListWindow(CurrentVolunteer!.Id);
            openCallsListWindow.Show();
        }


        //  public void comboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //=> CurrentVolunteer.Role = sender is ComboBox comboBox ? (BO.Role)comboBox.SelectedItem : BO.Role.STANDARD;


    }
}
