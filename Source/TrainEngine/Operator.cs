using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    class Operator
    {
        // mr. Carlos
        Train train1 = new Train("Any train");
        Station station1 = new Station("Gothenburg");
        Station station2 = new Station("Stockholm");

        ITravelPlan travelPlan = new TrainPlanner(train1, station1)
            .HeadTowards(station2)
            .StartTrainAt("10:22")
            .StopTrainAt(station2, "14:52")
        .GeneratePlan();
    }
}
