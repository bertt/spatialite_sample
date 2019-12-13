using SpatialiteSharp;
using System;
using System.Data.SQLite;

namespace spatialite_sample_net
{
    class Program
    {
        static void Main(string[] args)
        {
            string destDbFilename = @"test-2.3.sqlite";

            string connectString = "Data Source=" + destDbFilename + ";Version=3;";
            var connection = new SQLiteConnection(connectString);
            connection.Open();
            connection.EnableExtensions(true);
            SpatialiteLoader.Load(connection);

            string sql = "SELECT ST_MINX(geometry), ST_MINY(geometry), ST_MAXX(geometry), ST_MAXY(geometry) FROM Towns ";

            using (var command = new SQLiteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        double minX = reader.GetDouble(0);
                        double minY = reader.GetDouble(1);
                        double maxX = reader.GetDouble(2);
                        double maxY = reader.GetDouble(3);

                        Console.WriteLine($"{minX}, {minY}, {maxX}, {maxY}");
                    }
                }
            }

            connection.Close();
            connection.Dispose();
            Console.ReadKey();
        }
    }
}
