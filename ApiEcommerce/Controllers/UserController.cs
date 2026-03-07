using System.Collections.Generic;
using System.Threading.Tasks;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {  
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }
        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} was not found!");
            }

            var userDto = _mapper.Map<ProductDto>(user);
            return Ok(userDto);
        }

        [HttpPost(Name ="RegisterUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody]CreateUserDto createUserDto)
        {
            if(createUserDto == null|| !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(createUserDto.Username))
            {
                return BadRequest("Username is required");
            }
            if (!_userRepository.IsUniqueUser(createUserDto.Username))
            {
                return BadRequest("User already exists!");
            }
            var result = await _userRepository.Register(createUserDto);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong creating the user!");
            }

            return CreatedAtRoute("GetUser", new { id = result.Id }, result);
        }
        [HttpPost("Login",Name ="LoginUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody]UserLoginDto userLoginDto)
        {
            if(userLoginDto == null|| !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userRepository.Login(userLoginDto);
            if(user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
