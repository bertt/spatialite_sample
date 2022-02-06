# spatialite_sample

Sample for reading SpatiaLite database in .NET 6.0 on Mac/Linux/Windows

## Data preparation

Sample data: https://github.com/google/dspl/blob/master/samples/google/canonical/countries.csv

```
country,latitude,longitude,name
AD,42.546245,1.601554,Andorra
AE,23.424076,53.847818,"United Arab Emirates"
AF,33.93911,67.709953,Afghanistan
```

Convert to from countries.csv to Spatialite countries.sqlite:

```
$ ogr2ogr -f sqlite -dsco spatialite=yes -oo X_POSSIBLE_NAMES=Longitude -oo Y_POSSIBLE_NAMES=Latitude countries.sqlite countries.csv
```

List the tables:

```
$ sqlite3 countries.sqlite
sqlite> .tables
ElementaryGeometries                spatial_ref_sys_all
KNN                                 spatial_ref_sys_aux
SpatialIndex                        spatialite_history
countries                           sql_statements_log
data_licenses                       vector_layers
geom_cols_ref_sys                   vector_layers_auth
geometry_columns                    vector_layers_field_infos
geometry_columns_auth               vector_layers_statistics
geometry_columns_field_infos        views_geometry_columns
geometry_columns_statistics         views_geometry_columns_auth
geometry_columns_time               views_geometry_columns_field_infos
idx_countries_GEOMETRY              views_geometry_columns_statistics
idx_countries_GEOMETRY_node         virts_geometry_columns
idx_countries_GEOMETRY_parent       virts_geometry_columns_auth
idx_countries_GEOMETRY_rowid        virts_geometry_columns_field_infos
spatial_ref_sys                     virts_geometry_columns_statistics
```

Describe table countries:

```
sqlite> pragma table_info(countries);
0|ogc_fid|INTEGER|0||1
1|country|VARCHAR|0||0
2|latitude|FLOAT|0||0
3|longitude|FLOAT|0||0
4|name|VARCHAR|0||0
5|GEOMETRY|POINT|0||0
```

To query the geometry column, use command line tool 'spatialite':

```
$ spatialite
spatialite> select astext(geometry) from countries limit 10;
POINT(1.601554 42.546245)
POINT(53.847818 23.424076)
POINT(67.709953 33.93911)
POINT(-61.796428 17.060816)
POINT(-63.068615 18.220554)
POINT(20.168331 41.153332)
POINT(45.038189 40.069099)
POINT(-69.060087 12.226079)
POINT(17.873887 -11.202692)
POINT(-0.071389 -75.250973)
```

 alternative method: use a recent version of sqlite3 with Load Extensions support and load the SpatiaLite extensions:
 
 ```
 $ .load /usr/local/lib/mod_spatialite.dylib
 ```

## Running

For code see SpatialiteDapperNtsTests.cs

```
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
            var country1 = countries.First();
            Assert.IsTrue(country1.Name == "Andorra");
            var p = (Point)country1.Geometry;
            Assert.IsTrue(p.X == 1.601554 && p.Y == 42.546245);
            connection.Close();
        }
    }
```

Code dependencies:

- NetTopologySuite.Geometries - for deserialize the geometry

https://nettopologysuite.github.io/NetTopologySuite/api/NetTopologySuite.Geometries.html

- Dapper 2.0.123

https://github.com/DapperLib/Dapper

- Microsoft.Data.Sqlite 6.0.1

- mod_spatialite 4.3.0.100-rc1

https://github.com/bricelam/mod_spatialite-NuGet
