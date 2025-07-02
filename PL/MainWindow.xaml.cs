using BO;
using PL.Volunteer;
using PL.Call;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));
        private void btnAddOneMinute_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.PromotionClock(BO.TimeUnit.MINUTE);
        }
        private void btnAddOneHour_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.PromotionClock(BO.TimeUnit.HOUR);
        }
        private void btnAddOneDay_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.PromotionClock(BO.TimeUnit.DAY);
        }
        private void btnAddOneMonth_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.PromotionClock(BO.TimeUnit.MONTH);
        }
        private void btnAddOneYear_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.PromotionClock(BO.TimeUnit.YEAR);
        }

        public TimeSpan RiskRange
        {
            get { return (TimeSpan)GetValue(RiskRangeProperty); }
            set { SetValue(RiskRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RiskRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RiskRangeProperty =
            DependencyProperty.Register("RiskRange", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(TimeSpan.Zero));
        private void btnSetRiskRange_Click(object sender, RoutedEventArgs e)
        {
            s_bl.Admin.SetRiskRange(RiskRange);
        }

        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RiskRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(MainWindow), new PropertyMetadata(180));

        public string SimulatorButtonText
        {
            get { return (string)GetValue(SimulatorButtonTextProperty); }
            set { SetValue(SimulatorButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RiskRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulatorButtonTextProperty =
            DependencyProperty.Register("SimulatorButtonText", typeof(string), typeof(MainWindow), new PropertyMetadata("Start"));
        
        //private bool IsSimulatorRunning = false;
        public bool IsSimulatorRunning
        {
            get { return (bool)GetValue(IsSimulatorRunningProperty); }
            set { SetValue(IsSimulatorRunningProperty, value); }
        }

        public static readonly DependencyProperty IsSimulatorRunningProperty =
            DependencyProperty.Register("IsSimulatorRunning", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));


        public bool IsButtonsEnabled
        {
            get { return (bool)GetValue(IsButtonsEnabledProperty); }
            set { SetValue(IsButtonsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsButtonsEnabledProperty =
            DependencyProperty.Register("IsButtonsEnabled", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));

        private void btnSimulator_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSimulatorRunning)
            {
                    s_bl.Admin.StartSimulator(Interval);
                    SimulatorButtonText= "Stop"; // Update button text to "Stop" after starting the simulator
                    IsSimulatorRunning = true;
                    IsButtonsEnabled = false;
            }
            else
            {
                    s_bl.Admin.StopSimulator();
                    SimulatorButtonText = "Start";
                    IsSimulatorRunning = false; // Update button text to "Start" after stopping the simulator
                    IsButtonsEnabled = true; // Enable buttons again
            }
        }
        private void btnResetDB_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the DB?", "Reset DB", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != this)
                        {
                            window.Close();
                        }

                    }
                    s_bl.Admin.ResetDB();
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
                MessageBox.Show("DB reseted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnInitializeDB_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to initialize the DB?", "Initialize DB", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != this)
                        {
                            window.Close();
                        }

                    }
                    s_bl.Admin.InitializeDB();
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
                MessageBox.Show("DB initialized successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private volatile bool _observerWorking = false; //stage 7

        private void configObserver()
        {

            if (!_observerWorking)
            {
                _observerWorking = true;
                _ = Dispatcher.BeginInvoke(() =>
                {
                    RiskRange = s_bl.Admin.GetRiskRange();
                    _observerWorking = false;
                });
            }
        }
        private void clockObserver()
        {
            if (!_observerWorking)
            {
                _observerWorking = true;
                _ = Dispatcher.BeginInvoke(() =>
                {
                    CurrentTime = s_bl.Admin.GetClock();
                    _observerWorking = false;
                });
            }
        }
        /// <summary>
        /// Handles the Loaded event of the window.
        /// </summary>
        private void OnWindowLoaded(object sender, EventArgs e)
        {
            CurrentTime = s_bl.Admin.GetClock();
            RiskRange= s_bl.Admin.GetRiskRange();
            s_bl.Admin.AddClockObserver(clockObserver);
            s_bl.Admin.AddConfigObserver(configObserver);
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            s_bl.Admin.RemoveClockObserver(clockObserver);
            s_bl.Admin.RemoveConfigObserver(configObserver);
        }
        private void btnVolunteerList_Click(object sender, EventArgs e)
        {
            new VolunteerListWindow().Show();
        }

        private void btnCallList_Click(object sender, EventArgs e)
        {
            new CallListWindow().Show();
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnWindowLoaded; 
            this.Closed += OnWindowClosed; 
        }
    }
}