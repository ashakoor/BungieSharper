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
        /// Returns the partner sku and offer history of the targeted user. Elevated permissions are required to see users that are not yourself.
        /// </summary>
        public async Task<IEnumerable<Schema.Tokens.PartnerOfferSkuHistoryResponse>> Tokens_GetPartnerOfferSkuHistory(int partnerApplicationId, long targetBnetMembershipId, string authToken = null)
        {
            return await _apiAccessor.ApiRequestAsync<IEnumerable<Schema.Tokens.PartnerOfferSkuHistoryResponse>>(
                new Uri($"Tokens/Partner/History/{partnerApplicationId}/{targetBnetMembershipId}/", UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer
                ).ConfigureAwait(false);
        }
    }
}