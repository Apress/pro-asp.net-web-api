using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaApi.Domain
{
    public class InMemoryOrderService : IOrderService
    {

        private Dictionary<int, Order> _orders = new Dictionary<int, Order>(); 
        private Random _random = new Random();
        private readonly IPricingService _pricingService;

        public InMemoryOrderService(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public void Save(Order order)
        {
            if (order.Id == 0)
                order.Id = _random.Next(); // assign random Id if a new order

            order.TotalPrice = _pricingService.GetPrice(order);

            _orders.Add(order.Id, order);
        }

        public Order Get(int id)
        {
            Order order;
            _orders.TryGetValue(id, out order);
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders.Values;
        }

        public void Update(Order order)
        {
            order.TotalPrice = _pricingService.GetPrice(order);
            _orders[order.Id] = order;
        }

        public void Delete(int id)
        {
            _orders.Remove(id);
        }

        public bool Exists(int id)
        {
            return _orders.ContainsKey(id);
        }
    }
}
