using System;
using System.Runtime.CompilerServices;

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
        checkWeather();
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
    public void checkWeather(DailyWeather yesterday)
    {
        

    }

    public void CheckWeather()
    {
        
    }

    public void weatherMultiplier(int count)
    {

    }

    public bool checkYesterdayWater()
    {
        if (this.Yesterday.water?.Count == 0)
        {
            return false;
        }
        return true; 
    }

    public bool checkYesterdayWind()
    {
        if (this.Yesterday.wind?.Count == 0)
        {
            return false;
        }
        return true;
    }
    public bool checkYesterdayWave()
    {
        if (this.Yesterday.wave?.Count == 0)
        {
            return false;
        }
        return true;
    }

    public bool checkYesterdayThunder()
    {
        if (this.Yesterday.thunder.Count == 0)
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
        if (this.water.Count == 1) 
        {
            printString += this.water.Values.First() + " | ";
        }
        if (this.wind.Count == 1)
        {
            printString += this.wind.Values.First() + " | ";
        }
        if (this.thunder.Count == 1)
        {
            printString += this.thunder.Values.First() + " | ";
        }
        if (this.wave.Count == 1)
        {
            printString += this.wave.Values.First()+" | ";
        }
        Console.WriteLine(printString+ "\n");

    }

    public void printProbabilities()
    {
        Console.WriteLine();
    }
}

