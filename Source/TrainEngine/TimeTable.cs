using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TrainEngine
{
    public class TimeTable
    {
        public virtual int TrainID { get; }
        public virtual int StationID { get; }
        public virtual TimeSpan DepartureTime { get; }
        public virtual TimeSpan ArrivalTime { get; }

        public TimeTable(int trainID, int stationID, TimeSpan departureTime, TimeSpan arrivalTime)
        {
            TrainID = trainID;
            StationID = stationID;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public List<TimeTable> Load()
        {
            if (File.Exists("Data/timetable.txt"))
            {
                string[] data = File.ReadAllLines("Data/timetable.txt");
                List<TimeTable> result = new List<TimeTable>();

                foreach (var line in data)
                {
                    string[] content = line.Split(',');
                    int newID; //continue later
                    Int32.TryParse(content[0], out newID);
                    TimeTable current = new TimeTable();
                }
            }
            else
            {
                throw new Exception("Error reading file");
            }
        }
    }
}