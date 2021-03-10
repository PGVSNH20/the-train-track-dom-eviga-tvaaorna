using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Train
    {
        public int ID { get; }
        public string Name { get; }
        public int MaxSpeed { get; }
        public bool Operated { get; }

        public List<Passenger> Passengers { get; }
        
        public Train(int id, string name, int maxSpeed, bool operated)
        {
            ID = id;
            Name = name;
            MaxSpeed = maxSpeed;
            Operated = operated;

            Passengers = new List<Passenger>();
        }
    }
}