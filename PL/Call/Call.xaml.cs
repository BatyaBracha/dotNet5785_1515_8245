//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PL.Call
//{
//    /// <summary>
//    /// Interaction logic for Call.xaml
//    /// </summary>
//    public partial class Call : Window
//    {
//        public Call()
//        {
//            InitializeComponent();
//        }
//    }
//}
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

namespace PL.Call
{
    /// <summary>
    /// Interaction logic for CallWindow.xaml
    /// </summary>
    public partial class CallWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty ButtonTextProperty =
       DependencyProperty.Register("ButtonText", typeof(string), typeof(CallWindow), new PropertyMetadata("Add"));



        public BO.Call? CurrentCall
        {
            get { return (BO.Call)GetValue(CurrentCallProperty); }
            set { SetValue(CurrentCallProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentCall.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCallProperty =
            DependencyProperty.Register("CurrentCall", typeof(BO.Call), typeof(CallWindow), new PropertyMetadata(null));


        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        //public BO.Role Role { get; set; } = BO.Role.STANDARD;

        //public BO.TypeOfDistance TypeOfDistance { get; set; } = BO.TypeOfDistance.WALK;
        //public void comboBoxTypeOfDistance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //      => CurrentVolunteer.TypeOfDistance = sender is ComboBox comboBox ? (BO.TypeOfDistance)comboBox.SelectedItem : BO.TypeOfDistance.WALK;

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ButtonText == "Add")
                {
                    s_bl.Call.Create(CurrentCall!);
                    MessageBox.Show("Call added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (ButtonText == "Update")
                {
                    s_bl.Call.Update( CurrentCall!);
                    MessageBox.Show("Call updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentCall = s_bl.Call.GetCallDetails(CurrentCall!.Id);
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

        private void queryCallDetails()
        {
            int id = CurrentCall!.Id;
            CurrentCall = s_bl.Call.GetCallDetails(id);
        }

        private void VolunteerObserver()
                      => queryCallDetails();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentCall!.Id != 0)
                s_bl.Volunteer.AddObserver(CurrentCall!.Id, VolunteerObserver);
        }

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(CurrentCall!.Id, VolunteerObserver);

        public CallWindow(int Id = 0)
        {
            ButtonText = Id == 0 ? "Add" : "Update";
            CurrentCall = (Id != 0)
                ? s_bl.Call.GetCallDetails(Id)
                : new BO.Call(0, BO.TypeOfCall.NONE, "No description", "No address", null, null, s_bl.Admin.GetClock(), null, CallStatus.OPEN, null);

            InitializeComponent();
        }
      //  public void comboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
      //=> CurrentCall.Role = sender is ComboBox comboBox ? (BO.Role)comboBox.SelectedItem : BO.Role.STANDARD;

    }
}
