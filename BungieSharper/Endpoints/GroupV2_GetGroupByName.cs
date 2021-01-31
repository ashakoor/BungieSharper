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
        /// Get information about a specific group with the given name and type.
        /// </summary>
        public async Task<Schema.GroupsV2.GroupResponse> GroupV2_GetGroupByName(string groupName, Schema.GroupsV2.GroupType groupType, string authToken = null, CancellationToken cancelToken = default)
        {
            return await _apiAccessor.ApiRequestAsync<Schema.GroupsV2.GroupResponse>(
                new Uri($"GroupV2/Name/{Uri.EscapeDataString(groupName)}/{groupType}/", UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer, cancelToken
                ).ConfigureAwait(false);
        }
    }
}