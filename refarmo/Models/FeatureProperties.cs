using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace refarmo.Models
{
    public class FeatureProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public int area { get; set; }

        public string FeatureId { get; set; }
        public Feature Feature { get; set; }
    }

}
