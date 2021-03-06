using System;
using TrainEngine;
using System.Collections.Generic;
using System.IO;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Train track!");
            // Step 1:
            // Create Fluent API

            //List<Passenger> passengers = new List<Passenger>();

            //TimeTable timeTable1 = new TimeTable(2, 2, new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0));
            //Train train1 = new Train(2, "Golden Arrow", 120, true);

            //Station station1 = new Station(1, "Stonecro", true);
            //Station station2 = new Station(2, "Mount Juanceo", false);

            //TrainPlanner tp = new TrainPlanner();

            Operator carlos = new Operator();

            // FluentSchedule är ett vektyg för mr Carlos till att skApa en tidsplan vId hjälp av C# kod
            // Lite Skit
            //ITrainPlanner travelPlan = new TrainPlanner(train[0], station[0])
            //        .StartTrainAt("10:23")
            //        .StopTrainAt(station2, "14:53")
            //    .GeneratePlan();

            //TimeTable.Save("Data/schedule.txt", travelPlan);

            //var travelPlan1 = TimeTable.Load();
            

            #region PseudoCode

            //ITravelPlan travelPlan = new TrainPlanner()
            //    .NextStation(station2)
            //    .StartStation("10:22")
            //    .EndStation(station2, "14:52")
            //.GeneratePlan();

            // Step 2:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 3:
            // Make the trains run in treads

            /*
             //////////////////////////////////////////////////////////////////
            ---LOADING EVERYTHING NEEDED--- Own function(s) to avoid clutter?

            //How do we handle passengers? "Passenger" class that can move between stations and trains?
            //E.g. passenger1 gets on a train at station1, moved from passenger list of station1 to train.

            //Class train with a passenger list, name and so on
            Train trains[] = Load(Data/trains.json);

            //Each trackpiece could link to what is currently on it (trains/stations). 
            //Track before/after and sidetrack (if railroad switch) to chain all tracks together.
            Track tracks[] = Load(Data/traintrack.json);

            //If a train moves to another trackpiece, the new track takes over the train (links).
            //The old track's link to the train is removed.

            //Stations contain passenger lists? Name of station. Set position on track where?
            Station stations[] = Load(Data/stations.json);

            //Timetable should contain how the trains are moving by executing other methods?
            //Needs checks so one train isn't supposed to be in multiple places simultaneously.
            List<Timetable> timetable(s) = Load(Data/timetable.json);

            //ControllerLog stuff somewhere/somehow.




            ///////////OTHER COMMENTS
            -How should we handle time?
            -A train moving is its own thread? The entire trip? One thread per train staying active/idle?
            -Every train-track that needs to be passed could mean X travel time.
            -Time passed using Thread.Sleep?
            -Operator/Carlos gives the go when a train can keep moving?
            */

            #endregion
        }
    }
}
