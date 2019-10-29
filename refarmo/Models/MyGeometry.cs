using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace refarmo.Models
{
    public class MyGeometry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }


        //DB ignore
        [NotMapped]
        public double[][][] coordinates { get; set; }

        //JSON ignore
        [JsonIgnore]
        public Geometry PolygonCoordinates
        {
            get
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                List<Coordinate> ccc = new List<Coordinate>();
                foreach (var item in coordinates[0])
                {
                    ccc.Add(new Coordinate(item[0], item[1]));
                }
                IPolygon polygon = geometryFactory.CreatePolygon(ccc.ToArray());
                return (Geometry)polygon.Normalized().Reverse();
            }
            set
            {
                List<double[]> points = new List<double[]>();
                foreach (var c in value.Coordinates)
                    points.Add(new double[] { c.X, c.Y });
                double[][][] result = new double[1][][] { points.ToArray() };

                coordinates = result;
            }

        }
        public string type { get; set; }
    }

}
