using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VehicleTender.API.Helpers
{
    public class ErrorHandler
    {
        int errorCode;
        public ErrorHandler(Exception error)
        {
            errorCode = error.HResult;
        }

        public IHttpActionResult HandleError()
        {
            HttpResponseMessage response;
            if (errorCode == -2146233087 || errorCode == -2146233088)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest) 
                {
                    Content = new StringContent("Record already exists!"),
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(response);
            }
            response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Server problem!"),
                StatusCode = HttpStatusCode.InternalServerError
            };
            throw new HttpResponseException(response);
        }
    }
}