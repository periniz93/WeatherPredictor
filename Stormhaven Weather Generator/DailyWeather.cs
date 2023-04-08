using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class DailyWeather{
    /*Attributes*/
    DailyWeather Yesterday { get; set;}
    DailyWeather Tomorrow { get; set;}
    int Day { get; set;}
    int Month { get; set;}
    int Year { get; set;}
    int Temperature { get; set;}
    int Count { get; set;}
    int dayCount { get; set;}   
    int change { get; set;}
    int eventCount { get; set;} 

    public class Weather
    {
        public string weatherEvent { get; set; }
        public int severity { get; set; }

        public Weather()
        {
            weatherEvent = string.Empty;
            severity = 0;

        }
    }
    Dictionary<int, string> wind { get; set; }
    Dictionary<int, string> water { get; set; }
    Dictionary<int, string> wave { get; set; }
    Dictionary<int, string> thunder { get; set; }

    /*The reason for this is to 
     * have a key-value entry
     * for all weather types */
    Dictionary<string, Weather> weatherDict { get; set; }
    
    public DailyWeather() { 
        Yesterday = null;
        Tomorrow = null;
        Day = 0;
        Month = 0;
        Year = 0;
        Temperature = 0;
        Count = 0;
        change = 0;
        dayCount = 1;
        eventCount = 0;
        
        
        weatherDict = new Dictionary<string, Weather>();
        addWeathersToDict();

        water = new Dictionary<int, string>();
        wind = new Dictionary<int, string>();   
        wave = new Dictionary<int, string>();
        thunder = new Dictionary<int, string>();
        firstDay();
        Console.WriteLine();
    }

    



    public DailyWeather(DailyWeather yesterday)
    {
        Yesterday = yesterday;
        Tomorrow = null;
        Day = 0;
        Month = 0;
        Year = 0;
        Temperature = 0;
        Count = 0;
        change = 0;
        yesterday.Tomorrow = this;
        dayCount = Yesterday.dayCount + 1;
        weatherDict = new Dictionary<string, Weather>();
        addWeathersToDict();
        water = new Dictionary<int, string>();
        wind = new Dictionary<int, string>();
        wave = new Dictionary<int, string>();
        thunder = new Dictionary<int, string>();
        checkWeather(yesterday);
        eventCount = yesterday.eventCount;
    }

    public void addWeathersToDict()

    {
        this.weatherDict.Add("water", new Weather());
        this.weatherDict.Add("wind", new Weather());
        this.weatherDict.Add("thunder", new Weather());
        this.weatherDict.Add("wave", new Weather());         
    }

    public void firstDay()
    {
        
        //get the weaterdict dict value with water key 
        foreach(KeyValuePair<string, Weather> item in this.weatherDict)
        {
            item.Value.severity = 0;
            item.Value.weatherEvent = "None";
        }

    }

    public int countDailyChange()
    {
        return this.Yesterday.Count - this.Count;
        
    }
    public double countEvents() {
        switch (this.eventCount)
        {
            case 0:
                return 1;               
            case 1:
                return 0.75;            
            case 2:
                return 0.6;              
            case 3:
                return 0.5;                
            case 4:
                return 0.25;
            default: return 1;
        }
    }



    public void addToCount()
    {

    }
    public void checkWeather()
    {
        

    }

    public void checkWeather(DailyWeather yesterday)
    {
        //it doesn't have to return anything, it just has to add the weather to the weatherDict
        // Since there's no yesterday, no weather has to be checked
        double count = 0;

        
        // Water 
        //(1) Check eventCount before water weather
        //(2) pass eventcount to BaseWeather:WaterWeather GetWeatherEvent()
        //(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        //(4) Add the weather description and severity to the WeatherDict Water key
        
        BaseWeather baseWeather = new WaterWeather();
        Dictionary<int, string> weather = new Dictionary<int, string>();
        count = countEvents();
        weather = baseWeather.GetWeatherEvent(count);
        this.weatherDict["water"].weatherEvent = weather.Last().Value;
        this.weatherDict["water"].severity = weather.Last().Key;

        if (this.weatherDict["water"].severity > 0)
        {
            if (!(checkYesterdayWater()))
            { 
                this.eventCount++;
            }
        }
        /*
        *Wind
        *(1) Check eventCount before wind weather
        *(2) pass eventcount to BaseWeather: WindWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) Add the weather description and severity to the WeatherDict Wind Key
        */
        baseWeather = new WindWeather();
        weather = new Dictionary<int, string>();
        count = countEvents();
        weather = baseWeather.GetWeatherEvent(count);
        this.weatherDict["wind"].weatherEvent = weather.Last().Value;
        this.weatherDict["wind"].severity = weather.Last().Key;

        if (this.weatherDict["wind"].severity > 0)
        {
            if (!(checkYesterdayWind()))
            {
                this.eventCount++;
            }
        }
        /*
        *Thunder 
        *(1) Check eventCount before Thunder weather
        *(2) pass eventcount to BaseWeather: ThunderWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) add the weather description and severity to the WeatherDict Thunder Key
        */
        baseWeather = new ThunderWeather();
        weather = new Dictionary<int, string>();
        count = countEvents();
        weather= baseWeather.GetWeatherEvent(count);
        this.weatherDict["thunder"].weatherEvent = weather.Last().Value;
        this.weatherDict["thunder"].severity = weather.Last().Key;

        if (this.weatherDict["thunder"].severity > 0)
        {
            if (!(checkYesterdayThunder()))
            {
                this.eventCount++;
            }
        }   
        /*
        *Waves
        *(1) Check eventCount before Wave weather
        *(2) pass eventcount to BaseWeather: ThunderWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) Add the weather description and severity to the WeatherDict Waves Key
        *
        */

        baseWeather = new WaveWeather();
        weather = new Dictionary<int, string>();
        count = countEvents();
        weather = baseWeather.GetWeatherEvent(count);
        this.weatherDict["wave"].weatherEvent = weather.Last().Value;
        this.weatherDict["wave"].severity = weather.Last().Key; 
        if (this.weatherDict["wave"].severity > 0)
        {
            if (!(checkYesterdayWave()))
            {
                this.eventCount++;
            }
        }

        Console.WriteLine();
    }

    public void weatherMultiplier(int count)
    {

    }

    public bool checkYesterdayWater()
    {
        if (this.Yesterday.weatherDict["water"].severity == 0)
        {
            return false;
        }
        return true; 
    }

    public bool checkYesterdayWind()
    {
        if (this.Yesterday.weatherDict["wind"].severity == 0)
        {
            return false;
        }
        return true;
    }
    public bool checkYesterdayWave()
    {
        if (this.Yesterday.weatherDict["wave"].severity == 0)
        {
            return false;
        }
        return true;
    }

    public bool checkYesterdayThunder()
    {
        if (this.Yesterday.weatherDict["thunder"].severity == 0)
        {
            return false;
        }
        return true;
    }

    public void printDailyWeather()
    {
        Console.WriteLine("Schedule for Day " + dayCount);
        Console.WriteLine("______________________________");
        string printString = "";

        foreach(KeyValuePair<string, Weather> item in this.weatherDict)
        {
            printString += item.Value.weatherEvent + " | ";
        }
        Console.WriteLine(printString+ "\n");

    }

    public void printProbabilities()
    {
        Console.WriteLine();
    }
}

