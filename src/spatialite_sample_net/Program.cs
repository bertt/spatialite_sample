using System;
using System.Data.SQLite;

namespace spatialite_sample_net
{
    class Program
    {
        static void Main(string[] args)
        {
            string db = @"test-2.3.sqlite";

            string connectString = "Data Source=" + db + ";Version=3;";
            var connection = new SQLiteConnection(connectString);
            connection.Open();
            connection.EnableExtensions(true);
            SpatialiteLoader.Load(connection);

            string sql = "SELECT Name, ST_MINX(geometry), ST_MINY(geometry), ST_MAXX(geometry), ST_MAXY(geometry) FROM Towns ";

            using (var command = new SQLiteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        double minX = reader.GetDouble(1);
                        double minY = reader.GetDouble(2);
                        double maxX = reader.GetDouble(3);
                        double maxY = reader.GetDouble(4);

                        Console.WriteLine($"{name}: {minX}, {minY}, {maxX}, {maxY}");
                    }
                }
            }

            connection.Close();
            connection.Dispose();
        }
    }
}
