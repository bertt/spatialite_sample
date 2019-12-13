using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace spatialite_sample_net
{
	public class SpatialiteLoader
	{
		private static readonly object Lock = new object();
		
		private static bool _haveSetPath;

		public static void Load(SQLiteConnection conn)
		{
			lock (Lock)
			{
				if (!_haveSetPath)
				{
					var spatialitePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), (Environment.Is64BitProcess ? "x64" : "x86"), "spatialite");
					
					Environment.SetEnvironmentVariable("PATH", spatialitePath + ";" + Environment.GetEnvironmentVariable("PATH"));

					_haveSetPath = true;
				}
			}

			conn.LoadExtension("mod_spatialite.dll");
		}
	}
}