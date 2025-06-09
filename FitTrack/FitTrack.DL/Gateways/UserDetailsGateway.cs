using FitTrack.Models.Response;
using RestSharp;

namespace FitTrack.DL.Gateways
{
    public class UserDetailsGateway : IUserDetailsGateway
    {
        private readonly RestClient _client;

        public UserDetailsGateway()
        {
            _client = new RestClient("https://localhost:7159");
        }

        public async Task<UserExtraInfoResponse> GetUserExtraInfo(string userId)
        {
            var request = new RestRequest($"/UserInfo/{userId}", Method.Get);
            var response = await _client.ExecuteAsync<UserExtraInfoResponse>(request);

            return response.Data;
        }
    }
}
