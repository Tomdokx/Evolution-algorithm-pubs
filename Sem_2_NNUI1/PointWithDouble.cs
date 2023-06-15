using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_2_NNUI1
{
    public class PointWithDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string? ToString()
        {
            return $"X - {X} , Y - {Y}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is PointWithDouble)) return false;
            PointWithDouble PointToCompare = (PointWithDouble)obj;
            return PointToCompare.X == this.X && PointToCompare.Y == this.Y;
        }
    }
}
