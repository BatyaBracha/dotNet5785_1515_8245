using PL.Volunteer;
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

namespace PL.Call
{
    /// <summary>
    /// Interaction logic for CallListWindow.xaml
    /// </summary>
    public partial class OpenCallsListWindow : Window
    {
        public OpenCallsListWindow(int id)
        {
            Id = id;
            InitializeComponent();
        }


        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(int), typeof(OpenCallsListWindow), new PropertyMetadata(null));

        public IEnumerable<BO.OpenCallInList> OpenCallList
        {
            get { return (IEnumerable<BO.OpenCallInList>)GetValue(OpenCallListProperty); }
            set { SetValue(OpenCallListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for opencalllist.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenCallListProperty =
            DependencyProperty.Register("OpenCallList", typeof(IEnumerable<BO.OpenCallInList>), typeof(OpenCallsListWindow), new PropertyMetadata(null));

        public BO.CallInListField CallFieldsFilter { get; set; } = BO.CallInListField.None;
        public BO.TypeOfCall? SelectedTypeOfCallFilter { get; set; } = BO.TypeOfCall.NONE;


        public void comboBoxCallList_SelectionChanged(object sender, SelectionChangedEventArgs e)
                      => OpenCallList = SelectedTypeOfCallFilter == BO.TypeOfCall.NONE ? s_bl?.Call.GetOpenCallsCanBeSelectedByAVolunteer(Id,null, null, CallFieldsFilter)! : s_bl?.Call.GetOpenCallsCanBeSelectedByAVolunteer( Id,BO.CallFieldFilter.TypeOfCall, SelectedTypeOfCallFilter, CallFieldsFilter)!;
        //    public void comboBoxTypeOfCallFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //=> CallList = (SelectedTypeOfCallFilter == null)
        //    ? s_bl?.Call.ReadAll(null , null, null)!
        //    : s_bl?.Call.ReadAll(SelectedTypeOfCallFilter, null, CallFieldsFilter)!;

        private void queryCallList()
                      => OpenCallList = SelectedTypeOfCallFilter == BO.TypeOfCall.NONE ? s_bl?.Call.GetOpenCallsCanBeSelectedByAVolunteer(Id,null, null, CallFieldsFilter)! : s_bl?.Call.GetOpenCallsCanBeSelectedByAVolunteer(Id,BO.CallFieldFilter.TypeOfCall, SelectedTypeOfCallFilter, CallFieldsFilter)!;


        private void callListObserver()
                      => queryCallList();

        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Call.AddObserver(callListObserver);

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Call.RemoveObserver(callListObserver);

        public BO.OpenCallInList? SelectedCall { get; set; }

        //// AddButton_Click event handler to open the VolunteerWindow
        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Open the VolunteerWindow
        //    var callWindow = new CallWindow();
        //    callWindow.Show();

        //    // Refresh the volunteer list after adding a new volunteer
        //    queryCallList();
        //}
        private void lsvCallList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Get the clicked volunteer's ID
            if (SelectedCall != null)
                try
                {
                    s_bl?.Call.ChoosingACallForTreatment(Id, SelectedCall.Id);
                    MessageBox.Show("Call selected successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Call selection failed. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        //private void DeleteButton_Click(object sender, EventArgs e)
        //{
        //    if (sender is Button deleteButton && deleteButton.Tag is int callId)
        //    {
        //        // Confirm deletion with the user
        //        var result = MessageBox.Show("Are you sure you want to delete this call?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        //        if (result == MessageBoxResult.Yes)
        //        {
        //            try
        //            {
        //                // Call the Delete method in the BL
        //                s_bl.Call.Delete(callId);

        //                // If successful, the observer mechanism will automatically update the list
        //                MessageBox.Show("Call deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //                CallList = (CallFieldsFilter == BO.CallInListField.None) ?
        //                              s_bl?.Call.ReadAll(null, null, null)! : s_bl?.Call.ReadAll(null, CallFieldsFilter, null)!;
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any exceptions from the BL and show an error message
        //                MessageBox.Show($"Failed to delete call: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //    }
        //}

    }
}

