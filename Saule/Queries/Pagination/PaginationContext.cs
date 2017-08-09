using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Saule.Queries.Pagination
{
    internal class PaginationContext
    {
        public PaginationContext(IEnumerable<KeyValuePair<string, StringValues>> filters, int perPage)
        {
            var keyValuePairs = filters as IList<KeyValuePair<string, StringValues>> ?? filters.ToList();

            var dictionary = keyValuePairs.ToDictionary(kv => kv.Key.ToLowerInvariant(), kv => kv.Value.ToString().ToLowerInvariant());
            ClientFilters = dictionary;
            Page = GetNumber();
            PerPage = perPage;
        }

        public int Page { get; }

        public int PerPage { get; }

        public IDictionary<string, string> ClientFilters { get; }

        public override string ToString()
        {
            return $"page[number]={Page}&page[size]={PerPage}";
        }

        private int GetNumber()
        {
            if (!ClientFilters.ContainsKey(Constants.QueryNames.PageNumber))
            {
                return 0;
            }

            int result;
            var isNumber = int.TryParse(ClientFilters[Constants.QueryNames.PageNumber], out result);

            return isNumber ? result : 0;
        }
    }
}