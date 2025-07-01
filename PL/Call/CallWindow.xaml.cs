using BO;
using System;
using System.Windows;

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

        public static readonly DependencyProperty CurrentCallProperty =
            DependencyProperty.Register("CurrentCall", typeof(BO.Call), typeof(CallWindow), new PropertyMetadata(null));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public CallWindow(int? Id = 0)
        {
            ButtonText = Id == 0 ? "Add" : "Update";
            CurrentCall = (Id != 0) ? s_bl.Call.GetCallDetails(Id!.Value)!
                : new BO.Call(0, BO.TypeOfCall.NONE, "No description", "No address", null, null, DateTime.Now, null, BO.CallStatus.OPEN, new List<BO.CallAssignInList>());

            InitializeComponent();
        }
        private void queryCallDetails()
              => CurrentCall = s_bl.Call.GetCallDetails(CurrentCall!.Id);

        private void CallObserver()
                      => queryCallDetails();

        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Call.AddObserver(CallObserver);

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(CallObserver);


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
                    s_bl.Call.Update(CurrentCall!);
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
    }
}
