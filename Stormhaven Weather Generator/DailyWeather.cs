using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static DailyWeather;

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
        eventCount = yesterday.eventCount;
        checkWeather(yesterday);
        Console.WriteLine();
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
                return 0.85;            
            case 2:
                return 0.70;              
            case 3:
                return 0.55;                
            case 4:
                return 0.40;
            default: return 1;
        }
    }



    public void incrEventCount()
    {
        if (this.eventCount < 4){
            this.eventCount++;
        }
    }

    public void decrEventCount()
    {
        if (this.eventCount > 0)
        {
            this.eventCount--;
        }
    }
    public void checkWeather()
    {
        

    }

    public bool rollForChange()
    {
        Random r = new Random();
        double coinflip = r.Next(2); 
        double roll = coinflip;
        if (roll == 1)
        {
            return true;
        }
        return false;
    }

    public void checkWeather(DailyWeather yesterday)
    {
        // Water 
        //(1) Check eventCount before water weather
        //(2) pass eventcount to BaseWeather:WaterWeather GetWeatherEvent()
        //(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        //(4) Add the weather description and severity to the WeatherDict Water key
        if (rollForChange())
        {
            BaseWeather baseWeather = new WaterWeather();
            Dictionary<int, string> weather = new Dictionary<int, string>();
            double multiplier = countEvents();
            weather = baseWeather.GetWeatherEvent(multiplier);
            this.weatherDict["water"].weatherEvent = weather.Last().Value;
            this.weatherDict["water"].severity = weather.Last().Key;

            

            if (this.weatherDict["water"].severity > 0)
            {
                if (!(checkYesterdayWater()))
                {
                    incrEventCount();
                    Console.WriteLine("Incremented on Water Event");
                }
            }
            else
            {
                if ((checkYesterdayWater()))
                {
                    decrEventCount();
                    Console.WriteLine("Decremented on Water Event");
                }
            }
        }
        else
        {
            Console.WriteLine("No Change in Water");
            this.weatherDict["water"].weatherEvent = yesterday.weatherDict["water"].weatherEvent;
            this.weatherDict["water"].severity = yesterday.weatherDict["water"].severity;
        }
        /*
        *Wind
        *(1) Check eventCount before wind weather
        *(2) pass eventcount to BaseWeather: WindWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) Add the weather description and severity to the WeatherDict Wind Key
        */
        if (rollForChange())
        {
            BaseWeather baseWeather = new WindWeather();
            Dictionary<int, string> weather = new Dictionary<int, string>();
            double multiplier = countEvents();
            weather = baseWeather.GetWeatherEvent(multiplier);
            this.weatherDict["wind"].weatherEvent = weather.Last().Value;
            this.weatherDict["wind"].severity = weather.Last().Key;

            if (this.weatherDict["wind"].severity > 0)
            {
                if (!(checkYesterdayWind()))
                {
                    incrEventCount();
                    Console.WriteLine("Incremented on Wind Event");
                }
            }
            else
            {
                if ((checkYesterdayWind()))
                {
                    decrEventCount();
                    Console.WriteLine("Decremented on Wind Event");
                }
            }
        }
        else
        {
            this.weatherDict["wind"].weatherEvent = yesterday.weatherDict["wind"].weatherEvent;
            this.weatherDict["wind"].severity = yesterday.weatherDict["wind"].severity;
        }
        /*
        *Thunder 
        *(1) Check eventCount before Thunder weather
        *(2) pass eventcount to BaseWeather: ThunderWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) add the weather description and severity to the WeatherDict Thunder Key
        */
        if (rollForChange())
        {
            BaseWeather baseWeather = new ThunderWeather();
            Dictionary<int, string> weather = new Dictionary<int, string>();
            double multiplier = countEvents();
            weather = baseWeather.GetWeatherEvent(multiplier);
            this.weatherDict["thunder"].weatherEvent = weather.Last().Value;
            this.weatherDict["thunder"].severity = weather.Last().Key;

            if (this.weatherDict["thunder"].severity > 0)
            {
                if (!(checkYesterdayThunder()))
                {
                    incrEventCount();
                    Console.WriteLine("Incremented on Thunder Event");
                }
            }
            else
            {
                if ((checkYesterdayThunder()))
                {
                    decrEventCount();
                    Console.WriteLine("Decremented on Thunder Event");
                }
            }
        }
        else
        {
            this.weatherDict["thunder"].weatherEvent = yesterday.weatherDict["thunder"].weatherEvent;
            this.weatherDict["thunder"].severity = yesterday.weatherDict["thunder"].severity;
        }
        /*
        *Waves
        *(1) Check eventCount before Wave weather
        *(2) pass eventcount to BaseWeather: ThunderWeather GetWeatherEvent()
        *(3) GetWeatherEvent() will return a dict<string, int> with the weather description and severity
        *(4) Add the weather description and severity to the WeatherDict Waves Key
        *
        */

        if (rollForChange())
        {
            BaseWeather baseWeather = new WaveWeather();
            Dictionary<int, string> weather = new Dictionary<int, string>();
            double multiplier = countEvents();
            weather = baseWeather.GetWeatherEvent(multiplier);
            this.weatherDict["wave"].weatherEvent = weather.Last().Value;
            this.weatherDict["wave"].severity = weather.Last().Key;

            if (this.weatherDict["wave"].severity > 0)
            {
                if (!(checkYesterdayWave()))
                {
                    incrEventCount();
                    Console.WriteLine("Incremented on Wave Event");
                }
            }
            else
            {
                if ((checkYesterdayWave()))
                {
                    decrEventCount();
                    Console.WriteLine("Decremented on Wave Event");
                }
            }
        }
        else
        {
            Console.WriteLine("No Change in Wave");
            this.weatherDict["wave"].weatherEvent = yesterday.weatherDict["wave"].weatherEvent;
            this.weatherDict["wave"].severity = yesterday.weatherDict["wave"].severity;
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
        Console.WriteLine("Schedule for Day " + dayCount + "| Event Count: " + this.eventCount);
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

