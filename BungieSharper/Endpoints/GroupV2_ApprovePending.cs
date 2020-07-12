﻿using BungieSharper.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        /// <summary>
        /// Approve the given membershipId to join the group/clan as long as they have applied.
        /// </summary>
        public async Task<bool> GroupV2_ApprovePending(long groupId, long membershipId, Schema.BungieMembershipType membershipType, Schema.GroupsV2.GroupApplicationRequest requestBody)
        {
            return await _apiAccessor.ApiRequestAsync<bool>(
                new Uri($"GroupV2/{groupId}/Members/Approve/{membershipType}/{membershipId}/", UriKind.Relative),
                null, new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"), HttpMethod.Post
                ).ConfigureAwait(false);
        }
    }
}