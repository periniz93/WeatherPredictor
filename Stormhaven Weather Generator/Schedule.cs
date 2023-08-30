using System.Collections.Generic;
using System;

namespace WeatherProj
{
    public class Schedule : List<DailyWeather>
    {

        public Schedule()
        {

        }
        public void printSchedule()
        {
            foreach (DailyWeather weather in this)
            {
                weather.printDailyWeather();
            }
        }


    }
}
