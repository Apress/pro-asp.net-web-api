using System.ComponentModel.DataAnnotations;

namespace EmailAddressAttributeSample.Models {

    public class Person {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}