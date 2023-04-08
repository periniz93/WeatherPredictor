﻿using Microsoft.VisualBasic;
using System;

public abstract class BaseWeather
{
    protected string[] WeatherEvents { get; set; }
    protected int[] Chances { get; set; }
    protected static Random Random = new Random();

    public Dictionary<int, string> GetWeatherEvent(double eventCount)
    {
        Dictionary<int, string> weather = new Dictionary<int, string>();
        // d100
        double random = Random.Next(100);
        double roll = random * eventCount + 1;
        Console.WriteLine("roll was " + roll+"("+ random +" * " + eventCount + "+ 1)");

        int index = 0;
        int lastbeatenMax = 0;
        for (index = 0; index < Chances.Length; index++)
        {
            if (lastbeatenMax > 0 && Chances[index] >= lastbeatenMax)
            {
                
                    if (roll > Chances[index])
                    {
                        weather.Add(index, WeatherEvents[index]);
                        Console.WriteLine(WeatherEvents[index] + " added. Severity = " + (index));
                        lastbeatenMax = Chances[index];
                    }          
            }
            else //first run 
            {
                if (roll > Chances[index])
                {
                    weather.Add(index, WeatherEvents[index]);
                    Console.WriteLine(WeatherEvents[index] + " added. Severity = " + index);
                    lastbeatenMax = Chances[index];
                }
                //Console.WriteLine("roll was " + roll);
                
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
            "None", //sev 0
            "Fog", //sev 1
            "Light Rain", //sev 2           
            "Heavy Rain", //sev 3
            "Torrential downpour" //sev 4
        };

        Chances = new int[]
        {
            95, //sev 0 = 5%
            0, //sev 1 = 45%
            35, //sev 2 = 30%   
            65, //sev 3 = 20%
            85  // sev 4 = 5%
        };
    }
}

public class WaveWeather : BaseWeather
{
    public WaveWeather()
    {
        WeatherEvents = new string[]
        {
            "None",
            "Surging Tides",
            "Rising Swells",
            "Rolling Breakers",
            "Mighty Tsunami"
        };

        Chances = new int[]
        {
            0,
            50,
            70,
            85,
            99
        };
    }
}

public class ThunderWeather : BaseWeather
{
    public ThunderWeather()
    {
        WeatherEvents = new string[]
        {
            "None",
            "Heat Lightning",
            "Rumbling",
            "Lightning storm",
            "Heavy lightning",
        };

        Chances = new int[]
        {
            0,
            10,
            40,
            80,
            90
        };
    }
}

public class WindWeather : BaseWeather
{
    public WindWeather()
    {
        WeatherEvents = new string[]
        {
            "None",
            "Gentle Breeze",
            "Sudden Squalls",
            "Strong Gusts",
            "Imposing Tempest"
        };

        Chances = new int[]
        {
            0,
            15,
            40,
            75,
            90
        };
    }
}

