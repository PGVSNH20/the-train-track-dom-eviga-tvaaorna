using System.Collections.Generic;

namespace TrainEngine
{
    public class Track
    {   
        //public int NumberOfTrackParts { get; set; }
        public int TrackID { get; }
        public int StationID { get; set; } // För att kolla om det är en station eller crossing place
        private int? PreviousTrackID { get; set; }
        private int? NextTrackID { get; set; }
        private bool Blocked { get; set; }

        public Track(int trackID, int stationID, int? previousTrackID, int? nextTrackID, bool blocked)
        {
            TrackID = trackID;
            StationID = stationID;
            PreviousTrackID = previousTrackID;
            NextTrackID = nextTrackID;
            Blocked = blocked;
        }
    }
}