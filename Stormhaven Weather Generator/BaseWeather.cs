using Microsoft.VisualBasic;
using System;


namespace WeatherProj
{
    public abstract class BaseWeather
    {
        protected (string Event, int Chance)[] WeatherData = new (string, int)[] { };
        protected static Random Random = new Random();
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
    public class WaterWeather : BaseWeather
    {
        public WaterWeather()
        {
            WeatherData = new (string, int)[]
            {
            ("None", 0),
            ("Light Rain", 20),
            ("Heavy Rain", 50),
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
            ("Light Breeze", 25),
            ("Heavy Wind", 60),
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
            ("Rumbling", 30),
            ("Heavy lightning", 70)
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
            ("Choppy Seas", 40),
            ("Tidal Flooding", 80),
            };
        }
    }

    public class FogWeather : BaseWeather
    {
        public FogWeather()
        {
            WeatherData = new (string, int)[]
                { ("None", 0),
                ("Light Fog", 20),
                ("Heavy Fog", 50)
                };
        }
    }
}
