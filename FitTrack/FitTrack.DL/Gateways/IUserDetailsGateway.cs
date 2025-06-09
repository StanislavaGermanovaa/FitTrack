

using FitTrack.Models.Response;

namespace FitTrack.DL.Gateways
{
    public interface IUserDetailsGateway
    {
        Task<UserExtraInfoResponse> GetUserExtraInfo(string userId);
    }
}
