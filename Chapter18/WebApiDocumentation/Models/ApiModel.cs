using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using WebApiDocumentation.Entities;

namespace WebApiDocumentation.Models {
    public class ApiModel {
        private IApiExplorer _explorer;

        public ApiModel(IApiExplorer explorer) {
            if (explorer == null) {
                throw new ArgumentNullException("explorer");
            }
            _explorer = explorer;
        }

        public ILookup<string, ApiDescription> GetApis() {
            return _explorer.ApiDescriptions.ToLookup(
                api => api.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        private static readonly Dictionary<string, Type> _typeMappings = 
            new Dictionary<string, Type>() {
                { @"GETapi/Cars/{id}", typeof (Car) }
        };

        private Type GetResponseType(ApiDescription api) {
            Type t;
            return _typeMappings.TryGetValue(api.ID, out t) 
                ? t : api.ActionDescriptor.ReturnType;
        }


        private static readonly List<Car> _cars = new List<Car>() {
            new Car() {
                        Id = 17,
                        Make = "VW",
                        Model = "Golf",
                        Year = 1999,
                        Price = 1500f
                    },
            new Car() {
                        Id = 24,
                        Make = "Porsche",
                        Model = "911",
                        Year = 2011,
                        Price = 100000f
                    }
        };

        private Dictionary<Type, object> _sampleData =
            new Dictionary<Type, object>() {
                                               {typeof (Car), _cars[0]},
                                               {typeof (List<Car>), _cars}
                                           };

        public string GetSampleResponseBody(ApiDescription api, string mediaType) {
            string body = null;
            Type returnType = GetResponseType(api);

            object o;
            if (returnType != null && _sampleData.TryGetValue(returnType, out o)) {
                var formatters = api.SupportedResponseFormatters;

                MediaTypeFormatter formatter = formatters.FirstOrDefault(
                    f => f.SupportedMediaTypes.Any(m => m.MediaType == mediaType));

                if (formatter != null) {
                    var content = new ObjectContent(returnType, o, formatter);
                    body = content.ReadAsStringAsync().Result;
                }
            }
            return body;
        }
    }
}