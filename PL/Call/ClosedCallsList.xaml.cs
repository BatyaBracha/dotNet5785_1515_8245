using BO;
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
    /// Interaction logic for ClosedCallsList.xaml
    /// </summary>
    public partial class ClosedCallsList : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.ClosedCallInList> ClosedCallList
        {
            get { return (IEnumerable<BO.ClosedCallInList>)GetValue(ClosedCallListProperty); }
            set { SetValue(ClosedCallListProperty, value); }
        }

        public static readonly DependencyProperty ClosedCallListProperty =
            DependencyProperty.Register("ClosedCallList", typeof(IEnumerable<BO.ClosedCallInList>), typeof(VolunteerListWindow), new PropertyMetadata(null));

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(ClosedCallsList), new PropertyMetadata(null));

        public BO.CallInListField CallFieldsFilter { get; set; } = BO.CallInListField.None;
        public BO.TypeOfCall? SelectedTypeOfCallFilter { get; set; } = BO.TypeOfCall.NONE;

        private void comboBoxCallList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QueryCallList();
        }

        private void QueryCallList()
              => ClosedCallList = SelectedTypeOfCallFilter == BO.TypeOfCall.NONE ? s_bl?.Call.GetClosedCallsHandledByTheVolunteer(Id, null, null, CallFieldsFilter)! : s_bl?.Call.GetClosedCallsHandledByTheVolunteer(Id, BO.CallFieldFilter.TypeOfCall, SelectedTypeOfCallFilter, CallFieldsFilter)!;


        private void CallListObserver()
                      => QueryCallList();

        private void Window_Loaded(object sender, RoutedEventArgs e)
                      => s_bl.Call.AddObserver(CallListObserver);

        private void Window_Closed(object sender, EventArgs e)
                      => s_bl.Call.RemoveObserver(CallListObserver);


        public ClosedCallsList(int id)
        {
            Id = id;
            InitializeComponent();
        }

    }
}
