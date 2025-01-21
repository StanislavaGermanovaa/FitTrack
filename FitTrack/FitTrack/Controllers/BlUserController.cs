using FitTrack.Models.Response;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using FitTrack.BL.Interfaces;

namespace FitTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlUserController : ControllerBase
    {
        private readonly IBlUserService _businessService;
        private readonly IMapper _mapper;
        private readonly ILogger<BlUserController> _logger;

        public BlUserController(IBlUserService businessService, IMapper mapper, ILogger<BlUserController> logger)
        {
            _businessService = businessService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetUserSubscriptions/{userId}")]
        public IActionResult GetUserSubscriptions(string userId)
        {
            try
            {
                var subscriptions = _businessService.GetUserWithSubscriptions(userId);

                if (subscriptions == null || !subscriptions.Any())
                {
                    return NotFound($"No subscriptions found for user with ID {userId}.");
                }

                var response = subscriptions.Select(sub => _mapper.Map<SubscriptionResponse>(sub));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUserSubscriptions: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

