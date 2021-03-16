﻿using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.FileReaders;

namespace TrainEngine
{
    class TrackManager
    {
        private static List<Track> tracks;
        public TrackManager()
        {
            tracks = TrainTrackReader.Load();

            //foreach(var track in tracks)
            //{
            //    Console.Write(track);
            //}
        }
        public static int AmountTrackPieces(int firstStationID, int nextStationID)
        {
            var firstStation = tracks.Find(x => x.StationID == firstStationID).TrackID;
            var nextStation = tracks.Find(x => x.StationID == nextStationID).TrackID;

            return nextStation - firstStation;
        }

        // TODO: fix name, and formula for timecounting
        public static void TrainProgression(int distance, double speed)
        {
            float trackPieceLength = 1;


        }
    }
}
