using Microsoft.AspNetCore.Mvc;
using FitTrack.BL.Interfaces;
using FitTrack.Models.DTO;
using FitTrack.Models.Request;
using FitTrack.Models.Response;
using MapsterMapper;
using FitTrack.DL.Kafka;
using FitTrack.DL.Gateways;

namespace FitTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IKafkaProducer<User> _kafkaProducer;
        private readonly IUserDetailsGateway _userDetailsGateway;


        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger, IKafkaProducer<User> kafkaProducer, IUserDetailsGateway userDetailsGateway)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _kafkaProducer = kafkaProducer;
            _userDetailsGateway = userDetailsGateway;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var userResponses = users.Select(user => _mapper.Map<UserResponse>(user));
                return Ok(userResponses);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetAll {e.Message}-{e.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<UserResponse>> AddUser([FromBody] UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userRequest);
            await _userService.CreateUserAsync(user);
            var userResponse = _mapper.Map<UserResponse>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userResponse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("PublishUserToKafka")]
        public async Task<IActionResult> PublishUserToKafka([FromBody] UserRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userRequest);

            await _kafkaProducer.Produce(user);

            return Ok("User published to Kafka topic.");
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetUserWithDetails(string id)
        {
            var extra = await _userDetailsGateway.GetUserExtraInfo(id);

            if (extra == null)
            {
                return NotFound();
            }

            return Ok(extra);
        }

    }
}

