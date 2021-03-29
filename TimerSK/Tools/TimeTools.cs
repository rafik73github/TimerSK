using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerSK
{

    //TODO add exception handling (if required)
    class TimeTools
    {
        static readonly DateTime thisDay = DateTime.Today;
       
       
        public static void Test()
        {
            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            //Console.WriteLine(thisDay.ToString("yyyy-MM-dd"));
            Console.WriteLine(monday);
            Console.WriteLine(thisDay);
            string dateInput = "2020-06-09";
            var parsedDate = DateTime.Parse(dateInput);
            Console.WriteLine(parsedDate);

            string dateInput1 = "2020-06-14";
            var parsedDate1 = DateTime.Parse(dateInput1);

            string dateInput2 = "2020-06-08";
            var parsedDate2 = DateTime.Parse(dateInput2);

            Console.WriteLine((parsedDate1 - parsedDate2).TotalDays.ToString());
            if(parsedDate2 < monday)
            {
                Console.WriteLine("za stary");
            }

        }
        /// <summary>
        /// check if today is the weekend
        /// </summary>
        /// <returns></returns>
        public static bool IfWeekend()
        {
            bool result = false;
            string today = thisDay.DayOfWeek.ToString();
            if(today.Equals("Saturday") || today.Equals("Sunday"))
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// checks if the JSON file is up to date
        /// </summary>
        /// <param name="fromJson">The start date of the week taken from the JSON file on the disk</param>
        /// <returns>returns true if the file JSON is out of date</returns>
        public static bool IfFileIsOutOfDate(DateTime fromJson)
        {
            bool result = false;
            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            if(fromJson < monday)
            {
                result = true;
            }
            return result;
        }

    }
}
