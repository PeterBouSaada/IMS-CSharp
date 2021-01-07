using Client.Interfaces;
using System;
using System.Collections.Generic;
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
        private string bearerToken;

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
            Dictionary<string, string> request = ParseDataToDictionary(data);
            foreach(KeyValuePair<string, string> pair in request)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }
            var content = new StringContent(request.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            Console.WriteLine(response.StatusCode + ": " + response.Content);
            return response.StatusCode;
        }

        public HttpStatusCode PutRequest(T data, string url)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> ParseDataToDictionary(T data)
        {
            PropertyInfo[] dataInfo = data.GetType().GetProperties();

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach(PropertyInfo property in dataInfo)
            {
                if(property.GetValue(data) != null)
                {
                    result.Add(property.Name, property.GetValue(data).ToString());
                }
            }

            return result;

        }

    }
}
