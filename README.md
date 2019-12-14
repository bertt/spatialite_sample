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
Brozolo: 427002.77, 4996361.33
Campiglione-Fenile: 367470.48, 4962414.5
Canischio: 390084.12, 5025551.73
Cavagnolo: 425246.99, 5000248.3
Magliano Alfieri: 426418.89, 4957737.37
Mango: 432661.52, 4948470.32
```

In the sample library Wkx-sharp (https://github.com/cschwarz/wkx-sharp) is used to deserialize the geometry.

## Todo

- Create Docker sample