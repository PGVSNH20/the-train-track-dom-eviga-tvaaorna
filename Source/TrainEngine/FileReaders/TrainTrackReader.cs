using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainEngine.FileReaders
{
    class TrainTrackReader
    {
        public static List<Track> Load()
        {
            string url = "Data/traintrack1.txt";
            List<Track> TrackList = new List<Track>();
            try
            {
                using (StreamReader streamReader = new StreamReader(url))
                {
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] content = line.Split(',');


                        //TrackList.Add(new Track(int.Parse(content[0]), content[1], int.Parse(content[2]), bool.Parse(content[3])));
                    }
                }
                return TrackList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The file {url} could not be read:");
                Console.WriteLine(e.Message);
                return TrackList;
            }
        }
    }
}
