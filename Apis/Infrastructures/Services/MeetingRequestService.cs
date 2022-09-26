using Application.Interfaces;
using AutoMapper;
using Global.Shared.ViewModels.MeetingRequestViewModels;
using Global.Shared.ViewModels.MeetingRequestViewModels.Internal;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class MeetingRequestService : IMeetingRequestService
    {
        private const string MICROSOFT_GRAPH_BASE_URL = "https://graph.microsoft.com";

        private readonly IOAuth2AccessTokenAcquirer _accessTokenAcquirer;

        private readonly IMapper _mapper;

        private readonly HttpClient _httpClient;

        private static readonly string[] Scopes = new string[]
        {
            $"{MICROSOFT_GRAPH_BASE_URL}/Calendars.ReadWrite",
            $"{MICROSOFT_GRAPH_BASE_URL}/User.Read",
             "offline_access"
        };

        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            AllowTrailingCommas = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public MeetingRequestService(
            IOAuth2AccessTokenAcquirer accessTokenAcquirer,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _accessTokenAcquirer = accessTokenAcquirer;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> CreateMeetingRequestAsync(
            CreateMeetingRequestViewModel createMeetingRequest,
            IDeviceCodeNotifier deviceCodeNotifier)
        {
            var accessToken = await _accessTokenAcquirer
                                        .GetAccessTokenAsync(
                                            Scopes,
                                            createMeetingRequest.OrganizerEmail,
                                            deviceCodeNotifier);

            return await CreateMeetingAsync(createMeetingRequest, accessToken);
        }

        private async Task<string> CreateMeetingAsync(
            CreateMeetingRequestViewModel createMeetingRequest,
            string accessToken)
        {
            var microsoftGraphCreateMeetingRequest = 
                _mapper.Map<MicrosoftGraphCreateMeetingRequest>(createMeetingRequest);
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"{MICROSOFT_GRAPH_BASE_URL}/v1.0/me/events"),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(microsoftGraphCreateMeetingRequest, options: SerializerOptions)
            };
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var microsoftGraphCreateMeetingResponse =
                JsonSerializer.Deserialize<MicrosoftGraphCreateMeetingResponse>(
                    httpResponseContent, SerializerOptions);
            return microsoftGraphCreateMeetingResponse!.OnlineMeeting.JoinUrl;
        }
    }
}
