using System;
using System.Collections;
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
    /// Interaction logic for VolunteerListWindow.xaml
    /// </summary>
    /// <summary>
/// Interaction logic for the volunteer list window.
/// </summary>
public partial class VolunteerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public IEnumerable<BO.VolunteerInList> VolunteerList
        {
            get { return (IEnumerable<BO.VolunteerInList>)GetValue(VolunteerListProperty); }
            set { SetValue(VolunteerListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VolunteerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VolunteerListProperty =
            DependencyProperty.Register("VolunteerList", typeof(IEnumerable<BO.VolunteerInList>), typeof(VolunteerListWindow), new PropertyMetadata(null));

        public BO.VolunteerFields VolunteerFieldsFilter { get; set; } =BO.VolunteerFields.None;
        public BO.TypeOfCall? SelectedTypeOfCallFilter { get; set; }=BO.TypeOfCall.NONE;

        public void comboBoxVolunteerList_SelectionChanged(object sender, SelectionChangedEventArgs e) 
                      => VolunteerList = (VolunteerFieldsFilter == BO.VolunteerFields.None) ?
                                      s_bl?.Volunteer.ReadAll()! : s_bl?.Volunteer.ReadAll(null, VolunteerFieldsFilter)!;
        public void comboBoxTypeOfCallFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
                      => VolunteerList = (SelectedTypeOfCallFilter == null)
        ? s_bl?.Volunteer.ReadAll()!
        : s_bl?.Volunteer.ReadAll(null, null, SelectedTypeOfCallFilter)!;

        private void queryVolunteerList()
                      => VolunteerList = (VolunteerFieldsFilter == BO.VolunteerFields.None) ?
                                      s_bl?.Volunteer.ReadAll()! : s_bl?.Volunteer.ReadAll(null, VolunteerFieldsFilter)!;

        private volatile bool _observerWorking = false; //stage 7

        private void volunteerListObserver()
        {
            if (!_observerWorking)
            {
                _observerWorking = true;
                _ = Dispatcher.BeginInvoke(() =>
                {
                    queryVolunteerList();
                    _observerWorking = false;
                });
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Volunteer.AddObserver(volunteerListObserver);

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(volunteerListObserver);

        public BO.VolunteerInList? SelectedVolunteer { get; set; }

        // AddButton_Click event handler to open the VolunteerWindow
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the VolunteerWindow
            var volunteerWindow = new VolunteerWindow();
            volunteerWindow.Show();

            // Refresh the volunteer list after adding a new volunteer
            queryVolunteerList();
        }
        //private void lsvVolunteersList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    // Get the clicked volunteer's ID
        //    if (SelectedVolunteer != null)
        //        new VolunteerWindow(SelectedVolunteer.Id).Show();
        private void lsvVolunteersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject originalSource = (DependencyObject)e.OriginalSource;

            while (originalSource != null && !(originalSource is DataGridRow))
                originalSource = VisualTreeHelper.GetParent(originalSource);

            if (originalSource is DataGridRow row && row.Item is BO.VolunteerInList selectedVolunteer)
            {
                row.IsSelected = true; // ✅ צבע את השורה באדום
                new VolunteerWindow(selectedVolunteer.Id).Show();
            }
        }



        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.Tag is int volunteerId)
            {
                // Confirm deletion with the user
                var result = MessageBox.Show("Are you sure you want to delete this volunteer?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Call the Delete method in the BL
                        s_bl.Volunteer.Delete(volunteerId);

                        // If successful, the observer mechanism will automatically update the list
                        MessageBox.Show("Volunteer deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        VolunteerList = (VolunteerFieldsFilter == BO.VolunteerFields.None) ?
                                      s_bl?.Volunteer.ReadAll()! : s_bl?.Volunteer.ReadAll(null, VolunteerFieldsFilter)!;
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions from the BL and show an error message
                        MessageBox.Show($"Failed to delete volunteer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
            
        public VolunteerListWindow()
        {
            InitializeComponent();
        }
    }
}
