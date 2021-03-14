﻿using System;
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
                        int stationID;
                        bool isCrossing = false;
                        int trackID = -1;

                        Console.WriteLine(line);
                        char[] trackNodes = line.ToCharArray();

                        foreach(var node in trackNodes)
                        {
                            if (node == '[' || node == ']')
                            {
                                continue;
                            }
                            else if(node == '*')
                            {
                                continue;
                                //Console.WriteLine($"Train {} Start from {}");
                            }
                            else
                            {
                                int.TryParse(node.ToString(), out stationID);
                                trackID++;
                                //Console.WriteLine(stationID);
                            }
                            TrackList.Add(new Track(trackID, stationID, trackID, trackID, isCrossing));
                        }
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
