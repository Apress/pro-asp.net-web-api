using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaApi.Domain
{
    public class Order
    {
        public Order()
            : this(new OrderItem[0])
        {

        }

        public Order(IEnumerable<OrderItem> orderItems)
        {
            Items = new List<OrderItem>(orderItems);            
        }
        
        public IEnumerable<OrderItem> Items { get; set; }

        public decimal TotalPrice { get; set; }

        public string CustomerName { get; set; }

        public int Id { get; set; }
    }
}
