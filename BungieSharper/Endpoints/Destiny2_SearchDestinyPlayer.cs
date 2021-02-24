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
        /// Returns a list of Destiny memberships given a full Gamertag or PSN ID. Unless you pass returnOriginalProfile=true, this will return membership information for the users' Primary Cross Save Profile if they are engaged in cross save rather than any original Destiny profile that is now being overridden.
        /// </summary>
        /// <param name="displayName">The full gamertag or PSN id of the player. Spaces and case are ignored.</param>
        /// <param name="membershipType">A valid non-BungieNet membership type, or All.</param>
        /// <param name="returnOriginalProfile">(optional) If passed in and set to true, we will return the original Destiny Profile(s) linked to that gamertag, and not their currently active Destiny Profile.</param>
        public Task<IEnumerable<Entities.User.UserInfoCard>> Destiny2_SearchDestinyPlayer(string displayName, Entities.BungieMembershipType membershipType, bool? returnOriginalProfile = null, string? authToken = null, CancellationToken cancelToken = default)
        {
            return _apiAccessor.ApiRequestAsync<IEnumerable<Entities.User.UserInfoCard>>(
                new Uri($"Destiny2/SearchDestinyPlayer/{membershipType}/{Uri.EscapeDataString(displayName)}/" + HttpRequestGenerator.MakeQuerystring(returnOriginalProfile != null ? $"returnOriginalProfile={returnOriginalProfile}" : null), UriKind.Relative),
                null, HttpMethod.Get, authToken, AuthHeaderType.Bearer, cancelToken
                );
        }
    }
}