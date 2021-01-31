﻿using BungieSharper.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        /// <summary>
        /// Returns paginated lists of trending items for a category.
        /// </summary>
        public async Task<Schema.SearchResultOfTrendingEntry> Trending_GetTrendingCategory(string categoryId, int pageNumber, string authToken = null, CancellationToken cancelToken = default)
        {
            return await _apiAccessor.ApiRequestAsync<Schema.SearchResultOfTrendingEntry>(
                new Uri($"Trending/Categories/{Uri.EscapeDataString(categoryId)}/{pageNumber}/", UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer, cancelToken
                ).ConfigureAwait(false);
        }
    }
}