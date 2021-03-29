using System;
using System.Windows;
using System.Windows.Threading;
using TimerSK.Tools;

namespace TimerSK
{
    /// <summary>
    /// Logika interakcji dla klasy NoInternet.xaml
    /// </summary>
    public partial class NoInternet : Window
    {
        readonly DispatcherTimer timerClock = new DispatcherTimer();
        public NoInternet()
        {
            InitializeComponent();

            timerClock.Interval = new TimeSpan(0, 0, 5); //in Hour, Minutes, Second.
            timerClock.Tick += TimerClock_Tick;
            timerClock.Start();
        }

        private void TimerClock_Tick(object sender, EventArgs e)
        {
            if (new NetTest().IsInternetAvailable())
            {
                timerClock.Stop();
                LoginWindow loginWindow = new LoginWindow();
                this.Close();
                loginWindow.Show();
            }
        }

        private void NoNetExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
