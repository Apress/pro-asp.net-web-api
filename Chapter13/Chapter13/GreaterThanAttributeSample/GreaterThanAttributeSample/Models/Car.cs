using System;
using GreaterThanAttributeSample.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreaterThanAttributeSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }

        public DateTime SalesStartsAt { get; set; }

        [GreaterThan("SalesStartsAt")]
        public DateTime SalesEndsAt { get; set; }
    }
}