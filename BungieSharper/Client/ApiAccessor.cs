using BungieSharper.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BungieSharper.Client
{
    internal class ApiAccessor : IDisposable
    {
        private const string BaseUrl = "https://stats.bungie.net/Platform/";
        private const byte SimultaneousRequests = 2;

        private readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _semaphore;
        private readonly JsonSerializerOptions _serializerOptions;
        private TimeSpan _msPerRequest;
        private HashSet<PlatformErrorCodes> _retryErrorCodes;

        internal string ApiKey
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Remove("X-API-Key");
                _httpClient.DefaultRequestHeaders.Add("X-API-Key", value);
            }
        }

        internal string? UserAgent
        {
            set
            {
                _httpClient.DefaultRequestHeaders.Remove("User-Agent");

                if (value != null)
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", value);
            }
        }

        internal HashSet<PlatformErrorCodes> RetryErrorCodes
        {
            set => _retryErrorCodes = value;
        }

        internal byte RequestsPerSecond
        {
            set => _msPerRequest = TimeSpan.FromMilliseconds(1000.0 / value * SimultaneousRequests);
        }

        public void Dispose()
        {
            _httpClient.CancelPendingRequests();
            _httpClient.Dispose();
            _semaphore.Dispose();
        }

        internal ApiAccessor()
        {
            _semaphore = new SemaphoreSlim(SimultaneousRequests, SimultaneousRequests);
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new JsonLongConverter());

            var cookieContainer = new CookieContainer();
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true,
                MaxConnectionsPerServer = SimultaneousRequests
            };

            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(BaseUrl, UriKind.Absolute)
            };

            _retryErrorCodes = new HashSet<PlatformErrorCodes>
            {
                PlatformErrorCodes.ThrottleLimitExceeded,
                PlatformErrorCodes.ThrottleLimitExceededMinutes,
                PlatformErrorCodes.ThrottleLimitExceededMomentarily,
                PlatformErrorCodes.ThrottleLimitExceededSeconds,
                PlatformErrorCodes.PerEndpointRequestThrottleExceeded,
                PlatformErrorCodes.PerApplicationThrottleExceeded,
                PlatformErrorCodes.PerApplicationAnonymousThrottleExceeded,
                PlatformErrorCodes.PerApplicationAuthenticatedThrottleExceeded,
                PlatformErrorCodes.PerUserThrottleExceeded,
                PlatformErrorCodes.DestinyThrottledByGameServer
            };
        }

        internal async Task<T> ApiRequestAsync<T>(Uri uri, HttpContent? httpContent, HttpMethod httpMethod, string? authToken, CancellationToken cancelToken)
        {
            var semaphoreTask = _semaphore.WaitAsync(cancelToken).ConfigureAwait(false);
            var httpRequestMessage = HttpRequestGenerator.MakeApiRequestMessage(uri, httpContent, httpMethod, authToken);
            await semaphoreTask;

            while (true)
            {
                var throttleTask = Task.Delay(_msPerRequest, cancelToken);
                var httpResponseMessage = await GetApiResponseAsync(httpRequestMessage, cancelToken).ConfigureAwait(false);

                if (cancelToken.IsCancellationRequested)
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw new OperationCanceledException();
                }

                if (httpResponseMessage.Content.Headers.ContentType?.MediaType != "application/json")
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw ContentNotJsonException.NewContentNotJsonException(httpResponseMessage);
                }

                var apiResponse = JsonSerializer.Deserialize<Entities.ApiResponse<T>>(
                    await httpResponseMessage.Content.ReadAsStringAsync(cancelToken).ConfigureAwait(false),
                    _serializerOptions);

                if (apiResponse is null)
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw ContentNullJsonException.NewContentNullJsonException(httpResponseMessage);
                }

                if (apiResponse.ErrorCode != PlatformErrorCodes.Success)
                {
                    if (ApiRetry(apiResponse.ErrorCode))
                    {
                        await throttleTask.ConfigureAwait(false);
                        await Task.Delay(apiResponse.ThrottleSeconds, cancelToken).ConfigureAwait(false);
                    }
                    else
                    {
                        await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                        throw NonRetryErrorCodeException.NewNonRetryErrorCodeException(apiResponse);
                    }
                }
                else
                {
                    if (apiResponse.Response == null)
                    {
                        await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                        throw NullResponseException.NewNullResponseException(apiResponse);
                    }

                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    return apiResponse.Response;
                }
            }
        }

        internal async Task<Entities.TokenResponse> ApiTokenRequestResponseAsync(Uri uri, HttpContent httpContent, HttpMethod httpMethod, CancellationToken cancelToken)
        {
            var semaphoreTask = _semaphore.WaitAsync(cancelToken).ConfigureAwait(false);
            var httpRequestMessage = HttpRequestGenerator.MakeApiRequestMessage(uri, httpContent, httpMethod);
            await semaphoreTask;

            while (true)
            {
                var throttleTask = Task.Delay(_msPerRequest, cancelToken);
                var httpResponseMessage = await GetApiResponseAsync(httpRequestMessage, cancelToken).ConfigureAwait(false);

                if (cancelToken.IsCancellationRequested)
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw new OperationCanceledException();
                }

                if (httpResponseMessage.Content.Headers.ContentType?.MediaType != "application/json")
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw ContentNotJsonException.NewContentNotJsonException(httpResponseMessage);
                }

                var apiResponse = JsonSerializer.Deserialize<Entities.TokenResponse>(
                    await httpResponseMessage.Content.ReadAsStringAsync(cancelToken).ConfigureAwait(false),
                    _serializerOptions);

                if (apiResponse is null)
                {
                    await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                    throw ContentNullJsonException.NewContentNullJsonException(httpResponseMessage);
                }

                await AwaitThrottleAndReleaseSemaphore(throttleTask, _semaphore).ConfigureAwait(false);
                return apiResponse;
            }
        }

        internal async Task<Stream> GetStream(Uri uri, CancellationToken cancelToken)
        {
            var requestMsg = HttpRequestGenerator.MakeApiRequestMessage(uri, null, HttpMethod.Get);
            var response = await GetApiResponseAsync(requestMsg, cancelToken);

            return await response.Content.ReadAsStreamAsync(cancelToken);
        }

        internal async Task<string> GetString(Uri uri, CancellationToken cancelToken)
        {
            var requestMsg = HttpRequestGenerator.MakeApiRequestMessage(uri, null, HttpMethod.Get);
            var response = await GetApiResponseAsync(requestMsg, cancelToken);

            return await response.Content.ReadAsStringAsync(cancelToken);
        }

        private Task<HttpResponseMessage> GetApiResponseAsync(HttpRequestMessage request, CancellationToken cancelToken)
        {
            return _httpClient.SendAsync(request, cancelToken);
        }

        private static async Task AwaitThrottleAndReleaseSemaphore(Task throttleTask, SemaphoreSlim semaphore)
        {
            await throttleTask.ConfigureAwait(false);
            semaphore.Release();
        }

        private bool ApiRetry(PlatformErrorCodes errorCode)
        {
            return _retryErrorCodes.Contains(errorCode);
        }
    }
}