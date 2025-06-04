
using MessagePack;

namespace FitTrack.Models.DTO
{
    [MessagePackObject]
    public class User : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public string FirstName { get; set; }

        [Key(2)]
        public string LastName { get; set; }

        [Key(3)]
        public string Email { get; set; }

        [Key(4)]
        public string PhoneNumber { get; set; }

        [Key(5)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Key(6)]
        public DateTime UpdatedAt { get; set; }

        [Key(7)]
        public DateTime DateInserted { get; set; }

        public string GetKey() => Id;
    }
}
