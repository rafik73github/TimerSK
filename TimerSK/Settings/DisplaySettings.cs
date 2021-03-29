using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TimerSK
{
    
    
    class DisplaySettings
    {


        private static readonly Brush buttonStartColor = new SolidColorBrush(Color.FromRgb(54, 90, 112));
        private static readonly Brush buttonStopColor = new SolidColorBrush(Color.FromRgb(180, 20, 20));
        private static readonly Brush buttonDisableColor = new SolidColorBrush(Color.FromRgb(120, 120, 120));

        public static Image StartButtonImage()
        {
            Image startButtonImage = new Image();
            BitmapImage startButtonImageCreate = new BitmapImage();
            startButtonImageCreate.BeginInit();
            startButtonImageCreate.UriSource = new Uri("/Resources/play.png", UriKind.Relative);
            startButtonImageCreate.EndInit();
            startButtonImage.Source = startButtonImageCreate;

            return startButtonImage;
        }

        public static Image StopButtonImage()
        {

            Image stopButtonImage = new Image();
            BitmapImage stopButtonImageCreate = new BitmapImage();
            stopButtonImageCreate.BeginInit();
            stopButtonImageCreate.UriSource = new Uri("/Resources/stop.png", UriKind.Relative);
            stopButtonImageCreate.EndInit();
            stopButtonImage.Source = stopButtonImageCreate;

            return stopButtonImage;

        }

        public static void SetButtonStart(Button btn)
        {
            btn.Background = buttonStartColor;
            btn.BorderBrush = buttonStartColor;
            btn.IsEnabled = true;
        }

        public static void SetButtonStop(Button btn)
        {
            btn.Background = buttonStopColor;
            btn.BorderBrush = buttonStopColor;
        }

        public static void SetButtonDisable(Button btn)
        {
            btn.Background = buttonDisableColor;
            btn.BorderBrush = buttonDisableColor;
            btn.IsEnabled = false;
        }



    }
}
