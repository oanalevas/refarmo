using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace refarmo.Models
{

    public class FeatureCollection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public Crs crs { get; set; }

        public ICollection<Feature> features { get; set; }
    }

}
