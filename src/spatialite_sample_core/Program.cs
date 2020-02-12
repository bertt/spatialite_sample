using spatialite_sample_lib;
using System;

namespace spatialite_sample_core
{
    class Program
    {
        static void Main(string[] args)
        {
            string db = @"test-2.3.sqlite";
            string sql = "SELECT Name, ST_ASBinary(geometry) FROM Towns;";

            var points = PointsLoader.GetPoints(db, sql);
            foreach(var p in points)
            {
                Console.WriteLine(p.X + ", " + p.Y);
            }
        }

    }
}
