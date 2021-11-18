using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PlayStoreTopGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);

            if (!result) return BadRequest("EMail is exist!!!");
            
            return Ok(new { status = true, errors = "" });
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery login)
        {
            var token = await _mediator.Send(login);

            if (token == null) return BadRequest("Wrong mail or password");
            
            return Ok(token);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAll()
        {
            var query =  new GetUsersQuery();
            var result = await _mediator.Send(query);

            if (result == null) return BadRequest();
            
            return Ok(result);
        }
    }
}
