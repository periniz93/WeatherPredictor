using Microsoft.VisualBasic;
using System;


namespace WeatherProj
{
    public abstract class BaseWeather
    {
        protected (string Event, int Chance)[] WeatherData { get; set; }
        protected static Random Random = new Random();

        //public Dictionary<int, string> GetWeatherEvent(double multiplier)
        //{
        //    Dictionary<int, string> weather = new Dictionary<int, string>();
        //    // d100
        //    double random = Random.Next(100);
        //    double roll = random * multiplier + 1;
        //    Console.WriteLine("roll was " + roll + "(" + random + " * " + multiplier + "+ 1)");

        //    int index = 0;
        //    int lastbeatenMax = 0;
        //    for (index = 0; index < Chances.Length; index++)
        //    {
        //        if (lastbeatenMax > 0 && Chances[index] >= lastbeatenMax)
        //        {

        //            if (roll > Chances[index])
        //            {
        //                weather.Add(index, WeatherEvents[index]);
        //                Console.WriteLine(WeatherEvents[index] + " added. Severity = " + (index));
        //                lastbeatenMax = Chances[index];
        //            }
        //        }
        //        else //first run 
        //        {
        //            if (roll > Chances[index])
        //            {
        //                weather.Add(index, WeatherEvents[index]);
        //                Console.WriteLine(WeatherEvents[index] + " added. Severity = " + index);
        //                lastbeatenMax = Chances[index];
        //            }
        //            //Console.WriteLine("roll was " + roll);                
        //        }

        //    }

        //    return weather;
        //}
        public Dictionary<int, string> GetWeatherEvent(double multiplier)
        {
            Dictionary<int, string> weather = new Dictionary<int, string>();
            double random = Random.Next(100);
            double roll = random * multiplier + 1;

            int lastbeatenMax = 0;
            for (int index = 0; index < WeatherData.Length; index++)
            {
                var (Event, Chance) = WeatherData[index];
                if (lastbeatenMax > 0 && Chance >= lastbeatenMax)
                {
                    if (roll > Chance)
                    {
                        weather.Add(index, Event);
                        lastbeatenMax = Chance;
                    }
                }
                else
                {
                    if (roll > Chance)
                    {
                        weather.Add(index, Event);
                        lastbeatenMax = Chance;
                    }
                }
            }
            return weather;
        }

        

    }


    //    public Dictionary<int, string> GetWeatherEvent2(double multiplier, int ydaySeverity)
    //    {
    //        Dictionary<int, string> weather = new Dictionary<int, string>();
    //        // d100
    //        double num = Chances[ydaySeverity];
    //        double random = (num - 50) + (Random.NextDouble() * ((num + 50) - (num - 50)));
    //        if (random < 0)
    //        {
    //            random = 0;
    //        }

    //        if (random > 100)
    //        {
    //            random = 100;
    //        }

    //        double roll = random * multiplier;
    //        Console.WriteLine("roll was " + roll + "(" + random + " * " + multiplier + "+ 1)");

    //        int index = 0;
    //        int lastbeatenMax = 0;
    //        for (index = 0; index < Chances.Length; index++)
    //        {
    //            if (lastbeatenMax > 0 && Chances[index] >= lastbeatenMax)
    //            {

    //                if (roll > Chances[index])
    //                {
    //                    weather.Add(index, WeatherEvents[index]);
    //                    Console.WriteLine(WeatherEvents[index] + " added. Severity = " + (index));
    //                    lastbeatenMax = Chances[index];
    //                }
    //            }
    //            else //first run 
    //            {
    //                if (roll > Chances[index])
    //                {
    //                    weather.Add(index, WeatherEvents[index]);
    //                    Console.WriteLine(WeatherEvents[index] + " added. Severity = " + index);
    //                    lastbeatenMax = Chances[index];
    //                }
    //                //Console.WriteLine("roll was " + roll);                
    //            }

    //        }

    //        return weather;

    //    }
    //}

    //public class WaterWeather : BaseWeather
    //{
    //    public WaterWeather()
    //    {
    //        WeatherEvents = new string[]
    //        {
    //            "None", //sev 0
    //            "Fog", //sev 1
    //            "Light Rain", //sev 2           
    //            "Heavy Rain", //sev 3
    //            "Torrential downpour" //sev 4
    //        };

    //        Chances = new int[]
    //        {
    //            95, //sev 0 = 5%
    //            0, //sev 1 = 45%
    //            20, //sev 2 = 30%   
    //            50, //sev 3 = 20%
    //            75  // sev 4 = 5%
    //        };
    //    }
    //}

    //public class WaveWeather : BaseWeather
    //{
    //    public WaveWeather()
    //    {
    //        WeatherEvents = new string[]
    //        {
    //            "None",
    //            "Surging Tides",
    //            "Rising Swells",
    //            "Rolling Breakers",
    //            "Mighty Tsunami"
    //        };

    //        Chances = new int[]
    //        {
    //            0,
    //            50,
    //            70,
    //            85,
    //            99
    //        };
    //    }
    //}

    //public class ThunderWeather : BaseWeather
    //{
    //    public ThunderWeather()
    //    {
    //        WeatherEvents = new string[]
    //        {
    //            "None",
    //            "Heat Lightning",
    //            "Rumbling",
    //            "Lightning storm",
    //            "Heavy lightning",
    //        };

    //        Chances = new int[]
    //        {
    //            0,
    //            10,
    //            40,
    //            80,
    //            90
    //        };
    //    }
    //}

    //public class WindWeather : BaseWeather
    //{
    //    public WindWeather()
    //    {
    //        WeatherEvents = new string[]
    //        {
    //            "None",
    //            "Gentle Breeze",
    //            "Sudden Squalls",
    //            "Strong Gusts",
    //            "Imposing Tempest"
    //        };

    //        Chances = new int[]
    //        {
    //            0,
    //            15,
    //            40,
    //            75,
    //            90
    //        };
    //    }
    //}
    public class WaterWeather : BaseWeather
    {
        public WaterWeather()
        {
            WeatherData = new (string, int)[]
            {
            ("None", 95),
            ("Fog", 0),
            ("Light Rain", 20),
            ("Heavy Rain", 50),
            ("Torrential downpour", 75)
            };
        }
    }

    public class WindWeather : BaseWeather
    {
        public WindWeather()
        {
            WeatherData = new (string, int)[]
            {
            ("None", 0),
            ("Gentle Breeze", 15),
            ("Sudden Squalls", 40),
            ("Strong Gusts", 75),
            ("Imposing Tempest", 90)
            };
        }
    }

    public class ThunderWeather : BaseWeather
    {
        public ThunderWeather()
        {
            WeatherData = new (string, int)[]
            {
            ("None", 0),
            ("Heat Lightning", 10),
            ("Rumbling", 40),
            ("Lightning storm", 80),
            ("Heavy lightning", 90)
            };
        }
    }

    public class WaveWeather : BaseWeather
    {
        public WaveWeather()
        {
            WeatherData = new (string, int)[]
            {
            ("None", 0),
            ("Surging Tides", 50),
            ("Rising Swells", 70),
            ("Rolling Breakers", 85),
            ("Mighty Tsunami", 99)
            };
        }
    }
}
