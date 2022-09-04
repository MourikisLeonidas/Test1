using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    internal class CoordinateModel
    {
        public int Id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public double DistanceFromPrevious { get; set; }
        public double DistanceFromStart { get; set; }
    }
}
