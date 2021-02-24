﻿using BungieSharper.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BungieSharper.Endpoints
{
    public partial class Endpoints
    {
        /// <summary>
        /// Transfer an item to/from your vault. You must have a valid Destiny account. You must also pass BOTH a reference AND an instance ID if it's an instanced item. itshappening.gif
        /// Requires OAuth2 scope(s): MoveEquipDestinyItems
        /// </summary>
        public Task<int> Destiny2_TransferItem(Entities.Destiny.Requests.DestinyItemTransferRequest requestBody, string? authToken = null, CancellationToken cancelToken = default)
        {
            return _apiAccessor.ApiRequestAsync<int>(
                new Uri($"Destiny2/Actions/Items/TransferItem/", UriKind.Relative),
                new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"), HttpMethod.Post, authToken, AuthHeaderType.Bearer, cancelToken
                );
        }
    }
}