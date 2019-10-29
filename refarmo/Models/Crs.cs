using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace refarmo.Models
{
    public class Crs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public int FeatureCollectionId { get; set; }
        public FeatureCollection FeatureCollection { get; set; }
    }

}
