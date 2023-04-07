using System;

public abstract class BaseWeather
{
    protected string[] WeatherEvents { get; set; }
    protected int[] Chances { get; set; }
    protected static readonly Random Random = new Random();

    public Dictionary<int, string> GetWeatherEvent()
    {
        Dictionary<int, string> weather = new Dictionary<int, string>();
        int roll = Random.Next(100) + 1;

        int index = 0;
        for (index = 0; index < Chances.Length; index++)
        {
            if (roll > Chances[index])
            {
                weather.Add(index + 1, WeatherEvents[index]);
                return weather;
            }
        }
        return weather;
    }
}

public class WaterWeather : BaseWeather
{
    public WaterWeather()
    {
        WeatherEvents = new string[]
        {
            "Light Rain",
            "Fog",
            "Heavy Rain",
            "Torrential downpour"
        };

        Chances = new int[]
        {
            45,
            30,
            20,
            5
        };
    }
}

public class WaveWeather : BaseWeather
{
    public WaveWeather()
    {
        WeatherEvents = new string[]
        {
            "Surging Tides",
            "Rising Swells",
            "Rolling Breakers",
            "Mighty Tsunami"
        };

        Chances = new int[]
        {
            49,
            30,
            20,
            1
        };
    }
}

public class ThunderWeather : BaseWeather
{
    public ThunderWeather()
    {
        WeatherEvents = new string[]
        {
            "Lightning storm",
            "Rumbling on the horizon",
            "Heavy lightning",
            "Heat lightning",
            "St.Elmo's Fire"
        };

        Chances = new int[]
        {
            40,
            30,
            25,
            4,
            1
        };
    }
}

public class WindWeather : BaseWeather
{
    public WindWeather()
    {
        WeatherEvents = new string[]
        {
            "Strong Gusts",
            "Gentle Breeze",
            "Sudden Squalls",
            "Imposing Tempest"
        };

        Chances = new int[]
        {
            40,
            30,
            25,
            5
        };
    }
}

