using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaApi.Domain;

namespace PizzaApi.Domain
{
    public class SimplePricingService : IPricingService
    {
        public decimal GetPrice(Order order)
        {
            const decimal BasePriceForAnyPizza = 7.5m;
            const decimal ValueAddedTax = 0.20m;

            // price of the pizza is calculated by the base price plus 
            // 0.5 for every character in the name of the pizza! 


            var priceBeforeTax = order.Items.Sum(x => x.Quantity * 
                (BasePriceForAnyPizza + 0.5m*x.Name.Length));
                                 
            return priceBeforeTax + (priceBeforeTax * ValueAddedTax);
        }
    }
}