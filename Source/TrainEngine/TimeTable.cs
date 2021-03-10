using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class TimeTable
    {
        public int TrainID { get; }
        public int StationID { get; }
        public TimeSpan DepartureTime { get; }
        public TimeSpan ArrivalTime { get; }

        public TimeTable(int trainID, int stationID, TimeSpan departureTime, TimeSpan arrivalTime)
        {
            TrainID = trainID;
            StationID = stationID;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }
}