using BO;
using PL.Volunteer;
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
        private void configObserver()
        {
            RiskRange = s_bl.Admin.GetRiskRange();       
        }
        private void clockObserver()
        {
            CurrentTime = s_bl.Admin.GetClock();
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

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnWindowLoaded; // Register the Loaded event
            this.Closed += OnWindowClosed; // Register the Loaded event


        }
    }
}