using BO;
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
    /// Interaction logic for VolunteerWindow.xaml
    /// </summary>
    public partial class VolunteerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty ButtonTextProperty =
       DependencyProperty.Register("ButtonText", typeof(string), typeof(VolunteerWindow), new PropertyMetadata("Add"));



        public BO.Volunteer? CurrentVolunteer
        {
            get { return (BO.Volunteer)GetValue(CurrentVolunteerProperty); }
            set { SetValue(CurrentVolunteerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentVolunteer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentVolunteerProperty =
            DependencyProperty.Register("CurrentVolunteer", typeof(BO.Volunteer), typeof(VolunteerWindow), new PropertyMetadata(null));


        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public BO.Role Role { get; set; } = BO.Role.STANDARD;

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

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ButtonText == "Add")
                {
                    s_bl.Volunteer.Create(CurrentVolunteer!);
                    MessageBox.Show("Volunteer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ButtonText == "Update")
                {
                    s_bl.Volunteer.Update(CurrentVolunteer.Id, CurrentVolunteer!);
                    MessageBox.Show("Volunteer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentVolunteer = s_bl.Volunteer.Read(CurrentVolunteer!.Id);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ButtonText == "Add")
        //    {
        //        s_bl.Volunteer.Create(CurrentVolunteer);
        //        MessageBox.Show("Volunteer added successfully!");
        //    }
        //    else if (ButtonText == "Update")
        //    {
        //        s_bl.Volunteer.Update(CurrentVolunteer.Id,CurrentVolunteer);
        //        MessageBox.Show("Volunteer updated successfully!");
        //    }
        //}

        private void queryVolunteerDetails()
        {
            int id = CurrentVolunteer!.Id;
            CurrentVolunteer = s_bl.Volunteer.Read(id);
        }

        private void VolunteerObserver()
                      => queryVolunteerDetails();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (CurrentVolunteer!.Id != 0)
                s_bl.Volunteer.AddObserver(CurrentVolunteer!.Id, VolunteerObserver);
        }

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(CurrentVolunteer!.Id, VolunteerObserver);

        public VolunteerWindow(int Id = 0)
        {
            ButtonText = Id == 0 ? "Add" : "Update";
            CurrentVolunteer = (Id != 0) ? s_bl.Volunteer.Read(Id)!
           : new BO.Volunteer(0, "", "", "", "", null, null, null, BO.Role.STANDARD, true, null, BO.TypeOfDistance.WALK, 0, 0, 0, null);

            InitializeComponent();
        }
        public void comboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
      => CurrentVolunteer.Role = sender is ComboBox comboBox ? (BO.Role)comboBox.SelectedItem : BO.Role.STANDARD;

    }
}
