using kitchen_api.Database;
using kitchen_api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kitchen_api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly RecipeContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(RecipeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest loginUserRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginUserRequest.Username);
        
            if(user is null)
                return BadRequest("Username or password is incorrect");
            
            var passwordHash = Database.Entities.User.HashPassword(loginUserRequest.Password);

            if (user.Password != passwordHash)
            {
                return Unauthorized("Password is incorrect.");
            }

            var token = Database.Entities.User.GenerateToken(user.Id.ToString());
        
            return Ok( new { token });
        }
    }
}