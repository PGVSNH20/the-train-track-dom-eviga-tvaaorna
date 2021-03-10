using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Passenger
    {
        public int ID { get; }
        public string Name { get; }
        
        public Passenger(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
