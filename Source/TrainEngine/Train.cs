using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrainEngine
{
    public class Train
    {
        public int ID { get; }
        public string Name { get; }
        public int MaxSpeed { get; }
        public bool Operated { get; }
        public int CurrentPosition { get; set; } //ID of current trackpiece
        public Thread Thread { get; set; }

        public List<Passenger> Passengers { get; }
        
        public Train(int id, string name, int maxSpeed, bool operated)
        {
            ID = id;
            Name = name;
            MaxSpeed = maxSpeed;
            Operated = operated;
            CurrentPosition = 0;

            Passengers = new List<Passenger>();
        }

    }
}