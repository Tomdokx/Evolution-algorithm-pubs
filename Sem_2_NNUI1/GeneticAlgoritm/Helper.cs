using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_2_NNUI1.GeneticAlgoritm
{
    public static class Helper
    {
        private static int R = 6371;
        public static double CalculateDistance(Pub a, Pub b)
        {
            return R * Math.Acos(
                Math.Sin(DegreeToRadian(a.Position.X)) * Math.Sin(DegreeToRadian(b.Position.X)) +
                Math.Cos(DegreeToRadian(a.Position.X)) * Math.Cos(DegreeToRadian(b.Position.X)) *
                Math.Cos(DegreeToRadian(a.Position.Y) - DegreeToRadian(b.Position.Y)));
        }

        private static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }

    }
}
