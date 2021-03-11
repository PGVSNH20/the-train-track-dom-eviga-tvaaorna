using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace TrainEngine
{
    /// <summary>
    /// This is an API for Mr.Carlos, codename Operator
    /// </summary>
    public class TrainPlanner : ITravelPlan
    {
        public List<TimeTable> TimeTables { get; set; }
        public List<Train> Trains { get; }
        public TrainPlanner()
        {
            //TimeTables = new List<TimeTable>(); //Byt ut dessa två när vi fixat ORM
            //Trains = new List<Train>();

            //Trains.Add(new Train(0, "Nissetåget", 200, true));
            //TimeTables.Add((new TimeTable(0, 1, new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0))));
            //TimeTables.Add((new TimeTable(0, 2, new TimeSpan(13, 0, 0), new TimeSpan(14, 0, 0))));

            //Thread backgroundThread = new Thread(() => ExecutePlan(TimeTables));
            ////Thread backgroundThread = new Thread(() => ExecutePlan(TimeTables.Where(t => t.TrainID == 0) as List<TimeTable>));
            //backgroundThread.Start();
        }

        #region Fluent

        public TrainPlanner NextStation(Station station2)
        {
            Ingredients.Add(new Ingredient() { Name = name, Amount = amount });
            return this;
            throw new NotImplementedException();
        }

        public TrainPlanner StartStation(string time)
        {
            Ingredients.Add(new Ingredient() { Name = name, Amount = amount });
            return this;
            throw new NotImplementedException();
        }

        public TrainPlanner EndStation(Station station, string time)
        {
            Ingredients.Add(new Ingredient() { Name = name, Amount = amount });
            return this;
            throw new NotImplementedException();
        }

        public ITravelPlan GeneratePlan()
        {

            throw new NotImplementedException();
        }

        #endregion

        public void ExecutePlan(List<TimeTable> plan)
        {
            foreach (var t in plan)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{Trains[t.TrainID].Name} arrived at {t.ArrivalTime} to station {t.StationID}");
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