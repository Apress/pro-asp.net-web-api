using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataAnnotationValidationAttributesSample.Models {

    [DataContract]
    public class Car {

        public int Id { get; set; }

        [Required]
        [StringLength(
            maximumLength: 20,
            ErrorMessageResourceName = 
                "StringLengthAttribute_ValidationError",
            ErrorMessageResourceType = 
                typeof(ValidationErrors)
        )]
        public string Make { get; set; }

        [Required]
        [StringLength(
            maximumLength: 20, MinimumLength = 5,
            ErrorMessageResourceName = 
                "StringLengthAttribute_ValidationErrorIncludingMinimum",
            ErrorMessageResourceType = 
                typeof(ValidationErrors)
        )]
        public string Model { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        public int Year { get; set; }

        public float Price { get; set; }
    }
}