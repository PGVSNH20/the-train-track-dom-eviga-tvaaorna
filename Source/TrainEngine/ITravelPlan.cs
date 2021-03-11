using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        // Vad är vår travel plan?
        // Vad är alltså detta?
        public List<TimeTable> TimeTables { get; }
        public List<Train> Trains { get; }
        public void Save(string path);
        public void Load(string path);

    }
}
