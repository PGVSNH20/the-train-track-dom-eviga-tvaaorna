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
    public class TrainPlanner : ITrainPlanner
    {
        public List<TimeTable> TimeTables { get; set; }
        public List<Train> Trains { get; }

        public int TrainID { get; set; }
        public int StationID { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public TrainPlanner() //Espressomachine
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

            return this;
        }

        public TrainPlanner StartStation(TimeSpan time)
        {
            DepartureTime = time;
            return this;
        }

        public TrainPlanner EndStation(int station, TimeSpan time)
        {
            StationID = station;
            ArrivalTime = time;
            return this;
        }

        public TimeTable GeneratePlan()
        {
            return new TimeTable(TrainID, StationID, DepartureTime, ArrivalTime);
        }

        #endregion

        public void ExecutePlan(List<TimeTable> plan)
        {
            foreach (var t in plan)
            {
                Thread.Sleep(1000); //fix time
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