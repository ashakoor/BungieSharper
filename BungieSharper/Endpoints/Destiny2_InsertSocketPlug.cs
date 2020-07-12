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
        /// This is a preview method.
        /// Insert a plug into a socketed item. I know how it sounds, but I assure you it's much more G-rated than you might be guessing. We haven't decided yet whether this will be able to insert plugs that have side effects, but if we do it will require special scope permission for an application attempting to do so. You must have a valid Destiny Account, and either be in a social space, in orbit, or offline. Request must include proof of permission for 'InsertPlugs' from the account owner.
        /// </summary>
        public async Task<Schema.Destiny.Responses.DestinyItemChangeResponse> Destiny2_InsertSocketPlug(Schema.Destiny.Requests.Actions.DestinyInsertPlugsActionRequest requestBody)
        {
            return await _apiAccessor.ApiRequestAsync<Schema.Destiny.Responses.DestinyItemChangeResponse>(
                new Uri($"Destiny2/Actions/Items/InsertSocketPlug/", UriKind.Relative),
                null, new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json"), HttpMethod.Post
                ).ConfigureAwait(false);
        }
    }
}