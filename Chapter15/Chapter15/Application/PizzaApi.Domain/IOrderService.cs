using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaApi.Domain
{
    public interface IOrderService
    {
        void Save(Order order);
        Order Get(int id);
        IEnumerable<Order> GetAll();
        void Update(Order order);
        void Delete(int id);
        bool Exists(int id);
    }
}
