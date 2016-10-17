using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PizzaApi.Domain;

namespace PizzaApi.Api.Controllers
{
    public class OrderItemController : ApiController
    {
        private IOrderService _orderService;

        public OrderItemController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<OrderItem> GetItems(int id)
        {
            return _orderService.Get(id)
                                .Items;
        }

        public OrderItem GetItems(int id, string name)
        {
            return _orderService.Get(id)
                                .Items.FirstOrDefault(x => x.Name == name);
        }

    }
}