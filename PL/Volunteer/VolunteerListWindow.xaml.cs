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
    /// Interaction logic for VolunteerListWindow.xaml
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

        public void comboBoxVolunteerList_SelectionChanged(object sender, SelectionChangedEventArgs e) 
                      => VolunteerList = (VolunteerFieldsFilter == BO.VolunteerFields.None) ?
                                      s_bl?.Volunteer.ReadAll()! : s_bl?.Volunteer.ReadAll(null, VolunteerFieldsFilter)!;

        private void queryVolunteerList()
                      => VolunteerList = (VolunteerFieldsFilter == BO.VolunteerFields.None) ?
                                      s_bl?.Volunteer.ReadAll()! : s_bl?.Volunteer.ReadAll(null, VolunteerFieldsFilter)!;


        private void volunteerListObserver()
                      => queryVolunteerList();
 
        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Volunteer.AddObserver(volunteerListObserver);

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Volunteer.RemoveObserver(volunteerListObserver);

        public VolunteerListWindow()
        {
            InitializeComponent();
        }
    }
}
