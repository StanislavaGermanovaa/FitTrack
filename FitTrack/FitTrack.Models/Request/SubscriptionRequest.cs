
namespace FitTrack.Models.Request
{
    public class SubscriptionRequest
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
