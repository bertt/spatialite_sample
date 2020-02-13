using Microsoft.Data.Sqlite;
using spatialite_sample_net;
using System.Collections.Generic;
using Wkx;

namespace spatialite_sample_lib
{
    public class PointsLoader
    {
        public static List<Point> GetPoints(string db, string sql)
        {
            string connectString = "Data Source=" + db;
            var connection = new SqliteConnection(connectString);
            connection.Open();
            connection.EnableExtensions(true);

            #if Windows
                SpatialiteLoader.Load(connection);
            #elif OSX || Linux
                connection.LoadExtension("mod_spatialite");
            #endif

            var command = new SqliteCommand(sql, connection);
            var reader = command.ExecuteReader();
            var points = new List<Point>(); 
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var geometry = Geometry.Deserialize<WkbSerializer>((byte[])reader[1]);
                var point = (Point)geometry;
                points.Add(point);
            }

            connection.Close();
            connection.Dispose();

            return points;
        }
    }
}
