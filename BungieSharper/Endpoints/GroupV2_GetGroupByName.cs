﻿using BungieSharper.Client;
using System;
<<<<<<< HEAD
using System.Net.Http;
=======
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
>>>>>>> rewrite
using System.Threading;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        /// <summary>
        /// Get information about a specific group with the given name and type.
        /// </summary>
<<<<<<< HEAD
        public async Task<Schema.GroupsV2.GroupResponse> GroupV2_GetGroupByName(string groupName, Schema.GroupsV2.GroupType groupType, string authToken = null, CancellationToken cancelToken = default)
        {
            return await _apiAccessor.ApiRequestAsync<Schema.GroupsV2.GroupResponse>(
=======
        /// <param name="groupName">Exact name of the group to find.</param>
        /// <param name="groupType">Type of group to find.</param>
        public async Task<Entities.GroupsV2.GroupResponse> GroupV2_GetGroupByName(string groupName, Entities.GroupsV2.GroupType groupType, string? authToken = null, CancellationToken cancelToken = default)
        {
            return await _apiAccessor.ApiRequestAsync<Entities.GroupsV2.GroupResponse>(
>>>>>>> rewrite
                new Uri($"GroupV2/Name/{Uri.EscapeDataString(groupName)}/{groupType}/", UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer, cancelToken
                ).ConfigureAwait(false);
        }
    }
}