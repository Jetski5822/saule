using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Saule.Http
{
    public static class RequestExtensions
    {
        public static IDictionary<string, StringValues> GetQueryNameValuePairs(this HttpRequest request)
        {
            return request.Query.ToDictionary(q => q.Key, q => q.Value);
        }
    }
}
