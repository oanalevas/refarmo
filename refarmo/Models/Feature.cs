using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace refarmo.Models
{
    public class Feature
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [ForeignKey("FeatureCollection")]
        public int FeatureCollectionId { get; set; }

        [JsonProperty(PropertyName = "properties")]        
        public FeatureProperties Properties { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public MyGeometry Geometry { get; set; }
        public FeatureCollection FeatureCollection { get; set; }
    }

}
