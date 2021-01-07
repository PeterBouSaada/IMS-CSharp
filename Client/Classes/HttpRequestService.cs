using Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class HttpRequestService<T> : IHttpRequestService<T>
    {
        private HttpClient httpClient;
        private string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InBib3VzYWFkYSIsIm5iZiI6MTYxMDA0NzAxNCwiZXhwIjoxNjEwMTMzNDE0LCJpYXQiOjE2MTAwNDcwMTR9.jZMEM4Ovbylg6ZpRiBq45v3cYPTfMhkmFh01GpIL4As";

        public HttpRequestService(HttpClient client)
        {
            httpClient = client;
        }

        public HttpStatusCode DeleteRequest(T data, string url)
        {
            throw new NotImplementedException();
        }

        public List<T> GetRequest(T data, string url)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> PostRequest(T data, string url)
        {
            if(bearerToken == null && url != "https://localhost:5001/API/user/authenticate")
            {
                return HttpStatusCode.Unauthorized;
            }

            if(bearerToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }
            string request = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            string ret = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK && url == "https://localhost:5001/API/user/authenticate")
            {
                bearerToken = ret;
            }
            Console.WriteLine(ret);
            return response.StatusCode;
        }

        public HttpStatusCode PutRequest(T data, string url)
        {
            throw new NotImplementedException();
        }

    }
}
