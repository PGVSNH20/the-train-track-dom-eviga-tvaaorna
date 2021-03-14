using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.FileReaders;

namespace TrainEngine
{
    class TrackManager
    {
        private List<Track> tracks;
        public TrackManager()
        {
            tracks = TrainTrackReader.Load();

            foreach(var track in tracks)
            {
                Console.Write(track);
            }
        }
    }
}
