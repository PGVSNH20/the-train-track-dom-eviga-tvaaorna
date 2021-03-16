using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TrainEngine
{
    // ALLA UTOM FABIAN GER FAN I DENNA
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

        private List<Thread> threads = new List<Thread>();

        private static Timer timer;
        private static TimeSpan? clock = null;

        public TrainPlanner(List<Train> trains) //Espressomachine
        {
            Trains = trains;
        }

        ~TrainPlanner()
        {
            timer.Stop();
            timer.Dispose();
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

        public void StartTrains(List<TimeTable> timeTables)
        {
            HashSet<int> trainIDs = new HashSet<int>();
            foreach (var tt in timeTables)
            {
                if (!trainIDs.Contains(tt.TrainID))
                {
                    trainIDs.Add(tt.TrainID);
                }
            }

            foreach (var trainID in trainIDs)
            {
                var currentTimeTables = timeTables.Where(t => t.TrainID == trainID).ToList();
                var newThread = Trains.Find(x => x.ID == trainID).Thread = new Thread(() => ExecutePlan(currentTimeTables));

                Console.WriteLine(Trains.Find(x => x.ID == trainID).Thread.Name);

                threads.Add(newThread);
                newThread.Start();
            }
        }

        public void ExecutePlan(List<TimeTable> plan) //TODO: Time based on travel-distance with track-pieces
        {
            //TODO: Only once
            
            if(clock == null)
            {
                clock = plan[0].DepartureTime.GetValueOrDefault();
                SetTimer();
            }

            TimeSpan? actualArrivalTime = null;
            //Sort somewhere
            foreach (var t in plan)
            {
                int idleTime = (int)ConvertTime(t.ArrivalTime.GetValueOrDefault()).TotalMilliseconds -
                               (int)ConvertTime(actualArrivalTime.GetValueOrDefault()).TotalMilliseconds;
                
                Thread.Sleep(idleTime);
                actualArrivalTime = clock;
                Console.WriteLine($"{Trains[t.TrainID].Name} arrived at {actualArrivalTime} to station {t.StationID}");
                Console.WriteLine($"Actual arrival: {actualArrivalTime} | Travel time (ms): {idleTime} | Scheduled time: {t.ArrivalTime.GetValueOrDefault()}");
            }
        }

        private TimeSpan ConvertTime(TimeSpan time)
        {
            TimeSpan result = new TimeSpan(0, time.Hours, time.Minutes);
            return result;
        }

        private static void SetTimer()
        {
            // Create a timer with an interval in ms.
            timer = new Timer(1000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            clock += TimeSpan.FromMinutes(1.0);
        }
    }
}