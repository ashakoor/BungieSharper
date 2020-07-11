﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        /// <summary>
        /// Get information about the groups that a given member has joined.
        /// </summary>
        public async Task<Schema.GroupsV2.GetGroupsForMemberResponse> GroupV2_GetGroupsForMember(Schema.GroupsV2.GroupsForMemberFilter filter, Schema.GroupsV2.GroupType groupType, long membershipId, Schema.BungieMembershipType membershipType)
        {
            return await this._apiAccessor.ApiRequestAsync<Schema.GroupsV2.GetGroupsForMemberResponse>(
                $"GroupV2/User/{membershipType}/{membershipId}/{filter}/{groupType}/", null, null, HttpMethod.Get
                );
        }
    }
}