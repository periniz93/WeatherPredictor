using System.Runtime.CompilerServices;

// Set the number of days to generate weather schedules for
int numDays = 25;

// Initialize the random number generator
Schedule Schedule = new Schedule();

// Generate the first day's weather schedule
DailyWeather FirstDay = new DailyWeather();
Schedule.Add(FirstDay);


// Generate weather schedules for the specified number of days
for (int day = 2; day <= numDays; day++)
    {
        DailyWeather DW = new DailyWeather(FirstDay);
        Schedule.Add(DW);
        FirstDay = DW;
    }
Schedule.printSchedule();
Console.WriteLine();

