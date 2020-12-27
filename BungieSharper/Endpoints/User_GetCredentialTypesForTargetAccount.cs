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
        /// Returns a list of credential types attached to the requested account
        /// </summary>
        public async Task<IEnumerable<Schema.User.Models.GetCredentialTypesForAccountResponse>> User_GetCredentialTypesForTargetAccount(long membershipId)
        {
            return await _apiAccessor.ApiRequestAsync<IEnumerable<Schema.User.Models.GetCredentialTypesForAccountResponse>>(
                new Uri($"User/GetCredentialTypesForTargetAccount/{membershipId}/", UriKind.Relative),
                null, null, HttpMethod.Get
                ).ConfigureAwait(false);
        }
    }
}