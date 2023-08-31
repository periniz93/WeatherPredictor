using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace WeatherProj {
    public class DailyWeather
    {
        private int count;
        public enum WeatherType
        {
            Water,
            Wind,
            Wave,
            Thunder
        }

        DailyWeather? Yesterday { get; set; }
        DailyWeather? Tomorrow { get; set; }

        int Count { get => count; set => count = value; }
        int DayCount { get; set; }
        int eventCount { get; set; } //number of weather events

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

        public DailyWeather()
        
        {

            /* Instantiate Attributes*/
            Yesterday = null;
            Tomorrow = null;
            Count = 0;
            DayCount = 1;
            eventCount = 0;
            weatherDict = new Dictionary<string, Weather>();
            addWeathersToDict();
            firstDay();
            Console.WriteLine();
        }

        public DailyWeather(DailyWeather yesterday)
        {
            Yesterday = yesterday; //Links Yesterday's weather to the current.
            Tomorrow = null; //Sets Tomorrow's weather object to null.
            Count = 0; //Sets the count to 0.
            yesterday.Tomorrow = this; //Sets the current weather object to the yesterday's tomorrow so that it can be edited.
            DayCount = Yesterday.DayCount + 1; //Grabs the day count from yesterday and adds 1 to it. 
            weatherDict = new Dictionary<string, Weather>(); //Instantiates a new WeatherDict
            addWeathersToDict(); //Adds the four weather types to it.
            eventCount = yesterday.eventCount; //transfer eventCount over to today
            checkWeather(yesterday);
            Console.WriteLine();
        }
        
        private void addWeathersToDict()
        {
            foreach (WeatherType type in Enum.GetValues(typeof(WeatherType)))
            {
                
                this.weatherDict.Add(type.ToString(), new Weather());
            }
        }
        public void firstDay()
        {

            //get the weaterdict dict value with water key 
            foreach (KeyValuePair<string, Weather> item in this.weatherDict)
            {
                item.Value.severity = 0;
                item.Value.weatherEvent = "None";
            }

        }
        private BaseWeather CreateWeatherObject(string weatherType)
        {

            switch (weatherType)
            {
                case "Water":
                    return new WaterWeather();
                case "Wind":
                    return new WindWeather();
                case "Thunder":
                    return new ThunderWeather();
                case "Wave":
                    return new WaveWeather();
            }
            return null;
        }
        public void checkWeather(DailyWeather yesterday)
        {


            foreach (KeyValuePair<string, Weather> kvp in this.weatherDict)
            {
                if (rollForChange()) //coin flip for weather change*
                {
                    var weatherName = kvp.Key.ToString();
                    BaseWeather baseWeather = CreateWeatherObject(kvp.Key);
                    switch (weatherName)
                    {
                        case "Water":
                            baseWeather = new WaterWeather();
                            break;
                        case "Wind":
                            baseWeather = new WindWeather();
                            break;
                        case "Thunder":
                            baseWeather = new ThunderWeather();
                            break;
                        case "Wave":
                            baseWeather = new WaveWeather();
                            break;
                    }
                    double multiplier = countEvents();
                    Dictionary<int, string> weather = baseWeather.GetWeatherEvent(multiplier);
                    this.weatherDict[kvp.Key].weatherEvent = weather.Last().Value;
                    this.weatherDict[kvp.Key].severity = weather.Last().Key;

                    if (this.weatherDict[kvp.Key].severity > 0)
                    {
                        if (!(checkYesterdayWeatherEvent(kvp.Key)))
                        {
                            incrEventCount();
                            Console.WriteLine("Incremented on " + kvp.Key + " Event");
                        }
                    }

                    else
                    {
                        if (checkYesterdayWeatherEvent(kvp.Key))
                        {
                            decrEventCount();
                            Console.WriteLine("Incremented on " + kvp.Key + " Event");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Change in " + kvp.Key);
                    this.weatherDict[kvp.Key].weatherEvent = yesterday.weatherDict[kvp.Key].weatherEvent;
                    this.weatherDict[kvp.Key].severity = yesterday.weatherDict[kvp.Key].severity;
                }
            }
        }
        /*This functionality, when implemented, will measure the distance(can be 0,1,or 2) in weather severity. Weather events should only change states from 0 to 1, 1 to 2, 2 to 1, or 1 to 0. 
         * This is to prevent the weather from changing too drastically. 
         */
        public int calculateDailyWeatherChange()
        {
            if (this.Yesterday != null)
            {
                return this.Yesterday.Count - this.Count;
            }
            else return 0;
        }

        /* This logic is to trace out increasingly less common events if you already have a few.
         * The weather sibling Gods are a proud bunch, and don't want to share their spotlight.
         * However, right now I am leaving all to be 1.
         */
        public double countEvents()
        {
            //switch (this.eventCount)
            //{
            //    case 0:
            //        return 1;
            //    case 1:
            //        return 0.85;
            //    case 2:
            //        return 0.70;
            //    case 3:
            //        return 0.55;
            //    case 4:
            //        return 0.40;
            //    default: return 1;
            //}
            return 1;
        }



        public void incrEventCount()
        {
            if (this.eventCount < 4)
            {
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
        public bool checkYesterdayWeatherEvent(string weatherType)
        {
            if (this.Yesterday?.weatherDict?[weatherType].severity == 0)
            {
                return false;
            }
            return true;
        }

        public int severityCount()
        {
            /*For each weather type in WeatherDict, if the severity is 1, add 1 to the count. If it's 2, add 2 to the count. 
             */
            int count = 0;
            foreach(KeyValuePair<string, Weather> kvp in this.weatherDict)
            {
                if (kvp.Value.severity > 0)
                {
                    this.Count += kvp.Value.severity;
                    if(kvp.Value.severity == 1)
                    {

                    }
                }
            }
            return count;
        }
        public void printDailyWeather()
        {
            Console.WriteLine("Schedule for Day " + DayCount + "| Event Count: " + this.eventCount);
            Console.WriteLine("______________________________");
            string printString = "";

            foreach (KeyValuePair<string, Weather> item in this.weatherDict)
            {
                printString += item.Value.weatherEvent + " | ";
            }
            Console.WriteLine(printString + "\n");

        }

        
    }
}

