using Microsoft.AspNetCore.Mvc;
using FitTrack.BL.Interfaces;
using FitTrack.Models.DTO;
using FitTrack.Models.Request;
using FitTrack.Models.Response;
using MapsterMapper;

namespace FitTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        private readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(ISubscriptionService subscriptionService, IMapper mapper, ILogger<SubscriptionsController> logger)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SubscriptionResponse>>> GetAllSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
                var subscriptionResponses = subscriptions.Select(subscription => _mapper.Map<SubscriptionResponse>(subscription));
                return Ok(subscriptionResponses);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetAll {e.Message}-{e.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetSubscriptionById(string id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);

            if (subscription == null)
            {
                return NotFound();
            }

            var subscriptionResponse = _mapper.Map<SubscriptionResponse>(subscription);
            return Ok(subscriptionResponse);
        }

        [HttpPost("AddSubscription")]
        public async Task<ActionResult<SubscriptionResponse>> AddSubscription([FromBody] SubscriptionRequest subscriptionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subscription = _mapper.Map<Subscription>(subscriptionRequest);
            await _subscriptionService.CreateSubscriptionAsync(subscription);
            var subscriptionResponse = _mapper.Map<SubscriptionResponse>(subscription);

            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscriptionResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSubscription(string id)
        {
            await _subscriptionService.DeleteSubscriptionAsync(id);
            return NoContent();
        }
    }
}

