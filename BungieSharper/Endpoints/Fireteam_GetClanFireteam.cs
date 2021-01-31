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
        /// Gets a specific clan fireteam.
        /// </summary>
        public async Task<Schema.Fireteam.FireteamResponse> Fireteam_GetClanFireteam(long fireteamId, long groupId, string authToken = null, CancellationToken cancelToken = default)
        {
            return await _apiAccessor.ApiRequestAsync<Schema.Fireteam.FireteamResponse>(
                new Uri($"Fireteam/Clan/{groupId}/Summary/{fireteamId}/", UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer, cancelToken
                ).ConfigureAwait(false);
        }
    }
}