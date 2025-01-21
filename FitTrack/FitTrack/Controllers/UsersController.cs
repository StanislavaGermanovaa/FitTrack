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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UserResponse>> GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(string id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }

        [HttpPost("AddUser")]
        public ActionResult<UserResponse> AddUser([FromBody] UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userRequest);
            _userService.CreateUser(user);
            var userResponse = _mapper.Map<UserResponse>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userResponse);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(string id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}

