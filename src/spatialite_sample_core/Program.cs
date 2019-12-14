using spatialite_sample_net;
using System;
using System.Data.SQLite;
using Wkx;

namespace spatialite_sample_core
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

#if Windows
             SpatialiteLoader.Load(connection);
#elif OSX || Linux
             connection.LoadExtension("mod_spatialite");
#endif

            string sql = "SELECT Name, ST_ASBinary(geometry) FROM Towns ";

            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var geometry = Geometry.Deserialize<WkbSerializer>((byte[])reader[1]);
                var point = (Point)geometry;
                Console.WriteLine($"{name}: {point.X}, {point.Y}");
            }

            connection.Close();
            connection.Dispose();
        }
    }
}
