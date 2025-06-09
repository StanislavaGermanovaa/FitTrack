using Microsoft.AspNetCore.Mvc;

namespace UserDetailsAPI.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class UserInfoController : ControllerBase
        {
            private static readonly string[] Levels = new[]
            {
            "Beginner", "Intermediate", "Advanced", "Athlete"
        };

            [HttpGet("{userId}")]
            public IActionResult GetUserLevel(string userId)
            {
                var random = Random.Shared.Next(Levels.Length);
                return Ok(new
                {
                    UserId = userId,
                    Level = Levels[random],
                    RecommendedCalories = 2000 + random * 200
                });
            }
        }
    }
