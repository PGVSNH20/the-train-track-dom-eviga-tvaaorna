using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainEngine
{
    class PassengerReader
    {
        public static List<Passenger> Load()
        {
            string url = "Data/passengers.txt";
            List<Passenger> PassengerList = new List<Passenger>();
            try
            {
                using (StreamReader streamReader = new StreamReader(url))
                {
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] content = line.Split(';');

                        PassengerList.Add(new Passenger(int.Parse(content[0]), content[1]));
                    }
                }
                return PassengerList;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return PassengerList;
            }
        }
    }
}
