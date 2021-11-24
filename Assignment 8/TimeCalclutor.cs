using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    public class TimeCalclutor
    {
        public void AddTime(int min, int sec, int minNew, int secNew)
        {
            int secResult = 0, minResult = 0, hourResult = 0;
            min = (min > 60) ? min = 60 : min;
            sec = (sec > 60) ? sec = 60 : sec;
            minNew = (minNew > 60) ? minNew = 60 : minNew;
            secNew = (secNew > 60) ? secNew = 60 : secNew;
            if ((sec + secNew) > 60)
            {
                secResult = (sec + secNew) - 60;
                minNew++;
            }
            else
            {
                secResult = (sec + secNew);
            }
            minResult += min + minNew;
            if (minResult > 60)
            {
                hourResult++;
                minResult = minResult - 60;
            }
            Console.WriteLine($"{hourResult}:{minResult}:{secResult}");
        }

        public void MinusTime(int min, int sec, int minNew, int secNew)
        {
            int secResult = 0, minResult = 0, hourResult = 0;

            min = (min > 60) ? min = 60 : min;
            sec = (sec > 60) ? sec = 60 : sec;
            minNew = (minNew > 60) ? minNew = 60 : minNew;
            secNew = (secNew > 60) ? secNew = 60 : secNew;

            if ((sec - secNew) < 0)
            {
                secResult = 60- Math.Abs(sec - secNew);
                minNew++;
            }
            else
            {
                secResult = (sec - secNew);
            }
            minResult = Math.Abs(min - minNew );

            Console.WriteLine($"00:{minResult}:{secResult}");
        }

        public void SecToTime(int second)
        {
            var time = TimeSpan.FromSeconds(second);
            Console.WriteLine(time.ToString(@"hh\:mm\:ss\:fff"));
        }

        public void TimeToSec(string time)
        {
            Console.WriteLine(TimeSpan.Parse(time).TotalSeconds);
        }
    }
}
