﻿using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Saule.Queries;
using Saule.Queries.Pagination;

namespace Saule.Http
{
    /// <summary>
    /// Indicates that the returned collection must be paginated. If the collection
    /// implements <see cref="IQueryable{T}"/>, the query will be executed efficiently.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PaginatedAttribute : ActionFilterAttribute
    {
        private int _perPage = 10;

        /// <summary>
        /// Gets or sets the number of items to return per response.
        /// </summary>
        public int PerPage
        {
            get
            {
                return _perPage;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(PerPage), value, "Must have at least one item per page.");
                }

                _perPage = value;
            }
        }

        /// <summary>
        /// See base class documentation.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var context = new PaginationContext(
                actionContext.HttpContext.Request.GetQueryNameValuePairs(),
                PerPage);

            var query = GetQueryContext(actionContext);

            query.Pagination = context;
            base.OnActionExecuting(actionContext);
        }

        private static QueryContext GetQueryContext(ActionExecutingContext actionContext)
        {
            var hasQuery = actionContext.HttpContext.Request.Properties.ContainsKey(Constants.PropertyNames.QueryContext);
            QueryContext query;

            if (hasQuery)
            {
                query = actionContext.HttpContext.Request.Properties[Constants.PropertyNames.QueryContext]
                    as QueryContext;
            }
            else
            {
                query = new QueryContext();
                actionContext.HttpContext.Request.Properties.Add(Constants.PropertyNames.QueryContext, query);
            }

            return query;
        }
    }
}
