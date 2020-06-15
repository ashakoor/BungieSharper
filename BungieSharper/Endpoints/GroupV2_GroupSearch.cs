﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        public async Task<Schema.GroupsV2.GroupSearchResponse> GroupV2_GroupSearch()
        {
            return await this._apiAccessor.ApiRequestAsync<Schema.GroupsV2.GroupSearchResponse>(
                $"GroupV2/Search/", null, null, HttpMethod.Post
                );
        }
    }
}