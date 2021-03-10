using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class TrainPlanner : ITravelPlan
    {
        List<object> ITravelPlan.TimeTable => throw new NotImplementedException();

        object ITravelPlan.Train => throw new NotImplementedException();

        public TrainPlanner(object train, object station)
        {

        }

        public TrainPlanner HeadTowards(Station station2)
        {
            
            throw new NotImplementedException();
        }

        public TrainPlanner StartTrainAt(string time)
        {

            throw new NotImplementedException();
        }

        public TrainPlanner StopTrainAt(Station station, string time)
        {

            throw new NotImplementedException();
        }

        public ITravelPlan GeneratePlan()
        {

            throw new NotImplementedException();
        }

        void ITravelPlan.Save(string path)
        {

            throw new NotImplementedException();
        }

        void ITravelPlan.Load(string path)
        {

            throw new NotImplementedException();
        }
    }
}
