﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        public async Task<IEnumerable<Schema.Forum.ForumRecruitmentDetail>> Forum_GetRecruitmentThreadSummaries()
        {
            return await this._apiAccessor.ApiRequestAsync<IEnumerable<Schema.Forum.ForumRecruitmentDetail>>(
                $"Forum/Recruit/Summaries/", null, null, HttpMethod.Post
                );
        }
    }
}