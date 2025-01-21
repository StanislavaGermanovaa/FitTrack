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
        public ActionResult<IEnumerable<SubscriptionResponse>> GetAllSubscriptions()
        {
            try
            {
                var subscriptions = _subscriptionService.GetAllSubscriptions();
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
        public IActionResult GetSubscriptionById(string id)
        {
            var subscription = _subscriptionService.GetSubscriptionById(id);

            if (subscription == null)
            {
                return NotFound();
            }

            var subscriptionResponse = _mapper.Map<SubscriptionResponse>(subscription);
            return Ok(subscriptionResponse);
        }

        [HttpPost("AddSubscription")]
        public ActionResult<SubscriptionResponse> AddSubscription([FromBody] SubscriptionRequest subscriptionRequest)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var subscription = _mapper.Map<Subscription>(subscriptionRequest);
            _subscriptionService.CreateSubscription(subscription);
            var subscriptionResponse = _mapper.Map<SubscriptionResponse>(subscription);

            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscriptionResponse);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteSubscription(string id)
        {
            _subscriptionService.DeleteSubscription(id);
            return NoContent();
        }
    }
}

