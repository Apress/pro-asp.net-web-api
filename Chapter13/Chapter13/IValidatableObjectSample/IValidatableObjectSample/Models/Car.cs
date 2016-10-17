using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IValidatableObjectSample.Models {

    public class Car : IValidatableObject {

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext) {

            if (Year < 2010 && Price > 250000F) {

                yield return new ValidationResult(
                    "The Price cannot be above 250000 if the Year value is lower than 2010.", 
                    new string[] { "Price" });
            }

            yield return ValidationResult.Success;
        }
    }
}