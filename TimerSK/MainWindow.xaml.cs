using System;
using System.Windows;
using System.Windows.Threading;

namespace TimerSK
{
    /// <summary>
    /// Logika interakcji dla klasy MainFrame.xaml
    /// </summary>

    //TODO add exception handling (if required)

    public partial class MainWindow : Window
    {
        readonly DispatcherTimer timerClock = new DispatcherTimer();
        readonly MeetingList meetingList = new MeetingList();
        private int min;
        private int sec;
        private string zeroM = "";
        private string zeroS = "";
        private bool timerStarted = false;
        private int timerCountsDown = 0;
        private int meetingListPosition = 0;
        private readonly int meetingListPositionLength;
        private string pointName;




        public MainWindow()
        {

            InitializeComponent();

            meetingListPositionLength = meetingList.GetMeetingList().Count;

            min = meetingList.GetMeetingList()[0].minT;
            sec = meetingList.GetMeetingList()[0].secT;
            pointName = meetingList.GetMeetingList()[0].pointName;

            TitleLabel.Content = pointName;

            timerClock.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            timerClock.Tick += TimerClock_Tick;

            ShowDigits();

           
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!timerStarted)
            {
                timerClock.Start();
                timerStarted = true;
                PlayButton.Content = DisplaySettings.StopButtonImage();
                DisplaySettings.SetButtonStop(PlayButton);
                DisplaySettings.SetButtonDisable(PlusButton);
                DisplaySettings.SetButtonDisable(MinusButton);
                DisplaySettings.SetButtonDisable(ForwardButton);
             }
            else
            {
                timerClock.Stop();
                timerStarted = false;
                PlayButton.Content = DisplaySettings.StartButtonImage();
                DisplaySettings.SetButtonStart(PlayButton);
                DisplaySettings.SetButtonStart(PlusButton);
                DisplaySettings.SetButtonStart(MinusButton);
                DisplaySettings.SetButtonStart(ForwardButton);
                timerCountsDown = 0;
                if (meetingListPosition != meetingListPositionLength - 1)
                {
                    meetingListPosition++;
                    min = meetingList.GetMeetingList()[meetingListPosition].minT;
                    sec = meetingList.GetMeetingList()[meetingListPosition].secT;
                    pointName = meetingList.GetMeetingList()[meetingListPosition].pointName;
                    TitleLabel.Content = pointName;
                    ShowDigits();
                    BigDigits.Foreground = DigitsColors.GetColorForTimeRemaining(timerCountsDown);
                }
            }

        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (meetingListPosition != meetingListPositionLength - 1)
            {
                meetingListPosition++;
                min = meetingList.GetMeetingList()[meetingListPosition].minT;
                sec = meetingList.GetMeetingList()[meetingListPosition].secT;
                pointName = meetingList.GetMeetingList()[meetingListPosition].pointName;
                TitleLabel.Content = pointName;
                ShowDigits();
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            min++;
            if (min >= 60) { min = 60; }
            ShowDigits();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            min--;
            if (min <= 1) { min = 1; }
            ShowDigits();
        }


        private void TimerClock_Tick(object sender, EventArgs e)
        {
            TimerDisplay();
        }


        public void ShowDigits()
        {
            if (min == -1 && sec == -1)
            {
                zeroM = "0";
                min = 0;
                zeroS = "0";
                sec = 0;
                PlayButton.Content = DisplaySettings.StartButtonImage();
                DisplaySettings.SetButtonDisable(PlayButton);
                DisplaySettings.SetButtonDisable(PlusButton);
                DisplaySettings.SetButtonDisable(MinusButton);
                DisplaySettings.SetButtonDisable(ForwardButton);
            }
            else
            {

                if (min > 9) { zeroM = ""; } else { zeroM = "0"; }
                if (sec > 9) { zeroS = ""; } else { zeroS = "0"; }

            }
            BigDigits.Content = string.Format("{0}{1}:{2}{3}", zeroM, min, zeroS, sec);
            
        }

        public void TimerDisplay()
        {

            if (timerCountsDown == 0 || timerCountsDown == 1)
            {
                sec--;
                if (sec < 0)
                {
                    sec = 59;
                    min--;
                }
            }
            else
            {
                sec++;
                if (sec > 59)
                {
                    min++;
                    sec = 0;
                }
            }

            if (min > 9) { zeroM = ""; } else { zeroM = "0"; }
            if (sec > 9) { zeroS = ""; } else { zeroS = "0"; }

            if (sec == 0 && min == 0 && timerCountsDown == 1)
            {
                timerCountsDown = 2;
            }

            else if (sec <= 30 && min == 0 && timerCountsDown == 0)
            {
                timerCountsDown = 1;
            }

            BigDigits.Foreground = DigitsColors.GetColorForTimeRemaining(timerCountsDown);
            BigDigits.Content = string.Format("{0}{1}:{2}{3}", zeroM, min, zeroS, sec);
        }



    }
}



