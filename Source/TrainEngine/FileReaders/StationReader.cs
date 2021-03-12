using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainEngine.FileReaders
{
    class StationReader
    {
        public static List<Station> Load()
        {
            string url = "Data/stations.txt";
            List<Station> stations = new List<Station>();

            try
            {
                using (StreamReader streamReader = new StreamReader(url))
                {
                    string line;
                    line = streamReader.ReadLine();
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] content = line.Split('|');

                        stations.Add(new Station(int.Parse(content[0]), content[1], bool.Parse(content[2])));
                    }
                }
                return stations;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The file {url} could not be read:");
                Console.WriteLine(e.Message);
                return stations;
            }

        }
    }
}
