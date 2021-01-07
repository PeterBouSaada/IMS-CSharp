using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IHttpRequestService<T>
    {
        public List<T> GetRequest(T data, string url);
        public Task<HttpStatusCode> PostRequest(T data, string url);
        public HttpStatusCode DeleteRequest(T data, string url);
        public HttpStatusCode PutRequest(T data, string url);
    }
}
