using System.Windows.Media;

namespace TimerSK
{
    class DigitsColors
    {

        private static readonly Brush GreenDigit = new SolidColorBrush(Color.FromRgb(127, 255, 0));
        private static readonly Brush YellowDigit = new SolidColorBrush(Color.FromRgb(255, 255, 0));
        private static readonly Brush RedDigit = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        /// <summary>
        /// Sets the color of the timer digits according to timer mode
        ///(timerMode) 0 - timer down, 1 - last 30 seconds, 2 - timer up
        /// </summary>

        //TODO add exception handling (if required)
        public static Brush GetColorForTimeRemaining(int timerMode)
        {
            if (timerMode == 2)
            {
                return RedDigit;
            }

            else if (timerMode == 1)
            {
                return YellowDigit;
            }
            else
            { 
            return GreenDigit;
        }
        }

       
    }

    
}
