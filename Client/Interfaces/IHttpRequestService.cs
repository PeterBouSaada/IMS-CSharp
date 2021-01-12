using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IHttpRequestService<T>
    {
        public Task<HttpResponseMessage> GetRequest(T data, string url);
        public Task<HttpResponseMessage> PostRequest(T data, string url);
        public HttpStatusCode DeleteRequest(T data, string url);
        public HttpStatusCode PutRequest(T data, string url);
        public Task<HttpStatusCode> tokenRequest(T data);
    }
}
