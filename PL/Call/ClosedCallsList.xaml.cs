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
    /// <summary>
/// Interaction logic for the closed calls list window.
/// </summary>
public partial class ClosedCallsList : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public ClosedCallsList(int id)
        {
            Id = id;
            Loaded += Window_Loaded;
            Closed += Window_Closed;
            InitializeComponent();
            DataContext = this;
        }

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(nameof(Id), typeof(int), typeof(ClosedCallsList), new PropertyMetadata(0));

        public IEnumerable<BO.ClosedCallInList> ClosedCallList
        {
            get => (IEnumerable<BO.ClosedCallInList>)GetValue(ClosedCallListProperty);
            set => SetValue(ClosedCallListProperty, value);
        }

        public static readonly DependencyProperty ClosedCallListProperty =
            DependencyProperty.Register(nameof(ClosedCallList), typeof(IEnumerable<BO.ClosedCallInList>), typeof(ClosedCallsList), new PropertyMetadata(null));

        private volatile bool _observerWorking = false; //stage 7

        private void CallListObserver()
        {
            if (!_observerWorking)
            {
                _observerWorking = true;
                _ = Dispatcher.BeginInvoke(() =>
                {
                    QueryCallList();
                    _observerWorking = false;
                });
            }
        }

        //public BO.TypeOfCall? SelectedTypeOfCallFilter
        //{
        //    get => (BO.TypeOfCall?)GetValue(SelectedTypeOfCallFilterProperty);
        //    set => SetValue(SelectedTypeOfCallFilterProperty, value);
        //}

        //public static readonly DependencyProperty SelectedTypeOfCallFilterProperty =
        //    DependencyProperty.Register(nameof(SelectedTypeOfCallFilter), typeof(BO.TypeOfCall?), typeof(ClosedCallsList),
        //        new PropertyMetadata(BO.TypeOfCall.NONE, OnFilterChanged));

        //public BO.CallInListField CallFieldsFilter
        //{
        //    get => (BO.CallInListField)GetValue(CallFieldsFilterProperty);
        //    set => SetValue(CallFieldsFilterProperty, value);
        //}


        //public static readonly DependencyProperty CallFieldsFilterProperty =
        //    DependencyProperty.Register(nameof(CallFieldsFilter), typeof(BO.CallInListField), typeof(ClosedCallsList),
        //        new PropertyMetadata(BO.CallInListField.None, OnFilterChanged));
        public BO.CallInListField CallFieldsFilter { get; set; } = BO.CallInListField.None;
        public BO.TypeOfCall? SelectedTypeOfCallFilter { get; set; } = BO.TypeOfCall.NONE;


        public void comboBoxCallList_SelectionChanged(object sender, SelectionChangedEventArgs e)
                      => ClosedCallList = SelectedTypeOfCallFilter == BO.TypeOfCall.NONE ? s_bl?.Call.GetClosedCallsHandledByTheVolunteer(Id, null, null, CallFieldsFilter)! : s_bl?.Call.GetClosedCallsHandledByTheVolunteer(Id, BO.CallFieldFilter.TypeOfCall, SelectedTypeOfCallFilter, CallFieldsFilter)!;

        private static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ClosedCallsList win)
                win.QueryCallList();
        }

        private void QueryCallList()
            => ClosedCallList = SelectedTypeOfCallFilter == BO.TypeOfCall.NONE
                ? s_bl.Call.GetClosedCallsHandledByTheVolunteer(Id, null, null, CallFieldsFilter)
                : s_bl.Call.GetClosedCallsHandledByTheVolunteer(Id, BO.CallFieldFilter.TypeOfCall, SelectedTypeOfCallFilter, CallFieldsFilter);
        private void Window_Loaded(object sender, RoutedEventArgs e)
            => s_bl.Call.AddObserver(CallListObserver);

        private void Window_Closed(object sender, EventArgs e)
            => s_bl.Call.RemoveObserver(CallListObserver);
    }

}
