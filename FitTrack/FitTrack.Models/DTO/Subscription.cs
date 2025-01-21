

namespace FitTrack.Models.DTO
{
    public class Subscription
    {
        public string Id { get; set; } 
        public string UserId { get; set; } 
        public string Type { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
