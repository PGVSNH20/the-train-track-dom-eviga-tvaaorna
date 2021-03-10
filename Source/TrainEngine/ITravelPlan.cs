using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        public List<TimeTable> TimeTables { get; }
        public List<Train> Trains { get; }
        public void Save(string path);
        public void Load(string path);

    }
}
