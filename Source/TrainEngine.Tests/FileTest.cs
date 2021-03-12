using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TrainEngine;

namespace TrainEngine.Tests
{
    public class FileTest
    {
        [Fact]
        public void CanReadTimeTable()
        {
            private List<Passenger> timeTables;
            timeTables = TimeTable.Load();
            Assert.NotNull(timeTables);
        }
        [Fact]
        public void CanReadStations()
        {

        }
        [Fact]
        public void CanReadPassengers()
        {
            Assert.
        }
        [Fact]
        public void CanReadTrains()
        {

        }
    }
}
