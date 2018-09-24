using SignalREngine.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SignalREngine.Common
{
    internal static class HttpClientHelper
    {
        internal static HttpRequestMessage CreateHttpRequest(HttpMethod httpMethod, string uri, string body, string authorizationToken, HttpAuthorizationMethod authorizationMethod)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, uri);
            if (authorizationToken != null)
            {
                switch (authorizationMethod)
                {
                    case HttpAuthorizationMethod.Bearer:
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                        break;
                    case HttpAuthorizationMethod.AzureFunction:
                        requestMessage.Headers.Add("x-functions-key", authorizationToken);
                        break;
                    case HttpAuthorizationMethod.Basic:
                        requestMessage.Headers.Add("api-key", authorizationToken);
                        break;
                    default:
                        break;
                }

            }
            if (body != null)
            {
                var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
                requestMessage.Content = httpContent;
            }

            return requestMessage;
        }

        //internal static void VerifyResponse(this HttpResponseMessage responseMessage)
        //{
        //    if (!responseMessage.IsSuccessStatusCode)
        //    {
        //        throw new HttpClientException(responseMessage);
        //    }
        //}
    }
}
