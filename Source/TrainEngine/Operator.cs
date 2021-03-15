using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.FileReaders;

namespace TrainEngine
{
    public class Operator
    {
        // mr. Carlos (Mulle Meck?)

        //Gets a list with timetables
        //Can create/save new timetables
        //Assign plans to trains here, not in trainplanner, nuh uh
        //He needs the POWER...to start and stop trains
        //So he also needs a way to check the track for issues
        //Om det snöar en decimeter, stäng ner allt!!! LAPPMÖGEL!!!
        //Behöver öråd för att bestämma om Mr.Carlos är egentligen Mulle Meck

        private List<TimeTable> timeTables;
        private List<Passenger> passengers;
        private List<Station> stations;
        private List<Train> trains;
        private TrackManager track = new TrackManager();
        private TrainPlanner trainPlanner;

        public Operator()
        {
            timeTables = TimeTable.Load();
            passengers = PassengerReader.Load();
            stations = StationReader.Load();
            trains = TrainReader.Load();
            trainPlanner = new TrainPlanner(trains);

            //////////////////////

            trainPlanner.StartTrains(timeTables);

            TimeTable.Save();
        }
        
        //plan.NextStation(station1).NextStation(station2).NextStation(station1).NextStation(station2)


    }
}
