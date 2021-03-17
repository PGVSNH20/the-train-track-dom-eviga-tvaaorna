using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TrainEngine
{
    /// <summary>
    /// This is an API for Mr.Carlos, codename Operator, better known as Mulle Meck
    /// </summary>
    public class TrainPlanner : ITrainPlanner
    {
        //public List<TimeTable> TimeTables { get; set; }
        public List<Train> Trains { get; }
        public int TrainID { get; set; }
        public int StationID { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        private static List<Thread> threads = new List<Thread>();

        private static Timer timer;
        //private static TimeSpan? clock = null;
        //private static double speed = 0.1; //lower is faster

        //private static Timer tracker;

        //DEBUGGING
        private static double offsetInSeconds; //Change so realtime starts when the timetables start
        private static double timeModifier = 30; //Move time forward every second by N seconds

        public TrainPlanner(List<Train> trains)
        {
            Trains = trains;
        }

        ~TrainPlanner() //Destructor
        {
            timer.Stop();
            timer.Dispose();

            //tracker.Stop();
            //tracker.Dispose();
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

        /// <summary>
        /// Start threads for all trains that makes them follow their schedules.
        /// </summary>
        /// <param name="timeTables">All timetables that the trains move accordingly to.</param>
        public void StartTrains(List<TimeTable> timeTables)
        {
            //Check which trains that are in the timetables
            HashSet<int> trainIDs = new HashSet<int>();
            foreach (var tt in timeTables)
            {
                if (!trainIDs.Contains(tt.TrainID))
                {
                    trainIDs.Add(tt.TrainID);
                }
            }

            //Sort list to make everything in the right order
            timeTables = timeTables.OrderBy(p => p.ArrivalTime).ToList();

            //DEBUGGING: Makes only one train do something
            //trainIDs = new HashSet<int>();
            //trainIDs.Add(2);

            //Calculate how big the difference between real time and the first timetable's starting point 
            TimeSpan dT = timeTables[0].DepartureTime.GetValueOrDefault();
            offsetInSeconds = DateTime.Now.TimeOfDay.TotalSeconds - dT.TotalSeconds;
            SetTimer();

            //Start a new thread for each train
            foreach (var trainID in trainIDs)
            {
                var currentTimeTables = timeTables.Where(t => t.TrainID == trainID).ToList();
                var newThread = Trains.Find(x => x.ID == trainID).Thread = new Thread(() => ExecutePlan(currentTimeTables));

                Console.WriteLine(Trains.Find(x => x.ID == trainID).Thread.Name);

                threads.Add(newThread);
                newThread.Start();
            }
        }

        /// <summary>
        /// Makes a train move according to a set of schedules.
        /// </summary>
        /// <param name="timeTables">List of time-tables that should be executed. Should only contain one trainID</param>
        public void ExecutePlan(List<TimeTable> timeTables)
        {
            //TODO: Add checks for... | Only one trainID in timeTables | Stations actually exist on the route

            bool waiting;
            int trainID = timeTables[0].TrainID;
            TimeSpan dayStartedAt = GetCurrentTime();

            for (int i = 0; i < timeTables.Count - 1; i++)
            {
                TimeSpan nextArrival = timeTables[i + 1].ArrivalTime.GetValueOrDefault();
                TimeSpan nextDeparture = timeTables[i].DepartureTime.GetValueOrDefault();
                int currentStation = timeTables[i].StationID;
                int nextStation = timeTables[i+1].StationID;
                int tracksLeftUntilDestination = TrackManager.TrackPiecesBetweenStations(currentStation, nextStation);

                //Train currently at a station, wait for next departure
                do
                {
                    var time = GetCurrentTime(); //Debugging
                    if (GetCurrentTime() >= nextDeparture)
                    {
                        waiting = false;
                    }
                    else
                    {
                        Console.WriteLine($"{Trains[trainID].Name}: Time left until departure at station {currentStation} is {nextDeparture - GetCurrentTime()}.");
                        waiting = true;
                        Thread.Sleep(1000);
                    }
                } while (waiting);

                TimeSpan leftAtTime = GetCurrentTime();

                Console.WriteLine($"{Trains[trainID].Name} departed from station {currentStation}!");
                Console.WriteLine($"{Trains[trainID].Name} will arrive at station {nextStation} after passing {tracksLeftUntilDestination} tracks");

                TimeSpan expectedTravelTime = nextArrival - nextDeparture;
                TimeSpan expectedTrackTravelTime = expectedTravelTime / tracksLeftUntilDestination;
                Console.WriteLine($"Expected time between station {currentStation} and {nextStation} is {expectedTravelTime} ({expectedTrackTravelTime} per track).");
                
                for (int j = 0; j < tracksLeftUntilDestination; j++)
                {
                    //TODO: Check blocked road | trackthingy[j + whereTheFirstTrackIs].IsPassable;
                    do
                    {
                        var time = GetCurrentTime(); //Debugging
                        if (GetCurrentTime() >= leftAtTime + (j + 1) * expectedTrackTravelTime)
                        {
                            Console.WriteLine($"- {Trains[trainID].Name} is at track {j + 1}/{tracksLeftUntilDestination}, expected time left {expectedTravelTime - expectedTrackTravelTime * (j + 1)}");
                            waiting = false;
                        }
                        else
                        {
                            waiting = true;
                            Thread.Sleep(1000);
                        }
                    } while (waiting);
                }
                Console.WriteLine($"{Trains[trainID].Name} arrived at station {nextStation}! It took {GetCurrentTime() - leftAtTime}");
            }
            Console.WriteLine($"{Trains[trainID].Name} is done for the day, it was in traffic for {GetCurrentTime() - dayStartedAt}");
        }

        /// <summary>
        /// Modifies time to avoid having to wait half a day for the program to do anything.
        /// </summary>
        /// <returns>Current time with an offset</returns>
        private TimeSpan GetCurrentTime()
        {
            return DateTime.Now.TimeOfDay - TimeSpan.FromSeconds(offsetInSeconds);
        }

        /// <summary>
        /// Starts a timer that alters the speed of time
        /// </summary>
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
            offsetInSeconds -= timeModifier;
        }

        ///////////OLD STUFF BELOW///////////
        /*
        
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

            for (int i = 0; i < plan.Count - 1; i++) //(var t in plan)
            {
                if (plan[i].StationID == 4)
                {
                    continue;
                }
                //TODO: Om destinationsstationen är samma som den vi är på, vänta tills klockan är departure
                //double idleTime = ConvertTime(plan[i + 1].ArrivalTime.GetValueOrDefault()).TotalMilliseconds -
                //                  ConvertTime(plan[i].DepartureTime.GetValueOrDefault()).TotalMilliseconds;
                double idleTime = (plan[i + 1].ArrivalTime.GetValueOrDefault().TotalMilliseconds -
                                  plan[i].DepartureTime.GetValueOrDefault().TotalMilliseconds) / 60;
                //float idleTime = CalculateTrainSpeed(t.)
                if (idleTime < 0)
                {
                    idleTime = 0;
                }
                int tpbs = TrackManager.TrackPiecesBetweenStations(lastStation, plan[i].StationID);
                if (tpbs != 0)
                {
                    for (int j = 0; j < tpbs; j++)
                    {
                        Thread.Sleep((int)Math.Floor(idleTime * speed));
                        //SetTracker(idleTime);
                        //try
                        //{
                        //    Thread.Sleep(100000000);
                        //}
                        //catch
                        //{
                        //}
                        Console.WriteLine($"Train {Trains[plan[i].TrainID].Name} has passed train track {j} at {clock}.");
                    }
                }
                else
                {
                    Console.WriteLine($"{Trains[plan[i].TrainID].Name} started running at {clock} from station {plan[i].StationID}");
                    Thread.Sleep((int)(idleTime * speed));
                }
                lastStation = plan[i].StationID;

                //Thread.Sleep((int)(idleTime * speed));
                //Fix later
                //Passenger.MovePassengers(ref Trains[t.TrainID].Passengers, Operator.stations[t.StationID].Passengers);

                actualArrivalTime = clock;
                Console.WriteLine($"{Trains[plan[i].TrainID].Name} arrived at {actualArrivalTime} to station {plan[i].StationID}");
                TimeSpan scheduledTime = plan[i].ArrivalTime == null
                    ? plan[i].ArrivalTime.GetValueOrDefault()
                    : plan[i].DepartureTime.GetValueOrDefault();
                Console.WriteLine($"Actual arrival: {actualArrivalTime} | Travel time (ms): {idleTime} | Scheduled time: {scheduledTime}");
            }
        }
        
        private TimeSpan ConvertTime(TimeSpan time)
        {
            TimeSpan result = new TimeSpan(0, time.Hours, time.Minutes);
            return result;
        }

        //private static void SetTimer()
        //{
        //    // Create a timer with an interval in ms.
        //    timer = new Timer(1000 * speed);
        //    // Hook up the Elapsed event for the timer. 
        //    timer.Elapsed += OnTimedEvent;
        //    timer.AutoReset = true;
        //    timer.Enabled = true;
        //}

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    clock += TimeSpan.FromMinutes(1.0);
        //    //Console.WriteLine("chugga");

        //    TimeSpan choo = (TimeSpan)clock;
        //    if (choo.Minutes % 7 == 0)
        //    {
        //        //Console.WriteLine("choo choo!");
        //    }
        //}

        private void SetTracker(double time)
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
            threads[0].Interrupt();
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
        */
    }
}