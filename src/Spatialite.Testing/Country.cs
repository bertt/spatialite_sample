using NetTopologySuite.Geometries;

namespace Spatialite.Testing
{
    public class Country
    {
        public string Name { get; set; }
        public Geometry Geometry { get; set; }
    }
}
