using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TrainEngine
{
    public class TimeTable
    {
        public virtual int TrainID { get; }
        public virtual int StationID { get; }
        public virtual TimeSpan? DepartureTime { get; }
        public virtual TimeSpan? ArrivalTime { get; }
            
        public TimeTable(int trainID, int stationID, TimeSpan? departureTime, TimeSpan? arrivalTime)
        {
            TrainID = trainID;
            StationID = stationID;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public static List<TimeTable> Load()
        {
            string path = "Data/timetable.txt";

            try
            {
                string[] data = File.ReadAllLines(path);
                List<TimeTable> result = new List<TimeTable>();

                foreach (var line in data.Skip(1))
                {
                    string[] content = line.Split(',');

                    int.TryParse(content[0], out var newTrainId);
                    int.TryParse(content[1], out var newStationId);
                    
                    string[] timeSplit = content[2].Split(':');
                    TimeSpan? newDepartureTime = null;
                    if (timeSplit[0] != "null")
                    {
                        newDepartureTime = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                    }

                    timeSplit = content[3].Split(':');
                    TimeSpan? newArrivalTime = null;
                    if (timeSplit[0] != "null")
                    {
                        newArrivalTime = new TimeSpan(int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);
                    }

                    TimeTable current = new TimeTable(
                        newTrainId,
                        newStationId,
                        newDepartureTime,
                        newArrivalTime);

                    result.Add(current);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }

            throw new Exception("Encountered an error while loading file");
        }

        public static List<TimeTable> Save()
        {
            string path = "Data/controllerlog.txt";
            List<TimeTable> timeTables = TimeTable.Load();

            using (StreamWriter streamWriter = new StreamWriter(path))
            {              
                foreach (var timeTable in timeTables)
                {
                    
                }
            }   
        }
    }
}