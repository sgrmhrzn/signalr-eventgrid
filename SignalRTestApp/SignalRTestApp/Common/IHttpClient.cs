using SignalRTestApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTestApp.Common
{
    internal interface IHttpClient
    {
        Task<string> PostAsync<T>(string uri, T item, string authorizationToken = null, HttpAuthorizationMethod authorizationMethod = HttpAuthorizationMethod.Bearer);

        void AddHeader(string key, string value);
    }
}
