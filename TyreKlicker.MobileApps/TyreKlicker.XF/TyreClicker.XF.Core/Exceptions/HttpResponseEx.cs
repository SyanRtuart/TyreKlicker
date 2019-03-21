using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;

namespace TyreKlicker.XF.Core.Exceptions
{
    public class HttpResponseEx : Exception
    {
        public HttpStatusCode HttpCode { get; }

        public string Code { get; set; }

        public string Scenario { get; set; }

        public HttpResponseEx(HttpStatusCode code, string message)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());

            var httpExceptionResponse = JsonConvert.DeserializeObject<ErrorDetails>(message, serializerSettings);

            Code = httpExceptionResponse.Code;
            Scenario = httpExceptionResponse.Scenario;
            HttpCode = code;
        }
    }
}