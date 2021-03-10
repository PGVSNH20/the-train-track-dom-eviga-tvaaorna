using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace TrainEngine
{
    public class TrainPlanner : ITravelPlan
    {
        public List<TimeTable> TimeTables { get; set; }
        public List<Train> Trains { get; }

        public TrainPlanner()
        {
            TimeTables = new List<TimeTable>(); //Byt ut dessa två när vi fixat ORM
            Trains = new List<Train>();

            Trains.Add(new Train(0, "Nissetåget", 200, true));
            TimeTables.Add((new TimeTable(0, 1, new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0))));

            //Thread backgroundThread = new Thread(new ThreadStart(ExecutePlan(TimeTables.Where(t => t.TrainID == 0) as List<TimeTable>)));
        }

        #region Fluent

        public TrainPlanner NextStation(Station station2)
        {

            throw new NotImplementedException();
        }

        public TrainPlanner StartStation(string time)
        {

            throw new NotImplementedException();
        }

        public TrainPlanner EndStation(Station station, string time)
        {

            throw new NotImplementedException();
        }

        public ITravelPlan GeneratePlan()
        {

            throw new NotImplementedException();
        }

        #endregion

        public void ExecutePlan(List<TimeTable> plan)
        {
            for (int i = 0; i < plan.Count; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Arrived at {plan[i].ArrivalTime} to station {plan[i].StationID}");
            }
        }

        public void Save(string path)
        {

            throw new NotImplementedException();
        }

        public void Load(string path) //separator?
        {

            throw new NotImplementedException();
        }


    }
}