using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignalREngine.Enums;

namespace SignalREngine.Common
{
    public class StandardHttpClient : IHttpClient
    {
        private System.Net.Http.HttpClient _client;

        public StandardHttpClient()
        {
            _client = new System.Net.Http.HttpClient();
        }
        public async Task<string> PostAsync<T>(string uri, T item, string authorizationToken = null, HttpAuthorizationMethod authorizationMethod = HttpAuthorizationMethod.Bearer)
        {
            return await PostAsync(uri, item, false, authorizationToken, authorizationMethod);
        }

        public async Task<string> PostAsync<T>(string uri, T item, bool ignoreNullProperty, string authorizationToken = null, HttpAuthorizationMethod authorizationMethod = HttpAuthorizationMethod.Bearer)
        {
            string body = string.Empty;
            if (ignoreNullProperty)
            {
                body = JsonConvert.SerializeObject(item, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            }
            else
            {
                body = JsonConvert.SerializeObject(item);
            }
            var httpRequestMessage = HttpClientHelper.CreateHttpRequest(HttpMethod.Post, uri, body, authorizationToken, authorizationMethod);
            var response = await _client.SendAsync(httpRequestMessage);
            //response.VerifyResponse();
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }


        public void AddHeader(string key, string value)
        {
            if (_client.DefaultRequestHeaders.Contains(key))
            {
                _client.DefaultRequestHeaders.Remove(key);
            }
            _client.DefaultRequestHeaders.Add(key, value);
        }
    }
}
