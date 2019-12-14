# spatialite_sample

Sample for reading SpatiaLite database in .NET Core 3.1 on Mac/Linux/Windows

## SpatiaLite Dependencies

For Linux install libsqlite3-mod-spatialite: 

```
$ apt-get install libsqlite3-mod-spatialite
```

For Mac install libspatialite

```
$ brew install libspatialite
```

For Windows the SpatiaLite dll's are in the project spatialite_sample_lib

## Running

```
$ git clone https://github.com/bertt/spatialite_sample
$ cd spatialite_sample\src\spatialite_sample_core
$ dotnet build
$ dotnet run
```

## Todo

- Create Docker sample