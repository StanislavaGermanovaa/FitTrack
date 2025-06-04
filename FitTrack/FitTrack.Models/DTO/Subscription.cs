
using MessagePack;

namespace FitTrack.Models.DTO
{
    [MessagePackObject]
    public class Subscription : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public string UserId { get; set; }

        [Key(2)]
        public string Type { get; set; }

        [Key(3)]
        public DateTime StartDate { get; set; }

        [Key(4)]
        public DateTime EndDate { get; set; }

        [Key(5)]
        public bool IsActive { get; set; }

        [Key(6)]
        public DateTime UpdatedAt { get; set; }

        [Key(7)]
        public DateTime DateInserted { get; set; }

        public string GetKey() => Id;
    }
}
