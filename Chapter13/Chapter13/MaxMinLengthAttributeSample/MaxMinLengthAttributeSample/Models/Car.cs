using System.ComponentModel.DataAnnotations;

namespace MaxMinLengthAttributeSample.Models {

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

        [MinLength(1)]
        [MaxLength(4)]
        public string[] Tags { get; set; }
    }
}