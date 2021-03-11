using System;
using System.Collections.Generic;
using System.Text;

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

        public Operator()
        {
            timeTables = TimeTable.Load();
        }

        //plan.NextStation(station1).NextStation(station2).NextStation(station1).NextStation(station2)


    }
}
