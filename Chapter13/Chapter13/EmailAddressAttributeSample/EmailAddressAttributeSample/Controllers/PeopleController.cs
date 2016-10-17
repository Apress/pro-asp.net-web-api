using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmailAddressAttributeSample.Models;
using EmailAddressAttributeSample.Filters;

namespace EmailAddressAttributeSample.APIs {

    [InvalidModelStateFilter]
    public class PeopleController : ApiController {

        private readonly PeopleContext _peopleCtx = new PeopleContext();
	
        // GET /api/people
        public IEnumerable<Person> Get() {

            return _peopleCtx.All;
        }

        // POST /api/people
        public HttpResponseMessage PostPerson(Person person) {

            var createdPerson = _peopleCtx.Add(person);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdPerson);
            response.Headers.Location = new Uri(
                Url.Link("DefaultHttpRoute", new { id = createdPerson.Id }));

            return response;
        }
    }
}