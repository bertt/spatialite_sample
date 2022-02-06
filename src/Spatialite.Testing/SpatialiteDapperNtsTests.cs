using Dapper;
using Microsoft.Data.Sqlite;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Spatialite.Testing
{
    public class SpatialiteDapperNtsTests
    {
        private string db = @"testfixtures/countries.sqlite";

        [Test]
        public async Task ReadSpatialiteDataTest()
        {
            SqlMapper.AddTypeHandler(new GeometryTypeHandler());
            string sql = "SELECT name, ST_ASBinary(GEOMETRY) as geometry FROM countries";

            string connectString = "Data Source=" + db;
            Loader.EnsureLoadable(package: "mod_spatialite",library: "mod_spatialite");

            var connection = new SqliteConnection(connectString);
            connection.LoadExtension("mod_spatialite");
            await connection.OpenAsync();
            var countries = await connection.QueryAsync<Country>(sql);
            Assert.IsTrue(countries.AsList().Count == 245);
            Assert.IsTrue(countries.First().Name == "Andorra");
            connection.Close();
        }
    }
}