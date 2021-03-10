using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Station
    {
        public int ID { get; }
        public string StationName { get; }
        public bool EndStation { get; }

        public Station(int id, string stationName, bool endStation)
        {
            ID = id;
            StationName = stationName;
            EndStation = endStation;
        }
    }
}
