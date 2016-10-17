using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncDatabaseCall.Models {

    public class Car {

        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
    }
}