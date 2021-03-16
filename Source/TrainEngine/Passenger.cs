using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
    public class Passenger
    {
        public int ID { get; }
        public string Name { get; }
        
        public Passenger(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public static void MovePassengers(ref List<Passenger> fromPassengerList, ref List<Passenger> toPassengerList)
        {
            Random rndmPassenger = new Random();
            int randomFrom = rndmPassenger.Next(0, fromPassengerList.Count);
            for (int i = 0; i < randomFrom; i++)
            {
                int randomIndex = rndmPassenger.Next(0, fromPassengerList.Count);
                toPassengerList.Add(fromPassengerList[randomIndex]);
                fromPassengerList.RemoveAt(randomIndex);
            }

            int randomTo = rndmPassenger.Next(0, toPassengerList.Count);
            for (int i = 0; i < randomTo; i++)
            {
                int randomAgain = rndmPassenger.Next(0, toPassengerList.Count);
                fromPassengerList.Add(toPassengerList[randomAgain]);
                toPassengerList.RemoveAt(randomAgain);
            }

        }
    }
}
