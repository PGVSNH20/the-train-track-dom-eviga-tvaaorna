using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainEngine.FileReaders
{
    class TrainReader
    {
            public static List<Train> Load()
            {
                string url = "Data/trains.txt";
                List<Train> TrainList = new List<Train>();
                try
                {
                    using (StreamReader streamReader = new StreamReader(url))
                    {
                        string line;

                        line = streamReader.ReadLine();
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string[] content = line.Split(',');

                            TrainList.Add(new Train(int.Parse(content[0]), content[1], int.Parse(content[2]), bool.Parse(content[3])));
                        }
                    }
                    return TrainList;
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file " + url + " could not be read:");
                    Console.WriteLine(e.Message);
                    return TrainList;
                }
        }
    }
}
