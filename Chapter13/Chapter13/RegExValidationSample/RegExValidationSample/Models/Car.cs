using System.ComponentModel.DataAnnotations;

namespace RegExValidationSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Make { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Range(0, 500000)]
        public float Price { get; set; }

        [RegularExpression("([^\\s]+(\\.(?i)(jpg|png|gif|bmp))$)")]
        public string ImageName { get; set; }
    }
}