using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PizzaApi.Domain;

namespace PizzaApi.Api.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public HttpResponseMessage Post(HttpRequestMessage request, Order order)
        {
            if (!order.Items.Any())
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Order has no items");

            if(_orderService.Exists(order.Id))
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Order already exists");

            _orderService.Save(order);
            var response = request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.Id }));
            return response;
        }

        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            if (!_orderService.Exists(id))
                return request.CreateErrorResponse(HttpStatusCode.NotFound, "Order does not exist");

            return request.CreateResponse(HttpStatusCode.OK, _orderService.Get(id));
        }

        public IEnumerable<Order> Get()
        {
            return _orderService.GetAll();
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            if (!_orderService.Exists(id))
                return request.CreateErrorResponse(HttpStatusCode.NotFound, "Order does not exist");

            _orderService.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }


        public HttpResponseMessage Put(HttpRequestMessage request, Order order)
        {
            if (!_orderService.Exists(order.Id))
                return request.CreateErrorResponse(HttpStatusCode.NotFound, "Order does not exist");

            _orderService.Update(order);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}