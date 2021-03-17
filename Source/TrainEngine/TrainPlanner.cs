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
    //Klotter
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
        private static double speed = 0.1; //lower is faster

        private static Timer tracker;

        public TrainPlanner(List<Train> trains) //Espressomachine
        {
            Trains = trains;
        }

        ~TrainPlanner() //Destructor
        {
            timer.Stop();
            timer.Dispose();

            tracker.Stop();
            tracker.Dispose();
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
            TimeSpan? actualArrivalTime = null;

            plan = plan.OrderBy(p => p.ArrivalTime).ToList();

            //TODO: Only once
            if (clock == null)
            {
                clock = plan[0].DepartureTime.GetValueOrDefault();
                SetTimer();
            }

            int lastStation = 1;

            for (int i = 0; i < plan.Count-1; i++) //(var t in plan)
            {
                int idleTime = (int)ConvertTime(plan[i+1].ArrivalTime.GetValueOrDefault()).TotalMilliseconds -
                               (int)ConvertTime(plan[i].DepartureTime.GetValueOrDefault()).TotalMilliseconds;
                //float idleTime = CalculateTrainSpeed(t.)
                if (idleTime < 0)
                {
                    idleTime = 0;
                }
                int tpbs = TrackManager.TrackPiecesBetweenStations(lastStation, plan[i].StationID);
                for (int j = 0; j < tpbs; j++)
				{
                    Console.WriteLine($"Train {Trains[plan[i].TrainID].Name} has passed train track {j}.");
                    Thread.Sleep((int)(idleTime * speed));
                }
                lastStation = plan[i].StationID;

                //Thread.Sleep((int)(idleTime * speed));
                //Fix later
                //Passenger.MovePassengers(ref Trains[t.TrainID].Passengers, Operator.stations[t.StationID].Passengers);

                actualArrivalTime = clock;
                Console.WriteLine($"{Trains[plan[i].TrainID].Name} arrived at {actualArrivalTime} to station {plan[i].StationID}");
                Console.WriteLine($"Actual arrival: {actualArrivalTime} | Travel time (ms): {idleTime} | Scheduled time: {plan[i].ArrivalTime.GetValueOrDefault()}");
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
            timer = new Timer(1000 * speed);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            clock += TimeSpan.FromMinutes(1.0);
            //Console.WriteLine("chugga");

            TimeSpan choo = (TimeSpan)clock;
            if (choo.Minutes % 7 == 0)
            {
                //Console.WriteLine("choo choo!");
            }
        }

        private void SetTracker(int time)
		{
            // Create a timer with an interval in ms.
            tracker = new Timer(time * speed);
            // Hook up the Elapsed event for the timer. 
            tracker.Elapsed += OnTrackerEvent;
            tracker.AutoReset = false;
            tracker.Enabled = true;

            //float trackPieceTime = trackPieceLength / time; //How long it takes to traverse a trackpiece
        }

        private static void OnTrackerEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("mlem");
        }

        public static double CalculateTrainSpeed(int trackPieces, float maxSpeed, float startStationTime, float endStationTime)
        {
            float trackPieceLength = 2; //Km

            float trackLength = trackPieces * trackPieceLength; //Gets how far the train has to travel

            float time = endStationTime - startStationTime; //How long it has between stations

            float calcSpeed = trackLength / time; //How fast it has to go to make it to the next station

            if (calcSpeed > maxSpeed)
            {
                Console.WriteLine($"You need to go too fast for the current train! Speed is capped to {maxSpeed}!");
                return maxSpeed;
            }
            return calcSpeed;
        }
    }
}