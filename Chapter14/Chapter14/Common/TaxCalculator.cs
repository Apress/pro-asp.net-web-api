using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetWebApi.DependencyResolution
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal Calculate(decimal amount)
        {
            return amount * 0.2m; // assuming 20% VAT
        }
    }
}